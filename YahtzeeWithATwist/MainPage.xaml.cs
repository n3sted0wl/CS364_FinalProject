// Programmers:
// - Paul Antonio
// - Seth Neds
//
// Date:        April 27, 2017
// File Name:   MainPage.xaml.cs

using System;
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
using YahtzeeWithATwist.Classes;

namespace YahtzeeWithATwist
{
    /// <summary>
    ///     Manages the front-end main page of the Yahtzee Game
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Dice newDice = new Dice();
        }
    }
}
