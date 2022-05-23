using System.Configuration;
using ApiFatecWeb.Configuration;
using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Middleware;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("localhost:5050");


IConfiguration configuration = builder.Configuration;


// Add services to the container.

#region dbContext
builder.Services.AddDbContext<BaseDbContext>(
    options =>
    {
        options.UseMySql(
            configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
        );
    });
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddClassLibraryServices(configuration);


var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
