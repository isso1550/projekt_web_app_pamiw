using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KurierzyAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
        private readonly ILogger<PersonsController> _logger;
        public PersonsController(ILogger<PersonsController> logger)
        {
            _logger = logger;
        }
        [HttpPost("filter/[controller]")]
        public ActionResult Filter([FromBody] PersonsFilterDTO f) 
        {
            _logger.Log(LogLevel.Information, ("GET persons all " + DateTime.Now));
            List<Person> l = ks.getAll();
            List<Person> filtered = new List<Person>(l);
            foreach(Person p in l)
            {
                if (f.Email != "" && !(p.Email.Contains(f.Email)))
                {
                    filtered.Remove(p);
                } else if (f.Name != "" && !(p.Name.Contains(f.Name)))
                {
                    filtered.Remove(p);
                }
                else if (f.Surname != "" && !(p.Surname.Contains(f.Surname)))
                {
                    filtered.Remove(p);
                }
                else if (f.City != "" && !(p.City.Contains(f.City)))
                {
                    Console.WriteLine(p.City);
                    filtered.Remove(p);
                }
                else if (!((p.Birthday > f.Birthday)))
                {
                    filtered.Remove(p);
                }
                //RoleID = 0 => brak filtra
                else if (f.RoleId >= 0 && !(p.RoleId == f.RoleId))
                {
                    filtered.Remove(p);
                }
            }
            //init
            var json = JsonSerializer.Serialize("");
            if (f.Count > 0)
            {
                Console.WriteLine("Count " + f.Count.ToString());
                if (f.Page < 0)
                {
                    f.Page = 1;
                }
                int first = f.Count * (f.Page-1);
                if ((first + f.Count) > filtered.Count){
                    f.Count = filtered.Count;
                }
                if (first > filtered.Count)
                {
                    first = 0;
                    f.Count = 0;
                }
                json = JsonSerializer.Serialize(new ArraySegment<Person>(filtered.ToArray(), first, f.Count));
            } else
            {
                json = JsonSerializer.Serialize(filtered);
            }
            
            return Ok(json);
        }

        // GET: api/<ValuesController>
        [HttpGet("[controller]")]
        public ActionResult Get()
        {
            _logger.Log(LogLevel.Information, ("GET persons all " + DateTime.Now));
            List<Person> l = ks.getAll();

            var json = JsonSerializer.Serialize(l);
            return Ok(json);
        }

        // GET api/<ValuesController>/5
        [HttpGet("[controller]/{id}")]
        public string Get(int id)
        {
            _logger.Log(LogLevel.Information, ("GET persons " + id.ToString() + " " + DateTime.Now));
            Person p = ks.getOne(id);
            var json = JsonSerializer.Serialize(p);
            return json;
        }

        // POST api/<ValuesController>
        [HttpPost("[controller]")]
       // [Authorize(Roles = "1")] //menadzer
       //autoryzacja wylaczon na potrzeby inicjalizacji projektu
        public ActionResult Post([FromBody] RegisterPersonDTO p)
        {
            var passwordHasher = new PasswordHasher<Person>();
            Person temp = new Person();
            var hashed = passwordHasher.HashPassword(temp, p.Password);
            p.Password = hashed;
            string message = ks.RegisterPerson(p);
            if (message == "success")
            {
                return Ok();
                _logger.Log(LogLevel.Information, ("POST persons " + DateTime.Now));
            }
            else
            {
                _logger.Log(LogLevel.Warning, ("POST persons ERROR " + DateTime.Now + " " + message));
                return BadRequest("Registration failed");
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("[controller]/{id}")]
        [Authorize(Roles = "1")] //menadzer
        public ActionResult Put(int id, [FromBody] ModifyPersonDTO p)
        {
            string message = ks.ModifyPerson(id, p);
            if (message == "success")
                {
                    _logger.Log(LogLevel.Information, ("PUT persons "+ id.ToString() + " "+ DateTime.Now));
                    return Ok();
                }
                else
                {
                    _logger.Log(LogLevel.Warning, ("PUT persons error "+id.ToString() + DateTime.Now + " " + message));
                    return BadRequest(message);
                }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("[controller]/{id}")]
        [Authorize(Roles = "1")] //menadzer
        public ActionResult Delete(int id)
        {
            string message = ks.deletePerson(id);
            if (message == "success")
            {
                _logger.Log(LogLevel.Information, ("DELETE persons " + id.ToString() + " " + DateTime.Now));
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Information, ("DELETE persons error " +id.ToString() +" "+ DateTime.Now + " " + message));
                return BadRequest(message);
            }
        }
    }
}
