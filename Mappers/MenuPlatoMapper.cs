using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class MenuPlatoMapper : EntityMap<MenuPlato>
    {
        public MenuPlatoMapper()
        {
            Map(mp => mp.MenuId).ToColumn("Menu_ID");
            Map(mp => mp.PlatoId).ToColumn("Plato_ID");
        }
    }

}
