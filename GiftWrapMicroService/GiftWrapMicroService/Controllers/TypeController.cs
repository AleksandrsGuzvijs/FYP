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
    public class TypeController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var Types = new RangeFacade.GetAll();

                List<Type> allTypes = new List<Type>();
                allTypes.AddRange(await Types);

                if (allTypes.ToList().Count > 0)
                {
                    return Ok(allTypes);
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
                var Type = new TypeFacade.FindById(id);

                if (Type.ToList().Count > 0)
                {
                    return Type;
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
