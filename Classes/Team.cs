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

        // Properties to track team statistics
        [JsonProperty]
        public int PointsWon { get; set; } // Total points won by the team

        [JsonProperty]
        public int PointsLost { get; set; } // Total points lost by the team

        [JsonProperty]
        public int ManCoverageCount { get; set; } // Total times played man coverage

        [JsonProperty]
        public int ZoneCoverageCount { get; set; } // Total times played zone coverage

        [JsonProperty]
        public bool LastPointWon { get; set; } // Whether the team won the last point

        [JsonProperty]
        public bool LastPointManCoverage { get; set; } // Whether man coverage was played on the last point

        [JsonProperty]
        public int EloRating { get; set; } // Elo rating for the team

        public Team()
        {
            PlayerIds = new List<Guid>();
            PointsWon = 0;
            PointsLost = 0;
            ManCoverageCount = 0;
            ZoneCoverageCount = 0;
            LastPointWon = false;
            LastPointManCoverage = false;
            EloRating = 1500; // Default Elo rating (you can adjust this as needed)
        }

        // Method to update Elo rating based on match outcome
        public void UpdateEloRating(Team opponent, bool won)
        {
            double k = 32; // K-factor for Elo rating adjustments (you can adjust this as needed)

            double winProbability = 1.0 / (1.0 + Math.Pow(10, (opponent.EloRating - EloRating) / 400.0));

            double result = won ? 1.0 : 0.0;
            double newEloRating = EloRating + k * (result - winProbability);

            EloRating = (int)newEloRating;
        }
    }
}
