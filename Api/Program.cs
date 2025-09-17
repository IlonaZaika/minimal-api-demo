using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Contracts;
using Api.Services;
using Api.Domain;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();         // ProblemDetails
app.UseSwagger();
app.UseSwaggerUI();

// For Demo only: Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Name = "Laptop", Stock = 10, Price = 1200 },
            new Product { Name = "Phone", Stock = 25, Price = 700 },
            new Product { Name = "Tablet", Stock = 15, Price = 500 }
        );
        db.SaveChanges();
    }
}

app.MapGet("/", () => "Hello, World!");

app.MapGet("/api/products", async (string? q, int page, int pageSize, IProductService productService) =>
{
    page = page < 1 ? 1 : page;
    pageSize = Math.Clamp(pageSize, 1, 100);
    var (items, total) = await productService.GetAsync(q, page, pageSize);
    return Results.Ok(new { total, page, pageSize, items });
});

app.MapGet("/api/products/{id:int}", async (int id, IProductService productService) => {
    return await productService.GetByIdAsync(id) is { } p ? Results.Ok(p) : Results.NotFound();
});

app.MapPost("/api/products", async (Product product, IProductService productService) => {
    if (string.IsNullOrWhiteSpace(product.Name))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> { { "Name", new[] { "Required" } } });

    }
    var created = await productService.CreateAsync(product);
    return Results.Created($"/api/products/{created.Id}", created);
});

app.MapPut("/api/products/{id:int}", async (int id, Product product, IProductService productService) => {
    try
    {
        await productService.UpdateAsync(id, product);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
});

app.MapDelete("/api/products/{id:int}", async (int id, IProductService svc) => {
    try
    {
        await svc.DeleteAsync(id);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
});

app.Run();