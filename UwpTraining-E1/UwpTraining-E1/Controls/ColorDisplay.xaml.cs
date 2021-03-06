﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UwpTraining_E1.Controls
{
    public sealed partial class ColorDisplay : UserControl
    {
        public string FavColor
        {
            get { return (string)GetValue(FavColorProperty); }
            set { SetValue(FavColorProperty, value); }
        }

        public static readonly DependencyProperty FavColorProperty =
            DependencyProperty.Register("FavColor", typeof(string), typeof(ColorDisplay), new PropertyMetadata(0));

        public ColorDisplay()
        {
            this.InitializeComponent();
        }
    }
}
