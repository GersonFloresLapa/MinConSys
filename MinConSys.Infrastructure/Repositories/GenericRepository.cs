using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ConnectionFactory _connectionFactory;
        protected readonly string _tableName;

        protected GenericRepository(ConnectionFactory connectionFactory, string tableName)
        {
            _connectionFactory = connectionFactory;
            _tableName = tableName;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
            }
        }

        public abstract Task<int> AddAsync(T entity);
        public abstract Task<bool> UpdateAsync(T entity);

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var affected = await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = id });
                return affected > 0;
            }
        }
    }
}
