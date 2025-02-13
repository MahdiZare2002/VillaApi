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
services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
