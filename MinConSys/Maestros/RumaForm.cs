using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Helpers;
using MinConSys.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys
{
    public partial class RumaForm : Form
    {
        private readonly IRumaService _rumaService;
        private readonly string _codigoClase;
        private List<RumaDto> _rumas;
        public readonly ITicketService _ticketService ;
        public readonly IEmpresaService _empresaService ;
        public readonly ITablaGeneralesService _tablaGeneralesService ;
        public readonly IProductoService _productoService ;
        public readonly IClaseService _claseService ;
        public readonly IContratoService _contratoService ;
        public readonly ILocalidadService _localidadService ;
        public readonly IAdjuntoService _adjuntoService ;

        public RumaForm(IRumaService rumaService, 
                        ITicketService ticketService, 
                        IEmpresaService empresaService, 
                        ITablaGeneralesService tablaGeneralesService, 
                        IProductoService productoService, 
                        IClaseService claseService, 
                        IContratoService contratoService, 
                        ILocalidadService localidadService, 
                        IAdjuntoService adjuntoService, 
                        string codigoClase)
        {
            InitializeComponent();

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
        }

        private async void RumaForm_Load(object sender, EventArgs e)
        {
            await CargarRumasAsync();
            dgvRumas.ConfigurarGenerico(_rumas);
        }
        private async Task CargarRumasAsync()
        {
            try
            {
                _rumas = (await _rumaService.ListarRumasByClaseAsync(_codigoClase)).ToList();
                dgvRumas.DataSource = null;
                dgvRumas.DataSource = _rumas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var form = new RumaEditForm(
                _rumaService,
                _ticketService,
                _empresaService,
                _tablaGeneralesService,
                _productoService,
                _claseService,
                _contratoService,
                _localidadService,
                _adjuntoService,
                _codigoClase,
                0,
                0);

            form.MdiParent = this.MdiParent; // <-- importante: usar el contenedor principal
            form.GuardadoExitoso += async (s, args) =>
            {
                await CargarRumasAsync(); // solo si se guardó algo
            };

            form.Show(); // No ShowDialog
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idRuma = Convert.ToInt32(dgvRumas.CurrentRow.Cells["IdRuma"].Value);
            int idRumaEstado = Convert.ToInt32(dgvRumas.CurrentRow.Cells["IdRumaEstado"].Value);
            var form = new RumaEditForm(
                _rumaService,
                _ticketService,
                _empresaService,
                _tablaGeneralesService,
                _productoService,
                _claseService,
                _contratoService,
                _localidadService,
                _adjuntoService,
                _codigoClase,
                idRuma,
                idRumaEstado);

            form.MdiParent = this.MdiParent; // <-- importante: usar el contenedor principal
            form.GuardadoExitoso += async (s, args) =>
            {
                await CargarRumasAsync(); // solo si se guardó algo
            };

            form.Show(); // No ShowDialog
        }
    }
}
