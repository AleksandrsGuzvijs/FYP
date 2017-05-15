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
    public class GiftwrapFacade
    {
        private readonly HttpClient _client;

        public GiftwrapFacade()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("");
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        [HttpGet]
        public async Task<IQueryable<Wrapping>> GetGiftwraps(string rangeName = null, string typeName = null, int? typeId = null, int? rangeId = null, double? minPrice = 0, double? maxPrice = double.MaxValue)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "giftwraps" + "/").WithQueryString("rangeName=" + rangeName + "&typeName" + typeName + "&typeId" + typeId + "&rangeId" + rangeId + "&minPrice=" + MinPrice + "&maxPrice=" + MaxPrice).Result;
                if (response.IsSuccessStatusCode)
                {
                    IQueryable<Wrapping> data = response.Content.ReadAsAsync<IEnumerable<Wrapping>>().Result;

                    data = data.Where(d => d.Price >= minPrice);
                    data = data.Where(d => d.Price <= (maxPrice == 0 ? Double.MaxValue : maxPrice));
                    if (rangeName != null) data = data.Where(d => d.RangeName == rangeName);
                    if (typeName != null) data = data.Where(d => d.TypeName == typeName);
                    if (typeId != null) data = data.Where(d => d.TypeId == typeId);
                    if (rangeId != null) data = data.Where(d => d.RangeId == rangeId);

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
        public async Task<HttpResponseMessage> GetGiftwrap(int id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "giftwraps" + "/" + id);
                return response;
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}