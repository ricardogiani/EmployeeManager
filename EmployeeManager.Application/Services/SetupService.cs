using System.Text;
using EmployeeManager.Adapter.Repositories;
using EmployeeManager.Application;
using EmployeeManager.Application.Services;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Interfaces.Services;
using EmployeeManager.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace EmployeeManager.Application.Services;

public static class SetupService
{

    static public void ConfigureLog(WebApplicationBuilder builder)
    {
        try
        {

            var fileLogPath = builder.Configuration["Log:FilePath"];
            var logLevel = builder.Configuration["Log:Level"];

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(fileLogPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .MinimumLevel.Is((Serilog.Events.LogEventLevel)int.Parse(logLevel))
                .CreateLogger();

            builder.Services.AddSerilog();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    static public void ConfigureAdaptersApp(IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddSingleton<IAdapterConfig, AdapterConfig>();
    }

    static public void ConfigureServicesApp(IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAuthProviderService, AuthProviderService>();
        services.AddScoped<ILoginService, LoginService>();
    }

    static public void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "JWTToken_Auth_API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
    }

    static public void ConfigureAuth(WebApplicationBuilder builder)
    {
        var jwtSecret = builder.Configuration["Jwt:Secret"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret))
            };
        });
    }

}