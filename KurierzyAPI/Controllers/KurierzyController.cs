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

    public class KurierzyController : ControllerBase
    {

        private KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
        private readonly ILogger<KurierzyController> _logger;

        public KurierzyController(ILogger<KurierzyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/api2/persons")]
        public string GetWorkersInfo()
        {
            _logger.Log(LogLevel.Information, ("GET /all " + DateTime.Now));

            List<Person> l = ks.getAll();
            var json = JsonSerializer.Serialize(l);
            return json;
        }
        [HttpPost("/person/{id}/modify")]
        [Authorize(Roles = "1,Manager,Kurier")]
        public ActionResult ModifyPerson([FromRoute] int id,[FromBody] ModifyPersonDTO p)
        {
            _logger.Log(LogLevel.Information, ("POST /person/modify " + DateTime.Now));

            string message = ks.ModifyPerson(id, p);
            if(message == "success")
            {
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Warning, ("Modify error " + DateTime.Now));
                return BadRequest(message);
            }

        }
        [HttpPost("/register")]
        public ActionResult RegisterPerson([FromBody] RegisterPersonDTO p)
        {
            _logger.Log(LogLevel.Information, ("POST /register " + DateTime.Now));

            
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
                _logger.Log(LogLevel.Warning, ("Register error " + DateTime.Now));
                return BadRequest(message);
            }
        }
        [HttpGet("/authorizeTest")]
        [Authorize(Roles ="1,Deliverer,Kurier")]
        public ActionResult test()
        {
            return Ok();
            return BadRequest();
        }
        [HttpPost("/login")]
        public ActionResult LoginPerson([FromBody] LoginPersonDTO lp )
        {
            _logger.Log(LogLevel.Information, ("POST /login " + DateTime.Now));

            Person target = ks.LoginPerson(lp);
            if (target is null)
            {
                //nie ma takiej osoby w bazie
                _logger.Log(LogLevel.Warning, ("Login error " + DateTime.Now));
                return BadRequest("Email and/or password invalid");
            } else
            { 
                var passwordHasher = new PasswordHasher<Person>();
                var result = passwordHasher.VerifyHashedPassword(target, target.passwordHash, lp.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    _logger.Log(LogLevel.Warning, ("Login error " + DateTime.Now));
                    return BadRequest("Email and/or password invalid");
                }
                else
                {
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var claims = new List<Claim>()
                    //TODO zamienic roleid na role name
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