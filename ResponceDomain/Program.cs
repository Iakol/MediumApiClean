using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ResponceDomain.Application.AutoMapper;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.Services;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Application.UseCases;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Infrastructure.DataBase.Repositories;
using ResponceDomain.Infrastructure.DataBase.UnitsOfWork;
using ResponceDomain.Presentation.RabbitMq;
using ResponceDomain.Presentation.UseCases;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var envName = builder.Environment.EnvironmentName;

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapperProfile>();
});



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://indetitydomain",    // твій сервіс авторизації
        ValidateAudience = true,
        ValidAudiences = new[] { "userrelationshipapi", "topicapi", "ApiGateway" },       // аудиторія твого сервісу
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("MediumApiSecretKeyMediumApiSecretKey") // -> My key
            ), // той самий секрет, що при генерації JWT
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});

builder.Services.AddOpenApi();

if (envName.Equals("Development") || envName.Equals("DevelopmentTestUser"))
{
    builder.Services.AddSwaggerGen();
}


//Db context
builder.Services.AddDbContext<AppDBContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//RabitCred
builder.Services.Configure<RabbitHost>(builder.Configuration.GetSection("RabbitCred"));

//Repository
builder.Services.AddScoped<IClapsToResponceOfUsersIterfaces, ClapsToResponceOfUsersIterfaces>();
builder.Services.AddScoped<IResponceRepository, ResponceRepository>();

//UnitOfWork
builder.Services.AddScoped<IUpdateResponceUnit, UpdateResponceUnit>();
builder.Services.AddScoped<IUpdateClapsToResponceUnit, UpdateClapsToResponceUnit>();
builder.Services.AddScoped<IDeleteResponceUnit, DeleteResponceUnit>();
builder.Services.AddScoped<IDeleteResponcePerItemUnit, DeleteResponcePerItemUnit>();
builder.Services.AddScoped<IAddResponceUnit, AddResponceUnit>();
builder.Services.AddScoped<IAddClapsToResponceUnit, AddClapsToResponceUnit>();


//Service 

builder.Services.AddScoped<CreateResponceTreeByResponceTreeBuilder>();

//UserCases
builder.Services.AddScoped<IUpdateResponceCase, UpdateResponceCase>();
builder.Services.AddScoped<IGetResponcesForItemCase, GetResponcesForItemCase>();
builder.Services.AddScoped<IDeleteResponcePerItemCase, DeleteResponcePerItemCase>();
builder.Services.AddScoped<IDeleteResponceCase,DeleteResponceCase>();
builder.Services.AddScoped<IAddResponceCase, AddResponceCase>();
builder.Services.AddScoped<IAddClapsToResponceCase, AddClapsToResponceCase>();

// Background Service

builder.Services.AddHostedService<DeleteAllResponcesPerItemConsumer>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsEnvironment("DevelopmentTestUser"))
{
    app.Use(async (context, next) =>
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim("sub", "test-user-id-123"),
            new Claim(ClaimTypes.NameIdentifier, "test-user-id-123"),
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);
        context.User = principal;

        await next();
    });
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DevelopmentTestUser"))
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(); // Swagger UI за адресою /swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
