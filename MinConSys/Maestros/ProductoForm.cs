using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Helpers;
using MinConSys.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys
{
    public partial class ProductoForm : Form
    {
        private readonly IProductoService _productoService;
        private List<Producto> _productos;
        public ProductoForm(IProductoService productoService)
        {
            InitializeComponent();
            _productoService = productoService;
 
        }
        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            await CargarProductosAsync();
            dgvProductos.ConfigurarGenerico(_productos);
        }
        private async Task CargarProductosAsync()
        {
            try
            {
                _productos = (await _productoService.ListarProductosAsync()).ToList();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = _productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new ProductoEditForm(_productoService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarProductosAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["IdProducto"].Value);
            using (var form = new ProductoEditForm(_productoService, idProducto))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarProductosAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
