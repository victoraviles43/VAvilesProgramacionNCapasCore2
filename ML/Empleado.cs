using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        [Required(ErrorMessage = "Debe ingresar el Numero de empleado")]
        [Display(Name = "NumeroEmpleado")]
        public string NumeroEmpleado { get; set; } = null!;
        public string Rfc { get; set; }
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Debe ingresar el ApellidoPaterno")]
        [Display(Name = "ApellidoPaterno")]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; } = null!;

        //[Range(typeof(DateTime), "1/2/2020", "3/4/2020",
        //        ErrorMessage = "la fecha {0}deben de estar entre  {1} y {2}")]
        public string FechaNacimiento { get; set; }
        //[Range(8, 11,
        //   ErrorMessage = "El valor {0} debe estar entre  {1} y {2}.")]
        public string Nss { get; set; }
        public string FechaIngreso { get; set; }
        public string Foto { get; set; }
        public ML.Empresa Empresa { get; set; }
        public ML.Poliza Poliza { get; set; }


        public List<object> Empleados { get; set; }

        public string Action { get; set; }
    }
}
