using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using TotallyFairRace.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using TotallyFairRace.Controller;
using TotallyFairRace.View.ContentDialogs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerMenu : Page
    {
        public Account Player { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string str && str == "GameAfterGame")
            {
                Player = Account.CurrentAccount;
                HelloText.Text += Player.Nickname;
                return;
            }
            if (!await Account.DeSerializeNow()) Account.RankCollection = new RankCollection();
            Player = await Account.CreateAccount(e.Parameter as string ?? "Player");
            HelloText.Text += Player.Nickname;
        }

        public PlayerMenu()
        {
            this.InitializeComponent();
        }

        private async void OpenSimulation(object sender, TappedRoutedEventArgs e)
        {
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
                    Frame.Navigate(typeof(SimulationPage), race);
            } 
            while (race == null);
        }

        private async void OpenTopList(object sender, TappedRoutedEventArgs e) => await new RankListDialog().ShowAsync();

        private async void OpenProfile(object sender, TappedRoutedEventArgs e) => await new ProfileInfoDialog().ShowAsync();

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            await Account.SerializeNow();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Account.CurrentAccount = null;
            Frame.Navigate(typeof(InputNicknamePage), null);
        }
    }
}
