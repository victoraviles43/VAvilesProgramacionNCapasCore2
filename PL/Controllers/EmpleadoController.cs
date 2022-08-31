using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Aseguradora
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;
            //llamo al get all de aseguradora
            ML.Result result = BL.Empleado.GetAll(empleado);

            //creo una instancia de aseguradora


            //paso de mi lista result.objects a mi lista de aseguradoras
            empleado.Empleados = result.Objects;

            return View(empleado);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {

            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.Empresa.IdEmpresa = (empleado.ApellidoMaterno == null) ? 0 : empleado.Empresa.IdEmpresa;


            ML.Result result = BL.Empleado.GetAll(empleado);


            empleado.Empleados = result.Objects;
            //aseguradora.Aseguradoras = result.Objects;
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            empleado.Poliza = new ML.Poliza();

            ML.Result resultEmpresa = BL.Empresa.EmpresaGetAll();
            ML.Result resultPoliza = BL.Poliza.GetAll();
            empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
            empleado.Poliza.Polizas = resultPoliza.Objects.ToList();

            if (NumeroEmpleado != null)
            {
                empleado.Action = "Update";
                ML.Result result = BL.Empleado.GetByIdEF(NumeroEmpleado);

                if (result.Correct)

                {
                    empleado = ((ML.Empleado)result.Object);
                    empleado.Empresa = new ML.Empresa();
                    empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
                    empleado.Poliza = new ML.Poliza();
                    empleado.Poliza.Polizas = resultPoliza.Objects.ToList();


                    return View(empleado);


                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar " + result.ErrorMessage;
                    return PartialView("Modal");



                }

            }
            else
            {
                empleado.Action = "Add";
                empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
                empleado.Poliza.Polizas = resultPoliza.Objects.ToList();
                return View(empleado);
            }



        }


        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["Imagen"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                empleado.Foto = Convert.ToBase64String(ImagenBytes);

            }

            ML.Result result = new ML.Result();

            //si trae el id va acutualizar 
            if (empleado.NumeroEmpleado == null)
            {
                result = BL.Empleado.Update(empleado);

                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se actualizo" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                if(ModelState.IsValid)
                {
                    result = BL.Empleado.Add(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Message = "se ha agregado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha agregado";
                        return PartialView("Modal");
                    }
                }
                else
                {
                    empleado.Empresa = new ML.Empresa();
                    empleado.Poliza = new ML.Poliza();

                    ML.Result resultEmpresa = BL.Empresa.EmpresaGetAll();
                    ML.Result resultPoliza = BL.Poliza.GetAll();
                    empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
                    empleado.Poliza.Polizas = resultPoliza.Objects.ToList();

                    return View(empleado);
                }
            }

        }
        [HttpGet]
        public ActionResult Delete(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.NumeroEmpleado = NumeroEmpleado;

            ML.Result result = BL.Empleado.Delete(empleado);
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return PartialView("Modal");
            }

        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        [HttpPost]

        public ActionResult CargaMasiva()
        {
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            try
            {
                IFormFile archivo = Request.Form.Files["Archivo"];
                using (StreamReader sr = new StreamReader(archivo.OpenReadStream()))
                {
                    string line;
                    line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] datos = line.Split('|');

                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumeroEmpleado = datos[0];
                        empleado.Rfc = datos[1];
                        empleado.Nombre = datos[2];
                        empleado.ApellidoPaterno = datos[3];
                        empleado.ApellidoMaterno = datos[4];
                        empleado.Email = datos[5];
                        empleado.Telefono = datos[6];
                        empleado.FechaNacimiento = datos[7].ToString();
                        empleado.Nss = datos[8];
                        empleado.FechaIngreso = datos[9].ToString();
                        empleado.Foto = null;

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = int.Parse(datos[11]);

                        empleado.Poliza = new ML.Poliza();
                        empleado.Poliza.IdPoliza = int.Parse(datos[12]);

                        ML.Result result = BL.Empleado.Add(empleado);

                        if (!result.Correct) //si el resultado es diferente a correcto
                        {

                            resultErrores.Objects.Add(
                             "No se inserto el Nombre " + empleado.NumeroEmpleado +
                            "No se inserto el RFC " + empleado.Rfc +
                            "No se inserto el Nombre " + empleado.Nombre +
                            "No se inserto el ApellidoPaterno " + empleado.ApellidoPaterno +
                            "No se inserto el ApellidoMaterno " + empleado.ApellidoMaterno +
                            "No se inserto el Email " + empleado.Email +
                            "No se inserto el Telefono " + empleado.Telefono +
                            "No se inserto el FechaNacimiento " + empleado.FechaNacimiento +
                            "No se inserto el Nss " + empleado.Nss +
                            "No se inserto el FechaIngreso " + empleado.FechaIngreso +
                            "No se inserto el Foto " + empleado.Foto +
                            "No se inserto el Empresa " + empleado.Empresa.IdEmpresa +
                            "No se inserto el Poliza " + empleado.Poliza.IdPoliza);
                        } //Se le asigna agrega la lista de errores

                    }

                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return PartialView("Modal");
        }

        //public ActionResult Download()
        //{
        //    string file = HttpContext.Session.GetString["RutaDescarga"];
        //    string contentType = "text/plain";
        //    return File(file, contentType, Path.GetFileName(file));
        //}

    }
}
