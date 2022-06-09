# KhanGoogleGecoder

KhanGoogleGeocoder is easy to use .Net Library

## Getting started...
KhanGoogleGeocoder reverse geocoder is extremely easy to integrate and consume.

The Library consist of 5 reverse geocoding methods:

ReverseGeocode Method: This method take the latitude and longitude and return the full response.
ReverseGeocodeAddress: This method take the latitude and longitude and return the formatted address.
ReverseGeocodeStreet: This method take the latitude and longitude and return the full street name.
ReverseGeocodeDistrict: This method take the latitude and longitude and return the district name.
ReverseGeocodeStreetAndDistrict: This method take the latitude and longitude and return the full street name & district name.


Initialize the Constructor
//Please replace the api key with your own key Geocoder geocoder = new Geocoder("PrtyuiC34pYY7ujnhyz-tYeNqUrxghtrfdpqu2K");

//Get main model status var model = await geocoder.ReverseGeocode(latitude, longitude); Console.WriteLine("Model Status: " + model.status); Debug.WriteLine("Model Status: " + model.status);

//Get formatted address 
var address = await geocoder.ReverseGeocodeAddress(latitude, longitude); 
Console.WriteLine("Address: " + address); 
Debug.WriteLine("Address: " + address);

//Get the street name 
var street = await geocoder.ReverseGeocodeStreet(latitude, longitude); 
Console.WriteLine("Street: " + street); 
Debug.WriteLine("Street: " + street);

//Get district name 
var district = await geocoder.ReverseGeocodeDistrict(latitude, longitude); 
ConsolGe.WriteLine("District: " + district); 
Debug.WriteLine("District: " + district);

Supported Operations The following operations are supported.

Google Maps Reverse Geocode Location
