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
            using (var connection = _connectionFactory.GetConnection())
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
                       FROM Personas";

                var personas = await connection.QueryAsync<Persona>(sql);

                return personas.ToList();
            }
        }
    }
}
