using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PizzaApp.DataAccess.Data;
using PizzaApp.Domain.Entities;
using PizzaApp.Helpers.DependencyInjectionContainer;
using PizzaApp.Mappers.MappersConfig;
using PizzaApp.Shared.Settings;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var pizzaAppSettings = builder.Configuration.GetSection("PizzaAppSettings");
builder.Services.Configure<PizzaAppSettings>(pizzaAppSettings);
var pizzaAppSettingsObject = pizzaAppSettings.Get<PizzaAppSettings>();

var connectionString = pizzaAppSettingsObject.ConnectionString;

//Setting db context with connection string
builder.Services.AddDbContext<PizzaAppDbContext>(option =>
option.UseNpgsql(connectionString));

//Adding Identity
builder.Services.AddIdentityCore<User>(option =>
{
    option.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<PizzaAppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddCors(options => options.AddPolicy("myPolicy", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.InjectServices();
builder.Services.InjectRepositories();
builder.Services.InjectDbContext(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("myPolicy");

app.Run();