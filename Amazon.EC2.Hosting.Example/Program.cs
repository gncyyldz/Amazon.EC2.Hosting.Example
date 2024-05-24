using Amazon.EC2.Hosting.Example;
using Amazon.EC2.Hosting.Example.Models;
using Amazon.EC2.Hosting.Example.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

var app = builder.Build();

#region Dynamic Migration
using IServiceScope scope = app.Services.CreateScope();
ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
if (dbContext.Database.GetPendingMigrations().Any())
    await dbContext.Database.MigrateAsync();
#endregion

app.MapGet("/products", async (ApplicationDbContext dbContext) =>
{
    return await dbContext.Products.ToListAsync();
});

app.MapGet("/products/{id}", async (ApplicationDbContext dbContext, string id) =>
{
    Product? product = await dbContext.Products.FindAsync(id);
    if (product is null)
        return Results.NotFound();
    return Results.Ok(product);
});

app.MapPost("/products", (ApplicationDbContext dbContext, CreateProductVM createProductVM) =>
{
    Product product = new(Guid.NewGuid())
    {
        Name = createProductVM.Name,
        Description = createProductVM.Description,
        Price = createProductVM.Price
    };
});


app.Run();
