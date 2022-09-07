using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
 public    class MngNegocioZona
    {
     public static string obtienDescripcion(string psZona)
     {
         return MngDatosZonas.obtienDescripcion(psZona);
     }

     public static double obtieneTarifa(int psZona)
     {
         return MngDatosZonas.obtieneTarifa(psZona);
     }

    }
}
