using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Context;
using OnlineShop.Mappings;
using OnlineShop.Services.Detail;
using OnlineShop.Services.Villa;

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
}).AddMvc();
#endregion

#region Swagger
services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("VillaOpenApi",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Villa Api",
            Version = "1",
            Description = "api for villa rent and buy platform",
        });
    var path = Path.Combine(AppContext.BaseDirectory, "SwaggerComment.xml");
    option.IncludeXmlComments(path);
});
#endregion

services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(local);
});

services.AddTransient<IVillaService, VillaService>();
services.AddTransient<IDetailService, DetailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/VillaOpenApi/swagger.json", "Villa API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
