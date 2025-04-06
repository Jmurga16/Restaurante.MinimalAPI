using Dapper;
using RestauranteMVP.Back.Models;
using System.Data;

namespace RestauranteMVP.Back.Data
{
    public class IngredientesRepository
    {
        private readonly IDbConnection _dbConnection;

        public IngredientesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Ingredientes>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Ingredientes>(
                "Ingredientes_Listar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Ingredientes> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Ingredientes>(
                "Ingredientes_Obtener_Por_Id",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            ) ?? new Ingredientes();
        }

        public async Task<int> AddAsync(Ingredientes ingrediente)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(
                "Insert_Ingredientes",
                new
                {
                    Nombre = ingrediente.Nombre,
                    Unidad_Medida = ingrediente.UnidadMedida,
                    Cantidad_Disp = ingrediente.CantidadDisponible
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(int id, Ingredientes ingrediente)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Ingredientes_Update",
                new
                {
                    Ingrediente_ID = id,
                    Ingrediente_Nombre = ingrediente.Nombre,
                    Ingrediente_Cantidad_Disp = ingrediente.CantidadDisponible,
                    Ingrediente_Unidad_Medida = ingrediente.UnidadMedida
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Ingredientes_Delete",
                new { Ingrediente_ID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }

}
