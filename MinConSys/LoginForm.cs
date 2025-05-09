using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Services;
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
using static System.Collections.Specialized.BitVector32;

namespace MinConSys
{
    public partial class LoginForm : Form
    {
        private readonly ILoginService _loginService;
        public LoginForm(ILoginService loginService)
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.Fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch; // Puedes
            _loginService = loginService;
            this.AcceptButton = btnAceptar;
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtClave.Text))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                // Intentar autenticar al usuario
                var usuario = await _loginService.Autenticacion(txtUsuario.Text, txtClave.Text);

                if (usuario != null)
                {
                    // Almacenar información del usuario en la sesión
                    Session.UsuarioActual = usuario;

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error de autenticación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Text = "";
                    txtClave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnAceptar.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
