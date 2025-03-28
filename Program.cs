using Microsoft.Data.SqlClient;
using MinimalApiDapper.Endpoints;
using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Services;
using RestauranteMVP.Back.Utils;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Habilitar CORS
builder.Services.AddCors();

// Registrar la conexión a la BD
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorios
builder.Services.AddScoped<MenuRepository>();

// Registrar servicios
builder.Services.AddScoped<MenuService>();

// Registrar los mapeos de Dapper FluentMap
MappingConfig.ConfigureMappings(builder.Services);

var app = builder.Build();

// Registrar endpoints
MenuEndpoint.RegisterEndpoints(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.Run();