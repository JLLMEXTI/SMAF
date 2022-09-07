/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Entidades
	FileName:	Xml.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class Xml
    { 
        //cfdi:comprobante
        private string serie;
        private string folio;
        private string formaPago;
        private string CtaPago;
        private string FechaExp;
        private string subtotal;
        private string descuento;
        private string total;
        private string MetodoPago;
        private string tipoComprobante;
        private string LugarExp;
        private string noCertificado;
        //CFDI:EMISOR
        private string rfc;
        private string NombreEMisor;
        //CFDI:DOMICILIOFISCAL
        private string calle;
        private string noExterior;
        private string noInterior;
        private string localidad;
        private string colonia;
        private string municipio;
        private string estado;
        private string pais;
        private string cp;
        //cfdi:RegimenFiscal
        private string regimen;
        //cfdi:Receptor
        private string rfcR;
        private string nombreR;
        //cfdi:Domicilio
        private string calleR;
        private string noExteriorR;
        private string noInteriorR;
        private string localidadR;
        private string coloniaR;
        private string municipioR;
        private string estadoR;
        private string paisR;
        private string codigoPostalR;
        //cfdi:Conceptos
        private string cantidad;
        private string unidad;
        private string concepto;
        private string valorUnit;
        private string importe;
        //cfdi:Impuestos
        private string totalImpuestosTrasladados;
        private string total_Impuestos_retenidos;
        //cfdi:Retenciones
        private string iva_ret;
        private string tua_ret;
        private string isr_ret;
        private string ieps_ret;
        private string sfp_ret;
        private string ish_ret;
        //cfdi:Traslados
        private string iva;
        private string tua;
        private string isr;
        private string ieps;
        private string sfp;
        private string ish;
        //tfd:TimbreFiscalDigital 
        private string TimbreFiscalDigital;
        private string fechaTimbrado;
        private string AñoExp;

        public Xml()
        {
            //tfd:TimbreFiscalDigital 
            TimbreFiscalDigital = "";
            fechaTimbrado = "";
            //cfdi:Traslados
            iva = "0";
            tua = "0";
            isr = "0";
            ieps = "0";
            sfp = "0";
            ish = "0";
            //cfdi:Retenciones
            iva_ret = "0";
            tua_ret = "0";
            isr_ret = "0";
            ieps_ret = "0";
            sfp_ret = "0";
            ish_ret = "0";
            //cfdi:Impuestos
            totalImpuestosTrasladados = "0";
            total_Impuestos_retenidos = "0";
            //cfdi:Conceptos
            cantidad = "";
            unidad = "";
            valorUnit = "";
            importe = "";
            concepto = "";
            //cfdi:Domicilio
            localidadR = "";
            calleR = "";
            noExteriorR = "";
            coloniaR = "";
            coloniaR = "";
            municipioR = "";
            estadoR = "";
            paisR = "";
            codigoPostalR = "";
            //cfdi:receptor
            rfcR = "";
            nombreR = "";
            //cfdi:regimefiscal
            regimen = "";
            //CFDI:DOMICILIFISCAL
            calle = "";
            noExterior = "";
            noInterior = "";
            localidad = "";
            colonia = "";
            municipio = "";
            estado = "";
            pais = "";
            cp = "";
            //cfdi:emisor
            NombreEMisor = "";
            rfc = "";
            //CFDI:COMPROBANTE
            serie = "";
            folio = "";
            formaPago = "";
            CtaPago = "";
            FechaExp = "";
            subtotal = "";
            descuento = "";
            total = "";
            MetodoPago = "";
            tipoComprobante = "";
            LugarExp = "";
            noCertificado = "";
            AñoExp = "";
        }

        //cfdi:Conceptos
        public string AÑOEXP
        {
            get { return AñoExp; }
            set { AñoExp = value; }
        }
        public string IMPORTE
        {
            get { return importe; }
            set { importe = value; }
        }

        public string VALOR_UNITARIO
        {
            get { return valorUnit; }
            set { valorUnit = value; }
        }
        public string CONCEPTO
        {
            get { return concepto; }
            set { concepto = value; }
        }

        public string UNIDAD
        {
            get { return unidad; }
            set { unidad = value; }
        }

        public string CANTIDAD
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

      /*  private string ;
        private string ;
        private string ;
        private string ;*/

        //cfdi:Domicilio
        public string CP_RECEPTOR
        {
            get { return codigoPostalR; }
            set { codigoPostalR = value; }
        }

        public string PAIS_RECEPTOR
        {
            get { return paisR; }
            set { paisR = value; }
        }

        public string ESTADO_RECEPTOR
        {
            get { return estadoR; }
            set { estadoR = value; }
        }
        public string MUNICIPIO_RECEPTOR
        {
            get { return municipioR; }
            set { municipioR = value; }
        }
        public string COLONIA_RECEPTOR
        {
            get { return coloniaR; }
            set { coloniaR = value; }
        }

        public string LOCALIDAD_RECEPTOR
        {
            get { return localidadR; }
            set { localidadR = value; }
        }

        public string NOINTERIOR_RECEPTOR
        {
            get { return noInteriorR; }
            set { noInteriorR = value; }
        }

        public string NOEXTERIOR_RECEPTOR
        {
            get { return noExteriorR; }
            set { noExteriorR = value; }
        }

        public string CALLE_RECEPTOR
        {
            get { return calleR; }
            set { calleR = value; }
        }

        //cfdi:receptor
        public string NOMBRE_RECEPTOR
        {
            get { return nombreR; }
            set { nombreR = value; }
        }

        public string RFC_RECEPTOR
        {
            get { return rfcR; }
            set { rfcR = value; }
        }

        //CFDI:REGIMENFISCAL
        public string REGIMENFISCAL_EMISOR
        {
            get { return regimen; }
            set { regimen = value; }
        }
        //CFDI_DOMICILIFISCAL
        public string CP_EMISOR
        {
            get { return cp; }
            set { cp = value; }
        }

        public string PAIS_EMISOR
        {
            get { return pais; }
            set { pais = value; }
        }

        public string ESTADO_EMISOR
        {
            get { return estado; }
            set { estado = value; }
        }

        public string MUNICIPIO_EMISOR
        {
            get { return municipio; }
            set { municipio = value; }
        }

        public string COLONIA_EMISOR
        {
            get { return colonia; }
            set { colonia = value; }
        }

        public string LOCALIDAD_EMISOR
        {
            get { return localidad; }
            set { localidad = value; }
        }

        public string NO_INTERIOR_EMISOR
        {
            get { return noInterior; }
            set { noInterior = value; }
        }
        public string NO_EXTERIOR_EMMISOR
        {
            get { return noExterior; }
            set { noExterior = value; }
        }

        public string CALLE_EMISOR
        {
            get { return calle; }
            set { calle = value; }
        }

        //CFDI:EMISOR
        public string NOMBRE_EMISOR
        {
            get { return NombreEMisor; }
            set { NombreEMisor = value; }
        }

        public string RFC_EMISOR
        {
            get { return rfc; }
            set { rfc = value; }
        }


        //CFDI:COMPROBANTE
        public string SERIE
        {
            get { return serie; }
            set { serie = value; }
        }

        public string FOLIO
        {
            get { return folio; }
            set { folio = value; }
        }

        public string FORMA_PAGO
        {
            get { return formaPago; }
            set { formaPago = value; }
        }

        public string CUENTA_PAGO
        {
            get { return CtaPago; }
            set { CtaPago = value; }
        }


        public string FECHA_EXPEDICION
        {
            get { return FechaExp; }
            set { FechaExp = value; }
        }

        public string SUBTOTAL
        {
            get { return subtotal; }
            set { subtotal = value; }
        }

        public string DESCUENTO
        {
            get { return descuento; }
            set { descuento = value; }
        }

        public string TOTAL
        {
            get { return total; }
            set { total = value; }
        }

        public string METODO_PAGO
        {
            get { return MetodoPago; }
            set { MetodoPago = value; }
        }

        public string TIPO_COMPROBANTE
        {
            get { return tipoComprobante; }
            set { tipoComprobante = value; }
        }

        public string LUGAR_EXPEDICION
        {
            get { return LugarExp; }
            set { LugarExp = value; }
        }


        public string NO_CERTIFICADO
        {
            get { return noCertificado; }
            set { noCertificado = value; }
        }



        public string TOTAL_IMPUESTOS_RETENIDOS
        {
            get { return total_Impuestos_retenidos; }
            set { total_Impuestos_retenidos = value; }
        }

        public string FECHA_TIMBRADO
        {
            get { return fechaTimbrado; }
            set { fechaTimbrado = value; }
        }

        public string TIMBRE_FISCAL
        {
            get { return TimbreFiscalDigital; }
            set { TimbreFiscalDigital = value; }
        }

        public string TOTAL_IMPUESTOS_TRASLADADOS
        {
            get { return totalImpuestosTrasladados; }
            set { totalImpuestosTrasladados = value; }
        }

        public string ISH_RETENIDO
        {
            get { return ish_ret; }
            set { ish_ret = value; }
        }

        public string SFP_RETENIDO
        {
            get { return sfp_ret; }
            set { sfp_ret = value; }
        }

        public string IEPS_RETENIDO
        {
            get { return ieps_ret; }
            set { ieps_ret = value; }
        }

        public string ISR_RETENIDO
        {
            get { return isr_ret; }
            set { isr_ret = value; }
        }

        public string TUA_RETENIDO
        {
            get { return tua_ret; }
            set { tua_ret = value; }
        }

        public string IVA_RETENIDO
        {
            get { return iva_ret; }
            set { iva_ret = value; }
        }

     

        public string ISH
        {
            get { return ish; }
            set { ish = value; }
        }

        public string SFP
        {
            get { return sfp; }
            set { sfp = value; }
        }

        public string IEPS
        {
            get { return ieps; }
            set { ieps = value; }
        }

        public string ISR
        {
            get { return isr; }
            set { isr = value; }
        }

        public string TUA
        {
            get { return tua; }
            set { tua = value; }
        }

        public string IVA
        {
            get { return iva; }
            set { iva = value; }
        }




    }
}
