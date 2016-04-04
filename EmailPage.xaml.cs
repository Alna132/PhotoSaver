using System;
using Windows.ApplicationModel.Email;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoSaver
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmailPage : Page
    {
        public EmailPage()
        {
            this.InitializeComponent();
        }// End of EmailPage

        // Navigate to the HomePage or CameraPage
        private void btnHome_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void btnCamera_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CameraPage));
        }


        private async void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            EmailMessage emailMessage = new EmailMessage();

            // The user is asked to input their email to the textbox provided.
            // Validation to come in the future.
            string userEmail = txtEmail.Text;
            emailMessage.To.Add(new EmailRecipient(userEmail));

            whereAmI();
            // A message is added to tell the user where the photo came from.
            string messageBody = "Photo taken at " + strLocation + "  was sent from PhotoSaver App";
            emailMessage.Body = messageBody;

            // The image is taken/selected from the folder as directed by the user and attached to the email.
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            // This will become more automated untill no user direction is needed and the 
            //image captured is sent straight to the email without being saved in the phone.

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(file);
                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(
                         file.Name,
                         stream);
                emailMessage.Attachments.Add(attachment);
            }// End of if
            
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }// End of btnEmail_Click

        public string strLocation;
        public async void whereAmI()
        {
            // Get my current location.
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            strLocation = myGeocoordinate.ToString();
            return;
        }
    }// End of EmailPage
}// End of PhotoSaver
