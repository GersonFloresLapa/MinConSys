using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Response;
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
    public partial class VehiculoEditForm : Form
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IEmpresaService _empresaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;

        private List<ComboItem> _empresas;
        private List<TablaGeneralesCombo> _tipoVehiculo;
        private readonly int _idVehiculo;

        public VehiculoEditForm(IVehiculoService vehiculoService, 
                                IEmpresaService empresaService,
                                ITablaGeneralesService tablaGeneralesService,
                                int idVehiculo
                                )
        {
            InitializeComponent();
            _vehiculoService = vehiculoService;
            _empresaService = empresaService;
            _idVehiculo = idVehiculo;
            _tablaGeneralesService = tablaGeneralesService;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoVehiculo = new Vehiculo
            {
                IdVehiculo = _idVehiculo,
                Placa = txtPlaca.Text.Trim(),
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Anio = (int?)nudAnio.Value,
                TipoVehiculoCodigo = cboTipoVehiculo.SelectedValue.ToString(),
                CapacidadToneladas = decimal.TryParse(txtCapacidad.Text, out var capacidad) ? capacidad : (decimal?)null,
                IdTransportista = (int)cboTransportista.SelectedValue,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };


            try
            {
                if (_idVehiculo != 0)
                    await _vehiculoService.ActualizarVehiculoAsync(nuevoVehiculo);
                else
                    await _vehiculoService.CrearVehiculoAsync(nuevoVehiculo);

                MessageBox.Show("Vehículo guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void VehiculoEditForm_Load(object sender, EventArgs e)
        {
            cboTransportista.DropDownStyle = ComboBoxStyle.DropDown;
            nudAnio.Minimum = 0;
            nudAnio.Maximum = DateTime.Now.Year;
            nudAnio.Value = DateTime.Now.Year;

            await CargarEmpresasAsync();
            await CargarEstadoContribuyenteAsync();

            if (_idVehiculo != 0)
            {
                var vehiculo = await _vehiculoService.ObtenerPorIdAsync(_idVehiculo);
                if (vehiculo != null)
                {
                    txtPlaca.Text = vehiculo.Placa;
                    txtMarca.Text = vehiculo.Marca;
                    txtModelo.Text = vehiculo.Modelo;
                    nudAnio.Value = vehiculo.Anio ?? nudAnio.Minimum;
                    cboTipoVehiculo.SelectedValue = vehiculo.TipoVehiculoCodigo;
                    txtCapacidad.Text = vehiculo.CapacidadToneladas?.ToString("0.##") ?? "";
                    cboTransportista.SelectedValue = vehiculo.IdTransportista;
                }
            }
        }

        private async Task CargarEmpresasAsync()
        {

            _empresas = await _empresaService.ListarEmpresasTiposAsync("PROTRA");
 
            cboTransportista.DataSource = _empresas;
            cboTransportista.DisplayMember = "Descripcion";
            cboTransportista.ValueMember = "Id";
            cboTransportista.DropDownStyle = ComboBoxStyle.DropDown;
            cboTransportista.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTransportista.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
   
            var selector = new SelectorGenericoForm(_empresas);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboTransportista.SelectedItem = cboTransportista.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }
        private async Task CargarEstadoContribuyenteAsync()
        {

            _tipoVehiculo = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOVEHICULO");

            cboTipoVehiculo.DataSource = _tipoVehiculo;
            cboTipoVehiculo.DisplayMember = "Valor";
            cboTipoVehiculo.ValueMember = "Codigo";
            cboTipoVehiculo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
