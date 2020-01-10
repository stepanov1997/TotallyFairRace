using System.Globalization;
using TotallyFairRace.Model;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using TotallyFairRace.View.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.ContentDialogs
{
    public sealed partial class ProfileInfoDialog : ContentDialog
    {
        private const string TITLE = "PROFILE INFO";
        object ContentBackup { get; set; }

        public ProfileInfoDialog()
        {
            this.InitializeComponent();

            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(BackgroundProperty, Colors.DarkSlateGray));
            buttonStyle.Setters.Add(new Setter(ForegroundProperty, Colors.White));
            this.CloseButtonStyle = buttonStyle;
            this.PrimaryButtonStyle = buttonStyle;

            var textBox = new TextBlock
            {
                FontFamily = new FontFamily("Stencil"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff)),
                TextAlignment = TextAlignment.Center,
                Width = 500,
                Text = TITLE
            };
            Title = textBox;

            var account = Account.CurrentAccount;
            NicknameTextBlock.Text = $"{account.Nickname}";
            MoneyTextBlock.Text = $"{account.Money}";
            PointsTextBlock.Text = $"{account.Points}";
            LastPlayedTextBlock.Text = $"{account.LastMatch.ToString("dd.MM.yyyy.", CultureInfo.InvariantCulture)}";
            GamesTextBlock.Text = $"{account.NumberOfGames}";
        }

        public void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;
            Content = ContentBackup;
            PrimaryButtonText = "";
            if (!(Title is TextBlock title)) return;
            title.Text = TITLE;
            Title = title;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ShowMatchHistoryClick(object sender, RoutedEventArgs e)
        {
            ContentBackup = Content;
            Content = new GamesHistoryControl(this);
            PrimaryButtonText = "Back";
            if (!(Title is TextBlock title)) return;
            title.Text = TITLE;
            Title = title;
        }
    }
}
