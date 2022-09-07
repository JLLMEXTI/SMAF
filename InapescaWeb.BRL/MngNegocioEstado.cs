using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;


namespace InapescaWeb.BRL
{
  public  class MngNegocioEstado
    {

      public static List<Entidad> Estados()
      {

          return MngDatosEstado.Estados();
      }


      public static string Estado(string psEstado)
      {
          return MngDatosEstado.Estado(psEstado);
      }
      
    }
}
