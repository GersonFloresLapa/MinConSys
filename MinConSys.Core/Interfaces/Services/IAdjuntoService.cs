using MinConSys.Core.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IAdjuntoService
    {
        Task<List<Adjunto>> ListarAdjuntosAsync(string tablaReferencia, int idReferencia);
        Task<int> CrearAdjuntoAsync(Adjunto adjunto);
        Task<bool> EliminarAdjuntoAsync(int idAdjunto, string usuario);
    }
}
