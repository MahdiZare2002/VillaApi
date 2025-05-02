using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Context;
using OnlineShop.Mappings;
using OnlineShop.Services.Customer;
using OnlineShop.Services.Detail;
using OnlineShop.Services.Villa;
using OnlineShop.Utility;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var local = builder.Configuration.GetConnectionString("Local");

#region AutoMapper
services.AddAutoMapper(typeof(ModelMapper));
#endregion

services.AddControllers();
services.AddEndpointsApiExplorer();

#region Versioning
services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Adds API versions to response headers
    options.AssumeDefaultVersionWhenUnspecified = true; // Default if no version specified
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Read version from URL
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // v1, v2, etc.
    options.SubstituteApiVersionInUrl = true;
});
#endregion

#region Swagger
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVillaDocument>();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
#endregion

#region Jwt
var JwtSettingSection = builder.Configuration.GetSection("JwtSettings");
services.Configure<JWTSettings>(JwtSettingSection);

var jwtsetting = JwtSettingSection.Get<JWTSettings>();
var key = Encoding.ASCII.GetBytes(jwtsetting.Secret);

services.AddAuthentication(x =>
{
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtsetting.Issuer,
        ValidateIssuer = true,
        ValidAudience = jwtsetting.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
    };
});

#endregion

services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(local);
});

services.AddTransient<IVillaService, VillaService>();
services.AddTransient<IDetailService, DetailService>();
services.AddTransient<ICustomerService, CustomerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
