/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
	FileName:	MngNegocioComision.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		dicembre 2015
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
    public class MngNegocioComisionDetalle
    {
        public static bool UpdateEstatusFinancieros(string psEstatusFinancieros, string psArchivo, string psUsuario, string psPeriodo)
        {
            return MngDatosComisionDetalle.UpdateEstatusFinancieros(psEstatusFinancieros, psArchivo, psUsuario, psPeriodo);
        }

        public static bool UPdate_Monto_Pagado(string psMontoPagado, string psFechaPago, string psPartida, string psOpcion, string psArchivo, string psComisionado)
        {
            return MngDatosComisionDetalle.UPdate_Monto_Pagado(psMontoPagado, psFechaPago, psPartida, psOpcion, psArchivo, psComisionado);
        }

          public static List<ComisionComparativo> ListaFiltros1(string psPeriodo,  string psAdscripcion ,string psMes = "",string psUbicacionProyrecto ="",string psPrograma = "", string psUsuario = "", string psTipoViaticos = "", string psEstatus = "", string psFinancieros = "",bool pbIsDireccion = false)
        {
            return MngDatosComisionDetalle.ListaFiltros(psPeriodo, psAdscripcion, psMes, psUbicacionProyrecto,psPrograma, psUsuario, psTipoViaticos, psEstatus, psFinancieros, pbIsDireccion);
          }
    
        public static List<ComisionDetalle> ListaFiltros(string psTipo, string psEstatus, string psFinancieros, string psAdscripcion, string psUsuario, string psInicio, string psFin)
        {
            return MngDatosComisionDetalle.ListaFiltros(psTipo, psEstatus, psFinancieros, psAdscripcion, psUsuario, psInicio, psFin);
        }

        public static List<ComisionDetalle> ListaComisiones(string psProyecto, string psUbicacion, string psFinancieros = "", string psInicio= "", string psFin = "")
        {
            return MngDatosComisionDetalle.ListaComisiones(psProyecto, psUbicacion, psFinancieros, psInicio, psFin);
        }

        public static string Otorgado_por_Proyecto(string psProyecto, string psUbicacion, string psInicio, string psFin)
        {
            return MngDatosComisionDetalle.Otorgado_por_Proyecto(psProyecto ,psUbicacion ,psInicio ,psFin  );
        }

        public static ComisionDetalle Obtiene_detalle(string psarchivo, string psUsuario, string psDep, string psPeriodo)
        {
            return MngDatosComisionDetalle.Obtiene_detalle(psarchivo, psUsuario, psDep, psPeriodo);
        }
        public static Boolean Insertar_Detalle(string psNoOficio, string psArchivo, string psTipCom, string psFechIni, string psFechFin, string psProyecto, string psDepProy, string psComisionado, string psClvDepCom, string psFormaPagoViat, string psPeriodo, string psTerritorio, string psNameArchivoOf, string psViaticos = "", string psSingladuras = "", string psCombustible = "", string psPeaje = "", string psPasaje = "")
        {
            return MngDatosComisionDetalle.Insertar_Detalle(psNoOficio, psArchivo, psTipCom, psFechIni, psFechFin, psProyecto, psDepProy, psComisionado, psClvDepCom, psFormaPagoViat, psPeriodo, psTerritorio, psNameArchivoOf, psViaticos, psSingladuras, psCombustible, psPeaje, psPasaje);

        }
        public static comision_informe Obtiene_OficioFirmado(string psComisionado, string psUbiaccionCom, string psFolio, string psDepSolicitud, string psPeriodo)
        {
            return MngDatosComisionDetalle.Obtiene_OficioFirmado(psComisionado, psUbiaccionCom, psFolio, psDepSolicitud, psPeriodo);
        }
    }
}
