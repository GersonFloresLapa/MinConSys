using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
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
    public partial class BalanzaEditForm : Form
    {
        private readonly IBalanzaService _balanzaService;
        private readonly int _idBalanza;

        public BalanzaEditForm(IBalanzaService balanzaService, int idBalanza)
        {
            InitializeComponent();
            _balanzaService = balanzaService;
            _idBalanza = idBalanza;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevaBalanza = new Balanza
            {
                IdBalanza = _idBalanza,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Direccion = txtDireccion.Text,
                Ubigeo = txtUbigeo.Text,
                Unidad = txtUnidad.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idBalanza != 0)
                {
                    await _balanzaService.ActualizarBalanzaAsync(nuevaBalanza);
                }
                else
                {
                    await _balanzaService.CrearBalanzaAsync(nuevaBalanza);
                }

                MessageBox.Show("Balanza registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void BalanzaEditForm_Load(object sender, EventArgs e)
        {
            if (_idBalanza != 0)
            {
                var balanza = await _balanzaService.ObtenerPorIdAsync(_idBalanza);

                if (balanza != null)
                {
                    txtNombre.Text = balanza.Nombre;
                    txtDescripcion.Text = balanza.Descripcion;
                    txtDireccion.Text = balanza.Direccion;
                    txtUbigeo.Text = balanza.Ubigeo;
                    txtUnidad.Text = balanza.Unidad;
                }
            }
        }
    }


}
