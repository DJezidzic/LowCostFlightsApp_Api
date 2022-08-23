namespace LowCostFlightsAppApi.Models
{
    public class CheapFlightSearchResult
    {
        public List<FlightSearchData> Data { get; set; }
    }

    public class FlightSearchData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Source { get; set; }
        public Price Price { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public List<TravelerPricing> TravelerPricings { get; set; }

    }

    public class Departure
    {
        public string IataCode { get; set; }
        public string Zerminal { get; set; }
        public DateTime At { get; set; }
    }
    public class Price
    {
        public string Currency { get; set; }
        public string Total { get; set; }
        public string GrandTotal { get; set; }
    }

    public class Segment
    {
        public Departure Departure { get; set; }
        public string CarrierCode { get; set; }
        public string Number { get; set; }
        public int NumberOfStops { get; set; }
    }

    public class Itinerary
    {
        public string Duration { get; set; }
        public List<Segment> Segments { get; set; }
    }

    public class FareDetailsBySegment
    {
        public string Cabin { get; set; }
        
    }

    public class TravelerPricing
    {
        public string FareOption { get; set; }
        public List<FareDetailsBySegment> FareDetailsBySegment { get; set; }
    }

}
