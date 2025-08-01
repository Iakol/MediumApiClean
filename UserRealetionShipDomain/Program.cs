using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks;
using UserRealetionShipDomain.Application.UnitOfWorks.UserBlockUnitsOfWorks;
using UserRealetionShipDomain.Application.UnitOfWorks.UserMuteUnitsOfWorks;
using UserRealetionShipDomain.Application.UseCases.SaveReadingListCases;
using UserRealetionShipDomain.Application.UseCases.UserBlockCases;
using UserRealetionShipDomain.Application.UseCases.UserFollowCases;
using UserRealetionShipDomain.Application.UseCases.UserMuteCases;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;
using UserRealetionShipDomain.Infrastructure.DataBase.Repositories.RelationShip;
using UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.BlockUsersUnintsOfWorks;
using UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.MuteUsersUnitsOfWorks;
using UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.SaveReadingListUnitOfWorks;
using UserRealetionShipDomain.Presentation.UseCases.FollowUserCases;
using UserRealetionShipDomain.Presentation.UseCases.ReadingListCases;
using UserRealetionShipDomain.Presentation.UseCases.UserBlockCases;
using UserRealetionShipDomain.Presentation.UseCases.UserMuteCases;

var builder = WebApplication.CreateBuilder(args);
var envName = builder.Environment.EnvironmentName;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

if (envName.Equals("Development") || envName.Equals("DevelopmentTestUser")) 
{
    builder.Services.AddSwaggerGen();
}




//Repositories
builder.Services.AddScoped<IUserMuteRepository, UserMuteModelRepository>();
builder.Services.AddScoped<ISaveReadingListRepository, SaveReadingListModelRepository>();
builder.Services.AddScoped<IUserBlockRepository, UserBlockModelRepository>();
builder.Services.AddScoped<IUserFollowRepository, UserFollowModelRepository>();

// UnitsOfWork
builder.Services.AddScoped<IFollowUserUnitOfWork, FollowUserUnitOfWork>();
builder.Services.AddScoped<IUnFollowUserUnitOfWork, UnFollowUserUnitOfWork>();
builder.Services.AddScoped<ISaveReadingListUnitOfWork, SaveReadingListUnitOfWork>();
builder.Services.AddScoped<IUnSaveReadingListUnitOfWork, UnSaveReadingListUnitOfWork>();
builder.Services.AddScoped<IBlockUserUnitOfWork, BlockUserUnitOfWork>();
builder.Services.AddScoped<IUnBlockUserUnitOfWork, UnBlockUserUnitOfWork>();
builder.Services.AddScoped<IMuteUserUnitOfWork, MuteUserUnitOfWork>();
builder.Services.AddScoped<IUnMuteUserUnitOfWork, UnMuteUserUnitOfWork>();

//UseCases
builder.Services.AddScoped<IUnFollowUserCase, UnFollowUserCase>();
builder.Services.AddScoped<IGetUserFollowListCase, GetUserFollowListCase>();
builder.Services.AddScoped<IGetFollowCountCase, GetFollowCountCase>();
builder.Services.AddScoped<IFollowUserCase, FollowUserCase>();
builder.Services.AddScoped<IUnSaveReadingListCase, UnSaveReadingListCase>();
builder.Services.AddScoped<ISaveReadingListCase, SaveReadingListCase>();
builder.Services.AddScoped<IGetUserSaveReadingListCase, GetUserSaveReadingListCase>();
builder.Services.AddScoped<IUnBlockUserCase, UnBlockUserCase>();
builder.Services.AddScoped<IGetUserBlockListCase,GetUserBlockListCase>();
builder.Services.AddScoped<IBlockUserCase, BlockUserCase>();
builder.Services.AddScoped<IUnMuteCase, UnMuteCase>();
builder.Services.AddScoped<IMuteUserCase, MuteUserCase>();
builder.Services.AddScoped<IGetUserMuteListCase, GetUserMuteListCase>();



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

app.Use(async (context, next) =>
{
    Console.WriteLine(context.Request.Headers);

    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();




app.Run();
