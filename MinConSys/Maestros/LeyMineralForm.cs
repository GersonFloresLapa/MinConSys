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
    public partial class LeyMineralForm : Form
    {
        private readonly ILeyService _leyService;
        private readonly IRumaService _rumaService;
        public readonly ITicketService _ticketService;
        public readonly IEmpresaService _empresaService;
        public readonly ITablaGeneralesService _tablaGeneralesService;
        public readonly IClaseService _claseService;
        public readonly ILocalidadService _localidadService;
        public readonly IAdjuntoService _adjuntoService;
        private List<Ley> _leyMinerals;
        public LeyMineralForm(
                        ILeyService leyService,
                        IRumaService rumaService,
                        ITicketService ticketService,
                        IEmpresaService empresaService,
                        ITablaGeneralesService tablaGeneralesService,
                        IClaseService claseService,
                        ILocalidadService localidadService,
                        IAdjuntoService adjuntoService)
        {
            InitializeComponent();

            _leyService             = leyService;
            _rumaService            = rumaService;
            _ticketService          = ticketService;
            _empresaService         = empresaService;
            _tablaGeneralesService  = tablaGeneralesService;
            _claseService           = claseService;
            _localidadService       = localidadService;
            _adjuntoService         = adjuntoService;
        }
        private async void LeyMineralForm_Load(object sender, EventArgs e)
        {
            await CargarLeyMineralsAsync();
            dgvLeyMinerals.ConfigurarGenerico(_leyMinerals);
        }
        private async Task CargarLeyMineralsAsync()
        {
            try
            {
                _leyMinerals = (await _leyService.ListarLeyesAsync()).ToList();
                dgvLeyMinerals.DataSource = null;
                dgvLeyMinerals.DataSource = _leyMinerals;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new LeyMineralEditForm(_leyService, _rumaService, _ticketService,_empresaService,_tablaGeneralesService,_claseService,_localidadService,_adjuntoService, 0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarLeyMineralsAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idLey = Convert.ToInt32(dgvLeyMinerals.CurrentRow.Cells["IdLey"].Value);
            using (var form = new LeyMineralEditForm(_leyService, _rumaService, _ticketService, _empresaService, _tablaGeneralesService, _claseService, _localidadService, _adjuntoService,idLey))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarLeyMineralsAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
