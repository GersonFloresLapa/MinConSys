﻿using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Request;
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
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private readonly int _idPersona;
        private List<TablaGeneralesCombo> _tipoDocumento;
        public PersonaEditForm(IPersonaService personaService, ITablaGeneralesService tablaGeneralesService, int idPersona)
        {
            InitializeComponent();
            _personaService = personaService;
            _tablaGeneralesService = tablaGeneralesService;
            _idPersona = idPersona;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevaPersona = new Persona
            {
                IdPersona = _idPersona,
                TipoDocumento = cboTipoDocumento.SelectedValue.ToString(),
                NumeroDocumento = txtNumeroDocumento.Text,
                Nombres = txtNombres.Text,
                ApellidoPaterno = txtApellidoPaterno.Text,
                ApellidoMaterno = txtApellidoMaterno.Text,
                CorreoElectronico = txtCorreoElectronico.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                Brevete = txtBrevete.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idPersona != 0)
                {
                    await _personaService.ActualizarPersonaAsync(nuevaPersona);
                }
                else
                {
                    await _personaService.CrearPersonaAsync(nuevaPersona);
                }
               
                MessageBox.Show("Persona registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void PersonaEditForm_Load(object sender, EventArgs e)
        {
            await CargarTipoDocumentoAsync();

            if (_idPersona != 0)
            {
                var persona = await _personaService.ObtenerPorIdAsync(_idPersona);

                if (persona != null)
                {
                    txtNumeroDocumento.Text = persona.NumeroDocumento;
                    cboTipoDocumento.Text = persona.TipoDocumento;
                    txtNombres.Text = persona.Nombres;
                    txtApellidoPaterno.Text = persona.ApellidoPaterno;
                    txtApellidoMaterno.Text = persona.ApellidoMaterno;
                    txtCorreoElectronico.Text = persona.CorreoElectronico;
                    txtTelefono.Text = persona.Telefono;
                    txtDireccion.Text = persona.Direccion;
                    txtBrevete.Text = persona.Brevete;
                }
            }
        }

        private async Task CargarTipoDocumentoAsync()
        {

            _tipoDocumento = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPODOCUMENTO");

            cboTipoDocumento.DataSource = _tipoDocumento;
            cboTipoDocumento.DisplayMember = "Valor";
            cboTipoDocumento.ValueMember = "Codigo";

        }
    }
}
