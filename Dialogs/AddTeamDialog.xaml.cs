using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.UI.Xaml.Media;
using CP4.Classes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using System.Linq;
using Windows.UI.Xaml.Controls.Primitives;
using System.Globalization;
using Microsoft.Toolkit.Uwp.UI;


namespace CP4
{
    public sealed partial class AddTeamDialog : ContentDialog
    {
        private List<Team> teams; // Define the teams list at the class level
        private string selectedLogoImagePath = ""; // Declare a variable to store the selected logo path

        // Declare SolidColorBrush fields
        private SolidColorBrush PrimaryColorBrush;
        private SolidColorBrush SecondaryColorBrush;

        public AddTeamDialog()
        {
            InitializeComponent();
            teams = new List<Team>();
            LogoFlipView.SelectionChanged += LogoFlipView_SelectionChanged;

            // Access the brushes using FindResource
            PrimaryColorBrush = MyContentDialog.FindResource("PrimaryColorBrush") as SolidColorBrush;
            SecondaryColorBrush = MyContentDialog.FindResource("SecondaryColorBrush") as SolidColorBrush;

        }



        private Dictionary<Color, string> colorNames = new Dictionary<Color, string>
        {
    
             { Colors.White, "White" },
             { Colors.Black, "Black" },
             { Colors.Gray, "Gray" },
             { Windows.UI.Color.FromArgb(0xFF, 0x39, 0x59, 0x9F), "Manatee Blue" },
             { Windows.UI.Color.FromArgb(0xFF, 0x16, 0x35, 0x3F), "Great River Blue" },
             { Windows.UI.Color.FromArgb(0xFF, 0x1B, 0xA9, 0x4A), "Eagan Green" },
             { Windows.UI.Color.FromArgb(0xFF, 0x16, 0x68, 0x37), "Mounds View Green" },
             { Windows.UI.Color.FromArgb(0xFF, 0x15, 0x64, 0x33), "Blake Green" },
             { Windows.UI.Color.FromArgb(0xFF, 0x0E, 0x7C, 0x0C), "Edina Green" },
             { Windows.UI.Color.FromArgb(0xFF, 0xF3, 0x73, 0x21), "Orange" },
             { Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xC2, 0x13), "Apple Valley Gold" },
             { Windows.UI.Color.FromArgb(0xFF, 0xCE, 0x32, 0x1E), "Red" },
             { Windows.UI.Color.FromArgb(255, 242, 108, 35), "Washburn Orange" },
             { Windows.UI.Color.FromArgb(0xFF, 0x1C, 0x44, 0x8B), "Hopkins Blue" },
             { Windows.UI.Color.FromArgb(0xFF, 0x1F, 0x49, 0x9A), "Eagan Blue" },
             { Windows.UI.Color.FromArgb(0xFF, 0x20, 0x4F, 0x5E), "Great River Blue" },
             { Windows.UI.Color.FromArgb(0xFF, 0xF0, 0x5A, 0x28), "South Orange" },
             { Windows.UI.Color.FromArgb(0xFF, 0x92, 0x27, 0x8F), "Cretin Purple" },



        };



        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedToggleButton = sender as ToggleButton;
            var backgroundColor = (clickedToggleButton.Background as SolidColorBrush)?.Color;

            if (backgroundColor != null && colorNames.ContainsKey(backgroundColor.Value))
            {
                string selectedColorName = colorNames[backgroundColor.Value];
                SelectedPrimaryColorTextBlock.Text = "Primary Color: " + selectedColorName;

                // Set the primary color SolidColorBrush
                PrimaryColorBrush.Color = backgroundColor.Value;
            }

            // Uncheck all other ToggleButtons in the group
            foreach (var toggleButton in PrimaryColorGroup.Children.OfType<ToggleButton>())
            {
                if (toggleButton != clickedToggleButton)
                {
                    toggleButton.IsChecked = false;
                }
            }

            // Track the number of checked ToggleButtons
            int checkedCount = 0;

            if (backgroundColor != null && colorNames.ContainsKey(backgroundColor.Value))
            {
                string selectedColorName = colorNames[backgroundColor.Value];
                SelectedPrimaryColorTextBlock.Text = "Primary Color: " + selectedColorName;
            }

            // Check the count of checked ToggleButtons
            foreach (var toggleButton in PrimaryColorGroup.Children.OfType<ToggleButton>())
            {
                if (toggleButton.IsChecked == true)
                {
                    checkedCount++;
                }
            }

            // If no ToggleButtons are checked, display "None"
            if (checkedCount == 0)
            {
                SelectedPrimaryColorTextBlock.Text = "Primary Color: None";
            }
        }



        private void SecondaryColorToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedToggleButton = sender as ToggleButton;
            var backgroundColor = (clickedToggleButton.Background as SolidColorBrush)?.Color;

            if (backgroundColor != null && colorNames.ContainsKey(backgroundColor.Value))
            {
                string selectedColorName = colorNames[backgroundColor.Value];
                SelectedSecondaryColorTextBlock.Text = "Secondary Color: " + selectedColorName;

                // Set the secondary color SolidColorBrush
                SecondaryColorBrush.Color = backgroundColor.Value;
            }

            // Uncheck all other ToggleButtons in the group
            foreach (var toggleButton in SecondaryColorGroup.Children.OfType<ToggleButton>())
            {
                if (toggleButton != clickedToggleButton)
                {
                    toggleButton.IsChecked = false;
                }
            }

            // Track the number of checked ToggleButtons
            int checkedCount = 0;

            if (backgroundColor != null && colorNames.ContainsKey(backgroundColor.Value))
            {
                string selectedColorName = colorNames[backgroundColor.Value];
                SelectedSecondaryColorTextBlock.Text = "Secondary Color: " + selectedColorName;
            }

            // Check the count of checked ToggleButtons
            foreach (var toggleButton in SecondaryColorGroup.Children.OfType<ToggleButton>())
            {
                if (toggleButton.IsChecked == true)
                {
                    checkedCount++;
                }
            }

            // If no ToggleButtons are checked, display "None"
            if (checkedCount == 0)
            {
                SelectedSecondaryColorTextBlock.Text = "Secondary Color: None";
            }
        }

        private void LogoFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogoFlipView.SelectedItem is Image selectedImage)
            {
                selectedLogoImagePath = selectedImage.Source.ToString();
            }
        }


        private async Task SaveTeamsAsync(List<Team> teams)
        {
            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var teamsFile = await localFolder.CreateFileAsync("teams.json", CreationCollisionOption.ReplaceExisting);

                var teamsJson = JsonConvert.SerializeObject(teams);
                await FileIO.WriteTextAsync(teamsFile, teamsJson);
            }
            catch (Exception)
            {
                // Handle exceptions if necessary
            }
        }

        private async void SaveTeamButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the background color of the selected primary color ToggleButton
            var primaryColorButton = PrimaryColorGroup.Children.OfType<ToggleButton>().FirstOrDefault(tb => tb.IsChecked == true);
            Windows.UI.Color primaryColor = (primaryColorButton?.Background as SolidColorBrush)?.Color ?? Windows.UI.Colors.Transparent;

            // Get the background color of the selected secondary color ToggleButton
            var secondaryColorButton = SecondaryColorGroup.Children.OfType<ToggleButton>().FirstOrDefault(tb => tb.IsChecked == true);
            Windows.UI.Color secondaryColor = (secondaryColorButton?.Background as SolidColorBrush)?.Color ?? Windows.UI.Colors.Transparent;

            Team newTeam = new Team
            {
                Id = Guid.NewGuid(),
                Name = TeamNameTextBox.Text,
                PrimaryColor = primaryColor, // Set the primary color
                SecondaryColor = secondaryColor, // Set the secondary color
                LogoImagePath = selectedLogoImagePath // Set the selected logo image path
            };

            // Save the new team using the TeamManager
            TeamManager.SaveTeam(newTeam);

            teams.Add(newTeam);

            // Save the updated list of teams
            await SaveTeamsAsync(teams);

            this.Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {



            this.Hide();
        }

    }


}
