using ApiPokedex.Middleware;
using ApiPokedex.Options;
using DapperConnection.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokedexDataAccess.DataAccess.ADO;
using PokedexDataAccess.DataAccess.Dapper;
using PokedexDataAccess.DataAccess.EF;
using PokedexDataAccess.Interfaces;
using PokedexEF.DataAccess;
using PokedexServices.Interfaces;
using PokedexServices.Services;
using SqlServerADOConnection.SQLConnection;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = ApiVersion.Default;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
                x =>
                {
                    var secutiry = new Dictionary<string, IEnumerable<string>>()
                    {
                        { "Bearer", new string[0]}
                    };
                    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Description = "Token",
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                    });
                    x.AddSecurityRequirement(new OpenApiSecurityRequirement());
                }
                );

// Add ApiExplorer to discover versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
    
builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddScoped<LoggerMiddleware>();

builder.Services.AddTransient<IPokedexService, PokedexService>();
builder.Services.AddTransient<IPokedexVersionService, PokedexVersionService>();

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var typeOfConnection = builder.Configuration.GetConnectionString("TypeOfDataBaseConnection")!;
if (typeOfConnection == "ADO")
{
    builder.Services.AddTransient<ISqlServerADOQuery>(s => new SqlServerADOQuery(builder.Configuration.GetConnectionString("Pokedex")));
    builder.Services.AddTransient<IPokedexDataAccessService, PokedexADOSqlServer>();
}
else if (typeOfConnection == "EF")
{
    builder.Services.AddTransient<IPokedexDataAccessService, PokedexEntityFramework>();

    builder.Services.AddDbContext<DbPokedexContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Pokedex")!);
    });
}
else if (typeOfConnection == "DAPPER")
{
    builder.Services.AddTransient<IPokedexDataAccessService, PokedexDapper>();
    builder.Services.AddTransient<IPokedexVersionDataAccessService, PokedexVersionDapper>();
    builder.Services.AddTransient<ISqlDapperDataAccess>(s => new SqlDapperDataAccess(builder.Configuration.GetConnectionString("Pokedex"))) ;
}
    var app = builder.Build();

app.UseLoggerMiddleware();
app.UseExceptionHandlerMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
