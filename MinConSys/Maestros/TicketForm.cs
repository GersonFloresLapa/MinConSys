using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Helpers;
using MinConSys.Maestros;
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
    public partial class TicketForm : Form
    {
        private readonly ITicketService _ticketService;
        private readonly IEmpresaService _empresaService;

        private List<TicketDto> _tickets;
        public TicketForm(ITicketService ticketService, IEmpresaService empresaService)
        {
            InitializeComponent();
            _ticketService = ticketService;
            _empresaService = empresaService;
        }
        private async void TicketForm_Load(object sender, EventArgs e)
        {
            await CargarTicketsAsync();
            dgvTickets.ConfigurarGenerico(_tickets);
        }

        private async Task CargarTicketsAsync()
        {
            try
            {
                _tickets = (await _ticketService.ListarTicketsAsync()).ToList();
                dgvTickets.DataSource = null;
                dgvTickets.DataSource = _tickets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Tickets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new TicketEditForm(_ticketService, _empresaService, 0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarTicketsAsync(); // Vuelves a cargar la lista
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idTicket = Convert.ToInt32(dgvTickets.CurrentRow.Cells["IdTicket"].Value);
            using (var form = new TicketEditForm(_ticketService, _empresaService, idTicket))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarTicketsAsync(); // Vuelves a cargar la lista
                }
            }
        }

    }
}
