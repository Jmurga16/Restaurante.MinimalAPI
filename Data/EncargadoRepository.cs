using Dapper;
using RestauranteMVP.Back.Models;
using System.Data;

namespace RestauranteMVP.Back.Data
{
    public class EncargadoRepository
    {
        private readonly IDbConnection _dbConnection;

        public EncargadoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Encargado>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Encargado>(
                "Encargado_Listar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Encargado> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Encargado>(
                "Encargado_Obtener_Por_Id",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            ) ?? new Encargado();
        }

        public async Task<int> AddAsync(Encargado encargado)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(
                "Insert_Encargado",
                new
                {
                    Nombre = encargado.Nombre,
                    Apellido = encargado.Apellido,
                    Tipo_Doc = encargado.TipoDocumento,
                    Documento = encargado.Documento,
                    Mail = encargado.Email,
                    Tel = encargado.Telefono,
                    Direccion = encargado.Direccion
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(int id, Encargado encargado)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Update_Encargado",
                new
                {
                    Encargado_ID = id,
                    Encargado_Nombre = encargado.Nombre,
                    Encargado_Apellido = encargado.Apellido,
                    Encargado_Tipo_Doc = encargado.TipoDocumento,
                    Encargado_Documento = encargado.Documento,
                    Encargado_Mail = encargado.Email,
                    Encargado_Tel = encargado.Telefono,
                    Encargado_Direccion = encargado.Direccion
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Delete_Encargado",
                new { Encargado_ID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }

}
