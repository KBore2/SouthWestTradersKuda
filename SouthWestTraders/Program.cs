using Core.Authorization;
using Core.Cache;
using Core.Repositories;
using Core.Services;
using Core.Transactions;
using Data.Products.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
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

builder.Services.AddDistributedMemoryCache();
builder.Services.AddTransient(typeof(IDistributedCacheRepository), typeof(DistributedCacheRepository));

builder.Services.AddAutoMapper(Assembly.Load("Core"));

builder.Services.AddEndpointsApiExplorer();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
            // base-address of identityserver
            options.Authority = "https://localhost:7173";
            // name of the API resource
            options.Audience = "https://localhost:7173/resources";
          });

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

  // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
  options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
  {
    Type = SecuritySchemeType.OAuth2,
    Flows = new OpenApiOAuthFlows
    {
      Implicit = new OpenApiOAuthFlow
      {
        AuthorizationUrl = new Uri("https://localhost:7173/connect/authorize", UriKind.Absolute),
        Scopes = new Dictionary<string, string>
                {
                    { "read", "Access identity information" },
                    { "roles", "Access API roles" }
                }
      }
    }
  });

  options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "read", "roles" }
         }
    });


  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlFilenameTrading = $"{Assembly.Load("Trading").GetName().Name}.xml";
  var xmlFilenameCore = $"{Assembly.Load("Core").GetName().Name}.xml";

  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameTrading));
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameCore));
});

builder.Services.AddAuthorization(options =>
     options.AddPolicy(Policy.AdminAuthorizePolicy,
     requirement => requirement
           .AddRequirements(
             new SouthWestTradersAdminAuthorizeRequirement("Admin"))
           .RequireClaim(JwtClaimNames.Sub)
           ));

builder.Services.AddScoped<IAuthorizationHandler, SouthWestTradersAdminAuthorizeHandler>();

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

    options.OAuthClientId("southwest.traders");
    options.OAuthClientSecret("secret");
    options.OAuthRealm("South West Traders");
    options.OAuthAppName("South West Traders");
  });
}

app.UseReDoc(options =>
{
  options.DocumentTitle = "South West Traders API";
  options.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
