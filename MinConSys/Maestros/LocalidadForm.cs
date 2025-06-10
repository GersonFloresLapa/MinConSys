using MinConSys.Core.Interfaces.Services;
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
    public partial class LocalidadForm : Form
    {
        private readonly ILocalidadService _localidadService;
        private readonly IEmpresaService _empresaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private List<LocalidadDto> _localidad;

        public LocalidadForm(ILocalidadService localidadService, IEmpresaService empresaService, ITablaGeneralesService tablaGeneralesService)
        {
            InitializeComponent();
            _localidadService = localidadService;
            _empresaService = empresaService;
            _tablaGeneralesService = tablaGeneralesService;

        }

        private async void LocalidadForm_Load(object sender, EventArgs e)
        {
            await CargarLocalidadesAsync();
            dgvLocalidades.ConfigurarGenerico(_localidad);
        }
        private async Task CargarLocalidadesAsync()
        {
            try
            {
                _localidad = (await _localidadService.ListarLocalidadesAsync()).ToList();
                dgvLocalidades.DataSource = null;
                dgvLocalidades.DataSource = _localidad;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new LocalidadEditForm(_localidadService,_empresaService, _tablaGeneralesService,0))
            {

                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarLocalidadesAsync(); // Vuelves a cargar la lista
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idLocalidad = Convert.ToInt32(dgvLocalidades.CurrentRow.Cells["IdLocalidad"].Value);
            using (var form = new LocalidadEditForm(_localidadService, _empresaService, _tablaGeneralesService, idLocalidad))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarLocalidadesAsync(); // Vuelves a cargar la lista
                }
            }
        }

    }
}
