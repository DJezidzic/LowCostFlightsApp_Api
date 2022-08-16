﻿namespace LowCostFlightsAppApi.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string countryName { get; set; }
        public string countryCode { get; set; }
        public string regionCode { get; set; }
    }

    public class Analytics
    {
        public Travelers travelers { get; set; }
    }

    public class Location
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string name { get; set; }
        public string detailedName { get; set; }
        public string id { get; set; }
        public Self self { get; set; }
        public string timeZoneOffset { get; set; }
        public string iataCode { get; set; }
        public GeoCode geoCode { get; set; }
        public Address address { get; set; }
        public Analytics analytics { get; set; }
    }

    public class GeoCode
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
    }

    public class Meta
    {
        public Links links { get; set; }
    }

    public class Root
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
        public List<string> methods { get; set; }
    }

    public class Travelers
    {
        public int score { get; set; }
    }




    //public partial class LocationSearchResult
    //{
    //    internal LocationSearchResult() { }
    //    public string name { get; set; }
    //    public Address address { get; set; }
    //    public class Address
    //    {
    //        internal Address() { }
    //        public string cityName { get; set; }
    //        public string cityCode { get; set; }
    //    }

    //}
}
