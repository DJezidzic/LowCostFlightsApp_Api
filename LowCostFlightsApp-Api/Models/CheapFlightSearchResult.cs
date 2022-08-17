namespace LowCostFlightsAppApi.Models
{
    public class CheapFlightSearchResult
    {
        public List<CheapData> cheapDatas { get; set; }
    }
    public class CheapData
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string? ReturnDate { get; set; }
        public Price price { get; set; }

    }
    public class Price
    {
        public string Total { get; set; }
    }

}
