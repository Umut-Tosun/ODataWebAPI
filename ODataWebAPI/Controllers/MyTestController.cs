using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataWebAPI.Context;
using ODataWebAPI.Dtos;
using ODataWebAPI.Models;

namespace ODataWebAPI.Controllers
{
    [Route("odata")]
    [ApiController]
    [EnableQuery]
    public class MyTestController(ApplicationDbContext applicationDbContext) : ODataController
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder1 = new();
            builder1.EnableLowerCamelCase();
            builder1.EntitySet<Category>("Categories");
            builder1.EntitySet<Product>("Products");
            builder1.EntitySet<ProductDto>("ProductsDto");
            builder1.EntitySet<User>("Users");
            builder1.EntitySet<UserDto>("UsersDto");
            return builder1.GetEdmModel();
        }

        #region categories
        [HttpGet("categories")]
        //        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Top)] sadece top izinli
        //      [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All & ~AllowedQueryOptions.Top)] // top hariç tüm query opsiyonları izinli
        public IQueryable<Category> Categories()

        {
            var categories = applicationDbContext.Categories.AsQueryable();
            return categories;


        }
        #endregion
        #region products
        [HttpGet("products")]
        public IQueryable<Product> Products()
        {
            var products = applicationDbContext.Products.AsQueryable();
            return products;
        }
        #endregion
        #region ProductsDto
        [HttpGet("products-dto")]
        public IQueryable<ProductDto> ProductsDto()
        {
            var productsDto = applicationDbContext.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category != null ? p.Category.Name : string.Empty
                });
            return productsDto;


        }
        #endregion
        #region users   
        [HttpGet("users")]
        public IQueryable<User> Users()
        {
            var users = applicationDbContext.Users.AsQueryable();
            return users;
        }
        #endregion
        #region UsersDto
        [HttpGet("users-dto")]
        public IQueryable<UserDto> UsersDto()
        {
            var usersDto = applicationDbContext.Users
                .Select(s => new UserDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FullName,
                    Address = s.Address,
                    UserType = s.UserType,
                    UsertTypeName = s.UserType.Name,
                    UserTypeValue = s.UserType.Value,

                }).AsQueryable();

            return usersDto;
           
        }
        #endregion


    }
}
