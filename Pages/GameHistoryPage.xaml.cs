// GameHistoryContentDialog.xaml.cs
using CP4.Classes;
using CP4.Pages;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CP4.Dialogs
{
    public sealed partial class GameHistoryContentDialog : ContentDialog
    {
        public GameHistoryContentDialog()
        {
            this.InitializeComponent();
            LoadTeams();
        }

        private void LoadTeams()
        {
            // Load a list of teams from your TeamManager class
            List<Team> teams = TeamManager.LoadTeams();

            // Bind the list of teams to the ComboBox
            TeamComboBox.ItemsSource = teams;
            TeamComboBox.DisplayMemberPath = "Name"; // Display the team names in the ComboBox
        }

        private async void SavePlayerButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Get the player's name from the TextBox
            string playerName = PlayerNameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                // Get the selected team from the ComboBox
                Team selectedTeam = TeamComboBox.SelectedItem as Team;

                if (selectedTeam != null)
                {
                    // Create a new Player object
                    Player newPlayer = new Player
                    {
                        Id = Guid.NewGuid(),
                        Name = playerName,
                        TeamId = selectedTeam.Id, // Set the TeamId property
                        TeamName = selectedTeam.Name // Set the TeamName property
                    };

                    // Save the player using PlayerManager
                    await PlayerManager.SavePlayer(newPlayer);

                    // Close the ContentDialog
                    Hide();

                    // Refresh the player list in the EditTeamPage
                    ((EditTeamPage)((Frame)Window.Current.Content).Content).RefreshPlayerList();
                }
                else
                {
                    // Show an error message or handle the case where no team is selected
                }
            }
            else
            {
                // Show an error message or handle the case where the player's name is empty
            }
        }


        private void CancelButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Close the ContentDialog without saving
            Hide();
        }
    }
}
