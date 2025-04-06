using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _categoriaRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Categoria categoria)
        {
            return await _categoriaRepository.AddAsync(categoria);
        }

        public async Task<bool> UpdateAsync(int id, Categoria categoria)
        {
            return await _categoriaRepository.UpdateAsync(id, categoria);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoriaRepository.DeleteAsync(id);
        }
    }
}
