using CP4.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CP4.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameHistoryPage : Page
    {
        public GameHistoryPage()
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


        private async void SavePlayerButton_Click(object sender, RoutedEventArgs e)
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

                    // Navigate back to the previous page or perform any other desired action
                    Frame.GoBack();
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



    }
}
