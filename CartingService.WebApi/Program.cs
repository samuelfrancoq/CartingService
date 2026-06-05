using System.Reflection;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Services;
using CartingService.DAL.Interfaces;
using CartingService.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using MassTransit;
using MassTransit.Scheduling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductUpdatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("catalog-product-updated-queue", e =>
        {
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

            e.ConfigureConsumer<ProductUpdatedConsumer>(context);
        });
    });
});

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Carting Service API", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Carting Service API", Version = "v2" });
    // NFR: Self-Documented API (XML Docs)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// --- CUSTOM SERVICES REGISTRATION ---
// Registering the DAL (Repository)
builder.Services.AddSingleton<ICartRepository, LiteDbCartRepository>();

// Registering the BLL (Service)
builder.Services.AddScoped<ICartService, CartService>();
// ------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carting Service API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Carting Service API v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Asegúrate de tener instalado el paquete NuGet:
// Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer

// Si no lo tienes, ejecuta en la terminal del proyecto:
// dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer