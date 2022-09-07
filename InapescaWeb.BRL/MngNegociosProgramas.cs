using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
   public class MngNegociosProgramas
    {

       public static List<Programa> ObtieneProgramas1(string psYear)
       {
           return MngDatosProgramas.ObtieneProgramas1 (psYear);
       }

       public static List<Entidad> ListaProgramas(string psDirecciones, string psYear)
       {
           return MngDatosProgramas.ListaProgramas(psDirecciones, psYear);
       }

       public static Boolean Insert_Programas(string psclv_componente,string psPrograma, string psdescripcion, string psdesc_corta, string psobjetivo, string pscoordinador, string psclv_direccion, string pspresupuesto)
       { return MngDatosProgramas.Insert_Programas(psclv_componente,psPrograma, psdescripcion, psdesc_corta, psobjetivo, pscoordinador, psclv_direccion, pspresupuesto); }

       public static List<Entidad> Obtiene_Programas_Direccion(string psUbicacion)
       {
           return MngDatosProgramas.Obtiene_Programas_Direccion(psUbicacion);
       }

       public static Programa Obtiene_Datos_Programa(string psPrograma, string psDireccion)
       {
         return  MngDatosProgramas.Obtiene_Datos_Programa(psPrograma, psDireccion);
       }

       public static List<Programa > Obtiene_Programas(string psDirecciones, string psYear = "",bool proyecto = false )
       {
           return MngDatosProgramas.Obtiene_Programas(psDirecciones, psYear,proyecto);
       }

       public static string Obtiene_Max_Programa(string psDireccion)
       {
           return MngDatosProgramas.Obtiene_Max_Programa(psDireccion);
       }

       public static List<Entidad> Programas(string psDireccion, string psAnio = "")
       {
           return MngDatosProgramas.Programas(psDireccion, psAnio);
       }

       public static Boolean IsDireccion(string psDireccion)
       {
           return MngDatosProgramas.IsDireccion(psDireccion);
       }


    
    }
}
