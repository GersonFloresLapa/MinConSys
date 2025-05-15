using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            return await _usuarioRepository.GetAllUsuariosAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return await _usuarioRepository.GetUsuarioByIdAsync(id);
        }

        public async Task<int> CrearUsuarioAsync(Usuario usuario)
        {
            usuario.Estado = "A";
            usuario.FechaCreacion = DateTime.Now;
            return await _usuarioRepository.AddUsuarioAsync(usuario);
        }

        public async Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            usuario.FechaModificacion = DateTime.Now;
            return await _usuarioRepository.UpdateUsuarioAsync(usuario);
        }

        public async Task<bool> EliminarUsuarioAsync(int id, string usuarioNombre)
        {
            return await _usuarioRepository.DeleteUsuarioAsync(id, usuarioNombre);
        }
    }

}
