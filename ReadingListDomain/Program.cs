using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Application.UseCases;
using ReadingListDomain.Infrastructure.Database.DBContext;
using ReadingListDomain.Infrastructure.Database.Repositories;
using ReadingListDomain.Infrastructure.Database.UnitOfWorks;
using ReadingListDomain.Infrastructure.Database.UnitsOfWork;
using ReadingListDomain.Presentation.RabbitConcumers;
using ReadingListDomain.Presentation.UserCases;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var envName = builder.Environment.EnvironmentName;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDBContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

if (envName.Equals("Development") || envName.Equals("DevelopmentTestUser"))
{
    builder.Services.AddSwaggerGen();
}

//Repositories
builder.Services.AddScoped<IReadingListRepository, ReadingListRepository>();
builder.Services.AddScoped<IStoryInReadingListRepository, StoryInReadingListRepository>();


// Unit of Work
builder.Services.AddScoped<ICreateReadingListUnit, CreateReadingListUnit>();
builder.Services.AddScoped<ICreateStoryInReadingListUnit, CreateStoryInReadingListUnit>();
builder.Services.AddScoped<IDeleteReadingListUnit, DeleteReadingListUnit>();
builder.Services.AddScoped<IDeleteStoryInReadingListUnit, DeleteStoryInReadingListUnit>();
builder.Services.AddScoped<IUpdateNoteForStoryInReadingListUnit, UpdateNoteForStoryInReadingListUnit>();
builder.Services.AddScoped<IUpdateReadingListPrivateUnit, UpdateReadingListPrivateUnit>();
builder.Services.AddScoped<IUpdateReadingListUnit, UpdateReadingListUnit>();
builder.Services.AddScoped<IUpdateReadingListVisibleOfResponceUnit, UpdateReadingListVisibleOfResponceUnit>();


//Use cases
builder.Services.AddScoped<ICreateConstantReadingListToUserCase, CreateConstantReadingListToUserCase>();
builder.Services.AddScoped<ICreateReadingListCase, CreateReadingListCase>();
builder.Services.AddScoped<IDeleteReadingListCase, DeleteReadingListCase>();
builder.Services.AddScoped<IGetListReadingListByCreatorIdCase, GetListReadingListByCreatorIdCase>();
builder.Services.AddScoped<IGetListReadingListByIdsCase, GetListReadingListByIdsCase>();
builder.Services.AddScoped<IGetReadingListCase, GetReadingListCase>();
builder.Services.AddScoped<ISaveStoryToReadingListCase, SaveStoryToReadingListCase>();
builder.Services.AddScoped<IUnSaveStoryFromReadingListCase, UnSaveStoryFromReadingListCase>();
builder.Services.AddScoped<IUpdateNoteToSaveStoryInReadingListCase, UpdateNoteToSaveStoryInReadingListCase>();
builder.Services.AddScoped<IUpdateReadingListCase, UpdateReadingListCase>();
builder.Services.AddScoped<IUpdateReadingListPrivateCase, UpdateReadingListPrivateCase>();
builder.Services.AddScoped<IUpdateReadingListVisibleOfResponceCase, UpdateReadingListVisibleOfResponceCase>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


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
app.UseAuthorization();



app.MapControllers();

app.Run();
