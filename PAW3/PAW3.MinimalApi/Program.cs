using Microsoft.EntityFrameworkCore;
using PAW3.Data.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

//obtener todos los prodcutos
app.MapGet("/ProductItems", async (ProductDbContext db) =>
    await db.Products.ToListAsync());

app.MapGet("/ProductItems/complete", async (ProductDbContext db) =>
    await db.Products.Where(t => t.InventoryId != null).ToListAsync());

//obtener por id
app.MapGet("/ProductItems/{id}", async (int id, ProductDbContext db) =>
    await db.Products.FindAsync(id)
        is Product product
            ? Results.Ok(product)
            : Results.NotFound());

//crear un producto
app.MapPost("/ProductItems", async (Product product, ProductDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/ProductItems/{product.ProductId}", product);
});

//editar un producto
app.MapPut("/ProductItems/{id}", async (int id, Product inputProduct, ProductDbContext db) =>
{
    var producto = await db.Products.FindAsync(id);

    if (producto is null) return Results.NotFound();

    producto.ProductName = inputProduct.ProductName;
    producto.Description = inputProduct.Description;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

//delete
app.MapDelete("/ProductItems/{id}", async (int id, ProductDbContext db) =>
{
    if (await db.Products.FindAsync(id) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.Run();