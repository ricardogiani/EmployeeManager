using EmployeeManager.Adapter.Mappers;
using EmployeeManager.Application.Mappers;
using Serilog;


internal class Program
{
    private static void Main(string[] args)
    {        
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            
            SetupProgram.ConfigureLog(builder);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
                cfg.AddProfile<EmployeeDataMappingProfile>();
            });

            SetupProgram.ConfigureServicesApp(builder.Services);
            SetupProgram.ConfigureSwagger(builder.Services);
            SetupProgram.ConfigureAuth(builder);

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

