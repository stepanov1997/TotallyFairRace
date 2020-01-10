using System;
using Windows.Foundation;
using TotallyFairRace.Model;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InputNicknamePage : Page
    {
        private bool _playing = false;
        private static readonly MediaPlayer Player = new MediaPlayer();

        public InputNicknamePage()
        {
            this.InitializeComponent();
        }

        [Obsolete]
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            var song = await folder.GetFileAsync("Styles of Beyond - Nine Thou (Superstars Remix).mp3");

            Player.AutoPlay = false;
            Player.Source = MediaSource.CreateFromStorageFile(song);
            if (_playing)
            {
                Player.Pause();
                _playing = false;
                MusicButton.Content = "PLAY MUSIC";
            }
            else
            {
                Player.Play();
                _playing = true;
                MusicButton.Content = "STOP MUSIC";
            }
        }
        private async void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (TextBox.Text.Length >= 4)
            {
                await GameHistory.DeSerializeNow().ConfigureAwait(true);
                Frame.Navigate(typeof(PlayerMenu), TextBox.Text);
            }
            else
            {
                await new MessageDialog("Enter a nickname longer than four characters!", "Short nickname").ShowAsync();
            }
        }
    }
}
