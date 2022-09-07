/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
    FileName:	MngNegocioEncriptacion.cs
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


namespace InapescaWeb.BRL
{
   public  class MngNegocioEncriptacion
    {
       public static string Encriptacion(string psCadena)
       {
           return MngEncriptacion.encryptString(psCadena);
       }

       public static string Decriptacion(string psCadena)
       {
           return MngEncriptacion.decripString(psCadena);
       }
    }
}
