using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades ;
using InapescaWeb.DAL;
namespace InapescaWeb.BRL
{
  public   class MngNegocioTransporte
    {

      public static List<Entidad> ObtieneClaseTrans(string psUbicacion,string psYear = "")
      {
          return MngDatosTransporte.ObtieneClase(psUbicacion,psYear );
      }
      public static List<Entidad> ObtieneTipoTrans(string psClase, string psUbicacion, string psYear = "")
      {
          return MngDatosTransporte.ObtieneTipo(psClase, psUbicacion,psYear );
      }

      public static List <Entidad > Descripcion_Transporte (string psClase, string psTipo,string  psUbicacion,string psYear = "")
      {
          return MngDatosTransporte.Descripcion_Transporte(psClase, psTipo, psUbicacion, psYear);
      }

      public static Boolean Scheduller(string psFechaI, string psFechaF, string psTransp, string psUbicacion)
      {
          return MngDatosTransporte.MngScheduller(psFechaI, psFechaF, psTransp, psUbicacion);
      }

      public static string[] Descrip_Trans(string psClase, string psTipo,  string psUbicacion, string psVehiculo = "")
      {
          return InapescaWeb.DAL.MngDatosTransporte.Descrip_Trans(psClase, psTipo, psUbicacion, psVehiculo);
      }

      public static List<Entidad> Obtiene_TransPDF(string psMixto = "")
      {
          return MngDatosTransporte.Obtiene_TransPDF(psMixto );
      }

      public static string Obtiene_Clave_Trans(string psClase, string psTipo)
      {
          return MngDatosTransporte.Obtiene_Clave_Trans(psClase, psTipo);
      }
    }
}
