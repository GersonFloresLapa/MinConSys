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
    public class RolMenuPermisoRepository : IRolMenuPermisoRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public RolMenuPermisoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<RolMenuPermiso>> GetPermisosByRolAsync(int idRol)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdRol,
                IdMenu,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
                FROM RolMenuPermiso
                WHERE IdRol = @IdRol AND Estado = 'A'";

                var result = await connection.QueryAsync<RolMenuPermiso>(sql, new { IdRol = idRol });
                return result.ToList();
            }
        }

        public async Task<bool> AddOrUpdatePermisosAsync(List<RolMenuPermiso> permisos)
        {
            if (permisos == null || !permisos.Any())
                return false;

            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var permiso in permisos)
                    {
                        string sqlCheck = @"SELECT COUNT(1) FROM RolMenuPermiso WHERE IdRol = @IdRol AND IdMenu = @IdMenu";

                        var exists = await connection.ExecuteScalarAsync<int>(sqlCheck, permiso, transaction) > 0;

                        if (exists)
                        {
                            string sqlUpdate = @"UPDATE RolMenuPermiso SET
                            Estado = @Estado,
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdRol = @IdRol AND IdMenu = @IdMenu";

                            await connection.ExecuteAsync(sqlUpdate, permiso, transaction);
                        }
                        else
                        {
                            string sqlInsert = @"INSERT INTO RolMenuPermiso (
                            IdRol,
                            IdMenu,
                            Estado,
                            UsuarioCreacion,
                            FechaCreacion
                        ) VALUES (
                            @IdRol,
                            @IdMenu,
                            @Estado,
                            @UsuarioCreacion,
                            GETDATE()
                        )";

                            await connection.ExecuteAsync(sqlInsert, permiso, transaction);
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

        public async Task<bool> DeletePermisosByRolAsync(int idRol, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE RolMenuPermiso SET
                    Estado = 'I',
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                    WHERE IdRol = @IdRol";

                    var affected = await connection.ExecuteAsync(sql, new
                    {
                        IdRol = idRol,
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
