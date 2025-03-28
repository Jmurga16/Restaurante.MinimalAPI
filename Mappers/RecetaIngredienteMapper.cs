using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class RecetaIngredienteMapper : EntityMap<RecetaIngrediente>
    {
        public RecetaIngredienteMapper()
        {
            Map(ri => ri.RecetaId).ToColumn("Receta_ID");
            Map(ri => ri.IngredienteId).ToColumn("Ingrediente_ID");
            Map(ri => ri.CantidadRequerida).ToColumn("REC_ING_Cantidad_Requerida");
        }
    }
}
