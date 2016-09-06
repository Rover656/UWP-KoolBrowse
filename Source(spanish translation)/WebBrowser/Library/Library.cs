
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

public class Library
{
    public void Back(ref WebView web)
    {
        if (web.CanGoBack)
            web.GoBack();
    }
    public void Forward(ref WebView web)
    {
        if (web.CanGoForward)
            web.GoForward();
    }
    public void Go(ref WebView web,string value, KeyRoutedEventArgs args)
    {
        if(args.Key == Windows.System.VirtualKey.Enter)
        {
            try
            {
                if (value == "") {
                    web.Navigate(new System.Uri("http://google.com"));
                } else if (value.StartsWith("http://") || value.StartsWith("https://")) {
                    try
                    {
                        web.Navigate(new System.Uri(value));
                    } catch
                    {

                    }
                } else if (value.Contains(".")) {
                    string newvalue = "http://" + value;
                    web.Navigate(new System.Uri(newvalue));
                } else {
                    string newvalue = value.Replace(" ", "+");
                    web.Navigate(new System.Uri("http://google.com/search?q=" + newvalue));
                }
            }
            catch
            {

            }
            web.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
    }
}