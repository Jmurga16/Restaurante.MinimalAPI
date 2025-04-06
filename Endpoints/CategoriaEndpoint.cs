using RestauranteMVP.Back.Models;
using RestauranteMVP.Back.Services;

namespace RestauranteMVP.Back.Endpoints
{
    public static class CategoriaEndpoint
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/categoria", async (CategoriaService categoriaService) =>
            {
                return Results.Ok(await categoriaService.GetAllAsync());
            });

            app.MapGet("/categoria/{id}", async (int id, CategoriaService categoriaService) =>
            {
                var categoria = await categoriaService.GetByIdAsync(id);
                return categoria is not null ? Results.Ok(categoria) : Results.NotFound();
            });

            app.MapPost("/categoria", async (Categoria categoria, CategoriaService categoriaService) =>
            {
                var id = await categoriaService.AddAsync(categoria);
                return Results.Created($"/categoria/{id}", new { id });
            });

            app.MapPut("/categoria/{id}", async (int id, Categoria categoria, CategoriaService categoriaService) =>
            {
                var updated = await categoriaService.UpdateAsync(id, categoria);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            app.MapDelete("/categoria/{id}", async (int id, CategoriaService categoriaService) =>
            {
                var deleted = await categoriaService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }

}
