namespace RestauranteMVP.Back.Models
{
    public class Receta
    {
        public int RecetaId { get; set; }
        public string Preparacion { get; set; } = string.Empty;
        public int PlatoId { get; set; }
        //public virtual Plato Plato { get; set; }
        //public virtual ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
    }
}
