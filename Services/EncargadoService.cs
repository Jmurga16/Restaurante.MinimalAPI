using RestauranteMVP.Back.Data;
using RestauranteMVP.Back.Models;

namespace RestauranteMVP.Back.Services
{
    public class EncargadoService
    {
        private readonly EncargadoRepository _encargadoRepository;

        public EncargadoService(EncargadoRepository encargadoRepository)
        {
            _encargadoRepository = encargadoRepository;
        }

        public async Task<IEnumerable<Encargado>> GetAllAsync()
        {
            return await _encargadoRepository.GetAllAsync();
        }

        public async Task<Encargado> GetByIdAsync(int id)
        {
            return await _encargadoRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Encargado encargado)
        {
            return await _encargadoRepository.AddAsync(encargado);
        }

        public async Task<bool> UpdateAsync(int id, Encargado encargado)
        {
            return await _encargadoRepository.UpdateAsync(id, encargado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _encargadoRepository.DeleteAsync(id);
        }
    }
}
