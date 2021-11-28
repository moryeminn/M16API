using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using M16AlignAPI.Models;
using System.Web;

namespace M16AlignAPI
{
    public class GeoCodeAPI
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly string _key;
        private readonly string _url;

        public GeoCodeAPI(string url, string key)
        {
            _url = url;
            _key = key;
        }

        public void GetCoordinates(out string o_Latitude, out string o_Longitude, string address)
        {
            try
            {
                string urlWithAddress = UrlWithAdress(address);

                var httpResponseMessage = HttpClient.GetAsync(urlWithAddress).Result;
                httpResponseMessage.EnsureSuccessStatusCode();
                string responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                JToken responseConntentJson = JsonConvert.DeserializeObject<JToken>(responseBody);
                JArray resultsArr = responseConntentJson["results"] as JArray;
                JToken result = resultsArr[0];
                JToken geometry = result["geometry"];
                JToken location = geometry["location"];
                o_Latitude = location["lat"].ToString();
                o_Longitude = location["lng"].ToString();
            }
            catch(Exception ex)
            {
                o_Latitude = null;
                o_Longitude = null;
            }                                   
       }

        public string UrlWithAdress(string address)
        {
            var uriBuilder = new UriBuilder(_url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["address"] = address;
            query["key"] = _key;
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();
            return url;
        }
    }
}