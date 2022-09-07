/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
    FileName:	MngNegocioCOmprobacion.cs
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
 using System.Data;

namespace InapescaWeb.BRL
{
    public class MngNegocioComprobacion
    {
        public static string Total_No_Fiscal(string psUsuario, string psDep, string psComprobante, string psPeriodo)
        {
            return MngDatosComprobacion.Total_No_Fiscal(psUsuario, psDep, psComprobante, psPeriodo);
        }

        public static List<GridView> ListaComprobantes(string psOficio, string psComisionado, string psArchivo, string psDep)
        {
            return MngDatosComprobacion.ListaComprobantes(psOficio, psComisionado, psArchivo, psDep);
        }

        public static Entidad Obtiene_Comprobaciones(string psPeriodo, string psUbicacion, string psOficio, string psComisionado, string psConcepto)
        {

            return MngDatosComprobacion.Obtiene_Comprobaciones(psPeriodo, psUbicacion, psOficio, psComisionado, psConcepto);
        }

        public static List<Entidad> Obtiene_No_Facturables(string psComisionado, string psOficio, string psTipo, bool pbandera = false)
        {
            return MngDatosComprobacion.Obtiene_No_Facturables(psComisionado, psOficio, psTipo, pbandera);
        }

        public static List<Entidad> ObtieneListaImportes(string PSUsuario, string psConcepto, string psOficio, string psArchivo, string psUbicacion, string psTipo)
        {
            return MngDatosComprobacion.ObtieneListaImportes(PSUsuario, psConcepto, psOficio, psArchivo, psUbicacion, psTipo);
        }


        public static string Sum_Totales(string psConcepto, string psPeriodo, string psDep)
        {
            return MngDatosComprobacion.Sum_Totales(psConcepto, psPeriodo, psDep);
        }
        public static string Sum_TotalesProy(string psConcepto, string psPeriodo, string psProy, string psDep)
        {
            return MngDatosComprobacion.Sum_TotalesPory(psConcepto, psPeriodo, psProy, psDep);
        }

        //METODO PARA AUDITORÍA Y REPORTES DE VIATICOS
        public static string Sum_Importe1(string psUsuario, string psUbicacion, string psConcepto, string psOficio, string psArchivo)
        {
            return MngDatosComprobacion.Sum_Importe1(psUsuario, psUbicacion, psConcepto, psOficio, psArchivo);
        }



        public static string Sum_Importe(string psUsuario, string psUbicacion, string psConcepto, string psOficio, string psArchivo)
        {
            return MngDatosComprobacion.Sum_Importe(psUsuario, psUbicacion, psConcepto, psOficio, psArchivo);
        }


        public static string Fecha_Max_Factura(string psUsuario, string psUbicacion, string psConcepto, string psOficio, string psArchivo, string psOpcion)
        {
            return MngDatosComprobacion.Fecha_Max_Factura(psUsuario, psUbicacion, psConcepto, psOficio, psArchivo, psOpcion);
        }
        public static List<Entidad> Obtiene_Lista_Conceptos()
        {
            return MngDatosComprobacion.Obtiene_Lista_Conceptos();
        }

        public static DataSet ListaComprobaciones(string psUbicacion, string psUsuario, string psfolio, string psOpcion)
        {
            return MngDatosComprobacion.ListaComprobacion(psUbicacion, psUsuario, psfolio, psOpcion);
        }
        public static string Exist_UUUID_Ministracion(string psUUID)
        {
            return MngDatosComprobacion.Exist_UUUID_Ministracion(psUUID);
        }

        public static string Exist_UUUID(string psUUID)
        {
            return MngDatosComprobacion.Exist_UUUID(psUUID);
        }

        public static string Totales(string psUbicacion, string psUsuario, string psfolio, string psOpcion, string psConcepto = "")
        {
            return MngDatosComprobacion.Totales(psUbicacion, psUsuario, psfolio, psOpcion, psConcepto);
        }

        public static Boolean Update_Comprobacion(string psUbicacion, string psUsuario, string psfolio, string psArchivo, string psEstatus, string psOpcion = "")
        {
            return MngDatosComprobacion.Update_Comprobacion(psUbicacion, psUsuario, psfolio, psArchivo, psEstatus, psOpcion);
        }



        public static string NOm_Reintegro(string psOficio, string psArchivo)
        {
            return MngDatosComprobacion.NOm_Reintegro(psOficio, psArchivo);
        }

        public static Entidad Obtiene_TotalPagado(string psPeriodo, string psclaveproy, string psUbicacionProy, string psEstatus)
        {
            return MngDatosComprobacion.Obtiene_TotalPagado(psPeriodo, psclaveproy, psUbicacionProy, psEstatus);
        }

        public static string Total_Comprobado(string psPeriodo, string psClvProyecto, string psTipo)
        {
            return MngDatosComprobacion.Total_Comprobado(psPeriodo, psClvProyecto, psTipo);
        }

        public static bool Update_MetodoPagoUsuario(string psUsuario, string psTimbreFiscal, string psArchivo,string psTicket)
        {
            return MngDatosComprobacion.Update_MetodoPagoUsuario(psUsuario, psTimbreFiscal, psArchivo, psTicket);
        }

        //********* PATRICIA QUINO MORENO 28-06-17******//
        public static List<Comisio_Comprobacion> Regresa_ListComprobacion(string psUbicacion, string psUsuario, string psArchivo, string psOpcion, bool pBandera = false)
        {
            return MngDatosComprobacion.Regresa_ListComprobacion(psUbicacion, psUsuario, psArchivo, psOpcion,pBandera);
        }
        //********* PATRICIA QUINO MORENO 28-06-17 TERMINA ******//
        //********* PATRICIA QUINO MORENO 12-07-17  ******//
        public static string Total_AnualNoFiscal(string pComisionado)
        {
            return MngDatosComprobacion.Total_AnualNoFiscal(pComisionado);
        }
        public static string Metodo_PagoComprobacion(string ID)
        {
            return MngDatosComprobacion.Metodo_PagoComprobacion(ID);
        }
        //********* PATRICIA QUINO MORENO 12-07-17 TERMINA ******//
        //MODIFICACION PEDRO 01-09-2017
        public static string Obtiene_Fecha_Comprobacion(string psFolio, string psArchivo)
        {
            return MngDatosComprobacion.Obtiene_Fecha_Comprobacion(psFolio, psArchivo);
        }
        public static Entidad Obtiene_FechaMax_Importe(string psArchivo, string psComisionado, string psConcepto)
        {
            return MngDatosComprobacion.Obtiene_FechaMax_Importe(psArchivo, psComisionado, psConcepto);
        }
        public static string Total(string psUsuario, string psDep, string psArchivo, string psComprobante)
        {
            return MngDatosComprobacion.Total(psUsuario,psDep,psArchivo,psComprobante);
        }
    }

}


