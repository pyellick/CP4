using CP4.Classes;
using CP4.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using Windows.Foundation;


namespace CP4.Dialogs
{
    public sealed partial class AddPlayerDialog : ContentDialog
    {
        private double[] options = new double[5];
        private Line[] radarLines = new Line[5];
        private Line[] connectingLines = new Line[5];
        private TextBlock[] sliderDescriptions = new TextBlock[5];
        private Canvas radarChartCanvas;

        private Guid currentTeamId;
        private List<string> selectedRoles = new List<string>();

        public AddPlayerDialog(Guid teamId)
        {
            this.InitializeComponent();
            currentTeamId = teamId;
            InitializeSliders();

            for (int i = 0; i < 5; i++)
            {
                options[i] = 4;
            }
            InitializeRadarChart();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;

            // Toggle the IsChecked state
            toggleButton.IsChecked = !toggleButton.IsChecked;

            string roleName = toggleButton.Content.ToString();

            if (toggleButton.IsChecked == true)
            {
                selectedRoles.Add(roleName);
            }
            else
            {
                selectedRoles.Remove(roleName);
            }
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                string buttonText = toggleButton.Content.ToString();
                // Perform any actions when the toggle button is checked

                // Change the background color to #1a2c6c when checked
                toggleButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 26, 44, 108));
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                string buttonText = toggleButton.Content.ToString();
                // Perform any actions when the toggle button is unchecked

                // Change the background color to transparent when unchecked
                toggleButton.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }
        }

        private void SaveToggleStates()
        {
            // Save the toggle button states to a temporary storage
            ApplicationData.Current.LocalSettings.Values["ToggleStates"] = JsonConvert.SerializeObject(selectedRoles);
        }


        private void InitializeSliders()
        {
            SpeedSlider.ValueChanged += Slider_ValueChanged;
            DefenseSlider.ValueChanged += Slider_ValueChanged;
            ThrowingSlider.ValueChanged += Slider_ValueChanged;
            CuttingSlider.ValueChanged += Slider_ValueChanged;
            ScoringAbilitySlider.ValueChanged += Slider_ValueChanged;
        }

        private void InitializeRadarChart()
        {
            radarChartCanvas = radarCanvas;
            radarCanvas.Children.Clear();

            double lineLength = 7;
            double angleStep = 2 * Math.PI / 5;

            Point[] outerPoints = new Point[5];

            string[] sliderTitles = { "Speed", "Def.", "Throwing", "Cutting", "ScAb" };

            // Add 7 circles with white borders and no fill
            for (int i = 0; i < 7; i++)
            {
                double circleRadius = lineLength * (i + 1);
                Ellipse ellipse = new Ellipse()
                {
                    Width = circleRadius * 2,
                    Height = circleRadius * 2,
                    Stroke = new SolidColorBrush(Colors.White), // Set circle border color to white
                    StrokeThickness = .5,
                    Margin = new Thickness(radarChartCanvas.Width / 2 - circleRadius, radarChartCanvas.Height / 2 - circleRadius, 0, 0)
                };

                radarCanvas.Children.Add(ellipse);
            }

            for (int i = 0; i < 5; i++)
            {
                double angle = i * angleStep - Math.PI / 2;
                double x = radarChartCanvas.Width / 2 + Math.Cos(angle) * lineLength * options[i];
                double y = radarChartCanvas.Height / 2 + Math.Sin(angle) * lineLength * options[i];

                radarLines[i] = new Line()
                {
                    X1 = radarChartCanvas.Width / 2,
                    Y1 = radarChartCanvas.Height / 2,
                    X2 = x,
                    Y2 = y,
                    Stroke = new SolidColorBrush(Colors.Orange), // Set line color to orange
                    StrokeThickness = 2
                };

                radarCanvas.Children.Add(radarLines[i]);
                outerPoints[i] = new Point(x, y);

                // Calculate text positions relative to the center of the chart
                double textX = radarChartCanvas.Width / 2 + Math.Cos(angle) * (lineLength + 70); // 20 is the distance from the center
                double textY = radarChartCanvas.Height / 2 + Math.Sin(angle) * (lineLength + 70);

                // Adjust the "Speed" and "Throwing" text positions
                if (sliderTitles[i] == "Speed" || sliderTitles[i] == "Def." || sliderTitles[i] == "ScAb")
                {
                    textY += 20; // Move "Speed" text downward by 20 units
                }

                if (sliderTitles[i] == "Throwing" || sliderTitles[i] == "Cutting")
                {
                    textY += 5;
                }

                // Add text with titles at the calculated positions
                TextBlock textBlock = new TextBlock()
                {
                    Text = sliderTitles[i],
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 10,
                    Margin = new Thickness(textX - 20, textY - 20, 0, 0) // Set relative position
                };

                radarCanvas.Children.Add(textBlock);
                sliderDescriptions[i] = textBlock;
            }

            for (int i = 0; i < 5; i++)
            {
                int nextIndex = (i + 1) % 5;
                Line connectingLine = new Line()
                {
                    X1 = outerPoints[i].X,
                    Y1 = outerPoints[i].Y,
                    X2 = outerPoints[nextIndex].X,
                    Y2 = outerPoints[nextIndex].Y,
                    Stroke = new SolidColorBrush(Colors.Orange), // Set line color to orange
                    StrokeThickness = 2
                };

                radarCanvas.Children.Add(connectingLine);

                connectingLines[i] = connectingLine;

                // Create and fill the inner triangles with a lighter orange color
                // Create and fill the inner triangles with the specified color
                Polygon triangle = new Polygon()
                {
                    Fill = new SolidColorBrush(Color.FromArgb(255, 235, 61, 38)), // #EB3D26
                };

                triangle.Points.Add(new Point(radarChartCanvas.Width / 2, radarChartCanvas.Height / 2));
                triangle.Points.Add(outerPoints[i]);
                triangle.Points.Add(outerPoints[nextIndex]);

                radarCanvas.Children.Add(triangle);
            }
        }




        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateRadarChart();
        }
        private void UpdateRadarChart()
        {
            double[] options = new double[5];

            options[0] = SpeedSlider.Value;
            options[1] = DefenseSlider.Value;
            options[2] = ThrowingSlider.Value;
            options[3] = CuttingSlider.Value;
            options[4] = ScoringAbilitySlider.Value;

            for (int i = 0; i < 5; i++)
            {
                double angle = i * 2 * Math.PI / 5 - Math.PI / 2;
                double x = radarChartCanvas.Width / 2 + Math.Cos(angle) * 7 * options[i];
                double y = radarChartCanvas.Height / 2 + Math.Sin(angle) * 7 * options[i];

                radarLines[i].X2 = x;
                radarLines[i].Y2 = y;

                // Text position remains fixed at the tip of each segment
                // No need to change the margin here
            }

            for (int i = 0; i < 5; i++)
            {
                int nextIndex = (i + 1) % 5;
                connectingLines[i].X1 = radarLines[i].X2;
                connectingLines[i].Y1 = radarLines[i].Y2;
                connectingLines[i].X2 = radarLines[nextIndex].X2;
                connectingLines[i].Y2 = radarLines[nextIndex].Y2;

                // Update the fill color of the inner triangles based on the slider values
                // Update the fill color of the inner triangles to the specified color
                Polygon triangle = radarCanvas.Children.OfType<Polygon>().ElementAt(i);
                triangle.Points[1] = new Point(radarLines[i].X2, radarLines[i].Y2);
                triangle.Points[2] = new Point(radarLines[nextIndex].X2, radarLines[nextIndex].Y2);

                ((SolidColorBrush)triangle.Fill).Color = Color.FromArgb(255, 235, 61, 38); // #EB3D26

            }
        }



        private async void SavePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = PlayerNameTextBox.Text.Trim();
            int jerseyNumber;

            if (!string.IsNullOrEmpty(playerName) && int.TryParse(JerseyNumberTextBox.Text.Trim(), out jerseyNumber))
            {
                if (currentTeamId != Guid.Empty)
                {
                    Player newPlayer = new Player
                    {
                        Id = Guid.NewGuid(),
                        Name = playerName,
                        TeamId = currentTeamId,
                        JerseyNumber = jerseyNumber, // Assign jersey number
                        Speed = (int)SpeedSlider.Value, // Assign attributes from sliders
                        Defense = (int)DefenseSlider.Value,
                        Throwing = (int)ThrowingSlider.Value,
                        Cutting = (int)CuttingSlider.Value,
                        ScoringAbility = (int)ScoringAbilitySlider.Value,
                        // Assign player roles based on selected ToggleButtons
                        OLine = selectedRoles.Contains("OLine"),
                        DLine = selectedRoles.Contains("DLine"),
                        ALine = selectedRoles.Contains("ALine"),
                        SLine = selectedRoles.Contains("SLine"),
                        Handler = selectedRoles.Contains("Handler"),
                        Cutter = selectedRoles.Contains("Cutter"),
                        Hybrid = selectedRoles.Contains("Hybrid"),
                        StarPlayer = selectedRoles.Contains("Star Player"),
                    };


                    SaveToggleStates();
                    await PlayerManager.SavePlayer(newPlayer);


                    // Hide the dialog
                    Hide();
                    ((EditTeamPage)((Frame)Window.Current.Content).Content).RefreshPlayerList();
                }
                else
                {
                    // Handle the case where the team ID is invalid
                }
            }
            else
            {
                // Handle the case where the player's name is empty or jersey number is invalid
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
