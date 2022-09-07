using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public   class Ministracion
    {
      private string lsTipoMinistracion;
      private string lsClavePeriodo;
      private string lsFolio;
      private string lsFecha;
      private string lsPeriodo;
      private string lsUsuario;
      private string lsFechaAut;
      private string lsAutoriza;
      private string lsFechaPago;
      private string lsUsuarioPagador;
      private string lsDep;
      private string lsProy;
      private string lsDepProy;
      private string lsDocumento;
      private string lsARchivo;
      private string lsFormaPago;
      private string lsRefBancaria;
      private string lsBancoDestino;
      private string lsCuentaDestino;
      private string lsClabeDestino;
      private string lsRazonSocial;
      private string lsRFC;
      private string lsFolioFactura;
      private string lsFechEmision;
      private string lsConcepto;
      private string lsDescConcepto;
      private string lsSubtotal;
      private string lsIva;
      private string lsTua;
      private string lsSFP;
      private string lsISR;
      private string lsTotalSolicitado;
      private string lsIvaRet;
      private string lsTuaret;
      private string lsIsrRet;
      private string lsSfpRet;
      private string lsDescuento;
      private string lsToalPagado;
      private string lsFechaRecepcion;
      private string lsProceso;
      private string lsPartida;
      private string lsestatus;
      private string lsSec;
      private string lsFechaEff;
      private string lsObservaciones;

      private string lsBancoOrigen;
      private string lsCuentaOrigen;
      private string lsClabeOrigen;

      public string Usuario_Genera
      {
          get { return lsUsuario; }
          set { lsUsuario = value; }
      }

      public string Fecha_Autoriza
      {
          get { return lsFechaAut; }
          set { lsFechaAut = value; }
      }

      public string Autoriza
      {
          get { return lsAutoriza; }
          set { lsAutoriza = value; }
      }

      public string Usuario_Pagador
      {
          get { return lsUsuarioPagador; }
          set { lsUsuarioPagador = value; }
      }

      public string Ubicacion_Genera
      {
          get { return lsDep; }
          set { lsDep = value; }
      }

      public string Proyecto
      {
          get { return lsProy; }
          set { lsProy = value; }
      }

      public string Ubicacion_Proyecto
      {
          get { return lsDepProy; }
          set { lsDepProy = value; }
      }

      public string Documento
      {
          get { return lsDocumento; }
          set { lsDocumento = value; }
      }

      public string Archivo
      {
          get { return lsARchivo; }
          set { lsARchivo = value; }
      }

      public string Forma_Pago
      {
          get { return lsFormaPago; }
          set { lsFormaPago = value; }
      }

      public string Razon_Social
      {
          get { return lsRazonSocial; }
          set { lsRazonSocial = value; }
      }

      public string RFC
      {
          get { return lsRFC; }
          set { lsRFC = value; }
      }

      public string Folio_Factura
      {
          get { return lsFolioFactura; }
          set { lsFolioFactura = value; }

      }

      public string Fecha_Emision
      {
          get { return lsFechEmision; }
          set { lsFechEmision = value; }
      }

      public string Descripcion_Concepto
      {
          get { return lsDescConcepto; }
          set { lsDescConcepto = value; }
      }

      public string Subtotal
      {
          get { return lsSubtotal; }
          set { lsSubtotal = value; }
      }

      public string IVA
      {
          get { return lsIva; }
          set { lsIva = value; }
      }

      public string TUA
      {
          get { return lsTua; }
          set { lsTua = value; }
      }

      public string SFP
      {
          get { return lsSFP; }
          set { lsSFP = value; }
      }

      public string ISR
      {
          get { return lsISR; }
          set { lsISR = value; }
      }

      public string Iva_Retenido
      {
          get { return lsIvaRet; }
          set { lsIvaRet = value;}
      }

      public string Tua_Retenido
      {
          get { return lsTuaret; }
          set { lsTuaret = value; }
      }

      public string ISR_Retenido
      {
          get { return lsIsrRet; }
          set { lsIsrRet = value; }
      }

      public string SFP_Retenido
      {
          get { return lsSfpRet; }
          set { lsSfpRet = value; }
      }

      public string Descuentos
      {
          get { return lsDescuento; }
          set { lsDescuento = value; }
      }

      public string Fecha_Recepcion
      {
          get { return lsFechaRecepcion; }
          set { lsFechaRecepcion = value; }
      }

      public string Proceso
      {
          get { return lsProceso; }
          set { lsProceso = value; }
      }

      public string Partida
      {
          get { return lsPartida; }
          set { lsPartida = value; }
      }

      public string Estatus
      {
          get { return lsestatus; }
          set { lsestatus = value; }
      }

      public string Secuencia
      {
          get { return lsSec; }
          set { lsSec = value; }
      }

      public string Observaciones
      {
          get { return lsObservaciones; }
          set { lsObservaciones = value; }
      }

      public string Clabe_destino
      {
          get {return  lsClabeDestino; }
          set { lsClabeDestino = value; }
      }

      public string Cuenta_Destino
      {
          get { return lsCuentaDestino; }
          set { lsCuentaDestino = value; }
      }

      public string Banco_Destino
      {
          get { return lsBancoDestino; }
          set { lsBancoDestino = value; }
      }

      public string Referencia_Bancaria
      {
          get { return lsRefBancaria; }
          set { lsRefBancaria = value; }
      }

      public string Clabe_Origen
      {
          get { return lsClabeOrigen; }
          set { lsClabeOrigen = value; }
      }

      public string Cuenta_Origen
      {
          get { return lsCuentaOrigen; }
          set { lsCuentaOrigen = value; }
      }

      public string Banco_Origen
      {
          get { return lsBancoOrigen; }
          set { lsBancoOrigen = value; }
      }

      public string Clave_Periodo
      {
          get { return lsClavePeriodo; }
          set { lsClavePeriodo = value; }
      }

      public string Fecha_Eff
      {
          get { return lsFechaEff; }
          set { lsFechaEff = value; }
      }

      public string Fecha_Pago
      {
          get { return lsFechaPago; }
          set { lsFechaPago = value; }
      }


      public string Total_Pagado
      {
          get { return lsToalPagado; }
          set { lsToalPagado = value; }
      }

      public string Total_Solicitado
      {
          get { return lsTotalSolicitado; }
          set { lsTotalSolicitado = value; }
      }

      public string Concepto 
      {
          get { return lsConcepto; }
          set { lsConcepto = value; }
      }

      public string Periodo
      {
          get { return lsPeriodo; }
          set { lsPeriodo = value; }
      }

      public string Fecha
      {
          get { return lsFecha; }
          set { lsFecha = value; }
      }

      public string Folio
      {
          get { return lsFolio; }
          set { lsFolio = value; }
      }

      public string Tipo_Ministracion
      {
          get { return lsTipoMinistracion; }
          set { lsTipoMinistracion = value; }
      }

    }
}
