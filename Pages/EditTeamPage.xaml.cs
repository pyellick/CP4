using CP4.Classes;
using CP4.Dialogs;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static CP4.Pages.TeamListPage;



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


        public void RefreshPlayerList()
        {
            LoadPlayersByTeamId(navigationParams.TeamId);
        }


        private async void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            var gameHistoryDialog = new GameHistoryContentDialog();
            await gameHistoryDialog.ShowAsync();

        }




    }
}
