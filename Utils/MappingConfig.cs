using Dapper.FluentMap;
using RestauranteMVP.Back.Mappers;

namespace RestauranteMVP.Back.Utils
{
    public static class MappingConfig
    {
        public static void ConfigureMappings(IServiceCollection services)
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new MenuMapper());
                config.AddMap(new IngredientesMapper());
                config.AddMap(new EncargadoMapper());
                config.AddMap(new CategoriaMapper());
                config.AddMap(new PlatoMapper());
                config.AddMap(new RecetaMapper());
                config.AddMap(new MenuPlatoMapper());
                config.AddMap(new RecetaIngredienteMapper());
            });
        }
    }
}
