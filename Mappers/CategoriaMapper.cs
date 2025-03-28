using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class CategoriaMapper : EntityMap<Categoria>
    {
        public CategoriaMapper()
        {
            Map(c => c.CategoriaId).ToColumn("Categoria_ID");
            Map(c => c.Nombre).ToColumn("Categoria_Nombre");
            Map(c => c.Descripcion).ToColumn("Categoria_Descripcion");
            Map(c => c.EncargadoId).ToColumn("Encargado_ID");
        }
    }
}
