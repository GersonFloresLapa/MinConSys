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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<Producto>> ListarProductosAsync()
        {
            return await _productoRepository.GetAllProductosAsync();
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            return await _productoRepository.GetProductoByIdAsync(id);
        }

        public async Task<int> CrearProductoAsync(Producto producto)
        {
            producto.FechaCreacion = DateTime.Now;
            producto.Estado = "A";
            return await _productoRepository.AddProductoAsync(producto);
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            producto.FechaModificacion = DateTime.Now;
            return await _productoRepository.UpdateProductoAsync(producto);
        }

        public async Task<bool> EliminarProductoAsync(int id, string usuario)
        {
            return await _productoRepository.DeleteProductoAsync(id, usuario);
        }
    }
}
