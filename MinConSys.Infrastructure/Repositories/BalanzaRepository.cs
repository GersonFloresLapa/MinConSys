using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models.Base;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class BalanzaRepository : IBalanzaRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public BalanzaRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Balanza>> GetAllBalanzasAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdBalanza,
                Nombre,
                Descripcion,
                Direccion,
                Ubigeo,
                Unidad,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
                FROM Balanza
                WHERE Estado = 'A'";

                var balanzas = await connection.QueryAsync<Balanza>(sql);
                return balanzas.ToList();
            }
        }

        public async Task<Balanza> GetBalanzaByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdBalanza,
                Nombre,
                Descripcion,
                Direccion,
                Ubigeo,
                Unidad,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
            FROM Balanza
            WHERE IdBalanza = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Balanza>(sql, new { Id = id });
            }
        }

        public async Task<int> AddBalanzaAsync(Balanza balanza)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Balanza (
                    Nombre,
                    Descripcion,
                    Direccion,
                    Ubigeo,
                    Unidad,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion
                ) VALUES (
                    @Nombre,
                    @Descripcion,
                    @Direccion,
                    @Ubigeo,
                    @Unidad,
                    @Estado,
                    @UsuarioCreacion,
                    GETDATE()
                );
                SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, balanza, transaction);
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

        public async Task<bool> UpdateBalanzaAsync(Balanza balanza)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Balanza SET
                    Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    Direccion = @Direccion,
                    Ubigeo = @Ubigeo,
                    Unidad = @Unidad,
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                WHERE IdBalanza = @IdBalanza AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, balanza, transaction);
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

        public async Task<bool> DeleteBalanzaAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Balanza SET
                    Estado = 'I',
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                    WHERE IdBalanza = @IdBalanza AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdBalanza = id,
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

        public async Task<List<Balanza>> GetBalanzaCboAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdBalanza,
                    Nombre
                FROM Balanza
                WHERE Estado = 'A'";

                var balanzas = await connection.QueryAsync<Balanza>(sql);
                return balanzas.ToList();
            }
        }

    }

}
