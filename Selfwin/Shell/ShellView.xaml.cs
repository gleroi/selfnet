using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Selfwin.Shell
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : Page
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void OnMenuToggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.SplitView.IsPaneOpen = !this.SplitView.IsPaneOpen;
        }

        private void OnTitleBarLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var ui = sender as UIElement;
            if (ui != null)
            {
                Window.Current.SetTitleBar(ui);
            }
        }
    }
}