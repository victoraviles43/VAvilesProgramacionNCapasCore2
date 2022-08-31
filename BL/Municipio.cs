﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class Municipio
    {
        public static ML.Result MunicipioByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.VAvilesProgramacionNCapasContext context = new DL.VAvilesProgramacionNCapasContext())
                {

                    //var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                    var query = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado  {IdEstado}").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre;

                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = obj.IdMunicipio;

                            result.Objects.Add(municipio);
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
