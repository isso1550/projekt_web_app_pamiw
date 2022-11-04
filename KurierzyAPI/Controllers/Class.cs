using Microsoft.AspNetCore.Mvc;

namespace KurierzyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        [HttpGet(Name = "GetPersons")]
        public string Get()
        {
            return "Persons endpoint";
        }
    }
}