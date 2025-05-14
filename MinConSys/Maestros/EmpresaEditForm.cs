using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
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
    public partial class EmpresaEditForm : Form
    {
        private readonly IEmpresaService _empresaService;
        private readonly int _idEmpresa;
        public EmpresaEditForm(IEmpresaService empresaService)
        {
            InitializeComponent();
            _empresaService = empresaService;
            _idEmpresa = 0;
        }
        public EmpresaEditForm(IEmpresaService empresaService, int idEmpresa)
        {
            InitializeComponent();
            _empresaService = empresaService;
            _idEmpresa = idEmpresa;
        }

        private async void EmpresaEditForm_Load(object sender, EventArgs e)
        {
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
                    cboEstadoContribuyente.Text = empresa.EstadoContribuyente;
                    cboCondicionContribuyente.Text = empresa.CondicionContribuyente;
                    txtPartidaElectronica.Text = empresa.PartidaElectronica;
                    txtZonaPartidaElectronica.Text = empresa.ZonaPartidaElectronica;
                }
            }
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
                EstadoContribuyente = cboEstadoContribuyente.Text,
                CondicionContribuyente = cboCondicionContribuyente.Text,
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
    }
}
