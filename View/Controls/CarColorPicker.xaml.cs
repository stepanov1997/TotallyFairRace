﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TotallyFairRace.View.Controls
{
    public sealed partial class CarColorPicker : UserControl
    {
        public Color Color
        {
            get => ColorPicked.Color;
            set => ColorPicked.Color = value;
        }
        public CarColorPicker()
        {
            this.InitializeComponent();
            Color = ColorPicked.Color;
        }
    }
}
