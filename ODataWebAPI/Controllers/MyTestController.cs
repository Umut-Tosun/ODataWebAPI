using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataWebAPI.Context;
using ODataWebAPI.Models;

namespace ODataWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]
    public class MyTestController(ApplicationDbContext applicationDbContext) : ControllerBase
    {
        [HttpGet]
        public IQueryable<Category> Categories()

        {
            var categories = applicationDbContext.Categories.AsQueryable();
            return categories;


        }
    }
}
