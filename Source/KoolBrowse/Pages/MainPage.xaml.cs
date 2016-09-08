using System;
using System.Net;
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
using Windows.UI.Popups;
using System.Net.NetworkInformation;
using System.Globalization;
using KoolBrowse.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KoolBrowse
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Libs.BrowserControls Library = new Libs.BrowserControls();
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                if (Value.Text == "")
                {
                    Display.Navigate(new System.Uri("http://google.com"));
                }
            }
            else
            {
                CultureInfo ci = CultureInfo.CurrentCulture;
                if ((ci.TwoLetterISOLanguageName == "en-GB") | (ci.TwoLetterISOLanguageName == "es-ES"))
                {
                    var url = "ms-appx-web:///WebResources/ErrorPages/" + ci.TwoLetterISOLanguageName + "/noInternet.html";
                    Display.Navigate(new System.Uri(url));
                }
                else
                {
                    var url = "ms-appx-web:///WebResources/ErrorPages/" + "en-GB" + "/noInternet.html";
                    Display.Navigate(new System.Uri(url));
                }

            }
        }

        private void Value_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Library.Go(ref Display, Value.Text, e);
        }

        private void Display_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                Value.Text = args.Uri.ToString();
            }
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                var url = args.Uri.ToString();
                System.Uri source = Display.Source;
                CultureInfo ci = CultureInfo.CurrentCulture;
                if (!url.Contains("ms-appx-web://"))
                {
                    if ((ci.TwoLetterISOLanguageName == "en-GB") | (ci.TwoLetterISOLanguageName == "es-ES"))
                    {
                        var newurl = "ms-appx-web:///WebResources/ErrorPages/" + ci.TwoLetterISOLanguageName + "/noInternet.html";
                        Display.Navigate(new System.Uri(newurl));
                    }
                    else
                    {
                        var newurl = "ms-appx-web:///WebResources/ErrorPages/" + "en-GB" + "/noInternet.html";
                        Display.Navigate(new System.Uri(newurl));
                    }
                }
            }
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Library.Back(ref Display);
        }

        private void fowardBtn_Click(object sender, RoutedEventArgs e)
        {
            Library.Forward(ref Display);
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Display.Refresh();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Display.Stop();
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage), null);
        }

        private void devBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DevelopersPage), null);
        }
    }
}
