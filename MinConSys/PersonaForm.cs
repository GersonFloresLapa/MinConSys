using MinConSys.Core.Interfaces.Services;
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

namespace MinConSys
{
    public partial class PersonaForm : Form
    {
        private readonly IPersonaService _personaService;
        private List<PersonaDto> _personas;
        public PersonaForm(IPersonaService personaService)
        {
            InitializeComponent();
            _personaService = personaService;
 
        }

        private async void PersonaForm_Load(object sender, EventArgs e)
        {
            await CargarPersonasAsync();
        }
        private async Task CargarPersonasAsync()
        {
            try
            {
                _personas = (await _personaService.ListarPersonasAsync()).ToList();
                dgvPersonas.DataSource = null;
                dgvPersonas.DataSource = _personas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
