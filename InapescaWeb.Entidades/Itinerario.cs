/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.Entidades
	FileName:	Itinerario.cs
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
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public  class Itinerario
    {
       private string lsFolio;
       private string lsOficio;
       private string lsDepsolicitud;
       private string lsProvedor;
       private string lsAerolinea;
       private string lsNumVuelo;
       private string lsFechaVuelo;
       private string lsHoraVuelo;
       private string lsComisionado;
       private string lsUbicacionCom;
       private string lsTramo;

       public  Itinerario()
       {
           lsFolio = "";
           lsDepsolicitud = "";
           lsOficio = "";
           lsProvedor = "";
           lsAerolinea = "";
           lsFechaVuelo = "";
           lsHoraVuelo = "";
           lsNumVuelo = "";
           lsComisionado = "";
           lsUbicacionCom = "";
           lsTramo = "";
       }

       public string Tramo
       {
           get { return lsTramo; }
           set { lsTramo = value; }
       }

       public string Ubicacion_Comisionado
       {
           get { return lsUbicacionCom; }
           set { lsUbicacionCom = value; }
       }

       public string Comisionado
       {
           get { return lsComisionado; }
           set { lsComisionado = value; }
       }

       public string Folio
       {
           get { return lsFolio; }
           set { lsFolio = value; }
       }

       public string Ubicacion
       {
           get { return lsDepsolicitud; }
           set { lsDepsolicitud = value; }
       }

       public string Oficio
       {
           get { return lsOficio; }
           set { lsOficio = value; }
       }

       public string Proovedor
       {
           get { return lsProvedor; }
           set { lsProvedor = value; }
       }

       public string Aerolinea
       {
           get { return lsAerolinea; }
           set { lsAerolinea = value; }
       }

       public string Numero
       {
           get { return lsNumVuelo; }
           set { lsNumVuelo = value; }
       }

       public string Fecha
       {
           get { return lsFechaVuelo; }
           set { lsFechaVuelo = value; }
       }

       public string Hora
       {
           get { return lsHoraVuelo; }
           set { lsHoraVuelo = value; }
       }

    }
}
