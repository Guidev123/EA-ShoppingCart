using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedLib.MessageBus;
using ShoppingCart.API.BackgroundServices;
using ShoppingCart.API.Data;
using ShoppingCart.API.Data.Repositoreis;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.UseCases;
using ShoppingCart.API.UseCases.Interfaces;
using System.Text;

namespace ShoppingCart.API.Middlewares
{
    public static class ApplicationMiddlwares
    {
        public static void AddDbContext(this WebApplicationBuilder builder)=>
            builder.Services.AddDbContext<CartDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICustomerCartRepository, CustomerCartRepository>();
            builder.Services.AddTransient<IItemCartRepository, ItemCartRepository>();
        }

        public static void AddMessageBusConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBus(builder.Configuration.GetMessageQueueConnection("MessageBus"));
            builder.Services.AddHostedService<CartBackgroundService>();
        }

        public static void AddDocumentationConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Ecommerce APP Enterprise API",
                    Contact = new OpenApiContact() { Name = "Guilherme Nascimento", Email = "guirafaelrn@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/MIT") }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta forma: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
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
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddJwtConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.ASCII.GetBytes(builder.Configuration["JsonWebTokenData:Secret"] ?? string.Empty)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JsonWebTokenData:ValidAt"],
                    ValidIssuer = builder.Configuration["JsonWebTokenData:Issuer"]
                };
            });
            builder.Services.AddAuthorizationBuilder();
        }

        public static void AddUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IUserUseCase, UserUseCase>();
            builder.Services.AddTransient<ICartUseCase, CartUseCase>();
        }

        public static void UseSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
