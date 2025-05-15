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
    public partial class RolEditForm : Form
    {
        private readonly IRolService _rolService;
        private readonly int _idRol;

        public RolEditForm(IRolService rolService, int idRol)
        {
            InitializeComponent();
            _rolService = rolService;
            _idRol = idRol;
        }

        private async void RolEditForm_Load(object sender, EventArgs e)
        {
            if (_idRol != 0)
            {
                var rol = await _rolService.ObtenerPorIdAsync(_idRol);
                if (rol != null)
                {
                    txtNombreRol.Text = rol.NombreRol;
                    txtDescripcion.Text = rol.Descripcion;
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var rol = new Rol
            {
                IdRol = _idRol,
                NombreRol = txtNombreRol.Text,
                Descripcion = txtDescripcion.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idRol != 0)
                    await _rolService.ActualizarRolAsync(rol);
                else
                    await _rolService.CrearRolAsync(rol);

                MessageBox.Show("Rol guardado correctamente.", "Éxito");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error");
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }
    }
}
