using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.Rfc}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.Nss}','{empleado.Foto}',{empleado.Empresa.IdEmpresa},{empleado.Poliza.IdPoliza}");
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
        //UPDATE
        public static ML.Result Update(ML.Empleado empleado) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.Rfc}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.Nss},'{empleado.Foto}',{empleado.Empresa.IdEmpresa},{empleado.Poliza.IdPoliza}");

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
        public static ML.Result Delete(ML.Empleado empleado) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.NumeroEmpleado}'");
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
        //GETALL

        public static ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {

                    //var aseguradoras = context.AseguradoraGetAll().ToList();
                    var empleados = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleado.Nombre}','{empleado.ApellidoPaterno}',{empleado.Empresa.IdEmpresa}").ToList();

                    result.Objects = new List<object>();

                    if (empleados != null)
                    {
                        foreach (var obj in empleados)
                        {
                           
                            empleado = new ML.Empleado();


                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.Rfc = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString();
                            empleado.Nss = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.ToString();
                            empleado.Foto = obj.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.EmpresaNombre;


                            empleado.Poliza = new ML.Poliza();
                            empleado.Poliza.IdPoliza = obj.IdPoliza.Value;
                            empleado.Poliza.Nombre = obj.PolizaNombre;




                            result.Objects.Add(empleado);
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
        //GETBYID

        public static ML.Result GetByIdEF(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {
                    //var objDepartamento = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();
                    var objempleado = context.Empleados.FromSqlRaw($"EmpleadoGetById {NumeroEmpleado}").AsEnumerable().FirstOrDefault();
                    //var objusuarios = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objempleado != null)
                    {

                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumeroEmpleado = objempleado.NumeroEmpleado;
                        empleado.Rfc = objempleado.Rfc;
                        empleado.Nombre = objempleado.Nombre;
                        empleado.ApellidoPaterno = objempleado.ApellidoPaterno;
                        empleado.ApellidoMaterno = objempleado.ApellidoMaterno;
                        empleado.Email = objempleado.Email;
                        empleado.Telefono = objempleado.Telefono;
                        empleado.FechaNacimiento = objempleado.FechaNacimiento.ToString();
                        empleado.Nss = objempleado.Nss;
                        empleado.FechaIngreso = objempleado.FechaIngreso.ToString();
                        empleado.Foto = objempleado.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = objempleado.IdEmpresa.Value;
                        empleado.Empresa.Nombre = objempleado.EmpresaNombre;
                        empleado.Poliza = new ML.Poliza();
                        empleado.Poliza.IdPoliza = objempleado.IdPoliza.Value;
                        empleado.Poliza.Nombre = objempleado.PolizaNombre;




                        result.Object = empleado;


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
        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Hoja1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;
                        //Recuperar la informacion del exel y llenarlo en una tabla  
                        DataTable tableEmpleado = new DataTable();

                        da.Fill(tableEmpleado);
                        //Validando que tenga filas
                        if (tableEmpleado.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            //Recuperar las filas de las tablas 
                            foreach (DataRow row in tableEmpleado.Rows)
                            {
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.NumeroEmpleado = row[0].ToString();
                                empleado.Rfc= row[1].ToString();
                                empleado.Nombre = row[2].ToString();

                                empleado.ApellidoPaterno = row[3].ToString();
                                empleado.ApellidoMaterno = row[4].ToString();
                                empleado.Email = row[5].ToString();

                                empleado.Telefono = row[6].ToString();
                                empleado.FechaNacimiento = row[7].ToString();
                                empleado.Nss = row[8].ToString();

                                //empleado.FechaIngreso = row[9].ToString();
                                empleado.Foto = null;
                                empleado.Empresa = new ML.Empresa();
                                empleado.Empresa.IdEmpresa = int.Parse(row[10].ToString());
                                empleado.Poliza = new ML.Poliza();
                                empleado.Poliza.IdPoliza = int.Parse(row[11].ToString());
                                
                               

                                result.Objects.Add(empleado);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableEmpleado;

                        if (tableEmpleado.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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
        //Recibe una lista de objetos 
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                //Extrae informacion , de empleado 
                foreach (ML.Empleado empleado in Object)
                {

                    //Validacion de lo que esta correcto 
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (empleado.NumeroEmpleado == "")
                    {
                        error.Mensaje += "Ingresar el Numero de empleado  ";
                    }
                    if (empleado.Rfc == "")
                    {
                        error.Mensaje += "Ingresar RFC ";
                    }
                    if (empleado.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el Nombre ";
                    }
                    if (empleado.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el ApellidoPaterno ";
                    }
                    if (empleado.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el ApellidoMaterno ";
                    }
                    if (empleado.Email == "")
                    {
                        error.Mensaje += "Ingresar el Email ";
                    }
                    if (empleado.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el Telefono ";
                    }
                    
                    if (empleado.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar FechaNacimiento ";
                    }
                    if (empleado.Nss == "")
                    {
                        error.Mensaje += "Ingresar el NSS ";
                    }
                    //if (empleado.FechaIngreso == "")
                    //{
                    //    error.Mensaje += "Ingresar Fecha  ";
                    //}

                    if (empleado.Foto == "")
                    {
                        error.Mensaje += "Ingresar la Foto ";
                    }
                   
                    if (empleado.Empresa.IdEmpresa.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdEmpresa ";
                    }
                    if (empleado.Poliza.IdPoliza.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdStatus ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
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