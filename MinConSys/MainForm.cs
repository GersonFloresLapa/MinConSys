using Microsoft.Extensions.DependencyInjection;
using MinConSys.Core.Interfaces.Services;
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
            //var timer = new Timer();
            //timer.Interval = 1000;
            //timer.Tick += (s, ev) => {
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //};
            //timer.Start();

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
                        var personaForm = new PersonaForm(  _serviceProvider.GetRequiredService<IPersonaService>(), 
                                                            _serviceProvider.GetRequiredService<ITablaGeneralesService>(), 
                                                            _serviceProvider.GetRequiredService<ICuentaBancariaService>(),
                                                            _serviceProvider.GetRequiredService<IAdjuntoService>()
                                                          );
                        AbrirFormularioHijo(personaForm);
                        break;
                    case "Empresas":
                        var empresaForm = new EmpresaForm(  _serviceProvider.GetRequiredService<IEmpresaService>(), 
                                                            _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                            _serviceProvider.GetRequiredService<IAdjuntoService>(),
                                                            _serviceProvider.GetRequiredService<IRepresentanteService>(),
                                                            _serviceProvider.GetRequiredService<IPersonaService>(),
                                                            _serviceProvider.GetRequiredService<ICuentaBancariaService>()
                                                            );
                        AbrirFormularioHijo(empresaForm);
                        break;
                    case "Vehiculo":
                        var vehiculoForm = new VehiculoForm(_serviceProvider.GetRequiredService<IVehiculoService>(), 
                                                            _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                            _serviceProvider.GetRequiredService<ITablaGeneralesService>());
                        AbrirFormularioHijo(vehiculoForm);
                        break;
                    case "Contrato":
                        var contratoForm = new ContratoForm(_serviceProvider.GetRequiredService<IContratoService>(), 
                                                            _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                            _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                            _serviceProvider.GetRequiredService<IProductoService>(),
                                                            _serviceProvider.GetRequiredService<IClaseService>()
                                                            );
                        AbrirFormularioHijo(contratoForm);
                        break;
                    case "Localidad":
                        var localidadForm = new LocalidadForm(_serviceProvider.GetRequiredService<ILocalidadService>(), 
                                                              _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                              _serviceProvider.GetRequiredService<ITablaGeneralesService>()
                                                              );
                        AbrirFormularioHijo(localidadForm);
                        break;
                    case "Clase":
                        var claseForm = new ClaseForm(_serviceProvider.GetRequiredService<IClaseService>());
                        AbrirFormularioHijo(claseForm);
                        break;
                    case "Producto":
                        var productoForm = new ProductoForm(_serviceProvider.GetRequiredService<IProductoService>());
                        AbrirFormularioHijo(productoForm);
                        break;
                    case "Balanza":
                        var balanzaForm = new BalanzaForm(_serviceProvider.GetRequiredService<IBalanzaService>());
                        AbrirFormularioHijo(balanzaForm);
                        break;
                    case "TablaGenerales":
                        var tablaGeneralesForm = new TablaGeneralesForm(_serviceProvider.GetRequiredService<ITablaGeneralesService>());
                        AbrirFormularioHijo(tablaGeneralesForm);
                        break;
                    case "Menu":
                        var menuForm = new MenuForm(_serviceProvider.GetRequiredService<IMenuService>());
                        AbrirFormularioHijo(menuForm);
                        break;
                    case "Rol":
                        var rolForm = new RolForm(_serviceProvider.GetRequiredService<IRolService>());
                        AbrirFormularioHijo(rolForm);
                        break;
                    case "Usuario":
                        var usuarioForm = new UsuarioForm(_serviceProvider.GetRequiredService<IUsuarioService>());
                        AbrirFormularioHijo(usuarioForm);
                        break;
                    case "Ticket":
                        var ticketForm = new TicketForm(
                                                    _serviceProvider.GetRequiredService<ITicketService>(), 
                                                    _serviceProvider.GetRequiredService<IEmpresaService>(), 
                                                    _serviceProvider.GetRequiredService<IBalanzaService>(),
                                                    _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                    _serviceProvider.GetRequiredService<IProductoService>(),
                                                    _serviceProvider.GetRequiredService<IClaseService>(),
                                                    _serviceProvider.GetRequiredService<ILocalidadService>(),
                                                    _serviceProvider.GetRequiredService<IVehiculoService>(),
                                                    _serviceProvider.GetRequiredService<IPersonaService>(),
                                                    _serviceProvider.GetRequiredService<IAdjuntoService>()
                                                    );

                        AbrirFormularioHijo(ticketForm);
                        break;
                    case "RumaRecibir":
                        var rumaRecibirForm = new RumaForm(
                                                    _serviceProvider.GetRequiredService<IRumaService>(),
                                                    _serviceProvider.GetRequiredService<ITicketService>(),
                                                    _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                    _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                    _serviceProvider.GetRequiredService<IProductoService>(),
                                                    _serviceProvider.GetRequiredService<IClaseService>(),
                                                    _serviceProvider.GetRequiredService<IContratoService>(),
                                                    _serviceProvider.GetRequiredService<ILocalidadService>(),
                                                    _serviceProvider.GetRequiredService<IAdjuntoService>(),
                                                    "REC"
                                                    );

                        AbrirFormularioHijo(rumaRecibirForm);
                        break;
                    case "RumaMineral":
                        var rumaMineralForm = new RumaForm(
                                                    _serviceProvider.GetRequiredService<IRumaService>(),
                                                    _serviceProvider.GetRequiredService<ITicketService>(),
                                                    _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                    _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                    _serviceProvider.GetRequiredService<IProductoService>(),
                                                    _serviceProvider.GetRequiredService<IClaseService>(),
                                                    _serviceProvider.GetRequiredService<IContratoService>(),
                                                    _serviceProvider.GetRequiredService<ILocalidadService>(),
                                                    _serviceProvider.GetRequiredService<IAdjuntoService>(),
                                                    "MIS"
                                                    );

                        AbrirFormularioHijo(rumaMineralForm);
                        break;
                    case "RumaConcentrado":
                        var rumaConcentradoForm = new RumaForm(
                                                    _serviceProvider.GetRequiredService<IRumaService>(),
                                                    _serviceProvider.GetRequiredService<ITicketService>(),
                                                    _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                    _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                    _serviceProvider.GetRequiredService<IProductoService>(),
                                                    _serviceProvider.GetRequiredService<IClaseService>(),
                                                    _serviceProvider.GetRequiredService<IContratoService>(),
                                                    _serviceProvider.GetRequiredService<ILocalidadService>(),
                                                    _serviceProvider.GetRequiredService<IAdjuntoService>(),
                                                    "CON"
                                                    );

                        AbrirFormularioHijo(rumaConcentradoForm);
                        break;
                    case "LeyMineral":
                        var leyMineralForm = new LeyMineralForm(
                                                    _serviceProvider.GetRequiredService<ILeyService>(),
                                                    _serviceProvider.GetRequiredService<IRumaService>(),
                                                    _serviceProvider.GetRequiredService<ITicketService>(),
                                                    _serviceProvider.GetRequiredService<IEmpresaService>(),
                                                    _serviceProvider.GetRequiredService<ITablaGeneralesService>(),
                                                    _serviceProvider.GetRequiredService<IClaseService>(),
                                                    _serviceProvider.GetRequiredService<ILocalidadService>(),
                                                    _serviceProvider.GetRequiredService<IAdjuntoService>()
                                                    );

                        AbrirFormularioHijo(leyMineralForm);
                        break;
                    default:
                        break;
                }
            }
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
