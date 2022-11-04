using KurierzyDomain;
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
        [HttpPost("/login")]
        public string Login()
        {
            KurierzyService.KurierzyService ks = new KurierzyService.KurierzyService();
            Role r = ks.Login(1);
            return r.Name;
        }
    }
}