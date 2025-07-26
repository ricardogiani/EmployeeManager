using EmployeeManager.Adapter.Mappers;
using EmployeeManager.Application.Mappers;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
}

