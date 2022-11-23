using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KurierzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            List<Person> l = ks.getAll();
            var json = JsonSerializer.Serialize(l);
            return json;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Person p = ks.getOne(id);
            var json = JsonSerializer.Serialize(p);
            return json;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ModifyPersonDTO p)
        {
            string message = ks.ModifyPerson(id, p);
            if (message == "success")
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(message);
                }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
