using MinConSys.Modales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MinConSys.Helpers
{
    public static class DataGridViewHelper
    {
        public static void ConfigurarGenerico<T>(this DataGridView dgv, List<T> listaOriginal)
        {
            dgv.Tag = listaOriginal;

            dgv.AutoGenerateColumns = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;

            foreach (DataGridViewColumn col in dgv.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            if (dgv.Columns.Count > 0)
                dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Diccionario para guardar filtros activos por columna
            var filtrosActivos = new Dictionary<string, List<string>>();

            dgv.ColumnHeaderMouseClick += delegate (object sender, DataGridViewCellMouseEventArgs e)
            {
                int colIndex = e.ColumnIndex;
                string columnaFiltrada = dgv.Columns[colIndex].DataPropertyName;

                var listaOriginalInterna = dgv.Tag as List<T>;
                if (listaOriginalInterna == null)
                    return;

                // Obtener todos los valores únicos de la columna desde la lista original
                var valores = new HashSet<string>();
                var tipo = typeof(T);
                var propInfo = tipo.GetProperty(columnaFiltrada);
                if (propInfo == null)
                    return;

                foreach (var item in listaOriginalInterna)
                {
                    var valor = propInfo.GetValue(item, null);
                    if (valor != null)
                    {
                        var strValor = valor.ToString();
                        if (!string.IsNullOrEmpty(strValor))
                            valores.Add(strValor);
                    }
                }

                if (valores.Count == 0)
                    return;

                // Obtener valores seleccionados previos si los hay
                List<string> seleccionadosPrevios;
                if (!filtrosActivos.TryGetValue(columnaFiltrada, out seleccionadosPrevios))
                {
                    seleccionadosPrevios = valores.ToList(); // si no había filtro, selecciona todos
                }

                var headerRect = dgv.GetCellDisplayRectangle(colIndex, -1, true);
                var screenPoint = dgv.PointToScreen(new Point(headerRect.Left, headerRect.Bottom));

                var filtro = new FormFiltro(valores.ToList(), seleccionadosPrevios);
                filtro.StartPosition = FormStartPosition.Manual;
                filtro.Location = screenPoint;

                filtro.FiltroAplicado += delegate (List<string> seleccionados)
                {
                    filtrosActivos[columnaFiltrada] = seleccionados;

                    var filtrados = FiltrarPorSeleccionados(listaOriginalInterna, filtrosActivos);
                    dgv.DataSource = filtrados;
                };

                filtro.Show();
            };
        }

        public static List<T> FiltrarPorSeleccionados<T>(List<T> listaOriginal, Dictionary<string, List<string>> filtros)
        {
            if (filtros == null || filtros.Count == 0)
                return listaOriginal;

            var tipo = typeof(T);
            var resultado = new List<T>();

            foreach (var item in listaOriginal)
            {
                bool coincide = true;

                foreach (var filtro in filtros)
                {
                    var prop = tipo.GetProperty(filtro.Key);
                    if (prop == null)
                        continue;

                    var valor = prop.GetValue(item, null);
                    var strValor = valor != null ? valor.ToString() : null;

                    if (!filtro.Value.Contains(strValor))
                    {
                        coincide = false;
                        break;
                    }
                }

                if (coincide)
                    resultado.Add(item);
            }

            return resultado;
        }
    }
}
