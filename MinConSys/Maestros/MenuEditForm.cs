using MinConSys.Core.Interfaces.Services;
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
    public partial class MenuEditForm : Form
    {
        private readonly IMenuService _menuService;
        private readonly int _idMenu;

        public MenuEditForm(IMenuService menuService, int idMenu)
        {
            InitializeComponent();
            _menuService = menuService;
            _idMenu = idMenu;
        }

        private async void MenuEditForm_Load(object sender, EventArgs e)
        {
            if (_idMenu != 0)
            {
                var menu = await _menuService.ObtenerPorIdAsync(_idMenu);
                if (menu != null)
                {
                    txtNombre.Text = menu.Nombre;
                    txtNombreInterno.Text = menu.NombreInterno;
                    txtOrden.Text = menu.Orden.ToString();
                    txtPadreId.Text = menu.PadreId?.ToString();
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

            var menu = new Core.Models.Base.Menu
            {
                IdMenu = _idMenu,
                Nombre = txtNombre.Text,
                NombreInterno = txtNombreInterno.Text,
                Orden = int.Parse(txtOrden.Text),
                PadreId = !string.IsNullOrEmpty(txtPadreId.Text) ? Convert.ToInt32(txtPadreId.Text) : 0,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idMenu != 0)
                    await _menuService.ActualizarMenuAsync(menu);
                else
                    await _menuService.CrearMenuAsync(menu);

                MessageBox.Show("Menú guardado correctamente.", "Éxito");
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
