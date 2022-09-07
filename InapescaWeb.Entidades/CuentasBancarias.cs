using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public   class CuentasBancarias
    {
      private string lsCuenta;
      private string lsClabe;
      private string lsBanco;
      private string lsClvdep;

      public string Dependencia
      {
          get { return lsClvdep; }
          set { lsClvdep = value; }
      }

      public string Banco
      {
          get { return  lsBanco; }
          set { lsBanco = value; }
      }

      public string Clabe 
      {
          get { return lsClabe; }
          set { lsClabe = value; }
      }

      public string Cuenta
      {
          get { return lsCuenta; }
          set { lsCuenta = value; }
      }

    }
}
