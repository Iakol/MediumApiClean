using ApiGateway.Infrastructure.HttpService.UserRelationship;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var envName = builder.Environment.EnvironmentName;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
if (envName.Equals("Development") || envName.Equals("DevelopmentTestUser"))
{
    builder.Services.AddSwaggerGen();
}

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<FollowClient>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://indetitydomain",    // твій сервіс авторизації
        ValidateAudience = true,
        ValidAudiences = new[] { "userrelationshipapi", "topicapi", "ApiGateway" },       // аудиторія твого сервісу
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("MediumApiSecretKey") // -> My key
            ), // той самий секрет, що при генерації JWT
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});


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
    app.Use(async (context, next) =>
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("MediumApiSecretKeyMediumApiSecretKey");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("sub", "test-user-id-123"),
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.NameIdentifier, "test-user-id-123"),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "https://indetitydomain",
            Audience = "userrelationshipapi",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        context.Request.Headers["Authorization"] = $"Bearer {tokenString}";


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
