﻿using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class IngredienteMapper : EntityMap<Ingrediente>
    {
        public IngredienteMapper()
        {
            Map(i => i.IngredienteId).ToColumn("Ingrediente_ID");
            Map(i => i.Nombre).ToColumn("Ingrediente_Nombre");
            Map(i => i.UnidadMedida).ToColumn("Ingrediente_Unidad_Medida");
            Map(i => i.CantidadDisponible).ToColumn("Ingrediente_Cantidad_Disp");
        }
    }
}
