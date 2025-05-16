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
    public partial class RolForm : Form
    {
        private readonly IRolService _rolService;
        private List<Rol> _rols;
        public RolForm(IRolService rolService)
        {
            InitializeComponent();
            _rolService = rolService;
 
        }
        private async void RolForm_Load(object sender, EventArgs e)
        {
            await CargarRolsAsync();
            dgvRols.ConfigurarGenerico(_rols);
        }
        private async Task CargarRolsAsync()
        {
            try
            {
                _rols = (await _rolService.ListarRolesAsync()).ToList();
                dgvRols.DataSource = null;
                dgvRols.DataSource = _rols;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new RolEditForm(_rolService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarRolsAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idRol = Convert.ToInt32(dgvRols.CurrentRow.Cells["IdRol"].Value);
            using (var form = new RolEditForm(_rolService, idRol))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarRolsAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
