using PharmaGO.Api;
using PharmaGO.Api.Authentication;
using PharmaGO.Api.Doc;
using PharmaGO.Application;
using PharmaGO.Infrastructure;
using PharmaGO.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors();
    builder.Services.ConfigureJwtAuthentication(builder.Configuration).ConfigurePermissionsRoles();
    builder.Services
        .AddPresentation()
        .AddApplication(builder.Configuration)
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddOpenApiDefault();
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        await IdentitySeed.SeedAsync(scope.ServiceProvider);
        await PharmacyDemoSeed.SeedAsync(scope.ServiceProvider);
    }
    
    app.UseOpenApiDefault();

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}