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
            return await _dbConnection.QueryAsync<Menu>(
                "Menu_Listar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Menu>(
                "Menu_Obtener_Por_Id",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            ) ?? new Menu();
        }

        public async Task<int> AddAsync(Menu menu)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(
                "Insert_Menu",
                new
                {
                    Menu_Nombre = menu.Nombre,
                    Menu_Descripcion = menu.Descripcion
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(int id, Menu menu)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Menu_Update",
                new
                {
                    Menu_Nombre = menu.Nombre,
                    Menu_Descripcion = menu.Descripcion,
                    Menu_ID = id
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.ExecuteAsync(
                "Menu_Delete",
                new { Menu_ID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<List<MenuPlato>> GetMenuPlatoAsync()
        {
            var menuPlatosDict = new Dictionary<int, MenuPlato>();

            var result = await _dbConnection.QueryAsync<Menu, Plato, MenuPlato>(
                "Menu_Plato_Listar",
                (menu, plato) =>
                {
                    // Verificar si el Menu ya existe en el diccionario
                    if (!menuPlatosDict.TryGetValue(menu.MenuId, out var menuPlato))
                    {
                        // Crear un nuevo MenuPlato si no existe
                        menuPlato = new MenuPlato
                        {
                            MenuId = menu.MenuId,
                            Menus = new List<Menu> { menu }
                        };
                        menuPlatosDict.Add(menu.MenuId, menuPlato);
                    }

                    menuPlatosDict[menu.MenuId].Platos.Add(plato);

                    return menuPlato;
                },
                splitOn: "Plato_ID",
                commandType: CommandType.StoredProcedure
            );

            return menuPlatosDict.Values.ToList();
        }
    }

}
