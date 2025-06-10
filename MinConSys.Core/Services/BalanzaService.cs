﻿using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class BalanzaService : IBalanzaService
    {
        private readonly IBalanzaRepository _balanzaRepository;

        public BalanzaService(IBalanzaRepository balanzaRepository)
        {
            _balanzaRepository = balanzaRepository;
        }

        public async Task<List<Balanza>> ListarBalanzasAsync()
        {
            return await _balanzaRepository.GetAllBalanzasAsync();
        }

        public async Task<Balanza> ObtenerPorIdAsync(int id)
        {
            return await _balanzaRepository.GetBalanzaByIdAsync(id);
        }

        public async Task<int> CrearBalanzaAsync(Balanza balanza)
        {
            balanza.Estado = "A";
            balanza.FechaCreacion = DateTime.Now;
            return await _balanzaRepository.AddBalanzaAsync(balanza);
        }

        public async Task<bool> ActualizarBalanzaAsync(Balanza balanza)
        {
            balanza.FechaModificacion = DateTime.Now;
            return await _balanzaRepository.UpdateBalanzaAsync(balanza);
        }

        public async Task<bool> EliminarBalanzaAsync(int id, string usuario)
        {
            return await _balanzaRepository.DeleteBalanzaAsync(id, usuario);
        }

        public async Task<List<ComboItem>> ListarBalanzaCboAsync()
        {
            var balanzas = await _balanzaRepository.GetBalanzaCboAsync(); // Debes implementar esto
            var lista = balanzas.Select(e => new ComboItem
            {
                Id = e.IdBalanza,
                Descripcion =e.Nombre
            }).ToList();

            return lista;
        }


    }

}
