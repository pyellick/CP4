using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CP4.Classes
{
    [JsonObject]
    public class Team
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string PrimaryColor { get; set; }

        [JsonProperty]
        public string SecondaryColor { get; set; }

        [JsonProperty]
        public string LogoImagePath { get; set; }

        // Add additional properties for editable team information here

        [JsonProperty]
        public List<Guid> PlayerIds { get; set; } // Store the IDs of players in the team
        public List<string> PlayerNames { get; set; } // Store the names of players in the team


        public Team()
        {
            PlayerIds = new List<Guid>();
        }
    }
}
