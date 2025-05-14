using Dapper;
using MinConSys.Core.Interfaces.Services;
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
    public class ContratoRepository : IContratoRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public ContratoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<ContratoDto>> GetAllContratosAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT	C.IdContrato,
		                                C.CodigoContrato,
		                                C.TipoContrato,
		                                C.Clase,
		                                C.Producto,
		                                E.RazonSocial Empresa,
		                                P.RazonSocial Proveedor,
		                                C.FechaInicio,
		                                C.FechaFin
                                FROM Contratos C
                                INNER JOIN Empresas E ON C.IdEmpresa = E.IdEmpresa
                                INNER JOIN Empresas P ON C.IdProveedor = P.IdEmpresa
                                WHERE  C.Estado = 'A'";
                var contratos = await connection.QueryAsync<ContratoDto>(sql);
                return contratos.ToList();
            }
        }

        public async Task<Contrato> GetContratoByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT * FROM Contratos WHERE IdContrato = @IdContrato AND Estado = 'A'";
                return await connection.QueryFirstOrDefaultAsync<Contrato>(sql, new { IdContrato = id });
            }
        }

        public async Task<int> AddContratoAsync(Contrato contrato)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Contratos (
                        CodigoContrato,
                        IdEmpresa,
                        IdProveedor,
                        FechaInicio,
                        FechaFin,
                        TipoContrato,
                        Clase,
                        Producto,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @CodigoContrato,
                        @IdEmpresa,
                        @IdProveedor,
                        @FechaInicio,
                        @FechaFin,
                        @TipoContrato,
                        @Clase,
                        @Producto,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var id = await connection.QuerySingleAsync<int>(sql, contrato, transaction);
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

        public async Task<bool> UpdateContratoAsync(Contrato contrato)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Contratos SET
                        CodigoContrato = @CodigoContrato,
                        IdEmpresa = @IdEmpresa,
                        IdProveedor = @IdProveedor,
                        FechaInicio = @FechaInicio,
                        FechaFin = @FechaFin,
                        TipoContrato = @TipoContrato,
                        Clase = @Clase,
                        Producto = @Producto,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdContrato = @IdContrato AND Estado = 'A'";

                    var affected = await connection.ExecuteAsync(sql, contrato, transaction);
                    transaction.Commit();
                    return affected > 0;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteContratoAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Contratos SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdContrato = @IdContrato AND Estado = 'A'";

                    var affected = await connection.ExecuteAsync(sql, new
                    {
                        IdContrato = id,
                        UsuarioModificacion = usuario
                    }, transaction);

                    transaction.Commit();
                    return affected > 0;
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
