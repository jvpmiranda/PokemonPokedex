using ApiPokedex.Interfaces;
using ApiPokedex.Middleware;
using ApiPokedex.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using SqlServerDataBaseConnection.Interface;
using SqlServerDataBaseConnection.SQLConnection;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddScoped<LoggerMiddleware>();

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

//builder.Services.AddScoped<IDataBaseConnection>((serv) => new DataBaseConnection(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<ISqlConnection>((serv) => new SqlServerConnection(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddTransient<IPokedexService, PokedexService>();


var app = builder.Build();

app.UseLoggerMiddleware();
app.UseExceptionHandlerMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
