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
    public class RolRepository : IRolRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public RolRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Rol>> GetAllRolesAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdRol,
                NombreRol,
                Descripcion,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
            FROM Rol
            WHERE Estado = 'A'";

                var roles = await connection.QueryAsync<Rol>(sql);
                return roles.ToList();
            }
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdRol,
                NombreRol,
                Descripcion,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
            FROM Rol
            WHERE IdRol = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Rol>(sql, new { Id = id });
            }
        }

        public async Task<int> AddRolAsync(Rol rol)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Rol (
                    NombreRol,
                    Descripcion,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion
                ) VALUES (
                    @NombreRol,
                    @Descripcion,
                    @Estado,
                    @UsuarioCreacion,
                    GETDATE()
                );
                SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, rol, transaction);
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

        public async Task<bool> UpdateRolAsync(Rol rol)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Rol SET
                    NombreRol = @NombreRol,
                    Descripcion = @Descripcion,
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                WHERE IdRol = @IdRol AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, rol, transaction);
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

        public async Task<bool> DeleteRolAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Rol SET
                    Estado = 'I',
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                WHERE IdRol = @IdRol AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdRol = id,
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
