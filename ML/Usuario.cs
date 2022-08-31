using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {

       
        public int IdUsuario { get; set; }

        //public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar el Nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el ApellidoPaterno")]
        [Display(Name = "ApellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        //[Required(ErrorMessage = "Email es requerido ")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email No es valido")]
        public string Email { get; set; }

        public string Sexo { get; set; }

        public string Telefono { get; set; }

        //[Required(ErrorMessage = "Celular es requerido ")]
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Ingrese un número de teléfono válido.")]
        public string Celular { get; set; }


        //[Range(typeof(DateTime), "1/2/2020", "3/4/2020",
        //        ErrorMessage = "la fecha {0}deben de estar entre  {1} y {2}")]
        public string FechaNacimiento { get; set; }

        //[Range(10, 18,
        //ErrorMessage = "El valor {0} debe estar entre  {1} y {2}.")]
        public string CURP { get; set; }
        public string Imagen { get; set; }

        public string Pasword { get; set; }
        public bool Estatus { get; set; }
        public ML.Rol Rol { get; set; }

     
        public ML.Pais Pais { get; set; }
        public ML.Estado Estado { get; set; }
        public ML.Direccion Direccion { get; set; }
        public ML.Municipio Municipio { get; set; }
        public ML.Colonia Colonia { get; set; }
        public List<object> Usuarios { get; set; }

    }
}
