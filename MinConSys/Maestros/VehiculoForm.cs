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
    public partial class VehiculoForm : Form
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IEmpresaService _empresaService;
        private List<Vehiculo> _vehiculos;
        public VehiculoForm(IVehiculoService vehiculoService, IEmpresaService empresaService)
        {
            InitializeComponent();
            _vehiculoService = vehiculoService;
            _empresaService = empresaService;

        }

        private async void VehiculoForm_Load(object sender, EventArgs e)
        {
            await CargarVehiculosAsync();
            dgvVehiculos.ConfigurarGenerico();
        }
        private async Task CargarVehiculosAsync()
        {
            try
            {
                _vehiculos = (await _vehiculoService.ListarVehiculosAsync()).ToList();
                dgvVehiculos.DataSource = null;
                dgvVehiculos.DataSource = _vehiculos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new VehiculoEditForm(_vehiculoService, _empresaService, 0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarVehiculosAsync(); // Vuelves a cargar la lista
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idVehiculo = Convert.ToInt32(dgvVehiculos.CurrentRow.Cells["IdVehiculo"].Value);
            using (var form = new VehiculoEditForm(_vehiculoService, _empresaService, idVehiculo))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarVehiculosAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
