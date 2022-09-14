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
using System.Data;

namespace InapescaWeb.BRL
{
  public   class MngNegocioComision
    {
      //esta parte lo hizo patty
      public static ComisionProyecto RegresaDatos(string pPeriodo, string pClave, string pDependencia)
      {
          return MngDatosComision.RegresaDatos(pPeriodo, pClave, pDependencia);
      }
      // aqui termina


      public static List<Comision> Lista_ComisionesTransparencia(string psYear)
      {
          return MngDatosComision.Lista_ComisionesTransparencia(psYear);
      } 

      public static string Importe_Debito(string psUsuario, string psPeriodo, string psArchivo)
      {
          return MngDatosComision.Importe_Debito(psUsuario, psPeriodo, psArchivo);
      }
     
      public static TipoCambio TipoCambio(string psUsuario, string psFecha)
      {
          return MngDatosComision.TipoCambio(psUsuario, psFecha);
      }
      
      public static DataSet Filtros_Reintegros(string psPeriodo, string psInicio, string psFinal, string psEstatus, string psAdscripcion, string psUsuario)
      {
          return MngDatosComision.Filtros_Reintegros(psPeriodo, psInicio, psFinal, psEstatus, psAdscripcion, psUsuario);
      }

      public static List<Entidad> Lista_Centros_Comisiones(string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
      {
          return MngDatosComision.Lista_Centros_Comisiones(psPeriodo, psInicio, psFinal, psFormaPago, psEstatus, psAdscripcion, psUsuario);
      }
      public static List<Entidad> Lista_Proyectos_Comisiones(string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
      {
          return MngDatosComision.Lista_Proyectos_Comisiones(psPeriodo, psInicio, psFinal, psFormaPago, psEstatus, psAdscripcion, psUsuario);
      }
      public static DataSet Filtros1(string psTipoCom, string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
      {
          return MngDatosComision.Filtros1(psTipoCom , psPeriodo, psInicio, psFinal, psFormaPago, psEstatus, psAdscripcion, psUsuario);
      }

      public static  DataSet Filtros(string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
      {
          return MngDatosComision.Filtros(psPeriodo, psInicio, psFinal, psFormaPago, psEstatus, psAdscripcion, psUsuario);
      }
     
      public static Comision DetalleComision_Pagos(string psFolio, string psDep, string psComisionado = "",bool psUsuarioPagador = false )
      {
          return MngDatosComision.DetalleComision_Pagos(psFolio, psDep, psComisionado,psUsuarioPagador );
      }

      public static List<Comision> Comisiones_Efectivas(string psUbicacion , string psPeriodo)
      {
          return MngDatosComision.Comisiones_Efectivas(psUbicacion,psPeriodo );
      }

      public static string Obtiene_Dias_Continuos(string psFechas, string psComisionado, string psTerritorio)
      {
          return MngDatosComision.Obtiene_Dias_Continuos(psFechas, psComisionado, psTerritorio);
      }
      public static string Dias_Acumulados(string psComisionado,string  psPeriodo)
      {
          return MngDatosComision.Dias_Acumulados(psComisionado,psPeriodo );
      }

      public static bool Update_Estatus_Comprobacion(string psOficio, string psComisionado, string psUbicacion, string psFecha, string psImporte, string psDocumento,string psOpcion)
      {
          return MngDatosComision.Update_Estatus_Comprobacion(psOficio ,psComisionado,psUbicacion,psFecha ,psImporte ,psDocumento,psOpcion  );
      }

      public static List<Entidad> Lista_ComprobantesFiscales(string psOficio, string psComisionado, string psUbicacionComisionado, string psProyecto, string psUbicacionproyecto, string psarchivo)
      {
      return MngDatosComision.Lista_ComprobantesFiscales ( psOficio,  psComisionado,  psUbicacionComisionado,  psProyecto,  psUbicacionproyecto,  psarchivo);
      }

      public static Boolean Inserta_Comprobacion_Comision(string psOficio, string psClvOficio, string psComisionado, string psUbicacionComisionado, string psFechaFactura, string psProyecto, string psUbicacionProyecto, string psTipoComprobacion, string psClvConcepto, string psDescripcionConcepto, string psPdf, string psImporte, string psXml, string psMetPago, string psMetPagoUsser, string psObservaciones, string psDocumento, string psTicket, string psUUID, string psPeriodo, string psVersion="")
      {
          return MngDatosComision.Inserta_Comprobacion_Comision(psOficio, psClvOficio, psComisionado, psUbicacionComisionado, psFechaFactura, psProyecto, psUbicacionProyecto, psTipoComprobacion, psClvConcepto, psDescripcionConcepto, psPdf, psImporte, psXml,psMetPago,psMetPagoUsser, psObservaciones, psDocumento,psTicket , psUUID,psPeriodo, psVersion  );
      }

      public static string Obtiene_Folio_Comprobacion(string Oficio, string Archivo, string Comisionado)
      {
          return MngDatosComision.Obtiene_Folio_Comprobacion(Oficio, Archivo, Comisionado);
      }

      public static Boolean Insert_Folio_Comprobante(string psOficio, string psArchivo, string psComisionado)
      {
          return MngDatosComision.Insert_Folio_Comprobante(psOficio, psArchivo, psComisionado);
      }

      /// <summary>
      /// metodo que extrae lista de nombre de informes de comiison subidos al servidor
      /// </summary>
      /// <param name="psOficio"></param>
      /// <param name="psUbicacionSol"></param>
      /// <param name="psComisionado"></param>
      /// <param name="psUbicacionComisionado"></param>
      /// <param name="psProyect"></param>
      /// <param name="psUbicacionProy"></param>
      /// <returns></returns>
      public static List<Entidad> ObtieneNombresInformes(string psOficio, string psUbicacionSol, string psComisionado, string psUbicacionComisionado, string psProyect, string psUbicacionProy)
      {
          return MngDatosComision.ObtieneNombresInformes(psOficio, psUbicacionSol, psComisionado, psUbicacionComisionado, psProyect, psUbicacionProy);
      }

      /// <summary>
      /// Metodo que verifica si existe cruze de comisiones 
      /// </summary>
      /// <param name="psUsuario"></param>
      /// <param name="psFechaInicio"></param>
      /// <param name="psFechaFinal"></param>
      /// <param name="psOpcion"></param>
      /// <returns></returns>
      public static bool Comision_Extraordinaria(string psUsuario, string psFechaInicio, string psFechaFinal, string psOpcion)
      {
          return MngDatosComision.Comision_Extraordinaria(psUsuario, psFechaInicio, psFechaFinal, psOpcion);
      }

      /// <summary>
   /// Metodo que extrae de mngdatoscomision lista de tipos de comisiones
      /// </summary>
      /// <returns></returns>
      public static List<Entidad> Obtiene_tipo_Comision()
      {
          return MngDatosComision.Obtiene_Tipo_Comision();
      }

      /// <summary>
      /// Metodo que actualiza url y archivo en tabla comision una vez que se autoriza y genera oficios
      /// </summary>
      /// <param name="psRuta"></param>
      /// <param name="psArchivo"></param>
      /// <param name="psDep"></param>
      /// <param name="psComisionado"></param>
      /// <param name="psOficio"></param>
      /// <returns></returns>
      public static Boolean Update_ruta_Comision(string psRuta, string psArchivo, string psDep, string psComisionado, string psOficio, Comision poComision)
      {
          return MngDatosComision.Update_ruta_Comision (psRuta, psArchivo, psDep, psComisionado, psOficio,poComision );
      }

      /// <summary>
      /// metodo que retorna detalle comision
      /// </summary>
      /// <param name="psUsuario"></param>
      /// <param name="psUbiaccion"></param>
      /// <param name="psFolio"></param>
      /// <returns></returns>
      public static comision_informe Obtiene_Informe(string psUsuario, string psUbiaccion, string psFolio, string psDepSolicitud, string psPeriodo, string psProy = "")
      {
          return MngDatosComision.Obtiene_Informe(psUsuario, psUbiaccion, psFolio, psDepSolicitud, psPeriodo, psProy);
      }
      
      public static comision_informe Obtiene_Oficio(string psUsuario, string psUbiaccion, string psFolio, string psDepSolicitud, string psPeriodo)
      {
          return MngDatosComision.Obtiene_Oficio(psUsuario, psUbiaccion, psFolio, psDepSolicitud, psPeriodo);
      }
      
      /// <summary>
      /// Metodo que retorna de DAL lista de Tipos de comprobaciones de comision 
      /// </summary>
      /// <returns></returns>
      public static List<Entidad> ObtieneTipoComprobacion(bool pbBandera = false)
      {
          return MngDatosComision.ObtieneTipoComprobacion( pbBandera);
      }

      public static string Obtiene_Importe_total(string psUsuario, string psDep, string psFolio)
      {
          return MngDatosComision.Obtiene_Importe_total(psUsuario, psDep, psFolio);
      }

      public static string Obtiene_Max_Comision_Comprobar(string psComisionado )
      {
          return MngDatosComision.Obtiene_Max_Comision_Comprobar(psComisionado );
      }

      /// <summary>
      /// Obtiene todas las  Comisiones Comprobadas o en proceso de 
      /// </summary>
      /// <param name="psUsuario"></param>
      /// <param name="psDep"></param>
      /// <returns></returns>
      public static List<comprobacion  > Comision_Comp(string psUsuario, string psDep)
      {
          return MngDatosComision.Comision_Comp(psUsuario, psDep);
      }

      public static Boolean Update_Estatus_Informe(string psOficio, string psUbicacionSol, string psComisionado, string psUbicacionComisionado, string psProyect, string psUbicacionProy, string psSecuencia, int piopcion)
      {
          return MngDatosComision.Update_Estatus_Informe(psOficio, psUbicacionSol, psComisionado, psUbicacionComisionado, psProyect, psUbicacionProy,psSecuencia , piopcion );
      }
      public static Boolean Update_Oficio_Comision_DGAIPP(Comision poComision, int psNoMinDGAIPP) 
      {
          return MngDatosComision.Update_Oficio_Comision_DGAIPP(poComision,psNoMinDGAIPP);
      }
      /// <summary>
        /// Obtiene Comisiones Comprobadas o en proceso de 
      /// </summary>
      /// <param name="psUsuario"></param>
      /// <param name="psDep"></param>
      /// <returns></returns>
      public static List<Entidad> Comision_Comporbada(string psUsuario, string psDep, int tipo)
      {
          return MngDatosComision.Comision_Comprobada(psUsuario ,psDep , tipo );
      }

      public static DataSet Comisiones(string psUsuario, string psDep,string psPeriodo, string psEstatus="")
      {
          return MngDatosComision.Comisiones(psUsuario, psDep,psPeriodo , psEstatus);
      }

      public static List<Comision> ListaComisionesPagar(string psArchivo, string psPeriodo)
      {
          return MngDatosComision.ListaComisionesPagar(psArchivo, psPeriodo);
      }

      public static List<GridView  > Comisiones_Recurso(string psUsuario, string psDep,string psEstatus = "")
      {
          return MngDatosComision.Comisiones_Recurso(psUsuario, psDep,psEstatus );
      }

      public static string Obtiene_Lugar(string psFolio, string psUsuario, string psAccion)
      {
          return MngDatosComision.Obtiene_Lugar(psFolio, psUsuario, psAccion);
      }

      public static string Obtiene_SecEff_Comision(string psFolio)
      {
          return MngDatosComision.Otiene_SecEff_Comision(psFolio);
      }

      public static string ObtieneMaxComision(string psUbicacion,string psPeriodo)
      {
          return MngDatosComision.Obtiene_Max_Comision(psUbicacion,psPeriodo );
      }

      public static Entidad Vobo_Aut(string psFolio, string psVobo = "", string psAut = "")
      {
          return MngDatosComision.vobo_aut(psFolio, psVobo, psAut); 
      }

      public static Boolean Insert_Detalle_Comision(Comision poComision)
      {
          return MngDatosComision.Insert_Detalle_Comision(poComision );
      }

      public static Boolean Inserta_Comision(string psClv_Oficio,string psTipoComision, string psFolio, string psNo_Oficio, string psFecha_Sol, string psFecharesp, string psFechaVoBo, string psFechaAut, string psUsuarioSol, string psDep, string psArea, string psProy, string psDep_Proy, string psLugar, string psTelefono, string psCapitulo, string psProceso, string psIndicador, string psPart_Presupuestal, string psFechaI, string psFechaF, string psDiasTotal, string psDiasReales, string psObjetivo, string psClaseT, string psTipoT, string psTrans_Sol, string psTransAut, string psDescAuto, string psDepTrans, string psRecorridoTerrestre, string psVueloPartNum, string psFechVueloPart, string psHorVueloPart, string psVueloRetNum, string psFechVueloRet, string psHorVueloRet, string psRecorridoAereo, string psComSol, string psComAut, string psPartCom, string psPeaje, string psPartPeaje, string psPasaje, string psPartPasaje, string psRecorridoAcuatico, string psSecEff, string psEquipo, string psObservSol, string psRespProy, string psVoBo, string psAut, string psComisionado, string psDep_Com, string psEstatus, string psObservVoBo, string psObservAut, string psEspecies, string psProductos, string psActividades,string psTerritorio,string psUbicacion_Aut,string psPeriodo,string psPais, string psEstado)
      {
          return MngDatosComision.Insert_Comision(psClv_Oficio, psTipoComision, psFolio, psNo_Oficio, psFecha_Sol, psFecharesp, psFechaVoBo, psFechaAut, psUsuarioSol, psDep, psArea, psProy, psDep_Proy, psLugar, psTelefono, psCapitulo, psProceso, psIndicador, psPart_Presupuestal, psFechaI, psFechaF, psDiasTotal, psDiasReales, psObjetivo, psClaseT, psTipoT, psTrans_Sol, psTransAut, psDescAuto, psDepTrans, psRecorridoTerrestre, psVueloPartNum, psFechVueloPart, psHorVueloPart, psVueloRetNum, psFechVueloRet, psHorVueloRet, psRecorridoAereo, psComSol, psComAut, psPartCom, psPeaje, psPartPeaje, psPasaje, psPartPasaje, psRecorridoAcuatico, psSecEff, psEquipo, psObservSol, psRespProy, psVoBo, psAut, psComisionado, psDep_Com, psEstatus, psObservVoBo, psObservAut, psEspecies, psProductos, psActividades, psTerritorio, psUbicacion_Aut,psPeriodo,psPais ,psEstado  );
      }

      public static List <Entidad > Obtiene_Actions(string psRol)
      {
          return MngDatosComision.Action(psRol );
      }

      public static List<Entidad> Obtiene_Solicitudes(string psAction, string psUsuario,string psEstatus)
      {
          return MngDatosComision.Obtiene_Comision(psAction, psUsuario, psEstatus);
      }

      public static bool Update_Status_ComisionDetalle(string psEstatus, string psFolio, string psArchivo, string psComisionado, string psUbicacion)
      {
          return MngDatosComision.Update_Status_ComisionDetalle(psEstatus, psFolio, psArchivo, psComisionado, psUbicacion);
      }

      public static bool Update_estatus_Comision(string psEstatus, string psUsuario = "", string psFolio = "", string psDep = "", string psArchivo = "", string psObservaciones = "", bool x = false)
      {
          return MngDatosComision.Update_estatus_Comision(psEstatus, psUsuario, psFolio, psDep, psArchivo, psObservaciones,x);
      }

      public static Comision Obten_Detalle(string psFolio, string psDep, string psComisionado,string psEstatus)
      {
          return MngDatosComision.Obten_Detalle_Comision(psFolio, psDep, psComisionado,psEstatus );
      }

      public static Comision Detalle_Comision_Reimpresion(string psFolio, string psDep, string psPeriodo, string psComisionado = "", string psEstatus = "")
      {
          return MngDatosComision.Detalle_Comision_Reimpresion(psFolio, psDep,psPeriodo , psComisionado, psEstatus);
      }

      public static Comision PagoComision(string psArchivo, string psPeriodo, bool psBandera = false)
      {
          return MngDatosComision.PagoComision(psArchivo, psPeriodo, psBandera);
      }

      public static Comision Detalle_Comision(string psFolio, string psDep, string psComisionado = "",string psEstatus="")
      {
          return MngDatosComision.Detalle_Comision(psFolio, psDep , psComisionado,psEstatus  );
      }

      public static List<Entidades.GridView> ComisionadosS(string psFolio, string psOpcion, string psUsuario, string psEstatus, string psTipoCom = "", string psDep = "")
      {
          return MngDatosComision.ComisionadosS(psFolio, psOpcion, psUsuario, psEstatus, psTipoCom, psDep);
      }
      public static List<Entidad> Comisionados(string psFolio, string psOpcion,string psUsuario,string psEstatus,string psTipoCom="",string psDep="" )
      {
         return MngDatosComision.Comisionados(psFolio,psOpcion ,psUsuario, psEstatus,psTipoCom , psDep);
      }

      public static bool Update_Status_Comision_Detalle(string psFolio, string psUbicacion, string psComisionado = "", string psOpcion = "", string psComentarios = "", string psNoOficio = "", string psAutoriza = "")
      {
          return MngDatosComision.Update_Status_Comision_Detalle(psFolio, psUbicacion, psComisionado, psOpcion, psComentarios, psNoOficio ,psAutoriza );
      }

      public static bool Update_Sec_Eff(string psFolio, string psUbicacion, string psComisionado, string psSecEff)
      {
          return MngDatosComision.Update_Sec_Eff(psFolio, psUbicacion, psComisionado, psSecEff);
      }

      public static bool Update_status(string psFolio, string psUbicacion, string psComisionado = "", string psOpcion = "", string psComentarios = "", string psFecha = "")
      {
          return MngDatosComision.Update_Status(psFolio,  psUbicacion,psComisionado, psOpcion, psComentarios , psFecha );
      }

      public static bool Update_Comision(Comision pObjeto, string psOpcion, string psFolio, string psUsuario , string psPermisos,Boolean update = false )
      {
          return MngDatosComision.Autorizaciones(pObjeto, psOpcion, psFolio,psUsuario ,psPermisos,update  );
      }

      public static Boolean Update_Oficio_Comision(Comision poComision,Entidad poEntidad)
      {
          return MngDatosComision.Update_Oficio_Comision(poComision, poEntidad);
      }

      public static string Obtiene_Tarifa(string psZona)
      {
          return MngDatosComision.Obtiene_Tarifas(psZona);
      }

      public static Boolean Inserta_Informe_Comision(string psOficio, string psUbicacionSol, string pscComisionado, string psUbicacionComisionado, string psProyecto, string psUbicacionProyect, string psSecuencia, string psInforme, string psEvidencia1, string psEvidencia2, string psEvidencia3, string psDateI, string psDateF, string psPeriodo, string psDepAut = "")
      {
        return   MngDatosComision.Inserta_Informe_Comision(psOficio, psUbicacionSol, pscComisionado, psUbicacionComisionado, psProyecto, psUbicacionProyect, psSecuencia, psInforme, psEvidencia1, psEvidencia2, psEvidencia3, psDateI, psDateF,psPeriodo, psDepAut );
      }

      public static Boolean Inserta_Oficio_Comision(Comision poComision,string psOficio)
      {
          return MngDatosComision.Inserta_Oficio_Comision(poComision, psOficio);
      }
      public static List<Entidad> Obtiene_Detalles_ComisionUsuarioSolicitado(string pArchivo, string pPeriodo, string pUsuario)
      {
          return MngDatosComision.Obtiene_Detalles_ComisionUsuarioSolicitado(pArchivo, pPeriodo, pUsuario);
      }

      //Modificacion Pedro
      public static List<Comision> Regresa_ListComision(string psUsuario, string psPeriodo, string psEstatus = "")
      {
          return MngDatosComision.Regresa_ListComision(psUsuario,psPeriodo,psEstatus);
      }
      public static Entidad Obten_DetalleArchivo(string psUsuario, string psPeriodo, string psArchivo)
      {
          return MngDatosComision.Obten_DetalleArchivo(psUsuario,psPeriodo,psArchivo);
      }
      public static string Comisiones_SinComprobar(string psUsuario, string psPeriodo)
      {
          return MngDatosComision.Comisiones_SinComprobar(psUsuario,psPeriodo);
      }
      public static string Solicitudes_Registradas(string psUsuario, string psFecha)
      {
          return MngDatosComision.Solicitudes_Registradas(psUsuario, psFecha);
      }
      public static string Obtiene_PeriodoComision(string psArchivo)
      {
          return MngDatosComision.Obtiene_PeriodoComision(psArchivo);
      }
      public static Comision Detalle_Comision_Reimpresion1(string psFolio, string psDep, string psPeriodo)
      {
          return MngDatosComision.Detalle_Comision_Reimpresion1(psFolio,psDep,psPeriodo);
      }
    }
}
