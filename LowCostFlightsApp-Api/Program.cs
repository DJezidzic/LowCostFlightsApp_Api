using Microsoft.EntityFrameworkCore;
using LowCostFlightsAppApi.Data;
using LowCostFlightsAppApi.Services;
using LowCostFlightsAppApi.ServiceInterfaces;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("AmadeusServiceApi", client => {
    client.BaseAddress = new Uri("https://test.api.amadeus.com");
});
builder.Services.AddScoped<IAmadeusService,AmadeusService>();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration["DefaultConnection"]);
});

// Enable CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
