using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IMenuRepository
    {
        Task<List<MenuDto>> ObtenerMenuPorUsuario(string nombreUsuario);
        Task<List<Menu>> GetAllMenusAsync();
        Task<Menu> GetMenuByIdAsync(int id);
        Task<int> AddMenuAsync(Menu menu);
        Task<bool> UpdateMenuAsync(Menu menu);
        Task<bool> DeleteMenuAsync(int id, string usuario);
    }
}
