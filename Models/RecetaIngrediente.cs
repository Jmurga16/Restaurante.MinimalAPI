namespace RestauranteMVP.Back.Models
{
    public class RecetaIngrediente
    {
        public int RecetaId { get; set; }
        public int IngredienteId { get; set; }
        public decimal CantidadRequerida { get; set; }
        public Receta Receta { get; set; } = new Receta();
        public List<Ingredientes> Ingredientes { get; set; } = new List<Ingredientes>();
    }
}
