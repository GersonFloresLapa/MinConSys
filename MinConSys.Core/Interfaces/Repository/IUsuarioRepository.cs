using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<int> AddUsuarioAsync(Usuario usuario);
        Task<bool> UpdateUsuarioAsync(Usuario usuario);
        Task<bool> DeleteUsuarioAsync(int id, string usuario);
    }
}
