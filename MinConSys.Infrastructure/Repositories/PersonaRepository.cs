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
    public class PersonaRepository : IPersonaRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public PersonaRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Persona>> GetAllPersonasAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdPersona,
                    TipoDocumento,
                    NumeroDocumento,
                    Nombres,
                    ApellidoPaterno,
                    ApellidoMaterno,
                    CorreoElectronico,
                    Estado
                   FROM Personas
                   WHERE Estado = 'A'";

                var personas = await connection.QueryAsync<Persona>(sql);
                return personas.ToList();
            }
        }

        public async Task<Persona> GetPersonaByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT 
                    IdPersona,
                    TipoDocumento,
                    NumeroDocumento,
                    Nombres,
                    ApellidoPaterno,
                    ApellidoMaterno,
                    CorreoElectronico,
                    Estado
                   FROM Personas
                   WHERE IdPersona = @Id AND Estado = 'A'";

                return await connection.QueryFirstOrDefaultAsync<Persona>(sql, new { Id = id });
            }
        }

        public async Task<int> AddPersonaAsync(Persona persona)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"INSERT INTO Personas (
                            TipoDocumento,
                            NumeroDocumento,
                            Nombres,
                            ApellidoPaterno,
                            ApellidoMaterno,
                            CorreoElectronico,
                            Telefono,
                            Direccion,
                            Brevete,
                            Estado,
                            UsuarioCreacion,
                            FechaCreacion
                        ) VALUES (
                            @TipoDocumento,
                            @NumeroDocumento,
                            @Nombres,
                            @ApellidoPaterno,
                            @ApellidoMaterno,
                            @CorreoElectronico,
                            @Telefono,
                            @Direccion,
                            @Brevete,
                            @Estado,
                            @UsuarioCreacion,
                            GETDATE()
                        );
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                        var id = await connection.QuerySingleAsync<int>(sql, persona, transaction);
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

        public async Task<bool> UpdatePersonaAsync(Persona persona)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE Personas SET
                            TipoDocumento = @TipoDocumento,
                            NumeroDocumento = @NumeroDocumento,
                            Nombres = @Nombres,
                            ApellidoPaterno = @ApellidoPaterno,
                            ApellidoMaterno = @ApellidoMaterno,
                            CorreoElectronico = @CorreoElectronico,
                            Telefono = @Telefono,
                            Direccion = @Direccion,
                            Brevete = @Brevete,
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdPersona = @IdPersona AND Estado = 'A'";

                        var affectedRows = await connection.ExecuteAsync(sql, persona, transaction);
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

        public async Task<bool> DeletePersonaAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
      
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE Personas SET
                            Estado = 'I',
                            UsuarioModificacion = @UsuarioModificacion,
                            FechaModificacion = GETDATE()
                        WHERE IdPersona = @IdPersona AND Estado = 'A'";

                        var affectedRows = await connection.ExecuteAsync(sql, new
                        {
                            IdPersona = id,
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
