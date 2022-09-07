using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public class GenerarRef


    {
       private string lsNumero;
       private string lsId;
       private string lsReferencia;
       private string lsArchivo;
       private string lsComisionado;
       private string lsEstatus;
       private string lsSecEff;
       private string lsConceptos;
       private string lsImporte;
       private string lsFecha;
       private string lsFechaPago;
       private string lsImportePago;


       private string lsFechaVencimiento;


       public string Numero
       {
           get { return lsNumero; }
           set { lsNumero = value; }
       }
       public string ID
      {
          get { return lsId; }
          set { lsId = value; }
    }

       public string Referencia
       {
           get { return lsReferencia; }
           set { lsReferencia = value; }
       }

       public string Archivo
       {
           get { return lsArchivo; }
           set { lsArchivo = value; }
       }
       public string Comisionado
       {
           get { return lsComisionado; }
           set { lsComisionado = value; }
       }
       public string Estatus
       {
           get { return lsEstatus; }
           set { lsEstatus = value; }
       }
       public string SecEff
       {
           get { return lsSecEff; }
           set { lsSecEff = value; }
       }
       public string Concepto
       {
           get { return lsConceptos; }
           set { lsConceptos = value; }
       }
       public string Importe
       {
           get { return lsImporte; }
           set { lsImporte = value; }
       }
       public string Fecha
       {
           get { return lsFecha; }
           set { lsFecha = value; }
       }
       public string FechaPago
       {
           get { return lsFechaPago; }
           set { lsFechaPago = value; }
       }
       public string ImportePagado
       {
           get { return lsImportePago; }
           set { lsImportePago = value; }
       }
       public string FechaVencimiento
       {
           get { return lsFechaVencimiento; }
           set { lsFechaVencimiento = value; }
       }
   }

}
