using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TotallyFairRace.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TotallyFairRace.Controller;
using TotallyFairRace.View.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TotallyFairRace.View.ContentDialogs
{
    public sealed partial class PregameDialog : ContentDialog
    {
        public Race Race { get; set; }
        private RaceScaleSlider SliderDistance { get; set; }
        private RaceScaleSlider SliderBid { get; set; }
        private RaceScaleSlider SliderNumOfCars { get; set; }
        public PregameDialog()
        {
            this.InitializeComponent();
            SliderDistance = new RaceScaleSlider(Math.Exp, "km")
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StepFrequency = 0.01, Minimum = 0, Maximum = 7
            };
            BorderDistance.Child = SliderDistance;
            SliderBid = new RaceScaleSlider(e=>e, "RP")
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StepFrequency = 0.01, Minimum = 0, Maximum = 7
            };
            BorderBid.Child = SliderBid;
            SliderNumOfCars = new RaceScaleSlider(e => e, "CARS")
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StepFrequency = 1, Minimum = 2, Maximum = 10
            };
            BorderNumOfCars.Child = SliderNumOfCars;

            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(BackgroundProperty, Colors.DarkSlateGray));
            buttonStyle.Setters.Add(new Setter(ForegroundProperty, Colors.White));
            this.CloseButtonStyle = buttonStyle;
            this.PrimaryButtonStyle = buttonStyle;
            this.SecondaryButtonStyle = buttonStyle;

            var textBox = new TextBlock
            {
                FontFamily = new FontFamily("Stencil"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff)),
                TextAlignment = TextAlignment.Center,
                Width = 500,
                Text = "START GAME"
            };
            Title = textBox;
        }

        private void StartSimulationClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = false;
            var color = CarColorPicker.Color;
            var selectedDistance = (decimal)SliderDistance.ValueScaled;
            var numOfCars= (int)SliderNumOfCars.ValueScaled;
            var selectedBid = (decimal)SliderBid.ValueScaled;
            if (selectedBid > (decimal) Account.CurrentAccount.Money)
            {
                Race = null;
            }
            else
                Race = new Race(color, numOfCars, selectedDistance, selectedBid, null, null, null);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
