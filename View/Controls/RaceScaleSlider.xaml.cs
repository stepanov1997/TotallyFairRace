using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TotallyFairRace.View.Controls
{
    public sealed partial class RaceScaleSlider : UserControl
    {
        public double ValueScaled { get; set; }

        public Func<double,double> ScaleFunction { get; set; }
        public string Unit { get; }

        public double Value
        {
            get => Slider.Value;
            set
            {
                Slider.Value = value;
                DistanceBlock.Text = (ValueScaled = Math.Round(CalculateDistance(value), 2)) + $" {Unit}";
            } 
        }

        public double Maximum
        {
            get => Slider.Maximum;
            set => Slider.Maximum = value;
        }
        public double Minimum
        {
            get => Slider.Minimum;
            set => Slider.Minimum = value;
        }

        public double StepFrequency
        {
            get => Slider.StepFrequency;
            set => Slider.StepFrequency = value;
        }

        public RaceScaleSlider() => this.InitializeComponent();

        public RaceScaleSlider(Func<double,double> scaleFunction, string unit) : this()
        {
            ScaleFunction = scaleFunction;
            Unit = unit;
            DistanceBlock.Text = (ValueScaled=Math.Round(CalculateDistance(Value), 2)) + $" {Unit}";
        }

        private double CalculateDistance(double value) => ScaleFunction.Invoke(value);

        private void ChangedValueEvent(object sender, RangeBaseValueChangedEventArgs e) =>
            DistanceBlock.Text = (ValueScaled=Math.Round(CalculateDistance(e.NewValue),2)) + $" {Unit}";
    }
}
