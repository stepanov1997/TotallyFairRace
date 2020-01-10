using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyFairRace.Model;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using TotallyFairRace.Util;
using TotallyFairRace.View.Controls;
using TotallyFairRace.View.Pages;

namespace TotallyFairRace.Controller
{
    public class Race
    {
        public Car[] Cars { get; set; }
        public int NumberOfCars { get; set; }
        public decimal Distance { get; set; }
        public Grid MainGrid { get; set; }
        public Button BackButton { get; set; }
        public Button StartRaceButton { get; set; }
        public Color PlayerColor { get; set; }
        public decimal Bid { get; set; }
        public List<CarRaceBar> Bars { get; set; } = new List<CarRaceBar>();

        public Race(Color color, int numberOfCars, decimal distance, decimal bid, Grid mainGrid, Button startRaceButton, Button backButton)
        {
            Bid = bid;
            PlayerColor = color;
            NumberOfCars = numberOfCars;
            Distance = distance;
            MainGrid = mainGrid;
            BackButton = backButton;
            StartRaceButton = startRaceButton;
        }

        public async Task StartRace()
        {
            Car.Resetuj();
            await Task.WhenAll(Cars.Select(auto => auto?.StartCar()).Where(task => task != null).ToArray()).ConfigureAwait(true);
        }

        public async Task Initialize()
        {
            Bars.Clear();
            Cars = new Car[NumberOfCars];
            int teemoNum = RandomGen2.Next() % (NumberOfCars - 1) + 1;
            MainGrid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < NumberOfCars; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
                // Add CarRaceBar in mainGrid
                CarRaceBar bar = new CarRaceBar()
                {
                    Maximum = (double)Distance,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                MainGrid.Children.Add(bar);
                Grid.SetColumn(bar, 1);
                Grid.SetRow(bar, i + 1);

                // Set color of car.
                if (i == 0)
                    await bar.SetColor(PlayerColor);
                else
                    await bar.SetColor(Color.FromArgb(255, (byte)(RandomGen2.Next() % 255),
                    (byte)(RandomGen2.Next() % 255), (byte)(RandomGen2.Next() % 255)));
                Bars.Add(bar);

                // Make text
                TextBlock description = new TextBlock()
                {
                    Name = $"Text{i}",
                    Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                MainGrid.Children.Add(description);
                Grid.SetColumn(description, 2);
                Grid.SetRow(description, i + 1);

                Image placeImage = new Image();
                placeImage.Source = new BitmapImage(new Uri(@"appx:///Assets/0.png"));
                placeImage.HorizontalAlignment = HorizontalAlignment.Center;
                placeImage.VerticalAlignment = VerticalAlignment.Center;
                MainGrid.Children.Add(placeImage);
                Grid.SetColumn(placeImage, 0);
                Grid.SetRow(placeImage, i + 1);

                if (i == 0)
                    Cars[i] = Car.NapraviAuto(Account.CurrentAccount.Nickname, bar, description);
                else if (i == teemoNum)
                {
                    Cars[i] = Car.NapraviAuto("Teemo", bar, description);
                }
                else
                {
                    NamesCollection names = NamesCollection.GetInstance();
                    await names.ReadNamesAsync();
                    Cars[i] = Car.NapraviAuto(names[RandomGen2.Next() % names.Names.Count], bar, description);
                }
            }
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.Children.Add(BackButton);
            
            ButtonStyle(BackButton, "BACK", 0, NumberOfCars+1);
            ButtonStyle(StartRaceButton, "START RACE", 1, NumberOfCars+1);
        }

        public void ButtonStyle(Button button, string title, int column, int row)
        {
            button.HorizontalAlignment = HorizontalAlignment.Stretch;
            button.VerticalAlignment = VerticalAlignment.Stretch;
            button.FontFamily = new FontFamily("Stencil");
            button.FontSize = 20;
            button.Content = title;
            button.FontWeight = FontWeights.Bold;
            button.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xc5, 0xc5, 0xc5));
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
        }
    }

}
