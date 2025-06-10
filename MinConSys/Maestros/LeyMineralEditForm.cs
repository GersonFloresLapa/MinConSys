using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
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

namespace MinConSys.Maestros
{
    public partial class LeyMineralEditForm : Form
    {
        private readonly ILeyService _leyService;
        private readonly IRumaService _rumaService;
        public readonly ITicketService _ticketService;
        public readonly IEmpresaService _empresaService;
        public readonly ITablaGeneralesService _tablaGeneralesService;
        public readonly IClaseService _claseService;
        public readonly ILocalidadService _localidadService;
        public readonly IAdjuntoService _adjuntoService;
        private List<RumaDto> _rumas;
        public int _idRuma;
        public int _idRumaEstado;
        public int _idLey;
        public List<LeyContenido> leyContenidos = new List<LeyContenido>(); 
        public LeyMineralEditForm(
                        ILeyService leyService,
                        IRumaService rumaService,
                        ITicketService ticketService,
                        IEmpresaService empresaService,
                        ITablaGeneralesService tablaGeneralesService,
                        IClaseService claseService,
                        ILocalidadService localidadService,
                        IAdjuntoService adjuntoService,
                        int idLey)
        {
            InitializeComponent();

            _leyService = leyService;
            _rumaService = rumaService;
            _ticketService = ticketService;
            _empresaService = empresaService;
            _tablaGeneralesService = tablaGeneralesService;
            _claseService = claseService;
            _localidadService = localidadService;
            _adjuntoService = adjuntoService;
            _idLey = idLey;
        }

        private async void LeyTicket_Load(object sender, EventArgs e)
        {
            _rumas = await _rumaService.ListarRumasByClaseAsync("MIS");

            await CargarCombosAsync();
            await CargarGridView();

        }

        private async void btnBuscarRuma_Click(object sender, EventArgs e)
        {
            var selector = new SelectorRumaForm(_rumas);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                _idRuma = seleccionado.IdRuma;
                _idRumaEstado = seleccionado.IdRumaEstado;
                txtRuma.Text = seleccionado.Ruma;
                txtEmpresa.Text = seleccionado.Empresa + " - "+seleccionado.RazonSocialEmpresa;
                txtProveedor.Text = seleccionado.Proveedor + " - " + seleccionado.RazonSocialProveedor;

                var tickets = await _rumaService.ListarTicketAsync(_idRuma);

                dgvTickets.DataSource = tickets;
            }
        }
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

            var laboratorioTask = _empresaService.ListarEmpresasTiposAsync("PROLAB");
            var elementosTask = _tablaGeneralesService.ObtenerPorTipoGeneralAsync("ELEMENTOS");

            await Task.WhenAll(laboratorioTask, elementosTask);

            var elementos = elementosTask.Result;
            var laboratorios = laboratorioTask.Result;

            ConfigurarComboBox(cboLaboratorio, laboratorios, "Descripcion", "Id");
            ConfigurarComboBox(cboElemento.ComboBox, elementos, "Valor", "Codigo");

        }

        private async Task CargarGridView(){

            dgvTickets.Columns.Clear();
            dgvTickets.AutoGenerateColumns = false;
            // 1. Columna CheckBox
            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
            chkColumn.DataPropertyName = "Seleccionar";
            chkColumn.HeaderText = "";
            chkColumn.Width = 30;
            dgvTickets.Columns.Add(chkColumn);

            // 2. Columna Ticket (texto)
            DataGridViewTextBoxColumn ticketColumn = new DataGridViewTextBoxColumn();
            ticketColumn.DataPropertyName = "NroTicket";
            ticketColumn.HeaderText = "Ticket";
            ticketColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTickets.Columns.Add(ticketColumn);

            // 3. Columna Peso (texto o numérica)
            DataGridViewTextBoxColumn pesoColumn = new DataGridViewTextBoxColumn();
            pesoColumn.DataPropertyName = "PesoNeto";
            pesoColumn.HeaderText = "Peso (kg)";
            pesoColumn.Width = 100;
            dgvTickets.Columns.Add(pesoColumn);

            DataGridViewTextBoxColumn idTicketColumn = new DataGridViewTextBoxColumn();
            idTicketColumn.DataPropertyName = "idTicket";
            idTicketColumn.HeaderText = "Id";
            idTicketColumn.Width = 100;
            dgvTickets.Columns.Add(idTicketColumn);




            dgvLeyes.Columns.Clear();
            dgvLeyes.AutoGenerateColumns = false;


            // 2. Columna Ticket (texto)
            DataGridViewTextBoxColumn elementoColumn = new DataGridViewTextBoxColumn();
            elementoColumn.DataPropertyName = "Elemento";
            elementoColumn.HeaderText = "Elemento";
            elementoColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLeyes.Columns.Add(elementoColumn);

            // 3. Columna Peso (texto o numérica)
            DataGridViewTextBoxColumn contenidoColumn = new DataGridViewTextBoxColumn();
            contenidoColumn.DataPropertyName = "Contenido";
            contenidoColumn.HeaderText = "Contenido";
            contenidoColumn.Width = 100;
            dgvLeyes.Columns.Add(contenidoColumn);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var leyContenido = new LeyContenido();

            leyContenido.Elemento = cboElemento.ComboBox.SelectedValue.ToString();
            leyContenido.Contenido = Convert.ToDecimal(txtElemento.Text);

            leyContenidos.Add(leyContenido);

            dgvLeyes.DataSource = null;
            dgvLeyes.DataSource = leyContenidos;
        }
    }
}
