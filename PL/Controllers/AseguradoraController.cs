using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {

        // GET: Aseguradora
        public ActionResult GetAll()
        {
            //llamo al get all de aseguradora
            ML.Result result = BL.Aseguradora.AseguradoraGetAllEF();

            //creo una instancia de aseguradora
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            //paso de mi lista result.objects a mi lista de aseguradoras
            aseguradora.Aseguradoras = result.Objects;

            return View(aseguradora);
        }
        [HttpGet]


        public ActionResult Form(int? IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();


            aseguradora.Usuario = new ML.Usuario();

            ML.Result resultUsuario = BL.Usuario.UsuarioGetAllEF(aseguradora.Usuario);
            aseguradora.Usuario.Usuarios = resultUsuario.Objects.ToList();
            if (IdAseguradora == null)
            {


                return View(aseguradora);

            }
            else
            {
                ML.Result result = BL.Aseguradora.GetByIdEF(IdAseguradora.Value);

                if (result.Correct)

                {
                    aseguradora = ((ML.Aseguradora)result.Object);
                    aseguradora.Usuario = new ML.Usuario();
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects.ToList();


                    return View(aseguradora);

                }

            }
            return View(aseguradora);



        }



        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            IFormFile file = Request.Form.Files["Imagen"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                aseguradora.Imagen = Convert.ToBase64String(ImagenBytes);

            }
            ML.Result result = new ML.Result();

            //si trae el id va acutualizar 
            if (aseguradora.IdAseguradora != 0)
            {
                result = BL.Aseguradora.AseguradoraUpdateEF(aseguradora);

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
                result = BL.Aseguradora.AseguradoraAddEF(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado correctaente el producto";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.IdAseguradora = IdAseguradora;

            ML.Result result = BL.Aseguradora.AseguradoraDeleteEF(aseguradora);
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return PartialView("Modal");
            }

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
