using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Services
{
    public class MenuService
    {
        private readonly MenuRepository _menuRepository;

        public MenuService(MenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _menuRepository.GetAllAsync();
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _menuRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Menu menu)
        {
            return await _menuRepository.AddAsync(menu);
        }

        public async Task<bool> UpdateAsync(int id, Menu menu)
        {
            return await _menuRepository.UpdateAsync(id, menu);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _menuRepository.DeleteAsync(id);
        }
    }

}
