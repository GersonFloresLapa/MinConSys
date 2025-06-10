using System;

namespace MinConSys.Core.Models.Base
{
    public class Representante
    {
        public int IdRepresentante { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPersona { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Cargo { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
