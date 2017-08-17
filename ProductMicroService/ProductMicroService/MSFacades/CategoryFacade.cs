using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace ProductMicroService.MSFacades
{
    public class CategoryFacade : ApiController
    {
        private HttpClient _client;

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories(string url)
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
                    var categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result;
                    return categories;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}