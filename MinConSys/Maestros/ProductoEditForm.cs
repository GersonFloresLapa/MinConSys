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
    public partial class ProductoEditForm : Form
    {
        private readonly IProductoService _productoService;
        private readonly int _idProducto;

        public ProductoEditForm(IProductoService productoService, int idProducto)
        {
            InitializeComponent();
            _productoService = productoService;
            _idProducto = idProducto;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoProducto = new Producto
            {
                IdProducto = _idProducto,
                Nombre = txtNombre.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                Unidad = txtUnidad.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idProducto != 0)
                {
                    await _productoService.ActualizarProductoAsync(nuevoProducto);
                }
                else
                {
                    await _productoService.CrearProductoAsync(nuevoProducto);
                }

                MessageBox.Show("Producto registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void ProductoEditForm_Load(object sender, EventArgs e)
        {
            if (_idProducto != 0)
            {
                var producto = await _productoService.ObtenerPorIdAsync(_idProducto);

                if (producto != null)
                {
                    txtNombre.Text = producto.Nombre;
                    txtNombreCompleto.Text = producto.NombreCompleto;
                    txtPrecio.Text = producto.Precio.ToString("F2");
                    txtUnidad.Text = producto.Unidad;
                }
            }
        }
    }
}
