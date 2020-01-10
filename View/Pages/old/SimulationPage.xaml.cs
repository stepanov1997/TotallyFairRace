using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.Foundation;
using TotallyFairRace.Controller;
using TotallyFairRace.Model;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TotallyFairRace.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SimulationPage : Page
    {
        public Account Player { get; set; }
        public Race Race { get; set; }

        public SimulationPage()
        {
            InitializeComponent();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Race = e.Parameter as Race;
            if (Race == null) return;
            Race.Grid = grid;
            Race.Button = GObutton;
            Race.StackPanel = Panel;
            await Race.Initialize().ConfigureAwait(true);
            base.OnNavigatedTo(e);
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            GObutton.IsEnabled = false;
            var winner = await Race.StartRace().ConfigureAwait(true);

            // Update game stats
            List<Car> list = Car.EndQueue.ToArray().ToList();
            List<IndividualStat> stats = new List<IndividualStat>();
            foreach (Car car in Race.Cars)
            {
                var score = (int) Scoring.MakeScore(
                    Race.NumberOfCars,
                    list.IndexOf(car),
                    Race.Bid);
                stats.Add(
                    new IndividualStat(
                        list.IndexOf(car), 
                        car.CarBar.GetColor(), 
                        car.Nickname, 
                        score, 
                        (car.EndTime-car.StartTime).Minutes*60+(car.EndTime-car.StartTime).Seconds));
            }
            Game game = new Game(Race.Distance, stats);
            GameHistory.Games.Add(game);
            await GameHistory.SerializeNow().ConfigureAwait(true);

            // Update account stats
            IndividualStat playerStat = game.Stats.First(elem => elem.Nickname == Account.CurrentAccount.Nickname);
            Account.CurrentAccount.LastMatch = DateTime.Now;
            Account.CurrentAccount.Money += (double)playerStat.Points/Race.Cars.Length;
            Account.CurrentAccount.NumberOfGames++;
            Account.CurrentAccount.Points += playerStat.Points;
            await Account.SerializeNow().ConfigureAwait(true);
            GObutton.IsEnabled = true;
        }
    }
}

