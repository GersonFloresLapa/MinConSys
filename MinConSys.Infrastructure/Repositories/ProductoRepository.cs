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
    public class ProductoRepository : IProductoRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public ProductoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Producto>> GetAllProductosAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdProducto,
                    Nombre,
                    NombreCompleto,
                    Precio,
                    Unidad,
                    Estado
                FROM Producto
                WHERE Estado = 'A'";

                var productos = await connection.QueryAsync<Producto>(sql);
                return productos.ToList();
            }
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdProducto,
                    Nombre,
                    NombreCompleto,
                    Precio,
                    Unidad,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Producto
                WHERE IdProducto = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });
            }
        }

        public async Task<int> AddProductoAsync(Producto producto)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Producto (
                        Nombre,
                        NombreCompleto,
                        Precio,
                        Unidad,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @Nombre,
                        @NombreCompleto,
                        @Precio,
                        @Unidad,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, producto, transaction);
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

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Producto SET
                        Nombre = @Nombre,
                        NombreCompleto = @NombreCompleto,
                        Precio = @Precio,
                        Unidad = @Unidad,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdProducto = @IdProducto AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, producto, transaction);
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

        public async Task<bool> DeleteProductoAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Producto SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdProducto = @IdProducto AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdProducto = id,
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

        public async Task<List<Producto>> GetProductoCboAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdProducto,
                    Nombre,
                    NombreCompleto
                FROM Producto
                WHERE Estado = 'A'";

                var productos = await connection.QueryAsync<Producto>(sql);
                return productos.ToList();
            }
        }


    }
}
