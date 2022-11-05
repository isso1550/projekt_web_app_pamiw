using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet("/authorizeTest")]
        [Authorize(Roles ="1")]
        public ActionResult test()
        {
            return Ok();
            return BadRequest();
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
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, target.RoleId.ToString())
                    };
                    var config_key = config.GetSection("Jwt")["Key"];
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config_key));
                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expires = DateTime.Now.AddMinutes(5);

                    var config_issuer = config.GetSection("Jwt")["Issuer"];
                    var token = new JwtSecurityToken(
                        config_issuer,
                        config_issuer,
                        claims,
                        expires: expires,
                        signingCredentials: cred);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    string tokenString = tokenHandler.WriteToken(token);

                    return Ok(tokenString);
                }

            }
        }
    }
}