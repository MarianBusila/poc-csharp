using Newtonsoft.Json;

namespace Neo4JTestConsoleApp.Models
{
    public class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("released")]
        public int Released { get; set; }
    
    }
}
