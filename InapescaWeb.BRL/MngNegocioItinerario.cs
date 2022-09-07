/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
    FileName:	MngNegocioItinerario.cs
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
   public  class MngNegocioItinerario
    {

       public static Boolean Update_StatusGral_Comisionado_Itinerario(string psFolio, string psUbicacion, string pscomisionado)
       {
           return MngDatosItinerario.Update_StatusGral_Comisionado_Itinerario(psFolio, psUbicacion, pscomisionado);
       }

       public static Boolean Update_Estatus_Itinerario(string psFolio, string psUbicacion, string psUsuario, string psTipoCom, string psAerolinea, string psNumVuelo, string psFechaVuelo, string psHoraVuelo, string psTramo)
       {
           return MngDatosItinerario.Update_Estatus_Itinerario(psFolio ,psUbicacion ,psUsuario ,psTipoCom ,psAerolinea ,psNumVuelo,psFechaVuelo,psHoraVuelo,psTramo );
       }

       /// <summary>
       /// Metodo que devuelve Lista de Itinerario de Vuelos segun solicitud a consultar obtenidas de la capa dal
       /// </summary>
       /// <param name="psFolio"></param>
       /// <param name="psUbicacion"></param>
       /// <returns></returns>
       public static List<Itinerario> Obten_Itinerario_Solicitud(string psFolio, string psUbicacion)
       {
           return MngDatosItinerario.Obten_Lista_Itinerarios(psFolio, psUbicacion);
       }
       /// <summary>
       /// Metodo que llama a la insercion de itineracio del dal
       /// </summary>
       /// <param name="psFolio"></param>
       /// <param name="psUbicacion"></param>
       /// <param name="psProovedor"></param>
       /// <param name="psAerolinea"></param>
       /// <param name="psNumVuelo"></param>
       /// <param name="psFechaVuelo"></param>
       /// <param name="psHoraVuelo"></param>
       /// <param name="psComisionad"></param>
       /// <param name="psTramo"></param>
       /// <returns></returns>
       public static Boolean Inserta_Itinerario(string psFolio, string psUbicacion, string psProovedor, string psAerolinea, string psNumVuelo, string psFechaVuelo, string psHoraVuelo, string psComisionad,string psTramo)
       {
           return MngDatosItinerario.Inserta_Itinerario(psFolio, psUbicacion, psProovedor, psAerolinea, psNumVuelo, psFechaVuelo, psHoraVuelo, psComisionad,psTramo );
       }

    }
}
