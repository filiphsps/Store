﻿using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Store
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(Object sender, RoutedEventArgs e)
        {
            try {
                await App.StoreManager.Initialize();

                try { await App.StoreManager.Settings.Save(); } catch { }

                // TODO: use the json data
                this.Frame.Navigate(typeof(Pages.PackagesPage), null, new DrillInNavigationTransitionInfo());
                this.Frame.BackStack.Remove(this.Frame.BackStack.Last());
            } catch (Exception ex) {
                this.Frame.Navigate(typeof(Pages.ErrorPage), ex, new DrillInNavigationTransitionInfo());
            }
        }
    }
}
