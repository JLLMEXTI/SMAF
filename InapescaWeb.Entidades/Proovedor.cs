using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public class Proovedor
    {
      private string lsRfc;
      private string lsRazon;
      private string lsContacto;
      private string lsTelefono;
      private string lsEmail;
      private string lsCalle;
      private string lsNumExt;
      private string lsNumInt;
      private string lsColonia;
      private string lsMunicipio;
      private string lsCiudad;
      private string lsEstado;
      private string lsPais;
      private string lsCp;
      private string lsTel1;
      private string lsTel2;
      private string lsRegimen;
      private string lsServicio;
      private string lsEstatus;
      private string lsFecha;

      public string Expedicion
      {
          get;
          set;
      }
      public string Tipo_ident
      {
          get;
          set;
      }

      public string Num_Ident
      {
          get;
          set;
      }
      public string Curp
      {
          get;
          set;
      }

      public string Clabe
      {
          get;
          set;
      }
      public string Banco
      {
          get;
          set;
      }

      public string Cuenta
      {
          get;
          set;
      }

      public string Fecha
      {
          get { return lsFecha; }
          set { lsFecha = value; }
      }

      public string Estatus
      {
          get { return lsEstatus; }
          set { lsEstatus = value; }
      }

      public string Servicio
      {
          get { return lsServicio; }
          set { lsServicio = value; }
      }

      public string Regimen_Fiscal
      {
          get { return lsRegimen; }
          set { lsRegimen = value; }
      }

      public string Telefono2
      {
          get { return lsTel2; }
          set { lsTel2 = value; }
      }

      public string Telefono1
      {
          get { return lsTel1; }
          set {lsTel1 = value ;}
      }

      public string CP
      {
          get { return lsCp; }
          set { lsCp = value; }
      }

      public string Pais
      {
          get { return lsPais; }
          set { lsPais = value; }
      }

      public string Estado
      {
          get { return lsEstado; }
          set { lsEstado = value; }
      }

      public string Ciudad
      {
          get { return lsCiudad;}
          set { lsCiudad = value; }
      }

      public string Municipio
      {
          get { return lsMunicipio; }
          set { lsMunicipio = value; }
      }

      public string Colonia
      {
          get { return lsColonia; }
          set { lsColonia = value; }
      }

      public string Num_int
      {
          get { return lsNumInt; }
          set { lsNumInt = value; }
      }

      public string Num_Ext
      {
          get { return lsNumExt; }
          set { lsNumExt = value; }
      }

      public string Calle
      {
          get { return lsCalle;  }
          set { lsCalle = value; }
      }

      public string Email
      {
          get { return lsEmail; }
          set { lsEmail = value; }
      }

      public string Telefono
      {
          get { return lsTelefono; }
          set { lsTelefono = value; }
      }

      public string Contacto
      {
          get { return lsContacto; }
          set { lsContacto = value; }
      }

      public string Razon_Social
      {
          get { return lsRazon; }
          set { lsRazon = value; }
      }

      public string RFC
      {
          get { return lsRfc; }
          set { lsRfc = value; }
      }
    }
}
