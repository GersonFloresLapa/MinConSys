using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Request;
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
    public partial class LocalidadEditForm : Form
    {
        private readonly ILocalidadService _localidadService;
        private readonly IEmpresaService _empresaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private List<ComboItem> _empresas;
        private bool _cargandoComponentes = false;
        private List<TablaGeneralesCombo> _tipoLocalidades;


        private readonly int _idLocalidad;
        public LocalidadEditForm(   ILocalidadService localidadService, 
                                    IEmpresaService empresaService, 
                                    ITablaGeneralesService tablaGeneralesService,
                                    int idLocalidad)
        {
            InitializeComponent();

            _localidadService = localidadService;
            _empresaService = empresaService;
            _tablaGeneralesService = tablaGeneralesService;
            _idLocalidad = idLocalidad;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoRegistro = new Localidad
            {
                IdLocalidad     = _idLocalidad,
                IdEmpresa       = (int)cboEmpresa.SelectedValue,//int.Parse(cboEmpresa.SelectedItem.ToString()),
                TipoLocalidad   = cboTipoLocalidad.SelectedValue.ToString(),
                NombreLocalidad = txtNombreLocalidad.Text,
                Direccion       = txtDireccion.Text,
                Ubigeo          = txtUbigeo.Text,
                Estado          = "A",
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idLocalidad != 0)
                {
                    await _localidadService.ActualizarLocalidadAsync(nuevoRegistro);
                }
                else
                {
                    await _localidadService.CrearLocalidadAsync(nuevoRegistro);
                }

                MessageBox.Show("Registro Localidad correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void LocalidadEditForm_Load(object sender, EventArgs e)
        {
            _cargandoComponentes = true;
            var empresasTask = CargarEmpresasAsync();
            var tipolocalidadTask = CargarTipoLocalidadAsync();

            await Task.WhenAll(empresasTask, tipolocalidadTask);

            if (_idLocalidad != 0)
            {
                var registro = await _localidadService.ObtenerPorIdAsync(_idLocalidad);

                if (registro != null)
                {

                    cboEmpresa.SelectedValue = registro.IdEmpresa;
                    cboTipoLocalidad.SelectedValue = registro.TipoLocalidad;
                    txtNombreLocalidad.Text = registro.NombreLocalidad;
                    txtDireccion.Text = registro.Direccion;
                    txtUbigeo.Text = registro.Ubigeo;
                    
                }
            }
            else
            {
                cboEmpresa.SelectedIndex = -1;
                cboTipoLocalidad.SelectedIndex = -1;

            }

            _cargandoComponentes = false;
        }

        private async Task CargarEmpresasAsync()
        {

            _empresas = await _empresaService.ListarEmpresasTiposAsync("PROMIN");

            cboEmpresa.DataSource = _empresas;
            cboEmpresa.DisplayMember = "Descripcion";
            cboEmpresa.ValueMember = "Id";
            cboEmpresa.DropDownStyle = ComboBoxStyle.DropDown;
            cboEmpresa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboEmpresa.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private async Task CargarTipoLocalidadAsync()
        {

            _tipoLocalidades = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOLOCALIDAD");

            cboTipoLocalidad.DataSource = _tipoLocalidades;
            cboTipoLocalidad.DisplayMember = "Valor";
            cboTipoLocalidad.ValueMember = "Codigo";
            cboTipoLocalidad.DropDownStyle = ComboBoxStyle.DropDownList;

        }


    }
}
