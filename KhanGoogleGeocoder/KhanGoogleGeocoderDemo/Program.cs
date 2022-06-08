using KhanGoogleGeocoder;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KhanGoogleGeocoderDemo
{
    class Program
    {      
        static void Main(string[] args)
        {
            Console.WriteLine("Starting....");
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            TestReverseGeocode();

            
            Console.ReadLine();
        }


        private static async void TestReverseGeocode()
        {
            Console.WriteLine("Start Geocoding...");

            double latitude = 24.5536778820679;
            double longitude = 46.5208469610661;

            //please replace the api key with your own key
            Geocoder geocoder = new Geocoder("PrtyuiC34pYY7ujnhyz-tYeNqUrxghtrfdpqu2K");
            
            //get main model status
            var model = await geocoder.ReverseGeocode(latitude, longitude);
            Console.WriteLine("Model Status: " + model.status);
            Debug.WriteLine("Model Status: " + model.status);

            //get formatted address
            var address = await geocoder.ReverseGeocodeAddress(latitude, longitude);
            Console.WriteLine("Address: " + address);
            Debug.WriteLine("Address: " + address);

            //get the street name
            var street = await geocoder.ReverseGeocodeStreet(latitude, longitude);
            Console.WriteLine("Street: " + street);
            Debug.WriteLine("Street: " + street);

            //get district name
            var district = await geocoder.ReverseGeocodeDistrict(latitude, longitude);
            Console.WriteLine("District: " + district);
            Debug.WriteLine("District: " + district);

            //get the street and district name
            var streetdistrict = await geocoder.ReverseGeocodeStreetAndDistrict(latitude, longitude);
            Console.WriteLine("District: " + streetdistrict.DistrictName);
            Debug.WriteLine("District: " + streetdistrict.DistrictName);
            Console.WriteLine("Street: " + streetdistrict.StreetName);
            Debug.WriteLine("Street: " + streetdistrict.StreetName);


            Console.WriteLine("Ending Geocoding....");

        }


    }
}
