using Newtonsoft.Json;
using System;

namespace CP4.Classes
{
    [JsonObject]
    public class Player
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonIgnore]
        public Guid TeamId { get; set; }

        // Add a property to store the associated team's name
        [JsonIgnore]
        public string TeamName { get; set; }
    }
}
