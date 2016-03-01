using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Selfwin.Items
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadItemView : Page
    {
        public ReadItemView()
        {
            this.InitializeComponent();
        }

        private const string HtmlBodyStart = @"
<html>
<head>
<meta name=""viewport"" content=""width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1"">
</head>
<body>";

        public const string HtmlBodyEnd = @"
</body>
</html>";

        private void OnWebViewLoaded(object sender, RoutedEventArgs e)
        {
            var webView = sender as WebView;
            var item = this.DataContext as ReadItemViewModel;
            if (webView != null && item != null)
            {
               webView.NavigateToString(HtmlBodyStart + item.Html + HtmlBodyEnd);
            }
        }

        private void OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            var webView = sender as WebView;
            if (webView != null && args?.Uri != null)
            {
                var uri = args.Uri;
                args.Cancel = true;
                Launcher.LaunchUriAsync(uri);
            }
        }
    }
}
