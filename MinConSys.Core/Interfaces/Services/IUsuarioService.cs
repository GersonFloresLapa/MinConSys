using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ListarUsuariosAsync();
        Task<Usuario> ObtenerPorIdAsync(int id);
        Task<int> CrearUsuarioAsync(Usuario usuario);
        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
        Task<bool> EliminarUsuarioAsync(int id, string usuarioNombre);
    }
}
