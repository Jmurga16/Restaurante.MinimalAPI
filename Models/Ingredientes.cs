namespace RestauranteMVP.Back.Models
{
    public class Ingredientes
    {
        public int? IngredienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public int CantidadDisponible { get; set; }
    }
}
