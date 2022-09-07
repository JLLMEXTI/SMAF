//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
[XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/4", IsNullable = false)]
public partial class Comprobantev4
{

    private Comprobantev4CfdiRelacionados cfdiRelacionadosField;

    private Comprobantev4Emisor emisorField;

    private Comprobantev4Receptor receptorField;

    private Comprobantev4Concepto[] conceptosField;

    private Comprobantev4Impuestos impuestosField;

    private Comprobantev4Complemento[] complementoField;

    private Comprobantev4Addenda addendaField;

    private string versionField;

    private string serieField;

    private string folioField;

    private System.DateTime fechaField;

    private string selloField;

    private string formaPagoField;

    //eliminar 3.2
    private string formaDePagoField;

    private bool formaPagoFieldSpecified;

    private string noCertificadoField;

    private string certificadoField;

    private string condicionesDePagoField;

    private decimal subTotalField;

    private decimal descuentoField;

    private bool descuentoFieldSpecified;

    private string monedaField;

    private decimal tipoCambioField;

    private bool tipoCambioFieldSpecified;

    private decimal totalField;

    private string tipoDeComprobanteField;

    private string metodoPagoField;

    // eliminar 3.2
    private string metodoDePagoField;

    private bool metodoPagoFieldSpecified;

    private string lugarExpedicionField;

    private string confirmacionField;

    //eliminar 3.2
    private string numCtaPagoField;

    //public Comprobante() {
    //    this.versionField = "3.3";
    //}

    /// <comentarios/>
    public Comprobantev4CfdiRelacionados CfdiRelacionados
    {
        get
        {
            return this.cfdiRelacionadosField;
        }
        set
        {
            this.cfdiRelacionadosField = value;
        }
    }

    /// <comentarios/>
    public Comprobantev4Emisor Emisor
    {
        get
        {
            return this.emisorField;
        }
        set
        {
            this.emisorField = value;
        }
    }

    /// <comentarios/>
    public Comprobantev4Receptor Receptor
    {
        get
        {
            return this.receptorField;
        }
        set
        {
            this.receptorField = value;
        }
    }

    /// <comentarios/>
    [XmlArrayItemAttribute("Concepto", IsNullable = false)]
    public Comprobantev4Concepto[] Conceptos
    {
        get
        {
            return this.conceptosField;
        }
        set
        {
            this.conceptosField = value;
        }
    }

    /// <comentarios/>
    public Comprobantev4Impuestos Impuestos
    {
        get
        {
            return this.impuestosField;
        }
        set
        {
            this.impuestosField = value;
        }
    }

    /// <comentarios/>
    [XmlElementAttribute("Complemento")]
    public Comprobantev4Complemento[] Complemento
    {
        get
        {
            return this.complementoField;
        }
        set
        {
            this.complementoField = value;
        }
    }

    /// <comentarios/>
    public Comprobantev4Addenda Addenda
    {
        get
        {
            return this.addendaField;
        }
        set
        {
            this.addendaField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Serie
    {
        get
        {
            return this.serieField;
        }
        set
        {
            this.serieField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string serie
    {
        get
        {
            return this.serieField;
        }
        set
        {
            this.serieField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Folio
    {
        get
        {
            return this.folioField;
        }
        set
        {
            this.folioField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string folio
    {
        get
        {
            return this.folioField;
        }
        set
        {
            this.folioField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public System.DateTime Fecha
    {
        get
        {
            return this.fechaField;
        }
        set
        {
            this.fechaField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public System.DateTime fecha
    {
        get
        {
            return this.fechaField;
        }
        set
        {
            this.fechaField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Sello
    {
        get
        {
            return this.selloField;
        }
        set
        {
            this.selloField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string FormaPago
    {
        get
        {
            return this.formaPagoField;
        }
        set
        {
            this.formaPagoField = value;
        }
    }

    /// <eliminar 3.2/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string formaDePago
    {
        get
        {
            return this.formaDePagoField;
        }
        set
        {
            this.formaDePagoField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool FormaPagoSpecified
    {
        get
        {
            return this.formaPagoFieldSpecified;
        }
        set
        {
            this.formaPagoFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string NoCertificado
    {
        get
        {
            return this.noCertificadoField;
        }
        set
        {
            this.noCertificadoField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string noCertificado
    {
        get
        {
            return this.noCertificadoField;
        }
        set
        {
            this.noCertificadoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Certificado
    {
        get
        {
            return this.certificadoField;
        }
        set
        {
            this.certificadoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string CondicionesDePago
    {
        get
        {
            return this.condicionesDePagoField;
        }
        set
        {
            this.condicionesDePagoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal SubTotal
    {
        get
        {
            return this.subTotalField;
        }
        set
        {
            this.subTotalField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal subTotal
    {
        get
        {
            return this.subTotalField;
        }
        set
        {
            this.subTotalField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Descuento
    {
        get
        {
            return this.descuentoField;
        }
        set
        {
            this.descuentoField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal descuento
    {
        get
        {
            return this.descuentoField;
        }
        set
        {
            this.descuentoField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool DescuentoSpecified
    {
        get
        {
            return this.descuentoFieldSpecified;
        }
        set
        {
            this.descuentoFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Moneda
    {
        get
        {
            return this.monedaField;
        }
        set
        {
            this.monedaField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal TipoCambio
    {
        get
        {
            return this.tipoCambioField;
        }
        set
        {
            this.tipoCambioField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool TipoCambioSpecified
    {
        get
        {
            return this.tipoCambioFieldSpecified;
        }
        set
        {
            this.tipoCambioFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Total
    {
        get
        {
            return this.totalField;
        }
        set
        {
            this.totalField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal total
    {
        get
        {
            return this.totalField;
        }
        set
        {
            this.totalField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string TipoDeComprobante
    {
        get
        {
            return this.tipoDeComprobanteField;
        }
        set
        {
            this.tipoDeComprobanteField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string tipoDeComprobante
    {
        get
        {
            return this.tipoDeComprobanteField;
        }
        set
        {
            this.tipoDeComprobanteField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string MetodoPago
    {
        get
        {
            return this.metodoPagoField;
        }
        set
        {
            this.metodoPagoField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string metodoDePago
    {
        get
        {
            return this.metodoDePagoField;
        }
        set
        {
            this.metodoDePagoField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool MetodoPagoSpecified
    {
        get
        {
            return this.metodoPagoFieldSpecified;
        }
        set
        {
            this.metodoPagoFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string LugarExpedicion
    {
        get
        {
            return this.lugarExpedicionField;
        }
        set
        {
            this.lugarExpedicionField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Confirmacion
    {
        get
        {
            return this.confirmacionField;
        }
        set
        {
            this.confirmacionField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string NumCtaPago
    {
        get
        {
            return this.numCtaPagoField;
        }
        set
        {
            this.numCtaPagoField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4CfdiRelacionados
{

    private ComprobanteCfdiRelacionadosCfdiRelacionado[] cfdiRelacionadoField;

    private string tipoRelacionField;

    /// <comentarios/>
    [XmlElementAttribute("CfdiRelacionado")]
    public ComprobanteCfdiRelacionadosCfdiRelacionado[] CfdiRelacionado
    {
        get
        {
            return this.cfdiRelacionadoField;
        }
        set
        {
            this.cfdiRelacionadoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string TipoRelacion
    {
        get
        {
            return this.tipoRelacionField;
        }
        set
        {
            this.tipoRelacionField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4CfdiRelacionadosCfdiRelacionado
{

    private string uUIDField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string UUID
    {
        get
        {
            return this.uUIDField;
        }
        set
        {
            this.uUIDField = value;
        }
    }
}


/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4Emisor
{

    private string rfcField;

    private string nombreField;

    private string regimenFiscalField;


    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string RegimenFiscal
    {
        get
        {
            return this.regimenFiscalField;
        }
        set
        {
            this.regimenFiscalField = value;
        }
    }

}


/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4Receptor
{

    private string rfcField;

    private string nombreField;

    private string residenciaFiscalField;

    private bool residenciaFiscalFieldSpecified;

    private string numRegIdTribField;

    private string usoCFDIField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string ResidenciaFiscal
    {
        get
        {
            return this.residenciaFiscalField;
        }
        set
        {
            this.residenciaFiscalField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool ResidenciaFiscalSpecified
    {
        get
        {
            return this.residenciaFiscalFieldSpecified;
        }
        set
        {
            this.residenciaFiscalFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string NumRegIdTrib
    {
        get
        {
            return this.numRegIdTribField;
        }
        set
        {
            this.numRegIdTribField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string UsoCFDI
    {
        get
        {
            return this.usoCFDIField;
        }
        set
        {
            this.usoCFDIField = value;
        }
    }
}

/// <comentarios/>



/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4Concepto
{

    private Comprobantev4ConceptoImpuestos impuestosField;

    private Comprobantev4ConceptoInformacionAduanera[] informacionAduaneraField;

    private Comprobantev4ConceptoCuentaPredial cuentaPredialField;

    private Comprobantev4ConceptoComplementoConcepto complementoConceptoField;

    private Comprobantev4ConceptoParte[] parteField;

    private string claveProdServField;

    private string noIdentificacionField;

    private decimal cantidadField;

    private string claveUnidadField;

    private string unidadField;

    private string descripcionField;

    private decimal valorUnitarioField;

    private decimal importeField;

    private decimal descuentoField;

    private bool descuentoFieldSpecified;

    /// <comentarios/>
    public Comprobantev4ConceptoImpuestos Impuestos
    {
        get
        {
            return this.impuestosField;
        }
        set
        {
            this.impuestosField = value;
        }
    }

    /// <comentarios/>
    [XmlElementAttribute("InformacionAduanera")]
    public Comprobantev4ConceptoInformacionAduanera[] InformacionAduanera
    {
        get
        {
            return this.informacionAduaneraField;
        }
        set
        {
            this.informacionAduaneraField = value;
        }
    }

    /// <comentarios/>
    public Comprobantev4ConceptoCuentaPredial CuentaPredial
    {
        get
        {
            return this.cuentaPredialField;
        }
        set
        {
            this.cuentaPredialField = value;
        }
    }

    /// <comentarios/>
    public Comprobantev4ConceptoComplementoConcepto ComplementoConcepto
    {
        get
        {
            return this.complementoConceptoField;
        }
        set
        {
            this.complementoConceptoField = value;
        }
    }

    /// <comentarios/>
    [XmlElementAttribute("Parte")]
    public Comprobantev4ConceptoParte[] Parte
    {
        get
        {
            return this.parteField;
        }
        set
        {
            this.parteField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string ClaveProdServ
    {
        get
        {
            return this.claveProdServField;
        }
        set
        {
            this.claveProdServField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string NoIdentificacion
    {
        get
        {
            return this.noIdentificacionField;
        }
        set
        {
            this.noIdentificacionField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Cantidad
    {
        get
        {
            return this.cantidadField;
        }
        set
        {
            this.cantidadField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string ClaveUnidad
    {
        get
        {
            return this.claveUnidadField;
        }
        set
        {
            this.claveUnidadField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Unidad
    {
        get
        {
            return this.unidadField;
        }
        set
        {
            this.unidadField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Descripcion
    {
        get
        {
            return this.descripcionField;
        }
        set
        {
            this.descripcionField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string descripcion
    {
        get
        {
            return this.descripcionField;
        }
        set
        {
            this.descripcionField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal ValorUnitario
    {
        get
        {
            return this.valorUnitarioField;
        }
        set
        {
            this.valorUnitarioField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Descuento
    {
        get
        {
            return this.descuentoField;
        }
        set
        {
            this.descuentoField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool DescuentoSpecified
    {
        get
        {
            return this.descuentoFieldSpecified;
        }
        set
        {
            this.descuentoFieldSpecified = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoImpuestos
{

    private Comprobantev4ConceptoImpuestosTraslado[] trasladosField;

    private Comprobantev4ConceptoImpuestosRetencion[] retencionesField;

    /// <comentarios/>
    [XmlArrayItemAttribute("Traslado", IsNullable = false)]
    public Comprobantev4ConceptoImpuestosTraslado[] Traslados
    {
        get
        {
            return this.trasladosField;
        }
        set
        {
            this.trasladosField = value;
        }
    }

    /// <comentarios/>
    [XmlArrayItemAttribute("Retencion", IsNullable = false)]
    public Comprobantev4ConceptoImpuestosRetencion[] Retenciones
    {
        get
        {
            return this.retencionesField;
        }
        set
        {
            this.retencionesField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoImpuestosTraslado
{

    private decimal baseField;

    private string impuestoField;

    private string tipoFactorField;

    private decimal tasaOCuotaField;

    private bool tasaOCuotaFieldSpecified;

    private decimal importeField;

    private bool importeFieldSpecified;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Base
    {
        get
        {
            return this.baseField;
        }
        set
        {
            this.baseField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string TipoFactor
    {
        get
        {
            return this.tipoFactorField;
        }
        set
        {
            this.tipoFactorField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal TasaOCuota
    {
        get
        {
            return this.tasaOCuotaField;
        }
        set
        {
            this.tasaOCuotaField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool TasaOCuotaSpecified
    {
        get
        {
            return this.tasaOCuotaFieldSpecified;
        }
        set
        {
            this.tasaOCuotaFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool ImporteSpecified
    {
        get
        {
            return this.importeFieldSpecified;
        }
        set
        {
            this.importeFieldSpecified = value;
        }
    }
}



/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoImpuestosRetencion
{

    private decimal baseField;

    private string impuestoField;

    private string tipoFactorField;

    private decimal tasaOCuotaField;

    private decimal importeField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Base
    {
        get
        {
            return this.baseField;
        }
        set
        {
            this.baseField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string TipoFactor
    {
        get
        {
            return this.tipoFactorField;
        }
        set
        {
            this.tipoFactorField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal TasaOCuota
    {
        get
        {
            return this.tasaOCuotaField;
        }
        set
        {
            this.tasaOCuotaField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoInformacionAduanera
{

    private string numeroPedimentoField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string NumeroPedimento
    {
        get
        {
            return this.numeroPedimentoField;
        }
        set
        {
            this.numeroPedimentoField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoCuentaPredial
{

    private string numeroField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Numero
    {
        get
        {
            return this.numeroField;
        }
        set
        {
            this.numeroField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoComplementoConcepto
{

    private System.Xml.XmlElement[] anyField;

    /// <comentarios/>
    [XmlAnyElementAttribute()]
    public System.Xml.XmlElement[] Any
    {
        get
        {
            return this.anyField;
        }
        set
        {
            this.anyField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoParte
{

    private ComprobanteConceptoParteInformacionAduanera[] informacionAduaneraField;

    private string claveProdServField;

    private string noIdentificacionField;

    private decimal cantidadField;

    private string unidadField;

    private string descripcionField;

    private decimal valorUnitarioField;

    private bool valorUnitarioFieldSpecified;

    private decimal importeField;

    private bool importeFieldSpecified;

    /// <comentarios/>
    [XmlElementAttribute("InformacionAduanera")]
    public ComprobanteConceptoParteInformacionAduanera[] InformacionAduanera
    {
        get
        {
            return this.informacionAduaneraField;
        }
        set
        {
            this.informacionAduaneraField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string ClaveProdServ
    {
        get
        {
            return this.claveProdServField;
        }
        set
        {
            this.claveProdServField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string NoIdentificacion
    {
        get
        {
            return this.noIdentificacionField;
        }
        set
        {
            this.noIdentificacionField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Cantidad
    {
        get
        {
            return this.cantidadField;
        }
        set
        {
            this.cantidadField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Unidad
    {
        get
        {
            return this.unidadField;
        }
        set
        {
            this.unidadField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Descripcion
    {
        get
        {
            return this.descripcionField;
        }
        set
        {
            this.descripcionField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal ValorUnitario
    {
        get
        {
            return this.valorUnitarioField;
        }
        set
        {
            this.valorUnitarioField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool ValorUnitarioSpecified
    {
        get
        {
            return this.valorUnitarioFieldSpecified;
        }
        set
        {
            this.valorUnitarioFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool ImporteSpecified
    {
        get
        {
            return this.importeFieldSpecified;
        }
        set
        {
            this.importeFieldSpecified = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ConceptoParteInformacionAduanera
{

    private string numeroPedimentoField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string NumeroPedimento
    {
        get
        {
            return this.numeroPedimentoField;
        }
        set
        {
            this.numeroPedimentoField = value;
        }
    }
}


/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4Impuestos
{

    private ComprobanteImpuestosRetencion[] retencionesField;

    private ComprobanteImpuestosTraslado[] trasladosField;

    private decimal totalImpuestosRetenidosField;

    private bool totalImpuestosRetenidosFieldSpecified;

    private decimal totalImpuestosTrasladadosField;

    private bool totalImpuestosTrasladadosFieldSpecified;

    /// <comentarios/>
    [XmlArrayItemAttribute("Retencion", IsNullable = false)]
    public ComprobanteImpuestosRetencion[] Retenciones
    {
        get
        {
            return this.retencionesField;
        }
        set
        {
            this.retencionesField = value;
        }
    }

    /// <comentarios/>
    [XmlArrayItemAttribute("Traslado", IsNullable = false)]
    public ComprobanteImpuestosTraslado[] Traslados
    {
        get
        {
            return this.trasladosField;
        }
        set
        {
            this.trasladosField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal TotalImpuestosRetenidos
    {
        get
        {
            return this.totalImpuestosRetenidosField;
        }
        set
        {
            this.totalImpuestosRetenidosField = value;
        }
    }
    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal totalImpuestosRetenidos
    {
        get
        {
            return this.totalImpuestosRetenidosField;
        }
        set
        {
            this.totalImpuestosRetenidosField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool TotalImpuestosRetenidosSpecified
    {
        get
        {
            return this.totalImpuestosRetenidosFieldSpecified;
        }
        set
        {
            this.totalImpuestosRetenidosFieldSpecified = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal TotalImpuestosTrasladados
    {
        get
        {
            return this.totalImpuestosTrasladadosField;
        }
        set
        {
            this.totalImpuestosTrasladadosField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal totalImpuestosTrasladados
    {
        get
        {
            return this.totalImpuestosTrasladadosField;
        }
        set
        {
            this.totalImpuestosTrasladadosField = value;
        }
    }

    /// <comentarios/>
    [XmlIgnoreAttribute()]
    public bool TotalImpuestosTrasladadosSpecified
    {
        get
        {
            return this.totalImpuestosTrasladadosFieldSpecified;
        }
        set
        {
            this.totalImpuestosTrasladadosFieldSpecified = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ImpuestosRetencion
{

    private string impuestoField;

    private decimal importeField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4ImpuestosTraslado
{

    private string impuestoField;

    private string tipoFactorField;

    private decimal tasaOCuotaField;

    private decimal importeField;

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string Impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public string TipoFactor
    {
        get
        {
            return this.tipoFactorField;
        }
        set
        {
            this.tipoFactorField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public string impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal TasaOCuota
    {
        get
        {
            return this.tasaOCuotaField;
        }
        set
        {
            this.tasaOCuotaField = value;
        }
    }

    /// <comentarios/>
    [XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <eliminar 3.2/>
    [XmlAttributeAttribute()]
    public decimal importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4Complemento
{

    private System.Xml.XmlElement[] anyField;

    /// <comentarios/>
    [XmlAnyElementAttribute()]
    public System.Xml.XmlElement[] Any
    {
        get
        {
            return this.anyField;
        }
        set
        {
            this.anyField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/4")]
public partial class Comprobantev4Addenda
{

    private System.Xml.XmlElement[] anyField;

    /// <comentarios/>
    [XmlAnyElementAttribute()]
    public System.Xml.XmlElement[] Any
    {
        get
        {
            return this.anyField;
        }
        set
        {
            this.anyField = value;
        }
    }
}



