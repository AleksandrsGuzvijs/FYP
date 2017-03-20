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
    public class OrderFacade
    {
        private readonly HttpClient _client;

        public OrderFacade()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("Sample Link");
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }


        public async Task<HttpResponseMessage> PostOrder()
        {

        }
    }
}