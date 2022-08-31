using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Aseguradoras = new HashSet<Aseguradora>();
            Direccions = new HashSet<Direccion>();
            Polizas = new HashSet<Poliza>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Curp { get; set; }
        public string? Imagen { get; set; }
        public int? IdRol { get; set; }
        public bool? Estatus { get; set; }
        public string? Pasword { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Aseguradora> Aseguradoras { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }
        public virtual ICollection<Poliza> Polizas { get; set; }
        public string RolNombre { get; set; }
        public string DireccionCalle { get; set; }
        public string ColoniaCodigo { get; set; }
        public string MunicipioNombre { get; set; }
        public string NombrePais { get; set; }
        public string EstadoNombre { get; set; }
        public string NombreColonia { get; set; }
        public string DireccionNumeroInterior { get; set; }
        public string DireccionNumeroExterior { get; set; }
        public int DireccionID { get; set; }
        public int municipioId { get; set; }
        public int EstadoId { get; set; }
        public int ColoniaId { get; set; }
        public int paisId { get; set; }
    }
}
