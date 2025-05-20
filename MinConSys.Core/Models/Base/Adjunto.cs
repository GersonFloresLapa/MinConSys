using System;

namespace MinConSys.Core.Models.Base
{
    public class Adjunto
    {
        public int IdAdjunto { get; set; }
        public string TablaReferencia { get; set; }
        public int IdReferencia { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public char Estado { get; set; } = 'A';
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
