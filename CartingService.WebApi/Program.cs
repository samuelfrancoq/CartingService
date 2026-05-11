using CartingService.BLL.Interfaces;
using CartingService.BLL.Services;
using CartingService.DAL.Interfaces;
using CartingService.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();