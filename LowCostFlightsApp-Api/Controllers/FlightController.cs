using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using LowCostFlightsAppApi.Services;
using LowCostFlightsAppApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LowCostFlightsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        // GET: api/<FlightController>
        /*[HttpGet]
        public string Get()
        {
            return "stsa";
            
        }*/

        // GET api/<FlightController>/5
        [HttpGet]
        public async Task<ActionResult<Models.Location>> GetLocation([FromServices] AmadeusService api, [FromQuery] string locationid)
        {

            if (!string.IsNullOrEmpty(locationid))
            {
                return await api.GetLocationOfAirports(locationid);
            }
            else { return BadRequest(); }

        }   

        // POST api/<FlightController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
