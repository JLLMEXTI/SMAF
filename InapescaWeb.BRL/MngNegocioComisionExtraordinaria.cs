/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		BRL
    FileName:	MngNegocioComisionExtaordinario.cs
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
 public     class MngNegocioComisionExtraordinaria
    {

     public static List<Comision_Extraordinaria> Obtiene_ComisionExtraordinaria(string psUsuario, string psFechaInicio, string psFechaFin,string psProyecto= "")
     {
         return MngDatosComisionExtraordinaria.Obtiene_ComisionExtraordinaria(psUsuario, psFechaInicio, psFechaFin ,psProyecto );
     }
    }
}
