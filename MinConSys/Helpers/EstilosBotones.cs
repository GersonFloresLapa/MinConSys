using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys.Helpers
{
    public static class EstilosBotones
    {
        private static readonly Font FuenteBoton = new Font("Segoe UI", 9F, FontStyle.Bold);

        public static void AplicarEstiloNuevo(ToolStripButton btn)
        {
            AplicarEstiloBase(btn, "Nuevo", Properties.Resources.nuevo, Color.LightGreen);
        }

        public static void AplicarEstiloEditar(ToolStripButton btn)
        {
            AplicarEstiloBase(btn, "Editar", Properties.Resources.editar, Color.LightSkyBlue);
        }

        public static void AplicarEstiloGuardar(ToolStripButton btn)
        {
            AplicarEstiloBase(btn, "Guardar", Properties.Resources.guardar, Color.LightSalmon);
        }

        public static void AplicarEstiloExportar(ToolStripButton btn)
        {
            AplicarEstiloBase(btn, "Exportar", Properties.Resources.exportar, Color.Khaki);
        }

        public static void AplicarEstiloEliminar(ToolStripButton btn)
        {
            AplicarEstiloBase(btn, "Eliminar", Properties.Resources.eliminar, Color.LightCoral);
        }

        private static void AplicarEstiloBase(ToolStripButton btn, string texto, Image icono, Color fondo)
        {
            btn.Text = texto;
            btn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText; // Mostrar imagen y texto
            btn.TextImageRelation = TextImageRelation.ImageAboveText; // Imagen arriba, texto abajo
            btn.Text = ""; // Sin texto para mostrar solo la imagen
            btn.BackgroundImage = icono;
            btn.BackgroundImageLayout = ImageLayout.Zoom; // Escala la imagen manteniendo proporción
            btn.Font = FuenteBoton;
            btn.BackColor = fondo;
            btn.ForeColor = Color.Black;
            //btn.FlatStyle = FlatStyle.Flat;
            btn.Size = new Size(48, 48); // Nuevo tamaño más pequeño
        }

    }
}
