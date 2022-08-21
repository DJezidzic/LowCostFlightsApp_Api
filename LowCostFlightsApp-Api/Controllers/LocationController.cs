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
    public class LocationController : ControllerBase
    {
        // GET api/<LocationController>/5
        [HttpGet("{keyword}")]
        public async Task<ActionResult<Location>> GetLocation([FromServices] AmadeusService api, string keyword)
        {

            if (!string.IsNullOrEmpty(keyword))
            {
                return await api.GetLocationOfAirports(keyword);
            }
            else { return BadRequest(); }
        }

        // POST api/<LocationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}