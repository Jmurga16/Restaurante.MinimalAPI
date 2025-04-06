using RestauranteMVP.Back.Models;
using RestauranteMVP.Back.Services;

namespace RestauranteMVP.Back.Endpoints
{
    public static class EncargadoEndpoint
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/encargado", async (EncargadoService encargadoService) =>
            {
                return Results.Ok(await encargadoService.GetAllAsync());
            });

            app.MapGet("/encargado/{id}", async (int id, EncargadoService encargadoService) =>
            {
                var encargado = await encargadoService.GetByIdAsync(id);
                return encargado is not null ? Results.Ok(encargado) : Results.NotFound();
            });

            app.MapPost("/encargado", async (Encargado encargado, EncargadoService encargadoService) =>
            {
                var id = await encargadoService.AddAsync(encargado);
                return Results.Created($"/encargado/{id}", new { id });
            });

            app.MapPut("/encargado/{id}", async (int id, Encargado encargado, EncargadoService encargadoService) =>
            {
                var updated = await encargadoService.UpdateAsync(id, encargado);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            app.MapDelete("/encargado/{id}", async (int id, EncargadoService encargadoService) =>
            {
                var deleted = await encargadoService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }

}
