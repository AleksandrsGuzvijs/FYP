using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SelectionboxesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
    }
}
