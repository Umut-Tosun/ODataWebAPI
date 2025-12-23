using Bogus;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using ODataWebAPI.Context;
using ODataWebAPI.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ODataDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    options.UseSqlServer(connectionString);  
});
builder.Services.AddControllers().AddOData(opt => 
opt.EnableQueryFeatures()
);
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("seed-data/categoties", async (ApplicationDbContext dbContext) =>
{
    Faker faker = new();

    var categoryNames = faker.Commerce.Categories(100);
    List<Category> categories = categoryNames.Select(s => new Category
    {
        Name = s,
    }).ToList();

    dbContext.AddRange(categories);

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
}).Produces(204).WithTags("SeedCategories");
app.MapControllers();
app.Run();
