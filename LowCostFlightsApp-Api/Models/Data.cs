namespace LowCostFlightsAppApi.Models
{
    public class Address
    {
        public string CityName { get; set; }
    }

    public class LocationData
    {
        public string Name { get; set; }
        public string IataCode { get; set; }
        public Address Address { get; set; }
    }

    public class Location
    {
        public List<LocationData> Data { get; set; }
    }
}