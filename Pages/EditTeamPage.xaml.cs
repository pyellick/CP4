using CP4.Classes;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static CP4.Pages.TeamListPage;
using CP4.Dialogs; // Add this using directive


namespace CP4.Pages
{
    public sealed partial class EditTeamPage : Page
    {
        private TeamNavigationParams navigationParams;

        public EditTeamPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is TeamNavigationParams)
            {
                navigationParams = (TeamNavigationParams)e.Parameter;
                LoadTeamInformation();
            }

            // Load players with the same team ID
            LoadPlayersByTeamId(navigationParams.TeamId);
        }

        private void LoadPlayersByTeamId(Guid teamId)
        {
            List<Player> players = PlayerManager.GetPlayersByTeamId(teamId);

            // Set the ItemsSource of PlayerListView to the list of players
            PlayerListView.ItemsSource = players;
        }


        private void LoadTeamInformation()
        {
            Guid teamId = navigationParams.TeamId;

            // Load team information
            TeamNameText.Text = navigationParams.TeamName;

            // Load players for the specific team (based on the teamId)
            List<Player> players = PlayerManager.GetPlayersByTeamId(teamId);

            // Set the ItemsSource of PlayerListView to the list of players for the specific team
            PlayerListView.ItemsSource = players;
        }




        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void LoadPlayerList()
        {
            // Load the updated list of players and refresh the ListView or UI
            Guid teamId = navigationParams.TeamId;
            List<Player> players = PlayerManager.GetPlayersByTeamId(teamId);
            PlayerListView.ItemsSource = players;
        }


        private async void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerDialog = new AddPlayerContentDialog(navigationParams.TeamId, navigationParams.TeamName);
            var result = await addPlayerDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // Check if PlayerName is not null (user didn't cancel)
                if (!string.IsNullOrEmpty(addPlayerDialog.PlayerName))
                {
                    // Create a new Player object and set its properties
                    Player newPlayer = new Player
                    {
                        Id = Guid.NewGuid(), // Generate a new GUID for the player
                        Name = addPlayerDialog.PlayerName,
                        TeamId = addPlayerDialog.TeamId
                    };

                    // Save the new player
                    await PlayerManager.SavePlayer(newPlayer);

                    // Reload the player list or update the UI as needed
                    LoadPlayerList(); // Call the LoadPlayerList method to update the player list
                }
            }
        }




    }
}
