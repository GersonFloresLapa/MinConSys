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
    }
}
