using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Empleado
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            string file = @"C:\Users\digis\Documents\Aviles Alvarez Victor Uriel\LayOutEmpleado.txt";

            StreamReader archivo = new StreamReader(file);

            string line;
            bool isFirstLine = true;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();
            while ((line = archivo.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    line = archivo.ReadLine();
                }

                Console.WriteLine(line);
                string[] datos = line.Split('|');

                ML.Empleado empleado = new ML.Empleado();
                //dotos.Nombre = datos[2] == "" ? null : datos[2];
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


                result = BL.Empleado.Add(empleado);

                //if (!result.Correct) //si el resultado es diferente a correcto
                //{
                //    resultErrores.Objects.Add(
                //    "No se inserto el Nombre " + empleado.NumeroEmpleado +
                //            "No se inserto el RFC " + empleado.Rfc +
                //            "No se inserto el Nombre " + empleado.Nombre +
                //            "No se inserto el ApellidoPaterno " + empleado.ApellidoPaterno +
                //            "No se inserto el ApellidoMaterno " + empleado.ApellidoMaterno +
                //            "No se inserto el Email " + empleado.Email +
                //            "No se inserto el Telefono " + empleado.Telefono +
                //            "No se inserto el FechaNacimiento " + empleado.FechaNacimiento +
                //            "No se inserto el NSs " + empleado.Nss +
                //            "No se inserto el FechaIngreso " + empleado.FechaIngreso +
                //            "No se inserto el Foto " + empleado.Foto +
                //            "No se inserto el Idempresa " + empleado.IdEmpresa +
                //            "No se inserto el IDPoliza " + empleado.IdPoliza);

                //} //Se le asigna agrega la lista de errores
            }

            //if (resultErrores.Objects != null)
            //{

            //}

            //TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\Aviles Alvarez Victor Uriel\Errores.txt");
            //foreach (string error in resultErrores.Objects)
            //{
            //    tw.WriteLine(error); //Se le concatenan todos los errores
            //}
            //tw.Close();

            return result;
        }
    }
}
