using Microsoft.EntityFrameworkCore;
using OnlineShop.Context;
using OnlineShop.Mappings;
using OnlineShop.Services.Detail;
using OnlineShop.Services.Villa;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var local = builder.Configuration.GetConnectionString("Local");

#region automapper
services.AddAutoMapper(typeof(ModelMapper));
#endregion

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("VillaOpenApi",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Villa Api",
            Version = "1",
            Description = "api for villa rent and buy platform",
        });
});

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
