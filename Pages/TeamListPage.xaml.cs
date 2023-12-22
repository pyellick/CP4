using CP4.Classes;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CP4.Pages
{
    public sealed partial class TeamListPage : Page
    {
        public TeamListPage()
        {
            this.InitializeComponent();
            LoadTeams();
        }

        private void NavigateToMainPage()
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void LoadTeams()
        {
            // Load the list of teams using the TeamManager class
            List<Team> teams = TeamManager.LoadTeams();

            foreach (Team team in teams)
            {
                // Create a StackPanel to hold team information
                StackPanel teamPanel = new StackPanel();
                teamPanel.Margin = new Windows.UI.Xaml.Thickness(0, 30, 0, 0);
                teamPanel.Tag = team.Id;

                // Create a Grid to hold the team name, Edit button, and Delete button
                Grid teamGrid = new Grid();
                teamGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                teamGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                teamGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                // Display the team name in a TextBlock with white text color, larger font size, and semi-light font weight
                TextBlock teamText = new TextBlock();
                teamText.Text = team.Name;
                teamText.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                teamText.FontSize = 60;
                teamText.FontStyle = Windows.UI.Text.FontStyle.Italic;
                teamText.FontWeight = Windows.UI.Text.FontWeights.SemiLight;

                // Create an Edit button for each team
                Button editButton = new Button();
                editButton.Content = "Edit";
                editButton.Style = (Style)Application.Current.Resources["CustomEditButtonStyle"];
                editButton.Click += (sender, e) => EditTeamButtonClick(sender, e, team.Id);
                editButton.HorizontalAlignment = HorizontalAlignment.Right;
                editButton.Margin = new Thickness(700, 0, 0, 0);

                // Create a Delete button for each team
                Button deleteButton = new Button();
                deleteButton.Content = "Delete";
                deleteButton.Style = (Style)Application.Current.Resources["CustomDeleteButtonStyle"];
                deleteButton.Click += (sender, e) => DeleteTeamButtonClick(sender, e, team.Id);
                deleteButton.HorizontalAlignment = HorizontalAlignment.Right;
                deleteButton.Margin = new Thickness(100, 0, 0, 0);



                // Add the team name, Edit button, and Delete button to the Grid
                Grid.SetColumn(teamText, 0);
                Grid.SetColumn(editButton, 1);
                Grid.SetColumn(deleteButton, 2);
                teamGrid.Children.Add(teamText);
                teamGrid.Children.Add(editButton);
                teamGrid.Children.Add(deleteButton);

                // Add the Grid to the StackPanel
                teamPanel.Children.Add(teamGrid);

                // Add the StackPanel to the TeamListStackPanel
                TeamListStackPanel.Children.Add(teamPanel);
            }
        }

        private void EditTeamButtonClick(object sender, RoutedEventArgs e, Guid teamId)
        {
            // Find the team by its ID
            Team teamToEdit = TeamManager.GetTeamById(teamId);

            if (teamToEdit != null)
            {
                // Load the players associated with the team
                List<Player> playersWithTeam = PlayerManager.GetPlayersByTeamId(teamId);


                // Navigate to the EditTeamPage and pass the team and associated players as parameters
                Frame.Navigate(typeof(EditTeamPage), new TeamNavigationParams
                {
                    TeamId = teamId,
                    TeamName = teamToEdit.Name,
                    Players = playersWithTeam
                });
            }
        }





        public class TeamNavigationParams
        {
            public Guid TeamId { get; set; }
            public string TeamName { get; set; }
            public List<Player> Players { get; set; } // Add this property

        }


        private void DeleteTeamButtonClick(object sender, RoutedEventArgs e, Guid teamId)
        {
            // Remove the team from the UI
            StackPanel teamPanelToRemove = null;
            foreach (StackPanel panel in TeamListStackPanel.Children)
            {
                if (panel.Tag is Guid && (Guid)panel.Tag == teamId)
                {
                    teamPanelToRemove = panel;
                    break;
                }
            }

            if (teamPanelToRemove != null)
            {
                TeamListStackPanel.Children.Remove(teamPanelToRemove);
            }

            // Remove the team from the database
            TeamManager.DeleteTeam(teamId);
        }



        // Handle the Back button click to navigate to MainPage.xaml
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}
