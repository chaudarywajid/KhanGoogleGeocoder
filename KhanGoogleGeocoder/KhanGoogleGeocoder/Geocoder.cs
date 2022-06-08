using KhanGoogleGeocoder.Models;
using KhanGoogleGeocoder.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KhanGoogleGeocoder
{
    public class Geocoder
    {
        private string ApiKey = string.Empty;
        private string Language = "ar";
        private string BaseUri = "https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&language={2}&key={3}";

        public Geocoder(string ApiKey)
        {
            this.ApiKey = ApiKey;
        }
        public async Task<GoogleGeoCodeModel> ReverseGeocode(double latitude, double longitude)
        {
            string url = string.Format(BaseUri, latitude, longitude, Language, ApiKey);
            
            GoogleGeoCodeModel model = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var request = await client.GetAsync(url);
                    var JsonResponse = await request.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<GoogleGeoCodeModel>(JsonResponse);
                }
                if (model != null && model.status == "OK")
                {
                    return model;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(model.status);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return model;
        }

        public async Task<string> ReverseGeocodeAddress(double latitude, double longitude)
        {
            string url = string.Format(BaseUri, latitude, longitude, Language, ApiKey);

            GoogleGeoCodeModel model = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var request = await client.GetAsync(url);
                    var JsonResponse = await request.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<GoogleGeoCodeModel>(JsonResponse);
                }
                if (model != null && model.status == "OK")
                {
                    var formattedaddress = model.results[0].formatted_address;
                    if (!string.IsNullOrEmpty(formattedaddress))
                    {
                        return formattedaddress;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(model.status);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }


            return string.Empty;

        }

        public async Task<string> ReverseGeocodeStreet(double latitude, double longitude)
        {
            string street = string.Empty;
            string url = string.Format(BaseUri, latitude, longitude, Language, ApiKey);

            GoogleGeoCodeModel model = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var request = await client.GetAsync(url);
                    var JsonResponse = await request.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<GoogleGeoCodeModel>(JsonResponse);
                }
                if (model != null && model.status == "OK")
                {
                    var firstaddresses = model.results[0].address_components;
                    if (firstaddresses != null && firstaddresses.Count > 0)
                    {
                        foreach (AddressComponent item in firstaddresses)
                        {
                            if (item != null && item.types != null && item.types.Count > 0)
                            {
                                if (item.types.Contains("route"))
                                {
                                    Console.WriteLine("Street: " + item.long_name);
                                    System.Diagnostics.Debug.WriteLine("Street: " + item.long_name);
                                    street = item.long_name;

                                    return street;
                                }
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(model.status);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return street;
        }

        public async Task<string> ReverseGeocodeDistrict(double latitude, double longitude)
        {
            string district = string.Empty;
            string url = string.Format(BaseUri, latitude, longitude, Language, ApiKey);

            GoogleGeoCodeModel model = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var request = await client.GetAsync(url);
                    var JsonResponse = await request.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<GoogleGeoCodeModel>(JsonResponse);
                }
                if (model != null && model.status == "OK")
                {
                    var firstaddresses = model.results[0].address_components;
                    if (firstaddresses != null && firstaddresses.Count > 0)
                    {
                        foreach (AddressComponent item in firstaddresses)
                        {
                            if (item != null && item.types != null && item.types.Count > 0)
                            {                              
                                if (item.types.Contains("political") && item.types.Contains("sublocality") && item.types.Contains("sublocality_level_1"))
                                {
                                    Console.WriteLine("District: " + item.long_name);
                                    System.Diagnostics.Debug.WriteLine("District: " + item.long_name);
                                    district = item.long_name;
                                }
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(model.status);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return district;
        }

        public async Task<StreetDistrictViewModel> ReverseGeocodeStreetAndDistrict(double latitude, double longitude)
        {
            if (latitude == 0d || longitude == 0d)
                return null;

            string street = string.Empty;
            string district = string.Empty;

            string url = string.Format(BaseUri, latitude, longitude, Language, ApiKey);
            GoogleGeoCodeModel model = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var request = await client.GetAsync(url);
                    var JsonResponse = await request.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<GoogleGeoCodeModel>(JsonResponse);
                }

                if (model != null && model.status == "OK")
                {
                    var firstaddresses = model.results[0].address_components;
                    if (firstaddresses != null && firstaddresses.Count > 0)
                    {
                        foreach (AddressComponent item in firstaddresses)
                        {
                            if (item != null && item.types != null && item.types.Count > 0)
                            {
                                if (item.types.Contains("route"))
                                {
                                    Console.WriteLine("Street: " + item.long_name);
                                    System.Diagnostics.Debug.WriteLine("Street: " + item.long_name);
                                    if (!string.IsNullOrEmpty(item.long_name))
                                    {
                                        street = item.long_name;
                                    }
                                }

                                if (item.types.Contains("political") && item.types.Contains("sublocality") && item.types.Contains("sublocality_level_1"))
                                {
                                    Console.WriteLine("District: " + item.long_name);
                                    System.Diagnostics.Debug.WriteLine("District: " + item.long_name);
                                    if (!string.IsNullOrEmpty(item.long_name))
                                    {
                                        district = item.long_name;
                                    }
                                }

                            }
                        }
                    }

                    StreetDistrictViewModel streetdistrictModel = new StreetDistrictViewModel()
                    {
                        StreetId = Guid.NewGuid().ToString(),
                        StreetName = street,
                        DistrictName = district
                    };

                    return streetdistrictModel;

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            StreetDistrictViewModel streetModel = new StreetDistrictViewModel()
            {
                StreetId = Guid.NewGuid().ToString(),
                StreetName = street,
                DistrictName = district
            };

            return streetModel;
        }


    }

}
