using System.Collections.Generic;
using Apiphobia.Filters;
using Apiphobia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Apiphobia.Controllers
{
    [Route("api/[controller]")]
    [ExamplesFilter]
    public class ExamplesController : Controller
    {
        private readonly ExamplesRepository _examplesRepository;

        public ExamplesController(ExamplesRepository examplesRepository)
        {
            _examplesRepository = examplesRepository;
        }

        // GET api/examples
        [HttpGet]
        public IEnumerable<Example> Get()
        {
            return _examplesRepository.FindAll();
        }

        // GET api/examples/5
        [HttpGet("{id}", Name = "GetExample")]
        public ActionResult Get(string id)
        {
            var example = _examplesRepository.FindOrDefault(id);
            if (example == default(Example))
            {
                return NotFound();
            }
            return new ObjectResult(example);
        }

        // POST api/examples
        [HttpPost]
        public ActionResult Post([FromBody]Example example)
        {
            if (example == null)
            {
                return BadRequest();
            }
            _examplesRepository.Add(example);
            return CreatedAtRoute("GetExample", new {id = example.Id}, example);
        }

        // PUT api/examples/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/examples/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _examplesRepository.Remove(id);
        }
    }
}
