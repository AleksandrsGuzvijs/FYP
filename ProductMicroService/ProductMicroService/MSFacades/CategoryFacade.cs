using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;
using Newtonsoft.Json;

namespace ProductMicroService.MSFacades
{
    public class CategoryFacade : ApiController
    {
        private HttpClient _client;
        private JsonSerializerSettings _serializerSettings;

        public CategoryFacade()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        [HttpGet]
        public async Task<IQueryable<Category>> GetCategories(string url)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("Category");
                if (response.IsSuccessStatusCode)
                {
                    var categories = response.Content.ReadAsAsync<IQueryable<Category>>().Result;
                    return categories;
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
    }
}