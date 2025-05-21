using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Request;
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
        private List<TablaGeneralesCombo> _estadoContribuyentes;
        private List<TablaGeneralesCombo> _condicionContribuyentes;
        private List<TablaGeneralesCombo> _tipoEmpresaContribuyentes;
        private List<Ubigeo> _ubigeos;
        private AdjuntosViewerControl _adjuntosViewer;
        public EmpresaEditForm( IEmpresaService empresaService, 
                                ITablaGeneralesService tablaGenerales,
                                IAdjuntoService adjuntoService,
                                int idEmpresa)
        {
            InitializeComponent();
            _adjuntosViewer = new AdjuntosViewerControl
            {
                Dock = DockStyle.Fill
            };
            panel3.Controls.Add(_adjuntosViewer);
            _empresaService = empresaService;
            _idEmpresa = idEmpresa;
            _tablaGeneralesService = tablaGenerales;
            _adjuntoService = adjuntoService;
        }

        private async void EmpresaEditForm_Load(object sender, EventArgs e)
        {
            var estadoContribuyenteTask = CargarEstadoContribuyenteAsync();
            var condicionContribuyenteTask = CargarCondicionContribuyenteAsync();
            var tipoEmpresaTask = CargarTipoEmpresaAsync();
            var ubigeosEmpresaTask = CargarUbigeosAsync();

            await Task.WhenAll(estadoContribuyenteTask, condicionContribuyenteTask, tipoEmpresaTask, ubigeosEmpresaTask);

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
                    SetUbigeoEmpresa(empresa.Ubigeo);
                }
            }
            await _adjuntosViewer.InicializarAsync(_adjuntoService, "Empresa", _idEmpresa);
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
                Ubigeo = cboDistrito.SelectedValue.ToString(),
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
                {
                    var empresaId = await _empresaService.CrearEmpresaAsync(nuevaEmpresa);
                    await _adjuntosViewer.GuardarAdjuntosTemporalesAsync(empresaId);
                }
                    

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
        private async Task CargarTipoEmpresaAsync()
        {

            _tipoEmpresaContribuyentes = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOEMPRESA");
            
            foreach (var v in _tipoEmpresaContribuyentes)
            {
                clbTipoEmpresa.Items.Add(v);
            }

        }
        private async Task CargarUbigeosAsync()
        {
            _ubigeos = await _tablaGeneralesService.ObtenerUbigeosAsync();
            var departamentos = _ubigeos
                .Select(u => u.Departamento)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            cboDepartamento.DataSource = departamentos;
        }

        private void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string departamentoSeleccionado = cboDepartamento.SelectedItem.ToString();

            cboProvincia.DataSource = null; // Limpia distrito

            var provincias = _ubigeos
                .Where(u => u.Departamento == departamentoSeleccionado)
                .Select(u => u.Provincia)
                .Distinct()
                .OrderBy(p => p)
                .ToList();

            cboProvincia.DataSource = provincias;
           
        }

        private void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartamento.SelectedItem == null || cboProvincia.SelectedItem == null) return;

            string departamento = cboDepartamento.SelectedItem.ToString();
            string provincia = cboProvincia.SelectedItem.ToString();

            cboDistrito.DataSource = null; // Limpia distrito

            var distritos = _ubigeos
                .Where(u => u.Departamento == departamento && u.Provincia == provincia)
                .Distinct()
                .OrderBy(u => u.Distrito)
                .ToList();

            cboDistrito.DataSource = distritos;

            cboDistrito.DisplayMember = "Distrito"; // Lo que se muestra
            cboDistrito.ValueMember = "Codigo";     // Lo que se guarda/interna
        }

        private void SetUbigeoEmpresa(string codigoUbigeo)
        {
            var ubigeo = _ubigeos.FirstOrDefault(u => u.Codigo == codigoUbigeo);

            if (ubigeo == null)
            {
                MessageBox.Show("Ubigeo no encontrado.");
                return;
            }

            // 1. Setear Departamento
            cboDepartamento.SelectedItem = ubigeo.Departamento;

            cboProvincia.SelectedItem = ubigeo.Provincia;

            cboDistrito.SelectedValue = codigoUbigeo; // <- aquí seleccionas el ubigeo exacto
        }

    }
}
