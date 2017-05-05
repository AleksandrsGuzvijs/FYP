using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LibFYP.DTOs;
using ProductMicroService.MSFacades;

namespace ProductMicroService.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly string _cuttersUrl = "http://undercutters.azurewebsites.net/";
        private readonly string _BazaarUrl = "http://bazzasbazaar.azurewebsites.net/";
        private readonly string _DodgyUrl = "http://dodgydealers.azurewebsites.net/";

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var productsCutters = new ProductFacade.GetAll(_cuttersUrl);
                var productsBazaar = new ProductFacade.GetAll(_BazaarUrl);
                var productsDodgy = new ProductFacade.GetAll(_DodgyUrl);

                List<Product> allProducts = new List<Product>();
                allProducts.AddRange(await productsCutters);
                allProducts.AddRange(await productsBazaar);
                allProducts.AddRange(await productsDodgy);

                if (allProducts.ToList().Count > 0)
                { 
                    return Ok(allProducts);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> FindBy()
        {
            try
            {

            }
            catch
            {

            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindById(int id)
        {
            try
            {
                var productsCutters = new ProductFacade.FindById(_cuttersUrl, id);
                var productsBazaar = new ProductFacade.FindById(_BazaarUrl, id);
                var productsDodgy = new ProductFacade.FindById(_DodgyUrl, id);

                List<Product> allProducts = new List<Product>();
                allProducts.AddRange(await productsCutters);
                allProducts.AddRange(await productsBazaar);
                allProducts.AddRange(await productsDodgy);

                if (allProducts.ToList().Count > 0)
                {
                    return Ok(allProducts);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}