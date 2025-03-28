namespace RestauranteMVP.Back.Models
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public int CantidadDisponible { get; set; }
        //public virtual ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
    }
}
