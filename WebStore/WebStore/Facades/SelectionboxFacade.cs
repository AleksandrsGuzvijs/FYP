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
    public class SelectionboxFacade
    {
        private readonly HttpClient _client;

        public SelectionboxFacade()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("Sample Link");
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }


        public async Task<IEnumerable<Giftbox>> GetSelectionBoxes()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "selectionbox").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Giftbox>>().Result;
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


        public async Task<HttpResponseMessage> GetSelectionBox(int id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync(_baseUrl + "selectionbox/" + id);
                return response;
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public async Task<HttpResponseMessage> PostSelectionBox(Giftbox giftbox)
        {

        }


        public async Task<HttpResponseMessage> UpdateSelectionBox(Giftbox giftbox)
        {

        }


        public async Task<HttpResponseMessage> RemoveSelectionBox(int id)
        {

        }
    }
}