using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using UsingDapper.Endpoints;
using UsingDapper.Models;
using UsingDapper.repo;
using UsingDapper.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(ServiceProvider =>
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    var connectionstring = configuration.GetConnectionString("connection") ??
    throw new ApplicationException("The connection string is null");

    return new SqlConnectionFactory(connectionstring);
});

//builder.Services.AddTransient<DapperDbContext>();

//builder.Services.AddTransient<IEmployeeRepo, EmployeeRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEmployeeEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
