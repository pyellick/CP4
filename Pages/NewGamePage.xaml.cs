using CP4.Classes;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CP4.Pages
{
    public sealed partial class NewGamePage : Page
    {
        public NewGamePage()
        {
            this.InitializeComponent();
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            List<Player> players = PlayerManager.GetPlayersWithTeamIds();

            // Set the list of players as the data source for the ListView
            PlayerListView.ItemsSource = players;
        }

        private void DeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;

            if (deleteButton != null)
            {
                // Get the player's ID from the button's Tag property
                if (deleteButton.Tag is Guid playerId)
                {
                    // Delete the player from the database
                    PlayerManager.DeletePlayer(playerId);

                    // Refresh the player list
                    LoadPlayers();
                }
            }
        }
    }
}
