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
using KoolBrowse.Utils;
using KoolBrowse.ViewModels;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KoolBrowse.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }
        #region Settings Vars
        public bool StartOnSearchEngine
        {
            get { return SettingsService.Instance.StartOnSearchEngine; }
            set { SettingsService.Instance.StartOnSearchEngine = value; }
        }
        #endregion

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += onBack;// Event handler goes here
        }
        private void goBackBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }
        private void Update_StartOnSearchEngine(object sender, RoutedEventArgs e)
        {
            //var val = StartOnEngineCheckBox.IsEnabled;
        }
        private void onBack(object sender, BackRequestedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }
    }
}
