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
    public class CategoriesController : ApiController
    {
        private readonly string _cuttersUrl = "http://undercutters.azurewebsites.net/";
        private readonly string _BazaarUrl = "http://bazzasbazaar.azurewebsites.net/";
        private readonly string _DodgyUrl = "http://dodgydealers.azurewebsites.net/";

        [HttpGet]
        public async Task<HttpResponseMessage> GetCategories()
        {
            try
            {
                var categoriesCutters = new Facades.CategoryFacade.GetCategories(_cuttersUrl);
                var categoriesBazaar = new Facades.CategoryFacade.GetCategories(_BazaarUrl);
                var categoriesDodgy = new Facades.CategoryFacade.GetCategories(_DodgyUrl);

                List<Category> allCategories = new List<Category>();
                allCategories.AddRange(await categoriesCutters);
                allCategories.AddRange(await categoriesBazaar);
                allCategories.AddRange(await categoriesDodgy);

                if (allCategories.ToList().Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, allCategories);
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
