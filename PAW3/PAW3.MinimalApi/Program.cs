using Microsoft.EntityFrameworkCore;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;
using PAW3.Data.Repositories;
using PAW3.Models.DTOs;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



//REPOSITORIOES Y BUSINESS LOGIC 

builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
var app = builder.Build();


//obtener todos los prodcutos
app.MapGet("/ProductItems", async (IProductBusiness productBusiness) =>
    Results.Ok(await productBusiness.GetProducts(null)));

app.MapGet("/ProductItems/complete", async (ProductDbContext db) =>
    await db.Products.Where(t => t.InventoryId != null).ToListAsync());

//obtener por id
app.MapGet("/ProductItems/{id}", async (int id, ProductDbContext db) =>
    await db.Products.FindAsync(id)
        is Product product
            ? Results.Ok(product)
            : Results.NotFound());

//crear un producto
// no pude implementar el DTO, me dio errores en metodos y si los cambiaba todo el codigo se caia
app.MapPost("/ProductItems", async (Product products, ProductDbContext db) =>
{

    db.Products.Add(products);
    await db.SaveChangesAsync();

    return Results.Created($"/ProductItems/{products.ProductId}", products);

    /*
    await productBusiness.SaveProductAsync(products);
   

    return Results.Created($"/ProductItems/{products.ProductId}", products);
    */
});

//editar un producto
app.MapPut("/ProductItems/{id}", async (int id, ProductDTO inputProduct, ProductDbContext db) =>
{
    var producto = await db.Products.FindAsync(id);

    if (producto is null) return Results.NotFound();

    producto.ProductName = inputProduct.ProductName;
    producto.Description = inputProduct.Description;
    producto.Rating = inputProduct.Rating;


    await db.SaveChangesAsync();

    return Results.NoContent();
});

//delete
app.MapDelete("/ProductItems/{id}", async (int id, IProductBusiness productBusiness) =>
{
    var product = await productBusiness.DeleteProductAsync(id);

    return Results.NotFound();
});


app.Run();