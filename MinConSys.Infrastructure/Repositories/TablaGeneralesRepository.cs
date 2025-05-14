using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class TablaGeneralesRepository : ITablaGeneralesRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public TablaGeneralesRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<TablaGenerales>> GetAllTablaGeneralesAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                                IdGeneral,
                                TipoGeneral,
                                Codigo,
                                Valor,
                                Descripcion,
                                Estado,
                                UsuarioCreacion,
                                FechaCreacion,
                                UsuarioModificacion,
                                FechaModificacion
                               FROM TablaGenerales
                               WHERE Estado = 'A'";

                var result = await connection.QueryAsync<TablaGenerales>(sql);
                return result.ToList();
            }
        }

        public async Task<TablaGenerales> GetTablaGeneralesByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                                IdGeneral,
                                TipoGeneral,
                                Codigo,
                                Valor,
                                Descripcion,
                                Estado,
                                UsuarioCreacion,
                                FechaCreacion,
                                UsuarioModificacion,
                                FechaModificacion
                               FROM TablaGenerales
                               WHERE IdGeneral = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<TablaGenerales>(sql, new { Id = id });
            }
        }

        public async Task<int> AddTablaGeneralesAsync(TablaGenerales general)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO TablaGenerales (
                                        TipoGeneral,
                                        Codigo,
                                        Valor,
                                        Descripcion,
                                        Estado,
                                        UsuarioCreacion,
                                        FechaCreacion
                                   ) VALUES (
                                        @TipoGeneral,
                                        @Codigo,
                                        @Valor,
                                        @Descripcion,
                                        @Estado,
                                        @UsuarioCreacion,
                                        GETDATE()
                                   );
                                   SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var id = await connection.QuerySingleAsync<int>(sql, general, transaction);
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

        public async Task<bool> UpdateTablaGeneralesAsync(TablaGenerales general)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE TablaGenerales SET
                                    TipoGeneral = @TipoGeneral,
                                    Codigo = @Codigo,
                                    Valor = @Valor,
                                    Descripcion = @Descripcion,
                                    UsuarioModificacion = @UsuarioModificacion,
                                    FechaModificacion = GETDATE()
                                   WHERE IdGeneral = @IdGeneral AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, general, transaction);
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

        public async Task<bool> DeleteTablaGeneralesAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE TablaGenerales SET
                                    Estado = 'I',
                                    UsuarioModificacion = @UsuarioModificacion,
                                    FechaModificacion = GETDATE()
                                   WHERE IdGeneral = @IdGeneral AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdGeneral = id,
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
