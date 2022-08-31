using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult EmpleadoCargaMasiva()
           
        
        
        
        {
            ML.Result result = new ML.Result();
            return View(result);
        }
        [HttpPost]
        public ActionResult EmpleadoCargaMasiva(ML.Empleado empleado)
        {

            IFormFile archivo = Request.Form.Files["FileExcel"];

            //Sesion ya trae informacion 
            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (archivo.Length > 0)
                {
                    string fileName = Path.GetFileName(archivo.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            //Programa 
                            ML.Result resultEmpleados = BL.Empleado.ConvertirExceltoDataTable(connectionString);

                            if (resultEmpleados.Correct)
                            {
                                //Valida | Insertar
                                ML.Result resultValidacion = BL.Empleado.ValidarExcel(resultEmpleados.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    //Se Crea la session guarda el archivo
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                    }
                }
                else
                {
                    ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                //mismo archivo que ya esta validado 
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;
                // traducir a sql 
                ML.Result resultData = BL.Empleado.ConvertirExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();
                    //Sacar registro para insert de  
                    foreach (ML.Empleado empleadoItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Empleado.Add(empleadoItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Empleado con nombre: " + empleadoItem.NumeroEmpleado     + " Rfc:" + empleadoItem.Rfc + "Nombre:" + empleadoItem.Nombre + "Creditos "+empleadoItem.ApellidoPaterno+"ApellidoMaterno "+empleado.ApellidoMaterno +"Email"+empleado.Email+"Telefono"+empleado.Telefono+"FechaNacimineto"+empleado.FechaNacimiento+"Foto"+empleado.Foto+"IdEmpresa"+empleado.Empresa.IdEmpresa+"IdPoliza"+empleado.Poliza.IdPoliza +" Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {
                        string folderError = _configuration["PathFolderError:value"];
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath,folderError + @"\logError.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los emplados No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Empleados han sido registrados correctamente";
                    }

                }

            }
            return PartialView("Modal");
        }

    }
}
