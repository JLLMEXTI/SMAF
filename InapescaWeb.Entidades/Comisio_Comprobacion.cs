/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.Entidades
	FileName:	Comision_Comprobacion.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Diciembre 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public  class Comisio_Comprobacion
    {
       private string lsOficio;
       private string lsArchivo;
       private string lsFechaAut;
       private string lsfechaComp;
       private string lsProyecto;
       private string lsProyDep;
       private string lsTipoCOmp;
       private string lsConceptoComprobacion;
       private string lsDescripcion;
       private string lsNumComprobante;
       private string lsImporte;
       private string lsXML;
       private string lsObservaciones;
       private string lsClaveDoc;
       private string lsDoc;
       private string lsFechaVobo;
       private string psMetodoPagoUsuario;
       private string psNconcepto;

       public string FECHA_VOBO
       {
           get { return lsFechaVobo; }
           set { lsFechaVobo = value; }
       }

       public string OFICIO
       {
           get { return lsOficio; }
           set { lsOficio = value; }
       }

       public string ARCHIVO
       {
           get { return lsArchivo; }
           set { lsArchivo = value; }
       }

       public string FECHA_AUT
       {

           get { return lsFechaAut; }
       }

       public string FECHA_COMPROBACION
       {
           get { return lsfechaComp; }
           set { lsfechaComp = value; }
       }

       public string PROYECTO
       {
           get { return lsProyecto; }
           set { lsProyecto = value; }
       }

       public string UBICACION_PROYECTO
       {
           get { return lsProyDep; }
           set { lsProyDep = value; }
       }

       public string ISFISCAL
       {
           get { return lsTipoCOmp; }
           set { lsTipoCOmp = value; }
       }

       public string CONCEPTO_COMP
       {
           get { return lsConceptoComprobacion; }
           set { lsConceptoComprobacion = value; }
       }

       public string DESCRIPCION_COMP
       {
           get { return lsDescripcion; }
           set { lsDescripcion = value; }
       }

       public string NUM_COMP
       {
           get { return lsNumComprobante; }
           set { lsNumComprobante = value; }
       }

       public string IMPORTE
       {
           get { return lsImporte; }
           set { lsImporte = value; }
       }

       public string XML
       {
           get { return lsXML; }
           set { lsXML = value; }
       }

       public string OBSERVACIONES
       {
           get { return lsObservaciones; }
           set { lsObservaciones = value; }
       }

       public string CLAVE_DOC
       {
           get { return lsClaveDoc; }
           set { lsClaveDoc = value; }
       }

       public string DOC_CPOMPROBATORIO
       {
           get { return lsDoc; }
           set { lsDoc = value; }
       }
       public string METODO_PAGO_USUARIO
       {
           get { return psMetodoPagoUsuario; }
           set { psMetodoPagoUsuario = value; }
       }
       public string NUM_CONCEP
       {
           get { return psNconcepto; }
           set { psNconcepto = value; }
       }
       
   }

}
