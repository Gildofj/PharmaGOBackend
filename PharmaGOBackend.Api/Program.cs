using Microsoft.AspNetCore.Mvc.Infrastructure;
using PharmaGOBackend.Application;
using PharmaGOBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddAppliaction()
        .AddInfrastructure(builder.Configuration)
        .AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, PharmaGOProblemDetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
