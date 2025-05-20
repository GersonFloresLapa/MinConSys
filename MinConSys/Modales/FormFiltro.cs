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
        private List<string> _todosLosValores = new List<string>();
        private HashSet<string> _seleccionadosPrevios = new HashSet<string>();
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

                    if (seleccionadosPrevios.Contains(valor))
                    {
                        _seleccionadosPrevios.Add(valor);
                    }
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
            _todosLosValores = valores.ToList();

            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            //cboFiltro.SelectedIndexChanged += (s, e) => AplicarFiltroTexto();

            cboFiltro.Items.Add("Contiene");
            cboFiltro.Items.Add("Empieza por");
            cboFiltro.Items.Add("Termina en");
            cboFiltro.Items.Add("Exacto");
            cboFiltro.SelectedIndex = 0;
        }


        private void FormFiltro_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            ValoresSeleccionados = new List<string>();

            ValoresSeleccionados = _seleccionadosPrevios.ToList();

            // Recopilar solo los valores seleccionados (no incluir "Elegir todos")
            //for (int i = 1; i < checkedListBox1.Items.Count; i++)
            //{
            //    if (checkedListBox1.GetItemChecked(i))
            //        ValoresSeleccionados.Add(checkedListBox1.Items[i].ToString());
            //}

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
                        string valor = checkedListBox1.Items[i].ToString();

                        checkedListBox1.SetItemChecked(i, !isChecked);

                        if (!isChecked)
                            _seleccionadosPrevios.Add(valor);
                        else
                            _seleccionadosPrevios.Remove(valor);
                    }
                }
                else
                {
                    // Verificar si todos están marcados (para actualizar "Elegir todos")
                    bool todosMarcados = true;

                    string valor = checkedListBox1.Items[index].ToString();

                    if (checkedListBox1.GetItemChecked(index))
                        _seleccionadosPrevios.Add(valor);
                    else
                        _seleccionadosPrevios.Remove(valor);

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
          
                checkedListBox1.ClearSelected(); // ← esto evita que quede seleccionado

            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltroTexto();
        }

        private void AplicarFiltroTexto()
        {
            if (_actualizandoLista) return;

            _actualizandoLista = true;

            string texto = txtBuscar.Text.Trim().ToLower();
            string criterio = cboFiltro.SelectedItem?.ToString() ?? "Contiene";

            checkedListBox1.Items.Clear();
            checkedListBox1.Items.Add("(Elegir todos)");

            var filtrados = _todosLosValores.Where(val =>
            {
                string valLower = val.ToLower();
                if (criterio == "Contiene")
                    return valLower.Contains(texto);
                else if (criterio == "Empieza por")
                    return valLower.StartsWith(texto);
                else if (criterio == "Termina en")
                    return valLower.EndsWith(texto);
                else if (criterio == "Exacto")
                    return valLower == texto;
                else
                    return true;
            }).ToList();

            foreach (var val in filtrados)
            {
                int index = checkedListBox1.Items.Add(val);
                checkedListBox1.SetItemChecked(index, _seleccionadosPrevios.Contains(val));
            }

            checkedListBox1.SetItemChecked(0, TodosEstanMarcados());
          
            _actualizandoLista = false;
        }

    }
}