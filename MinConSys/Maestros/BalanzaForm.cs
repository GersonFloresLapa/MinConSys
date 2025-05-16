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
    public partial class BalanzaForm : Form
    {
        private readonly IBalanzaService _balanzaService;
        private List<Balanza> _balanzas;
        public BalanzaForm(IBalanzaService balanzaService)
        {
            InitializeComponent();
            _balanzaService = balanzaService;
 
        }
        private async void BalanzaForm_Load(object sender, EventArgs e)
        {
            await CargarBalanzasAsync();
            dgvBalanzas.ConfigurarGenerico(_balanzas);
        }
        private async Task CargarBalanzasAsync()
        {
            try
            {
                _balanzas = (await _balanzaService.ListarBalanzasAsync()).ToList();
                dgvBalanzas.DataSource = null;
                dgvBalanzas.DataSource = _balanzas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new BalanzaEditForm(_balanzaService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarBalanzasAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idBalanza = Convert.ToInt32(dgvBalanzas.CurrentRow.Cells["IdBalanza"].Value);
            using (var form = new BalanzaEditForm(_balanzaService, idBalanza))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarBalanzasAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
