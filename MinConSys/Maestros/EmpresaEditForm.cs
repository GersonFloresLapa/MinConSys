using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Helpers;
using MinConSys.Modales;
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
    public partial class EmpresaEditForm : Form
    {
        private readonly IEmpresaService _empresaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private readonly IAdjuntoService _adjuntoService;
        private readonly int _idEmpresa;
        private List<TablaGenerales> _estadoContribuyentes;
        private List<TablaGenerales> _condicionContribuyentes;
        private AdjuntosViewerControl _adjuntosViewer;
        public EmpresaEditForm( IEmpresaService empresaService, 
                                ITablaGeneralesService tablaGenerales,
                                IAdjuntoService adjuntoService,
                                int idEmpresa)
        {
            InitializeComponent();
            _empresaService = empresaService;
            _idEmpresa = idEmpresa;
            _tablaGeneralesService = tablaGenerales;
            _adjuntoService = adjuntoService;
        }

        private async void EmpresaEditForm_Load(object sender, EventArgs e)
        {
            var estadoContribuyenteTask = CargarEstadoContribuyenteAsync();
            var condicionContribuyenteTask = CargarCondicionContribuyenteAsync();

            await Task.WhenAll(estadoContribuyenteTask, condicionContribuyenteTask);

            if (_idEmpresa != 0)
            {
                var empresa = await _empresaService.ObtenerPorIdAsync(_idEmpresa);

                if (empresa != null)
                {
                    txtRUC.Text = empresa.RUC;
                    txtRazonSocial.Text = empresa.RazonSocial;
                    txtDireccionFiscal.Text = empresa.DireccionFiscal;
                    txtDireccionComercial.Text = empresa.DireccionComercial;
                    txtTelefono.Text = empresa.Telefono;
                    txtEmail.Text = empresa.Email;
                    cboEstadoContribuyente.SelectedValue = empresa.EstadoContribuyente;
                    cboCondicionContribuyente.SelectedValue = empresa.CondicionContribuyente;
                    txtPartidaElectronica.Text = empresa.PartidaElectronica;
                    txtZonaPartidaElectronica.Text = empresa.ZonaPartidaElectronica;
                }
            }

            // Instanciar el control
            _adjuntosViewer = new AdjuntosViewerControl();

            // Configurar servicio e identificación de los adjuntos
            _adjuntosViewer.SetService(_adjuntoService); // asegúrate de tener una instancia de IAdjuntoService
            _adjuntosViewer.TablaReferencia = "Empresa";
            _adjuntosViewer.IdReferencia = _idEmpresa; // por ejemplo

            // Ajustar al panel
            _adjuntosViewer.Dock = DockStyle.Fill;
            // Agregar al panel3
            panel3.Controls.Clear(); // opcional: limpiar contenido anterior
            panel3.Controls.Add(_adjuntosViewer);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevaEmpresa = new Empresa
            {
                IdEmpresa = _idEmpresa,
                RUC = txtRUC.Text,
                RazonSocial = txtRazonSocial.Text,
                DireccionFiscal = txtDireccionFiscal.Text,
                DireccionComercial = txtDireccionComercial.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                EstadoContribuyente = cboEstadoContribuyente.SelectedValue.ToString(),
                CondicionContribuyente = cboCondicionContribuyente.SelectedValue.ToString(),
                PartidaElectronica = txtPartidaElectronica.Text,
                ZonaPartidaElectronica = txtZonaPartidaElectronica.Text,
                Estado = cboEstadoContribuyente.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idEmpresa != 0)
                    await _empresaService.ActualizarEmpresaAsync(nuevaEmpresa);
                else
                    await _empresaService.CrearEmpresaAsync(nuevaEmpresa);

                MessageBox.Show("Empresa guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async Task CargarEstadoContribuyenteAsync()
        {

            _estadoContribuyentes = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("ESTADOCONTRIBUYENTE");

            cboEstadoContribuyente.DataSource = _estadoContribuyentes;
            cboEstadoContribuyente.DisplayMember = "Valor";
            cboEstadoContribuyente.ValueMember = "Codigo";
            cboEstadoContribuyente.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private async Task CargarCondicionContribuyenteAsync()
        {

            _condicionContribuyentes = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("CONDICIONCONTRIBUYENTE");

            cboCondicionContribuyente.DataSource = _condicionContribuyentes;
            cboCondicionContribuyente.DisplayMember = "Valor";
            cboCondicionContribuyente.ValueMember = "Codigo";
            cboCondicionContribuyente.DropDownStyle = ComboBoxStyle.DropDownList;

        }
    }
}
