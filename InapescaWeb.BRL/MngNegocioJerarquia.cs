using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;

namespace InapescaWeb.BRL
{
  public  class MngNegocioJerarquia
    {

      public static List<Entidad> Obtiene_Rol_Jerarquico(string psRol)
      {
          return MngDatosJerarquia.Obtiene_Rol_Jerarquico(psRol);
      }

      public static string Rol_jerarquico(string psRol)
      {
          return MngDatosJerarquia.Rol_Jerarquico(psRol);
      }
    }
}
