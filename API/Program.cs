

using System.Text;
using API.Data;
using API.Profiles;
using API.Services;
using API.Services.Interfaces;
using API.Tools.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);







/* PRODUCT */
//builder.Services.AddScoped<IProductService, ProductService>();

/* USER */
builder.Services.AddScoped<IUserService, UserService>();

/* INCOME */
builder.Services.AddScoped<IIncomesService, IncomesService>();

/* CATEGORY */
//builder.Services.AddScoped<ICategoryService, CategoryService>();

/* DATACONTEXT */

// lisätään dbcontext yläpuolella builder = WebApplication.CreateBuilder(args);
// tämä lisää DataContextin osaksi buildi prosessia
// näin saamme tietokantayhteyden mukaan
builder.Services.AddDbContext<DataContext>(opt =>
{
    // AddDbContextille pitää kertoa, mistä tietokantayhteyden speksit löytyvät
    // näitä meillä ei vielä ole.
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// alapuolella on builde.Services.AddControllers();

/*  MAPPER */
var mapperConfig = new MapperConfiguration(mc =>
{
    // UserProfile
    mc.AddProfile(new UserProfile());

    // Income Profile
    mc.AddProfile(new IncomesProfile());
    
});

// luodaan uusi mapper aiemmin tehdyllä konfiguraatiolla
IMapper mapper = mapperConfig.CreateMapper();
// voidaan käyttää singletonia (kaikki mapperit kaikissa controllereissa käyttävät samaa)
builder.Services.AddSingleton(mapper);


/* TOKEN */

// Tämä on tokenin luomista varten
builder.Services.AddScoped<ITokenTool, API.Tools.SymmetricToken>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    // varmistetaan, että TokenKey löytyy
    var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("token key not found");
    // konfataan tässä, mitä tarkistetaan
    o.TokenValidationParameters = new TokenValidationParameters
    {
        // varmistaa allekirjoituksen
        ValidateIssuerSigningKey = true,
        // jotta allekirjoituksen voi tarkistaa,
        // pitää kertoa, mitä avainta allekirjoituksessa käytetään
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        // issuerin tarkistus on pois päältä
        ValidateIssuer = false,
        // myös audiencen tarkistus on pois päältä
        ValidateAudience = false
    };
});

/* LÖYDETTY KOODI, KOSKA AUTENTIKAATIO EI MUUTEN ONNISTUNUT CATEGORIOIDEN ADMIN HALLINASSA */
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
