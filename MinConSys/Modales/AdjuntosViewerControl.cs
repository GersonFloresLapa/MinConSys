using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys.Modales
{
    public partial class AdjuntosViewerControl : UserControl
    {
        private string _tablaReferencia;
        private int _idReferencia;
        private IAdjuntoService _adjuntoService;
        private List<Adjunto> _adjuntosTemporales = new List<Adjunto>();
        public AdjuntosViewerControl()
        {
            InitializeComponent();
            cboTipoDocumento.Items.AddRange(new string[] {
                "DNI", "RUC", "Contrato", "FichaRuc", "Otro"
            });
            cboTipoDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Inicializa el control (en lugar de usar propiedades con lógica asíncrona)
        public async Task InicializarAsync(IAdjuntoService adjuntoService, string tablaReferencia, int idReferencia)
        {
            _adjuntoService = adjuntoService ?? throw new ArgumentNullException(nameof(adjuntoService));
            _tablaReferencia = tablaReferencia ?? throw new ArgumentNullException(nameof(tablaReferencia));
            _idReferencia = idReferencia;

            await CargarAdjuntosAsync();
        }

        private async Task CargarAdjuntosAsync()
        {
            if (string.IsNullOrEmpty(_tablaReferencia) || _idReferencia == 0 || _adjuntoService == null)
                return;

            try
            {
                List<AdjuntoDto> lista = await _adjuntoService.ListarAdjuntosAsync(_tablaReferencia, _idReferencia);
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
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_tablaReferencia) /*|| _idReferencia == 0*/ || _adjuntoService == null)
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

                    try
                    {
                        var adjunto = new Adjunto
                        {
                            TablaReferencia = _tablaReferencia,
                            IdReferencia = _idReferencia,
                            NombreArchivo = nombreArchivo,
                            UrlArchivo = rutaOrigen,
                            TipoDocumento = tipoDocumento,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Session.UsuarioActual.NombreUsuario
                        };

                        if (_idReferencia == 0)
                        {
                            _adjuntosTemporales.Add(adjunto);
                            var listaDtos = _adjuntosTemporales.Select(a => ConvertirADto(a)).ToList();
                            dgvAdjuntos.DataSource = null;
                            dgvAdjuntos.DataSource = listaDtos;
                        }
                        else
                        {
                            await _adjuntoService.CrearAdjuntoAsync(adjunto);
                            await CargarAdjuntosAsync();
                        }

                        await _adjuntoService.CrearAdjuntoAsync(adjunto);
                        await CargarAdjuntosAsync();

                        MessageBox.Show("Archivo adjuntado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al agregar el adjunto: " + ex.Message);
                    }
                }
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (dgvAdjuntos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un archivo.");
                return;
            }

            var adjunto = dgvAdjuntos.SelectedRows[0].DataBoundItem as Adjunto;
            if (adjunto == null || !File.Exists(adjunto.UrlArchivo))
            {
                MessageBox.Show("El archivo no existe o no se puede acceder.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = adjunto.NombreArchivo;
                sfd.Filter = "Todos los archivos|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(adjunto.UrlArchivo, sfd.FileName, overwrite: true);
                        MessageBox.Show("Archivo descargado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al descargar el archivo: " + ex.Message);
                    }
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAdjuntos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un archivo.");
                return;
            }

            var adjunto = dgvAdjuntos.SelectedRows[0].DataBoundItem as Adjunto;
            if (adjunto == null)
                return;

            var confirm = MessageBox.Show($"¿Desea eliminar el archivo '{adjunto.NombreArchivo}'?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await _adjuntoService.EliminarAdjuntoAsync(adjunto.IdAdjunto, Session.UsuarioActual.NombreUsuario); // Asegúrate de tener este método
                await CargarAdjuntosAsync();

                MessageBox.Show("Archivo eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el archivo: " + ex.Message);
            }
        }

        private AdjuntoDto ConvertirADto(Adjunto adjunto)
        {
            return new AdjuntoDto
            {
                IdAdjunto = adjunto.IdAdjunto,
                TipoDocumento = adjunto.TipoDocumento,
                NombreArchivo = adjunto.NombreArchivo,
                UrlArchivo = adjunto.UrlArchivo,
                UsuarioCreacion = adjunto.UsuarioCreacion,
                FechaCreacion = adjunto.FechaCreacion
            };
        }

        public async Task GuardarAdjuntosTemporalesAsync(int nuevoIdReferencia)
        {
            if (_adjuntosTemporales.Count == 0 || _adjuntoService == null)
                return;

            foreach (var adjunto in _adjuntosTemporales)
            {
                adjunto.IdReferencia = nuevoIdReferencia;
                await _adjuntoService.CrearAdjuntoAsync(adjunto);
            }

            _adjuntosTemporales.Clear();
            _idReferencia = nuevoIdReferencia; // Esto también recarga
        }

    }
}
