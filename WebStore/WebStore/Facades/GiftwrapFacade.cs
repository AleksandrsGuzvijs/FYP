using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LibFYP.DTOs;
using System.Net;

namespace WebStore.Facades
{
    public class GiftwrapFacade : ApiController
    {
        private readonly HttpClient _client;
        private JsonSerializerSettings _serializerSettings;
        private readonly string _baseUrl = "webapi link"; // add link later

        public GiftwrapFacade()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        [HttpGet]
        public async Task<IQueryable<Wrapping>> GetGiftwraps(string rangeName = null, string typeName = null, int? typeId = null, int? rangeId = null, double? minPrice = 0, double? maxPrice = double.MaxValue)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "getgiftwraps")
                };

                IQueryable<Wrapping> data = await RequestAsyncList<Wrapping>(request);

                data = data.Where(d => d.Price >= minPrice);
                    data = data.Where(d => d.Price <= (maxPrice == 0 ? Double.MaxValue : maxPrice));
                    if (rangeName != null) data = data.Where(d => d.RangeName == rangeName);
                    if (typeName != null) data = data.Where(d => d.TypeName == typeName);
                    if (typeId != null) data = data.Where(d => d.TypeId == typeId);
                    if (rangeId != null) data = data.Where(d => d.RangeId == rangeId);

                    return data;
            }

            catch (Exception ex)
            {
                return Enumerable.Empty<Wrapping>().AsQueryable();
            }
        }

        [HttpGet]
        public async Task<Wrapping> GetGiftwrap(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "getgiftwrap/" + id)
                };

                return await RequestAsync<Wrapping>(request);
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

        private async Task<IQueryable<T>> RequestAsyncList<T>(HttpRequestMessage request) where T : class
        {
            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(content, _serializerSettings).AsQueryable();
        }
    }
}