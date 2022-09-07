using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public class Minutario
    {
      private string lsOficio;
      private string lsComplemento;
      private string lsArchivo;
      private string lsTipo;
      private string lsReservado;
      private string lsExpediente;
      private string lsDoc;
      private string lsDep;
      private string lsDescripcion;
      private string lsUsuarioDestino;
      private string lsUsuarioRegistro;
      private string lsFechaCap;
      private string lsEstatus;
      private string lsSec;
      private string lsPeriodo;
      private string lsEstatusReservado;

      public string Estatus_Reservado
      {
          get { return lsEstatusReservado; }
          set { lsEstatusReservado = value; }
      }

      public string Oficio
      {
          get { return lsOficio; }
          set { lsOficio = value; }
      }

      public string Complemento
      {
          get { return lsComplemento; }
          set { lsComplemento = value; }
      }

      public string Archivo
      {
          get { return lsArchivo; }
          set { lsArchivo = value; }
      }

      public string Tipo
      {
          get { return lsTipo; }
          set { lsTipo = value; }
      }

      public string Reservado
      {
          get { return lsReservado; }
          set { lsReservado = value; }
      }

      public string Expediente
      {
          get { return lsExpediente; }
          set { lsExpediente = value; }
      }

      public string Docuemnto_Referencia
      {
          get { return  lsDoc; }
          set { lsDoc = value; }
      }

      public string Ubicacion_Destino
      {
          get { return lsDep; }
          set { lsDep = value; }
      }

      public string Descripcion
      {
          get { return lsDescripcion; }
          set { lsDescripcion = value; }
      }

      public string Usuario_destino
      {
          get { return lsUsuarioDestino; }
          set { lsUsuarioDestino = value; }
      }

      public string Usuario_Captura
      {
          get { return lsUsuarioRegistro; }
          set { lsUsuarioRegistro = value; }
      }

      public string Fecha_captura
      {
          get { return lsFechaCap; }
          set { lsFechaCap = value ;}
      }

      public string Estatus
      {
          get { return lsEstatus; }
          set { lsEstatus = value; }
      }

      public string Secuencia
      {
          get { return lsSec; }
          set { lsSec = value; }
      }

      public string Periodo
      {
          get { return lsPeriodo; }
          set { lsPeriodo = value; }
      }



    }
}
