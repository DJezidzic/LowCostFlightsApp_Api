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
            
            //return token.Length > 0 ? token : null;

            return token;
        }

        private void ConfigBearerTokenHeader()
        {
            var bearer = getToken();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearer}");
            http.DefaultRequestHeaders.Add("Accept", "application/vnd.amadeus+json");
        }


        public async Task<Models.Data> GetLocationOfAirports(string locationId)
        {
            Models.Data LocationObject = new Models.Data();
            var message = new HttpRequestMessage(HttpMethod.Get,
                $"v1/reference-data/locations/{locationId}");

            ConfigBearerTokenHeader();
            using (HttpResponseMessage response = await http.SendAsync(message))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                if (json != null)
                {
                    LocationObject = JsonConvert.DeserializeObject<Models.Data>(json);
                    return LocationObject;
                }
                else { throw new Exception("Response is negative."); }
            }
            //return JsonConvert.DeserializeObject<LocationSearchResult>(stream);
        }









        // Samo za testiranje
        public async Task<BusiestPeriodResults> GetBusiestTravelPeriodsOfYear(string cityCode, int year)
        {
            var message = new HttpRequestMessage(HttpMethod.Get,
                $"/v1/shopping/flight-destinations/?cityCode={cityCode}&period={year}");

            ConfigBearerTokenHeader();
            var response = await http.SendAsync(message);
            using var stream = await response.Content.ReadAsStreamAsync();
            return await System.Text.Json.JsonSerializer.DeserializeAsync<BusiestPeriodResults>(stream);
        }


        public class BusiestPeriodResults
        {
            public class Item
            {
                public string type { get; set; }
                public string period { get; set; }
                public Analytics analytics { get; set; }
                public int score => analytics.travelers.score;
            }

            public class Analytics
            {
                public Travelers travelers { get; set; }
            }

            public class Travelers
            {
                public int score { get; set; }
            }

            public List<Item> data { get; set; }
            public string graphArray => string.Join(',', data.OrderBy(x => x.period).Select(x => x.score));
        }


    }
}
