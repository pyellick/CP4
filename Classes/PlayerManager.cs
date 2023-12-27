
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace CP4.Classes
{
    public class PlayerManager
    {
        private const string PlayersKey = "SavedPlayers";

        public static async Task SavePlayer(Player player)
        {
            // Load existing players from local storage
            List<Player> players = await LoadPlayersAsync();

            // Add the new player to the list
            players.Add(player);

            // Serialize the list of players to JSON
            string serializedPlayers = JsonConvert.SerializeObject(players);

            // Save the serialized players to local storage
            ApplicationData.Current.LocalSettings.Values[PlayersKey] = serializedPlayers;
        }

        public static List<Player> GetPlayersByTeamId(Guid teamId)
        {
            List<Player> allPlayers = LoadPlayers();

            // Filter players by the specified team ID
            List<Player> filteredPlayers = allPlayers.Where(player => player.TeamId == teamId).ToList();

            return filteredPlayers;
        }


        public static List<Player> GetPlayersWithTeamIds()
        {
            List<Player> allPlayers = LoadPlayers();

            // Optionally load the team IDs associated with players
            // You can add this information to your Player class based on your data structure

            return allPlayers;
        }


        public static async Task<List<Player>> LoadPlayersAsync()
        {
            return await Task.Run(() =>
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(PlayersKey))
                {
                    // Deserialize the list of players from JSON
                    string serializedPlayers = ApplicationData.Current.LocalSettings.Values[PlayersKey].ToString();
                    return JsonConvert.DeserializeObject<List<Player>>(serializedPlayers);
                }
                else
                {
                    // No players stored, return an empty list
                    return new List<Player>();
                }
            });
        }

        public static List<Player> LoadPlayers()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(PlayersKey))
            {
                // Deserialize the list of players from JSON
                string serializedPlayers = ApplicationData.Current.LocalSettings.Values[PlayersKey].ToString();
                return JsonConvert.DeserializeObject<List<Player>>(serializedPlayers);
            }
            else
            {
                // No players stored, return an empty list
                return new List<Player>();
            }
        }

        public static List<Player> GetPlayersWithTeamNames()
        {
            List<Player> allPlayers = LoadPlayers();
            List<Team> allTeams = TeamManager.LoadTeams();

            // Update player's TeamName property by matching TeamId with Team objects
            foreach (Player player in allPlayers)
            {
                Team associatedTeam = allTeams.FirstOrDefault(team => team.Id == player.TeamId);
                if (associatedTeam != null)
                {
                    player.TeamName = associatedTeam.Name;
                }
            }

            return allPlayers;
        }


        private static void SavePlayers(List<Player> players)
        {
            // Serialize the list of players to JSON
            string serializedPlayers = JsonConvert.SerializeObject(players);

            // Save the serialized players to local storage
            ApplicationData.Current.LocalSettings.Values[PlayersKey] = serializedPlayers;
        }




        public static Guid GetPlayerId(string playerName)
        {
            List<Player> players = LoadPlayers();
            Player player = players.FirstOrDefault(p => p.Name == playerName);
            return player != null ? player.Id : Guid.Empty; // Return Guid.Empty if player not found
        }

        public static void DeletePlayer(Guid playerId)
        {
            // Load existing players from local storage
            List<Player> players = LoadPlayers();

            // Find the player with the specified playerId
            Player playerToDelete = players.FirstOrDefault(player => player.Id == playerId);

            if (playerToDelete != null)
            {
                // Remove the player from the list
                players.Remove(playerToDelete);

                // Serialize the updated list of players to JSON and save it to local storage
                string serializedPlayers = JsonConvert.SerializeObject(players);
                ApplicationData.Current.LocalSettings.Values[PlayersKey] = serializedPlayers;
            }
        }

    }
}

