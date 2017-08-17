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
    public class OrdersController : ApiController
    {
        private Order _orderRepo;

        public OrdersController(Order _repo)
        {
            _orderRepo = _repo;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostOrder(Order order)
        {
            try
            {
                var repo = new Facades.PostOrderFacade();
                Order result;
                result = await repo.PostOrder(order);
                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }
                return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}