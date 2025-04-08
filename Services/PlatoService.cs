using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Services
{
    public class PlatoService
    {
        private readonly PlatoRepository _platoRepository;

        public PlatoService(PlatoRepository platoRepository)
        {
            _platoRepository = platoRepository;
        }

        public async Task<IEnumerable<Plato>> GetAllAsync()
        {
            return await _platoRepository.GetAllAsync();
        }

        public async Task<Plato> GetByIdAsync(int id)
        {
            return await _platoRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Plato plato)
        {
            return await _platoRepository.AddAsync(plato);
        }

        public async Task<bool> UpdateAsync(int id, Plato plato)
        {
            return await _platoRepository.UpdateAsync(id, plato);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _platoRepository.DeleteAsync(id);
        }
        public async Task<RecetaIngrediente> GetRecetaIngredientesByPlatoIdAsync(int platoId)
        {
            return await _platoRepository.GetRecetaIngredientesByPlatoIdAsync(platoId);
        }
    }

}
