using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;
using Newtonsoft.Json;

namespace GiftWrapMicroService.MSFacades
{
    public class GiftWrapFacade : ApiController
    {
        private HttpClient _client;


        public async Task<HttpResponseMessage> GetAll()
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("http://khanskwikimart.azurewebsites.net/");
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("/api/Giftwrap");
                if (response.IsSuccessStatusCode)
                {
                    var giftwrap = response.Content.ReadAsAsync<IEnumerable<Wrapping>>().Result;
                    // todo: enter name of the store into StoreName field of each giftwrap object
                    return Request.CreateResponse<IEnumerable<Wrapping>>(HttpStatusCode.OK, giftwrap);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetAll");
            }
        }

        public async Task<HttpResponseMessage> FindBy()
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("http://khanskwikimart.azurewebsites.net/");
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");


            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing FindBy");
            }
        }

        public async Task<HttpResponseMessage> FindById(int id)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("http://khanskwikimart.azurewebsites.net/");
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("/api/Giftwrap/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var giftwrap = response.Content.ReadAsAsync<Wrapping>().Result;
                    return Request.CreateResponse<Wrapping>(HttpStatusCode.OK, giftwrap);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing FindById");
            }
        }
    }
}