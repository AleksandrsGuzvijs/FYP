using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using LibFYP.DTOs;
using System.Net;

namespace WebStore.Facades
{
    public class OrderFacade : ApiController
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "webapi link"; // add link later

        public OrderFacade()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("");
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }


        public async Task<HttpResponseMessage> PostOrder(Order order)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                response = await _client.PostAsJsonAsync(_baseUrl + "order/", order);
                return response;
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}