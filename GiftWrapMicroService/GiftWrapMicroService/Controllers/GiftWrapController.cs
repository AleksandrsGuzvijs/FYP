using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LibFYP.DTOs;
using System.Net;
using GiftWrapMicroService.MSFacades;

namespace GiftWrapMicroService.Controllers
{
    public class GiftWrapController : ApiController
    {

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var giftWraps = new GiftWrapFacade.GetAll();

                List<Wrapping> allWrappings = new List<Wrapping>();
                allWrappings.AddRange(await giftWraps);

                if (allWrappings.ToList().Count > 0)
                {
                    return Ok(allWrappings);
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
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindById(int id)
        {
            try
            {
                var giftWrap = new GiftWrapFacade.FindById(id);

                if (giftWrap.ToList().Count > 0)
                {
                    return giftWrap;
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
