/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.BRL
	FileName:	MngNegocioComision.cs
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Oficinas Centrales 
	Date:		Abril 2020
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
    public class MngNegocioProyeccionViaticos
    {
        public static string Dias_Acumulados(string psComisionado, string psPeriodo)
        {
            return MngDatosProyeccionViaticos.Dias_Acumulados(psComisionado, psPeriodo);
        }
        public static string Dias_AcumuladosMes(string psComisionado, string psPeriodo, string psMes)
        {
            return MngDatosProyeccionViaticos.Dias_AcumuladosMes(psComisionado, psPeriodo, psMes);
        }
        public static Boolean Insert_Proyecion_Viaticos(string psComisionado, string psProyecto, string psDepProy, string psUbicacion, double psCantDiasViat, string psRol, int psZona, double psTotalViat, string psUsserRegistro, string psMes)
        {
            return MngDatosProyeccionViaticos.Insert_Proyecion_Viaticos(psComisionado, psProyecto, psDepProy, psUbicacion, psCantDiasViat, psRol, psZona,psTotalViat,psUsserRegistro, psMes);
        }
        public static string Obtener_Existen_Registro(string psComisionado, string psProyecto, string psDepProy, string psDepUbic, string psMes, string psZona)
        {
            return MngDatosProyeccionViaticos.Obtener_Existen_Registro(psComisionado, psProyecto, psDepProy, psDepUbic, psMes, psZona);
        }
        public static List<Entidad> Obtener_Lista_Meses(string psMesActual)
        {
            return MngDatosProyeccionViaticos.Obtener_Lista_Meses(psMesActual);
        }
        public static DataSet ObtieneProyeccionViat(string psPeriodo, string psAdscripcion, string psUsuario)
        {
            return MngDatosProyeccionViaticos.ObtieneProyeccionViat(psPeriodo, psAdscripcion, psUsuario);
        }
        public static string ObtieneDescrMes(string psMes)
        {
            return MngDatosProyeccionViaticos.ObtieneDescrMes(psMes);
        }
    }
}
