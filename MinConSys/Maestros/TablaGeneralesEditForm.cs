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
    public partial class TablaGeneralesEditForm : Form
    {
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private readonly int _idGeneral;

        public TablaGeneralesEditForm(ITablaGeneralesService tablaGeneralesService, int idGeneral)
        {
            InitializeComponent();
            _tablaGeneralesService = tablaGeneralesService;
            _idGeneral = idGeneral;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoRegistro = new TablaGenerales
            {
                IdGeneral = _idGeneral,
                TipoGeneral = txtTipoGeneral.Text,
                Codigo = txtCodigo.Text,
                Valor = txtValor.Text,
                Descripcion = txtDescripcion.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idGeneral != 0)
                {
                    await _tablaGeneralesService.ActualizarTablaGeneralesAsync(nuevoRegistro);
                }
                else
                {
                    await _tablaGeneralesService.CrearTablaGeneralesAsync(nuevoRegistro);
                }

                MessageBox.Show("Registro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void TablaGeneralesEditForm_Load(object sender, EventArgs e)
        {
            if (_idGeneral != 0)
            {
                var registro = await _tablaGeneralesService.ObtenerPorIdAsync(_idGeneral);

                if (registro != null)
                {
                    txtTipoGeneral.Text = registro.TipoGeneral;
                    txtCodigo.Text = registro.Codigo;
                    txtValor.Text = registro.Valor;
                    txtDescripcion.Text = registro.Descripcion;
                }
            }
        }
    }

}
