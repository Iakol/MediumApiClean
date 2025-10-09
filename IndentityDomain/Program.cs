using IndentityDomain.Application.Interfaces;
using IndentityDomain.Infrastructure.Repositories;
using IndentityDomain.Presentation.RabbitConsumers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "IdentityService:";
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "495881695226-sd65dbfd36drtn4hperac8nrr0qnsiu2.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
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

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<SMTPCred>(builder.Configuration.GetSection("smtp"));
builder.Services.AddHostedService<CreateUserRabbitConsumer>();
builder.Services.AddScoped<IEmailSendler, SendEmailRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
