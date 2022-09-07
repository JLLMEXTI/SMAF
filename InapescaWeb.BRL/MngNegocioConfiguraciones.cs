/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
    FileName:	MngNegocioConfiguracion.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Mayo 2016
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
using System.Data;

namespace InapescaWeb.BRL
{
  public   class MngNegocioConfiguraciones
    {
      public static string Obtiene_Dep_Configuracion(string psDep, string psConfiguracion, string psPeriodo = "")
      {
          return MngDatosConfiguraciones.Obtiene_Dep_Configuracion(psDep, psConfiguracion, psPeriodo);
      }


      public static List<Entidad> Lista_Configuraciones(string psDep, string psConfiguracion, string psPeriodo = "")
      {
          return MngDatosConfiguraciones.Lista_Configuraciones(psDep, psConfiguracion, psPeriodo);
      }
    }
}
