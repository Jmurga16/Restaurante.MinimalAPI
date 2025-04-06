namespace RestauranteMVP.Back.Models
{
    public class Plato
    {
        public int PlatoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Dificultad { get; set; } = string.Empty;
        public int Precio { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
