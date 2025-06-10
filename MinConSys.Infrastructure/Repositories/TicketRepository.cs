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
                                        C.IdProcedencia,
                                        C.IdDeposito,
                                        C.IdTransportista,
                                        C.IdVehiculo,
                                        C.IdVehiculo2,
                                        C.IdChofer,
                                        C.IdProveedor,
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
                                        L.NombreLocalidad Procedencia,
		                                T.RazonSocial Transportista,
		                                G.Valor EstadoContribuyente,
                                        P.Brevete,
                                        OP.Valor TipoOperacion,
                                        CL.Nombre Clase,
                                        PR.Nombre Producto,
                                        DE.NombreLocalidad Deposito,
                                        PE.NumeroDocumento +' - '+ rtrim(PE.Nombres)+' '+rtrim(PE.ApellidoPaterno)+' '+rtrim(PE.ApellidoMaterno) Chofer,
                                        TR.Placa Tracto,
                                        PL.Placa Remolque,
                                        PV.Ruc+' - '+rtrim(PV.RazonSocial) Proveedor,
                                        EG.Ruc+' - '+rtrim(EG.RazonSocial) Empresa
                                FROM Ticket C
                                INNER JOIN Balanza B ON B.IdBalanza = c.IdBalanza
                                INNER JOIN Localidad L ON L.IdLocalidad = C.IdProcedencia
                                INNER JOIN Localidad DE ON DE.IdLocalidad = C.IdDeposito
                                INNER JOIN Empresas EG on EG.IdEmpresa=C.IdEmpresa 
                                INNER JOIN Empresas T ON T.IdEmpresa=C.IdTransportista
                                INNER JOIN Empresas PV ON PV.IdEmpresa=C.IdProveedor
                                LEFT JOIN TablaGenerales G on G.TipoGeneral='ESTADOCONTRIBUYENTE' and G.Codigo=T.EstadoContribuyente
                                LEFT JOIN TablaGenerales OP on OP.TipoGeneral='TIPOOPERACION' and OP.Codigo=C.TipoOperacion
                                LEFT JOIN Personas P on P.IdPersona = C.IdChofer
                                LEFT JOIN Clase CL on CL.IdClase =C.Clase
                                LEFT JOIN Producto PR on PR.IdProducto=C.Producto
                                LEFT JOIN Personas PE on PE.IdPersona=C.IdChofer
                                LEFT JOIN Vehiculos TR on TR.IdVehiculo=C.IdVehiculo
                                LEFT JOIN Vehiculos PL on PL.IdVehiculo=C.IdVehiculo2
                                WHERE  C.Estado = 'A'";
                var tickets = await connection.QueryAsync<TicketDto>(sql);
                return tickets.ToList();
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT	C.IdTicket,
		                                C.NroTicket,
		                                C.IdBalanza,
		                                C.TipoOperacion,
                                        C.Clase,
		                                C.Producto,
                                        C.IdProcedencia,
                                        C.IdDeposito,
                                        C.IdTransportista,
                                        C.IdVehiculo,
                                        C.IdVehiculo2,
                                        C.IdChofer,
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
		                                G.Valor EstadoContribuyente,
                                        P.Brevete    
                                FROM Ticket C
                                INNER JOIN Balanza B ON B.IdBalanza = c.IdBalanza
                                INNER JOIN Localidad L ON L.IdLocalidad = C.IdProcedencia
                                INNER JOIN Empresas T ON T.IdEmpresa=C.IdTransportista
                                INNER JOIN TablaGenerales G on G.TipoGeneral='ESTADOCONTRIBUYENTE' and g.Codigo=T.EstadoContribuyente
                                LEFT JOIN Personas P on P.IdPersona =C.IdChofer
                                WHERE IdTicket = @IdTicket AND C.Estado = 'A'";
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
                    const string sql = @"
                                        INSERT INTO Ticket (
                                              IdEmpresa,
                                              NroTicket,
                                              IdBalanza,
                                              TipoOperacion,
                                              Clase,
                                              Producto,
                                              IdProcedencia,
                                              IdDeposito,
                                              IdTransportista,
                                              IdChofer,
                                              IdVehiculo,
                                              IdVehiculo2,
                                              FechaMovimiento,
                                              GuiaRemision,
                                              FechaGuiaRemision,
                                              GuiaTransporte,
                                              IdProveedor,
                                              Observacion,
                                              FechaPesoBruto,
                                              PesoBruto,
                                              FechaPesoTara,
                                              PesoTara,
                                              PesoNeto,
                                              PesoAcreditacion,
                                              Estado,
                                              UsuarioCreacion,
                                              FechaCreacion,
                                              UsuarioModificacion,
                                              FechaModificacion
                                        ) VALUES (
                                              @IdEmpresa,
                                              @NroTicket,
                                              @IdBalanza,
                                              @TipoOperacion,
                                              @Clase,
                                              @Producto,
                                              @IdProcedencia,
                                              @IdDeposito,
                                              @IdTransportista,
                                              @IdChofer,
                                              @IdVehiculo,
                                              @IdVehiculo2,
                                              @FechaMovimiento,
                                              @GuiaRemision,
                                              @FechaGuiaRemision,
                                              @GuiaTransporte,
                                              @IdProveedor,
                                              @Observacion,
                                              @FechaPesoBruto,
                                              @PesoBruto,
                                              @FechaPesoTara,
                                              @PesoTara,
                                              @PesoNeto,
                                              @PesoAcreditacion,
                                              @Estado,
                                              @UsuarioCreacion,
                                              @FechaCreacion,       -- usa valor del modelo; cámbialo a GETDATE() si prefieres
                                              @UsuarioModificacion,
                                              @FechaModificacion
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
                    string sql = @"UPDATE Ticket SET
                        IdEmpresa =@IdEmpresa,
                        NroTicket = @NroTicket,
                        IdBalanza = @IdBalanza,
                        TipoOperacion = @TipoOperacion,
                        Clase = @Clase,
                        Producto = @Producto,
                        IdProcedencia=@IdProcedencia,
                        IdDeposito =@IdProcedencia,
                        IdTransportista= @IdTransportista,
                        IdChofer= @IdChofer,
                        IdVehiculo= @IdVehiculo,
                        IdVehiculo2=@IdVehiculo2,
                        FechaMovimiento=@FechaMovimiento,
                        IdProveedor   = @IdProveedor,
                        GuiaRemision=@GuiaRemision,
                        FechaGuiaRemision=@FechaGuiaRemision,
                        GuiaTransporte=@GuiaTransporte,
                        Observacion=@Observacion,
                        FechaPesoBruto=@FechaPesoBruto,
                        PesoBruto=@PesoBruto,
                        FechaPesoTara=@FechaPesoTara,
                        PesoTara=@PesoTara,
                        PesoNeto=@PesoNeto,
                        PesoAcreditacion=@PesoAcreditacion,
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE IdTicket = @IdTicket AND Estado = 'A'";

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

        public async Task<List<TicketRumaDto>> GetAllTicketsForRumaAsync(int idProveedor, int idClase, int idProducto)
        {
            using (var connection = await _connectionFactory.GetConnection())
            {
                string sql = @"SELECT	C.IdTicket,
		                                C.NroTicket,
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
                                        L.NombreLocalidad Procedencia,
		                                T.RazonSocial Transportista,
		                                G.Valor EstadoContribuyente,
                                        P.Brevete,
                                        OP.Valor TipoOperacion,
                                        CL.Nombre Clase,
                                        PR.Nombre Producto,
                                        DE.NombreLocalidad Deposito,
                                        PE.NumeroDocumento +' - '+ rtrim(PE.Nombres)+' '+rtrim(PE.ApellidoPaterno)+' '+rtrim(PE.ApellidoMaterno) Chofer,
                                        TR.Placa Tracto,
                                        PL.Placa Remolque,
                                        PV.Ruc+' - '+rtrim(PV.RazonSocial) Proveedor,
                                        EG.Ruc+' - '+rtrim(EG.RazonSocial) Empresa
                                FROM Ticket C
                                INNER JOIN Balanza B ON B.IdBalanza = c.IdBalanza
                                INNER JOIN Localidad L ON L.IdLocalidad = C.IdProcedencia
                                INNER JOIN Localidad DE ON DE.IdLocalidad = C.IdDeposito
                                INNER JOIN Empresas EG on EG.IdEmpresa=C.IdEmpresa 
                                INNER JOIN Empresas T ON T.IdEmpresa=C.IdTransportista
                                INNER JOIN Empresas PV ON PV.IdEmpresa=C.IdProveedor
                                LEFT JOIN TablaGenerales G on G.TipoGeneral='ESTADOCONTRIBUYENTE' and G.Codigo=T.EstadoContribuyente
                                LEFT JOIN TablaGenerales OP on OP.TipoGeneral='TIPOOPERACION' and OP.Codigo=C.TipoOperacion
                                LEFT JOIN Personas P on P.IdPersona = C.IdChofer
                                LEFT JOIN Clase CL on CL.IdClase =C.Clase
                                LEFT JOIN Producto PR on PR.IdProducto=C.Producto
                                LEFT JOIN Personas PE on PE.IdPersona=C.IdChofer
                                LEFT JOIN Vehiculos TR on TR.IdVehiculo=C.IdVehiculo
                                LEFT JOIN Vehiculos PL on PL.IdVehiculo=C.IdVehiculo2
                                WHERE  C.Estado = 'A' and C.IdProveedor = @IdProveedor and C.Clase = @IdClase and C.Producto = @IdProducto 
                                AND NOT EXISTS (SELECT 1 FROM RumaTicket RT WHERE RT.IdTicket = C.IdTicket)";
                var tickets = await connection.QueryAsync<TicketRumaDto>(sql,new { IdProveedor = idProveedor, IdClase = idClase, IdProducto = idProducto});
                return tickets.ToList();
            }
        }
    }
}
