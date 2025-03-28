namespace RestauranteMVP.Back.Models
{
    public class RecetaIngrediente
    {
        public int RecetaId { get; set; }
        public int IngredienteId { get; set; }
        public decimal CantidadRequerida { get; set; }        
        //public virtual Receta Receta { get; set; }        
        //public virtual Ingrediente Ingrediente { get; set; }
    }
}
