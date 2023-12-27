using CP4.Classes;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CP4.Dialogs
{
    public sealed partial class AddPlayerContentDialog : ContentDialog
    {
        public string PlayerName { get; private set; }
        public Guid TeamId { get; private set; }

        public AddPlayerContentDialog(Guid teamId, string teamName)
        {
            TeamId = teamId;
            InitializeComponent();
            TeamNameTextBlock.Text = teamName;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Collect the player's name from the TextBox
            PlayerName = PlayerNameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(PlayerName))
            {
                // Create a new Player object
                Player newPlayer = new Player
                {
                    Id = Guid.NewGuid(),
                    Name = PlayerName,
                    TeamId = TeamId
                };

                // Save the player using PlayerManager
                await PlayerManager.SavePlayer(newPlayer);

                // Close the dialog
                Hide();
            }
            else
            {
                // Display an error message or handle the case where the player's name is empty
                args.Cancel = true; // This prevents the dialog from closing
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // User clicked the Cancel button
            PlayerName = null;
        }
    }
}
