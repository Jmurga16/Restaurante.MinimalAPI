using Dapper;
using RestauranteMVP.Back.Models;
using System.Data;


namespace RestauranteMVP.Back.Data
{
    public class CategoriaRepository
    {
        private readonly IDbConnection _dbConnection;

        public CategoriaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Categoria>(
                "Categoria_Listar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Categoria>(
                "Categoria_Obtener_Por_Id",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            ) ?? new Categoria();
        }

        public async Task<int> AddAsync(Categoria categoria)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(
                "Insert_Categoria",
                new
                {
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion,
                    Encargado_ID = categoria.EncargadoId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(int id, Categoria categoria)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Update_Categoria",
                new
                {
                    Categoria_ID = id,
                    Categoria_Nombre = categoria.Nombre,
                    Categoria_Descripcion = categoria.Descripcion

                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Delete_Categoria",
                new { Categoria_ID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }


}
