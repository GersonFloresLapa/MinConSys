using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys.Modales
{
    public partial class AdjuntosViewerControl : UserControl
    {
        private string _tablaReferencia;
        private int _idReferencia;
        private IAdjuntoService _adjuntoService;

        public string TablaReferencia
        {
            get => _tablaReferencia;
            set
            {
                _tablaReferencia = value;
                CargarAdjuntos();
            }
        }

        public int IdReferencia
        {
            get => _idReferencia;
            set
            {
                _idReferencia = value;
                CargarAdjuntos();
            }
        }

        public AdjuntosViewerControl()
        {
            InitializeComponent();
            cboTipoDocumento.Items.AddRange(new string[] {
                    "DNI", "RUC", "Contrato", "Ficha Técnica", "Otro"
                });
            cboTipoDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public async void SetService(IAdjuntoService service)
        {
            _adjuntoService = service;
            await CargarAdjuntos(); // Opcional: recarga al asignar servicio
        }

        private async Task CargarAdjuntos()
        {
            if (string.IsNullOrEmpty(_tablaReferencia) || _idReferencia == 0 || _adjuntoService == null)
                return;

            try
            {
                List<Adjunto> lista = await _adjuntoService.ListarAdjuntosAsync(_tablaReferencia, _idReferencia);
                dgvAdjuntos.DataSource = lista;
                EstiloGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los adjuntos: " + ex.Message);
            }
        }
        private void EstiloGrid()
        {
            dgvAdjuntos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAdjuntos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAdjuntos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdjuntos.ReadOnly = true;
            dgvAdjuntos.AllowUserToAddRows = false;
            dgvAdjuntos.AllowUserToDeleteRows = false;
            dgvAdjuntos.MultiSelect = false;
            dgvAdjuntos.AllowUserToResizeRows = false;
            dgvAdjuntos.RowHeadersVisible = false;

            foreach (DataGridViewColumn col in dgvAdjuntos.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            if (dgvAdjuntos.Columns.Count > 0)
                dgvAdjuntos.Columns[dgvAdjuntos.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_tablaReferencia) || _idReferencia == 0 || _adjuntoService == null)
            {
                MessageBox.Show("No se ha configurado correctamente la tabla o el ID de referencia.");
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = "Todos los archivos|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string rutaOrigen = ofd.FileName;
                    string nombreArchivo = Path.GetFileName(rutaOrigen);
                    string tipoDocumento = cboTipoDocumento.SelectedItem?.ToString();

                    if (string.IsNullOrEmpty(tipoDocumento))
                    {
                        MessageBox.Show("Debe seleccionar un tipo de documento.");
                        return;
                    }

                    // Aquí puedes copiar el archivo a una carpeta específica si deseas
                    // Por ahora, usaremos la misma ruta
                    var adjunto = new Adjunto
                    {
                        TablaReferencia = _tablaReferencia,
                        IdReferencia = _idReferencia,
                        NombreArchivo = nombreArchivo,
                        UrlArchivo = rutaOrigen, // O la ruta destino si lo copias
                        TipoDocumento = tipoDocumento,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = Environment.UserName // o el usuario actual
                    };

                    // Guardar en base de datos
                    await _adjuntoService.CrearAdjuntoAsync(adjunto);

                    // Recargar adjuntos
                    await CargarAdjuntos();

                    MessageBox.Show("Archivo adjuntado correctamente.");
                }
            }
        }
    }
}
