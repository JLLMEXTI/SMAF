using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;


namespace InapescaWeb.BRL
{
  public     class MngNegocioComponente
    {
      public static List<Entidad> Obtienencomponente()
      {
          return InapescaWeb.DAL.MngDatosComponente.Obtienencomponente();
      }
      
    }
}
