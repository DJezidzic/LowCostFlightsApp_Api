using Newtonsoft.Json;
using RestSharp;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using LowCostFlightsAppApi.Models;
using LowCostFlightsAppApi.ServiceInterfaces;

namespace LowCostFlightsAppApi.Services
{
    public class AmadeusService : IAmadeusService
    {

        private HttpClient _http;
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _tokenUrl;

        public AmadeusService(IConfiguration config, IHttpClientFactory httpFactory)
        {
            _apiKey = config.GetValue<string>("AmadeusAPI:APIKey");
            _apiSecret = config.GetValue<string>("AmadeusAPI:APISecret");
            _tokenUrl = config.GetValue<string>("AmadeusServiceTokenUrl");
            _http = httpFactory.CreateClient("AmadeusServiceApi");
        }
        private string GetToken()
        {
            // Fetching Oauth2 Bearer token from Amadeus
            //string client_id = "KLDkPx97EGepKI8VmVWgSoKat5gFDKYQ";
            //string client_secret = "qVP7XaGJZkR6WTZQ";

            //request token
            var restclient = new RestClient(_tokenUrl);
            RestRequest request = new RestRequest("request/oauth") { Method = Method.Post };
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _apiKey);
            request.AddParameter("client_secret", _apiSecret);
            var tResponse = restclient.Execute(request);
            var responseJson = tResponse.Content;
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson)["access_token"].ToString();
            }
            catch { throw new Exception("Auth failed to fetch"); }
        }

        private void ConfigBearerTokenHeader()
        {
            var bearer = GetToken();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearer}");
            _http.DefaultRequestHeaders.Add("Accept", "application/vnd.amadeus+json");
        }


        public async Task<CheapFlightSearchResult> GetCheapFlights(string location, string destination,
            string departureDate, string? returnDate, int adults, Boolean nonStop)
        {
            HttpRequestMessage reqMessage = new();
            if (!string.IsNullOrEmpty(returnDate))
            {
                reqMessage = new HttpRequestMessage(HttpMethod.Get,
                $"v2/shopping/flight-offers?originLocationCode={location}&destinationLocationCode={destination}&departureDate={departureDate}" +
                $"&returnDate={returnDate}&adults={adults}&nonStop={nonStop.ToString().ToLower()}");
            }
            else
            {
                reqMessage = new HttpRequestMessage(HttpMethod.Get,
                $"v2/shopping/flight-offers?originLocationCode={location}&destinationLocationCode={destination}&departureDate={departureDate}" +
                $"&adults={adults}&nonStop={nonStop.ToString().ToLower()}");
            }

            ConfigBearerTokenHeader();
            using (HttpResponseMessage response = await _http.SendAsync(reqMessage))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                if (json != null)
                {
                    return JsonConvert.DeserializeObject<CheapFlightSearchResult>(json);
                }
                else { throw new Exception("Response is negative."); }
            }

        }

        public async Task<Location> GetLocationOfAirports(string keyword)
        {

            var message = new HttpRequestMessage(HttpMethod.Get,
                $"v1/reference-data/locations?subType=CITY,AIRPORT&keyword={keyword.ToUpper()}");

            ConfigBearerTokenHeader();
            using HttpResponseMessage response = await _http.SendAsync(message);
            using HttpContent content = response.Content;
            string json = await content.ReadAsStringAsync();
            if (json != null)
            {
                return JsonConvert.DeserializeObject<Location>(json);
            }
            else { throw new Exception("Response is negative."); }
        }
    }
}
