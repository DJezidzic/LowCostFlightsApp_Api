namespace LowCostFlightsAppApi.Models
{
    public class FlightOffer
    {
        public string Location { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string? ReturnDate { get; set; }
        public int Adults { get; set; }
        public Boolean NonStop { get; set; }
    }
}
