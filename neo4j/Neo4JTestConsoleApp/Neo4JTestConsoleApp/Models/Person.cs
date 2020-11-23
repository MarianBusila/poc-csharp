using Newtonsoft.Json;

namespace Neo4JTestConsoleApp.Models
{
    public class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("born")]
        public int Born { get; set; }
    }
}
