using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyFairRace.Model;
using TotallyFairRace.View;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using TotallyFairRace.Util;
using TotallyFairRace.View.Controls;

namespace TotallyFairRace.Controller
{
    public class Race
    {
        public Car[] Cars { get; set; }
        public TextBlock[] TextBlocks { get; set; }
        public int NumberOfCars { get; set; }
        public decimal Distance { get; set; }
        public Grid Grid { get; set; }
        public StackPanel StackPanel { get; set; }
        public Button Button { get; set; }
        public Color PlayerColor { get; set; }
        public decimal Bid { get; set; }
        public List<CarRaceBar> Bars { get; set; } = new List<CarRaceBar>();

        public Race(Color color, int numberOfCars, decimal distance, decimal bid, Grid grid, Button button, StackPanel panel)
        {
            Bid = bid;
            PlayerColor = color;
            NumberOfCars = numberOfCars;
            Distance = distance;
            Grid = grid;
            Button = button;
            StackPanel = panel;
        }

        public async Task<Car> StartRace()
        {
            Car.Resetuj();
            await Task.WhenAll(Cars.Select(auto => auto?.StartCar()).Where(task => task != null).ToArray());
            return Car.EndQueue.First();
        }

        public async Task Initialize()
        {
            Bars.Clear();
            Cars = new Car[NumberOfCars];
            int teemoNum = RandomGen2.Next() % (NumberOfCars - 1) + 1;
            for (int i = 0; i < NumberOfCars; i++)
            {
                CarRaceBar bar = new CarRaceBar()
                {
                    Name = "Progress" + i,
                    Maximum = (double)Distance,
                    Margin = new Thickness(50, 160 * i, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 1200,
                    Height = 80,
                };
                if (i == 0)
                    await bar.SetColor(PlayerColor);
                else
                    await bar.SetColor(Color.FromArgb(255, (byte) (RandomGen2.Next() % 255),
                    (byte) (RandomGen2.Next() % 255), (byte) (RandomGen2.Next() % 255)));
                Bars.Add(bar);
                Grid grid = new Grid()
                {
                    Name = "Text" + i,
                    Margin = new Thickness(50, 80 * (2 * i + 1), 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 900,
                    Height = 80,
                };

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Star) });

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30, GridUnitType.Pixel) });

                grid.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

                var textBlocks = new[]
                {
                    new TextBlock {Text = ""},
                    new TextBlock {Text = ""},
                    new TextBlock {Text = ""},
                    new TextBlock {Text = ""}
                };

                int index = 0;
                foreach (var textBlock in textBlocks)
                {
                    grid.Children.Add(textBlock);
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    Grid.SetColumn(textBlock, index++);
                }

                TextBlock text = new TextBlock()
                {
                    Name = "Text" + i,
                    IsColorFontEnabled = true,
                    Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff)),
                    Margin = new Thickness(50, 160 * i, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 400,
                    Height = 80
                };
                Grid.Children.Add(bar);
                Grid.Children.Add(grid);
                Grid.Children.Add(text);

                StackPanel.Height += 80;
                if(i==0)
                    Cars[i] = Car.NapraviAuto(Account.CurrentAccount.Nickname, bar, textBlocks);
                else if(i==teemoNum)
                {
                    Cars[i] = Car.NapraviAuto("Teemo", bar, textBlocks);
                }
                else
                {
                    NamesCollection names = NamesCollection.GetInstance();
                    await names.ReadNamesAsync();
                    Cars[i] = Car.NapraviAuto(names[RandomGen2.Next() % names.Names.Count], bar, textBlocks);
                }
            }
            Button.Margin = new Thickness(0, 160 * NumberOfCars, 0, 0);
            Button.HorizontalAlignment = HorizontalAlignment.Center;
            Button.VerticalAlignment = VerticalAlignment.Top;
            Button.Width = 100;
            Button.Height = 40;
        }
    }
}
