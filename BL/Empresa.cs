using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result EmpresaGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {

                    //var aseguradoras = context.AseguradoraGetAll().ToList();
                    var empresas = context.Empresas.FromSqlRaw($"EmpresaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (empresas != null)
                    {
                        foreach (var obj in empresas)
                        {
                            ML.Empresa empresa = new ML.Empresa();


                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;




                            result.Objects.Add(empresa);
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

        //add
        public static ML.Result EmpresaAdd(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}',{empresa.Telefono},'{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");
                    // '{aseguradora.FechaCreacion}','{aseguradora.FechaModificacion}'


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
        //Update
        public static ML.Result EmpresaUpdate(ML.Empresa empresa) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa},'{empresa.Nombre}','{empresa.Telefono}','{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");

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
        //GETBYID

        public static ML.Result GetByIdEF(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    //var objDepartamento = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();
                    var objempresa = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();
                    //var objusuarios = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objempresa != null)
                    {

                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = objempresa.IdEmpresa;
                        empresa.Nombre = objempresa.Nombre;
                        empresa.Telefono = objempresa.Telefono;
                        empresa.Email = objempresa.Email;
                        empresa.DireccionWeb = objempresa.DireccionWeb;
                        empresa.Logo = objempresa.Logo;



                        result.Object = empresa;


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
        //DELETE
        public static ML.Result Delete(ML.Empresa empresa) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaDelete {empresa.IdEmpresa}");
                    //var updateResult = context.AseguradoraDelete(aseguradora.IdAseguradora);

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

    }
}
