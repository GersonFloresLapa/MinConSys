using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
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
        private readonly IRepresentanteService _representanteService;
        private readonly IPersonaService _personaService;
        private readonly ICuentaBancariaService _cuentabancariaService;

        private readonly int _idEmpresa;
        private string _nombreEmpresa;
        private readonly string _tipoEntidad="EMP";


        private List<TablaGeneralesCombo> _estadoContribuyentes;
        private List<TablaGeneralesCombo> _condicionContribuyentes;
        private List<TablaGeneralesCombo> _tipoEmpresaContribuyentes;
        private List<TablaGeneralesCombo> _adjuntosEmpresa;
        private List<Ubigeo> _ubigeos;
        private List<RepresentanteDto> _representantes;
        private List<CuentaBancariaDto> _cuentasbancarias;
        private List<TablaGeneralesCombo> _cargos;
        private List<ComboItem> _personas;
        private List<Representante> _representantesNuevos = new List<Representante>();


        private AdjuntosViewerControl _adjuntosViewer;

        private bool _cargandoComponentes = false;
        public EmpresaEditForm(IEmpresaService empresaService,
                                ITablaGeneralesService tablaGeneralesService,
                                IAdjuntoService adjuntoService,
                                IRepresentanteService representanteService,
                                IPersonaService personaService,
                                ICuentaBancariaService cuentabancariaService,
                                int idEmpresa)
        {
            InitializeComponent();

            _empresaService = empresaService;
            _idEmpresa = idEmpresa;
            _tablaGeneralesService = tablaGeneralesService;
            _adjuntoService = adjuntoService;
            _representanteService = representanteService;
            _personaService = personaService;
            _cuentabancariaService = cuentabancariaService;

            _adjuntosViewer = new AdjuntosViewerControl
            {
                Dock = DockStyle.Fill
            };

            panel3.Controls.Add(_adjuntosViewer);
            
        }
        private async void EmpresaEditForm_Load(object sender, EventArgs e)
        {

            _cargandoComponentes = true;
            var estadoContribuyenteTask = CargarEstadoContribuyenteAsync();
            var condicionContribuyenteTask = CargarCondicionContribuyenteAsync();
            var tipoEmpresaTask = CargarTipoEmpresaAsync();
            var ubigeosEmpresaTask = CargarUbigeosAsync();
            var representantesTask = CargarRepresentantesAsync();
            var cargosTask = CargarCargoAsync();
            var adjuntosEmpresa = CargarTipoAdjuntoEmpresaAsync();
            var cuentabancaria = CargarCuentasBancariasByIdEmpresaAsync();


            await Task.WhenAll(estadoContribuyenteTask, 
                               condicionContribuyenteTask, 
                               tipoEmpresaTask, 
                               ubigeosEmpresaTask, 
                               representantesTask, 
                               cargosTask, 
                               adjuntosEmpresa, 
                               cuentabancaria);

            clbTipoEmpresa.CheckOnClick = true;

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
                    dgvRepresentates.ConfigurarGenerico(_representantes, false);

                    _nombreEmpresa = empresa.RUC +" - " + empresa.RazonSocial;
                    var tipoEmpresas = await _empresaService.ObtenerTipoEmpresaPorEmpresaAsync(_idEmpresa);

                    MarcarTipoEmpresasEnEdicion(tipoEmpresas);
                }
            }
            await _adjuntosViewer.InicializarAsync(_adjuntoService, "Empresa", _idEmpresa,_adjuntosEmpresa);

            _cargandoComponentes = false;
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
                Ubigeo = cboDistrito.SelectedValue?.ToString(),
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                EstadoContribuyente = cboEstadoContribuyente.SelectedValue.ToString(),
                CondicionContribuyente = cboCondicionContribuyente.SelectedValue.ToString(),
                PartidaElectronica = txtPartidaElectronica.Text,
                ZonaPartidaElectronica = txtZonaPartidaElectronica.Text,
                Estado = cboEstadoContribuyente.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                
            };

            var empresaRequest = new EmpresaRequest
            {
                Empresa = nuevaEmpresa,
                TipoEmpresas = ObtenerTipoEmpresasMarcados(_idEmpresa, Session.UsuarioActual.NombreUsuario)
            };

            try
            {
                if (_idEmpresa != 0)
                    await _empresaService.ActualizarEmpresaAsync(empresaRequest);
                else
                {
                    var empresaId = await _empresaService.CrearEmpresaAsync(empresaRequest);
                    await GuardarRepresentantesTemporalesAsync(empresaId);
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
        private async Task CargarCargoAsync()
        {

            _cargos = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOREPRESENTANTE");

            cboCargo.ComboBox.DataSource = _cargos;
            cboCargo.ComboBox.DisplayMember = "Valor";
            cboCargo.ComboBox.ValueMember = "Codigo";
            cboCargo.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

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

            var provincias = _ubigeos
                .Select(u => u.Provincia)
                .Distinct()
                .OrderBy(d => d)
                .ToList();


            var distritos = _ubigeos
                .Select(u => u.Distrito)
                .Distinct()
                .OrderBy(d => d)
                .ToList();


            cboDepartamento.DataSource = departamentos;
            cboDepartamento.SelectedIndex = -1;

            cboProvincia.DataSource = provincias;
            cboProvincia.SelectedIndex = -1;

            cboDistrito.DataSource = distritos;
            cboDistrito.SelectedIndex = -1;


        }
        private void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoComponentes)
            {
                return;
            }
            
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
            if (_cargandoComponentes)
            {
                return;
            }

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

            cboDistrito.SelectedItem = ubigeo.Distrito; //codigoUbigeo; // <- aquí seleccionas el ubigeo exacto
        }



        private async Task CargarCuentasBancariasByIdEmpresaAsync()
        {
            try
            {
                _cuentasbancarias = (await _cuentabancariaService.ListarCuentaBancariasByIdEmpresaAsync(_idEmpresa)).ToList();
                dgvCuentas.DataSource = null;
                dgvCuentas.DataSource = _cuentasbancarias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Cuentas Bancarias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task CargarRepresentantesAsync()
        {
            try
            {
                _representantes = (await _representanteService.ListarRepresentantesByIdEmpresaAsync(_idEmpresa)).ToList();
                dgvRepresentates.DataSource = null;
                dgvRepresentates.DataSource = _representantes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            var cargo = cboCargo.ComboBox.SelectedValue.ToString();

            if (string.IsNullOrEmpty(cargo))
            {
                MessageBox.Show("Debe seleccionar un cargo.");
                return;
            }

            _personas = await _personaService.ListarPersonasTiposAsync("REPRESENTANTE");

            var selector = new SelectorGenericoForm(_personas);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;

                var representante = new Representante() {
                    IdEmpresa = _idEmpresa,
                    IdPersona = seleccionado.Id,
                    Cargo = cargo
                };

                if (_idEmpresa == 0)
                {
                    var representanteDto = new RepresentanteDto()
                    {
                        IdRepresentante = 0,
                        Cargo = cboCargo.ComboBox.Text,
                        Nombres = seleccionado.Descripcion
                    };

                    _representantes.Add(representanteDto);

                    dgvRepresentates.DataSource = null;
                    dgvRepresentates.DataSource = _representantes;
                }
                else
                {
                   await _representanteService.CrearRepresentanteAsync(representante);
                   await CargarRepresentantesAsync();
                }
                
            }
        }

        private async void btnNuevoCuenta_Click(object sender, EventArgs e)
        {
            if (_idEmpresa != 0)
            {
                using (var form = new CuentaBancariaEditForm(_tablaGeneralesService, _cuentabancariaService, _tipoEntidad, _idEmpresa, _nombreEmpresa, 0))
                {
                    var result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        await CargarCuentasBancariasByIdEmpresaAsync(); // Vuelves a cargar la lista
                    }
                }
            }
            else 
            {
                MessageBox.Show("Debe grabar la Empresa para agregar Cuentas Bancarias.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private async void btnEditarCuenta_Click(object sender, EventArgs e)
        {
            int idCuenta = Convert.ToInt32(dgvCuentas.CurrentRow.Cells["IdCuenta"].Value);
            using (var form = new CuentaBancariaEditForm(_tablaGeneralesService, _cuentabancariaService, _tipoEntidad, _idEmpresa, _nombreEmpresa, idCuenta))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarCuentasBancariasByIdEmpresaAsync(); // Vuelves a cargar la lista
                }
            }
        }



        private async Task GuardarRepresentantesTemporalesAsync(int nuevoIdReferencia)
        {
            if (_representantesNuevos.Count == 0 || _representantesNuevos == null)
                return;

            foreach (var representante in _representantesNuevos)
            {
                representante.IdEmpresa = nuevoIdReferencia;
                await _representanteService.CrearRepresentanteAsync(representante);
            }

            _representantesNuevos.Clear();
            
        }
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRepresentates.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un representante para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idRepresentante = Convert.ToInt32(dgvRepresentates.CurrentRow.Cells["IdRepresentante"].Value);
            string nombreRepresentante = dgvRepresentates.CurrentRow.Cells["Nombres"].Value.ToString(); // si tienes esta columna

            var resultado = MessageBox.Show(
                $"¿Está seguro que desea eliminar al representante \"{nombreRepresentante}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    await _representanteService.EliminarRepresentanteAsync(idRepresentante, Session.UsuarioActual.NombreUsuario);
                    dgvRepresentates.Rows.Remove(dgvRepresentates.CurrentRow);
                    MessageBox.Show("Representante eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<TipoEmpresa> ObtenerTipoEmpresasMarcados(int idEmpresa, string usuario)
        {
            var tipoEmpresas = new List<TipoEmpresa>();

            foreach (var item in clbTipoEmpresa.CheckedItems)
            {
                var general = item as TablaGeneralesCombo;

                if (general != null)
                {
                    tipoEmpresas.Add(new TipoEmpresa
                    {
                        CodigoTipoEmpresa = general.Codigo,
                        IdEmpresa = idEmpresa, // esto puedes asignarlo después si aún no lo tienes
                        Estado = 'A',
                        UsuarioCreacion = usuario
                    });
                }
            }

            return tipoEmpresas;
        }
        private void MarcarTipoEmpresasEnEdicion(List<TipoEmpresa> tipoEmpresasMarcados)
        {
            // Recorrer los ítems del CheckedListBox
            for (int i = 0; i < clbTipoEmpresa.Items.Count; i++)
            {
                var item = clbTipoEmpresa.Items[i] as TablaGeneralesCombo;

                // Verificamos si el código del item está en la lista de los que deben marcarse
                bool estaMarcado = tipoEmpresasMarcados.Any(te => te.CodigoTipoEmpresa == item?.Codigo);

                clbTipoEmpresa.SetItemChecked(i, estaMarcado);
            }
        }

        private async Task CargarTipoAdjuntoEmpresaAsync()
        {

            _adjuntosEmpresa = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("ADJUNTOEMPRESA");
        }
    }
}
