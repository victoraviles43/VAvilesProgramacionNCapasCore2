using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DependienteController : Controller
    {
        public ActionResult GetAllEmpleados()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;
            ML.Result result = BL.Empleado.GetAll(empleado);
            empleado.Empleados = result.Objects;

            return View(empleado);
        }
      
        public ActionResult GettAllDependiente()
        {
            //llamo al get all de aseguradora
            ML.Result result = BL.Dependiente.GetAll();

            //creo una instancia de aseguradora
            ML.Dependiente dependiente = new ML.Dependiente();

            //paso de mi lista result.objects a mi lista de aseguradoras
            dependiente.Dependientes = result.Objects;

            return View(dependiente);
        }
    }
}
