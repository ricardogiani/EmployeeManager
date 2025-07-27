using EmployeeManager.Adapter.Mappers;
using EmployeeManager.Application;
using EmployeeManager.Application.Mappers;
using EmployeeManager.Application.Middlewares;
using EmployeeManager.Application.Services;
using Serilog;


internal class Program
{
    private static void Main(string[] args)
    {        
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            
            SetupService.ConfigureLog(builder);                        
            SetupService.ConfigureAdaptersApp(builder.Services);
            SetupService.ConfigureServicesApp(builder.Services);
            SetupService.ConfigureSwagger(builder.Services);
            SetupService.ConfigureAuth(builder);
            builder.Services.AddHostedService<StartupService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
                cfg.AddProfile<EmployeeDataMappingProfile>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();

            // Middleware to access claims
            app.UseMiddleware<CustomInterceptorMiddleware>();

            app.UseAuthorization();
            app.MapControllers();
            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

