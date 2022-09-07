using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;

namespace InapescaWeb.BRL
{
   public  class MngNegocioViaticos
    {
       public static List<Entidad> Obtiene_Zonas()
       {
           return MngDatosViaticos.Obtiene_Zonas();
       }

       public static List<Entidad> Metodo_Viaticos(bool pbBandera = false )
       {
           return MngDatosViaticos.Metodo_Viaticos(pbBandera );
       }

       public static string Descrip_Zona(string psClvZona)
       {
           return MngDatosViaticos.Descrip_Zona(psClvZona);
       }

       public static string Descrip_Metodo_Viaticos(string psClvPagos)
       {
           return MngDatosViaticos.Descrip_Metodo_Viaticos(psClvPagos);
       }

       //******************************PATRICIA QUINO MORENO*************************************//

       public static List<Entidad> TraerDatosCrip(string psTipo, string psEstatus, string psPeriodo)
       {
           return MngDatosViaticos.TraerDatosCrip(psTipo, psEstatus, psPeriodo);
       }

       public static string Total_Concepto(string psPeriodo, string psClvProyecto, string psEstatus, string psTipo)
       {
           return MngDatosViaticos.Total_Concepto(psPeriodo, psClvProyecto, psEstatus, psTipo);
       }

       public static List<Entidad> Adscripcion(string psClvDep, string psOficio, string psEstatus)
       {
           return MngDatosViaticos.Adscripcion(psClvDep, psOficio, psEstatus);
       }

       public static List<Comision> TraerComision(string psUsuario, string psPeriodo)
       {
           return MngDatosViaticos.TraerComision(psUsuario, psPeriodo);
       }


       

       //******************************PATRICIA QUINO MORENO*************************************//

       

    }
}
