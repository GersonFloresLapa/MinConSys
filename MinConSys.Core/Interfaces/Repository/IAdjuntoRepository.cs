using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IAdjuntoRepository
    {
        Task<List<AdjuntoDto>> ObtenerAdjuntosPorEntidadAsync(string tablaReferencia, int idReferencia);
        Task<int> AgregarAdjuntoAsync(Adjunto adjunto);
        Task<bool> EliminarAdjuntoAsync(int idAdjunto, string usuario);
        // Opcional: puedes agregar más métodos si necesitas actualizar o buscar adjuntos específicos
    }
}
