using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PizzaApp.DataAccess.Data;
using PizzaApp.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace PizzaApp.Helpers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public class ConfigBuilder
        {
            public IServiceCollection Services { get; set; }
            public IConfiguration Configuration { get; }
            public IdentityBuilder IdentityBuilder { get; set; }
            public AuthenticationBuilder AuthenticationBuilder { get; set; }

            public ConfigBuilder(IServiceCollection services, IConfiguration configuration)
            {
                Services = services;
                Configuration = configuration;
            }
        }

        public static ConfigBuilder AddPostgreSQLDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").Value;

            services.AddDbContext<PizzaAppDbContext>(option =>
            option.UseNpgsql(connectionString));

            return new(services, configuration);
        }

        public static ConfigBuilder AddSwagger(this ConfigBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme, e.g. Bearer {token}",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return builder;
        }

        public static ConfigBuilder AddIdentity(this ConfigBuilder builder)
        {
            builder.IdentityBuilder = builder.Services.AddIdentityCore<User>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<PizzaAppDbContext>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static ConfigBuilder AddCors(this ConfigBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed((hosts) => true));
            });

            return builder;
        }

        public static ConfigBuilder AddAuthentication(this ConfigBuilder builder)
        {
            builder.AuthenticationBuilder = builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            return builder;
        }

        public static ConfigBuilder AddJWT(this ConfigBuilder builder, IConfiguration configuration)
        {
            var Token = configuration.GetSection("Token").Value;
            builder.AuthenticationBuilder.AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                    GetBytes(Token)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            return builder;
        }
    }
}