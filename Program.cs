using Microsoft.Data.SqlClient;
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

var app = builder.Build();

// Registrar los mapeos de Dapper FluentMap
MappingConfig.ConfigureMappings(builder.Services);

// 🔹 Registrar los servicios 
//builder.Services.AddScoped<IMenuService, MenuService>();


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