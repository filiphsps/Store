﻿using System;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class SettingsPage {
        public SettingsPage() {
            this.InitializeComponent();

            PackageVersion version = Package.Current.Id.Version;
            this.AppVersionStr.Text = String.Format($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");

            this.AppPkgsStr.Text = App.StoreManager.Packages.Count.ToString();
            this.AppDepsStr.Text = App.StoreManager.Dependencies.Count.ToString();
            this.AppResCntStr.Text = App.StoreManager.Repositories.Count.ToString();
            this.ReposList.ItemsSource = App.StoreManager.Repositories;
        }
    }
}
