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
    public partial class PersonaEditForm : Form
    {
        private readonly IPersonaService _personaService;
        public PersonaEditForm(IPersonaService personaService)
        {
            InitializeComponent();

            _personaService = personaService;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var nuevaPersona = new Persona
            {

                TipoDocumento = cboTipoDocumento.Text.ToString(),
                NumeroDocumento = txtNumeroDocumento.Text.ToString(),
                Nombres = txtNombres.Text.ToString(),
                ApellidoPaterno = txtApellidoPaterno.Text.ToString(),
                ApellidoMaterno = txtApellidoMaterno.Text.ToString(),
                CorreoElectronico = txtCorreoElectronico.Text.ToString(),
                Telefono = txtTelefono.Text.ToString(),
                Direccion = txtDireccion.Text.ToString(),
                Estado = cboEstado.Text.ToString(),
                Brevete = txtBrevete.Text.ToString(),
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                await _personaService.CrearPersonaAsync(nuevaPersona);

                MessageBox.Show("Persona registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
