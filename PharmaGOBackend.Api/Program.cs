using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PharmaGOBackend.Application;
using PharmaGOBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors();
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);


    builder.Services.AddSingleton<ProblemDetailsFactory, PharmaGOProblemDetailsFactory>();

    builder.Services.AddDbContext(builder.Configuration.GetConnectionString("PharmaGOContext"));

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
