using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public  class Transparencia_Partidas
    {
      private string qsImporte;
        private string qsID;
        private string qsClv_PartidaConceptos;
        private string qsClv_Denominacion_PartidaConcepto;
    

      public string Importe
      {
          get { return qsImporte; }
          set { qsImporte = value; }
      }
      public string ID
      {
          get { return qsID; }
          set { qsID = value; }
      }
      public string Clv_PartidaConceptos
      {
          get { return qsClv_PartidaConceptos; }
          set { qsClv_PartidaConceptos = value; }
      }
      public string Clv_Denominacion_PartidaConcepto
      {
          get { return qsClv_Denominacion_PartidaConcepto; }
          set { qsClv_Denominacion_PartidaConcepto = value; }
      }

    }
}
