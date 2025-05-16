using MinConSys.Core.Interfaces.Services;
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

namespace MinConSys
{
    public partial class PersonaForm : Form
    {
        private readonly IPersonaService _personaService;
        private readonly ITablaGeneralesService _tablaGeneralesService;
        private List<PersonaDto> _personas;
        public PersonaForm(IPersonaService personaService, ITablaGeneralesService tablaGeneralesService)
        {
            InitializeComponent();
            _personaService = personaService;
            _tablaGeneralesService = tablaGeneralesService;
        }
        private async void PersonaForm_Load(object sender, EventArgs e)
        {
            await CargarPersonasAsync();
            dgvPersonas.ConfigurarGenerico();
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
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new PersonaEditForm(_personaService, _tablaGeneralesService, 0))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarPersonasAsync(); // Vuelves a cargar la lista
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idPersona = Convert.ToInt32(dgvPersonas.CurrentRow.Cells["IdPersona"].Value);
            using (var form = new PersonaEditForm(_personaService,_tablaGeneralesService, idPersona))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await CargarPersonasAsync(); // Vuelves a cargar la lista
                }
            }
        }
    }
}
