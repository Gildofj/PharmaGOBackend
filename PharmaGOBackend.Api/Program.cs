using System.Reflection;
using Microsoft.OpenApi;
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
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "PharmaGOBackend API",
            Description = "The API to handle Pharamcy ECommerce",
            Contact = new OpenApiContact
            {
                Name = "Gildo Junior",
                Email = "1gildojunior@gmail.com"
            }
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(_ =>
        {
            var requirement = new OpenApiSecurityRequirement();
            requirement.Add(new OpenApiSecuritySchemeReference("Bearer"), []);
            return requirement;
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
    }

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}