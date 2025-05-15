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
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public UsuarioRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdUsuario,
                IdRol,
                NombreUsuario,
                Clave,
                Nombres,
                ApellidoPaterno,
                ApellidoMaterno,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
            FROM Usuario
            WHERE Estado = 'A'";

                var usuarios = await connection.QueryAsync<Usuario>(sql);
                return usuarios.ToList();
            }
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                IdUsuario,
                IdRol,
                NombreUsuario,
                Clave,
                Nombres,
                ApellidoPaterno,
                ApellidoMaterno,
                Estado,
                UsuarioCreacion,
                FechaCreacion,
                UsuarioModificacion,
                FechaModificacion
            FROM Usuario
            WHERE IdUsuario = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
            }
        }

        public async Task<int> AddUsuarioAsync(Usuario usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Usuario (
                    IdRol,
                    NombreUsuario,
                    Clave,
                    Nombres,
                    ApellidoPaterno,
                    ApellidoMaterno,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion
                ) VALUES (
                    @IdRol,
                    @NombreUsuario,
                    @Clave,
                    @Nombres,
                    @ApellidoPaterno,
                    @ApellidoMaterno,
                    @Estado,
                    @UsuarioCreacion,
                    GETDATE()
                );
                SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, usuario, transaction);
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

        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Usuario SET
                    IdRol = @IdRol,
                    NombreUsuario = @NombreUsuario,
                    Clave = @Clave,
                    Nombres = @Nombres,
                    ApellidoPaterno = @ApellidoPaterno,
                    ApellidoMaterno = @ApellidoMaterno,
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                WHERE IdUsuario = @IdUsuario AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, usuario, transaction);
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

        public async Task<bool> DeleteUsuarioAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Usuario SET
                    Estado = 'I',
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = GETDATE()
                WHERE IdUsuario = @IdUsuario AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdUsuario = id,
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
