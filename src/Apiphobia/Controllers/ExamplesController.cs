using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Apiphobia.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Apiphobia.Controllers
{
    [Route("api/[controller]")]
    [ExamplesFilter]
    public class ExamplesController : Controller
    {
        // GET api/examples
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/examples/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            if (!id.All(char.IsDigit))
            {
                throw new HttpRequestException("ID must be a number.");
            }

            return $"value{id}";
        }

        // POST api/examples
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/examples/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/examples/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
