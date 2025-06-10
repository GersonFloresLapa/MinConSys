using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinConSys.Core.Models.Common;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> ListarProductosAsync();
        Task<Producto> ObtenerPorIdAsync(int id);
        Task<int> CrearProductoAsync(Producto producto);
        Task<bool> ActualizarProductoAsync(Producto producto);
        Task<bool> EliminarProductoAsync(int id, string usuario);
        Task<List<ComboItem>> ListarProductosCboAsync();
    }
}
