using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Common;
using MinConSys.Helpers;
using MinConSys.Modales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinConSys.Core.Models;

namespace MinConSys.Maestros
{
    public partial class TicketEditForm : Form
    {

        private readonly ITicketService _ticketService;
        private readonly IEmpresaService _empresaService;
        private readonly IBalanzaService _balanzaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private readonly IProductoService _productoService;
        private readonly IClaseService _claseService;
        private readonly ILocalidadService _localidadService;
        private readonly IVehiculoService _vehiculoService;
        private readonly IPersonaService _personaService;
        private readonly IAdjuntoService _adjuntoService;

        private List<ComboItem> _empresas;
        private List<ComboItem> _proveedores;
        private List<ComboItem> _balanzas;
        private List<TablaGeneralesCombo> _tipoOperaciones;
        private List<ComboItem> _productos;
        private List<ComboItem> _clases;
        private List<ComboItem> _localidades;
        private List<ComboItem> _depositos;
        private List<ComboItem> _transportistas;
        private List<ComboItem> _tractos;
        private List<ComboItem> _placas;
        private List<ComboItem> _choferes;
        private List<TablaGeneralesCombo> _adjuntosTicket;

        private bool _cargandoComponentes = false;
        private AdjuntosViewerControl _adjuntosViewer;

        private int _idTransportista;
        private readonly int _idTicket;
        public TicketEditForm(ITicketService ticketService,
                                IEmpresaService empresaService,
                                IBalanzaService balanzaService,
                                ITablaGeneralesService tablaGenerales,
                                IProductoService productoService,
                                IClaseService claseService,
                                ILocalidadService localidadService,
                                IVehiculoService vehiculoService,
                                IPersonaService personaService,
                                IAdjuntoService adjuntoService,
                                int idTicket
                                )
        {
            InitializeComponent();
            _ticketService = ticketService;
            _empresaService = empresaService;
            _balanzaService = balanzaService;
            _tablaGeneralesService = tablaGenerales;
            _productoService  = productoService;
            _claseService     = claseService;
            _localidadService = localidadService;
            _vehiculoService  = vehiculoService;
            _personaService   = personaService;
            _idTicket = idTicket;
            _adjuntoService = adjuntoService;

            _adjuntosViewer = new AdjuntosViewerControl
            {
                Dock = DockStyle.Fill
            };

            panel2.Controls.Add(_adjuntosViewer);

        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoTicket = new Ticket
            {
                IdTicket = _idTicket,
                NroTicket = txtNroTicket.Text,
                IdEmpresa = (int)cboEmpresa.SelectedValue,
                IdBalanza = (int)cboBalanza.SelectedValue,
                TipoOperacion=cboTipoOperacion.SelectedValue.ToString(),
                Producto  = cboProducto.SelectedValue.ToString(),
                Clase     = cboClase.SelectedValue.ToString(),
                IdProcedencia= (int)cboProcedencia.SelectedValue,
                IdDeposito   = (int)cboDeposito.SelectedValue,
                IdTransportista = (int)cboTransportista.SelectedValue,
                IdVehiculo   = (int)cboPlacaTracto.SelectedValue,
                IdVehiculo2  = (int?)cboPlacaVehiculo.SelectedValue,
                IdChofer     = (int?)cboChofer.SelectedValue,
                GuiaTransporte= txtNroGuiaTransporte.Text,
                IdProveedor  = (int)cboRemitente.SelectedValue,
                GuiaRemision = txtNroGuiaRemitente.Text,
                FechaGuiaRemision= dtpFechaGuiaRem.Value,
                Observacion  = txtObservacion.Text,
                FechaPesoBruto= dtpFechaPesoBruto.Value,
                PesoBruto    = PesoBruto.Value,
                FechaPesoTara = dtpFechaPesoTara.Value,
                PesoTara      = PesoTara.Value,
                PesoNeto      = PesoNeto.Value,
                PesoAcreditacion = PesoAcreditado.Value,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idTicket != 0)
                    await _ticketService.ActualizarTicketAsync(nuevoTicket);
                else
                    await _ticketService.CrearTicketAsync(nuevoTicket);

                MessageBox.Show("Ticket guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private async void TicketEditForm_Load(object sender, EventArgs e)
        {
            _cargandoComponentes = true;
            var empresasTask = CargarEmpresasAsync();
            var proveedoresTask = CargarProveedoresAsync();
            var balanzasTask = CargarBalanzasAsync();
            var tipoOperacionTask = CargarTipoOperacionAsync();
            var productosTask = CargarProductosAsync();
            var clasesTask = CargarClasesAsync();
            var procedenciasTask = CargarProcedenciasAsync();
            var transportistasTask = CargarTransportistasAsync();
            var depositosTask = CargarDepositosAsync();
            var vehiculosTask = CargarTractoAsync();
            var placasTask = CargarPlacasAsync();
            var choferesTask = CargarChoferesAsync();
            var adjuntosTicket = CargarTipoAdjuntoTicketAsync();



            await Task.WhenAll(empresasTask,
                               proveedoresTask,
                               balanzasTask,
                               tipoOperacionTask,
                               productosTask,
                               clasesTask,
                               procedenciasTask,
                               transportistasTask,
                               depositosTask,
                               vehiculosTask,
                               placasTask,
                               choferesTask,
                               adjuntosTicket
                               );

            if (_idTicket != 0)
            {
                var ticket = await _ticketService.ObtenerPorIdAsync(_idTicket);

                if (ticket != null)
                {
                    txtNroTicket.Text = ticket.NroTicket;
                    txtNroGuiaTransporte.Text = ticket.GuiaTransporte;
                    txtNroGuiaRemitente.Text = ticket.GuiaRemision;
                    txtObservacion.Text = ticket.Observacion;
                    dtpFechaPesoBruto.Text = ticket.FechaPesoBruto.ToString();
                    dtpFechaPesoTara.Text = ticket.FechaPesoTara.ToString();
                    PesoBruto.Text = ticket.PesoBruto.ToString();
                    PesoTara.Text = ticket.PesoTara.ToString();
                    PesoNeto.Text = ticket.PesoNeto.ToString();
                    PesoAcreditado.Text = ticket.PesoAcreditacion.ToString();
                    cboPlacaTracto.Text = ticket.IdVehiculo.ToString();
                    cboPlacaVehiculo.Text = ticket.IdVehiculo2.ToString();
                    txtEstadoSunat.Text = ticket.EstadoContribuyente;
                    txtBrevete.Text = ticket.Brevete;
                    _idTransportista = ticket.IdTransportista;

                    await CargarTractoAsync();
                    await CargarPlacasAsync();

                }


            }
            else
            {
                cboEmpresa.SelectedIndex = -1;
                cboBalanza.SelectedIndex = -1;
                cboProducto.SelectedIndex = -1;
                cboClase.SelectedIndex = -1;
                cboTipoOperacion.SelectedIndex = -1;
                cboProcedencia.SelectedIndex = -1;
                cboDeposito.SelectedIndex = -1;
                cboRemitente.SelectedIndex = -1;
                cboTransportista.SelectedIndex = -1;
                cboPlacaTracto.SelectedIndex = -1;
                cboPlacaVehiculo.SelectedIndex = -1;
                cboChofer.SelectedIndex = -1;
            }

            await _adjuntosViewer.InicializarAsync(_adjuntoService, "Ticket", _idTicket, _adjuntosTicket);
            _cargandoComponentes = false;
                
        }

        private async Task CargarEmpresasAsync()
        {

            _empresas = await _empresaService.ListarEmpresasGrupoAsync();

            cboEmpresa.DataSource = _empresas;
            cboEmpresa.DisplayMember = "Descripcion";
            cboEmpresa.ValueMember = "Id";
            cboEmpresa.DropDownStyle = ComboBoxStyle.DropDown;
            cboEmpresa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboEmpresa.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

       

        private async Task CargarBalanzasAsync()
        {

            _balanzas = await _balanzaService.ListarBalanzaCboAsync();

            cboBalanza.DataSource = _balanzas;
            cboBalanza.DisplayMember = "Descripcion";
            cboBalanza.ValueMember = "Id";
            cboBalanza.DropDownStyle = ComboBoxStyle.DropDown;
            cboBalanza.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboBalanza.AutoCompleteSource = AutoCompleteSource.ListItems;


        }

        private async Task CargarProductosAsync()
        {

            _productos = await _productoService.ListarProductosCboAsync();

            cboProducto.DataSource = _productos;
            cboProducto.DisplayMember = "Descripcion";
            cboProducto.ValueMember = "Id";
            cboProducto.DropDownStyle = ComboBoxStyle.DropDown;
            cboProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProducto.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private async Task CargarClasesAsync()
        {

            _clases = await _claseService.ListarClasesCboAsync();

            cboClase.DataSource = _clases;
            cboClase.DisplayMember = "Descripcion";
            cboClase.ValueMember = "Id";
            cboClase.DropDownStyle = ComboBoxStyle.DropDown;
            cboClase.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboClase.AutoCompleteSource = AutoCompleteSource.ListItems;
 
        }

        private async Task CargarTipoOperacionAsync()
        {

            _tipoOperaciones = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOOPERACION");

            cboTipoOperacion.DataSource = _tipoOperaciones;
            cboTipoOperacion.DisplayMember = "Valor";
            cboTipoOperacion.ValueMember = "Codigo";
            cboTipoOperacion.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private async Task CargarProcedenciasAsync()
        {

            _localidades = await _localidadService.ListarLocalidadesTiposAsync("P");

            cboProcedencia.DataSource = _localidades;
            cboProcedencia.DisplayMember = "Descripcion";
            cboProcedencia.ValueMember = "Id";
            cboProcedencia.DropDownStyle = ComboBoxStyle.DropDown;
            cboProcedencia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProcedencia.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private async Task CargarDepositosAsync()
        {

            _depositos = await _localidadService.ListarLocalidadesTiposAsync("D");

            cboDeposito.DataSource = _depositos;
            cboDeposito.DisplayMember = "Descripcion";
            cboDeposito.ValueMember = "Id";
            cboDeposito.DropDownStyle = ComboBoxStyle.DropDown;
            cboDeposito.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboDeposito.AutoCompleteSource = AutoCompleteSource.ListItems;

        }


        private async Task CargarProveedoresAsync()
        {

            _proveedores = await _empresaService.ListarEmpresasTiposAsync("PROMIN");

            cboRemitente.DataSource = _proveedores;
            cboRemitente.DisplayMember = "Descripcion";
            cboRemitente.ValueMember = "Id";
            cboRemitente.DropDownStyle = ComboBoxStyle.DropDown;
            cboRemitente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboRemitente.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private async Task CargarTransportistasAsync()
        {
            
            _transportistas = await _empresaService.ListarEmpresasTiposAsync("PROTRA");

            cboTransportista.DataSource = _transportistas;
            cboTransportista.DisplayMember = "Descripcion";
            cboTransportista.ValueMember = "Id";
            cboTransportista.DropDownStyle = ComboBoxStyle.DropDown;
            cboTransportista.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTransportista.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private async Task CargarTractoAsync()
        {
            _tractos = await _vehiculoService.ListarVehiculosTiposAsync(_idTransportista, "TRA");

            cboPlacaTracto.DataSource = _tractos;
            cboPlacaTracto.DisplayMember = "Descripcion";
            cboPlacaTracto.ValueMember = "Id";
            cboPlacaTracto.DropDownStyle = ComboBoxStyle.DropDown;
            cboPlacaTracto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPlacaTracto.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private async Task CargarPlacasAsync()
        {

            _placas = await _vehiculoService.ListarVehiculosTiposAsync(_idTransportista, "REM");

            cboPlacaVehiculo.DataSource = _placas;
            cboPlacaVehiculo.DisplayMember = "Descripcion";
            cboPlacaVehiculo.ValueMember = "Id";
            cboPlacaVehiculo.DropDownStyle = ComboBoxStyle.DropDown;
            cboPlacaVehiculo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPlacaVehiculo.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private async Task CargarChoferesAsync()
        {

            _choferes = await _personaService.ListarPersonasTiposAsync("CHO");

            cboChofer.DataSource = _choferes;
            cboChofer.DisplayMember = "Descripcion";
            cboChofer.ValueMember = "Id";
            cboChofer.DropDownStyle = ComboBoxStyle.DropDown;
            cboChofer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboChofer.AutoCompleteSource = AutoCompleteSource.ListItems;

        }


        private void btnBuscarEmpresa_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_empresas);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboEmpresa.SelectedItem = cboEmpresa.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }

        private void btnBuscarProcedencia_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_localidades);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboProcedencia.SelectedItem = cboProcedencia.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }

        private void btnBuscarDeposito_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_depositos);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboDeposito.SelectedItem = cboDeposito.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }


        private void btnBuscarTransportista_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_transportistas);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboTransportista.SelectedItem = cboTransportista.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }

        private void btnBuscarChofer_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_choferes);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboChofer.SelectedItem = cboChofer.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {

        }

        private async void cboTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoComponentes)
            {
                return;
            }

            if (cboTransportista.SelectedValue is int idTransportista)
            {
                _idTransportista = idTransportista;

                await CargarTractoAsync();
                await CargarPlacasAsync();
            }

        }

        private async void cboChofer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoComponentes)
            {
                return;
            }

            if (cboChofer.SelectedValue is int idChofer)
            {
                Persona persona = await _personaService.ObtenerPorIdAsync(idChofer);
                txtBrevete.Text = persona.Brevete;

            }
        }

        private async Task CargarTipoAdjuntoTicketAsync()
        {

            _adjuntosTicket = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("ADJUNTOTICKET");
        }


    }
}
