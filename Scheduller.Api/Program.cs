using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Middlewares;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Implementations;
using Scheduller.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});

builder.Services.AddScoped<ModelRepository>();
builder.Services.AddScoped<PartRepository>();
builder.Services.AddScoped<ProcessComponentRepository>();
builder.Services.AddScoped<ProcessDetailRepository>();
builder.Services.AddScoped<WorkCenterRepository>();

builder.Services.AddScoped<IModelService, ModelService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
