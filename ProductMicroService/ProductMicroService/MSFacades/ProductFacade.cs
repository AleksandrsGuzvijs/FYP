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

                string storeLinkName;
                if (url == "http://undercutters.azurewebsites.net/")
                {
                    storeLinkName = "Undercutters";
                }
                else if (url == "http://dodgydealers.azurewebsites.net/")
                {
                    storeLinkName = "Dodgydealers";
                }
                else
                {
                    storeLinkName = "Bazzasbazaar";
                }

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("/api/Product");
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                    // todo: enter name of the store data was pulled from, into StoreName field of each product object
                    //foreach(string StoreName in products)
                    //{

                    //}
                    return Request.CreateResponse<IEnumerable<Product>>(HttpStatusCode.OK ,products);
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

        public async Task<HttpResponseMessage> FindBy(string url)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                string storeLinkName;
                if (url == "http://undercutters.azurewebsites.net/")
                {
                    storeLinkName = "Undercutters";
                }
                else if (url == "http://dodgydealers.azurewebsites.net/")
                {
                    storeLinkName = "Dodgydealers";
                }
                else
                {
                    storeLinkName = "Bazzasbazaar";
                }


            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing FindBy");
            }
        }

        public async Task<HttpResponseMessage> FindById(string url,int id)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(url);
                _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                string storeLinkName;
                if (url == "http://undercutters.azurewebsites.net/")
                {
                    storeLinkName = "Undercutters";
                }
                else if (url == "http://dodgydealers.azurewebsites.net/")
                {
                    storeLinkName = "Dodgydealers";
                }
                else
                {
                    storeLinkName = "Bazzasbazaar";
                }

                HttpResponseMessage response = new HttpResponseMessage();
                response = await _client.GetAsync("/api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<Product>().Result;
                    return Request.CreateResponse<Product>(HttpStatusCode.OK, products);
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