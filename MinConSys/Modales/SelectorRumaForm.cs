using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
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
    public partial class SelectorRumaForm : Form
    {
        private List<RumaDto> _items;
        public RumaDto ItemSeleccionado { get; private set; }
        public SelectorRumaForm(List<RumaDto> items)
        {
            InitializeComponent();
            _items = items;
            
           
        }

        private void dgvDatos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ItemSeleccionado = (RumaDto)dgvDatos.Rows[e.RowIndex].DataBoundItem;
                DialogResult = DialogResult.OK;
            }
        }

        private void CargarDatos()
        {
            //dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id" });
            //dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = "Descripcion" });
            dgvDatos.DataSource = _items;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.ToLower();

            dgvDatos.DataSource = _items
                .Where(x =>
                    x.GetType()
                     .GetProperties()
                     .Select(p => p.GetValue(x))
                     .Any(value => value != null && value.ToString().ToLower().Contains(filtro))
                )
                .ToList();
        }


        private void SelectorRumaForm_Load(object sender, EventArgs e)
        {
            CargarDatos();
            dgvDatos.ConfigurarGenerico(_items);
            //dgvDatos.ConfigurarGenerico();
        }
    }
}
