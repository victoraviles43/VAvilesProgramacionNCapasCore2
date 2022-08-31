using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        public ActionResult GetAll()
        {
            //llamo al get all de aseguradora
            ML.Result result = BL.Empresa.EmpresaGetAll();

            //creo una instancia de aseguradora
            ML.Empresa empresa = new ML.Empresa();

            //paso de mi lista result.objects a mi lista de aseguradoras
            empresa.Empresas = result.Objects;
            //aseguradora.Aseguradoras = result.Objects;
            return View(empresa);
            //return View();
        }
        [HttpGet]


        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
     
            if (IdEmpresa == null)
            {

                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.GetByIdEF(IdEmpresa.Value);

                if (result.Correct)

                {
                    empresa = ((ML.Empresa)result.Object);

                    return View(empresa);
                }

            }
            return View(empresa);

        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile file = Request.Form.Files["Imagen"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                empresa.Logo = Convert.ToBase64String(ImagenBytes);

            }
            ML.Result result = new ML.Result();

            //si trae el id va acutualizar 
            if (empresa.IdEmpresa != 0)
            {
                result = BL.Empresa.EmpresaUpdate(empresa);

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
                result = BL.Empresa.EmpresaAdd(empresa);
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

        }
        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            empresa.IdEmpresa = IdEmpresa;

            ML.Result result = BL.Empresa.Delete(empresa);
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
    }
}



