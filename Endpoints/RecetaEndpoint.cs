using RestauranteMVP.Back.Models;
using RestauranteMVP.Back.Services;

namespace RestauranteMVP.Back.Endpoints
{
    public static class RecetaEndpoint
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/receta", async (RecetaService recetaService) =>
            {
                return Results.Ok(await recetaService.GetAllAsync());
            });

            app.MapGet("/receta/{id}", async (int id, RecetaService recetaService) =>
            {
                var receta = await recetaService.GetByIdAsync(id);
                return receta is not null ? Results.Ok(receta) : Results.NotFound();
            });

            app.MapPost("/receta", async (Receta receta, RecetaService recetaService) =>
            {
                var id = await recetaService.AddAsync(receta);
                return Results.Created($"/receta/{id}", new { id });
            });

            app.MapPut("/receta/{id}", async (int id, Receta receta, RecetaService recetaService) =>
            {
                var updated = await recetaService.UpdateAsync(id, receta);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            app.MapDelete("/receta/{id}", async (int id, RecetaService recetaService) =>
            {
                var deleted = await recetaService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
