using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using LowCostFlightsAppApi.Services;
using LowCostFlightsAppApi.Models;
using LowCostFlightsAppApi.ServiceInterfaces;

namespace LowCostFlightsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IAmadeusService _amadeusService;
        public LocationController(IAmadeusService amadeusService)
        {
            _amadeusService = amadeusService;
        }

        // GET api/<LocationController>/5
        [HttpGet("{keyword}")]
        public async Task<ActionResult<Location>> GetLocation(string keyword)
        {

            if (!string.IsNullOrEmpty(keyword))
            {
                return this.Ok(await _amadeusService.GetLocationOfAirports(keyword));
            }
            else { return BadRequest(); }
        }

    }
}