using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace WebStore.Facades
{
    public class CategoriesFacade : ApiController
    {
        private readonly HttpClient _client;
        private JsonSerializerSettings _serializerSettings;
        private readonly string _baseUrl = "webapi link"; // add link later

        public CategoriesFacade()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        [HttpGet]
        public async Task<IQueryable<Category>> GetCategories()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "getCategories");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IQueryable<Category>>().Result;
                }
                else
                {
                    return Enumerable.Empty<Category>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Category>().AsQueryable();
            }
        }

        [HttpGet]
        public async Task<Category> GetCategory(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "getCategory/" + id)
                };

                return await RequestAsync<Category>(request);
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