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
    public partial class ContratoForm : Form
    {
        private readonly IContratoService _contratoService;
        private readonly IEmpresaService _empresaService;
        private List<ContratoDto> _contratos;
        public ContratoForm(IContratoService contratoService, IEmpresaService empresaService)
        {
            InitializeComponent();
            _contratoService = contratoService;
            _empresaService = empresaService;
        }

        private async void ContratoForm_Load(object sender, EventArgs e)
        {
            await CargarContratosAsync();
            dgvContratos.ConfigurarGenerico(_contratos);
        }
        private async Task CargarContratosAsync()
        {
            try
            {
                _contratos = (await _contratoService.ListarContratosAsync()).ToList();
                dgvContratos.DataSource = null;
                dgvContratos.DataSource = _contratos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new ContratoEditForm(_contratoService,_empresaService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarContratosAsync(); // Vuelves a cargar la lista
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idContrato = Convert.ToInt32(dgvContratos.CurrentRow.Cells["IdContrato"].Value);
            using (var form = new ContratoEditForm(_contratoService,_empresaService, idContrato))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarContratosAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
