using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using TotallyFairRace.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using TotallyFairRace.View.ContentDialogs;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TotallyFairRace.View.Controls
{
    public sealed partial class GameInfoControl : UserControl
    {
        public Game Game { get; set; }
        public UIElement ContentOld { get; set; }
        public ProfileInfoDialog ProfileInfoDialog { get; set; }
        public int NumberOfRows { get; set; } = 1;

        public GameInfoControl()
        {
            this.InitializeComponent();
        }
        public GameInfoControl(Game game, UIElement content, ProfileInfoDialog profileInfoDialog) : this()
        {
            Game = game;
            Game.Stats = Game.Stats.OrderBy( e => e.Place).ToList();
            ContentOld = content;
            ProfileInfoDialog = profileInfoDialog;
            ProfileInfoDialog.PrimaryButtonText = "Back";

            ProfileInfoDialog.PrimaryButtonClick -= ProfileInfoDialog.ContentDialog_PrimaryButtonClick;
            ProfileInfoDialog.PrimaryButtonClick += OnProfileInfoDialogOnPrimaryButtonClick;

            foreach (var individualStat in Game.Stats)
            {
                AddRow(individualStat);
            }
        }

        private void OnProfileInfoDialogOnPrimaryButtonClick(ContentDialog a, ContentDialogButtonClickEventArgs b)
        {
            b.Cancel = true;
            Content = ContentOld;
            if (a.Title is TextBlock title)
            {
                title.Text = "GAME HISTORY";
                a.Title = title;
            }

            ProfileInfoDialog.PrimaryButtonClick -= OnProfileInfoDialogOnPrimaryButtonClick;
            ProfileInfoDialog.PrimaryButtonClick += ProfileInfoDialog.ContentDialog_PrimaryButtonClick;
        }

        public void AddRow(IndividualStat stat)
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
                if (cells[index].Item2 != null)
                {
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                }
                Grid.SetColumn(border, index);
                Grid.SetRow(border, NumberOfRows);
            }
            cells[0].Item2.Text = $"{stat.Place}";
            cells[1].Item2.Text = $"{stat.Nickname}";
            cells[2].Item1.Background = new SolidColorBrush(stat.CarColor);
            cells[3].Item2.Text = $"{stat.Durance}s";
            cells[4].Item2.Text = $"{stat.Points}";
            NumberOfRows++;
        }
    }
}
