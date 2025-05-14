using MinConSys.Core.Models.Common;
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

namespace MinConSys.Modales
{
    public partial class SelectorGenericoForm : Form
    {
        private List<ComboItem> _items;
        public ComboItem ItemSeleccionado { get; private set; }
        public SelectorGenericoForm(List<ComboItem> items)
        {
            InitializeComponent();
            _items = items;
            
           
        }

        private void dgvDatos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ItemSeleccionado = (ComboItem)dgvDatos.Rows[e.RowIndex].DataBoundItem;
                DialogResult = DialogResult.OK;
            }
        }

        private void CargarDatos()
        {
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = "Descripcion" });
            dgvDatos.DataSource = _items;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.ToLower();
            dgvDatos.DataSource = _items
                .Where(x => x.Descripcion.ToLower().Contains(filtro))
                .ToList();
        }

        private void SelectorGenericoForm_Load(object sender, EventArgs e)
        {
            CargarDatos();

            dgvDatos.ConfigurarGenerico();
        }
    }
}
