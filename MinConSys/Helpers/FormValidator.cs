using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Helpers
{
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public static class FormValidator
    {
        private static ErrorProvider _errorProvider;
        private static Color _colorError = Color.MistyRose;
        private static Color _colorNormal = SystemColors.Window;

        public static bool Validar(Control container, out string mensaje)
        {
            mensaje = "";
            bool valido = true;

            if (_errorProvider == null)
            {
                _errorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };
            }

            _errorProvider.Clear();

            foreach (Control control in container.Controls)
            {
                if (control.HasChildren)
                {
                    if (!Validar(control, out string subMensaje))
                    {
                        mensaje += subMensaje;
                        valido = false;
                    }
                    continue;
                }

                if (control.Tag == null) continue;

                var reglas = control.Tag.ToString().Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                
                // Si el control no es requerido y está vacío, no seguimos validando
                bool esRequerido = reglas.Any(r => r.Trim().Equals("requerido", StringComparison.OrdinalIgnoreCase));
                
                bool sinTexto = string.IsNullOrWhiteSpace(control.Text);
                bool esComboBox = control is ComboBox;
                
                bool comboSinSeleccion = esComboBox && ((ComboBox)control).SelectedIndex == -1;

                if (!esRequerido && (sinTexto || comboSinSeleccion))
                {
                    _errorProvider.SetError(control, "");
                    control.BackColor = _colorNormal;
                    continue;
                }

                foreach (var reglaRaw in reglas)
                {
                    string error = "";
                    string regla = reglaRaw.Trim();

                    if (regla.StartsWith("longitud:", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!int.TryParse(regla.Split(':')[1], out int esperado) || control.Text.Length != esperado)
                            error = $"{NombreAmigable(control.Name)} debe tener {esperado} caracteres.";
                    }
                    else if (regla.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
                    {
                        string patron = reglaRaw.Substring(6);
                        if (!Regex.IsMatch(control.Text, patron))
                            error = $"{NombreAmigable(control.Name)} tiene un formato inválido.";
                    }
                    else
                    {
                        switch (regla.ToLower())
                        {
                            case "requerido":
                                if (control is ComboBox combo)
                                {
                                    if (combo.SelectedIndex == -1 || combo.SelectedValue == null || combo.SelectedValue.ToString() == "0")
                                        error = $"{NombreAmigable(control.Name)} es obligatorio.";
                                }
                                else
                                {
                                    if (string.IsNullOrWhiteSpace(control.Text))
                                        error = $"{NombreAmigable(control.Name)} es obligatorio.";
                                }
                                break;

                            case "email":
                                if (!Regex.IsMatch(control.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                                    error = $"{NombreAmigable(control.Name)} no es un correo válido.";
                                break;

                            case "entero":
                                if (!int.TryParse(control.Text, out _))
                                    error = $"{NombreAmigable(control.Name)} debe ser un número entero.";
                                break;

                            case "decimal":
                                if (!decimal.TryParse(control.Text, out _))
                                    error = $"{NombreAmigable(control.Name)} debe ser un número decimal.";
                                break;
                            case "anio":
                                if (!int.TryParse(control.Text, out int anio) || anio < 1900 || anio > DateTime.Now.Year)
                                    error = $"{NombreAmigable(control.Name)} debe ser un año válido entre 1900 y {DateTime.Now.Year}.";
                                break;

                        }
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        mensaje += error + Environment.NewLine;
                        _errorProvider.SetError(control, error);
                        control.BackColor = _colorError;
                        valido = false;
                        break;
                    }
                    else
                    {
                        _errorProvider.SetError(control, "");
                        control.BackColor = _colorNormal;
                    }
                }
            }

            return valido;
        }

        private static string NombreAmigable(string nombre)
        {
            return nombre.Replace("txt", "")
                         .Replace("cbo", "")
                         .Replace("lbl", "")
                         .Replace("_", " ")
                         .Trim();
        }
    }
}
