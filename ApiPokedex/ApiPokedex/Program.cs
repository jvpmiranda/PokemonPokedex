using ApiPokedex.Interfaces;
using ApiPokedex.Middleware;
using ApiPokedex.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlServerDataBaseConnection.Interface;
using SqlServerDataBaseConnection.SQLConnection;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    //x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };
                });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
                //x =>
                //{
                //    var secutiry = new Dictionary<string, IEnumerable<string>>()
                //    {
                //        { "Bearer", new string[0]}
                //    };
                //    x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                //    {
                //        Description = "TOKEN da API DA POKEDEX",
                //        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                //        In = ParameterLocation.Header,
                //        Name = "Authorization",
                //    });
                //    x.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement());
                //}
                );

builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddScoped<LoggerMiddleware>();

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

//builder.Services.AddScoped<IDataBaseConnection>((serv) => new DataBaseConnection(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<ISqlConnection>((serv) => new SqlServerConnection(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddTransient<IPokedexService, PokedexSqlServer>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
