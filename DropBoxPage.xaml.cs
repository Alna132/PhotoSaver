using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoSaver
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DropBoxPage : Page
    {
        public DropBoxPage()
        {
            this.InitializeComponent();
        }

        private void btnHome_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void btnCamera_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CameraPage));
        }

        private void btnDropbox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
