using PizzaApp.Helpers.DependencyInjectionContainer;
using PizzaApp.Helpers.Extensions;
using PizzaApp.Mappers.MappersConfig;
using PizzaApp.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var pizzaAppSettings = builder.Configuration.GetSection("PizzaAppSettings");

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly)
    .AddPostgreSQLDbContext(pizzaAppSettings)
    .AddSwagger()
    .AddIdentity()
    .AddCors()
    .AddAuthentication()
    .AddJWT(pizzaAppSettings);

builder.Services.Configure<PizzaAppSettings>(pizzaAppSettings);
var pizzaAppSettingsObject = pizzaAppSettings.Get<PizzaAppSettings>();

var connectionString = pizzaAppSettingsObject.ConnectionString;

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

app.UseCors("CORSPolicy");

app.Run();