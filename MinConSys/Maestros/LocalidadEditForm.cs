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
        private readonly int _idLocalidad;
        public LocalidadEditForm(ILocalidadService localidadService, int idLocalidad)
        {
            InitializeComponent();

            _localidadService = localidadService;
            _idLocalidad = idLocalidad;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoRegistro = new Localidad
            {
                IdLocalidad     = _idLocalidad,
                IdEmpresa       = int.Parse(cboEmpresa.SelectedItem.ToString()),
                TipoLocalidad   = cboTipoLocalidad.Text,
                NombreLocalidad = txtNombreLocalidad.Text,
                Direccion       = txtDireccion.Text,
                Ubigeo          = txtUbigeo.Text,
                Estado          = cboEstado.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idLocalidad != 0)
                {
                    await _localidadService.ActualizarLocalidadAsync(nuevoRegistro);
                }
                else
                {
                    await _localidadService.CrearLocalidadAsync(nuevoRegistro);
                }

                MessageBox.Show("Registro Localidad correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private async void LocalidadEditForm_Load(object sender, EventArgs e)
        {
            if (_idLocalidad != 0)
            {
                var registro = await _localidadService.ObtenerPorIdAsync(_idLocalidad);

                if (registro != null)
                {

                    cboEmpresa.Text = registro.IdEmpresa.ToString();
                    cboTipoLocalidad.Text = registro.TipoLocalidad;
                    txtNombreLocalidad.Text = registro.NombreLocalidad;
                    txtDireccion.Text = registro.Direccion;
                    txtUbigeo.Text = registro.Ubigeo;
                    cboEstado.Text = registro.Estado;
                }
            }
        }
    }
}
