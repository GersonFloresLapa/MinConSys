using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        public void SetService(IAdjuntoService service)
        {
            _adjuntoService = service;
            CargarAdjuntos(); // Opcional: recarga al asignar servicio
        }

        private async void CargarAdjuntos()
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
    }
}
