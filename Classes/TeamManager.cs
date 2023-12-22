using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Newtonsoft.Json;

namespace CP4.Classes
{
    public class TeamManager
    {
        private const string TeamsKey = "SavedTeams";

        public static void SaveTeam(Team team)
        {
            // Load existing teams from local storage
            List<Team> teams = LoadTeams();

            // Add the new team to the list
            teams.Add(team);

            // Serialize the list of teams to JSON and save it to local storage
            string serializedTeams = JsonConvert.SerializeObject(teams);
            ApplicationData.Current.LocalSettings.Values[TeamsKey] = serializedTeams;
        }

        public static List<Team> LoadTeams()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(TeamsKey))
            {
                // Deserialize the list of teams from JSON
                string serializedTeams = ApplicationData.Current.LocalSettings.Values[TeamsKey].ToString();
                return JsonConvert.DeserializeObject<List<Team>>(serializedTeams);
            }
            else
            {
                // No teams stored, return an empty list
                return new List<Team>();
            }
        }

        public static Team GetTeamByName(string teamName)
        {
            List<Team> teams = LoadTeams();
            return teams.FirstOrDefault(t => t.Name == teamName);
        }


        public static Team GetTeamById(Guid teamId)
        {
            List<Team> teams = LoadTeams();
            return teams.FirstOrDefault(t => t.Id == teamId);
        }



        public static void DeleteTeam(Guid teamId)
        {
            List<Team> teams = LoadTeams();
            Team teamToDelete = teams.FirstOrDefault(t => t.Id == teamId);
            if (teamToDelete != null)
            {
                teams.Remove(teamToDelete);
                string serializedTeams = Newtonsoft.Json.JsonConvert.SerializeObject(teams);
                ApplicationData.Current.LocalSettings.Values[TeamsKey] = serializedTeams;
            }
        }
    }
}
