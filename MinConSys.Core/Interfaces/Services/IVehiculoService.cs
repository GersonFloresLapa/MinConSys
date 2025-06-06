﻿using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IVehiculoService
    {
        Task<List<Vehiculo>> ListarVehiculosAsync();
        Task<Vehiculo> ObtenerPorIdAsync(int id);
        Task<int> CrearVehiculoAsync(Vehiculo vehiculo);
        Task<bool> ActualizarVehiculoAsync(Vehiculo vehiculo);
        Task<bool> EliminarVehiculoAsync(int id, string usuario);
    }
}
