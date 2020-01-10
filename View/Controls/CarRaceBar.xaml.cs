using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;
using Svg;
using TotallyFairRace.Model;
using static Windows.UI.Xaml.Visibility;
using Color = Windows.UI.Color;
using Image = Windows.UI.Xaml.Controls.Image;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TotallyFairRace.View.Controls
{
    public sealed partial class CarRaceBar : UserControl
    {
        private static int _num = 0;

        private double _currentPosition = 0;

        private Color _color = Color.FromArgb(255, (byte)(RandomGen2.Next() % 255), (byte)(RandomGen2.Next() % 255), (byte)(RandomGen2.Next() % 255));
        public Color GetColor() => _color;
        public Image TrafficLights => Semaphore;

        public async Task SetColor(Color color) => await ChangeColorOfPicture(color);

        public static async Task<SvgImageSource> StorageFileToSvgImage(StorageFile savedStorageFile)
        {
            using (var fileStream = await savedStorageFile.OpenAsync(FileAccessMode.Read))
            {
                var svgImage = new SvgImageSource();
                await svgImage.SetSourceAsync(fileStream);
                return svgImage;
            }
        }

        private async Task ChangeColorOfPicture(Color color)
        {
            _color = color;
            var svg = "";
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/CarPics/car.svg"));
            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                svg = await streamReader.ReadToEndAsync();
            }
            var newSvg = Regex.Replace(svg, "white", CreateHexFromColor(color));
            var storageFolder =
                ApplicationData.Current.LocalFolder;
            var storageFile =
                await storageFolder.CreateFileAsync($"Image{_num}.svg",
                    CreationCollisionOption.ReplaceExisting);

            //Write data to the file
            await FileIO.WriteTextAsync(storageFile, newSvg);

            var svgImage = await StorageFileToSvgImage(storageFile);
            svgImage.RasterizePixelHeight = 200;
            svgImage.RasterizePixelWidth = 500;
            CarImage.Source = svgImage;
            CarImage.Width = 100;
            CarImage.Height = 100;
            CarImage.Margin = new Thickness(0, -10, 0, 0);
            await storageFile.DeleteAsync(StorageDeleteOption.Default);
        }

        public string CreateHexFromColor(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public double CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
                if (_currentPosition < Maximum)
                    CarImage.Margin = new Thickness(_currentPosition / Maximum * (ActualWidth - CarImage.ActualWidth), -10, 0, 0);
            }
        }

        public double Maximum { get; set; }

        public CarRaceBar()
        {
            _num++;
            InitializeComponent();
            Semaphore.Visibility = Collapsed;
        }

        public void End()
        {
            CarImage.HorizontalAlignment = HorizontalAlignment.Right;
            CarImage.Margin = new Thickness(0, CarImage.Margin.Top, 0, CarImage.Margin.Bottom);
        }

        public async Task SemaphoreTurnOn()
        {
            Semaphore.Visibility = Visible;

            await Task.Delay(5000).ConfigureAwait(true);

            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets//TrafficLights//yellow.svg"));
            var Svg = await StorageFileToSvgImage(file).ConfigureAwait(true);
            Svg.RasterizePixelHeight = 600;
            Semaphore.VerticalAlignment = VerticalAlignment.Center;
            Semaphore.HorizontalAlignment = HorizontalAlignment.Right;
            Semaphore.Source = Svg;

            await Task.Delay(1000).ConfigureAwait(true);

            var file2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets//TrafficLights//green.svg"));
            var Svg2 = await StorageFileToSvgImage(file2).ConfigureAwait(true);
            Svg2.RasterizePixelHeight = 600;
            Semaphore.VerticalAlignment = VerticalAlignment.Center;
            Semaphore.HorizontalAlignment = HorizontalAlignment.Right;
            Semaphore.Source = Svg2;

            await Task.Run(async () => await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await Task.Delay(2000).ConfigureAwait(true);
                Semaphore.Visibility = Visibility.Collapsed;
            })).ConfigureAwait(true);
        }
    }
}
