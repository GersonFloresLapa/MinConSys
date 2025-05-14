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
    public class LocalidadRepository : ILocalidadRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public LocalidadRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Localidad>> GetAllLocalidadesAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdLocalidad,
                    IdEmpresa,
                    TipoLocalidad,
                    NombreLocalidad,
                    Direccion,
                    Ubigeo,
                    Estado
                   FROM Localidad
                   WHERE Estado = 'A'";

                var localidad = await connection.QueryAsync<Localidad>(sql);
                return localidad.ToList();
            }
        }

        public async Task<Localidad> GetLocalidadByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdLocalidad,
                    IdEmpresa,
                    TipoLocalidad,
                    NombreLocalidad,
                    Direccion,
                    Ubigeo,
                    Estado
                   FROM Localidad
                   WHERE IdLocalidad = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Localidad>(sql, new { Id = id });
            }
        }

        public async Task<int> AddLocalidadAsync(Localidad localidad)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"INSERT INTO Localidad (
                            IdEmpresa,
                            TipoLocalidad,
                            NombreLocalidad,
                            Direccion,
                            Ubigeo,
                            Estado,
                            UsuarioCreacion,
                            FechaCreacion
                        ) VALUES (
                            @idEmpresa,
                            @TipoLocalidad,
                            @NombreLocalidad,
                            @Direccion,
                            @Ubigeo,
                            @Estado,
                            @UsuarioCreacion,
                            GETDATE()
                        );
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                        var id = await connection.QuerySingleAsync<int>(sql, localidad, transaction);
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

        public async Task<bool> UpdateLocalidadAsync(Localidad localidad)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE Localidad SET
                            IdEmpresa=@IdEmpresa,
                            TipoLocalidad=@TipoLocalidad,
                            NombreLocalidad=@NombreLocalidad,
                            Direccion=@Direccion,
                            Ubigeo=@Ubigeo,
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdLocalidad = @IdLocalidad AND Estado = 'A'";

                        var affectedRows = await connection.ExecuteAsync(sql, localidad, transaction);
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

        public async Task<bool> DeleteLocalidadAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE Localidad SET
                            Estado = 'I',
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdLocalidad = @IdLocalidad AND Estado = 'A'";

                        var affectedRows = await connection.ExecuteAsync(sql, new
                        {
                            IdLocalidad = id,
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
}
