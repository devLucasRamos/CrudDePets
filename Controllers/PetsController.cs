using Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class PetsController : ControllerBase
    {
        [HttpGet]
        [Route(template: "Pets")]
        public IActionResult Get([FromServices]AppDbContext context)
        {
            var pets = context.Pets.ToList();
            return Ok(pets);
        }





    }
}
