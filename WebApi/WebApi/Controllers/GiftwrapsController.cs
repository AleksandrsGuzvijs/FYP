using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using LibFYP.DTOs;
using System.Net;


namespace WebApi.Controllers
{
    public class GiftwrapsController : ApiController
    {

        private Wrapping _giftwrapRepo;

        public GiftwrapsController(Wrapping repo)
        {
            _giftwrapRepo = repo;
        }

        // GET: api/values
        [HttpGet]
        public async Task<HttpResponseMessage> GetGiftwraps(string rangeName = null, string typeName = null, int? typeId = null, int? rangeId = null, double? minPrice = 0, double? maxPrice = double.MaxValue)
        {
            try
            {
                var repo = new WrappingRepo(new HttpClient());
                IQueryable<Wrapping> results;

                if ((typeName != null) || (rangeName != null) || (typeId != null) || (rangeId != null) || (minPrice != 0) || (maxPrice != double.MaxValue))
                {
                    // If params passed
                    var query = new Dictionary<string, dynamic>();
                    if (typeName != null)
                    {
                        query.Add("typeName", typeName);
                    }

                    if (rangeName != null)
                    {
                        query.Add("rangeName", rangeName);
                    }

                    if (typeId != null)
                    {
                        query.Add("typeId", typeId);
                    }

                    if (rangeId != null)
                    {
                        query.Add("rangeId", rangeId);
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
                    if (results.ToList().Count > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, results);
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
                }
                results = await repo.GetAll();
                if (results.ToList().Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, results);
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        // GET api/values/5
        [HttpGet]
        public async Task<HttpResponseMessage> GetGiftwrap(int id)
        {
            try
            {
                var repo = new WrappingRepo(new HttpClient());
                Wrapping results;
                results = await repo.GetById(id);
                if (results == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Nothing found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}
