using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
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
    }
}
