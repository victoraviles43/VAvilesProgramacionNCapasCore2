using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            //usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            //usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            //usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            //ML.Result result = BL.Usuario.UsuarioGetAllEF(usuario);
            //usuario.Usuarios = result.Objects;  
            //return View(usuario);
            ML.Result resultUsuarios = new ML.Result();
            resultUsuarios.Objects = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("api/usuario/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuarios.Objects.Add(resultItemList);
                    }
                }
                usuario.Usuarios = resultUsuarios.Objects;
            }
            return View(usuario);

        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;


            ML.Result result = BL.Usuario.UsuarioGetAllEF(usuario);


            usuario.Usuarios = result.Objects;
            //aseguradora.Aseguradoras = result.Objects;
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();


            ML.Result resultRol = BL.Rol.RolAllEF();
            ML.Result resultPais = BL.Pais.GetAll();
            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();





            // usuario.Pais = new ML.Pais();

            if (IdUsuario != null)
            {
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);

                //ML.Result resultUsuarios = new ML.Result();
                //resultUsuarios.Objects = new List<Object>();



                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);
                    var responseTask = client.GetAsync("api/usuario/GetById/" + IdUsuario);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = new ML.Usuario();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        result.Correct = true;
                    }

                }


                if (result.Correct)
                {
                    //usuario = new ML.Usuario();

                    usuario = ((ML.Usuario)result.Object);//unboxing 
                    usuario.Rol.Roles = resultRol.Objects.ToList();



                    ML.Result resultEstados = BL.Estado.EstadoByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultMunicipios = BL.Municipio.MunicipioByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    ML.Result resultColonia = BL.Colonia.ColoniaByIdMunicipio(usuario.Direccion.Colonia.IdColonia);


                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects.ToList();
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects.ToList();
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects.ToList();
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects.ToList();


                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = "Error" + result.ErrorMessage;
                    return PartialView("Modal");

                }

            }
            else
            {

                usuario.Rol.Roles = resultRol.Objects.ToList();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects.ToList();
                return View(usuario);
            }

        }




        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile file = Request.Form.Files["Imagen"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);

            }
            //  ML.Result result = new ML.Result();

            if (usuario.IdUsuario != 0)
            {
                //ML.Result result = BL.Usuario.UpdateEF(usuario);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("api/usuario/update", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    //if (result.IsSuccessStatusCode)
                    //{
                    //    return RedirectToAction("GetAll");
                    //}

                    //return View("GetAll");

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha registrado la materia";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha podido registrar la materia";
                        return PartialView("Modal");
                    }
                }


            }
            else
            {
                if (ModelState.IsValid)
                {
                    // ML.Result result = BL.Usuario.AddEF(usuario);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/usuario/add", usuario);
                        postTask.Wait();

                        var result = postTask.Result;
                        //if (result.IsSuccessStatusCode)
                        //{
                        //    return RedirectToAction("GetAll");
                        //}

                        //return View("GetAll");

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Se ha registrado la materia";
                            return PartialView("Modal");
                        }
                        else
                        {
                            ViewBag.Message = "No se ha podido registrar la materia";
                            return PartialView("Modal");
                        }
                    }


                }
                else
                {
                    ML.Result resultRol = BL.Rol.RolAllEF();
                    ML.Result resultPais = BL.Pais.GetAll();
                    ML.Result resultEstados = BL.Estado.EstadoByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultMunicipios = BL.Municipio.MunicipioByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    ML.Result resultColonia = BL.Colonia.ColoniaByIdMunicipio(usuario.Direccion.Colonia.IdColonia);
                    usuario.Rol.Roles = resultRol.Objects.ToList();
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects.ToList();
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects.ToList();
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects.ToList();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects.ToList();


                    return View(usuario);
                }

            }



        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;

            // ML.Result result = BL.Usuario.DeleteEF(usuario);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Usuario>("api/usuario/delete", usuario);
                postTask.Wait();

                var result = postTask.Result;
                //if (result.IsSuccessStatusCode)
                //{
                //    return RedirectToAction("GetAll");
                //}

                //return View("GetAll");

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha eliminado el usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha podido registrar la materia";
                    return PartialView("Modal");
                }
            }


        }
        public ActionResult UpdateEstatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            if (result.Correct)
            {
                usuario = ((ML.Usuario)result.Object);

                usuario.Estatus = (usuario.Estatus) ? false : true; // Operador ternario 


                ML.Result resultUpdate = BL.Usuario.UpdateEF(usuario);

                ViewBag.Message = "Se actualizo el status del usuario";
            }

            else
            {
                ViewBag.Message = "No se actualizo el status del usuario" + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.EstadoByIdPais(IdPais);

            return Json(result.Objects);
        }
        public JsonResult MunicipioGetByEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.MunicipioByIdEstado(IdEstado);

            return Json(result.Objects);
        }
        public JsonResult ColoniaGetByMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.ColoniaByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }
        //metodo para convertir a bytes la imagen
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
