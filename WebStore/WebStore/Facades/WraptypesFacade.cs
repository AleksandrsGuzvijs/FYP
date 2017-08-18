using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace WebStore.Facades
{
    public class WraptypesFacade : ApiController
    {
        private readonly HttpClient _client;
        private JsonSerializerSettings _serializerSettings;
        private readonly string _baseUrl = "webapi link"; // add link later

        public WraptypesFacade()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        [HttpGet]
        public async Task<IQueryable<Types>> GetWraptypes()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "getWraptypes");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IQueryable<Types>>().Result;
                }
                else
                {
                    return Enumerable.Empty<Types>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Types>().AsQueryable();
            }
        }

        [HttpGet]
        public async Task<Types> GetWraptype(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "getWraptypes/" + id)
                };

                return await RequestAsync<Types>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<T> RequestAsync<T>(HttpRequestMessage request) where T : class
        {
            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content, _serializerSettings);
        }
    }
}