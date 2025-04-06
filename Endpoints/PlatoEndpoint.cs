using RestauranteMVP.Back.Models;
using RestauranteMVP.Back.Services;

namespace RestauranteMVP.Back.Endpoints
{
    public static class PlatoEndpoint
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/plato", async (PlatoService platoService) =>
            {
                return Results.Ok(await platoService.GetAllAsync());
            });

            app.MapGet("/plato/{id}", async (int id, PlatoService platoService) =>
            {
                var plato = await platoService.GetByIdAsync(id);
                return plato is not null ? Results.Ok(plato) : Results.NotFound();
            });

            app.MapPost("/plato", async (Plato plato, PlatoService platoService) =>
            {
                var id = await platoService.AddAsync(plato);
                return Results.Created($"/plato/{id}", new { id });
            });

            app.MapPut("/plato/{id}", async (int id, Plato plato, PlatoService platoService) =>
            {
                var updated = await platoService.UpdateAsync(id, plato);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            app.MapDelete("/plato/{id}", async (int id, PlatoService platoService) =>
            {
                var deleted = await platoService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
