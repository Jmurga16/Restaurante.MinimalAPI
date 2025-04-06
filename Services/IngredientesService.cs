using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Services
{
    public class IngredientesService
    {
        private readonly IngredientesRepository _ingredientesRepository;

        public IngredientesService(IngredientesRepository ingredientesRepository)
        {
            _ingredientesRepository = ingredientesRepository;
        }

        public async Task<IEnumerable<Ingredientes>> GetAllAsync()
        {
            return await _ingredientesRepository.GetAllAsync();
        }

        public async Task<Ingredientes> GetByIdAsync(int id)
        {
            return await _ingredientesRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Ingredientes ingredientes)
        {
            return await _ingredientesRepository.AddAsync(ingredientes);
        }

        public async Task<bool> UpdateAsync(int id, Ingredientes ingredientes)
        {
            return await _ingredientesRepository.UpdateAsync(id, ingredientes);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _ingredientesRepository.DeleteAsync(id);
        }
    }
}
