using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class MenuService : IMenuService
    { 
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuDto>> ObtenerMenuPermitido(string nombreUsuario)
        {
            var menu =  await _menuRepository.ObtenerMenuPorUsuario(nombreUsuario);
            var lookup = menu.ToDictionary(x => x.IdMenu);

            // Mapear a DTO
            foreach (var item in menu)
            {
                if (item.PadreId.HasValue && lookup.ContainsKey(item.PadreId.Value))
                    lookup[item.PadreId.Value].Hijos.Add(item);
            }

            return menu.Where(x => x.PadreId == 0).OrderBy(x => x.Orden).ToList();

        }
        public async Task<List<Menu>> ListarMenusAsync()
        {
            return await _menuRepository.GetAllMenusAsync();
        }

        public async Task<Menu> ObtenerPorIdAsync(int id)
        {
            return await _menuRepository.GetMenuByIdAsync(id);
        }

        public async Task<int> CrearMenuAsync(Menu menu)
        {
            menu.Estado = "A";
            menu.FechaCreacion = DateTime.Now;
            return await _menuRepository.AddMenuAsync(menu);
        }

        public async Task<bool> ActualizarMenuAsync(Menu menu)
        {
            menu.FechaModificacion = DateTime.Now;
            return await _menuRepository.UpdateMenuAsync(menu);
        }

        public async Task<bool> EliminarMenuAsync(int id, string usuario)
        {
            return await _menuRepository.DeleteMenuAsync(id, usuario);
        }
    }
}
