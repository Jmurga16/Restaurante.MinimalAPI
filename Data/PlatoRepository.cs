using Dapper;
using RestauranteMVP.Back.Models;
using System.Data;

namespace RestauranteMVP.Back.Data
{
    public class PlatoRepository
    {
        private readonly IDbConnection _dbConnection;

        public PlatoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Plato>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Plato>(
                "Plato_Listar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Plato> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Plato>(
                "Plato_Obtener_Por_Id",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            ) ?? new Plato();
        }

        public async Task<int> AddAsync(Plato plato)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(
                "Insert_Plato",
                new
                {
                    Nombre = plato.Nombre,
                    Descripcion = plato.Descripcion,
                    Foto = plato.Foto,
                    Dificultad = plato.Dificultad,
                    Precio = plato.Precio,
                    Categoria_ID = plato.CategoriaId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(int id, Plato plato)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Update_Plato",
                new
                {
                    ID = id,
                    Nombre = plato.Nombre,
                    Descripcion = plato.Descripcion,
                    Foto = plato.Foto,
                    Dificultad = plato.Dificultad,
                    Precio = plato.Precio,
                    Categoria_ID = plato.CategoriaId
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Delete_Plato",
                new { PlatoID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }

}
