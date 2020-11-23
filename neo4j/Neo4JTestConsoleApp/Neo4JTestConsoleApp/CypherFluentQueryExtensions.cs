using Microsoft.Extensions.Logging;
using Neo4jClient.Cypher;

namespace Neo4JTestConsoleApp
{
    public static class CypherFluentQueryExtensions
    {
        public static ICypherFluentQuery<T> LogQuery<T>(this ICypherFluentQuery<T> query, ILogger logger)
        {
            logger.LogInformation("Query: {query}", query.Query.QueryText);
            return query;
        }

        public static ICypherFluentQuery LogQuery(this ICypherFluentQuery query, ILogger logger)
        {
            logger.LogInformation("Query: {query}", query.Query.QueryText);
            return query;
        }
    }
}
