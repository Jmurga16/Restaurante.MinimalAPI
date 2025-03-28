using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class PlatoMapper : EntityMap<Plato>
    {
        public PlatoMapper()
        {
            Map(p => p.PlatoId).ToColumn("Plato_ID");
            Map(p => p.Nombre).ToColumn("Plato_Nombre");
            Map(p => p.Descripcion).ToColumn("Plato_Descripcion");
            Map(p => p.Foto).ToColumn("Plato_Foto");
            Map(p => p.Dificultad).ToColumn("Plato_Dificultad");
            Map(p => p.Precio).ToColumn("Plato_Precio");
            Map(p => p.CategoriaId).ToColumn("Categoria_ID");
        }
    }
}
