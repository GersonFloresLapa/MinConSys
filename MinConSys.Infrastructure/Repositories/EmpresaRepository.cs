using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Response;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public EmpresaRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Empresa>> GetAllEmpresasAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdEmpresa,
                    RUC,
                    RazonSocial,
                    DireccionFiscal,
                    DireccionComercial,
                    Telefono,
                    Email,
                    EstadoContribuyente,
                    CondicionContribuyente,
                    PartidaElectronica,
                    ZonaPartidaElectronica,
                    Estado
                FROM Empresas
                WHERE Estado = 'A'";

                var empresas = await connection.QueryAsync<Empresa>(sql);
                return empresas.ToList();
            }
        }
        public async Task<Empresa> GetEmpresaByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdEmpresa,
                    RUC,
                    RazonSocial,
                    DireccionFiscal,
                    DireccionComercial,
                    Telefono,
                    Email,
                    EstadoContribuyente,
                    CondicionContribuyente,
                    PartidaElectronica,
                    ZonaPartidaElectronica,
                    Estado
                FROM Empresas
                WHERE IdEmpresa = @IdEmpresa AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Empresa>(sql, new { IdEmpresa = id });
            }
        }
        public async Task<List<Empresa>> GetEmpresaByTipoAsync(string tipo)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdEmpresa,
                    RUC,
                    RazonSocial
                FROM Empresas
                WHERE Estado = 'A'";

                var empresas = await connection.QueryAsync<Empresa>(sql);
                return empresas.ToList();
            }
        }
        public async Task<int> AddEmpresaAsync(Empresa empresa)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Empresas (
                        RUC,
                        RazonSocial,
                        DireccionFiscal,
                        DireccionComercial,
                        Telefono,
                        Email,
                        EstadoContribuyente,
                        CondicionContribuyente,
                        PartidaElectronica,
                        ZonaPartidaElectronica,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @RUC,
                        @RazonSocial,
                        @DireccionFiscal,
                        @DireccionComercial,
                        @Telefono,
                        @Email,
                        @EstadoContribuyente,
                        @CondicionContribuyente,
                        @PartidaElectronica,
                        @ZonaPartidaElectronica,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, empresa, transaction);
                    transaction.Commit();
                    return id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateEmpresaAsync(Empresa empresa)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Empresas SET
                        RUC = @RUC,
                        RazonSocial = @RazonSocial,
                        DireccionFiscal = @DireccionFiscal,
                        DireccionComercial = @DireccionComercial,
                        Telefono = @Telefono,
                        Email = @Email,
                        EstadoContribuyente = @EstadoContribuyente,
                        CondicionContribuyente = @CondicionContribuyente,
                        PartidaElectronica = @PartidaElectronica,
                        ZonaPartidaElectronica = @ZonaPartidaElectronica,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdEmpresa = @IdEmpresa AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, empresa, transaction);
                    transaction.Commit();
                    return affectedRows > 0;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteEmpresaAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Empresas SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdEmpresa = @IdEmpresa AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdEmpresa = id,
                        UsuarioModificacion = usuario
                    }, transaction);

                    transaction.Commit();
                    return affectedRows > 0;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
