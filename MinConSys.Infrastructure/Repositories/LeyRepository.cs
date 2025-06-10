using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Request;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class LeyRepository : ILeyRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public LeyRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Ley>> GetAllLeyAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdLey,
                    IdRumaEstado,
                    IdLaboratorio,
                    TipoLey,
                    Producto,
                    Contenido,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Ley
                WHERE Estado = 'A'";

                var leyes = await connection.QueryAsync<Ley>(sql);
                return leyes.ToList();
            }
        }

        public async Task<Ley> GetLeyByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdLey,
                    IdRumaEstado,
                    IdLaboratorio,
                    TipoLey,
                    Producto,
                    Contenido,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Ley
                WHERE IdLey = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Ley>(sql, new { Id = id });
            }
        }

        public async Task<int> AddLeyAsync(LeyRequest leyNueva)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Ley (
                        IdRumaEstado,
                        IdLaboratorio,
                        TipoLey,
                        Producto,
                        Contenido,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @IdRumaEstado,
                        @IdLaboratorio,
                        @TipoLey,
                        @Producto,
                        @Contenido,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, leyNueva.Ley, transaction);

                    if (leyNueva.EsMineral && leyNueva.Tickets.Count > 0)
                    {
                        string sqlTicket = @"INSERT INTO Ley (
                                        IdLey,
                                        IdTicket
                                    ) VALUES (
                                        @IdLey,
                                        @IdTicket
                                    );
                                    SELECT CAST(SCOPE_IDENTITY() as int);";

                        foreach (var ticket in leyNueva.Tickets)
                        {
                           await connection.QuerySingleAsync<int>(sqlTicket, ticket, transaction);
                        }
                    }

                    if (leyNueva.LeyContenidos.Count > 0)
                    {
                        string sqlContenido = @"INSERT INTO LeyContenido (
                                        IdLey,
                                        Elemento,
                                        Contenido
                                    ) VALUES (
                                        @IdLey,
                                        @Elemento,
                                        @Contenido
                                    );
                                    SELECT CAST(SCOPE_IDENTITY() as int);";

                        foreach (var contenido in leyNueva.LeyContenidos)
                        {
                            await connection.QuerySingleAsync<int>(sqlContenido, contenido, transaction);
                        }
                    }

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

        public async Task<bool> UpdateLeyAsync(LeyRequest leyUpdate)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Ley SET
                        IdRumaEstado = @IdRumaEstado,
                        IdLaboratorio = @IdLaboratorio,
                        TipoLey = @TipoLey,
                        Producto = @Producto,
                        Contenido = @Contenido,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdLey = @IdLey AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, leyUpdate.Ley, transaction);

                    if (leyUpdate.EsMineral && leyUpdate.Tickets.Count > 0)
                    {
                        string sqlTicketInsert = @"INSERT INTO LeyTicket (
                                        IdLey,
                                        IdTicket
                                    ) VALUES (
                                        @IdLey,
                                        @IdTicket
                                    );
                                    SELECT CAST(SCOPE_IDENTITY() as int);";

                        string sqlTicketDelete = @" DELETE LeyTicket
                                                    WHERE IdLey = @IdLey;";

                        await connection.ExecuteAsync(sqlTicketDelete, new { IdLey = leyUpdate.Ley.IdLey}, transaction);

                        foreach (var ticket in leyUpdate.Tickets)
                        {
                            
                             await connection.QuerySingleAsync<int>(sqlTicketInsert, ticket, transaction);
                            
                           
                        }

                    }

                    if(leyUpdate.LeyContenidos.Count > 0)
                    {
                        string sqlContenido = @"INSERT INTO LeyContenido (
                                        IdLey,
                                        Elemento,
                                        Contenido
                                    ) VALUES (
                                        @IdLey,
                                        @Elemento,
                                        @Contenido
                                    );
                                    SELECT CAST(SCOPE_IDENTITY() as int);";

                        string sqlContenidoDelete = @"DELETE LeyContenido
                                                    WHERE IdLey = @IdLey;";

                        await connection.ExecuteAsync(sqlContenidoDelete, new { IdLey = leyUpdate.Ley.IdLey }, transaction);

                        foreach (var contenido in leyUpdate.LeyContenidos)
                        {
                            await connection.QuerySingleAsync<int>(sqlContenido, contenido, transaction);
                        }
                    }

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

        public async Task<bool> DeleteLeyAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Ley SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdLey = @IdLey AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdLey = id,
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
        public async Task<int> AddLeyTicketAsync(Ley ley)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Ley (
                        IdLey,
                        IdTicket
                    ) VALUES (
                        @IdLey,
                        @IdTicket
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, ley, transaction);
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
    }
}
