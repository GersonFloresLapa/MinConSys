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
    public partial class ClaseEditForm : Form
    {
        private readonly IClaseService _claseService;
        private readonly int _idClase;

        public ClaseEditForm(IClaseService claseService, int idClase)
        {
            InitializeComponent();
            _claseService = claseService;
            _idClase = idClase;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevaClase = new Clase
            {
                IdClase = _idClase,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Unidad = txtUnidad.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idClase != 0)
                {
                    await _claseService.ActualizarClaseAsync(nuevaClase);
                }
                else
                {
                    await _claseService.CrearClaseAsync(nuevaClase);
                }

                MessageBox.Show("Clase registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void ClaseEditForm_Load(object sender, EventArgs e)
        {
            if (_idClase != 0)
            {
                var clase = await _claseService.ObtenerPorIdAsync(_idClase);

                if (clase != null)
                {
                    txtNombre.Text = clase.Nombre;
                    txtDescripcion.Text = clase.Descripcion;
                    txtUnidad.Text = clase.Unidad;
                }
            }
        }
    }

}
