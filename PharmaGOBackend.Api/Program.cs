using PharmaGOBackend.Application;
using PharmaGOBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddAppliaction()
        .AddInfrastructure(builder.Configuration)
        .AddControllers();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
