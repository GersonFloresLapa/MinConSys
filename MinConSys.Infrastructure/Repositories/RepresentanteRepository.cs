using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class RepresentanteRepository : IRepresentanteRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public RepresentanteRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Representante>> GetAllRepresentantesAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdRepresentante,
                    IdEmpresa,
                    IdPersona,
                    FechaInicio,
                    FechaFin,
                    Cargo,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Representantes
                WHERE Estado = 'A'";

                var data = await connection.QueryAsync<Representante>(sql);
                return data.ToList();
            }
        }

        public async Task<Representante> GetRepresentanteByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                                    IdRepresentante,
                                    IdEmpresa,
                                    IdPersona,
                                    FechaInicio,
                                    FechaFin,
                                    Cargo,
                                    Estado,
                                    UsuarioCreacion,
                                    FechaCreacion,
                                    UsuarioModificacion,
                                    FechaModificacion
                                FROM Representantes' 	
                                WHERE IdRepresentante = @Id AND R.Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Representante>(sql, new { Id = id });
            }
        }

        public async Task<int> AddRepresentanteAsync(Representante representante)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Representantes (
                        IdEmpresa,
                        IdPersona,
                        FechaInicio,
                        FechaFin,
                        Cargo,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @IdEmpresa,
                        @IdPersona,
                        @FechaInicio,
                        @FechaFin,
                        @Cargo,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, representante, transaction);
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

        public async Task<bool> UpdateRepresentanteAsync(Representante representante)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Representantes SET
                        IdEmpresa = @IdEmpresa,
                        IdPersona = @IdPersona,
                        FechaInicio = @FechaInicio,
                        FechaFin = @FechaFin,
                        Cargo = @Cargo,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdRepresentante = @IdRepresentante AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, representante, transaction);
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

        public async Task<bool> DeleteRepresentanteAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Representantes SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdRepresentante = @IdRepresentante AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdRepresentante = id,
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

        public async Task<List<RepresentanteDto>> GetRepresentanteByIdEmpresaAsync(int idEmpesa)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                                    IdRepresentante,
					                TG.Valor Cargo,
					                P.NumeroDocumento+' '+P.Nombres+' '+P.ApellidoPaterno Nombres
                                FROM Representantes R INNER JOIN
					                Personas P ON R.IdPersona = P.IdPersona INNER JOIN
					                TablaGenerales TG ON TG.Codigo = R.Cargo and TG.TipoGeneral = 'TIPOREPRESENTANTE' 	
                                WHERE R.IdEmpresa = @IdEmpresa AND R.Estado = 'A'";

                var data = await connection.QueryAsync<RepresentanteDto>(sql, new { IdEmpresa = idEmpesa });
                return data.ToList();

                
            }
        }
    }
}
