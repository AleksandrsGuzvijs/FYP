using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LibFYP.DTOs;

namespace WebApi.Controllers
{
    public class SelectionBoxController : ApiController
    {
        private Giftbox _selectionbox;

        public SelectionBoxController(Giftbox repo)
        {
            _selectionbox = repo;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetSelectionBoxes()
        {
            try
            {
                var repo = new Facades.SelectionBoxServiceFacade();
                IEnumerable<Giftbox> results;
                results = await repo.GetSelectionBoxes();
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

        [HttpGet]
        public async Task<HttpResponseMessage> GetSelectionBox(int id)
        {
            try
            {
                var repo = new Facades.SelectionBoxServiceFacade();
                Giftbox results;
                results = await repo.GetSelectionBoxById(id);
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

        [HttpPost]
        public async Task<HttpResponseMessage> PostSelectionBox(Giftbox giftbox)
        {
            try
            {
                var repo = new Facades.SelectionBoxServiceFacade();
                Giftbox results;
                results = await repo.PostSelectionBox(giftbox);
                if (results == null)
                {
                    return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
                }
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateSelectionBox(Giftbox giftbox)
        {
            try
            {
                var repo = new Facades.SelectionBoxServiceFacade();
                Giftbox results;
                results = await repo.PostSelectionBox(giftbox);
                if (results == null)
                {
                    return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
                }
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveSelectionBox(int id)
        {
            try
            {
                var repo = new Facades.SelectionBoxServiceFacade();
                Boolean results;
                results = await repo.RemoveSelectionBox(id);
                if(results != true)
                {
                    return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
                }
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}
