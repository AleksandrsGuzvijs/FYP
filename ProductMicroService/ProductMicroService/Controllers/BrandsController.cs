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
    public class BrandsController : ApiController
    {
        private readonly string _cuttersUrl = "http://undercutters.azurewebsites.net/";
        private readonly string _BazaarUrl = "http://bazzasbazaar.azurewebsites.net/";
        private readonly string _DodgyUrl = "http://dodgydealers.azurewebsites.net/";

        [HttpGet]
        public async Task<HttpResponseMessage> GetBrands()
        {
            try
            {
                var brandsCutters = new Facades.BrandFacade.GetBrands(_cuttersUrl);
                var brandsBazaar = new Facades.BrandFacade.GetBrands(_BazaarUrl);
                var brandsDodgy = new Facades.BrandFacade.GetBrands(_DodgyUrl);

                List<Brand> allBrands = new List<Brand>();
                allBrands.AddRange(await brandsCutters);
                allBrands.AddRange(await brandsBazaar);
                allBrands.AddRange(await brandsDodgy);

                if (allBrands.ToList().Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, allBrands);
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
