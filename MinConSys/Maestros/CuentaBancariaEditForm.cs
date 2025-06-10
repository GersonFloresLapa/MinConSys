using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
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
    public partial class CuentaBancariaEditForm : Form
    {
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private readonly ICuentaBancariaService _cuentabancariaService;
        private readonly int _idCuentaBancaria;
        private readonly string _tipoEntidad;
        private readonly int _idEntidad;
        private readonly string _nombreEntidad;


        private List<TablaGeneralesCombo> _bancos;
        private List<TablaGeneralesCombo> _monedas;
        private List<TablaGeneralesCombo> _tipocuentas;

        private bool _cargandoComponentes = false;
        public event EventHandler GuardadoExitoso;
        public  CuentaBancariaEditForm(
                                    ITablaGeneralesService tablaGeneralesService,
                                    ICuentaBancariaService cuentabancariaService,
                                    string       tipoEntidad,
                                    int         idEntidad,
                                    string      nombreEntidad,
                                    int         idCuentaBancaria
                                    )
        {
            InitializeComponent();
            _tablaGeneralesService = tablaGeneralesService;
            _cuentabancariaService = cuentabancariaService;
            _tipoEntidad           = tipoEntidad;
            _idEntidad = idEntidad;
            _idCuentaBancaria = idCuentaBancaria;
            _nombreEntidad = nombreEntidad;

        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var cuentabancariaRequest = new CuentaBancariaRequest
            {
                IdCuenta            = _idCuentaBancaria,
                CodigoTipoEntidad   = _tipoEntidad,
                IdEntidad           = _idEntidad,
                CodigoBanco         = cboCodigoBanco.SelectedValue.ToString(),
                Moneda              = cboMoneda.SelectedValue.ToString(),
                TipoCuenta          = cboTipoCuenta.SelectedValue.ToString(),
                NroCuenta           = nroCuenta.Text,
                UsuarioCreacion     = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };


            try
            {
                if (_idCuentaBancaria != 0)
                    await _cuentabancariaService.ActualizarCuentaBancariaAsync(cuentabancariaRequest);
                else
                    await _cuentabancariaService.CrearCuentaBancariaAsync(cuentabancariaRequest);

                MessageBox.Show("Cuenta Bancaria guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void CuentaBancariaEditForm_Load(object sender, EventArgs e)
        {
            _cargandoComponentes = true;
            await CargarCombosAsync();


            if (_idCuentaBancaria != 0)
            {
                var cuentabancaria = await _cuentabancariaService.ObtenerPorIdAsync(_idCuentaBancaria);

                if (cuentabancaria != null)
                {
                    cboCodigoBanco.Text = cuentabancaria.CodigoBanco.ToString();
                    cboMoneda.Text      = cuentabancaria.Moneda.ToString();
                    cboTipoCuenta.Text  = cuentabancaria.TipoCuenta.ToString();
                    nroCuenta.Text      = cuentabancaria.NroCuenta;
                    EmpresaTxt.Text     = _nombreEntidad; 

                }


            }
            else
            {
                cboCodigoBanco.SelectedIndex = -1;
                cboMoneda.SelectedIndex = -1;
                cboTipoCuenta.SelectedIndex = -1;
                EmpresaTxt.Text = _nombreEntidad;

            }

            _cargandoComponentes = false;

        }

        private void ConfigurarComboBox(ComboBox combo, object dataSource, string display, string value, bool autoComplete = true)
        {
            combo.DataSource = dataSource;
            combo.DisplayMember = display;
            combo.ValueMember = value;
            combo.DropDownStyle = ComboBoxStyle.DropDown;
            if (autoComplete)
            {
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combo.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            combo.SelectedIndex = -1;
        }

        private async Task CargarCombosAsync()
        {

            var bancosTask      = _tablaGeneralesService.ObtenerPorTipoGeneralAsync("CODIGOBANCO");
            var monedaTask      = _tablaGeneralesService.ObtenerPorTipoGeneralAsync("MONEDA");
            var tipocuentaTask  = _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOCUENTA");

            await Task.WhenAll(bancosTask, monedaTask, tipocuentaTask);

            _bancos      = bancosTask.Result;
            _monedas     = monedaTask.Result;
            _tipocuentas = tipocuentaTask.Result;

            ConfigurarComboBox(cboCodigoBanco, _bancos, "Valor", "Codigo", false);
            ConfigurarComboBox(cboTipoCuenta, _tipocuentas, "Valor", "Codigo", false);
            ConfigurarComboBox(cboMoneda, _monedas, "Valor", "Codigo", false);

        }


    }
}
