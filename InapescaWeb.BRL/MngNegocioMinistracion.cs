/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
    FileName:	MngNegocioMinistracion.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Febrero 2016
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
   public class MngNegocioMinistracion
    {

       public static List<Entidad> Lista_tipo_Pagos()
       {
           return MngDatosMinistracion.Lista_tipo_Pagos();
       }

       public static Boolean Inserta_MInistracionDetalle(string psTipoMinistracion, string psFolio, string psFecha, string psPeriodo, string psUsuario, string psDep, string psFechaAut, string psUsuarioAut, string psFechaPago, string psUsuarioPagador, string psConcepto, string psTotalSolicitado, string psTotalPagado, string psEstatus, string psSecuencia, string psFechEff, string psClvPeriodo, string psBancoOrigen, string psCuentaOrigen, string psClabeOrigen, string psRefBan, string psBancoDestino, string psCuentaDestino, string psClabeDestino)
       {
           return MngDatosMinistracion.Inserta_MInistracionDetalle(psTipoMinistracion, psFolio, psFecha, psPeriodo, psUsuario, psDep, psFechaAut, psUsuarioAut, psFechaPago, psUsuarioPagador, psConcepto, psTotalSolicitado, psTotalPagado, psEstatus, psSecuencia, psFechEff, psClvPeriodo, psBancoOrigen, psCuentaOrigen, psClabeOrigen, psRefBan, psBancoDestino, psCuentaDestino, psClabeDestino);
       }

       public static Entidad PagadoVsPagar(string psArchivo, string psMinistracion)
       {
           return MngDatosMinistracion.PagadoVsPagar(psArchivo ,psMinistracion );
       }

       public static bool UpdateEstatusMinistracion(string psEstatus, string psArchivo, string psMinistracion, string psConcepto)
       {
           return MngDatosMinistracion.UpdateEstatusMinistracion(psEstatus, psArchivo, psMinistracion, psConcepto);
       }

       public static  bool Update_MInistracion(string psPartida,string psEstatus, string psPagado, string psFecha, string psUsuarioPagador, string psBancoOrigen, string psCuentaOrigen, string psClabeOrigen, string psBancoDestino, string psCuentaDestino, string psClabeDestino, string psRefBancaria, string psDocumento, string psMinistracion, string psConcepto, string psRazonSocial, bool psRFC = false)
       {
           return MngDatosMinistracion.Update_MInistracion(psPartida , psEstatus, psPagado, psFecha, psUsuarioPagador, psBancoOrigen, psCuentaOrigen, psClabeOrigen, psBancoDestino, psCuentaDestino, psClabeDestino, psRefBancaria, psDocumento, psMinistracion, psConcepto, psRazonSocial, psRFC);
       }

       public static List<Entidad> ListaMinistracionesSolicitadas(string psArchivo, string psMinistracion)
       {
           return MngDatosMinistracion.ListaMinistracionesSolicitadas(psArchivo, psMinistracion);
       }

       public static Ministracion Objeto_Ministracion(string psArchivo, string psMinistracion, string psConcepto, bool pbBandera = false )
       {
           return MngDatosMinistracion.Objeto_Ministracion_Completo(psArchivo, psMinistracion, psConcepto, pbBandera);
       }

       public static string ArchivoInMinistracion(string psArchivo)
       {
           return MngDatosMinistracion.ArchivoInMinistracion(psArchivo);
       }

       public static string Obtiene_ClvPeriodo(string psFecha, string psYear)
       {
           return MngDatosMinistracion.Obtiene_ClvPeriodo(psFecha, psYear);
       }


       public static List<Entidad> ListaTipoMinistracion(string psOpcion)
       {
           return MngDatosMinistracion.ListaTipoMinistracion(psOpcion);
       }

       public static string Obtiene_Max_Ministracion()
       {
           return MngDatosMinistracion.Obtiene_Max_Ministracion();
       }

       public static Boolean Inserta_Ministracion(string psTipoMinistracion, string psClvPeriodo, string psFolio, string psFecha, string psPeriodo, string psUsuario, string psFechaAut, string psAutoriza, string psFechaPago, string psUsuarioPagador, string psUbicacion, string psProyecto, string psUbicaProy, string psDocumento, string psArchivo, string psTipoPago, string psRefBanc, string psBanco, string psCuenta, string psClabe, string psRazon, string psRFC, string psOficio, string psFechaEmision, string psClvConcepto, string psConcepto, string psSubtotal, string psIVA, string psTUA, string psSFP, string psISR, string psTotalPagar, string psIvaRet, string psIsrRet, string psTuaRet, string psSfpRet, string psDescuentos, string psTotalPagado, string psFechaRecep, string psComponente, string psPartida, string psEstatus, string psSecuencia, string psObservaciones, string psBancoOrigen, string psCuentaOrigen, string psClabeOrigen)
       {
           return MngDatosMinistracion.Inserta_Ministracion( psTipoMinistracion,  psClvPeriodo,  psFolio,  psFecha,  psPeriodo,  psUsuario,  psFechaAut,  psAutoriza,  psFechaPago,  psUsuarioPagador,  psUbicacion,  psProyecto,  psUbicaProy,  psDocumento,  psArchivo,  psTipoPago,  psRefBanc,  psBanco,  psCuenta,  psClabe,  psRazon,  psRFC,  psOficio,  psFechaEmision,  psClvConcepto,  psConcepto,  psSubtotal,  psIVA,  psTUA,  psSFP,  psISR,  psTotalPagar,  psIvaRet,  psIsrRet,  psTuaRet,  psSfpRet,  psDescuentos,  psTotalPagado,  psFechaRecep,  psComponente,  psPartida,  psEstatus, psSecuencia,  psObservaciones,psBancoOrigen,  psCuentaOrigen, psClabeOrigen);
       }

       public static List<Entidad> Obtiene_Ministracion(string psPeriodo, string psArchivo)
       {
           return MngDatosMinistracion.Obtiene_Ministracion(psPeriodo, psArchivo);
       }

       

    }
}
