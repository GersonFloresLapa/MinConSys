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
    public partial class SelectorTicketForm : Form
    {
        private List<TicketRumaDto> _items;
        public TicketRumaDto ItemSeleccionado { get; private set; }
        public SelectorTicketForm(List<TicketRumaDto> items)
        {
            InitializeComponent();
            _items = items;
            
           
        }

        private void dgvDatos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ItemSeleccionado = (TicketRumaDto)dgvDatos.Rows[e.RowIndex].DataBoundItem;
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


        private void SelectorTicketForm_Load(object sender, EventArgs e)
        {
            CargarDatos();
            dgvDatos.ConfigurarGenerico(_items);
            //dgvDatos.ConfigurarGenerico();
        }
    }
}
