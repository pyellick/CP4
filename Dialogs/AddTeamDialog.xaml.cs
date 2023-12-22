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


namespace CP4
{
    public sealed partial class AddTeamDialog : ContentDialog
    {
        private List<Team> teams; // Define the teams list at the class level

        public AddTeamDialog()
        {
            InitializeComponent();



            // Populate the PrimaryColorComboBox and SecondaryColorComboBox with colors
            PopulateColorComboBoxes();

            teams = new List<Team>();

            // Assuming you have a list of teams and their logo file names
            List<TeamInfo> teamsInfos = new List<TeamInfo>
                        {
                             new TeamInfo { TeamName = "Washburn Millers", LogoImagePath = "ms-appx:///Assets/TeamLogos/MillerLogoV2.png" },
                             new TeamInfo { TeamName = "South Squall", LogoImagePath = "ms-appx:///Assets/TeamLogos/SouthLogoV2.png" },
                             new TeamInfo { TeamName = "Edina Lanterns", LogoImagePath = "ms-appx:///Assets/TeamLogos/EdinaLogoV2.png" },
                             new TeamInfo { TeamName = "Hopkins Hurt", LogoImagePath = "ms-appx:///Assets/TeamLogos/HopkinsLogoV2.png" },
                             new TeamInfo { TeamName = "St. Louis Park Crush", LogoImagePath = "ms-appx:///Assets/TeamLogos/CrushLogoV2.png" },
                             new TeamInfo { TeamName = "Eagan Wildcats", LogoImagePath = "ms-appx:///Assets/TeamLogos/EaganLogoV2.png" },
                             new TeamInfo { TeamName = "White Bear Lake Bears", LogoImagePath = "ms-appx:///Assets/TeamLogos/WhiteBearLakeLogoV2.png" },
                             new TeamInfo { TeamName = "Mounds View Mustangs", LogoImagePath = "ms-appx:///Assets/TeamLogos/MoundsViewLogoV2.png" },
                             new TeamInfo { TeamName = "Open World Learning Manatees", LogoImagePath = "ms-appx:///Assets/TeamLogos/OpenWorldLearningLogoV2.png" },
                             new TeamInfo { TeamName = "Great River Stars", LogoImagePath = "ms-appx:///Assets/TeamLogos/GreatRiverStateLogoV2.png" },
                             new TeamInfo { TeamName = "Blake Bears", LogoImagePath = "ms-appx:///Assets/TeamLogos/BlakeLogoV2.png" },
                             new TeamInfo { TeamName = "Benilde-St. Margaret's Knights", LogoImagePath = "ms-appx:///Assets/TeamLogos/Benilde-St.MargaretLogoV2.png" },
                             new TeamInfo { TeamName = "Apple Valley Eagles", LogoImagePath = "ms-appx:///Assets/TeamLogos/AppleValleyLogoV2.png" },
                             new TeamInfo { TeamName = "Cathedral Phoenixes", LogoImagePath = "ms-appx:///Assets/TeamLogos/Cathedral Logo V2.png" },
                             new TeamInfo { TeamName = "St. Paul Central Revolution", LogoImagePath = "ms-appx:///Assets/TeamLogos/St. Paul Central.png" },
                             new TeamInfo { TeamName = "Cretin-Derham Hall Raiders", LogoImagePath = "ms-appx:///Assets/TeamLogos/CretinDerhamHallLogoV2.png" },
                             new TeamInfo { TeamName = "Other Team", LogoImagePath = "ms-appx:///Assets/TeamLogos/Other Team Logo.png" },


                            };

            TeamLogoComboBox.ItemsSource = teamsInfos;


        }





        private void TeamLogoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamLogoComboBox.SelectedItem is TeamInfo selectedTeam)
            {
                // Update the selected team logo image
                SelectedLogoImage.Source = new BitmapImage(new Uri(selectedTeam.LogoImagePath));
                UpdateSelectedLogoVisibility();
            }
            else
            {
                // Clear the selected team logo if no item is selected
                SelectedLogoImage.Source = null;
                UpdateSelectedLogoVisibility();
            }
        }


        private void PrimaryColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrimaryColorComboBox.SelectedItem is ColorItem selectedColor)
            {


                UpdateSelectedLogoVisibility();

                // Update the border stroke color with the selected primary color
                SelectedLogoBorder.BorderBrush = new SolidColorBrush(selectedColor.Color);
            }
        }

        private void SecondaryColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondaryColorComboBox.SelectedItem is ColorItem selectedColor)
            {
  

                UpdateSelectedLogoVisibility();

                // Update the border background color with the selected secondary color
                SelectedLogoBorder.Background = new SolidColorBrush(selectedColor.Color);


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

        private void UpdateSelectedLogoVisibility()
        {
            if (TeamLogoComboBox.SelectedItem is TeamInfo selectedTeam &&
                PrimaryColorComboBox.SelectedItem is ColorItem primaryColor &&
                SecondaryColorComboBox.SelectedItem is ColorItem secondaryColor)
            {
                // All three selections have been made, so show the SelectedLogoBorder
                SelectedLogoBorder.Visibility = Visibility.Visible;

                // Update the selected team logo image
                SelectedLogoImage.Source = new BitmapImage(new Uri(selectedTeam.LogoImagePath));

                // Update the border stroke color with the selected primary color
                SelectedLogoBorder.BorderBrush = new SolidColorBrush(primaryColor.Color);

                // Create a radial gradient brush
                RadialGradientBrush radialGradient = new RadialGradientBrush();

                // Define gradient stops - from secondary color to black
                radialGradient.GradientStops.Add(new GradientStop { Color = secondaryColor.Color, Offset = 0 });
                radialGradient.GradientStops.Add(new GradientStop { Color = Windows.UI.Colors.Black, Offset = 5.8 });

                // Set the radial gradient as the background of SelectedLogoBorder
                SelectedLogoBorder.Background = radialGradient;
            }
            else
            {
                // Hide the SelectedLogoBorder if any of the selections is missing
                SelectedLogoBorder.Visibility = Visibility.Collapsed;
                SelectedLogoImage.Source = null; // Clear the selected logo image
            }
        }



        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) //THIS IS THE CANCEL BUTTON
        {

            // Hide the dialog
            this.Hide();


        }

        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)  // THIS IS THE SAVE BUTTON
        {


            // Create a new team
            Team newTeam = new Team
            {
                Id = Guid.NewGuid(),
                Name = TeamNameTextBox.Text,
                PrimaryColor = ((ColorItem)PrimaryColorComboBox.SelectedItem).Name,
                SecondaryColor = ((ColorItem)SecondaryColorComboBox.SelectedItem).Name,
                LogoImagePath = ((TeamInfo)TeamLogoComboBox.SelectedItem).LogoImagePath
            };

            // Save the new team using the TeamManager
            TeamManager.SaveTeam(newTeam);

            teams.Add(newTeam);

            // Save the updated list of teams
            await SaveTeamsAsync(teams);


            this.Hide();
        }

        // Create a class to represent color items
        public class ColorItem
        {
            public string Name { get; set; }
            public Windows.UI.Color Color { get; set; }
        }

        // Helper method to populate the ComboBoxes with colors
        private void PopulateColorComboBoxes()
        {
            // Define your color items here
            var primaryColors = new List<ColorItem>
            {
                new ColorItem { Name = "White", Color = Windows.UI.Colors.White },
                new ColorItem { Name = "Black", Color = Windows.UI.Colors.Black },
                new ColorItem { Name = "Washburn Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x26, 0x57, 0xA7) },
                new ColorItem { Name = "Manatee Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x39, 0x59, 0x9F) },
                new ColorItem { Name = "Great River Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x16, 0x35, 0x3F) },
                new ColorItem { Name = "Eagan Green", Color = Windows.UI.Color.FromArgb(0xFF, 0x1B, 0xA9, 0x4A) },
                new ColorItem { Name = "Mounds View Green", Color = Windows.UI.Color.FromArgb(0xFF, 0x16, 0x68, 0x37) },
                new ColorItem { Name = "Blake Green", Color = Windows.UI.Color.FromArgb(0xFF, 0x15, 0x64, 0x33) },
                new ColorItem { Name = "Edina Green", Color = Windows.UI.Color.FromArgb(0xFF, 0x0E, 0x7C, 0x0C) },
                new ColorItem { Name = "Crush Orange", Color = Windows.UI.Color.FromArgb(0xFF, 0xF3, 0x73, 0x21) },
                new ColorItem { Name = "Apple Valley Gold", Color = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xC2, 0x13) },
                new ColorItem { Name = "White Bear Lake Orange", Color = Windows.UI.Color.FromArgb(0xFF, 0xF5, 0x7B, 0x20) },
                new ColorItem { Name = "Benilde St Margaret Red", Color = Windows.UI.Color.FromArgb(0xFF, 0xCE, 0x32, 0x1E) },
                // Add more primary colors as needed
            };

            var secondaryColors = new List<ColorItem>
            {
                new ColorItem { Name = "White", Color = Windows.UI.Colors.White },
                new ColorItem { Name = "Black", Color = Windows.UI.Colors.Black },
                new ColorItem { Name = "Washburn Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x25, 0x3E, 0x97) },
                new ColorItem { Name = "Hopkins Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x1C, 0x44, 0x8B) },
                new ColorItem { Name = "Eagan Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x1F, 0x49, 0x9A) },
                new ColorItem { Name = "Great River Blue", Color = Windows.UI.Color.FromArgb(0xFF, 0x20, 0x4F, 0x5E) },
                new ColorItem { Name = "South Orange", Color = Windows.UI.Color.FromArgb(0xFF, 0xF0, 0x5A, 0x28) },
                new ColorItem { Name = "Cretin Purple", Color = Windows.UI.Color.FromArgb(0xFF, 0x92, 0x27, 0x8F) },
                // Add more secondary colors as needed
            };

            // Set the ComboBox item sources
            PrimaryColorComboBox.ItemsSource = primaryColors;
            SecondaryColorComboBox.ItemsSource = secondaryColors;

        }


        public class TeamInfo
        {
            public string TeamName { get; set; }
            public string LogoImagePath { get; set; }
        }





    }


}
