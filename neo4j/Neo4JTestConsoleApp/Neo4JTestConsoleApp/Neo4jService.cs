using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Neo4jClient.Cypher;
using Neo4JTestConsoleApp.Models;

namespace Neo4JTestConsoleApp
{
    public class Neo4jService : BackgroundService
    {
        private readonly ILogger<Neo4jService> _logger;
        private readonly IGraphClient _graphClient;

        public Neo4jService(ILogger<Neo4jService> logger, IGraphClient graphClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string tomCruise = "Tom Cruise";
            // add a movie 
            var missionImpossibleFallout = new Movie { Rating = 6, Released = 2018, Title = "Mission Impossible Fallout" };
            IEnumerable<string> properties = missionImpossibleFallout.GetType().GetProperties().Where(x => x.GetValue(missionImpossibleFallout) != null).Select(x => $"{x.Name.ToLower()}: ${x.Name}");
            string propertiesStr = string.Join(", ", properties);
            await _graphClient.Cypher.Merge($"(:Movie {{ {propertiesStr} }})")
                .WithParams(missionImpossibleFallout)
                .LogQuery(_logger)
                .ExecuteWithoutResultsAsync();

            // add a relationship
            await _graphClient.Cypher
                .Match("(p:Person), (m:Movie)")
                .Where((Person p, Movie m) => p.Name == tomCruise && m.Title == missionImpossibleFallout.Title)
                .Merge("(p)-[:ACTED_IN]->(m)")
                .LogQuery(_logger)
                .ExecuteWithoutResultsAsync();

            // movies where Tom Cruise acted in
            var results = await _graphClient.Cypher
                .Match("(p:Person)-[:ACTED_IN]->(m:Movie)")
                .Where((Person p) => p.Name == tomCruise)
                .Return( (p, m) => new 
                {
                    Person = p.As<Person>(),
                    MoviesActedIn = m.CollectAs<Movie>()
                })
                .LogQuery(_logger)
                .ResultsAsync;

            _logger.LogInformation("Found {count} results", results.Count());

            foreach (var result in results)
            {
                _logger.LogInformation("Person {@person} acted in", result.Person);
                foreach (Movie movie in result.MoviesActedIn)
                {
                    _logger.LogInformation("movie {@movie} ", movie);
                }
            }


        }

    }
}
