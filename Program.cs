using Microsoft.Data.SqlClient;
using MinimalApiDapper.Endpoints;
using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Endpoints;
using RestauranteMVP.Back.Services;
using RestauranteMVP.Back.Utils;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Restaurante API",
        Version = "v1",
        Description = "API para gestionar recetas en el restaurante"
    });
});

// Habilitar CORS
builder.Services.AddCors();

// Registrar la conexión a la BD
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorios
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<EncargadoRepository>();
builder.Services.AddScoped<IngredientesRepository>();
builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<PlatoRepository>();
builder.Services.AddScoped<RecetaRepository>();

// Registrar servicios
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<EncargadoService>();
builder.Services.AddScoped<IngredientesService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<PlatoService>();
builder.Services.AddScoped<RecetaService>();

// Registrar los mapeos de Dapper FluentMap
MappingConfig.ConfigureMappings(builder.Services);

var app = builder.Build();

// Registrar endpoints
CategoriaEndpoint.RegisterEndpoints(app);
EncargadoEndpoint.RegisterEndpoints(app);
IngredientesEndpoint.RegisterEndpoints(app);
MenuEndpoint.RegisterEndpoints(app);
PlatoEndpoint.RegisterEndpoints(app);
RecetaEndpoint.RegisterEndpoints(app);


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