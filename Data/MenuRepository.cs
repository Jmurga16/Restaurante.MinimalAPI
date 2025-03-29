using Dapper;
using RestauranteMVP.Back.Models;
using System.Data;

namespace RestauranteMVP.Back.Data
{
    public class MenuRepository
    {
        private readonly IDbConnection _dbConnection;

        public MenuRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Menu>("Menu_Obtener", commandType: CommandType.StoredProcedure);
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Menu>("Menu_Obtener_Por_Id", new { MenuId = id }, commandType: CommandType.StoredProcedure)
                ?? new Menu();
        }

        public async Task<int> AddAsync(Menu menu)
        {
            return await _dbConnection.ExecuteScalarAsync<int>("Insert_Menu", new { menu.Nombre, menu.Descripcion }, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAsync(int id, Menu menu)
        {
            var result = await _dbConnection.ExecuteAsync("Menu_Update", new { menu.Nombre, menu.Descripcion, MenuId = id }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync("Menu_Delete", new { MenuId = id }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
    }
}
