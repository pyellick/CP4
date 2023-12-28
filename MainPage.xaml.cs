using System;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation;
using CP4.Pages;


namespace CP4
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SetExclusiveFullScreen();
        }

        private void FadeIn(UIElement element)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25) // Adjust the duration as needed
            };

            Storyboard.SetTarget(fadeInAnimation, element);
            Storyboard.SetTargetProperty(fadeInAnimation, "Opacity");

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Begin();
        }

        private void FadeOut(UIElement element)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25) // Adjust the duration as needed
            };

            Storyboard.SetTarget(fadeOutAnimation, element);
            Storyboard.SetTargetProperty(fadeOutAnimation, "Opacity");

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeOutAnimation);
            storyboard.Completed += (sender, e) =>
            {
                element.Visibility = Visibility.Collapsed;
            };

            storyboard.Begin();
        }

        private void GamesButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesButton.IsChecked == true)
            {
                // Fade in the "NEW GAME" and "GameSheet" buttons
                NewGameButton.Visibility = Visibility.Visible;
                FadeIn(NewGameButton);

                GameSheetButton.Visibility = Visibility.Visible;
                FadeIn(GameSheetButton);

                GameHistoryButton.Visibility = Visibility.Visible;
                FadeIn(GameHistoryButton);

                // Disable the TeamsButton if GamesButton is toggled on
                TeamsButton.IsEnabled = false;
            }
            else
            {
                // Fade out the "NEW GAME" and "GameSheet" buttons
                FadeOut(NewGameButton);
                FadeOut(GameSheetButton);
                FadeOut(GameHistoryButton);

                // Enable the TeamsButton
                TeamsButton.IsEnabled = true;
            }
        }

        private void TeamsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsButton.IsChecked == true)
            {
                // Fade in the "AddTeamButton" and "EditTeamButton"
                AddTeamButton.Visibility = Visibility.Visible;
                FadeIn(AddTeamButton);

                EditTeamButton.Visibility = Visibility.Visible;
                FadeIn(EditTeamButton);

                // Disable the GamesButton if TeamsButton is toggled on
                GamesButton.IsEnabled = false;
            }
            else
            {
                // Fade out the "AddTeamButton" and "EditTeamButton"
                FadeOut(AddTeamButton);
                FadeOut(EditTeamButton);

                // Enable the GamesButton
                GamesButton.IsEnabled = true;
            }
        }




        private void GameSheetButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }



        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewGamePage));
        }

        private void GameHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void AddTeamButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddTeamDialog();
            await dialog.ShowAsync();
        }




        private void EditTeamButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the TeamListPage when the "Edit Team" button is clicked
            Frame.Navigate(typeof(TeamListPage));
        }






        private void SetExclusiveFullScreen()
        {
            ApplicationView view = ApplicationView.GetForCurrentView();
            view.TryEnterFullScreenMode();

            // Hide the title bar and window decorations
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
            titleBar.ButtonForegroundColor = Windows.UI.Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.Transparent;

            // Hide the system back button (optional)
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            // Set the PreferredLaunchWindowingMode to ExclusiveFullScreen
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }



    }



}
