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
    public partial class MenuForm : Form
    {
        private readonly IMenuService _menuService;
        private List<Core.Models.Base.Menu> _menus;
        public MenuForm(IMenuService menuService)
        {
            InitializeComponent();
            _menuService = menuService;
 
        }
        private async void MenuForm_Load(object sender, EventArgs e)
        {
            await CargarMenusAsync();
            dgvMenus.ConfigurarGenerico(_menus);
        }
        private async Task CargarMenusAsync()
        {
            try
            {
                _menus = (await _menuService.ListarMenusAsync()).ToList();
                dgvMenus.DataSource = null;
                dgvMenus.DataSource = _menus;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new MenuEditForm(_menuService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarMenusAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idMenu = Convert.ToInt32(dgvMenus.CurrentRow.Cells["IdMenu"].Value);
            using (var form = new MenuEditForm(_menuService, idMenu))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarMenusAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
