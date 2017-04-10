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
    public class OrdersController : ApiController
    {
        private readonly string _cuttersUrl = "http://undercutters.azurewebsites.net/";
        private readonly string _BazaarUrl = "http://bazzasbazaar.azurewebsites.net/";
        private readonly string _DodgyUrl = "http://dodgydealers.azurewebsites.net/";

        [HttpGet]
        public async Task<HttpResponseMessage> GetOrders()
        {
            try
            {
                var ordersCutters = new Facades.OrderFacade.GetOrders(_cuttersUrl);
                var ordersBazaar = new Facades.OrderFacade.GetOrders(_BazaarUrl);
                var ordersDodgy = new Facades.OrderFacade.GetOrders(_DodgyUrl);

                List<Order> allOrders = new List<Order>();
                allOrders.AddRange(await ordersCutters);
                allOrders.AddRange(await ordersBazaar);
                allOrders.AddRange(await ordersDodgy);

                if (allOrders.ToList().Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, allOrders);
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
