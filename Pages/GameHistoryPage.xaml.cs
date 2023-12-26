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
        }

        private void SavePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the player's name from the TextBox
            string playerName = PlayerNameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                // Create a new Player object
                Player newPlayer = new Player
                {
                    Id = Guid.NewGuid(), // Generate a new GUID for the player
                    Name = playerName,
                };

                // Save the player using PlayerManager
                PlayerManager.SavePlayer(newPlayer);

                // Navigate back to the previous page or perform any other desired action
                Frame.GoBack();
            }
            else
            {
                // Show an error message or handle the case where the player's name is empty
            }
        }

    }
}
