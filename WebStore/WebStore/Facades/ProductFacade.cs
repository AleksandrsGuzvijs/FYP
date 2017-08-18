using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace WebStore.Facades
{
    public class ProductFacade : ApiController
    {
        private readonly HttpClient _client;
        private JsonSerializerSettings _serializerSettings;
        private readonly string _baseUrl = "webapi link"; // add link later

        public ProductFacade()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        [HttpGet]
        public async Task<IQueryable<Product>> GetProducts()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "getproducts");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IQueryable<Product>>().Result;
                }
                else
                {
                    return Enumerable.Empty<Product>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Product>().AsQueryable();
            }
        }

        [HttpGet]
        public async Task<IQueryable<Product>> GetProductsByQuery(int? catId = null, int? brandId = null, double? minPrice = 0,
            double? maxPrice = double.MaxValue, bool? InStock = null)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "getproducts")
                };

                IQueryable<Product> data = await RequestAsyncList<Product>(request);

                data = data.Where(d => d.Price >= minPrice);
                    data = data.Where(d => d.Price <= (maxPrice == 0 ? Double.MaxValue : maxPrice));
                    if (catId != null) data = data.Where(d => d.CategoryId == catId);
                    if (brandId != null) data = data.Where(d => d.BrandId == brandId);
                    if (InStock != null) data = data.Where(d => d.InStock == InStock);

                    return data;
            }

            catch (Exception ex)
            {
                return Enumerable.Empty<Product>().AsQueryable();
            }
        }

        [HttpGet]
        public async Task<Product> GetProduct(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "getproduct/" + id)
                };

                return await RequestAsync<Product>(request);
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