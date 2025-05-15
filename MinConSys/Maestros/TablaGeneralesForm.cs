using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
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
    public partial class TablaGeneralesForm : Form
    {
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private List<TablaGeneralesDto> _tablaGenerales;
        public TablaGeneralesForm(ITablaGeneralesService tablaGeneralesService)
        {
            InitializeComponent();
            _tablaGeneralesService = tablaGeneralesService;
 
        }
        private async void TablaGeneralesForm_Load(object sender, EventArgs e)
        {
            await CargarTablaGeneralessAsync();
            dgvTablaGenerales.ConfigurarGenerico();
        }
        private async Task CargarTablaGeneralessAsync()
        {
            try
            {
                _tablaGenerales = (await _tablaGeneralesService.ListarTablaGeneralesAsync()).ToList();
                dgvTablaGenerales.DataSource = null;
                dgvTablaGenerales.DataSource = _tablaGenerales;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new TablaGeneralesEditForm(_tablaGeneralesService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarTablaGeneralessAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idTablaGenerales = Convert.ToInt32(dgvTablaGenerales.CurrentRow.Cells["IdGeneral"].Value);
            using (var form = new TablaGeneralesEditForm(_tablaGeneralesService, idTablaGenerales))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarTablaGeneralessAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
