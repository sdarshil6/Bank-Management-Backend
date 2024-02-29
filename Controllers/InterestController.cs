using BankManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController : Controller
    {
        private readonly IServiceFactory services;
        public InterestController(IServiceFactory i)
        {
            services = i;
        }

        [HttpGet("get_interest")]
        public IActionResult GetInterest(string type)
        {
            var something = services.GetInstance(type);
            if(something != null)
            {
                return Ok("Interest rate of a " + type + " account is " + something.GetInterest());
            }
            return BadRequest("Invalid Account Type");
        }
        
    }
}
