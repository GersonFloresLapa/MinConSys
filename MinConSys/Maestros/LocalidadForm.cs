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
        private List<LocalidadDto> _localidad;

        public LocalidadForm(ILocalidadService localidadService)
        {
            InitializeComponent();
            _localidadService = localidadService;
 
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
            using (var form = new LocalidadEditForm(_localidadService,0 ))
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
            using (var form = new LocalidadEditForm(_localidadService , idLocalidad))
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
