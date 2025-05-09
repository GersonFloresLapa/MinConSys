using Dapper;
using MinConSys.Core.Interfaces.Repository;
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
    public class LoginRepository : ILoginRepository
    {
        protected readonly ConnectionFactory _connectionFactory;
        public LoginRepository(ConnectionFactory connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<LoginResponse> Autenticar(string nombreUsuario, string clave)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var parameters = new { NombreUsuario = nombreUsuario, Clave = clave };

                var usuario = await connection.QueryFirstOrDefaultAsync<LoginResponse>(
                    "usp_LoginUsuario",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return usuario;
            }
        }
    }
}
