using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Serilog;

namespace Neo4JTestConsoleApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger());
                    });
                    var neo4jConfiguration = new Neo4jConfiguration();
                    context.Configuration.Bind("neo4j", neo4jConfiguration);
                    var neo4jClient = new GraphClient(new Uri(neo4jConfiguration.HttpEndpoint), neo4jConfiguration.Username, neo4jConfiguration.Password);
                    neo4jClient.ConnectAsync();
                    services.AddSingleton<IGraphClient>(neo4jClient);
                    services.AddHostedService<Neo4jService>();
                })
                .RunConsoleAsync();
        }
    }

}
