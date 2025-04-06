using Dapper;
using RestauranteMVP.Back.Models;
using System.Data;

namespace RestauranteMVP.Back.Data
{
    public class RecetaRepository
    {
        private readonly IDbConnection _dbConnection;

        public RecetaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Receta>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Receta>(
                "Receta_Listar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Receta> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Receta>(
                "Receta_Obtener_Por_Id",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            ) ?? new Receta();
        }

        public async Task<int> AddAsync(Receta receta)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(
                "Insert_Receta",
                new
                {
                    Receta_Preparacion = receta.Preparacion,
                    Plato_ID = receta.PlatoId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(int id, Receta receta)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Update_Receta",
                new
                {
                    Receta_Preparacion = receta.Preparacion,
                    ID = id
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Eliminar_Receta",
                new { Receta_ID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }

}
