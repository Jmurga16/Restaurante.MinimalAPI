using Dapper;
using Microsoft.Data.SqlClient;
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

        public async Task<RecetaIngrediente> GetRecetaIngredientesByPlatoIdAsync(int platoId)
        {
            var parameters = new { Plato_ID = platoId };

            var recetaIngredientesDict = new Dictionary<int, RecetaIngrediente>();

            var result = await _dbConnection.QueryAsync<Receta, Ingredientes, RecetaIngrediente>(
                "Plato_Receta_Ingredientes_Listar", // El nombre del procedimiento almacenado
                (receta, ingrediente) =>
                {
                    // Solo esperamos una receta por plato, así que si la receta no está en el diccionario, la agregamos
                    if (!recetaIngredientesDict.TryGetValue(receta.RecetaId, out var recetaIngrediente))
                    {
                        recetaIngrediente = new RecetaIngrediente
                        {
                            RecetaId = receta.RecetaId,
                            Receta = receta,  // Asignamos la receta
                            Ingredientes = new List<Ingredientes>()
                        };

                        recetaIngredientesDict[receta.RecetaId] = recetaIngrediente;
                    }

                    // Agregamos el ingrediente a la lista de ingredientes
                    recetaIngrediente.Ingredientes.Add(ingrediente);

                    return recetaIngrediente;
                },
                parameters,
                splitOn: "Ingrediente_ID", // Esto divide los resultados entre Receta e Ingredientes
                commandType: CommandType.StoredProcedure
            );

            // Devolvemos la receta junto con los ingredientes
            return recetaIngredientesDict.Values.FirstOrDefault(); // Devolvemos la receta única asociada al plato
        }


    }

}
