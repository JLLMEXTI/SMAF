/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngNegocioActividades.cs
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
   public  class MngNegocioPago
    {

       public static List<Entidad> ListaComisionesPagar(string psUbicacion, string psUbicacionProyecto= "", string psPeriodo = "", bool psPagador = false)
       {
           return MngDatosPagos.ListaComisionesPagar(psUbicacion,psUbicacionProyecto , psPeriodo, psPagador);
       }

       public static List<Pagos> Comisiones_Pagar(string psUbicacion,string psPeriodo = "")
       {
           return MngDatosPagos.Comisiones_Pagar(psUbicacion,psPeriodo );
       }
    }
}
