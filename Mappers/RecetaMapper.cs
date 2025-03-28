using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class RecetaMapper : EntityMap<Receta>
    {
        public RecetaMapper()
        {
            Map(r => r.RecetaId).ToColumn("Receta_ID");
            Map(r => r.Preparacion).ToColumn("Receta_Preparacion");
            Map(r => r.PlatoId).ToColumn("Plato_ID");
        }
    }
}
