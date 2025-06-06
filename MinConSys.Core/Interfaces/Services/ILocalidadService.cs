﻿using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ILocalidadService
    {
        Task<List<LocalidadDto>> ListarLocalidadesAsync();
        Task<Localidad> ObtenerPorIdAsync(int id);
        Task<int> CrearLocalidadAsync(Localidad request);
        Task<bool> ActualizarLocalidadAsync(Localidad request);
        Task<bool> EliminarLocalidadAsync(int id, string nombreUsuario);
    }
}
