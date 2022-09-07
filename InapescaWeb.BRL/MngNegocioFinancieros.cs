using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
namespace InapescaWeb.BRL
{
  public   class MngNegocioFinancieros
    {
      public static string Obtiene_Trimestre(string psFecha)
      {
          return MngDatosFinancieros.Obtiene_Trimestre(psFecha);
      }
    }
}
