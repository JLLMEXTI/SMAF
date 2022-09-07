using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
namespace InapescaWeb.BRL
{
  public  class mngNegocioProovedor
    {
      public static bool Update_Proveedor(Proovedor poProovedor)
      {
          return MngDatosProovedores.Update_Proveedor(poProovedor);
      }
      public static Proovedor Proveedor(string psRFC)
      {
          return MngDatosProovedores.Proveedor(psRFC);
      }
    }
}
