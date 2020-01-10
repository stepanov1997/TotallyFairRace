using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TotallyFairRace.Controller;
using TotallyFairRace.Model;
using TotallyFairRace.View.Controls;
using TotallyFairRace.View.Pages;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.ContentDialogs
{
    public sealed partial class AfterGameDialog : ContentDialog
    {
        private Page page;
        public AfterGameDialog()
        {
            this.InitializeComponent();
            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(BackgroundProperty, Colors.DarkSlateGray));
            buttonStyle.Setters.Add(new Setter(ForegroundProperty, Colors.White));
            this.CloseButtonStyle = buttonStyle;
            this.PrimaryButtonStyle = buttonStyle;
            this.SecondaryButtonStyle = buttonStyle;
        }

        public AfterGameDialog(string text, Page page, Game game) : this()
        {
            this.page = page;
            Message.Text = text;

            foreach (var individualStat in game.Stats)
            {
                GameInfoControl.AddRow(individualStat);
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
            Race race;
            do
            {
                var dialog = new PregameDialog();
                var res = await dialog.ShowAsync();
                if (res != ContentDialogResult.Primary) return;
                race = dialog.Race;
                if (race == null)
                {
                    await new MessageDialog("You dont have money for that bid.", "No money").ShowAsync();
                }
                else
                    page.Frame.Navigate(typeof(SimulationPage), race);
            }
            while (race == null);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
            page.Frame.Navigate(typeof(PlayerMenu), "GameAfterGame");
        }
    }
}
