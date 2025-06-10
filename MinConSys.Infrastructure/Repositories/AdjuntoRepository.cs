using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class AdjuntoRepository : IAdjuntoRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public AdjuntoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<AdjuntoDto>> ObtenerAdjuntosPorEntidadAsync(string tablaReferencia, int idReferencia)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdAdjunto,
                    TablaReferencia,
                    IdReferencia,
                    TipoDocumento,
                    NombreArchivo,
                    UrlArchivo,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Adjuntos
                WHERE TablaReferencia = @tabla AND IdReferencia = @id AND Estado = 'A'";

                var adjuntos = await connection.QueryAsync<AdjuntoDto>(sql, new { tabla = tablaReferencia, id = idReferencia });
                return adjuntos.ToList();
            }
        }

        public async Task<int> AgregarAdjuntoAsync(Adjunto adjunto)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Adjuntos (
                        TablaReferencia,
                        IdReferencia,
                        TipoDocumento,
                        NombreArchivo,
                        UrlArchivo,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @TablaReferencia,
                        @IdReferencia,
                        @TipoDocumento,
                        @NombreArchivo,
                        @UrlArchivo,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, adjunto, transaction);
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

        public async Task<bool> EliminarAdjuntoAsync(int idAdjunto, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Adjuntos SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdAdjunto = @IdAdjunto AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdAdjunto = idAdjunto,
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
