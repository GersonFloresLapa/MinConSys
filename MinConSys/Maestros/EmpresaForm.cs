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
    public partial class EmpresaForm : Form
    {
        private readonly IEmpresaService _empresaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private List<Empresa> _empresas;
        public EmpresaForm(IEmpresaService empresaService, ITablaGeneralesService tablaGeneralesService)
        {
            InitializeComponent();
            _empresaService = empresaService;
            _tablaGeneralesService = tablaGeneralesService;
        }

        private async void EmpresaForm_Load(object sender, EventArgs e)
        {
            await CargarEmpresasAsync();
            dgvEmpresas.ConfigurarGenerico();
        }
        private async Task CargarEmpresasAsync()
        {
            try
            {
                _empresas = (await _empresaService.ListarEmpresasAsync()).ToList();
                dgvEmpresas.DataSource = null;
                dgvEmpresas.DataSource = _empresas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new EmpresaEditForm(_empresaService, _tablaGeneralesService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarEmpresasAsync(); // Vuelves a cargar la lista
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idEmpresa = Convert.ToInt32(dgvEmpresas.CurrentRow.Cells["IdEmpresa"].Value);
            using (var form = new EmpresaEditForm(_empresaService, _tablaGeneralesService, idEmpresa))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarEmpresasAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
