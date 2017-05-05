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
    public class ProductFacade
    {
        private HttpClient _client;
        
        public async Task<HttpResponseMessage> GetAll(string url)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("/api/Product");
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
            catch
            {
                return NotFound;
            }
        }

        public async Task<IEnumerable<Product>> FindBy(string url)
        {
            try
            {

            }
            catch
            {
                return NotFound;
            }
        }

        public async Task<Product> FindById(string url,int id)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("/api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<Product>().Result;
                    return Ok(products);
                }
                else
                {
                    return NotFound;
                }
            }
            catch
            {
                return NotFound;
            }
        }
    }
}