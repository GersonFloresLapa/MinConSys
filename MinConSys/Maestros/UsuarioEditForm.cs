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
    public partial class UsuarioEditForm : Form
    {
        private readonly IUsuarioService _usuarioService;
        private readonly int _idUsuario;

        public UsuarioEditForm(IUsuarioService usuarioService, int idUsuario)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _idUsuario = idUsuario;
        }

        private async void UsuarioEditForm_Load(object sender, EventArgs e)
        {
            if (_idUsuario != 0)
            {
                var usuario = await _usuarioService.ObtenerPorIdAsync(_idUsuario);
                if (usuario != null)
                {
                    txtNombreUsuario.Text = usuario.NombreUsuario;
                    txtClave.Text = usuario.Clave;
                    txtNombres.Text = usuario.Nombres;
                    txtApellidoPaterno.Text = usuario.ApellidoPaterno;
                    txtApellidoMaterno.Text = usuario.ApellidoMaterno;
                    cboRol.SelectedValue = usuario.IdRol;
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

            var usuario = new Usuario
            {
                IdUsuario = _idUsuario,
                IdRol = Convert.ToInt32(cboRol.SelectedValue),
                NombreUsuario = txtNombreUsuario.Text,
                Clave = txtClave.Text,
                Nombres = txtNombres.Text,
                ApellidoPaterno = txtApellidoPaterno.Text,
                ApellidoMaterno = txtApellidoMaterno.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idUsuario != 0)
                    await _usuarioService.ActualizarUsuarioAsync(usuario);
                else
                    await _usuarioService.CrearUsuarioAsync(usuario);

                MessageBox.Show("Usuario guardado correctamente.", "Éxito");
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
