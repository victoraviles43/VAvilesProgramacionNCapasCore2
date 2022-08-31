using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Aseguradora
    {
        public int IdAsegurradora { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdUsuario { get; set; }
        public string? Imagen { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public string UsuarioNombre { get; set; }
    }
}
