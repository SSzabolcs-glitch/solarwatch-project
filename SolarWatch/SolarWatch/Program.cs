using SolarWatch.Services.OpeningApis;
using SolarWatch.Services.Processors;
using SolarWatch.Services.Provider;
using SolarWatch.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICordinatesProcessor, CordinatesProcessor>();
builder.Services.AddSingleton<ISolarWatchProvider, SolarWatchProvider>();
builder.Services.AddSingleton<IOpenGeocodingApi, OpenGeocodingApi>();
builder.Services.AddSingleton<IOpenSunsetAndSunriseApi, OpenSunsetAndSunriseApi>();
builder.Services.AddSingleton<ICityRepository, CityRepository>();
builder.Services.AddSingleton<ISunsetSunriseRepository, SunsetSunriseRespoitory>();

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
