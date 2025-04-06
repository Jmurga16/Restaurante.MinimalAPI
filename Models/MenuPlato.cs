namespace RestauranteMVP.Back.Models
{
    public class MenuPlato
    {
        public int MenuId { get; set; }
        public int PlatoId { get; set; }
        public List<Plato>? Platos { get; set; } = new List<Plato>();
        public List<Menu>? Menus { get; set; } = new List<Menu>();

    }
}
