using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LibFYP.DTOs;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        /*private Product _productRepo;
        
        public ProductsController(Product _repo)
        {
            _productRepo = _repo;
        }*/
        
        [HttpGet]
        public async Task<HttpResponseMessage> GetProducts(int? catId = null, int? brandId = null, double? minPrice = 0,
            double? maxPrice = double.MaxValue, bool? InStock = null)
        {
            try
            {
                var repo = new ProductRepo(new HttpClient());
                IQueryable<Product> results;
               
                if((catId != null) || (brandId != null) || (minPrice != 0) || (maxPrice != double.MaxValue))
                {
                    // If params passed
                    var query = new Dictionary<string, dynamic>();
                    if (catId != null)
                    {
                        query.Add("catId", catId);
                    }
                    if (brandId != null)
                    {
                        query.Add("brandId", brandId);
                    }

                    if (minPrice != 0)
                    {
                        query.Add("minPrice", minPrice);
                    }

                    if (maxPrice != double.MaxValue)
                    {
                        query.Add("maxPrice", maxPrice);
                    }
                    results = await repo.FindBy(query);
                    results = results.Where(p => p.InStock == true);
                    if (results.ToList().Count > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, results);
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
                }
                results = await repo.GetAll();
                results = results.Where(p => p.InStock == true);
                if (results.ToList().Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, results);
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }


        [HttpGet]
        public async Task<HttpResponseMessage> GetProduct(int id)
        {
            try
            {
                var repo = new CacheAdapter.Core.ProductRepo(new HttpClient());
                Product results;
                results = await repo.GetById(id);
                if (results.InStock != true || results == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
                }
                    return Request.CreateResponse(HttpStatusCode.OK, results);
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}
