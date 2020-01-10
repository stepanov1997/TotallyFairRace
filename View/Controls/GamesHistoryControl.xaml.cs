using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TotallyFairRace.Model;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using TotallyFairRace.View.ContentDialogs;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TotallyFairRace.View.Controls
{
    public sealed partial class GamesHistoryControl : UserControl
    {
        public ProfileInfoDialog ProfileInfoDialog { get; set; }
        public int NumberOfRows { get; set; } = 1;
        public List<Game> GameHistoryList { get; set; }

        public GamesHistoryControl()
        {
            GameHistoryList = GameHistory.Games;
            this.InitializeComponent();

            foreach (var game in GameHistoryList.Where(game => game.Stats.Any(stat => stat.Nickname == Account.CurrentAccount.Nickname)))
            {
                AddRow(game);
            }
        }

        public GamesHistoryControl(ProfileInfoDialog profileInfoDialog) : this()
        {
            ProfileInfoDialog = profileInfoDialog;
            ProfileInfoDialog.PrimaryButtonText = "Back";
        }

        public void AddRow(Game game)
        {
            var cells = new (Border, UIElement)[5];
            for (var index = 0; index < cells.Length; index++)
            {
                GridView.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                if (index == cells.Length - 1)
                    cells[index] = (new Border(), new Button());
                else
                    cells[index] = (new Border(), new TextBlock());
                var border = cells[index].Item1;
                var textBlock = cells[index].Item2;
                GridView.Children.Add(border);
                border.Child = textBlock;
                border.Background = new SolidColorBrush(Color.FromArgb(255, 227, 229, 140));
                border.BorderThickness = new Thickness(3);
                if (cells[index].Item2 is TextBlock)
                {
                    ((TextBlock)textBlock).FontWeight = FontWeights.Bold;
                    ((TextBlock)textBlock).HorizontalAlignment = HorizontalAlignment.Center;
                    ((TextBlock)textBlock).VerticalAlignment = VerticalAlignment.Center;
                    ((TextBlock)textBlock).Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                }
                else
                {
                    ((Button)textBlock).FontWeight = FontWeights.Bold;
                    ((Button)textBlock).FontSize = 10;
                    ((Button)textBlock).HorizontalAlignment = HorizontalAlignment.Stretch;
                    ((Button)textBlock).VerticalAlignment = VerticalAlignment.Stretch;
                    ((Button)textBlock).VerticalContentAlignment = VerticalAlignment.Center;
                    ((Button)textBlock).Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                }
                Grid.SetColumn(border, index);
                Grid.SetRow(border, NumberOfRows);
            }
            ((TextBlock)cells[0].Item2).Text = $"{NumberOfRows}";
            ((TextBlock)cells[1].Item2).Text = $"{game.Date.ToString("dd.MM.yyyy.", CultureInfo.InvariantCulture)}";
            ((TextBlock)cells[2].Item2).Text = $"{game.Distance} km";
            ((TextBlock)cells[3].Item2).Text = $"{game.Durance}s";
            ((Button)cells[4].Item2).Content = "MORE INFO";
            ((Button)cells[4].Item2).Click += (sender, args) =>
            {
               Content = new GameInfoControl(game, Content, ProfileInfoDialog);
            };
            NumberOfRows++;
        }
    }
}
