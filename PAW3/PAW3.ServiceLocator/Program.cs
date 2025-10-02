using PAW3.Architecture;
using PAW3.ServiceLocator.Services;
using static PAW3.ServiceLocator.Services.PeopleDataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRestProvider, RestProvider>();
builder.Services.AddScoped<IDogDataService, DogDataService>();
builder.Services.AddScoped<ITempDataService, TempDataService>();
builder.Services.AddScoped<IPeopleDataService, PeopleDataService>();

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
