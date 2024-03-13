using WebApiProject.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/temperature/{longitude}/{latitude}", async (string longitude, string latitude) =>
{
    WeatherController weatherController = new WeatherController();
    var temperture = await weatherController.GetCurrentTemperature(longitude, latitude);
    return temperture;
})
.WithName("Get temperature")
.WithDescription("Get temperature from SMHI based on cordinates")
.WithOpenApi();

app.Run();