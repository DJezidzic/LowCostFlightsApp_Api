namespace LowCostFlightsAppApi.Models
{
    public class CheapFlightSearchResult
    {
        public List<FlightSearchData> Data { get; set; }
    }

    public class FlightSearchData
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public Price price { get; set; }
        public List<Itinerary> itineraries { get; set; }
        public List<TravelerPricing> travelerPricings { get; set; }

    }

    public class Departure
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }
    public class Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string grandTotal { get; set; }
    }

    public class Segment
    {
        public Departure departure { get; set; }
        public string carrierCode { get; set; }
        public string number { get; set; }
        public int numberOfStops { get; set; }
    }

    public class Itinerary
    {
        public string duration { get; set; }
        public List<Segment> segments { get; set; }
    }

    public class FareDetailsBySegment
    {
        public string cabin { get; set; }
        
    }

    public class TravelerPricing
    {
        public string fareOption { get; set; }
        public List<FareDetailsBySegment> fareDetailsBySegment { get; set; }
    }


    //public class CheapData
    //{
    //    public string Origin { get; set; }
    //    public string Destination { get; set; }
    //    public string DepartureDate { get; set; }
    //    public string? ReturnDate { get; set; }
    //    public Price price { get; set; }

    //}
    //public class Price
    //{
    //    public string Total { get; set; }
    //}

}
