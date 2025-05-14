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
    public class VehiculoRepository : IVehiculoRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public VehiculoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Vehiculo>> GetAllVehiculosAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdVehiculo,
                    Placa,
                    Marca,
                    Modelo,
                    Anio,
                    TipoVehiculoCodigo,
                    CapacidadToneladas,
                    IdEmpresa,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Vehiculos
                WHERE Estado = 'A'";

                var vehiculos = await connection.QueryAsync<Vehiculo>(sql);
                return vehiculos.ToList();
            }
        }

        public async Task<Vehiculo> GetVehiculoByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdVehiculo,
                    Placa,
                    Marca,
                    Modelo,
                    Anio,
                    TipoVehiculoCodigo,
                    CapacidadToneladas,
                    IdEmpresa,
                    Estado,
                    UsuarioCreacion,
                    FechaCreacion,
                    UsuarioModificacion,
                    FechaModificacion
                FROM Vehiculos
                WHERE IdVehiculo = @IdVehiculo AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Vehiculo>(sql, new { IdVehiculo = id });
            }
        }

        public async Task<int> AddVehiculoAsync(Vehiculo vehiculo)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Vehiculos (
                        Placa,
                        Marca,
                        Modelo,
                        Anio,
                        TipoVehiculoCodigo,
                        CapacidadToneladas,
                        IdEmpresa,
                        Estado,
                        UsuarioCreacion,
                        FechaCreacion
                    ) VALUES (
                        @Placa,
                        @Marca,
                        @Modelo,
                        @Anio,
                        @TipoVehiculoCodigo,
                        @CapacidadToneladas,
                        @IdEmpresa,
                        @Estado,
                        @UsuarioCreacion,
                        GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.QuerySingleAsync<int>(sql, vehiculo, transaction);
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

        public async Task<bool> UpdateVehiculoAsync(Vehiculo vehiculo)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Vehiculos SET
                        Placa = @Placa,
                        Marca = @Marca,
                        Modelo = @Modelo,
                        Anio = @Anio,
                        TipoVehiculoCodigo = @TipoVehiculoCodigo,
                        CapacidadToneladas = @CapacidadToneladas,
                        IdEmpresa = @IdEmpresa,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdVehiculo = @IdVehiculo AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, vehiculo, transaction);
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

        public async Task<bool> DeleteVehiculoAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Vehiculos SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdVehiculo = @IdVehiculo AND Estado = 'A'";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        IdVehiculo = id,
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
