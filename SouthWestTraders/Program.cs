using Core.Repositories;
using Core.Services;
using Core.Transactions;
using Data.Products.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductsDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsDBContext"), x => x.CommandTimeout(120)).EnableSensitiveDataLogging().EnableDetailedErrors());

// Register repositories
builder.Services.AddScoped(typeof(IProductsRepository), typeof(ProductsRepository));
builder.Services.AddScoped(typeof(IOrdersRepository), typeof(OrdersRepository));
builder.Services.AddScoped(typeof(IStockRepository), typeof(StockRepository));
builder.Services.AddScoped(typeof(IOrderStatesRepository), typeof(OrderStatesRepository));

builder.Services.AddScoped(typeof(IDatabaseTransaction<>), typeof(ProductsDatabaseTransaction<>));

builder.Services.AddAutoMapper(Assembly.Load("Core"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "South West Traders API",
    Description = "This API simplifies order management for different products"
  });

  //swagger documentation
  options.EnableAnnotations();
  
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlFilenameTrading = $"{Assembly.Load("Trading").GetName().Name}.xml";
  var xmlFilenameCore = $"{Assembly.Load("Core").GetName().Name}.xml";

  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameTrading));
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameCore));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  { 
    
    options.DocExpansion(DocExpansion.None);
    options.DisplayRequestDuration();
    options.DefaultModelRendering(ModelRendering.Model);

    options.EnableFilter();
    // options.DefaultModelExpandDepth(5);
    options.DefaultModelExpandDepth(-1);
  });
}

app.UseReDoc(options =>
{
  options.DocumentTitle = "South West Traders API";
  options.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
