using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using LowCostFlightsAppApi.Services;
using LowCostFlightsAppApi.Models;
using Microsoft.CodeAnalysis;
using LowCostFlightsAppApi.ServiceInterfaces;

namespace LowCostFlightsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private readonly IAmadeusService _amadeusService;
        public FlightController(IAmadeusService amadeusService)
        {
            _amadeusService = amadeusService;
        }

        // POST api/<FlightController>
        [HttpPost]
        public async Task<ActionResult<CheapFlightSearchResult>> GetCheapFlightsAmadeus([FromBody] FlightOffer flightOffer)
        {
            if (((flightOffer.Location ?? flightOffer.Destination ?? flightOffer.DepartureDate) != null) && flightOffer.Adults != 0)
            {
                return this.Ok(await _amadeusService.GetCheapFlights(flightOffer.Location, flightOffer.Destination, flightOffer.DepartureDate, 
                    flightOffer.ReturnDate, flightOffer.Adults, flightOffer.NonStop));
            }
            else { return BadRequest(); }
        }

    }
}
