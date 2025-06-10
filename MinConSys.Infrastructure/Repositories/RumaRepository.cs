using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class RumaRepository : IRumaRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public RumaRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddRumaCompletaAsync(RumaNuevaRequest request)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int idRuma = request.RumaYaExiste
                            ? request.Ruma.IdRuma
                            : await connection.QuerySingleAsync<int>(@"
                                    INSERT INTO Ruma (IdContrato, CodigoRuma, Descripcion, IdProducto, Estado, UsuarioCreacion, FechaCreacion)
                                    VALUES (@IdContrato, @CodigoRuma, @Descripcion, @IdProducto, @Estado, @UsuarioCreacion, GETDATE());
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                                ", request.Ruma, transaction);

                        request.Estado.IdRuma = idRuma;
                                    await connection.ExecuteAsync(@"
                                INSERT INTO RumaEstado (IdRuma, IdClase, IdProcedencia, IdDeposito, IdOrigen, IdDestino, TmhEstimado,
                                    FechaDesde, Estado, UsuarioCreacion, FechaCreacion)
                                VALUES (@IdRuma, @IdClase, @IdProcedencia, @IdDeposito, @IdOrigen, @IdDestino, @TmhEstimado,
                                    GETDATE(), @Estado, @UsuarioCreacion, GETDATE());
                            ", request.Estado, transaction);

                        if (request.EsMineral && !request.RumaYaExiste)
                        {

                            request.Procesos.IdRuma = idRuma;
                                await connection.ExecuteAsync(@"
                            INSERT INTO RumaEstadoProceso (IdRuma, EstadoProceso, FechaCambio, UsuarioCambio, Observacion, Estado, UsuarioCreacion, FechaCreacion)
                            VALUES (@IdRuma, @EstadoProceso, @FechaCambio, @UsuarioCambio, @Observacion, @Estado, @UsuarioCreacion, GETDATE());
                        ", request.Procesos, transaction);
                            
                        }

                        transaction.Commit();
                        return idRuma;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task<Ruma> GetRumaByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            { return await connection.QueryFirstOrDefaultAsync<Ruma>("SELECT * FROM Ruma WHERE IdRuma = @id AND Estado = 'A'", new { id }); }
        }
        public async Task<List<Ruma>> GetAllRumasAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var result = await connection.QueryAsync<Ruma>("SELECT * FROM Ruma WHERE Estado = 'A'");
                return result.ToList();
            }
        }
        public async Task<List<RumaDto>> GetAllRumasByClaseAsync(string codigoClase)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var result = await connection.QueryAsync<RumaDto>("SELECT * FROM ViewRuma WHERE CodigoClase = @CodigoClase", new { CodigoClase = codigoClase });
                return result.ToList();
            }
        }
        public async Task<bool> UpdateRumaAsync(Ruma ruma)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE Ruma SET CodigoRuma = @CodigoRuma, Descripcion = @Descripcion, IdProducto = @IdProducto, UsuarioModificacion = @UsuarioModificacion, FechaModificacion = GETDATE() WHERE IdRuma = @IdRuma AND Estado = 'A'";
                var rows = await connection.ExecuteAsync(sql, ruma);
                return rows > 0;
            }
        }
        public async Task<bool> DeleteRumaAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE Ruma SET Estado = 'I', UsuarioModificacion = @UsuarioModificacion, FechaModificacion = GETDATE() WHERE IdRuma = @IdRuma AND Estado = 'A'";
                var rows = await connection.ExecuteAsync(sql, new { IdRuma = id, UsuarioModificacion = usuario });
                return rows > 0;
            }
        }
        public async Task<List<RumaEstado>> GetEstadosByRumaAsync(int idRuma)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var result = await connection.QueryAsync<RumaEstado>("SELECT * FROM RumaEstado WHERE IdRuma = @idRuma AND Estado = 'A'", new { idRuma });
                return result.ToList();
            }
        }
        public async Task<RumaEstado> GetEstadosByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<RumaEstado>("SELECT * FROM RumaEstado WHERE IdRumaEstado = @id AND Estado = 'A'", new { id });

            }
        }
        public async Task<bool> AddEstadoRumaAsync(RumaEstado estado)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"INSERT INTO RumaEstado (IdRuma, IdClase, IdProcedencia, IdDeposito, IdOrigen, IdDestino, TmhEstimado, FechaDesde, Estado, UsuarioCreacion, FechaCreacion) VALUES (@IdRuma, @IdClase, @IdProcedencia, @IdDeposito, @IdOrigen, @IdDestino, @TmhEstimado, GETDATE(), @Estado, @UsuarioCreacion, GETDATE())";
                var rows = await connection.ExecuteAsync(sql, estado);
                return rows > 0;
            }
        }
        public async Task<bool> UpdateEstadoRumaAsync(RumaEstado estado)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE RumaEstado SET IdClase = @IdClase, IdProcedencia = @IdProcedencia, IdDeposito = @IdDeposito, IdOrigen = @IdOrigen, IdDestino = @IdDestino, TmhEstimado = @TmhEstimado, FechaHasta = @FechaHasta, UsuarioModificacion = @UsuarioModificacion, FechaModificacion = GETDATE() WHERE IdRumaEstado = @IdRumaEstado AND Estado = 'A'";
                var rows = await connection.ExecuteAsync(sql, estado);
                return rows > 0;
            }
        }
        public async Task<bool> DeleteEstadoRumaAsync(int idEstado, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE RumaEstado SET Estado = 'I', UsuarioModificacion = @usuario, FechaModificacion = GETDATE() WHERE IdRumaEstado = @idEstado AND Estado = 'A'";
                var rows = await connection.ExecuteAsync(sql, new { idEstado, usuario });
                return rows > 0;
            }
        }
        public async Task<List<RumaEstadoProceso>> GetEstadosProcesoByRumaAsync(int idRuma)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var result = await connection.QueryAsync<RumaEstadoProceso>("SELECT * FROM RumaEstadoProceso WHERE IdRuma = @idRuma AND Estado = 'A'", new { idRuma });
                return result.ToList();
            }
        }
        public async Task<bool> AddEstadoProcesoAsync(RumaEstadoProceso proceso)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"INSERT INTO RumaEstadoProceso (IdRuma, EstadoProceso, FechaCambio, UsuarioCambio, Observacion, Estado, UsuarioCreacion, FechaCreacion) VALUES (@IdRuma, @EstadoProceso, @FechaCambio, @UsuarioCambio, @Observacion, @Estado, @UsuarioCreacion, GETDATE())";
                var rows = await connection.ExecuteAsync(sql, proceso);
                return rows > 0;
            }
        }
        public async Task<bool> UpdateProcesoRumaAsync(RumaEstadoProceso proceso)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE RumaEstadoProceso SET EstadoProceso = @EstadoProceso, FechaCambio = @FechaCambio, UsuarioCambio = @UsuarioCambio, Observacion = @Observacion, UsuarioModificacion = @UsuarioModificacion, FechaModificacion = GETDATE() WHERE IdEstadoProceso = @IdEstadoProceso AND Estado = 'A'";
                var rows = await connection.ExecuteAsync(sql, proceso);
                return rows > 0;
            }
        }
        public async Task<bool> DeleteProcesoRumaAsync(int idProceso, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE RumaEstadoProceso SET Estado = 'I', UsuarioModificacion = @usuario, FechaModificacion = GETDATE() WHERE IdEstadoProceso = @idProceso AND Estado = 'A'";
                var rows = await connection.ExecuteAsync(sql, new { idProceso, usuario });
                return rows > 0;
            }  
        }
        public async Task<string> GetCodigoRuma(int idEmpresa, int idProducto)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
               
                var parameters = new { IdEmpresa = idEmpresa, IdProducto = idProducto };

                var codigoEmpresa = await connection.ExecuteScalarAsync<string>(
                    "usp_ObtenerCodigoRuma",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return codigoEmpresa;
            }
        }
        public async Task<int> UpdateRumaYEstadoAsync(RumaNuevaRequest request)
        {
            if (!request.RumaYaExiste)
                throw new InvalidOperationException("La ruma no existe. Este método es solo para actualizaciones.");

            using (var connection = await _connectionFactory.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int idRuma = request.Ruma.IdRuma;

                        // Update a Ruma
                        await connection.ExecuteAsync(@"
                    UPDATE Ruma
                    SET 
                        IdContrato = @IdContrato,
                        CodigoRuma = @CodigoRuma,
                        Descripcion = @Descripcion,
                        IdProducto = @IdProducto,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdRuma = @IdRuma;
                ", request.Ruma, transaction);

                        // Update a RumaEstado
                        request.Estado.IdRuma = idRuma;
                        await connection.ExecuteAsync(@"
                    UPDATE RumaEstado
                    SET 
                        IdClase = @IdClase,
                        IdProcedencia = @IdProcedencia,
                        IdDeposito = @IdDeposito,
                        IdOrigen = @IdOrigen,
                        IdDestino = @IdDestino,
                        TmhEstimado = @TmhEstimado,
                        FechaDesde = GETDATE(),
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdRuma = @IdRuma;
                ", request.Estado, transaction);

                        transaction.Commit();
                        return idRuma;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task<int> UpdateRumaAddEstadoAsync(RumaNuevaRequest request)
        {
            if (!request.RumaYaExiste)
                throw new InvalidOperationException("La ruma no existe. Este método es solo para actualizaciones.");

            using (var connection = await _connectionFactory.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int idRuma = request.Ruma.IdRuma;

                        // Actualizar Ruma
                        await connection.ExecuteAsync(@"
                    UPDATE Ruma
                    SET 
                        IdContrato = @IdContrato,
                        CodigoRuma = @CodigoRuma,
                        Descripcion = @Descripcion,
                        IdProducto = @IdProducto,
                        Estado = @Estado,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdRuma = @IdRuma;
                ", request.Ruma, transaction);

                        // Insertar nuevo registro en RumaEstado (histórico)
                        request.Estado.IdRuma = idRuma;
                        await connection.ExecuteAsync(@"
                    INSERT INTO RumaEstado (
                        IdRuma, IdClase, IdProcedencia, IdDeposito, IdOrigen, IdDestino, TmhEstimado,
                        FechaDesde, Estado, UsuarioCreacion, FechaCreacion
                    )
                    VALUES (
                        @IdRuma, @IdClase, @IdProcedencia, @IdDeposito, @IdOrigen, @IdDestino, @TmhEstimado,
                        GETDATE(), @Estado, @UsuarioCreacion, GETDATE()
                    );
                ", request.Estado, transaction);

                        if (request.EsMineral)
                        {

                            request.Procesos.IdRuma = idRuma;
                            await connection.ExecuteAsync(@"
                            INSERT INTO RumaEstadoProceso (IdRuma, EstadoProceso, FechaCambio, UsuarioCambio, Observacion, Estado, UsuarioCreacion, FechaCreacion)
                            VALUES (@IdRuma, @EstadoProceso, @FechaCambio, @UsuarioCambio, @Observacion, @Estado, @UsuarioCreacion, GETDATE());
                        ", request.Procesos, transaction);

                        }

                        transaction.Commit();
                        return idRuma;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task<List<RumaTicketDto>> GetAllRumasTicketAsync(int idRumaEstado)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var result = await connection.QueryAsync<RumaTicketDto>
                ("SELECT * FROM ViewRumaTicket WHERE IdRumaEstado = @IdRumaEstado", new { IdRumaEstado = idRumaEstado });
                return result.ToList();
            }
        }
        public async Task AddTicketRumasAsync(List<RumaTicket> lista)
        {
            if (lista == null || lista.Count == 0)
                return;

            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO RumaTicket (
                                IdTicket,
                                IdRuma,
                                IdRumaEstado,
                                Estado,
                                UsuarioCreacion,
                                FechaCreacion,
                                UsuarioModificacion,
                                FechaModificacion
                            ) VALUES (
                                @IdTicket,
                                @IdRuma,
                                @IdRumaEstado,
                                @Estado,
                                @UsuarioCreacion,
                                @FechaCreacion,
                                @UsuarioModificacion,
                                @FechaModificacion
                            );";

                    await connection.ExecuteAsync(sql, lista, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task DeleteTicketRumaAsync(int idRumaTicket)
        {
            var connection = await _connectionFactory.GetConnection();
            try
            {
                const string sql = "DELETE FROM RumaTicket WHERE IdRumaTicket = @IdRumaTicket";
                await connection.ExecuteAsync(sql, new { IdRumaTicket = idRumaTicket });
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
