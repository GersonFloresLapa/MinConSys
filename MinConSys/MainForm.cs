using Microsoft.Extensions.DependencyInjection;
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


        private void MainForm_Load(object sender, EventArgs e)
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
