using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<int> AddProductoAsync(Producto producto);
        Task<bool> UpdateProductoAsync(Producto producto);
        Task<bool> DeleteProductoAsync(int id, string usuario);
    }
}
