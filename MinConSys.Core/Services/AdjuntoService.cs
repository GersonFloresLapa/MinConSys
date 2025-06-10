using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class AdjuntoService : IAdjuntoService
    {
        private readonly IAdjuntoRepository _adjuntoRepository;

        public AdjuntoService(IAdjuntoRepository adjuntoRepository)
        {
            _adjuntoRepository = adjuntoRepository;
        }

        public async Task<List<AdjuntoDto>> ListarAdjuntosAsync(string tablaReferencia, int idReferencia)
        {
            return await _adjuntoRepository.ObtenerAdjuntosPorEntidadAsync(tablaReferencia, idReferencia);
        }

        public async Task<int> CrearAdjuntoAsync(Adjunto adjunto)
        {
            adjunto.Estado = 'A';
            adjunto.FechaCreacion = DateTime.Now;
            return await _adjuntoRepository.AgregarAdjuntoAsync(adjunto);
        }

        public async Task<bool> EliminarAdjuntoAsync(int idAdjunto, string usuario)
        {
            return await _adjuntoRepository.EliminarAdjuntoAsync(idAdjunto, usuario);
        }
    }
}
