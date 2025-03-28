namespace RestauranteMVP.Back.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EncargadoId { get; set; }
        //public virtual Encargado Encargado { get; set; }
        //public virtual ICollection<Plato> Platos { get; set; } = new List<Plato>();
    }
}
