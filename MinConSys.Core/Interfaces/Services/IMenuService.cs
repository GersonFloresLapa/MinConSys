using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IMenuService
    {
        Task<List<MenuDto>> ObtenerMenuPermitido(string nombreUsuario);
        Task<List<Menu>> ListarMenusAsync();
        Task<Menu> ObtenerPorIdAsync(int id);
        Task<int> CrearMenuAsync(Menu menu);
        Task<bool> ActualizarMenuAsync(Menu menu);
        Task<bool> EliminarMenuAsync(int id, string usuario);
    }
}
