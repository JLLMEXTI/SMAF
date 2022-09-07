/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
	FileName:	MngNegociopef.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		diciembre 2015
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
   public class MngNegocioPef
   {

       public static Boolean Inserta_Pef_Detalle(Pef poPef, string psSec,string psUsuario,string psFile)
       {
           return MngDatosPef.Inserta_Pef_Detalle(poPef, psSec,psUsuario ,psFile );
       }

       public static Boolean Inserta_Pef(Pef poPef, string psSec)
       {
           return MngDatosPef.Inserta_Pef(poPef, psSec);
       }

       public static int Obtiene_pef()
       {
           return MngDatosPef.Obtiene_pef();
       }

       public static Pef Obtiene_Pef()
       {
           return MngDatosPef.Obtiene_Pef();
       }

       public static DataSet Lee_Excel_Pef(string psArchivo, string psHoja, string psExtencion, string psNombreArchivo)
       {
           return MngDatosPef.Lee_Excel_Pef(psArchivo, psHoja, psExtencion, psNombreArchivo);
       }
   }
}
