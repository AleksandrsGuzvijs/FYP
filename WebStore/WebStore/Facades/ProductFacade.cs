using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace WebStore.Facades
{
    public class ProductFacade
    {
        private readonly HttpClient _client;

        public ProductFacade()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("");
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("products");
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
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

        [HttpGet]
        public async Task<IQueryable<Product>> GetProductsByQuery(int? catId = null, int? brandId = null, double? minPrice = 0,
            double? maxPrice = double.MaxValue, bool? InStock = null)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "products" + "/").WithQueryString("catId=" + CatId + "&brandId" + brandId + "&minPrice=" + MinPrice + "&maxPrice=" + MaxPrice + "&InStock" + InStock).Result;
                if (response.IsSuccessStatusCode)
                {
                    IQueryable<Product> data = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;

                    data = data.Where(d => d.Price >= minPrice);
                    data = data.Where(d => d.Price <= (maxPrice == 0 ? Double.MaxValue : maxPrice));
                    if (catId != null) data = data.Where(d => d.CategoryId == catId);
                    if (brandId != null) data = data.Where(d => d.BrandId == brandId);
                    if (InStock != null) data = data.Where(d => d.InStock == InStock);

                    return data;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            catch (Exception ex)
            {
                return Enumerable.Empty<Giftbox>().AsQueryable();
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProduct(int id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "products" + "/" + id);
                return response;
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}