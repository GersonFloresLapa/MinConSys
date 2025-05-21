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
    public partial class TicketEditForm : Form
    {

        private readonly ITicketService _ticketService;
        private readonly IEmpresaService _empresaService;

        private List<ComboItem> _empresas;
        private List<ComboItem> _proveedores;
        private readonly int _idTicket;
        public TicketEditForm(ITicketService ticketService,
                                IEmpresaService empresaService,
                                int idTicket
                                )
        {
            InitializeComponent();
            _ticketService = ticketService;
            _empresaService = empresaService;
            _idTicket = idTicket;
        }

    }
}
