namespace RestauranteMVP.Back.Models
{
    public class RecetaIngrediente
    {
        public int RecetaId { get; set; }
        public int IngredienteId { get; set; }
        public decimal CantidadRequerida { get; set; }
        public List<Receta> Recetas { get; set; } = new List<Receta>();
        public List<Ingredientes> Ingredientes { get; set; } = new List<Ingredientes>();
    }
}
