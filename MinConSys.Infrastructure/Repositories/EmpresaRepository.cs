using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
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

        public async Task<List<EmpresaDto>> GetAllEmpresasAsync()
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
                                    EC.Valor EstadoContribuyente,
                                    CC.Valor CondicionContribuyente,
                                    PartidaElectronica,
                                    ZonaPartidaElectronica
                                FROM Empresas E
	                                INNER JOIN TablaGenerales EC on EC.Codigo = E.EstadoContribuyente and EC.TipoGeneral = 'EstadoContribuyente'
	                                INNER JOIN TablaGenerales CC on CC.Codigo = E.CondicionContribuyente and CC.TipoGeneral = 'CondicionContribuyente'
                                WHERE E.Estado = 'A'";

                var empresas = await connection.QueryAsync<EmpresaDto>(sql);
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
                    Ubigeo,
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
                    Empresas.IdEmpresa,
                    Empresas.RUC,
                    Empresas.RazonSocial,
                    Empresas.EstadoContribuyente
                FROM Empresas
                INNER JOIN TipoEmpresa on TipoEmpresa.IdEmpresa=Empresas.IdEmpresa
                WHERE CodigoTipoEmpresa=@CodigoTipoEmpresa and Empresas.Estado = 'A'";

                var empresas = await connection.QueryAsync<Empresa>(sql, new {CodigoTipoEmpresa = tipo});
                return empresas.ToList();
            }
        }
        public async Task<List<Empresa>> GetEmpresaGrupoAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    Empresas.IdEmpresa,
                    Empresas.RUC,
                    Empresas.RazonSocial
                FROM Empresas
                
                WHERE EmpresaGrupo = 1 and Empresas.Estado = 'A'";

                var empresas = await connection.QueryAsync<Empresa>(sql);
                return empresas.ToList();
            }
        }
        public async Task<int> AddEmpresaAsync(EmpresaRequest request)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // 1. Insertar Empresa
                    string sqlEmpresa = @"INSERT INTO Empresas (
                RUC,
                RazonSocial,
                DireccionFiscal,
                DireccionComercial,
                Ubigeo,
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
                @Ubigeo,
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

                    var idEmpresa = await connection.QuerySingleAsync<int>(sqlEmpresa, request.Empresa, transaction);

                    // 2. Insertar TipoEmpresas (si hay)
                    if (request.TipoEmpresas != null && request.TipoEmpresas.Count > 0)
                    {
                        string sqlTipoEmpresa = @"INSERT INTO TipoEmpresa (
                    CodigoTipoEmpresa,
                    IdEmpresa,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion
                ) VALUES (
                    @CodigoTipoEmpresa,
                    @IdEmpresa,
                    @Estado,
                    @UsuarioCreacion,
                    GETDATE()
                );";

                        foreach (var tipo in request.TipoEmpresas)
                        {
                            tipo.IdEmpresa = idEmpresa; // asignar el IdEmpresa
                            await connection.ExecuteAsync(sqlTipoEmpresa, tipo, transaction);
                        }
                    }

                    transaction.Commit();
                    return idEmpresa;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public async Task<bool> UpdateEmpresaAsync(EmpresaRequest request)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // 1. Actualizar Empresa
                    string sqlEmpresa = @"UPDATE Empresas SET
                RUC = @RUC,
                RazonSocial = @RazonSocial,
                DireccionFiscal = @DireccionFiscal,
                DireccionComercial = @DireccionComercial,
                Ubigeo = @Ubigeo,
                Telefono = @Telefono,
                Email = @Email,
                EstadoContribuyente = @EstadoContribuyente,
                CondicionContribuyente = @CondicionContribuyente,
                PartidaElectronica = @PartidaElectronica,
                ZonaPartidaElectronica = @ZonaPartidaElectronica,
                UsuarioModificacion = @UsuarioModificacion,
                FechaModificacion = GETDATE()
                    WHERE IdEmpresa = @IdEmpresa AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sqlEmpresa, request.Empresa, transaction);

                    if (affectedRows == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    // 2. Eliminar TipoEmpresa actuales
                    string deleteTipoEmpresa = @"DELETE FROM TipoEmpresa WHERE IdEmpresa = @IdEmpresa";
                    await connection.ExecuteAsync(deleteTipoEmpresa, new { request.Empresa.IdEmpresa }, transaction);

                    // 3. Insertar nuevos TipoEmpresa
                    if (request.TipoEmpresas != null && request.TipoEmpresas.Count > 0)
                    {
                        string insertTipoEmpresa = @"INSERT INTO TipoEmpresa (
                    CodigoTipoEmpresa,
                    IdEmpresa,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion
                ) VALUES (
                    @CodigoTipoEmpresa,
                    @IdEmpresa,
                    @Estado,
                    @UsuarioCreacion,
                    GETDATE()
                )";

                        foreach (var tipo in request.TipoEmpresas)
                        {
                            tipo.IdEmpresa = request.Empresa.IdEmpresa;
                            await connection.ExecuteAsync(insertTipoEmpresa, tipo, transaction);
                        }
                    }

                    transaction.Commit();
                    return true;
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
        public async Task<List<TipoEmpresa>> GetTipoEmpresaByEmpresaAsync(int idEmpresa)
        {
            using (var connection = await _connectionFactory.GetConnection()) 
            {
                string sql = @"
                        SELECT 
                            IdTipoEmpresa,
                            CodigoTipoEmpresa,
                            IdEmpresa,
                            Estado,
                            UsuarioCreacion,
                            FechaCreacion,
                            UsuarioModificacion,
                            FechaModificacion
                        FROM TipoEmpresa
                        WHERE IdEmpresa = @IdEmpresa AND Estado = 'A';";

                var result = await connection.QueryAsync<TipoEmpresa>(sql, new { IdEmpresa = idEmpresa });
                return result.AsList();
            }
        }
    }
}
