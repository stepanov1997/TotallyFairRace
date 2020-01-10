using System.Globalization;
using TotallyFairRace.Model;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.ContentDialogs
{
    public sealed partial class RankListDialog : ContentDialog
    {
        public int NumberOfRows { get; set; } = 1;
        public RankCollection RankCollection { get; set; }

        public RankListDialog()
        {
            RankCollection = Account.RankCollection;
            RankCollection.Sort(true);
            this.InitializeComponent();

            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, Colors.DarkSlateGray));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Colors.White));
            this.CloseButtonStyle = buttonStyle;

            foreach (Account account in RankCollection)
            {
                AddRow(account);
            }
        }

        public void AddRow(Account account)
        {
            var cells = new (Border, TextBlock)[5];
            for (var index = 0; index < cells.Length; index++)
            {
                GridView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                cells[index] = (new Border(), new TextBlock());
                var border = cells[index].Item1;
                var textBlock = cells[index].Item2;
                GridView.Children.Add(border);
                border.Child = textBlock;
                border.Background = new SolidColorBrush(Color.FromArgb(255, 227, 229, 140));
                border.BorderThickness = new Thickness(3);
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                Grid.SetColumn(border, index);
                Grid.SetRow(border, NumberOfRows);
            }
            cells[0].Item2.Text = $"{account.Nickname}";
            cells[1].Item2.Text = $"{account.Money}";
            cells[2].Item2.Text = $"{account.NumberOfGames}";
            cells[3].Item2.Text = $"{account.LastMatch.ToString("dd.MM.yyyy.", CultureInfo.InvariantCulture)}";
            cells[4].Item2.Text = $"{account.Points}";
            NumberOfRows++;
        }
    }
}
