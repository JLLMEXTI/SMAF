using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
  public   class MngNegocioEtq
    {
      public static string[] Obtiene_Etq(string psIdioma)
      {
          return MngDatosEtq.Obt_Etq(psIdioma);
      }
    }
}
