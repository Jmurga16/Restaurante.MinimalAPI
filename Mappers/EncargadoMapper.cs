using Dapper.FluentMap.Mapping;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Mappers
{
    public class EncargadoMapper : EntityMap<Encargado>
    {
        public EncargadoMapper()
        {
            Map(e => e.EncargadoId).ToColumn("Encargado_ID");
            Map(e => e.Nombre).ToColumn("Encargado_Nombre");
            Map(e => e.Apellido).ToColumn("Encargado_Apellido");
            Map(e => e.TipoDocumento).ToColumn("Encargado_Tipo_Doc");
            Map(e => e.Documento).ToColumn("Encargado_Documento");
            Map(e => e.Email).ToColumn("Encargado_Mail");
            Map(e => e.Telefono).ToColumn("Encargado_Tel");
            Map(e => e.Direccion).ToColumn("Encargado_Direccion");
        }
    }
}
