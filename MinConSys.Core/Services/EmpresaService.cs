using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
       
        public async Task<List<EmpresaDto>> ListarEmpresasAsync()
        {
            return await _empresaRepository.GetAllEmpresasAsync();
        }

        public async Task<Empresa> ObtenerPorIdAsync(int id)
        {
            return await _empresaRepository.GetEmpresaByIdAsync(id);
        }

        public async Task<int> CrearEmpresaAsync(EmpresaRequest empresa)
        {
            empresa.Empresa.FechaCreacion = DateTime.Now;
            empresa.Empresa.Estado = "A";
            return await _empresaRepository.AddEmpresaAsync(empresa);
        }

        public async Task<bool> ActualizarEmpresaAsync(EmpresaRequest empresa)
        {
            empresa.Empresa.FechaModificacion = DateTime.Now; ;
            return await _empresaRepository.UpdateEmpresaAsync(empresa);
        }

        public async Task<bool> EliminarEmpresaAsync(int id, string usuario)
        {
            return await _empresaRepository.DeleteEmpresaAsync(id, usuario);
        }

        public async Task<List<ComboItem>> ListarEmpresasTiposAsync(string tipo)
        {
            var empresas = await _empresaRepository.GetEmpresaByTipoAsync(tipo); // Debes implementar esto
            var lista = empresas.Select(e => new ComboItem
            {
                Id = e.IdEmpresa,
                Descripcion = $"{e.RUC} - {e.RazonSocial}"
            }).ToList();

            return lista;
        }
        public async Task<List<TipoEmpresa>> ObtenerTipoEmpresaPorEmpresaAsync(int idEmpresa)
        {
            return await _empresaRepository.GetTipoEmpresaByEmpresaAsync(idEmpresa);
        }

        public async Task<List<ComboItem>> ListarEmpresasGrupoAsync()
        {
            var empresas = await _empresaRepository.GetEmpresaGrupoAsync(); // Debes implementar esto
            var lista = empresas.Select(e => new ComboItem
            {
                Id = e.IdEmpresa,
                Descripcion = $"{e.RUC} - {e.RazonSocial}"
            }).ToList();

            return lista;
        }
    }
}
