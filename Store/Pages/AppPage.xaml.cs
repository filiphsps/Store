﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using StoreManager.Models;
using System.Globalization;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media.Animation;

namespace Store.Pages {
    /// <summary>
    /// The App/Package view
    /// </summary>
    internal sealed partial class AppPage {
        private AppModel _app;

        public AppPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("appLogoIn");
            animation?.TryStart(this.DetailsWrapper);

            this._app = (AppModel)e.Parameter;

            if (this._app == null) {
                // TODO: include details
                throw new Exception("Invalid Application");
            }

            this.PageTitleStr.Text = this._app.Title;
            this.AppNsStr.Text = this._app.Id;
            this.AppNameStr.Text = this._app.Title;
            this.AppAuthorStr.Text = this._app.Author;
            this.AppVersionStr.Text = this._app.Version.ToString();
            this.AppDescStr.Text = this._app.Description;
            this.AppImg.Source = new BitmapImage(new Uri(this._app.LogoUrl));
            if (this._app.Timestamp != null) {
                this.AppTimestampStr.Text = this._app.Timestamp?.ToLocalTime().ToString("dd MMMM yyyy");
            }

            this.AppSizeStr.Text = this._app.Size.ToString(CultureInfo.CurrentCulture);
            this.ContributorsList.ItemsSource = this._app.Contributors;
            this.DependencyList.ItemsSource = this._app.Dependencies;

            if (this._app.Timestamp == null) {
                this.AppTimestamp.Visibility = Visibility.Collapsed;
            }

            if (this._app.Contributors.Count <= 0) {
                this.ContributorsList.Visibility = Visibility.Collapsed;
                this.ContributorsListTitle.Visibility = Visibility.Collapsed;
            }

            if (this._app.Dependencies.Count <= 0) {
                this.DependencyList.Visibility = Visibility.Collapsed;
                this.DependencyListTitle.Visibility = Visibility.Collapsed;
            }
        }

        /*
         * protected override void OnNavigatedFrom(NavigationEventArgs e) {
            base.OnNavigatedFrom(e);
            // ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("appLogoOut", this.AppImgWrapper);
        }
        */

        private void InstallBtn_Click(Object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(InstallerPage), this._app);
        }

        private void AppAuthorStr_Tapped(Object sender, TappedRoutedEventArgs e) {
            // TODO: Navigate to AuthorView
        }

        private void ShareBtn_OnClick(Object sender, RoutedEventArgs e) {
            DataTransferManager.GetForCurrentView().DataRequested += (DataTransferManager s, DataRequestedEventArgs args) => {
                args.Request.Data.SetWebLink(new Uri($"store://{this._app.Id}"));
                args.Request.Data.Properties.ContentSourceApplicationLink = new Uri($"store://{this._app.Id}");
                args.Request.Data.Properties.Title = $"Share {this._app.Title}";
                //args.Request.Data.Properties.Description = "description";
            };

            DataTransferManager.ShowShareUI();
        }
    }
}
