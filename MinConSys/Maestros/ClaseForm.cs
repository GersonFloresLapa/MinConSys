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
    public partial class ClaseForm : Form
    {
        private readonly IClaseService _claseService;
        private List<Clase> _clases;
        public ClaseForm(IClaseService claseService)
        {
            InitializeComponent();
            _claseService = claseService;
 
        }
        private async void ClaseForm_Load(object sender, EventArgs e)
        {
            await CargarClasesAsync();
            dgvClases.ConfigurarGenerico(_clases);
        }
        private async Task CargarClasesAsync()
        {
            try
            {
                _clases = (await _claseService.ListarClasesAsync()).ToList();
                dgvClases.DataSource = null;
                dgvClases.DataSource = _clases;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new ClaseEditForm(_claseService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarClasesAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idClase = Convert.ToInt32(dgvClases.CurrentRow.Cells["IdClase"].Value);
            using (var form = new ClaseEditForm(_claseService, idClase))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarClasesAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
