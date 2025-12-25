using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataWebAPI.Context;
using ODataWebAPI.Models;

namespace ODataWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(ApplicationDbContext applicationDbContext) : ControllerBase
    {
        [HttpGet("Users")]
        public IActionResult GetUsers(ODataQueryOptions<User> options)
        {
            var users = applicationDbContext.Users.AsQueryable();
            var res = options.ApplyTo(users);
            return Ok(res);
        }
    }
}
