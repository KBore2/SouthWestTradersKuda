using Core.Repositories;
using Core.Services;
using Core.Transactions;
using Data.Products.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(Assembly.Load("Core"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "SouthWestTraders",
    Description = "Traders API"
  });

  options.EnableAnnotations();

  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlFilenameTrading = $"{Assembly.Load("Trading").GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameTrading));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//static string XmlCommentsFilePath
//{
//  get
//  {
//    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
//    return basePath;
//  }
//}
