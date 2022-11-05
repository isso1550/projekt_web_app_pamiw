using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace KurierzyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        [HttpGet("/all")]
        public string GetWorkersInfo()
        {
            KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
            List<Person> l = ks.getAll();
            var json = JsonSerializer.Serialize(l);
            return json;
        }
        [HttpPost("/register")]
        public ActionResult RegisterPerson([FromBody] RegisterPersonDTO p)
        {
            KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
            var passwordHasher = new PasswordHasher<Person>();
            Person temp = new Person();
            var hashed = passwordHasher.HashPassword(temp, p.Password);
            p.Password = hashed;
            string message = ks.RegisterPerson(p);
            if(message == "success")
            {
                return Ok();
            } else
            {
                return BadRequest(message);
            }
        }

        [HttpPost("/login")]
        public ActionResult LoginPerson([FromBody] LoginPersonDTO lp )
        {
            KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
            Person target = ks.LoginPerson(lp);
            if (target is null)
            {
                //nie ma takiej osoby w bazie
                return BadRequest("Email and/or password invalid");
            } else
            { 
                var passwordHasher = new PasswordHasher<Person>();
                Console.WriteLine(target);
                Console.WriteLine(target.passwordHash);
                var result = passwordHasher.VerifyHashedPassword(target, target.passwordHash, lp.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return BadRequest("Email and/or password invalid");
                }
                else
                {
                        return Ok();
                }

            }
        }
    }
}