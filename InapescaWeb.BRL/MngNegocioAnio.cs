using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
   public class MngNegocioAnio
    {
       public static Entidad Inicio_Final(string psPeridodo)
       {
           return MngDatosAnio.Inicio_Final(psPeridodo);
       }
        public static List<Entidad> ObtieneAnios()
        {
            return MngDatosAnio.ObtieneAnios();
        }

        public static string Anio(string psPeriodo)
        {
            return MngDatosAnio.Anio(psPeriodo);
        }

        public static string Fin_Anio(string psPeriodo)
        {
            return MngDatosAnio.Fin_Anio(psPeriodo);
        }

        public static string Obtiene_Periodo(string psInicio, string psFinal)
        {
            return MngDatosAnio.Obtiene_Periodo(psInicio, psFinal);
        }
    }
}
