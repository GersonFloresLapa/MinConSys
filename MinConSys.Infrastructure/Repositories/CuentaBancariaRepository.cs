using Dapper;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Response;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class CuentaBancariaRepository : ICuentaBancariaRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public CuentaBancariaRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<CuentaBancaria>> GetAllCuentaBancariasAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdCuenta,
                    CodigoTipoEntidad,
                    IdEntidad,
                    CodigoBanco,
                    Moneda,
                    NroCuenta,
                    TipoCuenta,
                    Estado
                   FROM CuentaBancaria
                   WHERE Estado = 'A'";

                var cuentabancarias = await connection.QueryAsync<CuentaBancaria>(sql);
                return cuentabancarias.ToList();
            }
        }

        public async Task<CuentaBancaria> GetCuentaBancariaByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdCuenta,
                    CodigoTipoEntidad,
                    IdEntidad,
                    CodigoBanco,
                    Moneda,
                    NroCuenta,
                    TipoCuenta,
                    Estado
                   FROM CuentaBancaria
                   WHERE IdCuenta = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<CuentaBancaria>(sql, new { Id = id });
            }
        }

        public async Task<List<CuentaBancaria>> GetCuentaBancariaByIdEmpresaAsync(int idEmpresa)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdCuenta,
                    CodigoBanco,
                    Moneda,
                    TipoCuenta,
                    NroCuenta,
                    Estado
                   FROM CuentaBancaria
                   WHERE IdEntidad = @Id AND Estado = 'A'";

                var cuentabancarias = await connection.QueryAsync<CuentaBancaria>(sql, new { Id = idEmpresa });
                return cuentabancarias.ToList();

            }
        }

        public async Task<int> AddCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"INSERT INTO CuentaBancaria (
                            CodigoTipoEntidad,
                            IdEntidad,
                            CodigoBanco,
                            Moneda,
                            NroCuenta,
                            TipoCuenta,
                            Estado,
                            UsuarioCreacion,
                            FechaCreacion
                        ) VALUES (
                            @CodigoTipoEntidad,
                            @IdEntidad,
                            @CodigoBanco,
                            @Moneda,
                            @NroCuenta,
                            @TipoCuenta,
                            @Estado,
                            @UsuarioCreacion,
                            GETDATE()
                        );
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                        var id = await connection.QuerySingleAsync<int>(sql, cuentabancaria, transaction);
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

        public async Task<bool> UpdateCuentaBancariaAsync(CuentaBancariaRequest request)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sqlCuentaBancaria = @"UPDATE CuentaBancaria SET
                            CodigoTipoEntidad=@CodigoTipoEntidad,
                            IdEntidad=@IdEntidad,
                            CodigoBanco=@CodigoBanco,
                            Moneda=@Moneda,
                            NroCuenta=@NroCuenta,
                            TipoCuenta=@TipoCuenta,
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdCuenta = @IdCuenta AND Estado = 'A'";


                        var affectedRows = await connection.ExecuteAsync(sqlCuentaBancaria, request, transaction);

                        if (affectedRows == 0)
                        {
                            transaction.Rollback();
                            return false;
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
        }

        public async Task<bool> DeleteCuentaBancariaAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE CuentaBancaria SET
                            Estado = 'I',
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdCuenta = @IdCuenta AND Estado = 'A'";

                        var affectedRows = await connection.ExecuteAsync(sql, new
                        {
                            IdCuentaBancaria = id,
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

        public async Task<List<CuentaBancaria>> GetCuentaBancariasByTipoAsync(string tipo)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    P.IdCuenta,
                    P.CodigoTipoEntidad,
                    p.TipoCuenta,
                    p.NroCuenta
                   FROM CuentaBancaria P
                   WHERE P.Estado = 'A' and P.CodigoTipoEntidad=@TipoEntidad ";

                var cuentabancarias = await connection.QueryAsync<CuentaBancaria>(sql, new { TipoEntidad = tipo });
                return cuentabancarias.ToList();
            }
        }

        //public async Task<List<TipoPersona>> GetTipoPersonaByPersonaAsync(int idPersona)
        //{
        //    using (var connection = await _connectionFactory.GetConnection())
        //    {
        //        string sql = @"
        //                SELECT 
        //                    IdTipoPersona,
        //                    CodigoTipoPersona,
        //                    IdPersona,
        //                    Estado,
        //                    UsuarioCreacion,
        //                    FechaCreacion,
        //                    UsuarioModificacion,
        //                    FechaModificacion
        //                FROM TipoPersona
        //                WHERE IdPersona = @IdPersona AND Estado = 'A';";

        //        var result = await connection.QueryAsync<TipoPersona>(sql, new { IdPersona = idPersona });
        //        return result.AsList();
        //    }
        //}


    }
}
