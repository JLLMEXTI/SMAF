/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngNegocioXml.cs
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
   public  class MngNegocioXml
    {
       public static Boolean Inserta_DetalleXML(string psUsuario, string psUbicacion, string psDocumento, string psXml, string psTimbreFiscal, string psProvedor, string psConcepto, string psPartida, string psFechaTimbrado, string psImporteUnid, string psIva, string psTua, string psisr, string psieps, string pssfp, string psIsh, string psIvaRet, string psTuaRet, string psIsrRet, string psIepsRet, string psSfpRet, string psIshRet, string psSubtotal, string pstotal, string psImpuestosRetenidos, string psimpuestostrasladados)
       {
           return MngDatosXml.Inserta_DetalleXML( psUsuario,  psUbicacion, psDocumento,  psXml, psTimbreFiscal, psProvedor, psConcepto, psPartida, psFechaTimbrado, psImporteUnid, psIva, psTua, psisr, psieps,  pssfp,  psIsh,  psIvaRet, psTuaRet, psIsrRet, psIepsRet, psSfpRet, psIshRet, psSubtotal,  pstotal,  psImpuestosRetenidos, psimpuestostrasladados);
       }
       public static string Obtiene_RegimenFiscal(string psRegimen)
       {
           return MngDatosCatalogos.Obtiene_RegimenFiscal(psRegimen);
       }
    }

 
}
