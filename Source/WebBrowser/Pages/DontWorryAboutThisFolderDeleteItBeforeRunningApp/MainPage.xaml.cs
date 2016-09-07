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
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace WebBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Library Library = new Library();
        public static bool isInternet()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public MainPage()
        {
            this.InitializeComponent();
            if (isInternet())
            {
                if (Value.Text == "")
                {
                    Display.Navigate(new System.Uri("http://google.com"));
                }
            } else {
                currentLanguage = CultureInfo.CurrentUICulture;
                if (currentLanguage == "es-ES")
                {
                    Display.Navigate(new System.Uri("ms-appx-web:///WebResources/ErrorPages/Spanish/noInternet.html"));
                }
                else if (currentLanguage == "en-GB" or "en-US")
                {
                    Display.Navigate(new System.Uri("ms-appx-web:///WebResources/ErrorPages/English/noInternet.html"));
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
                Value.Text = args.Uri.ToString();
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
        
        private void settingsBrn_Click(object sender, RoutedEventArgs e
        {
            var dialog = new MessageDialog("Settings are not implemented yet!");
            await dialog.ShowAsync();
        }
        //Function for textbox selectall
        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }
    }
}