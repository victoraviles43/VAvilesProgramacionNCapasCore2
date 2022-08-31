using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
            {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    //var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario}");
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}','{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.CURP}','{usuario.Imagen}', {usuario.Rol.IdRol},{1},'{usuario.Pasword}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");

                    
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        //UPDATE

        public static ML.Result UpdateEF(ML.Usuario usuario) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())

                {
                    //var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                    //    usuario.Email, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol);
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate  {usuario.IdUsuario},'{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}','{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.CURP}','{usuario.Imagen}', {usuario.Rol.IdRol},'{usuario.Estatus}','{usuario.Pasword}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        //DELETE
        public static ML.Result DeleteEF(ML.Usuario usuario) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {


                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())

                {
                    // var updateResult = context.UsuarioDelete(usuario.IdUsuario);
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        //GETALL

        public static ML.Result UsuarioGetAllEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {

                    // var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}' , '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString();
                            usuario.CURP = obj.Curp;
                            usuario.Imagen =obj.Imagen;


                            usuario.Pasword =obj.Pasword;   

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.RolNombre;

                            usuario.Estatus = obj.Estatus.Value;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.DireccionID;
                            usuario.Direccion.Calle = obj.DireccionCalle;
                            usuario.Direccion.NumeroInterior = obj.DireccionNumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.DireccionNumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.ColoniaId;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.ColoniaCodigo;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.municipioId;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre;
                      

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();   
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.EstadoId;    
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.paisId;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            result.Objects.Add(usuario);

                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    //var objDepartamento = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();
                    //var objusuarios = context..UsuarioGetById(IdUsuario).FirstOrDefault();
                    var objusuarios = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();


                    result.Objects = new List<object>();

                    if (objusuarios != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objusuarios.IdUsuario;
                        usuario.Nombre = objusuarios.Nombre;
                        usuario.ApellidoPaterno = objusuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = objusuarios.ApellidoMaterno;
                        usuario.Email = objusuarios.Email;
                        usuario.Sexo = objusuarios.Sexo;
                        usuario.Telefono = objusuarios.Telefono;
                        usuario.Celular = objusuarios.Celular;
                        usuario.FechaNacimiento = objusuarios.FechaNacimiento.ToString();
                        usuario.CURP = objusuarios.Curp;
                        usuario.Imagen = objusuarios.Imagen;

                        usuario.Pasword = objusuarios.Pasword;  

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objusuarios.IdRol.Value;

                        usuario.Rol.Nombre = objusuarios.RolNombre;
                    

                        usuario.Estatus = objusuarios.Estatus.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objusuarios.DireccionID;
                        usuario.Direccion.Calle = objusuarios.DireccionCalle;
                        usuario.Direccion.NumeroInterior = objusuarios.DireccionNumeroInterior;
                        usuario.Direccion.NumeroExterior = objusuarios.DireccionNumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objusuarios.ColoniaId;
                        usuario.Direccion.Colonia.Nombre = objusuarios.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = objusuarios.ColoniaCodigo;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objusuarios.municipioId;
                        usuario.Direccion.Colonia.Municipio.Nombre = objusuarios.MunicipioNombre;


                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objusuarios.EstadoId;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objusuarios.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objusuarios.paisId;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objusuarios.NombrePais;


                        result.Object = usuario;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Departamento";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetByIdEmail(string  Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    //var objDepartamento = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();
                    //var objusuarios = context..UsuarioGetById(IdUsuario).FirstOrDefault();
                    var objusuarios = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{Email}'").AsEnumerable().FirstOrDefault();


                    result.Objects = new List<object>();

                    if (objusuarios != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objusuarios.IdUsuario;
                        usuario.Nombre = objusuarios.Nombre;
                        usuario.ApellidoPaterno = objusuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = objusuarios.ApellidoMaterno;
                        usuario.Email = objusuarios.Email;
                        usuario.Sexo = objusuarios.Sexo;
                        usuario.Telefono = objusuarios.Telefono;
                        usuario.Celular = objusuarios.Celular;
                        usuario.FechaNacimiento = objusuarios.FechaNacimiento.ToString();
                        usuario.CURP = objusuarios.Curp;
                        usuario.Imagen = objusuarios.Imagen;

                        usuario.Pasword = objusuarios.Pasword;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objusuarios.IdRol.Value;

                        usuario.Rol.Nombre = objusuarios.RolNombre;


                        usuario.Estatus = objusuarios.Estatus.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objusuarios.DireccionID;
                        usuario.Direccion.Calle = objusuarios.DireccionCalle;
                        usuario.Direccion.NumeroInterior = objusuarios.DireccionNumeroInterior;
                        usuario.Direccion.NumeroExterior = objusuarios.DireccionNumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objusuarios.ColoniaId;
                        usuario.Direccion.Colonia.Nombre = objusuarios.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = objusuarios.ColoniaCodigo;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objusuarios.municipioId;
                        usuario.Direccion.Colonia.Municipio.Nombre = objusuarios.MunicipioNombre;


                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objusuarios.EstadoId;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objusuarios.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objusuarios.paisId;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objusuarios.NombrePais;


                        result.Object = usuario;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Departamento";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
