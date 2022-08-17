using Newtonsoft.Json;
using RestSharp;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using LowCostFlightsAppApi.Models;

namespace LowCostFlightsAppApi.Services
{
    public class AmadeusService
    {

        private HttpClient http;
        private string apiKey;
        private string apiSecret;

        public AmadeusService(IConfiguration config, IHttpClientFactory httpFactory)
        {
            //apiKey = config.GetValue<string>("AmadeusAPI:APIKey");
            //apiSecret = config.GetValue<string>("AmadeusAPI:APISecret");
            http = httpFactory.CreateClient("AmadeusServiceApi");
        }
        private string getToken()
        {
            // Fetching Oauth2 Bearer token from Amadeus
            string url = "https://test.api.amadeus.com/v1/security/oauth2/token";
            string client_id = "KLDkPx97EGepKI8VmVWgSoKat5gFDKYQ";
            string client_secret = "qVP7XaGJZkR6WTZQ";
            string token;
            //request token
            var restclient = new RestClient(url);
            RestRequest request = new RestRequest("request/oauth") { Method = Method.Post };
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);
            var tResponse = restclient.Execute(request);
            var responseJson = tResponse.Content;
            try
            {
                token = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson)["access_token"].ToString();
            }
            catch { throw new Exception("Auth failed to fetch"); }
            return token;
        }

        private void ConfigBearerTokenHeader()
        {
            var bearer = getToken();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearer}");
            http.DefaultRequestHeaders.Add("Accept", "application/vnd.amadeus+json");
        }


        public async Task<CheapFlightSearchResult> GetCheapFlights(string location, string destination, 
            string departureDate, string? returnDate, int adults, Boolean nonStop)
        {
            CheapFlightSearchResult cheapFlightSearchResult = new CheapFlightSearchResult();
            var reqMessage = new HttpRequestMessage(HttpMethod.Get,
                $"v2/shopping/flight-offers?originLocationCode={location}&destinationLocationCode={destination}&departureDate={departureDate}" +
                $"&returnDate={returnDate}&adults={adults}&nonStop={nonStop.ToString().ToLower()}"); //hardkodirano kao false prolazi
            ConfigBearerTokenHeader();
            using (HttpResponseMessage response = await http.SendAsync(reqMessage))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                if (json != null)
                {
                    cheapFlightSearchResult = JsonConvert.DeserializeObject<CheapFlightSearchResult>(json);
                    return cheapFlightSearchResult;
                }
                else { throw new Exception("Response is negative."); }
            }

        }
        
        public async Task<Location> GetLocationOfAirports(string keyword)
        {

            Location LocationObject = new Location();
            var message = new HttpRequestMessage(HttpMethod.Get,
                $"v1/reference-data/locations?subType=CITY,AIRPORT&keyword={keyword.ToUpper()}");

            ConfigBearerTokenHeader();
            using (HttpResponseMessage response = await http.SendAsync(message))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                if (json != null)
                {
                    LocationObject = JsonConvert.DeserializeObject<Location>(json);
                    return LocationObject;
                }
                else { throw new Exception("Response is negative."); }
            }
        }
    }
}
