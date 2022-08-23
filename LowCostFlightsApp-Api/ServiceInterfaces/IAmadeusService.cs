using LowCostFlightsAppApi.Models;

namespace LowCostFlightsAppApi.ServiceInterfaces
{
    public interface IAmadeusService
    {
        Task<CheapFlightSearchResult> GetCheapFlights(string location, string destination, string departureDate, string? returnDate, int adults, bool nonStop);
        Task<Location> GetLocationOfAirports(string keyword);
    }
}