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
    public partial class UsuarioForm : Form
    {
        private readonly IUsuarioService _usuarioService;
        private List<Usuario> _usuarios;
        public UsuarioForm(IUsuarioService usuarioService)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
 
        }
        private async void UsuarioForm_Load(object sender, EventArgs e)
        {
            await CargarUsuariosAsync();
            dgvUsuarios.ConfigurarGenerico();
        }
        private async Task CargarUsuariosAsync()
        {
            try
            {
                _usuarios = (await _usuarioService.ListarUsuariosAsync()).ToList();
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = _usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new UsuarioEditForm(_usuarioService,0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarUsuariosAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
            using (var form = new UsuarioEditForm(_usuarioService, idUsuario))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarUsuariosAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
