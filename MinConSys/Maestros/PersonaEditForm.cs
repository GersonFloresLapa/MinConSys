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
    public partial class PersonaEditForm : Form
    {
        private readonly IPersonaService _personaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private readonly IAdjuntoService _adjuntoService;
        private readonly ICuentaBancariaService _cuentabancariaService;

        private readonly int _idPersona;
        private string _nombrePersona;
        private readonly string _tipoEntidad = "PER";

        private List<TablaGeneralesCombo> _tipoDocumento;
        private List<TablaGeneralesCombo> _tipoPersona;
        private AdjuntosViewerControl _adjuntosViewer;
        private List<CuentaBancariaDto> _cuentasbancarias;
        private List<TablaGeneralesCombo> _adjuntosPersona;
        



        public PersonaEditForm( 
                                IPersonaService personaService,
                                ITablaGeneralesService tablaGeneralesService,
                                ICuentaBancariaService cuentabancariaService,
                                IAdjuntoService adjuntoService,
                                int idPersona)
        {
            InitializeComponent();
            _personaService = personaService;
            _tablaGeneralesService = tablaGeneralesService;
            _idPersona = idPersona;
            _cuentabancariaService = cuentabancariaService;
            _adjuntoService = adjuntoService;

            _adjuntosViewer = new AdjuntosViewerControl
            {
                Dock = DockStyle.Fill
            };

            panel3.Controls.Add(_adjuntosViewer);


        }

       

        private async void PersonaEditForm_Load(object sender, EventArgs e)
        {
            var cargosTask = CargarTipoDocumentoAsync();
            var tipoPersonaTask = CargarTipoPersonaAsync();
            var cuentabancaria = CargarCuentasBancariasByIdEmpresaAsync();
            var adjuntosPersona = CargarTipoAdjuntoEmpresaAsync();

            await Task.WhenAll(tipoPersonaTask, cargosTask, adjuntosPersona );

            clbTipoPersona.CheckOnClick = true;

            if (_idPersona != 0)
            {
                var persona = await _personaService.ObtenerPorIdAsync(_idPersona);

                if (persona != null)
                {
                    txtNumeroDocumento.Text = persona.NumeroDocumento;
                    cboTipoDocumento.Text = persona.TipoDocumento;
                    txtNombres.Text = persona.Nombres;
                    txtApellidoPaterno.Text = persona.ApellidoPaterno;
                    txtApellidoMaterno.Text = persona.ApellidoMaterno;
                    txtCorreoElectronico.Text = persona.CorreoElectronico;
                    txtTelefono.Text = persona.Telefono;
                    txtDireccion.Text = persona.Direccion;
                    txtBrevete.Text = persona.Brevete;

                    var tipoPersonas = await _personaService.ObtenerTipoPersonaPorPersonaAsync(_idPersona);
                    _nombrePersona = persona.NumeroDocumento + " - " + persona.Nombres+" " + persona.ApellidoPaterno+" "+ persona.ApellidoMaterno;

                    MarcarTipoPersonasEnEdicion(tipoPersonas);
                }
            }

            await _adjuntosViewer.InicializarAsync(_adjuntoService, "Persona", _idPersona, _adjuntosPersona);

        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!FormValidator.Validar(this, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuardar.Enabled = false;

            var nuevaPersona = new Persona
            {
                IdPersona = _idPersona,
                TipoDocumento = cboTipoDocumento.SelectedValue.ToString(),
                NumeroDocumento = txtNumeroDocumento.Text,
                Nombres = txtNombres.Text,
                ApellidoPaterno = txtApellidoPaterno.Text,
                ApellidoMaterno = txtApellidoMaterno.Text,
                CorreoElectronico = txtCorreoElectronico.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                Brevete = txtBrevete.Text,
                UsuarioCreacion = Session.UsuarioActual.NombreUsuario,
                UsuarioModificacion = Session.UsuarioActual.NombreUsuario
            };

            var personaRequest = new PersonaRequest
            {
                Persona = nuevaPersona,
                TipoPersonas = ObtenerTipoPersonasMarcados(_idPersona, Session.UsuarioActual.NombreUsuario)
            };

            try
            {
                if (_idPersona != 0)
                    await _personaService.ActualizarPersonaAsync(personaRequest);
                else
                {
                    var personaId = await _personaService.CrearPersonaAsync(personaRequest);
                    await _adjuntosViewer.GuardarAdjuntosTemporalesAsync(_idPersona);

                }


                MessageBox.Show("Persona guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void btnNuevoCuenta_Click(object sender, EventArgs e)
        {
            if (_idPersona != 0)
            {
                using (var form = new CuentaBancariaEditForm(_tablaGeneralesService, _cuentabancariaService, _tipoEntidad, _idPersona, _nombrePersona, 0))
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
            using (var form = new CuentaBancariaEditForm(_tablaGeneralesService, _cuentabancariaService, _tipoEntidad, _idPersona, _nombrePersona, idCuenta))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarCuentasBancariasByIdEmpresaAsync(); // Vuelves a cargar la lista
                }
            }
        }

        private async Task CargarCuentasBancariasByIdEmpresaAsync()
        {
            try
            {
                _cuentasbancarias = (await _cuentabancariaService.ListarCuentaBancariasByIdEmpresaAsync(_idPersona)).ToList();
                dgvCuentas.DataSource = null;
                dgvCuentas.DataSource = _cuentasbancarias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Cuentas Bancarias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task CargarTipoPersonaAsync()
        {

            _tipoPersona = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPOPERSONA");

            foreach (var v in _tipoPersona)
            {
                clbTipoPersona.Items.Add(v);
            }

        }
        private async Task CargarTipoDocumentoAsync()
        {

            _tipoDocumento = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("TIPODOCUMENTO");

            cboTipoDocumento.DataSource = _tipoDocumento;
            cboTipoDocumento.DisplayMember = "Valor";
            cboTipoDocumento.ValueMember = "Codigo";

        }

        private List<TipoPersona> ObtenerTipoPersonasMarcados(int idPersona, string usuario)
        {
            var tipoPersonas = new List<TipoPersona>();

            foreach (var item in clbTipoPersona.CheckedItems)
            {
                var general = item as TablaGeneralesCombo;

                if (general != null)
                {
                    tipoPersonas.Add(new TipoPersona
                    {
                        CodigoTipoPersona = general.Codigo,
                        IdPersona = idPersona, // esto puedes asignarlo después si aún no lo tienes
                        Estado = 'A',
                        UsuarioCreacion = usuario
                    });
                }
            }

            return tipoPersonas;
        }


        private void MarcarTipoPersonasEnEdicion(List<TipoPersona> tipoPersonasMarcados)
        {
            // Recorrer los ítems del CheckedListBox
            for (int i = 0; i < clbTipoPersona.Items.Count; i++)
            {
                var item = clbTipoPersona.Items[i] as TablaGeneralesCombo;

                // Verificamos si el código del item está en la lista de los que deben marcarse
                bool estaMarcado = tipoPersonasMarcados.Any(te => te.CodigoTipoPersona == item?.Codigo);

                clbTipoPersona.SetItemChecked(i, estaMarcado);
            }
        }

        private async Task CargarTipoAdjuntoEmpresaAsync()
        {

            _adjuntosPersona = await _tablaGeneralesService.ObtenerPorTipoGeneralAsync("ADJUNTOPERSONA");
        }


    }
}
