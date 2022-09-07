using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using System.Data;


namespace InapescaWeb.BRL
{
 public    class MngNegocioReportesViaticos
    {

     public static List<Reporte_Viaticos > ListaComprobacion(string psPeriodo = "", string psInicio = "", string psFinal = "", string psFormaPago = "", string psEstatus = "", string psFinancieros = "", string psDep = "", string psUsuario = "")
     {
         return MngDatosReportes.ListaComprobacion(psPeriodo, psInicio, psFinal, psFormaPago, psEstatus, psFinancieros, psDep, psUsuario);
     }
    }
}
