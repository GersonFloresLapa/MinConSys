using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinConSys.Core.Models.Common;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IClaseService
    {
        Task<List<Clase>> ListarClasesAsync();
        Task<Clase> ObtenerPorIdAsync(int id);
        Task<int> CrearClaseAsync(Clase clase);
        Task<bool> ActualizarClaseAsync(Clase clase);
        Task<bool> EliminarClaseAsync(int id, string usuario);
        Task<List<ComboItem>> ListarClasesCboAsync();
    }
}
