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
    public class MenuRepository : IMenuRepository
    {
        protected readonly ConnectionFactory _connectionFactory;
        public MenuRepository(ConnectionFactory connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<MenuDto>> ObtenerMenuPorUsuario(string nombreUsuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                var menu =  await connection.QueryAsync<MenuDto>(
                       "usp_ObtenerMenuPorUsuario",
                       new { nombreUsuario },
                       commandType: CommandType.StoredProcedure
                        );

                return menu.ToList();
            }
        }
    }
}
