using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Request;
using MinConSys.Helpers;
using MinConSys.Modales;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys
{
    public partial class RumaEditForm : Form
    {
        private readonly IRumaService _rumaService;
        private readonly string _codigoClase;
        public readonly ITicketService _ticketService;
        public readonly IEmpresaService _empresaService;
        public readonly ITablaGeneralesService _tablaGeneralesService;
        public readonly IProductoService _productoService;
        public readonly IClaseService _claseService;
        public readonly IContratoService _contratoService;
        public readonly ILocalidadService _localidadService;
        public readonly IAdjuntoService _adjuntoService;
        public int _idRuma;
        public int _idRumaEstado;
        private AdjuntosViewerControl _adjuntosViewer;

        private List<ComboItem> _empresas;
        private List<ComboItem> _proveedores;
        private List<ComboItem> _contrato;
        private List<ComboItem> _productos;
        private List<ComboItem> _clases;
        private List<ComboItem> _procedencia;
        private List<ComboItem> _deposito;
        private bool _cargandoComponentes = false;
        public int _idClase;
        

        public event EventHandler GuardadoExitoso;
        public RumaEditForm(
            IRumaService rumaService,
            ITicketService ticketService,
            IEmpresaService empresaService,
            ITablaGeneralesService tablaGeneralesService,
            IProductoService productoService,
            IClaseService claseService,
            IContratoService contratoService,
            ILocalidadService localidadService,
            IAdjuntoService adjuntoService,
            string codigoClase,
            int idRuma,
            int idRumaEstado)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            _rumaService = rumaService;
            _ticketService = ticketService;
            _empresaService = empresaService;
            _tablaGeneralesService = tablaGeneralesService;
            _productoService = productoService;
            _claseService = claseService;
            _contratoService = contratoService;
            _localidadService = localidadService;
            _adjuntoService = adjuntoService;
            _codigoClase = codigoClase;
            _idRuma = idRuma;
            _idRumaEstado = idRumaEstado;

            
        }

        private async void RumaEditForm_Load(object sender, EventArgs e)
        {

            _cargandoComponentes = true;

            await CargarCombosAsync();
            if (_idRuma != 0)
            {
                await CargarRumaActualizar();

                await CargarTipoAdjuntoRumaEstadoAsync();

            }

            _cargandoComponentes = false; 

        }

        #region Carga de Combos
        private void ConfigurarComboBox(ComboBox combo, object dataSource, string display, string value, bool autoComplete = true)
        {
            combo.DataSource = dataSource;
            combo.DisplayMember = display;
            combo.ValueMember = value;
            combo.DropDownStyle = ComboBoxStyle.DropDown;
            if (autoComplete)
            {
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combo.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            combo.SelectedIndex = -1;
        }

        private async Task CargarCombosAsync()
        {
            var empresasTask = _empresaService.ListarEmpresasGrupoAsync();
            var proveedoresTask = _empresaService.ListarEmpresasTiposAsync("PROMIN");
            var productosTask = _productoService.ListarProductosCboAsync();
            var clasesTask = _claseService.ListarClasesCboAsync();
            var clasesFullTask = _claseService.ListarClasesAsync();
            var procedenciaTask = _localidadService.ListarLocalidadesTiposAsync("P");
            var depositoTask = _localidadService.ListarLocalidadesTiposAsync("D");
            var adjuntoTicketTask = _tablaGeneralesService.ObtenerPorTipoGeneralAsync("ADJUNTOTICKET");

            await Task.WhenAll(empresasTask, proveedoresTask, productosTask, clasesTask, clasesFullTask, procedenciaTask, depositoTask, adjuntoTicketTask);

            _empresas = empresasTask.Result;
            _proveedores = proveedoresTask.Result;
            _productos = productosTask.Result;
            _clases = clasesTask.Result;
            var clasesFull = clasesFullTask.Result;
            _procedencia = procedenciaTask.Result;
            _deposito = depositoTask.Result;
            var adjuntoTicket = adjuntoTicketTask.Result;

            ConfigurarComboBox(cboEmpresa, _empresas, "Descripcion", "Id");
            ConfigurarComboBox(cboProveedor, _proveedores, "Descripcion", "Id");
            ConfigurarComboBox(cboProducto, _productos, "Descripcion", "Id", false);
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            ConfigurarComboBox(cboClase, _clases, "Descripcion", "Id", false);
            cboClase.DropDownStyle = ComboBoxStyle.DropDownList;

            _idClase = clasesFull.Where(x => x.Nombre == _codigoClase).Select(x => x.IdClase).FirstOrDefault();
            if (_idClase != 0) cboClase.SelectedValue = _idClase;

            ConfigurarComboBox(cboProcedencia, _procedencia, "Descripcion", "Id");
            ConfigurarComboBox(cboDeposito, _deposito, "Descripcion", "Id");
            ConfigurarComboBox(cboAdjuntoTicket.ComboBox, adjuntoTicket, "Valor", "Codigo");
            //cboAdjuntoTicket.SelectedIndex = 1;
            await CargarCombosContratosAsync(null, null);
        }


        private async Task CargarCombosContratosAsync(int? idEmpresa, int? idProveedor)
        {
            _contrato = await _contratoService.ListarContratosTiposAsync(idEmpresa, idProveedor);
            ConfigurarComboBox(cboContrato, _contrato, "Descripcion", "Id");
        }
        #endregion

        #region Eventos ComboBox
        private async void cboContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoComponentes) return;

            if (cboContrato.SelectedValue is int idContrato)
            {
                _cargandoComponentes = true;

                var contrato = await _contratoService.ObtenerPorIdAsync(idContrato);
                cboEmpresa.SelectedValue = contrato.IdEmpresa;
                cboEmpresa.Enabled = false;
                cboProveedor.SelectedValue = contrato.IdProveedor;
                cboProveedor.Enabled = false;
                cboProducto.SelectedValue = contrato.IdProducto;
                cboProducto.Enabled = false;

                _cargandoComponentes = false;
            }
        }

        private async void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoComponentes) return;

            _cargandoComponentes = true;
            await CargarCombosContratosAsync((int?)cboEmpresa.SelectedValue, (int?)cboProveedor.SelectedValue);
            _cargandoComponentes = false;

        }

        private async void cboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoComponentes) return;

            _cargandoComponentes = true;
            await CargarCombosContratosAsync((int?)cboEmpresa.SelectedValue, (int?)cboProveedor.SelectedValue);
            _cargandoComponentes = false;
        }
        #endregion

        #region Guardar
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            try
            {
                var request = await ConstruirRequestRumaNueva();

                if (_idRuma != 0)
                {
                    if (_idRumaEstado != 0)
                        await _rumaService.ActualizarRumaYEstadoAsync(request);
                    else
                        await _rumaService.ActualizarRumaInsertarEstadoAsync(request);
                }
                else
                {
                    await _rumaService.CrearRumaAsync(request);
                    await _adjuntosViewer.GuardarAdjuntosTemporalesAsync(_idRumaEstado);
                }

                

                MessageBox.Show("Contrato guardado correctamente la ruma : " + request.Ruma.CodigoRuma, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuardadoExitoso?.Invoke(this, EventArgs.Empty);
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

        private async Task<RumaNuevaRequest> ConstruirRequestRumaNueva()
        {
            var idEmpresa = (int)cboEmpresa.SelectedValue;
            var idProducto = (int)cboProducto.SelectedValue;
            var codigoRuma = txtRuma.Text;

            if (_idRuma == 0)
                codigoRuma = await _rumaService.ObtenerCodigoRumaAsync(idEmpresa, idProducto);

            var ruma = new Ruma
            {
                IdRuma = _idRuma,
                IdContrato = (int)cboContrato.SelectedValue,
                CodigoRuma = codigoRuma,
                Descripcion = txtDescripcion.Text,
                IdProducto = idProducto,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            var estado = new RumaEstado
            {
                IdRumaEstado = _idRumaEstado,
                IdRuma = _idRuma,
                IdClase = (int)cboClase.SelectedValue,
                IdProcedencia = (int)cboProcedencia.SelectedValue,
                IdDeposito = (int)cboDeposito.SelectedValue,
                TmhEstimado = Convert.ToDecimal(txtTmhEstimado.Text),
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            var proceso = new RumaEstadoProceso
            {
                IdEstadoProceso = 0,
                IdRuma = _idRuma,
                EstadoProceso = "Acumulacion"
            };

            return new RumaNuevaRequest { Ruma = ruma, Estado = estado, Procesos = proceso, EsMineral = _codigoClase == "MIS", RumaYaExiste = _idRuma != 0 };
        }
        #endregion

        private async Task CargarRumaActualizar()
        {
            var ruma = await _rumaService.ObtenerRumaPorIdAsync(_idRuma);

            var rumaEstado = await _rumaService.ObtenerEstadoPorIdAsync(_idRumaEstado);

            cboContrato.SelectedValue = ruma.IdContrato;
            var contrato = await _contratoService.ObtenerPorIdAsync(ruma.IdContrato);

            cboEmpresa.SelectedValue = contrato.IdEmpresa;
            cboEmpresa.Enabled = false;
            cboProveedor.SelectedValue = contrato.IdProveedor;
            cboProveedor.Enabled = false;
            cboProducto.SelectedValue = contrato.IdProducto;
            cboProducto.Enabled = false;

            txtRuma.Text = ruma.CodigoRuma;
            txtDescripcion.Text = ruma.Descripcion;
            cboClase.SelectedValue = _idClase;
            cboProcedencia.SelectedValue = rumaEstado.IdProcedencia;
            cboDeposito.SelectedValue = rumaEstado.IdDeposito;
            txtTmhEstimado.Text = rumaEstado.TmhEstimado.ToString();

            await CargarTicketsAsync();
        }


        private async void btnAgregarTicket_Click(object sender, EventArgs e)
        {
            // Validar selección de proveedor, clase y producto
            if (cboProveedor.SelectedValue == null || (int)cboProveedor.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboClase.SelectedValue == null || (int)cboClase.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar una clase.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboProducto.SelectedValue == null || (int)cboProducto.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var idProveedor = (int)cboProveedor.SelectedValue;
            var idClase = (int)cboClase.SelectedValue;
            var idProducto = (int)cboProducto.SelectedValue;

            var tickets = await _ticketService.ListarTickestParaRumaAsync(idProveedor, idClase, idProducto);
            var selector = new SelectorTicketForm(tickets);
            var listaTickets = new List<RumaTicket>();

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;

                if (_idRumaEstado == 0)
                {

                }
                else
                {
                    listaTickets.Add(new RumaTicket()
                    {
                        IdRuma = _idRuma,
                        IdRumaEstado = _idRumaEstado,
                        IdTicket = seleccionado.IdTicket,
                        Estado = 'A',
                        UsuarioCreacion = Session.UsuarioActual.NombreUsuario
                    });

                   await _rumaService.InsertarTicketRumasAsync(listaTickets);
                }

                await CargarTicketsAsync();

            }
        }

        private async void btnEliminarTicket_Click(object sender, EventArgs e)
        {
            if (dgvRumaTicket.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un ticket para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idRumaTicket = Convert.ToInt32(dgvRumaTicket.CurrentRow.Cells["IdRumaTicket"].Value);

            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este ticket?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                await _rumaService.EliminarTicketRumaAsync(idRumaTicket);
                await CargarTicketsAsync();
                MessageBox.Show("Ticket eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async Task CargarTicketsAsync()
        {
            try
            {
                var ticketsRuma = (await _rumaService.ListarTicketAsync(_idRumaEstado)).ToList();
                dgvRumaTicket.DataSource = null;
                dgvRumaTicket.DataSource = ticketsRuma;
                dgvRumaTicket.ConfigurarGenerico(ticketsRuma,false);
                CalcularSumaPesoNeto();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task CargarTipoAdjuntoRumaEstadoAsync()
        {
            _adjuntosViewer = new AdjuntosViewerControl
            {
                Dock = DockStyle.Fill
            };

            panel2.Controls.Add(_adjuntosViewer);
            
            var adjuntoRumaEstado = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync(_codigoClase=="MIS"? "ADJUNTOMINERAL": "ADJUNTOCONCENTRADO");

            await _adjuntosViewer.InicializarAsync(_adjuntoService, "RumaEstado", _idRumaEstado, adjuntoRumaEstado);

        }
        private void CalcularSumaPesoNeto()
        {
            decimal suma = 0;

            foreach (DataGridViewRow row in dgvRumaTicket.Rows)
            {
                if (row.Cells["PesoNeto"].Value != null &&
                    decimal.TryParse(row.Cells["PesoNeto"].Value.ToString(), out decimal peso))
                {
                    suma += peso;
                }
            }

            txtTmh.Text = suma.ToString("N2"); // O muestra donde quieras
        }

        private async void btnAdjuntarTicket_Click(object sender, EventArgs e)
        {

            if (dgvRumaTicket.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un ticket para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTicket = Convert.ToInt32(dgvRumaTicket.CurrentRow.Cells["IdTicket"].Value);


            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = "Todos los archivos|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string rutaOrigen = ofd.FileName;
                    string nombreArchivo = Path.GetFileName(rutaOrigen);
                    string tipoDocumento = cboAdjuntoTicket.ComboBox.SelectedValue?.ToString();

                    if (string.IsNullOrEmpty(tipoDocumento))
                    {
                        MessageBox.Show("Debe seleccionar un tipo de documento.");
                        return;
                    }

                    try
                    {
                        var adjunto = new Adjunto
                        {
                            TablaReferencia = "Ticket",
                            IdReferencia = idTicket,
                            NombreArchivo = nombreArchivo,
                            UrlArchivo = rutaOrigen,
                            TipoDocumento = tipoDocumento,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Session.UsuarioActual.NombreUsuario
                        };

                       
                        await _adjuntoService.CrearAdjuntoAsync(adjunto);

                        dgvRumaTicket.CurrentRow.Cells[tipoDocumento=="TKT"? "AdjuntTK":tipoDocumento== "GRE"? "AdjuntoGR": "AdjuntoGT"].Value = rutaOrigen;


                        MessageBox.Show("Archivo adjuntado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al agregar el adjunto: " + ex.Message);
                    }
                }
            }
        }
    }
}
