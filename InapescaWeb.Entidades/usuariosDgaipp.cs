using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public   class usuariosDgaipp
    {
      private string lsUsuario;
      private string lsPassword;
      private string lsNombre;
      private string lsApPat;
      private string lsApMat;
      private string lsRFC;
      private string lsCargo;
      private string lsEmail;
      private string lsRol;
      private string lsAbreviatura;

      public string Abreviatura
      {
          get { return lsAbreviatura; }
          set { lsAbreviatura = value; }
      }

      public string Rol
      {
          get { return lsRol; }
          set { lsRol = value; }
      }

      public string Email
      {
          get { return lsEmail; }
          set { lsEmail = value; }
      }

      public  string Cargo
      {
          get { return lsCargo; }
          set { lsCargo = value; }
      }

      public string RFC
      {
          get { return lsRFC; }
          set { lsRFC = value; }
      }

      public string ApellidoMaterno
      {
          get { return lsApMat; }
          set { lsApMat = value; }
      }

      public string ApellidoPaterno
      {
          get { return lsApPat; }
          set { lsApPat = value; }
      }

      public string Nombre
      {
          get { return lsNombre; }
          set { lsNombre = value; }
      }

      public string Usuario
      {
          get { return lsUsuario; }
          set { lsUsuario = value; }
      }

      public string Password
      {
          get { return lsPassword; }
          set { lsPassword = value; }
      }

      
    }
}
