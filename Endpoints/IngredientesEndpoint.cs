using RestauranteMVP.Back.Models;
using RestauranteMVP.Back.Services;

namespace RestauranteMVP.Back.Endpoints
{
    public static class IngredientesEndpoint
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/ingredientes", async (IngredientesService ingredientesService) =>
            {
                return Results.Ok(await ingredientesService.GetAllAsync());
            });

            app.MapGet("/ingredientes/{id}", async (int id, IngredientesService ingredientesService) =>
            {
                var ingredientes = await ingredientesService.GetByIdAsync(id);
                return ingredientes is not null ? Results.Ok(ingredientes) : Results.NotFound();
            });

            app.MapPost("/ingredientes", async (Ingredientes ingredientes, IngredientesService ingredientesService) =>
            {
                var id = await ingredientesService.AddAsync(ingredientes);
                return Results.Created($"/ingredientes/{id}", new { id });
            });

            app.MapPut("/ingredientes/{id}", async (int id, Ingredientes ingredientes, IngredientesService ingredientesService) =>
            {
                var updated = await ingredientesService.UpdateAsync(id, ingredientes);
                return updated ? Results.Ok(ingredientes) : Results.NotFound();            });

            app.MapDelete("/ingredientes/{id}", async (int id, IngredientesService ingredientesService) =>
            {
                var deleted = await ingredientesService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }

}
