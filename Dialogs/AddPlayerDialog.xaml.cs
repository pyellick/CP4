using CP4.Classes;
using CP4.Pages;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CP4.Dialogs
{
    public sealed partial class AddPlayerDialog : ContentDialog
    {
        private Guid currentTeamId; // Add a private field to store the currently open team ID

        public AddPlayerDialog(Guid teamId)
        {
            this.InitializeComponent();

            // Set the selected team in the ComboBox based on the provided teamId
            currentTeamId = teamId;
        }

        private async void SavePlayerButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Get the player's name from the TextBox
            string playerName = PlayerNameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                // Check if a valid team ID is available
                if (currentTeamId != Guid.Empty)
                {
                    // Create a new Player object
                    Player newPlayer = new Player
                    {
                        Id = Guid.NewGuid(),
                        Name = playerName,
                        TeamId = currentTeamId, // Assign the current team ID
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
                    // Show an error message or handle the case where the team ID is invalid
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
