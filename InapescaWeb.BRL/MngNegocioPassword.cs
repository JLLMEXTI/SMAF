using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
  public   class MngNegocioPassword
    {

      public static bool Cambia_Password(string psPassword, string psUsuario)
      {
          return MngDatosPassword.Cambia_Password(psPassword, psUsuario);
      }
    }
}
