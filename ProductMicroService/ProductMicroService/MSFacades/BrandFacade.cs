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
    public class BrandFacade : ApiController
    {
        private HttpClient _client;

        [HttpGet]
        public async Task<IEnumerable<Brand>> GetBrands(string url)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("Brand");
                if (response.IsSuccessStatusCode)
                {
                    var brands = response.Content.ReadAsAsync<IEnumerable<Brand>>().Result;
                    return brands;
                }
                else
                {
                    return Enumerable.Empty<Brand>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Brand>().AsQueryable();
            }
        }
    }
}