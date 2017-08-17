using LibFYP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class BrandsController : ApiController
    {

        private Order _brand;

        public BrandsController(Order repo)
        {
            _brand = repo;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetBrands()
        {
            try
            {
                var repo = new BrandRepo(new HttpClient());
                IQueryable<Brand> results;
                results = await repo.GetAll();
                if (results.ToList().Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, results);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
        
        [HttpGet]
        public async Task<HttpResponseMessage> GetBrand(int id)
        {
            try
            {
                var repo = new BrandRepo(new HttpClient());
                Brand results;
                results = await repo.GetById(id);
                if (results == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}