using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using TotallyFairRace.Util;
using Size = Windows.Foundation.Size;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        private bool _playing = false;
        private static readonly MediaPlayer Player = new MediaPlayer();

        public WelcomePage()
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
        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(InputNicknamePage), Player);
        }
    }
}
