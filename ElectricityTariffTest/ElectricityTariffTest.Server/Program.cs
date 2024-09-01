using ElectricityTariffTest.Middleware;
using ElectricityTariffTest.Server.Calculators;
using ElectricityTariffTest.Server.Interfaces;
using ElectricityTariffTest.Server.Repositories;
using ElectricityTariffTest.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging(); // Add logging services
builder.Services.AddScoped<ITariffService, TariffService>();
builder.Services.AddScoped<ITariffRepository, TariffRepository>();
builder.Services.AddScoped<ITariffCalculator, BasicTariffCalculator>();
builder.Services.AddScoped<ITariffCalculator, PackagedTariffCalculator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials()
           );


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
