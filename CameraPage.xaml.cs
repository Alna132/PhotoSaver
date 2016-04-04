using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoSaver
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CameraPage : Page
    {
        public CameraPage()
        {
            this.InitializeComponent();


        }// End of 

        // Navigate to the homepage
        private void btnHome_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }// End of 

        public StorageFile storeFile;
        public IRandomAccessStream stream;  
        private async void btnCapture_Click(object sender, RoutedEventArgs e)
        {  
            // Camera is accessed and user can take a photo which is shown in the app.
            CameraCaptureUI capture = new CameraCaptureUI();  
            capture.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;  
            capture.PhotoSettings.CroppedAspectRatio = new Size(3, 5);  
            capture.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.HighestAvailable;  
            storeFile = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);  
            if (storeFile != null)  
            {  
                BitmapImage bimage = new BitmapImage();  
                stream = await storeFile.OpenAsync(FileAccessMode.Read);;  
                bimage.SetSource(stream);  
                captureImage.Source = bimage;
            }// End of   if
        }// End of btnCapture_Click
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Saves image in a folder of the users choosing with a name of their choosing.
                // An optional name is suggested/provided as default.
                FileSavePicker fs = new FileSavePicker();
                fs.FileTypeChoices.Add("Image", new List<string>()
                {
                    ".jpeg"
                });
                fs.DefaultFileExtension = ".jpeg";
                fs.SuggestedFileName = "Image" + DateTime.Today.ToString();
                fs.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                fs.SuggestedSaveFile = storeFile;

                // Saving the file  
                var s = await fs.PickSaveFileAsync();
                if (s != null)
                {
                    using (var dataReader = new DataReader(stream.GetInputStreamAt(0)))
                    {
                        await dataReader.LoadAsync((uint)stream.Size);
                        byte[] buffer = new byte[(int)stream.Size];
                        dataReader.ReadBytes(buffer);
                        await FileIO.WriteBytesAsync(s, buffer);
                    }// End of using
                }// End of if
            }// End of try
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("Unable to save at this time.");
                await messageDialog.ShowAsync();
            }// End of catch

            this.Frame.Navigate(typeof(ConfirmationPage));
        }// End of btnSave_Click

        // Navigate to Email page
        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmailPage));
        }// End of btnEmail_Click

        // Navigate to DropBox page (InFuture)
        //private void btnDropBox_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(DropBoxPage));
        //}
    }// End of CameraPage
}// End of PhotoSaver
