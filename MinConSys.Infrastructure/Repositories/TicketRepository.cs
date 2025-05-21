using Dapper;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        protected readonly ConnectionFactory _connectionFactory;

        public TicketRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<TicketDto>> GetAllTicketsAsync()
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT	C.IdTicket,
		                                C.NroTicket,
		                                C.IdBalanza,
		                                C.TipoOperacion,
                                        C.Clase,
		                                C.Producto,
                                        C.IdLocalidad,
                                        C.IdTransportista,
                                        C.IdVehiculo,
                                        C.IdVehiculo2,
                                        C.FechaMovimiento,
                                        C.GuiaRemision,
                                        C.FechaGuiaRemision,
                                        C.GuiaTransporte,
                                        C.Observacion,
                                        C.FechaPesoBruto,
                                        C.PesoBruto,
                                        C.FechaPesoTara,
                                        C.PesoTara,
                                        C.PesoNeto,
                                        C.PesoAcreditacion,
                                        C.Estado,
                                        B.Nombre Balanza,
                                        L.NombreLocalidad Localidad,
		                                T.RazonSocial Transportista,
		                                P.RazonSocial Proveedor
                                FROM Ticket C
                                INNER JOIN Balanza B ON B.IdBalanza = c.IdBalanza
                                INNER JOIN Localidad L ON L.IdLocalidad = C.IdLocalidad
                                INNER JOIN Empresa T ON T.IdEmpresa=C.IdTransportista
                                WHERE  C.Estado = 'A'";
                var tickets = await connection.QueryAsync<TicketDto>(sql);
                return tickets.ToList();
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT * FROM Ticket WHERE IdTicket = @IdTicket AND Estado = 'A'";
                return await connection.QueryFirstOrDefaultAsync<Ticket>(sql, new { IdTicket = id });
            }
        }

        public async Task<int> AddTicketAsync(Ticket ticket)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"INSERT INTO Ticket (
                                        NroTicket,
	                                    IdBalanza,
	                                    TipoOperacion,
	                                    Clase,
	                                    Producto,
                                        IdLocalidad,
	                                    IdTransportista,
	                                    IdVehiculo,
	                                    IdVehiculo2,
                                        FechaMovimiento,
	                                    GuiaRemision,
	                                    FechaGuiaRemision,
	                                    GuiaTransporte,
	                                    Observacion ,
	                                    FechaPesoBruto,
	                                    PesoBruto ,
	                                    FechaPesoTara,
	                                    PesoTara ,
	                                    PesoNeto ,
	                                    PesoAcreditacion, 
	                                    Estado,
                                        UsuarioCreacion,
                                        FechaCreacion
                                    ) VALUES (
                                        @NroTicket,
                                        @IdBalanza,
                                        @TipoOperacion,
                                        @Clase,
                                        @Producto,
                                        @TipoContrato,
                                        @Clase,
                                        @Producto,
                                        @IdLocalidad,
                                        @IdTransportista,
                                        @IdVehiculo,
                                        @IdVehiculo2,
                                        @FechaMovimiento,
                                        @GuiaRemision,
                                        @FechaGuiaRemision,
                                        @GuiaTransporte,
                                        @Observacion,
                                        @FechaPesoBruto,
                                        @PesoBruto,
                                        @FechaPesoTara,
                                        @PesoTara,
                                        @PesoNeto,
                                        @PesoAcreditacion,
                                        @Estado,
                                        @UsuarioCreacion,
                                        GETDATE()
                                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var id = await connection.QuerySingleAsync<int>(sql, ticket, transaction);
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

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Contratos SET
                        CodigoContrato = @CodigoContrato,
                        IdEmpresa = @IdEmpresa,
                        IdProveedor = @IdProveedor,
                        FechaInicio = @FechaInicio,
                        FechaFin = @FechaFin,
                        TipoContrato = @TipoContrato,
                        Clase = @Clase,
                        Producto = @Producto,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdContrato = @IdContrato AND Estado = 'A'";

                    var affected = await connection.ExecuteAsync(sql, ticket, transaction);
                    transaction.Commit();
                    return affected > 0;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteTicketAsync(int id, string usuario)
        {
            using (var connection = await _connectionFactory.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sql = @"UPDATE Contratos SET
                        Estado = 'I',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdContrato = @IdContrato AND Estado = 'A'";

                    var affected = await connection.ExecuteAsync(sql, new
                    {
                        IdTicket = id,
                        UsuarioModificacion = usuario
                    }, transaction);

                    transaction.Commit();
                    return affected > 0;
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
