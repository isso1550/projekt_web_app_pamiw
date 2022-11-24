using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KurierzyService;

namespace KurierzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
        private readonly ILogger<LoginsController> _logger;
        public LoginsController(ILogger<LoginsController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public ActionResult LoginPerson([FromBody] LoginPersonDTO lp)
        {
            _logger.Log(LogLevel.Information, ("POST /login " + DateTime.Now));

            Person target = ks.LoginPerson(lp);
            if (target is null)
            {
                //nie ma takiej osoby w bazie
                _logger.Log(LogLevel.Warning, ("Login error " + DateTime.Now));
                return BadRequest("Email and/or password invalid");
            }
            else
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
