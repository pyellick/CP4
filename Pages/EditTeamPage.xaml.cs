using CP4.Classes;
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
        }

        private void LoadTeamInformation()
        {
            Guid teamId = navigationParams.TeamId;

            // Load team information
            TeamNameText.Text = navigationParams.TeamName;

            // Load players for the team and associate their team names
            List<Player> players = PlayerManager.GetPlayersByTeamId(teamId);
            foreach (Player player in players)
            {
                player.TeamName = navigationParams.TeamName;
            }

            // Now you have a list of players with their associated team names
            // You can use this list to display player information as needed
            PlayerListView.ItemsSource = players;
        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle adding a new player to the team
        }
    }
}
