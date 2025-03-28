namespace RestauranteMVP.Back.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        //public virtual ICollection<MenuPlato> MenuPlatos { get; set; } = new List<MenuPlato>();
    }
}
