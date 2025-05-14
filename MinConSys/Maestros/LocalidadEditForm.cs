using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys.Maestros
{
    public partial class LocalidadEditForm : Form
    {
        private readonly ILocalidadService _localidadService;
        public LocalidadEditForm(ILocalidadService localidadService)
        {
            InitializeComponent();

            _localidadService = localidadService;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var nuevaLocalidad = new Localidad
            {
                IdEmpresa= Convert.ToInt32(cboEmpresa.Text),
                TipoLocalidad =cboTipoLocalidad.Text.ToString(),
                NombreLocalidad = txtNombreLocalidad.Text.ToString(),
                Direccion = txtDireccion.Text.ToString(),
                Ubigeo=txtUbigeo.Text.ToString(),
                Estado = cboEstado.Text.ToString(),
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                await _localidadService.CrearLocalidadAsync(nuevaLocalidad);

                MessageBox.Show("Localidad registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
