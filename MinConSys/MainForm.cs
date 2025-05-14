using Microsoft.Extensions.DependencyInjection;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Dto;
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

namespace MinConSys
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.Fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch; // Puedes
            _serviceProvider = serviceProvider;
            this.IsMdiContainer = true;

        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Sistema de Gestión Minero";
            
            lblUsuario.Text = $"Usuario: {Session.UsuarioActual.NombreCompleto}";
            lblRol.Text = $"Rol: {Session.UsuarioActual.NombreRol}"; // ajusta según tu clase Usuario
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // Opcional: actualizar la hora cada segundo
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) => {
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            timer.Start();

            var _menuService = _serviceProvider.GetRequiredService<IMenuService>();

            var menuItems = await _menuService.ObtenerMenuPermitido(Session.UsuarioActual.NombreUsuario);

            ConstruirMenu(menuItems);
        }

        private void ConstruirMenu(List<MenuDto> menuItems)
        {
            menuStrip1.Items.Clear();

            foreach (var item in menuItems.Where(x => x.PadreId == 0).OrderBy(x => x.Orden))
            {
                var menuItem = CrearToolStripMenuItem(item);
                menuStrip1.Items.Add(menuItem);
            }
        }

        private ToolStripMenuItem CrearToolStripMenuItem(MenuDto item)
        {
            var tsItem = new ToolStripMenuItem
            {
                Text = item.Nombre,
                Tag = item.NombreInterno
            };

            var hijos = item.Hijos;

            if (hijos.Count > 0)
            {
                foreach (var hijo in hijos)
                    tsItem.DropDownItems.Add(CrearToolStripMenuItem(hijo));
            }
            else
            {
                tsItem.Click += MenuItem_Click;
            }

            return tsItem;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                string nombreInterno = item.Tag.ToString();

                switch (nombreInterno)
                {
                    case "Persona":
                        var personaForm = new PersonaForm(_serviceProvider.GetRequiredService<IPersonaService>());
                        AbrirFormularioHijo(personaForm);
                        break;
                    case "Empresas":
                        var empresaForm = new EmpresaForm(_serviceProvider.GetRequiredService<IEmpresaService>());
                        AbrirFormularioHijo(empresaForm);
                        break;
                    case "Vehiculo":
                        var vehiculoForm = new VehiculoForm(_serviceProvider.GetRequiredService<IVehiculoService>(), _serviceProvider.GetRequiredService<IEmpresaService>());
                        AbrirFormularioHijo(vehiculoForm);
                        break;
                    case "Contrato":
                        var contratoForm = new ContratoForm(_serviceProvider.GetRequiredService<IContratoService>(), _serviceProvider.GetRequiredService<IEmpresaService>());
                        AbrirFormularioHijo(contratoForm);
                        break;
                    default:
                        break;
                }
            }
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var personaForm = new PersonaForm(_serviceProvider.GetRequiredService<IPersonaService>());
            AbrirFormularioHijo(personaForm);
        }
        private void AbrirFormularioHijo(Form formHijo)
        {

            // Configura el formulario hijo como parte del contenedor
            formHijo.MdiParent = this;
            formHijo.WindowState = FormWindowState.Maximized; // o Normal si prefieres
            formHijo.Show();
        }
    }
}
