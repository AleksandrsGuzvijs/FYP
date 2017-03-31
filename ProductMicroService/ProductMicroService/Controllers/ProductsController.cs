using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LibFYP.DTOs;

namespace ProductMicroService.Controllers
{
    public class ProductsController
    {
        private readonly string _cuttersUrl = "url/";
        private readonly string _BazaarUrl = "url/";
        private readonly string _DodgyUrl = "url/";

        [HttpGet]
        public async Task<HttpResponseMessage> GetProducts()
        {
            try
            {
                var productsCutters = new Facades.ProductFacade.GetProducts(_cuttersUrl);
                var productsBazaar = new Facades.ProductFacade.GetProducts(_BazaarUrl);
                var productsDodgy = new Facades.ProductFacade.GetProducts(_DodgyUrl);

                List<Product> allProducts = new List<Product>();
                allProducts.AddRange(await productsCutters);
                allProducts.AddRange(await productsBazaar);                allProducts.AddRange(await productsDodgy);

                if (allProducts.ToList().Count > 0)
                { 
                    return Request.CreateResponse(HttpStatusCode.OK, allProducts);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }

        }
    }
}