using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace ProductMicroService.Facades
{
    public class ProductFacade
    {
        private HttpClient _client;
        
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts(string url)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "products").Result;
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                    return products;
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