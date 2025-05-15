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
    public class ClaseRepository : IClaseRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public ClaseRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Clase>> GetAllClasesAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdClase,
                    Nombre,
                    Descripcion,
                    Unidad,
                    Estado
                FROM Clase
                WHERE Estado = 'A'";

                var clases = await connection.QueryAsync<Clase>(sql);
                return clases.ToList();
            }
        }

        public async Task<Clase> GetClaseByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdClase,
                    Nombre,
                    Descripcion,
                    Unidad,
                    Estado
                FROM Clase
                WHERE IdClase = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Clase>(sql, new { Id = id });
            }
        }

        public async Task<int> AddClaseAsync(Clase clase)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Clase (
                        Nombre,
                        Descripcion,
                        Unidad,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @Nombre,
                        @Descripcion,
                        @Unidad,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, clase, transaction);
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

        public async Task<bool> UpdateClaseAsync(Clase clase)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Clase SET
                        Nombre = @Nombre,
                        Descripcion = @Descripcion,
                        Unidad = @Unidad,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdClase = @IdClase AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, clase, transaction);
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

        public async Task<bool> DeleteClaseAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Clase SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdClase = @IdClase AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdClase = id,
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
