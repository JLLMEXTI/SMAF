/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngNegocioActividades.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Noviembre 2015
    Description: Clase quefunciona de puente con la clase de datos productos en el DAL
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
 * */
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
    public class MngNegocioActividades
    {

        public static string Obtiene_Descripcion_Actividades(string psActividad)
        {
            return MngDatosActividades.Obtiene_Descripcion_Actividades(psActividad);
        }


        /// <summary>
        /// Metodo que devuelve lista de actividades obtenidas de la capa DAL
        /// </summary>
        /// <returns></returns>
        public static List<Entidad> Obtiene_Actividades(string psComponente = "", string psDireccion = "")
        {
            return MngDatosActividades.Obtiene_Actividades(psComponente,psDireccion);
        }

    }
}
