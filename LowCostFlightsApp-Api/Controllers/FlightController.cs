using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using LowCostFlightsAppApi.Services;
using LowCostFlightsAppApi.Models;
using Microsoft.CodeAnalysis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LowCostFlightsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        //[HttpGet]
        //public async Task<ActionResult<CheapFlightSearchResult>> GetCheapFlightsAmadeus([FromServices] AmadeusService api, [FromQuery] string location
        //    , [FromQuery] string destination, [FromQuery] string departureDate, [FromQuery] string? returnDate, 
        //    [FromQuery] int adults, [FromQuery] Boolean nonStop)
        //{
        //    if(((location ?? destination ?? departureDate) !=null) && adults != 0)
        //    {
        //        return await api.GetCheapFlights(location, destination, departureDate, returnDate, adults, nonStop);
        //    }
        //    else { return BadRequest(); }
        //}

        // POST api/<FlightController>
        [HttpPost]
        public async Task<ActionResult<CheapFlightSearchResult>> GetCheapFlightsAmadeus([FromServices] AmadeusService api, [FromBody] FlightOffer flightOffer)
        {
            if (((flightOffer.Location ?? flightOffer.Destination ?? flightOffer.DepartureDate) != null) && flightOffer.Adults != 0)
            {
                return await api.GetCheapFlights(flightOffer.Location, flightOffer.Destination, flightOffer.DepartureDate, 
                    flightOffer.ReturnDate, flightOffer.Adults, flightOffer.NonStop);
            }
            else { return BadRequest(); }
        }

        // PUT api/<FlightController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlightController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
