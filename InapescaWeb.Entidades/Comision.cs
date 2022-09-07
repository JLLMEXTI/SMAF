/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Entidades
	FileName:	Comision.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public   class Comision
    {
      private string lsFolio;
      private string lsFechaSol;
      private string lsfechaRespP;
      private string lsFechaVobo;
      private string lsFechaAut;
      private string lsUsuSol;
      private string lsUbicacion;
      private string lsArea;
      private string lsProyecto;
      private string lsDepProy;
      private string lsLugar;
      private string lsCapitulo;
      private string lsProceso;
      private string lsIndicador;
      private string lsPartPre;
      private string lsFechaI;
      private string lsFechaF;
      private string lsDiasT;
      private string lsDiasR;
      private string lsObjetivo;
      private string lsCLaseT;
      private string lsTipoT;
      private string lsVehiculoSol;
      private string lsClaseTransAut;
      private string lsTipoTransAut;
      private string lsVehiculoAut;
      private string lsdepTransAut;
      private string lsDescVehiculo;
      private string lsDepTrans;
      private string lsOrigenDest_Ter;
      private string lsVueloPartN;
      private string lsFechVueloPart;
      private string lsHoraVueloPart;
      private string lsVueloRetN;
      private string lsFechVueloRet;
      private string lsHoraVueloRet;
      private string lsOrigenDest_Aer;
      private string lsCombustSol;
      private string lsCombustAut;
      private string lsPartCombustible;
      private string lsClv_Viaticos;
      private string lsTotalViaticos;
      private string lsFormaPagoViaticos;
      private string lsEquipo;
      private string lsObservacionesSol;
      private string lsRespProyect;
      private string lsVobo;
      private string lsAutoriza;
      private string lsComisionado;
      private string lsDep_Com;
      private string lsObservVobo;
      private string lsObservAut;
      private string lsAerolinea_Part;
      private string lsAerolinea_Ret;
      private string lsTipo_Viaticos;
      private string lsOrigenDest_Acua;
      private string lsValeI;
      private string lsValeF;
      private string lsHoraInicio;
      private string lsHoraFinal;
     // private string lsOrigen_Destino_Terrestre;
     // private string lsOrigen_Destino_Aereo;
    //  private string lsOrigen_Destino_Acuatico;
      private string lsPagoCombustible;
      private string lsSecuencia;
      private string lsOficio;
      private string lsTipoPagoViatico;
      private string lsPeaje;
      private string lsPartidaPeaje;
      private string lsPasaje;
      private string lsPartidaPasaje;
      private string lsRuta;
      private string lsArchivo;
      private string lsTipoCom;
      private string lsPesqueria;
      private string lsProducto;
      private string lsActividad;
      private string lsEspecie;
      private string lsEstatus;
      private string lsTerritorio;
      private string lsDiasTotalPagar;
      private string lsdiasRural;
      private string lsdiasComercial;
      private string lsDiasNavegados;
      private string lsDias50km;
      private string lsCombustEfe;
      private string lsCombustVales;
      private string lsUbicacionAutoriza;
      private string lsSingladuras;
      private string lsPeriodo;

      public string Estado
      {
          get;
          set;
      }

      public string Pais
      {
          get;
          set;
      }

      public string Oficio_DG
      {
          get;
          set;
      }

      public string Oficio_DGAIPP
      {
          get;
          set;
      }

      public string Archivo_Ampliacion
      {
          get;
          set;
      }

      public string Oficio_Ampliacion
      {
          get;
          set;
      }

      public string Periodo
      {
          get { return lsPeriodo; }
          set { lsPeriodo = value; }
      }

      public string Singladuras
      {
          get { return lsSingladuras; }
          set { lsSingladuras = value; }
      }


      public string  Ubicacion_Autoriza
      {
          get { return lsUbicacionAutoriza; }
          set { lsUbicacionAutoriza = value; }
  }

      public string Combustible_Vales
      {
          get { return lsCombustVales; }
          set { lsCombustVales = value; }
      }

      public string Combustible_Efectivo
      {
          get { return lsCombustEfe; }
          set { lsCombustEfe = value; }
      }

      public string Dias_50
      {
          get { return lsDias50km; }
          set { lsDias50km = value; }
      }

      public string Dias_Navegados
      {
          get { return lsDiasNavegados; }
          set { lsDiasNavegados = value; }
      }


      public string Dias_Comercial
      {
          get { return lsdiasComercial; }
          set { lsdiasComercial = value; }
      }

      public string Dias_Rural
      {
          get { return lsdiasRural; }
          set { lsdiasRural = value; }
      }

      public string Dias_Pagar
      {
          get { return lsDiasTotalPagar; }
          set { lsDiasTotalPagar = value; }
      }


      public  string Territorio
      {
          get { return lsTerritorio; }
          set { lsTerritorio = value; }
      }

      public string Especie
      {
          get { return lsEspecie; }
          set { lsEspecie = value; }
      }

      public string Estatus
      {
          get { return lsEstatus; }
          set { lsEstatus = value; }
      }

      public string Tipo_Comision
      {
          get { return lsTipoCom; }
          set { lsTipoCom = value; }
      }

      public string Pesqueria
      {
          get { return lsPesqueria; }
          set { lsPesqueria = value; }
      }

      public string Producto
      {
          get { return lsProducto; }
          set { lsProducto = value; }
      }

      public string Actividad
      {
          get { return lsActividad; }
          set { lsActividad = value; }
      }

      public string Archivo
      {
          get { return lsArchivo; }
          set { lsArchivo = value; }
      }

      public string Ruta
      {
          get { return lsRuta; }
          set { lsRuta = value; }
      }

      public string Partida_Pasaje
      {
          get { return  lsPartidaPasaje; }
          set { lsPartidaPasaje = value; }
      }

      public string Pasaje
      {
          get { return lsPasaje; }
          set { lsPasaje = value; }
      }

      public string Partida_Peaje
      {
          get { return lsPartidaPeaje; }
          set { lsPartidaPeaje = value; }
      }

      public string Peaje
      {
          get { return lsPeaje; }
          set { lsPeaje = value; }
      }

      public string Tipo_Pago_Viatico
      {
          get { return lsTipoPagoViatico; }
          set { lsTipoPagoViatico = value; }
      }

      public string Oficio
      {
          get { return lsOficio; }
          set { lsOficio = value; }
      }

      public string Secuencia
      {
          get { return lsSecuencia; }
          set { lsSecuencia = value; }
      }

      public string Pago_Combustible
      {
          get { return lsPagoCombustible; }
          set { lsPagoCombustible = value; }
      }
      public string Folio
      {
          get { return lsFolio; }
          set { lsFolio = value; }
      }

      public string Fecha_Solicitud
      {
          get { return lsFechaSol; }
          set { lsFechaSol = value; }
      }

      public string Fecha_Responsable
      {
          get { return lsfechaRespP; }
          set { lsfechaRespP = value; }
      }

      public string Fecha_Vobo
      {
          get { return lsFechaVobo; }
          set { lsFechaVobo = value; }
      }

      public string Fecha_Autoriza
      {
          get { return lsFechaAut; }
          set { lsFechaAut = value; }
      }

      public string Usuario_Solicita
      {
          get { return lsUsuSol; }
          set { lsUsuSol = value; }
      }

      public string Ubicacion
      {
          get { return lsUbicacion; }
          set { lsUbicacion = value; }
      }

      public string Area
      {
          get { return lsArea; }
          set { lsArea = value; }
      }

      public string Proyecto
      {
          get { return lsProyecto; }
          set { lsProyecto = value; }
      }

      public string Dep_Proy
      {
          get { return lsDepProy; }
          set { lsDepProy = value; }
      }
      public string Lugar
      {
          get { return lsLugar; }
          set { lsLugar = value; }
      }

      public string Capitulo
      {
          get { return lsCapitulo; }
          set { lsCapitulo = value; }
      }

      public string Proceso
      {
          get { return lsProceso; }
          set { lsProceso = value; }
      }

      public string Indicador
      {
          get { return lsIndicador; }
          set { lsIndicador = value; }
      }
  
      public string Partida_Presupuestal
      {
          get { return lsPartPre; }
          set { lsPartPre = value; }
      }

      public string Fecha_Inicio
      {
          get { return lsFechaI; }
          set { lsFechaI = value; }
      }

      public string Fecha_Final
      {
          get { return lsFechaF; }
          set { lsFechaF = value; }
      }

      public string Dias_Total
      {
          get { return lsDiasT ; }
          set { lsDiasT = value; }
      }

      public string Dias_Reales
      {
          get { return lsDiasR; }
          set { lsDiasR = value; }
      }

      public string Objetivo
      {
          get { return lsObjetivo; }
          set { lsObjetivo = value; }
      }

      public string Clase_Trans
      {
          get { return lsCLaseT; }
          set { lsCLaseT = value; }
      }

      public string Tipo_Trans
      {
          get { return lsTipoT; }
          set { lsTipoT = value; }
      }

      public string Vehiculo_Solicitado
      {
          get { return lsVehiculoSol; }
          set { lsVehiculoSol = value; }
      }

      public string Clase_Aut
      {
          get { return lsClaseTransAut; }
          set { lsClaseTransAut = value; }
      }

      public string Tipo_Aut
      {
          get { return lsTipoTransAut; }
          set { lsTipoTransAut = value; }
      }

      public string Vehiculo_Autorizado
      {
          get { return lsVehiculoAut; }
          set { lsVehiculoAut = value; }
      }

      public string Ubicacion_Trans_Aut
      {
          get { return lsdepTransAut; }
          set { lsdepTransAut = value; }
      }

      public string Descrip_vehiculo
      {
          get { return lsDescVehiculo; }
          set { lsDescVehiculo = value; }
      }

      public string Ubicacion_Transporte
      {
          get { return lsDepTrans; }
          set { lsDepTrans = value; }
      }

      public string Origen_Terrestre
      {
          get { return lsOrigenDest_Ter; }
          set { lsOrigenDest_Ter = value;  }
      }

      public string Vuelo_Part_Num
      {
          get { return lsVueloPartN; }
          set { lsVueloPartN = value; }
      }

      public string Fecha_Vuelo_part
      {
          get { return lsFechVueloPart; }
          set { lsFechVueloPart = value; }
      }

      public string Hora_Vuelo_Part
      {
          get { return lsHoraVueloPart; }
          set { lsHoraVueloPart = value; }
      }

      public string Vuelo_Ret_Num
      {
          get { return lsVueloRetN; }
          set { lsVueloRetN = value; }
      }

      public string Fecha_Vuelo_Ret
      {
          get { return lsFechVueloRet; }
          set {lsFechVueloRet  = value; }
      }

      public string Hora_Vuelo_ret
      {
          get { return lsHoraVueloRet; }
          set { lsHoraVueloRet = value; }
      }

      public string Origen_Aereo
      {
          get { return lsOrigenDest_Aer; }
          set { lsOrigenDest_Aer = value; }
      }
      public string Combustible_Solicitado
      {
          get { return lsCombustSol; }
          set { lsCombustSol = value; }
      }


      public string Aerolinea_Part
      {
          get { return lsAerolinea_Part; }
          set { lsAerolinea_Part = value; }
      }
      public string Aerolinea_Ret
      {
          get { return lsAerolinea_Ret; }
          set { lsAerolinea_Ret = value; }
      }

      public string Tipo_Viaticos
      {
          get { return lsTipo_Viaticos; }
          set { lsTipo_Viaticos = value; }
      }

      public string Origen_Dest_Acu
      {
          get { return lsOrigenDest_Acua; }
          set { lsOrigenDest_Acua = value; }
      }

      public string Combustible_Autorizado
      {
          get { return lsCombustAut; }
          set { lsCombustAut = value; }
      }

      public string Partida_Combustible
      {
          get { return lsPartCombustible; }
          set { lsPartCombustible = value; }
      }

      public string Zona_Comercial
      {
          get { return lsClv_Viaticos; }
          set { lsClv_Viaticos = value; }
      }

      public string Total_Viaticos
      {
          get { return lsTotalViaticos; }
          set { lsTotalViaticos = value; }
      }

      public string Forma_Pago_Viaticos
      {
          get { return lsFormaPagoViaticos; }
          set { lsFormaPagoViaticos = value; }
      }

      public string Equipo
      {
          get { return lsEquipo; }
          set { lsEquipo = value; }
      }

      public string Observaciones_Solicitud
      {
          get { return lsObservacionesSol; }
          set { lsObservacionesSol = value; }
      }

      public string Responsable_proyecto
      {
          get { return lsRespProyect; }
          set { lsRespProyect = value; }
      }

      public string Vobo
      {
          get { return lsVobo; }
          set { lsVobo = value; }
      }

      public string Autoriza
      {
          get { return lsAutoriza; }
          set { lsAutoriza = value; }
      }

      public string Comisionado
      {
          get { return lsComisionado; }
          set { lsComisionado = value; }
      }

      public string Ubicacion_Comisionado
      {
          get { return lsDep_Com; }
          set { lsDep_Com = value; }
      }

      public string Observaciones_Vobo
      {
          get { return lsObservVobo; }
          set { lsObservVobo = value; }
      }

      public string Observaciones_Autoriza
      {
          get { return lsObservAut; }
          set { lsObservAut = value; }
      }

      public string Vale_Comb_I
      {
          get { return lsValeI; }
          set { lsValeI = value; }
      }

      public string Vale_Comb_F
      {
          get { return lsValeF; }
          set { lsValeF = value; }
      }

      public string Inicio_Comision
      {
          get { return lsHoraInicio; }
          set { lsHoraInicio = value; }
      }

      public string Fin_Comision
      {
          get { return lsHoraFinal; }
          set { lsHoraFinal = value; }
      }

      
      

  }
}
