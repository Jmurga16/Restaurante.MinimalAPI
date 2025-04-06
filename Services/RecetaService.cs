using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Services
{
    public class RecetaService
    {
        private readonly RecetaRepository _recetaRepository;

        public RecetaService(RecetaRepository recetaRepository)
        {
            _recetaRepository = recetaRepository;
        }

        public async Task<IEnumerable<Receta>> GetAllAsync()
        {
            return await _recetaRepository.GetAllAsync();
        }

        public async Task<Receta> GetByIdAsync(int id)
        {
            return await _recetaRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Receta receta)
        {
            return await _recetaRepository.AddAsync(receta);
        }

        public async Task<bool> UpdateAsync(int id, Receta receta)
        {
            return await _recetaRepository.UpdateAsync(id, receta);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _recetaRepository.DeleteAsync(id);
        }
    }
}
