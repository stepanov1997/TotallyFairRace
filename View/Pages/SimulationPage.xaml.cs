using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.UI.Core;
using TotallyFairRace.Controller;
using TotallyFairRace.Model;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using TotallyFairRace.View.ContentDialogs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TotallyFairRace.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SimulationPage : Page
    {
        public Race Race { get; set; }

        private bool backed = false;

        public SimulationPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Race = e.Parameter as Race;
            if (Race == null) return;
            Race.MainGrid = MainGrid;
            Race.StartRaceButton = GObutton;
            Race.BackButton = new Button();
            Race.BackButton.Click += BackButtonOnClick;
            await Race.Initialize().ConfigureAwait(true);

            Window.Current.SizeChanged += OnSizeChangedEventHandler;

            base.OnNavigatedTo(e);
        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {
            backed = true;
            Frame.Navigate(typeof(PlayerMenu), Account.CurrentAccount.Nickname);
        }

        private async void StartRaceAsync(object sender, RoutedEventArgs e)
        {
            // Start race
            GObutton.Visibility = Visibility.Collapsed;

            await Task.WhenAll(Race.Bars.Select(bar => bar.SemaphoreTurnOn()).ToArray()).ConfigureAwait(true);
            await Race.StartRace().ConfigureAwait(true);

            // Result dialog
            var score = (int)Scoring.MakeScore(Race.NumberOfCars, Car.PlaceOfCurrentUser(), Race.Bid);

            // Update game stats
            List<IndividualStat> stats = new List<IndividualStat>();
            foreach (Car car in Race.Cars)
            {
                var scoreStats = (int)Scoring.MakeScore(Race.NumberOfCars, Car.PlaceOfCar(car), Race.Bid);
                stats.Add(new IndividualStat
                (
                        Car.PlaceOfCar(car),
                        car.CarBar.GetColor(),
                        car.Nickname,
                        scoreStats,
                        car.GetDurance()));
            }
            Game game = new Game(Race.Distance, stats);
            game.Stats = game.Stats.OrderBy(a => a.Place).ToList();
            GameHistory.Games.Add(game);
            await GameHistory.SerializeNow().ConfigureAwait(true);

            // Update account stats
            IndividualStat playerStat = game.Stats.First(elem => elem.Nickname == Account.CurrentAccount.Nickname);
            Account.CurrentAccount.LastMatch = DateTime.Now;
            Account.CurrentAccount.Money += Math.Round((double)playerStat.Points / Race.Cars.Length, 2);
            Account.CurrentAccount.NumberOfGames++;
            Account.CurrentAccount.Points += playerStat.Points;
            await Account.SerializeNow().ConfigureAwait(true);

            if (!backed)
            {
                AfterGameDialog afterGameDialog = new AfterGameDialog(MakeMessageForPlayer(score), this, game);
                await afterGameDialog.ShowAsync();
            }
        }

        private void OnSizeChangedEventHandler(object sender, WindowSizeChangedEventArgs args) => args.Handled = false;

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Window.Current.SizeChanged -= OnSizeChangedEventHandler;
            base.OnNavigatingFrom(e);
        }

        public string MakeMessageForPlayer(int score)
        {
            int place = Car.PlaceOfCurrentUser();
            string placeStr = $"{place}";

            switch (place)
            {
                case 1:
                    placeStr += "st";
                    break;
                case 2:
                    placeStr += "nd";
                    break;
                case 3:
                    placeStr += "rd";
                    break;
                default:
                    placeStr += "th";
                    break;
            }
            string message = $"You are {placeStr} place. \nYou placed {Race.Bid} RP bid.\n";
            if (score > 0)
            {
                message += $"You earned {score} RP. Well done! 😎";
            }
            else if (score < 0)
            {
                message += $"Unfortunately you lost {-1 * score} RP 🤔, I wish you more luck next time !";
            }
            else
            {
                message += $"You are on null, you won/lost 0 RP";
            }

            return message;
        }
    }
}

