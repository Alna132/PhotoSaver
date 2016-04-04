using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoSaver
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        Library Library = new Library();

        private async void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geopoint position = await Library.Position();
                    DependencyObject marker = Library.Marker();
                    mapWithLocation.Children.Add(marker);
                    Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(marker, position);
                    Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(marker, new Point(0.5, 0.5));
                    mapWithLocation.ZoomLevel = 12;
                    mapWithLocation.Center = position;
                    break;

                case GeolocationAccessStatus.Denied:
                    var messageDeniedDialog = new MessageDialog("Access to location is denied.");
                    await messageDeniedDialog.ShowAsync();
                    break;

                case GeolocationAccessStatus.Unspecified:
                    var messageUnknownDialog = new MessageDialog("Unspecified error.");
                    await messageUnknownDialog.ShowAsync();
                    break;
            }// End of Switch
        }// End of btnLocation_Click

        //private async void ShowMyLocationOnTheMap()
        //{
        //    // Get my current location.
        //    Geolocator myGeolocator = new Geolocator();
        //    Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
        //    Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
        //    Geocoordinate myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

        //    // Make my current location the center of the Map.
        //    this.mapWithMyLocation.Center = myGeoCoordinate;
        //    this.mapWithMyLocation.ZoomLevel = 13;
        //}

        private void btnHome_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }// End of 
        private void btnCamera_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CameraPage));
        }// End of 
    }// End of MainPage
}// End of PhotoSaver