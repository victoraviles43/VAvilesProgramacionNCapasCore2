using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLWebAPI.Controllers
{
   
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("api/usuario/GetAll")]
        public IActionResult GetAll()
        {   

            ML.Usuario usuario = new ML.Usuario();
            // materia.Semestre = new ML.Semestre();
            //usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            //usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            //usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            ML.Result result = BL.Usuario.UsuarioGetAllEF(usuario);
            //ML.Result result = BL.Materia.GetAll(materia);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        //Add
        //[Route("api/usuario")]
        //[HttpPost]
        //public IActionResult Add(ML.Usuario usuario)
        //{
        //    ML.Result result = BL.Usuario.AddEF(usuario);

        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound(result);
        //    }
        //}

        [HttpPost]
        [Route("api/usuario/add")]

        // POST api/subcategoria
        public IActionResult Post([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.AddEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    
        [HttpPost]
        [Route("api/usuario/update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            var result= BL.Usuario.UpdateEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

       
        [HttpDelete]
        [Route("api/usuario/delete")]
        public IActionResult Delete([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("api/usuario/GetById{IdUsuario}")]


        public IActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
