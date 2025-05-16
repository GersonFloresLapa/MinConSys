using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MinConSys.Modales
{
    public partial class FormFiltro : Form
    {
        public List<string> ValoresSeleccionados { get; private set; }
        public event Action<List<string>> FiltroAplicado;
        private bool _actualizandoLista = false; // Flag para evitar eventos recursivos

        public FormFiltro(List<string> valores, List<string> seleccionadosPrevios)
        {
            InitializeComponent();

            ValoresSeleccionados = new List<string>();
            _actualizandoLista = true;

            // Agregar elemento "Elegir todos"
            checkedListBox1.Items.Add("(Elegir todos)");

            // Agregar los valores a la lista
            foreach (var v in valores)
            {
                checkedListBox1.Items.Add(v);
            }

            // Establecer el estado de selección según los valores previos
            if (seleccionadosPrevios != null)
            {
                for (int i = 1; i < checkedListBox1.Items.Count; i++)
                {
                    string valor = checkedListBox1.Items[i].ToString();
                    checkedListBox1.SetItemChecked(i, seleccionadosPrevios.Contains(valor));
                }

                // Actualizar el estado de "Elegir todos" según la selección
                checkedListBox1.SetItemChecked(0, TodosEstanMarcados());
            }
            else
            {
                // Si no hay selección previa, marcar todos por defecto
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }

            _actualizandoLista = false;

            // Suscribirse a eventos
            checkedListBox1.MouseDown += CheckedListBox1_MouseDown;

            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Deactivate += FormFiltro_Deactivate;
            btnAplicar.Click += btnAplicar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }


        private void FormFiltro_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            ValoresSeleccionados = new List<string>();

            // Recopilar solo los valores seleccionados (no incluir "Elegir todos")
            for (int i = 1; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    ValoresSeleccionados.Add(checkedListBox1.Items[i].ToString());
            }

            // Invocar el evento si hay suscriptores
            FiltroAplicado?.Invoke(ValoresSeleccionados);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool TodosEstanMarcados()
        {
            for (int i = 1; i < checkedListBox1.Items.Count; i++)
            {
                if (!checkedListBox1.GetItemChecked(i))
                    return false;
            }
            return true;
        }
        private void CheckedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (index < 0) return;

            if (e.Button == MouseButtons.Left)
            {
                // Cambiar manualmente el estado
                bool isChecked = checkedListBox1.GetItemChecked(index);
                checkedListBox1.SetItemChecked(index, !isChecked);

                // Lógica para "Elegir todos"
                if (index == 0)
                {
                    for (int i = 1; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, !isChecked);
                    }
                }
                else
                {
                    // Verificar si todos están marcados (para actualizar "Elegir todos")
                    bool todosMarcados = true;
                    for (int i = 1; i < checkedListBox1.Items.Count; i++)
                    {
                        if (!checkedListBox1.GetItemChecked(i))
                        {
                            todosMarcados = false;
                            break;
                        }
                    }
                    checkedListBox1.SetItemChecked(0, todosMarcados);
                }

                // Evitar que el ítem gane el foco
                checkedListBox1.ClearSelected(); // ← esto evita que quede seleccionado
            }
        }
    }
}