using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class MenuMapper : EntityMap<Menu>
    {
        public MenuMapper()
        {
            Map(m => m.MenuId).ToColumn("Menu_ID");
            Map(m => m.Nombre).ToColumn("Menu_Nombre");
            Map(m => m.Descripcion).ToColumn("Menu_Descripcion");
        }
    }
}
