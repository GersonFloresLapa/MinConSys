using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
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
    public partial class ContratoEditForm : Form
    {
        private readonly IContratoService _contratoService;
        private readonly IEmpresaService _empresaService;
        private List<ComboItem> _empresas;
        private List<ComboItem> _proveedores;
        private readonly int _idContrato;
        public ContratoEditForm(IContratoService contratoService, 
                                IEmpresaService empresaService,
                                int idContrato
                                )
        {
            InitializeComponent();
            _contratoService = contratoService;
            _empresaService = empresaService;
            _idContrato = idContrato;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevoContrato = new Contrato
            {
                IdContrato = _idContrato,
                CodigoContrato = txtCodigoContrato.Text,
                IdEmpresa = (int)cboEmpresa.SelectedValue,
                IdProveedor = (int)cboProveedor.SelectedValue,
                FechaInicio = dtpFechaInicio.Value.Date,
                FechaFin = dtpFechaFin.Checked ? dtpFechaFin.Value.Date : (DateTime?)null,
                TipoContrato = txtTipoContrato.Text,
                Clase = txtClase.Text,
                Producto = txtProducto.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            try
            {
                if (_idContrato != 0)
                    await _contratoService.ActualizarContratoAsync(nuevoContrato);
                else
                    await _contratoService.CrearContratoAsync(nuevoContrato);

                MessageBox.Show("Contrato guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void ContratoEditForm_Load(object sender, EventArgs e)
        {
            cboEmpresa.DropDownStyle = ComboBoxStyle.DropDown;
            cboProveedor.DropDownStyle = ComboBoxStyle.DropDown;

            var empresasTask = CargarEmpresasAsync();
            var proveedoresTask = CargarProveedoresAsync();

            await Task.WhenAll(empresasTask, proveedoresTask);

            if (_idContrato != 0)
            {
                var contrato = await _contratoService.ObtenerPorIdAsync(_idContrato);

                if (contrato != null)
                {
                    txtCodigoContrato.Text = contrato.CodigoContrato;

                    cboEmpresa.SelectedValue = contrato.IdEmpresa;
                    cboProveedor.SelectedValue = contrato.IdProveedor;

                    dtpFechaInicio.Value = contrato.FechaInicio;
                    if (contrato.FechaFin.HasValue)
                    {
                        dtpFechaFin.Value = contrato.FechaFin.Value;
                        dtpFechaFin.Checked = true;
                    }
                    else
                    {
                        dtpFechaFin.Checked = false;
                    }
                    txtTipoContrato.Text = contrato.TipoContrato;
                    txtClase.Text = contrato.Clase;
                    txtProducto.Text = contrato.Producto;
                }
            }
        }
        private async Task CargarEmpresasAsync()
        {

            _empresas = await _empresaService.ListarEmpresasTiposAsync("TRA");

            cboEmpresa.DataSource = _empresas;
            cboEmpresa.DisplayMember = "Descripcion";
            cboEmpresa.ValueMember = "Id";
            cboEmpresa.DropDownStyle = ComboBoxStyle.DropDown;
            cboEmpresa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboEmpresa.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private async Task CargarProveedoresAsync()
        {

            _proveedores = await _empresaService.ListarEmpresasTiposAsync("TRA");


            cboProveedor.DataSource = _proveedores;
            cboProveedor.DisplayMember = "Descripcion";
            cboProveedor.ValueMember = "Id";
            cboProveedor.DropDownStyle = ComboBoxStyle.DropDown;
            cboProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void btnBuscarEmpresa_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_empresas);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboEmpresa.SelectedItem = cboEmpresa.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            var selector = new SelectorGenericoForm(_proveedores);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var seleccionado = selector.ItemSeleccionado;
                cboProveedor.SelectedItem = cboProveedor.Items
                    .Cast<ComboItem>()
                    .FirstOrDefault(x => x.Id == seleccionado.Id);
            }

        }
    }
}
