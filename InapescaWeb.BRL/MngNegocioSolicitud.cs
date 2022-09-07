/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Sustativo
	FileName:	ActualizaModuloConsulta.aspx
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Of. Centrales
	Date:		Marzo 2021
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/
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
    public class MngNegocioSolicitud
    {
        public static SolicitudMC Detalle_SolicitudMC(string psFolio, string psPeriodo = "")
        {
            return MngDatosSolicitud.Detalle_SolicitudMC(psFolio, psPeriodo);
        }
        public static List<Entidad> ObtieneListaEstatus(Boolean psFlag = false)
        {
            return MngDatosSolicitud.ObtieneListaEstatus(psFlag);
        }
        public static string ObtieneDGA(string psUbicacion)
        {
            return MngDatosSolicitud.ObtieneDGA(psUbicacion);
        }
        public static string ObtieneDocumento(string psFolio)
        {
            return MngDatosSolicitud.ObtieneDocumento(psFolio);
        }


        //public static Boolean Inserta_Documento(string psFolio, string psNameDoc, string psFechaActual)
        //{
        //    return MngDatosSolicitud.InsertarDocumento(psFolio, psNameDoc, psFechaActual);
        //}

        public static Boolean InsertNuevoRegSolicitud(SolicitudMC psDetSolicitud, string psEstatus, string psFecha, string psObs, string psUsuario, string psOfResp)
        {
            return MngDatosSolicitud.InsertNuevoRegSolicitud(psDetSolicitud, psEstatus, psFecha, psObs, psUsuario, psOfResp);
        }
        public static string Obtiene_Max(string psFolio)
        { 
            return MngDatosSolicitud.Obtiene_Max(psFolio);
        }

    }
}
