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
    public class RangeController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var Ranges = new RangeFacade.GetAll();

                List<Range> allRanges = new List<Range>();
                allRanges.AddRange(await Ranges);

                if (allRanges.ToList().Count > 0)
                {
                    return Ok(allRanges);
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
                var Range = new RangeFacade.FindById(id);

                if (Range.ToList().Count > 0)
                {
                    return Range;
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
