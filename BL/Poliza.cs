using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Poliza
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {

                    //var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                    var polizas = context.Polizas.FromSqlRaw($"PolizaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (polizas != null)
                    {
                        foreach (var obj in polizas)
                        {
                            ML.Poliza poliza = new ML.Poliza();

                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.Nombre;
                            poliza.NumeroPoliza= obj.NumeroPoliza;
                            poliza.FechaCreacion = obj.FechaCreacion.ToString();
                            poliza.FechaModificacion = obj.FechaModificacion.ToString();

                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.IdSubPoliza = obj.IdSubPoliza.Value;

                            poliza.Usuario = new ML.Usuario();
                            poliza.IdUsuario = obj.IdUsuario.Value; 


            



                            result.Objects.Add(poliza);
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
    }
}
