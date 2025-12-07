using PharmaGOBackend.Api;
using PharmaGOBackend.Application;
using PharmaGOBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors();
    builder.Services
        .AddPresentation()
        .AddApplication(builder.Configuration)
        .AddInfrastructure(builder.Configuration);
    
    builder.Services.AddOpenApiDefault();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApiDefault();
    }

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}