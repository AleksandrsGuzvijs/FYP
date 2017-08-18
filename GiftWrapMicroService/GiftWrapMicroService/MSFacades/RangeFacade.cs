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
    public class RangeFacade : ApiController
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
                response = await _client.GetAsync("/api/Range");
                if (response.IsSuccessStatusCode)
                {
                    var Range = response.Content.ReadAsAsync<IEnumerable<Range>>().Result;
                    // todo: enter name of the store into StoreName field of each Range object
                    return Request.CreateResponse<IEnumerable<Range>>(HttpStatusCode.OK, Range);
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
                response = await _client.GetAsync("/api/Range/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Range = response.Content.ReadAsAsync<Range>().Result;
                    return Request.CreateResponse<Range>(HttpStatusCode.OK, Range);
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