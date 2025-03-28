using RestauranteMVP.Back.Models;
using RestauranteMVP.Back.Services;

namespace MinimalApiDapper.Endpoints;

public static class MenuEndpoint
{
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/menus", async (MenuService menuService) =>
        {
            return Results.Ok(await menuService.GetAllAsync());
        });

        app.MapGet("/menus/{id}", async (int id, MenuService menuService) =>
        {
            var menu = await menuService.GetByIdAsync(id);
            return menu is not null ? Results.Ok(menu) : Results.NotFound();
        });

        app.MapPost("/menus", async (Menu menu, MenuService menuService) =>
        {
            var id = await menuService.AddAsync(menu);
            return Results.Created($"/menus/{id}", new { id });
        });

        app.MapPut("/menus/{id}", async (int id, Menu menu, MenuService menuService) =>
        {
            var updated = await menuService.UpdateAsync(id, menu);
            return updated ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/menus/{id}", async (int id, MenuService menuService) =>
        {
            var deleted = await menuService.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}
