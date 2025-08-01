using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UnitOfWork;
using TopicDomain.Application.UseCases;
using TopicDomain.Application.UseCases.CreateTopic;
using TopicDomain.Infrastructure.Database.DBContext;
using TopicDomain.Infrastructure.Database.Repositories;
using TopicDomain.Infrastructure.Database.UnitsOfWork;

var builder = WebApplication.CreateBuilder(args);
var envName = builder.Environment.EnvironmentName;

if (envName.Equals("Development") || envName.Equals("DevelopmentTestUser"))
{
    builder.Services.AddSwaggerGen();
}

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICreateTopicCase,CreateTopicCase>();
builder.Services.AddScoped<FindTopicsByNameCase>();
builder.Services.AddScoped<FindTopicsDTOByNameCase>();
builder.Services.AddScoped<GetLast100TopicsCase>();
builder.Services.AddScoped<GetTopicCase>();
builder.Services.AddScoped<GetTopicDTOByIdCase>();
builder.Services.AddScoped<GetTopicDTOByIdsCase>();
builder.Services.AddScoped<GetTopicDTOCase>();
builder.Services.AddScoped<GetTopicsByIdsCase>();

builder.Services.AddScoped<ITopicRepository,TopicRepository>();

builder.Services.AddScoped<ICreateTopiсUnitOfWork, CreateTopiсUnitOfWork>();


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
            Encoding.UTF8.GetBytes("MediumApiSecretKeyMediumApiSecretKey") // -> My key
            ), // той самий секрет, що при генерації JWT
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});









// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DevelopmentTestUser"))
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(); // Swagger UI за адресою /swagger
}



app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
