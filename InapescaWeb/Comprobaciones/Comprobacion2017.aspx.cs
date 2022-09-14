using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InapescaWeb.Comprobaciones
{
    public partial class Comprobacion2017v2 : System.Web.UI.Page
    {
        string lsFolio;
        static clsDictionary Dictionary = new clsDictionary();
        string Ruta;
        string UbicacionFile;
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static string UltimoDia = "31-12-" + Year;

        //empieza Modificacion Pedro
        private static DatosComprobacion oDatosNum = new DatosComprobacion();

        private static bool ActRural = false;
        private static bool ActSingladura = false;
        private static bool ActPeajeNoFacturable = false;
        private static bool Actualizar = false;

        private static string IDMaximo;
        private static string ReferenciaExis;
        private static string TimbreFiscal;
        private static string ticket;
        //load inicial
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                string[] lsCadena = new string[2];
                lsFolio = Request.QueryString["folio"];
                lsCadena = lsFolio.Split(new Char[] { '|' });

                Comision DetalleComision = new Comision();
                if (lsCadena[1] == "0")
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Server.MapPath("~/Comprobaciones/Menu_Comprobaciones.aspx"), true);
                }

                if (Session["Crip_Usuario"] == null)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Server.MapPath("~/index.aspx"), true);
                }

                DetalleComision = MngNegocioComision.Obten_Detalle(lsCadena[0] + ".pdf", Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), lsCadena[1]);
                Session["DetallesComision"] = DetalleComision;

                if (Convert.ToDateTime(DetalleComision.Fecha_Inicio) > Convert.ToDateTime(lsHoy))
                {
                    DetalleComision = null;
                    Response.Redirect("Menu_Comprobaciones.aspx", true);
                }
                else
                {
                    Carga_Detalle(DetalleComision);

                    if (!IsPostBack)
                    {
                        //Contadores en Cero de La Comprobacion
                        oDatosNum = new DatosComprobacion();

                        Carga_Detalle(DetalleComision);
                        if (Session["Crip_Usuario"] == null)
                        {
                            HttpContext.Current.Response.Redirect("index.aspx", true);
                        }
                        //NombreArchivo = lsCadena[0] + ".pdf";
                        //Session["Crip_Estatus"] = lsCadena[1];
                        Carga_Valores(DetalleComision);
                        Crear_Tabla(DetalleComision);
                        clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
                        Valida_Carpeta(DetalleComision.Ruta);
                        Calcula_Reintegro(DetalleComision);
                        comision_informe ccEstatus5 = new comision_informe();

                        ccEstatus5 = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), DetalleComision.Ubicacion_Comisionado, DetalleComision.Oficio, DetalleComision.Dep_Proy, DetalleComision.Periodo, DetalleComision.Proyecto);
                       
                        
                        if (lsCadena[1] == "7" || lsCadena[1] == "5")
                        {
                            //if (lsCadena[1] == "7")
                            //{ 
                            //    clsFuncionesGral.Activa_Paneles (PanelTodo, true, false); 
                            //}
                            LnkDetallesReintegro.Enabled = false;
                            dplFiscales.Enabled = false;
                            if ((ccEstatus5.FOLIO != null) & (clsFuncionesGral.FormatFecha(ccEstatus5.FECHA_FINAL) != Dictionary.FECHA_NULA) || (Session["Crip_Rol"].ToString()) == Dictionary.DIRECTOR_GRAL)
                            {
                                
                                    LnkDetallesReintegro.Enabled = false;
                                    clsFuncionesGral.Activa_Paneles(pnlPdfDescarga, true);

                            }
                            else 
                            {
                                if ((lsCadena[1] == "5"))
                                {
                                    LnkDetallesReintegro.Enabled = true;
                                    clsFuncionesGral.Activa_Paneles(pnlPdfDescarga, false);
                                }
                            
                            }

                            //LinkDescargaMinistracion.InnerText = "DESCARGAR MINISTRACION";

                            DateTime dtFinals = Convert.ToDateTime("2017-09-30");

                            if (Convert.ToDateTime(DetalleComision.Fecha_Final) < dtFinals)
                            {
                                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                                clsPdf.Genera_MinistracionSmaf(DetalleComision, folio_comprobante, 0, 0);
                              //  LinkDescargaMinistracion.HRef = "~/Descarga.aspx?Ministracion=" + DetalleComision.Archivo + "|" + DetalleComision.Ubicacion_Comisionado;
                                LinkDescargaMinistracion.HRef = "~/Descarga.aspx?Ministracion=" + DetalleComision.Archivo + "|" + DetalleComision.Ruta;
                            }
                            else
                            {
                                //LinkDescargaMinistracion.HRef = "~/Descarga.aspx?Ministracion=" + DetalleComision.Archivo + "|" + DetalleComision.Ubicacion_Comisionado;
                                LinkDescargaMinistracion.HRef = "~/Descarga.aspx?Ministracion=" + DetalleComision.Archivo + "|" + DetalleComision.Ruta;
                            }

                        }


                    }


                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }
        //metodo que carga detalle de tabla de recursos otorgados encomision
        public void Carga_Detalle(Comision poComision)
        {
            lblActividadesOb.Text = "";
            limpiaObjetoDatos(1);
            tblDetalle.Rows.Clear();
            double total = clsFuncionesGral.Convert_Double(Dictionary.NUMERO_CERO);
            int consecutivo = 1;
            TableRow tbencabezado = new TableRow();
            TableCell TcNum = new TableCell();
            TcNum.Text = clsFuncionesGral.ConvertMayus("num.");
            TcNum.BackColor = Color.Gray; TcNum.ForeColor = Color.White;
            tbencabezado.Cells.Add(TcNum);
            TableCell TcLugar = new TableCell();
            TcLugar.Text = clsFuncionesGral.ConvertMayus("lugar de la comisión");
            TcLugar.BackColor = Color.Gray; TcLugar.ForeColor = Color.White;
            tbencabezado.Cells.Add(TcLugar);
            TableCell TcPeriodo = new TableCell();
            TcPeriodo.Text = clsFuncionesGral.ConvertMayus("periodo de la comisión");
            TcPeriodo.BackColor = Color.Gray; TcPeriodo.ForeColor = Color.White;
            tbencabezado.Cells.Add(TcPeriodo);
            TableCell TcCuota = new TableCell();
            TcCuota.Text = clsFuncionesGral.ConvertMayus("cuota diaria");
            TcCuota.BackColor = Color.Gray; TcCuota.ForeColor = Color.White;
            tbencabezado.Cells.Add(TcCuota);
            TableCell TcDias = new TableCell();
            TcDias.Text = clsFuncionesGral.ConvertMayus("dias");
            TcDias.BackColor = Color.Gray; TcDias.ForeColor = Color.White;
            tbencabezado.Cells.Add(TcDias);

            //Modificacion Pedro
            string[] psCadena, psCadena2;

            List<Entidad> ListArchivosAmp = new List<Entidad>();
            if (poComision.Archivo_Ampliacion != "")
            {
                if (poComision.Archivo_Ampliacion != null)
                {
                    psCadena = poComision.Archivo_Ampliacion.Split(new Char[] { '|' });
                    psCadena2 = poComision.Oficio_Ampliacion.Split(new Char[] { '|' });
                    for (int i = 0; i < psCadena.Length; i++)
                    {
                        Entidad oEntidad = new Entidad();
                        oEntidad.Codigo = psCadena2[i];
                        oEntidad.Descripcion = psCadena[i];
                        ListArchivosAmp.Add(oEntidad);
                    }
                }
            }
            Session["Ampliaciones"] = null;
            Session["Ampliaciones"] = ListArchivosAmp;
            //Termina Modificacion pedro


            if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
            {
                TableCell TcTotal = new TableCell();
                TcTotal.Text = clsFuncionesGral.ConvertMayus("importe calculado");
                TcTotal.BackColor = Color.Gray; TcTotal.ForeColor = Color.White;
                tbencabezado.Cells.Add(TcTotal);
                tblDetalle.Rows.Add(tbencabezado);
            }
            else
            {
                TableCell TcTotal = new TableCell();
                TcTotal.Text = clsFuncionesGral.ConvertMayus("importe OTORGADO");
                TcTotal.BackColor = Color.Gray; TcTotal.ForeColor = Color.White;
                tbencabezado.Cells.Add(TcTotal);
                tblDetalle.Rows.Add(tbencabezado);
            }

            //internacional Dolares y euros
            if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
            {
                TableRow trUSD = new TableRow();
                TableCell c1USD = new TableCell();
                c1USD.Text = consecutivo.ToString();
                consecutivo += 1;
                trUSD.Cells.Add(c1USD);
                TableCell c2USD = new TableCell();
                c2USD.Text = poComision.Lugar;
                trUSD.Cells.Add(c2USD);
                TableCell c3USD = new TableCell();
                c3USD.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                trUSD.Cells.Add(c3USD);
                //tarifa segun tipo de cambio
                TableCell tc4 = new TableCell();
                TipoCambio tc = new TipoCambio();
                tc = MngNegocioComision.TipoCambio(poComision.Comisionado, poComision.Fecha_Vobo);
                if (tc.Denominacion == "USD")
                {
                    tc4.Text = tc.Denominacion + " $ " + clsFuncionesGral.Convert_Decimales(tc.Tarifa);
                }
                else
                {
                    tc4.Text = tc.Denominacion + " € " + clsFuncionesGral.Convert_Decimales(tc.Tarifa);
                }

                trUSD.Cells.Add(tc4);

                TableCell c5USD = new TableCell();
                c5USD.Text = poComision.Dias_Reales;
                trUSD.Cells.Add(c5USD);
                TableCell tc6 = new TableCell();
                if (tc.Denominacion == "USD")
                {
                    tc6.Text = tc.Denominacion + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(tc.Tarifa) * clsFuncionesGral.Convert_Double(poComision.Dias_Pagar)).ToString());
                }
                else
                {
                    tc6.Text = tc.Denominacion + "  € " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(tc.Tarifa) * clsFuncionesGral.Convert_Double(poComision.Dias_Pagar)).ToString());
                }
                trUSD.Cells.Add(tc6);
                tblDetalle.Rows.Add(trUSD);

                TableRow trUSD1 = new TableRow();
                TableCell c51 = new TableCell();
                c51.ColumnSpan = 5;
                c51.HorizontalAlign = HorizontalAlign.Right;
                c51.Text = clsFuncionesGral.ConvertMayus(" Tipo de Cambio a la Fecha:  ") + clsFuncionesGral.FormatFecha(tc.Fecha);
                trUSD1.Cells.Add(c51);

                TableCell c52 = new TableCell();
                c52.Text = "MX $ " + clsFuncionesGral.Convert_Decimales(tc.Tipo_Cambio);
                trUSD1.Cells.Add(c52);
                tblDetalle.Rows.Add(trUSD1);
                tc = null;
            }
            //Modificacion Pedro Julio 2017
            //zonas simples
            if ((poComision.Zona_Comercial == "0") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "8") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "12") /*| (poComision.Zona_Comercial == "14") | (poComision.Zona_Comercial == "15")*/ | (poComision.Zona_Comercial == "19") | (poComision.Zona_Comercial == "7"))
            {
                string FechaComision = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                double dSumaviat = Convert.ToDouble(poComision.Total_Viaticos) + Convert.ToDouble(poComision.Singladuras);
                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    oDatosNum.DiasDev = Convert.ToDouble(poComision.Dias_Reales);
                    oDatosNum.TotalViaticosDev = Convert.ToDouble(poComision.Total_Viaticos) + Convert.ToDouble(poComision.Singladuras);
                }
                else
                {
                    oDatosNum.DiasAnt = Convert.ToDouble(poComision.Dias_Reales);
                    oDatosNum.TotalViaticosAnt = Convert.ToDouble(poComision.Total_Viaticos);
                }
                double TarifaDias = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa(poComision.Zona_Comercial));
                if ((poComision.Zona_Comercial != "7"))
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, TarifaDias, poComision.Dias_Reales, dSumaviat);
                }
                
                consecutivo += 1;

                if (poComision.Zona_Comercial == "14")
                {
                    oDatosNum.TotalViaticosRurales = Convert.ToDouble(poComision.Total_Viaticos);
                }
                if (poComision.Zona_Comercial == "15")
                {
                    oDatosNum.TotalViaticosNavegados = Convert.ToDouble(poComision.Singladuras);
                }


            }

            //Modificacion Pedro con zonas dobles y triples

            //zonas dobles y triples
            if ((poComision.Zona_Comercial == "16") | (poComision.Zona_Comercial == "17") | (poComision.Zona_Comercial == "18") | (poComision.Zona_Comercial == "20") | (poComision.Zona_Comercial == "21") | (poComision.Zona_Comercial == "22") | (poComision.Zona_Comercial == "23"))
            {

                double TarifaComercial = 0;
                double TarifaRural = 0;
                double TarifaNavegable = 0;
                double Tarifa50km = 0;

                double tViatComercial = 0;
                double tViatRural = 0;
                double tViatNavegados = 0;
                double tViat50km = 0;

                switch (poComision.Zona_Comercial)
                {

                    case "16":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                    case "17":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                    case "18":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        TarifaNavegable = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("15"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatNavegados = Convert.ToDouble(poComision.Dias_Navegados) * TarifaNavegable;
                        oDatosNum.TotalViaticosNavegados = oDatosNum.TotalViaticosNavegados + tViatNavegados;
                        break;
                    case "20":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        break;
                    case "21":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        break;
                    case "22":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                    case "23":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                }

                string FechaComision = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;

                if (tViatComercial != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, TarifaComercial, poComision.Dias_Comercial, tViatComercial);
                    consecutivo += 1;
                }
                if (tViatRural != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, TarifaRural, poComision.Dias_Rural, tViatRural);
                    consecutivo += 1;
                }
                if (tViatNavegados != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, TarifaNavegable, poComision.Dias_Navegados, tViatNavegados);
                    consecutivo += 1;
                }
                if (tViat50km != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, Tarifa50km, poComision.Dias_50, tViat50km);
                    consecutivo += 1;
                }

                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    oDatosNum.DiasDev = Convert.ToDouble(poComision.Dias_Comercial);
                    oDatosNum.TotalViaticosDev = tViatComercial + tViatRural + tViatNavegados + tViat50km;
                }
                else
                {
                    oDatosNum.DiasAnt = Convert.ToDouble(poComision.Dias_Comercial);
                    oDatosNum.TotalViaticosAnt = tViatComercial + tViatRural + tViatNavegados + tViat50km;
                }

            }
            //Termina modificacion pedro zonas dobles y triples

            //Modificacion Pedro TraeApliaciones de comicion
            if (ListArchivosAmp.Count != 0)
            {
                foreach (Entidad r in ListArchivosAmp)
                {
                    Comision DetalleComisionAmp = new Comision();

                    DetalleComisionAmp = MngNegocioComision.Obten_Detalle(r.Descripcion, Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), "9");

                    if (DetalleComisionAmp.Archivo != null)
                    {
                        if ((DetalleComisionAmp.Zona_Comercial == "0") | (DetalleComisionAmp.Zona_Comercial == "2") | (DetalleComisionAmp.Zona_Comercial == "4") | (DetalleComisionAmp.Zona_Comercial == "6") | (DetalleComisionAmp.Zona_Comercial == "8") | (DetalleComisionAmp.Zona_Comercial == "10") | (DetalleComisionAmp.Zona_Comercial == "12") /*| (DetalleComisionAmp.Zona_Comercial == "14") | (DetalleComisionAmp.Zona_Comercial == "15") */| (DetalleComisionAmp.Zona_Comercial == "19"))
                        {
                            string FechaComision = DetalleComisionAmp.Fecha_Inicio + " al " + DetalleComisionAmp.Fecha_Final;
                            double TViaticos = Convert.ToDouble(DetalleComisionAmp.Total_Viaticos);
                            double TarifaDias = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa(DetalleComisionAmp.Zona_Comercial));

                            InsertarTrTable(consecutivo, DetalleComisionAmp.Lugar, FechaComision, TarifaDias, DetalleComisionAmp.Dias_Reales, TViaticos);
                            consecutivo += 1;


                            /*if (DetalleComisionAmp.Zona_Comercial == "14")
                            {
                                oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + Convert.ToDouble(DetalleComisionAmp.Total_Viaticos);
                            }
                            if (DetalleComisionAmp.Zona_Comercial == "15")
                            {
                                oDatosNum.TotalViaticosNavegados = oDatosNum.TotalViaticosNavegados + Convert.ToDouble(DetalleComisionAmp.Singladuras);
                            }*/

                            if ((DetalleComisionAmp.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (DetalleComisionAmp.Forma_Pago_Viaticos == "0"))
                            {
                                oDatosNum.TotalViaticosDev = oDatosNum.TotalViaticosDev + Convert.ToDouble(DetalleComisionAmp.Total_Viaticos) + oDatosNum.TotalViaticosNavegados;
                                oDatosNum.TotalCombAmpDev = oDatosNum.TotalCombAmpDev + Convert.ToDouble(DetalleComisionAmp.Combustible_Efectivo);
                                oDatosNum.TotalPasajeAmpDev = oDatosNum.TotalPasajeAmpDev + Convert.ToDouble(DetalleComisionAmp.Pasaje);
                                oDatosNum.TotalPeajeAmpDev = oDatosNum.TotalPeajeAmpDev + Convert.ToDouble(DetalleComisionAmp.Peaje);
                                oDatosNum.DiasDev = oDatosNum.DiasDev + Convert.ToDouble(DetalleComisionAmp.Dias_Comercial);
                            }
                            else
                            {
                                oDatosNum.DiasAnt = oDatosNum.DiasAnt + Convert.ToDouble(DetalleComisionAmp.Dias_Comercial);
                                oDatosNum.TotalViaticosAnt = oDatosNum.TotalViaticosAnt + Convert.ToDouble(DetalleComisionAmp.Total_Viaticos);
                                oDatosNum.TotalCombAmpAnt = oDatosNum.TotalCombAmpAnt + Convert.ToDouble(DetalleComisionAmp.Combustible_Efectivo);
                                oDatosNum.TotalPasajeAmpAnt = oDatosNum.TotalPasajeAmpAnt + Convert.ToDouble(DetalleComisionAmp.Pasaje);
                                oDatosNum.TotalPeajeAmpAnt = oDatosNum.TotalPeajeAmpAnt + Convert.ToDouble(DetalleComisionAmp.Peaje);
                            }

                        }
                        if ((DetalleComisionAmp.Zona_Comercial == "16") | (DetalleComisionAmp.Zona_Comercial == "17") | (DetalleComisionAmp.Zona_Comercial == "18") | (DetalleComisionAmp.Zona_Comercial == "20") | (DetalleComisionAmp.Zona_Comercial == "21") | (DetalleComisionAmp.Zona_Comercial == "22") | (DetalleComisionAmp.Zona_Comercial == "23"))
                        {

                            double TarifaComercial = 0;
                            double TarifaRural = 0;
                            double TarifaNavegable = 0;
                            double Tarifa50km = 0;

                            double tViatComercial = 0;
                            double tViatRural = 0;
                            double tViatNavegados = 0;
                            double tViat50km = 0;


                            switch (DetalleComisionAmp.Zona_Comercial)
                            {
                                case "16":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                                    break;
                                case "17":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                                    break;
                                case "18":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    TarifaNavegable = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("15"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatNavegados = Convert.ToDouble(DetalleComisionAmp.Dias_Navegados) * TarifaNavegable;
                                    oDatosNum.TotalViaticosNavegados = oDatosNum.TotalViaticosNavegados + tViatNavegados;
                                    break;
                                case "20":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    break;
                                case "21":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    break;
                                case "22":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                                    break;
                                case "23":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                                    break;
                            }

                            string FechaComision = DetalleComisionAmp.Fecha_Inicio + " al " + DetalleComisionAmp.Fecha_Final;

                            if (tViatComercial != 0)
                            {
                                InsertarTrTable(consecutivo, DetalleComisionAmp.Lugar, FechaComision, TarifaComercial, DetalleComisionAmp.Dias_Comercial, tViatComercial);
                                consecutivo += 1;
                            }
                            if (tViatRural != 0)
                            {
                                InsertarTrTable(consecutivo, DetalleComisionAmp.Lugar, FechaComision, TarifaRural, DetalleComisionAmp.Dias_Rural, tViatRural);
                                consecutivo += 1;
                            }
                            if (tViatNavegados != 0)
                            {
                                InsertarTrTable(consecutivo, DetalleComisionAmp.Lugar, FechaComision, TarifaNavegable, DetalleComisionAmp.Dias_Navegados, tViatNavegados);
                                consecutivo += 1;
                            }
                            if (tViat50km != 0)
                            {
                                InsertarTrTable(consecutivo, DetalleComisionAmp.Lugar, FechaComision, Tarifa50km, DetalleComisionAmp.Dias_50, tViat50km);
                                consecutivo += 1;
                            }

                            if ((DetalleComisionAmp.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (DetalleComisionAmp.Forma_Pago_Viaticos == "0"))
                            {
                                oDatosNum.TotalViaticosDev = tViatComercial + tViatRural + tViatNavegados + tViat50km;

                                oDatosNum.TotalCombAmpDev = oDatosNum.TotalCombAmpDev + Convert.ToDouble(DetalleComisionAmp.Combustible_Efectivo);
                                oDatosNum.TotalPasajeAmpDev = oDatosNum.TotalPasajeAmpDev + Convert.ToDouble(DetalleComisionAmp.Pasaje);
                                oDatosNum.TotalPeajeAmpDev = oDatosNum.TotalPeajeAmpDev + Convert.ToDouble(DetalleComisionAmp.Peaje);
                                oDatosNum.DiasDev = oDatosNum.DiasDev + Convert.ToDouble(DetalleComisionAmp.Dias_Comercial);
                            }
                            else
                            {
                                oDatosNum.TotalViaticosAnt = tViatComercial + tViatRural + tViatNavegados + tViat50km;

                                oDatosNum.TotalCombAmpAnt = oDatosNum.TotalCombAmpAnt + Convert.ToDouble(DetalleComisionAmp.Combustible_Efectivo);
                                oDatosNum.TotalPasajeAmpAnt = oDatosNum.TotalPasajeAmpAnt + Convert.ToDouble(DetalleComisionAmp.Pasaje);
                                oDatosNum.TotalPeajeAmpAnt = oDatosNum.TotalPeajeAmpAnt + Convert.ToDouble(DetalleComisionAmp.Peaje);
                                oDatosNum.DiasAnt = oDatosNum.DiasAnt + Convert.ToDouble(DetalleComisionAmp.Dias_Comercial);
                            }
                        }

                    }
                }
            }
            //Termina Modificaciones trae apliacion de comision

            //empieza Actualizacion Pedro Totales

            if ((poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO) || (oDatosNum.TotalCombAmpAnt > 0) || (oDatosNum.TotalCombAmpDev > 0))
            {
                TableRow tr1 = new TableRow();
                TableCell tc11 = new TableCell();
                tc11.ColumnSpan = 5;
                tc11.HorizontalAlign = HorizontalAlign.Right;
                tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                tr1.Cells.Add(tc11);
                TableCell tc12 = new TableCell();
                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    oDatosNum.TotalCombustibleAnt = oDatosNum.TotalCombAmpAnt;
                    oDatosNum.TotalCombustibleDev = Convert.ToDouble(poComision.Combustible_Efectivo) + oDatosNum.TotalCombAmpDev;
                }
                else
                {
                    oDatosNum.TotalCombustibleDev = oDatosNum.TotalCombAmpDev;
                    oDatosNum.TotalCombustibleAnt = Convert.ToDouble(poComision.Combustible_Efectivo) + oDatosNum.TotalCombAmpAnt;
                }

                tc12.Text = (oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                tr1.Cells.Add(tc12);
                tblDetalle.Rows.Add(tr1);
            }

            if ((poComision.Peaje != Dictionary.NUMERO_CERO) | (oDatosNum.TotalPeajeAmpAnt > 0) | (oDatosNum.TotalPeajeAmpDev > 0))
            {
                TableRow tr2 = new TableRow();
                TableCell tc21 = new TableCell();
                tc21.ColumnSpan = 5;
                tc21.HorizontalAlign = HorizontalAlign.Right;
                tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                tr2.Cells.Add(tc21);
                TableCell tc22 = new TableCell();
                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    oDatosNum.TotalPeajeAnt = oDatosNum.TotalPeajeAmpAnt;
                    oDatosNum.TotalPeajeDev = Convert.ToDouble(poComision.Peaje) + oDatosNum.TotalPeajeAmpDev;
                }
                else
                {
                    oDatosNum.TotalPeajeDev = oDatosNum.TotalPeajeAmpDev;
                    oDatosNum.TotalPeajeAnt = Convert.ToDouble(poComision.Peaje) + oDatosNum.TotalPeajeAmpAnt;
                }
                tc22.Text = (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                tr2.Cells.Add(tc22);
                tblDetalle.Rows.Add(tr2);
            }

            if ((poComision.Pasaje != Dictionary.NUMERO_CERO) | (oDatosNum.TotalPasajeAmpAnt > 0) | (oDatosNum.TotalPasajeAmpDev > 0))
            {
                TableRow tr3 = new TableRow();
                TableCell tc31 = new TableCell();
                tc31.ColumnSpan = 5;
                tc31.HorizontalAlign = HorizontalAlign.Right;
                tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                tr3.Cells.Add(tc31);
                TableCell tc32 = new TableCell();
                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    oDatosNum.TotalPasajeAnt = oDatosNum.TotalPasajeAmpAnt;
                    oDatosNum.TotalPasajeDev = Convert.ToDouble(poComision.Pasaje) + oDatosNum.TotalPasajeAmpDev;
                }
                else
                {
                    oDatosNum.TotalPasajeDev = oDatosNum.TotalPasajeAmpDev;
                    oDatosNum.TotalPasajeAnt = Convert.ToDouble(poComision.Pasaje) + oDatosNum.TotalPasajeAmpAnt;
                }

                tc32.Text = (oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                tr3.Cells.Add(tc32);
                tblDetalle.Rows.Add(tr3);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            oDatosNum.TotalTotalDev = oDatosNum.TotalViaticosDev + oDatosNum.TotalCombustibleDev + oDatosNum.TotalPasajeDev + oDatosNum.TotalPeajeDev;
            oDatosNum.TotalTotalAnt = oDatosNum.TotalViaticosAnt + oDatosNum.TotalCombustibleAnt + oDatosNum.TotalPasajeAnt + oDatosNum.TotalPeajeAnt;
            oDatosNum.GranTotal = oDatosNum.TotalTotalDev + oDatosNum.TotalTotalAnt;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (oDatosNum.TotalTotalDev > 0 && oDatosNum.TotalTotalAnt > 0)
            {
                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
                {
                    tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total calculado en moneda nacional: ");
                }
                else
                {
                    tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total calculado: ");
                }
                tr5.Cells.Add(tc51);
                TableCell tc52 = new TableCell();
                tc52.Text = oDatosNum.TotalTotalDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                tr5.Cells.Add(tc52);
                tblDetalle.Rows.Add(tr5);

                tr5 = new TableRow();
                tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
                {
                    tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado en moneda nacional: ");
                }
                else
                {
                    tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado: ");
                }
                tr5.Cells.Add(tc51);
                tc52 = new TableCell();
                tc52.Text = oDatosNum.TotalTotalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                tr5.Cells.Add(tc52);
                tblDetalle.Rows.Add(tr5);

                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de:");
                tr4.Cells.Add(tc41);
                tblDetalle.Rows.Add(tr4);
                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();

                tc61.Text = oDatosNum.TotalTotalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                tr6.Cells.Add(tc61);
                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;

                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(oDatosNum.TotalTotalAnt.ToString()), true);

                tr6.Cells.Add(tc62);
                tblDetalle.Rows.Add(tr6);

                if (poComision.Combustible_Vales != Dictionary.NUMERO_CERO)
                {
                    TableRow tr7 = new TableRow();
                    TableCell tc72 = new TableCell();
                    tc72.ColumnSpan = 6;
                    tc72.HorizontalAlign = HorizontalAlign.Center;
                    tc72.Text = "Se debe comprobar de forma fisica y con ticket de carga de gasolina la cantidad de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio: " + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                    tr7.Cells.Add(tc72);
                    tblDetalle.Rows.Add(tr7);
                }

            }
            else
            {
                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;

                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
                    {
                        tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total calculado en moneda nacional: ");
                    }
                    else
                    {
                        tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total calculado: ");
                    }
                }
                else
                {
                    if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
                    {
                        tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado en moneda nacional: ");
                    }
                    else
                    {
                        tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado: ");
                    }
                }

                tr5.Cells.Add(tc51);
                TableCell tc52 = new TableCell();
                tc52.Text = oDatosNum.GranTotal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                tr5.Cells.Add(tc52);
                tblDetalle.Rows.Add(tr5);
                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de:");
                tr4.Cells.Add(tc41);
                tblDetalle.Rows.Add(tr4);
                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();

                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO);
                }
                else
                {
                    tc61.Text = oDatosNum.GranTotal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                }
                tr6.Cells.Add(tc61);
                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    tc62.Text = clsFuncionesGral.ConvertMayus("    d   e   v   e   n   g   a   d   o   s ");
                }
                else
                {
                    tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(oDatosNum.GranTotal.ToString()), true);
                }
                tr6.Cells.Add(tc62);
                tblDetalle.Rows.Add(tr6);
                if (poComision.Combustible_Vales != Dictionary.NUMERO_CERO)
                {
                    TableRow tr7 = new TableRow();
                    TableCell tc72 = new TableCell();
                    tc72.ColumnSpan = 6;
                    tc72.HorizontalAlign = HorizontalAlign.Center;
                    tc72.Text = "Se debe comprobar de forma fisica y con ticket de carga de gasolina la cantidad de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio: " + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                    tr7.Cells.Add(tc72);
                    tblDetalle.Rows.Add(tr7);
                }

            }
            //termina tabla detalle
            //validacion de anticipado devengado para leyenda 
        }
        //CALCULAR REINTEGRO
        public void Calcula_Reintegro(Comision poComision)
        {
            limpiaObjetoDatos(2);
            bool ActNoFiscal = false;
            clsFuncionesGral.Activa_Paneles(pnlInternacional, false);
            clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
            clsFuncionesGral.Activa_Paneles(pnlComprob, false);
            clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, false);
            clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
            clsFuncionesGral.Activa_Paneles(pnlpdf, false);
            //RECORRE LISTA COMPROBACIONES FISCALES
            if (Session["DatosFiscales"] != null)
            {
                List<Comisio_Comprobacion> ListaComprobacion = new List<Comisio_Comprobacion>();
                ListaComprobacion = (List<Comisio_Comprobacion>)Session["DatosFiscales"];
                if (ListaComprobacion.Count != 0)
                {
                    foreach (Comisio_Comprobacion Rec in ListaComprobacion)
                    {
                        if ((Rec.NUM_CONCEP == "5") || (Rec.NUM_CONCEP == "9") || (Rec.NUM_CONCEP == "11") || (Rec.NUM_CONCEP == "14") || (Rec.NUM_CONCEP == "15") || (Rec.NUM_CONCEP == "17") || (Rec.NUM_CONCEP == "12") || (Rec.NUM_CONCEP == "20"))
                        {
                            if (Rec.METODO_PAGO_USUARIO == "28")
                            {
                                oDatosNum.TotalComprobadoTarjeta += Convert.ToDouble(Rec.IMPORTE.Replace("$", ""));
                            }
                            if ((Rec.NUM_CONCEP == "9") || (Rec.NUM_CONCEP == "15"))
                            {
                                oDatosNum.TotalCompHospedaje = oDatosNum.TotalCompHospedaje + Convert.ToDouble(Rec.IMPORTE.Replace("$", ""));
                            }
                            oDatosNum.TotalComprobadoViaticos = oDatosNum.TotalComprobadoViaticos + Convert.ToDouble(Rec.IMPORTE.Replace("$", ""));
                        }
                        //if ((Rec.NUM_CONCEP == "7") || (Rec.NUM_CONCEP == "16"))
                        if (Rec.NUM_CONCEP == "7")
                        {
                            oDatosNum.TotalComprobadoPeaje = oDatosNum.TotalComprobadoPeaje + Convert.ToDouble(Rec.IMPORTE.Replace("$", ""));
                        }
                        if (Rec.NUM_CONCEP == "6")
                        {
                            oDatosNum.TotalComprobadoCombustible = oDatosNum.TotalComprobadoCombustible + Convert.ToDouble(Rec.IMPORTE.Replace("$", ""));
                        }
                        if (Rec.NUM_CONCEP == "8")
                        {
                            oDatosNum.TotalComprobadoPasaje = oDatosNum.TotalComprobadoPasaje + Convert.ToDouble(Rec.IMPORTE.Replace("$", ""));
                        }
                    }
                    ListaComprobacion = null;
                    clsFuncionesGral.Activa_Paneles(pnlComprob, true);
                }
            }
            //RECORRE LISTA COMPROBACIONES NO FISCALES
            if (Session["DatosNoFiscales"] != null)
            {
                List<Comisio_Comprobacion> ListaComprobNofiscal = new List<Comisio_Comprobacion>();
                ListaComprobNofiscal = (List<Comisio_Comprobacion>)Session["DatosNoFiscales"];
                if (ListaComprobNofiscal.Count != 0)
                {
                    foreach (Comisio_Comprobacion rNofis in ListaComprobNofiscal)
                    {
                        if (rNofis.NUM_CONCEP == "16")
                        {
                            oDatosNum.TotalCompPeajeNoFact = oDatosNum.TotalCompPeajeNoFact + Convert.ToDouble(rNofis.IMPORTE.Replace("$", ""));
                        }
                        if (rNofis.NUM_CONCEP == "12")
                        {
                            oDatosNum.TotalComprobadoNoFiscal = oDatosNum.TotalComprobadoNoFiscal + Convert.ToDouble(rNofis.IMPORTE.Replace("$", ""));
                        }
                        if (rNofis.NUM_CONCEP == "19")
                        {
                            oDatosNum.TotalCompSingladura = oDatosNum.TotalCompSingladura + Convert.ToDouble(rNofis.IMPORTE.Replace("$", ""));
                        }
                        if (rNofis.NUM_CONCEP == "18")
                        {
                            oDatosNum.TotalCompRural = oDatosNum.TotalCompRural + Convert.ToDouble(rNofis.IMPORTE.Replace("$", ""));
                        }
                        if (rNofis.NUM_CONCEP == "13")
                        {
                            oDatosNum.TotalCompReintegro = oDatosNum.TotalCompReintegro + Convert.ToDouble(rNofis.IMPORTE.Replace("$", ""));
                        }
                    }

                    ListaComprobNofiscal = null;
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, true);
                }

            }
            //RECORRE LISTA COMPROBACIONES INTERNACIONALES
            if (Session["DatosInternacional"] != null)
            {
                List<Comisio_Comprobacion> ListaComprobInternacional = new List<Comisio_Comprobacion>();
                ListaComprobInternacional = (List<Comisio_Comprobacion>)Session["DatosInternacional"];
                if (ListaComprobInternacional != null)
                {
                    foreach (Comisio_Comprobacion rInt in ListaComprobInternacional)
                    {
                        oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + Convert.ToDouble(rInt.IMPORTE.Replace("$", ""));
                    }
                    clsFuncionesGral.Activa_Paneles(pnlInternacional, true);
                }
                ListaComprobInternacional = null;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////centrarce para comprobaciones ant ext dev

            if ((oDatosNum.TotalTotalDev > 0) && (oDatosNum.TotalTotalAnt > 0))
            {
                double dGastoHospAnt=0;
                double dGastoHospDev = 0;
                //modoficar para obtener 20 y 10
                if (oDatosNum.TotalCompHospedaje > 0)
                {
                    double DiasTotales = Math.Truncate(oDatosNum.DiasAnt) + Math.Truncate(oDatosNum.DiasDev);
                    dGastoHospDev = Math.Round(((oDatosNum.TotalCompHospedaje / DiasTotales) * Math.Truncate(oDatosNum.DiasDev)), 2);
                    dGastoHospAnt = Math.Round(((oDatosNum.TotalCompHospedaje / DiasTotales) * Math.Truncate(oDatosNum.DiasAnt)), 2);
 
                }
                double TotalCompViaticosSinHospDev = 0.00;
                TotalCompViaticosSinHospDev = oDatosNum.TotalComprobadoViaticos - dGastoHospDev;
                if (TotalCompViaticosSinHospDev >= oDatosNum.TotalViaticosAnt)
                {
                    oDatosNum.TotalComprobadoViaticosAnt = oDatosNum.TotalViaticosAnt;
                    //double TotalCompViaticosSinHospAnt = oDatosNum.TotalComprobadoViaticos - dGastoHospAnt;
                    //double TotalCompViaticosSinHospAnt2 = ((oDatosNum.TotalComprobadoViaticos - oDatosNum.TotalCompHospedaje) + dGastoHospDev);
                    oDatosNum.TotalComprobadoViaticosDev = oDatosNum.TotalComprobadoViaticos - oDatosNum.TotalViaticosAnt;
                }
                else
                {
                    oDatosNum.TotalComprobadoViaticosAnt = (oDatosNum.TotalComprobadoViaticos - oDatosNum.TotalCompHospedaje) + dGastoHospAnt;
                    oDatosNum.TotalComprobadoViaticosDev = dGastoHospDev;
                }
                ////////comprobacion no fiscal
                if (oDatosNum.TotalComprobadoNoFiscal > 0)
                {
                    Label19.Text = clsFuncionesGral.ConvertMayus("USTED YA CUENTA CON UN COMPROBACION DE NO FISCALES EN SU COMISION, SI ES INCORRECTA FAVOR DE ELIMINAR LA COMPROBACION PARA HACER EL CALCULO NUEVAMENTE");
                    Carga_Lista_Fiscal(false);
                }
                else
                {
                    if (oDatosNum.TotalComprobadoTarjeta >= Math.Round((oDatosNum.TotalViaticosAnt * .80), 2))
                    {
                        Proyecto objProyecto = new Proyecto();
                        objProyecto = MngNegocioProyecto.ObtieneDatosProy(poComision.Dep_Proy, poComision.Proyecto, poComision.Periodo);
                        Programa objPrograma = new Programa();
                        objPrograma = MngNegociosProgramas.Obtiene_Datos_Programa(objProyecto.Programa, objProyecto.Direccion);
                        if (objProyecto.Componente == "4KS" && objProyecto.Dependencia==Dictionary.DGAIA && objPrograma.Tipo=="3")
                        {
                            Carga_Lista_Fiscal(false);
                        }
                        else 
                        {
                            oDatosNum.MaxNofiscalAnt = Math.Round(oDatosNum.TotalViaticosAnt * .20, 2);
                            Carga_Lista_Fiscal(true);
                            Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 20% de total COMPROBADO EN LA PARTIDA DE VIATiCOS NO MAYOR A:" + oDatosNum.MaxNofiscalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")));

                        }
                        
                        
                    }
                    else if (oDatosNum.TotalComprobadoViaticosDev <= 0)
                    {
                        Label19.Text = clsFuncionesGral.ConvertMayus("PARA EL DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES ES NECESARIO CUMPLIR CON EL 20% POR METODO PAGO TARJETA, USTED CUENTA CON UN ");
                        Carga_Lista_Fiscal(false);
                    }
                    else
                    {
                        ActNoFiscal = true;
                        Carga_Lista_Fiscal(true);
                        Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total COMPROBADO EN LA PARTIDA DE VIATiCOS NO MAYOR A:" + oDatosNum.MaxNofiscalDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")));

                    }
                }
                ////TERMINA comprobacion no fiscal

                double dTCPeA = (oDatosNum.TotalComprobadoPeaje + oDatosNum.TotalCompPeajeNoFact) - oDatosNum.TotalPeajeAnt;
                if (dTCPeA > 0)
                {
                    oDatosNum.TotalComprobadoPeajeAnt = oDatosNum.TotalPeajeAnt;
                    oDatosNum.TotalComprobadoPeajeDev = dTCPeA;
                }
                else
                {
                    oDatosNum.TotalComprobadoPeajeAnt = (oDatosNum.TotalComprobadoPeaje + oDatosNum.TotalCompPeajeNoFact);
                }

                double dTCPaA = oDatosNum.TotalComprobadoPasaje - oDatosNum.TotalPasajeAnt;
                if (dTCPaA > 0)
                {
                    oDatosNum.TotalComprobadoPasajeAnt = oDatosNum.TotalPasajeAnt;
                    oDatosNum.TotalComprobadoPasajeDev = dTCPaA;
                }
                else
                {
                    oDatosNum.TotalComprobadoPasajeAnt = oDatosNum.TotalComprobadoPasaje;
                }

                double dTCCoA = oDatosNum.TotalComprobadoCombustible - oDatosNum.TotalCombustibleAnt;
                if (dTCCoA > 0)
                {
                    oDatosNum.TotalComprobadoCombustibleAnt = oDatosNum.TotalCombustibleAnt;
                    oDatosNum.TotalComprobadoCombustibleDev = dTCCoA;
                }
                else
                {
                    oDatosNum.TotalComprobadoCombustibleAnt = oDatosNum.TotalComprobadoCombustible;
                }
                if (ActNoFiscal == true)
                {
                    oDatosNum.MaxNofiscalDev = Math.Round(oDatosNum.TotalComprobadoViaticosDev * .10, 2);
                    Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total COMPROBADO EN LA PARTIDA DE VIATiCOS NO MAYOR A: " + oDatosNum.MaxNofiscalDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")));
                }
                ////agregar el no fiscal
                if (Math.Round((oDatosNum.TotalComprobadoViaticosDev * .10), 2) == oDatosNum.TotalComprobadoNoFiscal)
                {
                    oDatosNum.TotalComprobadoViaticosDev += oDatosNum.TotalComprobadoNoFiscal;
                }
                else
                {
                    oDatosNum.TotalComprobadoViaticosAnt += oDatosNum.TotalComprobadoNoFiscal;
                }
                oDatosNum.TotalComprobadoAnt = oDatosNum.TotalComprobadoViaticosAnt + oDatosNum.TotalComprobadoPeajeAnt + oDatosNum.TotalComprobadoPasajeAnt + oDatosNum.TotalComprobadoCombustibleAnt;
                oDatosNum.TotalComprobadoDev = oDatosNum.TotalComprobadoViaticosDev + oDatosNum.TotalComprobadoPeajeDev + oDatosNum.TotalComprobadoPasajeDev + oDatosNum.TotalComprobadoCombustibleDev;


                oDatosNum.TotalComprobado = (oDatosNum.TotalComprobadoAnt + (oDatosNum.TotalCompRural + oDatosNum.TotalCompSingladura + oDatosNum.TotalCompReintegro));

                Label9.Text = clsFuncionesGral.ConvertMayus("Reintegro a efectuar:");

                double tReintegro = (oDatosNum.TotalTotalAnt - oDatosNum.TotalComprobado);

                if (tReintegro > 0)
                {
                    oDatosNum.TotalImporteReintegro = tReintegro;
                    oDatosNum.TotalComprobadoDev = oDatosNum.TotalComprobadoDev;
                    lblMonto.Text = oDatosNum.TotalImporteReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                }
                else
                {
                    oDatosNum.TotalImporteReintegro = 0;
                    oDatosNum.TotalComprobadoDev = oDatosNum.TotalComprobadoDev;
                    lblMonto.Text = oDatosNum.TotalImporteReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                }
            //double dTResRyD = oDatosNum.TotalComprobadoDev - tReintegro;

            //if (dTResRyD > 0)
            //{
            //    oDatosNum.TotalImporteReintegro = 0;
            //    oDatosNum.TotalComprobadoDev = dTResRyD;
            //    lblMonto.Text = oDatosNum.TotalImporteReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            //}
            //else
            //{
            //    oDatosNum.TotalImporteReintegro = tReintegro + oDatosNum.TotalComprobadoDev;
            //    oDatosNum.TotalComprobadoDev = 0;
            //    lblMonto.Text = oDatosNum.TotalImporteReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            //}

            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////termina centrarce para comprobaciones ant ext dev
            else
            {
                //aqui va la operacion para obtener reintegro
                double SumaViaticos = (oDatosNum.TotalViaticosAnt + oDatosNum.TotalViaticosDev) - oDatosNum.TotalComprobadoViaticos;
                if (SumaViaticos < 0)
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + (oDatosNum.TotalViaticosAnt + oDatosNum.TotalViaticosDev);
                }
                else
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + oDatosNum.TotalComprobadoViaticos;
                }
                double SumaPeaje = (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev) - (oDatosNum.TotalComprobadoPeaje + oDatosNum.TotalCompPeajeNoFact);
                if (SumaPeaje < 0)
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev);
                }
                else
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + (oDatosNum.TotalComprobadoPeaje + oDatosNum.TotalCompPeajeNoFact);
                }
                double SumaPasaje = (oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev) - oDatosNum.TotalComprobadoPasaje;
                if (SumaPasaje < 0)
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + (oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev);
                }
                else
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + oDatosNum.TotalComprobadoPasaje;
                }
                double SumaCombustible = (oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev) - oDatosNum.TotalComprobadoCombustible;
                if (SumaCombustible < 0)
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + (oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev);
                }
                else
                {
                    oDatosNum.TotalComprobado = oDatosNum.TotalComprobado + oDatosNum.TotalComprobadoCombustible;
                }

                oDatosNum.TotalComprobado = (oDatosNum.TotalComprobado + (oDatosNum.TotalCompRural + oDatosNum.TotalCompSingladura + oDatosNum.TotalComprobadoNoFiscal + oDatosNum.TotalCompReintegro));

                //COMPARACION DE FORMA DE VIATICOS y calculos para fiscales y no fiscales
                if ((poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS) || (poComision.Forma_Pago_Viaticos == "0"))
                {
                    if ((poComision.Zona_Comercial == "15") || (poComision.Zona_Comercial == "14"))
                    {
                        Carga_Lista_Fiscal(true);
                    }
                    else 
                    {
                        Carga_Lista_Fiscal(false);
                    }
                    

                    Label9.Text = clsFuncionesGral.ConvertMayus("Monto Comprobado a la fecha: ");
                    lblMonto.Text = oDatosNum.TotalComprobado.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    //if (oDatosNum.TotalComprobadoViaticos < oDatosNum.TotalViaticosDev)
                    //{
                    //    Carga_Lista_Fiscal(true);
                    //    oDatosNum.MaxNofiscalDev = oDatosNum.TotalComprobadoViaticos * 0.10;
                    //    //metodo sacar total de no fiscales anual
                    //    Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total COMPROBADO EN LA PARTIDA DE VIATiCOS NO MAYOR A:" + oDatosNum.MaxNofiscalDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")));

                    //}
                    //else if (oDatosNum.TotalComprobadoViaticos > oDatosNum.TotalViaticosDev)
                    //{
                    //    Carga_Lista_Fiscal(true);
                    //    oDatosNum.MaxNofiscalAnt = oDatosNum.TotalViaticosDev * 0.10;
                    //    //metodo sacar total de no fiscales anual
                    //    Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total COMPROBADO EN LA PARTIDA DE VIATiCOS NO MAYOR A:" + oDatosNum.MaxNofiscalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")));
                    //}
                    //else
                    //{
                    //    if (oDatosNum.TotalPeajeDev > 0)
                    //    {
                    //        Carga_Lista_Fiscal(true);
                    //    }
                    //    else
                    //    {
                    //        Carga_Lista_Fiscal(false);
                    //    }
                    //}
                }
                else if (poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_ANTICIPADOS)
                {
                    Label9.Text = clsFuncionesGral.ConvertMayus("Reintegro a efectuar:");

                    double tReintegro = oDatosNum.TotalTotalAnt - oDatosNum.TotalComprobado;

                    double dNumCero = 0;
                    if (tReintegro < 0)
                    {
                        oDatosNum.TotalImporteReintegro = 0;
                        lblMonto.Text = dNumCero.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    else
                    {
                        oDatosNum.TotalImporteReintegro = tReintegro;
                        lblMonto.Text = oDatosNum.TotalImporteReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }

                    if (oDatosNum.TotalComprobadoTarjeta >= (oDatosNum.TotalViaticosAnt * .80))
                    {
                        Proyecto objProyecto = new Proyecto();
                        objProyecto = MngNegocioProyecto.ObtieneDatosProy(poComision.Dep_Proy, poComision.Proyecto, poComision.Periodo);
                        Programa objPrograma = new Programa();
                        objPrograma = MngNegociosProgramas.Obtiene_Datos_Programa(objProyecto.Programa, objProyecto.Direccion);
                        if (objProyecto.Componente == "4KS" && objProyecto.Dependencia == Dictionary.DGAIA && objPrograma.Tipo == "3")
                        {
                            Carga_Lista_Fiscal(false);
                        }
                        else
                        {
                            oDatosNum.MaxNofiscalAnt = oDatosNum.TotalViaticosAnt - oDatosNum.TotalComprobadoViaticos;
                            if (oDatosNum.MaxNofiscalAnt > 0)
                            {
                                Carga_Lista_Fiscal(true);
                                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 20% de total COMPROBADO EN LA PARTIDA DE VIATiCOS NO MAYOR A:" + oDatosNum.MaxNofiscalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")));
                            }//metodo sacar total de no fiscales anual
                            else
                            {
                                Label19.Text = clsFuncionesGral.ConvertMayus("USTED YA SUPERO LA COMPROBACION DE SU COMISION ASIGNADA, POR LO CUAL YA NO CUENTA CON SU DERECHO AL 20%");
                                Carga_Lista_Fiscal(false);
                            }
                            
                        }
                    }
                    else
                    {
                        Label19.Text = clsFuncionesGral.ConvertMayus("PARA EL DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES ES NECESARIO CUMPLIR CON EL 80% POR METODO PAGO TARJETA");
                        if ((oDatosNum.TotalPeajeAnt > 0) || (oDatosNum.TotalViaticosRurales > 0))
                        {
                            Carga_Lista_Fiscal(true);
                        }
                        else
                        {
                            Carga_Lista_Fiscal(false);
                        }
                        //mandar mensaje de no cumple con el 80% comprobado con tarjeta
                    }
                }
            }

        }
        //Reutilizar Objeto
        private void limpiaObjetoDatos(int iValor)
        {
            if (iValor == 1)
            {
                oDatosNum.GranTotal = 0;
                oDatosNum.TotalViaticosRurales = 0;
                oDatosNum.TotalViaticosNavegados = 0;

                oDatosNum.DiasAnt = 0;
                oDatosNum.TotalCombAmpAnt = 0;
                oDatosNum.TotalPeajeAmpAnt = 0;
                oDatosNum.TotalPasajeAmpAnt = 0;
                oDatosNum.TotalViaticosAnt = 0;
                oDatosNum.TotalCombustibleAnt = 0;
                oDatosNum.TotalPasajeAnt = 0;
                oDatosNum.TotalPeajeAnt = 0;
                oDatosNum.TotalTotalAnt = 0;

                oDatosNum.DiasDev = 0;
                oDatosNum.TotalCombAmpDev = 0;
                oDatosNum.TotalPeajeAmpDev = 0;
                oDatosNum.TotalPasajeAmpDev = 0;
                oDatosNum.TotalViaticosDev = 0;
                oDatosNum.TotalCombustibleDev = 0;
                oDatosNum.TotalPasajeDev = 0;
                oDatosNum.TotalPeajeDev = 0;
                oDatosNum.TotalTotalDev = 0;
            }
            if (iValor == 2)
            {


                oDatosNum.TotalImporteReintegro = 0;
                oDatosNum.TotalComprobadoTarjeta = 0;
                oDatosNum.TotalCompHospedaje = 0;
                oDatosNum.MaxNofiscalAnt = 0;
                oDatosNum.MaxNofiscalDev = 0;
                oDatosNum.TotalComprobado = 0;
                oDatosNum.TotalComprobadoViaticos = 0;
                oDatosNum.TotalComprobadoCombustible = 0;
                oDatosNum.TotalComprobadoPasaje = 0;
                oDatosNum.TotalComprobadoPeaje = 0;
                oDatosNum.TotalComprobadoNoFiscal = 0;
                oDatosNum.TotalCompPeajeNoFact = 0;
                oDatosNum.TotalCompSingladura = 0;
                oDatosNum.TotalCompRural = 0;
                oDatosNum.TotalCompReintegro = 0;

                oDatosNum.TotalComprobadoViaticosAnt = 0;
                oDatosNum.TotalComprobadoCombustibleAnt = 0;
                oDatosNum.TotalComprobadoPasajeAnt = 0;
                oDatosNum.TotalComprobadoPeajeAnt = 0;

                oDatosNum.TotalComprobadoViaticosDev = 0;
                oDatosNum.TotalComprobadoCombustibleDev = 0;
                oDatosNum.TotalComprobadoPasajeDev = 0;
                oDatosNum.TotalComprobadoPeajeDev = 0;
            }
        }
        //Carga DropdownList Fiscales
        public void Carga_Lista_Fiscal(bool opcion)
        {
            clsFuncionesGral.Activa_Paneles(pnlFiscales, true);
            //Modiicacion
            dplFiscales.Items.Clear();
            dplFiscales.Items.Add(new ListItem(" = S E L E C C I O N E = ", "1"));
            double suma = ((oDatosNum.TotalViaticosAnt + oDatosNum.TotalCombustibleAnt + oDatosNum.TotalPasajeAnt + oDatosNum.TotalPeajeAnt + oDatosNum.TotalViaticosDev + oDatosNum.TotalCombustibleDev + oDatosNum.TotalPasajeDev + oDatosNum.TotalPeajeDev) - (oDatosNum.TotalViaticosNavegados + oDatosNum.TotalViaticosRurales));
            if (suma > 0)
            {
                dplFiscales.Items.Add(new ListItem("COMPROBANTES FISCALES", "2"));
            }
            if (opcion == true)
            {
                dplFiscales.Items.Add(new ListItem("COMPROBANTES NO FISCALES", "3"));
            }

            dplFiscales.AppendDataBoundItems = true;
            dplFiscales.DataBind();

            //TERMINA MODIFICACION PEDRO

        }
        
        //Carga DropdownList  NO  Fiscales
        public void Carga_Lista_Conceptos()
        {
            //Modiicacion
            dplConcepto.Items.Clear();
            dplConcepto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "1"));
            if ((oDatosNum.TotalViaticosAnt + oDatosNum.TotalViaticosDev) != 0)
            {
                if (((oDatosNum.TotalViaticosAnt + oDatosNum.TotalViaticosDev) - (oDatosNum.TotalViaticosNavegados + oDatosNum.TotalViaticosRurales)) > 0)
                {
                    dplConcepto.Items.Add(new ListItem("ALIMENTACION", "5"));
                    dplConcepto.Items.Add(new ListItem("HOSPEDAJE", "9"));
                    dplConcepto.Items.Add(new ListItem("TAXI (DENTRO DE VIATICOS)", "11"));
                    dplConcepto.Items.Add(new ListItem("ALIMENTACION Y HOSPEDAJE", "15"));
                   
                    //dplConcepto.Items.Add(new ListItem("TELEFONIA", "14"));
                   
                }

            }
            if ((oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev) != 0)
            {
                dplConcepto.Items.Add(new ListItem("COMBUSTIBLE", "6"));

            }
            if ((oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev) != 0)
            {
                dplConcepto.Items.Add(new ListItem("PEAJE", "7"));
                dplConcepto.Items.Add(new ListItem("PEAJE NO FACTURABLE", "16"));

            }
            if ((oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev) != 0)
            {
                dplConcepto.Items.Add(new ListItem("PASAJE", "8"));
            }


            dplConcepto.AppendDataBoundItems = true;
            dplConcepto.DataBind();
            //TERMINA MODIFICACION PEDRO
        }

        //Metodo añade datos a tabla detalles
        private void InsertarTrTable(int consecutivo, string ComisionLugar, string Fecha, double Tarifa, string Dias, double Total)
        {
            TableRow TableTr = new TableRow();
            TableCell TableTd = new TableCell();

            TableTr = new TableRow();
            TableTd = new TableCell();
            TableTd.Text = consecutivo.ToString();

            TableTr.Cells.Add(TableTd);
            TableTd = new TableCell();
            TableTd.Text = ComisionLugar;
            TableTr.Cells.Add(TableTd);
            TableTd = new TableCell();
            TableTd.Text = Fecha;
            TableTr.Cells.Add(TableTd);
            TableTd = new TableCell();
            TableTd.Text = Tarifa.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            TableTr.Cells.Add(TableTd);
            TableTd = new TableCell();
            TableTd.Text = Dias;
            TableTr.Cells.Add(TableTd);
            TableTd = new TableCell();
            TableTd.Text = Total.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
            TableTr.Cells.Add(TableTd);
            tblDetalle.Rows.Add(TableTr);
        }
        //metodo que carga valores iniciales
        public void Carga_Valores(Comision poComision)
        {
            ActRural = false;
            ActSingladura = false;
            ActPeajeNoFacturable = false;
            Actualizar = false;

            TbCompEnDev.Enabled = false;
            divComprDev.Visible = false;
            clsFuncionesGral.Activa_Paneles(pnlPdfDescarga, false);
            LinkDescarga.Visible = false;
            lblMensajeNoFac.ForeColor = Color.Black;
            clsFuncionesGral.Activa_Paneles(pnlInternacional, false);
            clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
            clsFuncionesGral.Activa_Paneles(pnlComprob, false);
            //clsFuncionesGral.Activa_Paneles(pnlReferencia, false);
            clsFuncionesGral.Activa_Paneles(pnlCerraDevengado, false);
            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, false);
            clsFuncionesGral.Activa_Paneles(pnlFiscales, false);
            clsFuncionesGral.Activa_Paneles(pnlpdf, false);
            clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
            clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
            clsFuncionesGral.Activa_Paneles(pnlMetodoPago, false);
            clsFuncionesGral.Activa_Paneles(Pnlticket2, false);



            string declaracion = "Se hace de su conocimiento que las comprobaciones serán validadas por el área financiera  con respaldo de los estados de cuenta débito,";
            declaracion += "tickets,  en caso de infringir en falsedad de datos con respecto a su comprobación.";
            declaracion += " El área correspondiente será la encargada de  tomar las medidas necesarias, conforme al artículo 93,";
            declaracion += " Fracc. XVII. Podrán no presentar comprobantes fiscales hasta por un 20% del total de viáticos";
            declaracion += " erogados cuando no existan servicios para emitir los mismos, sin que en ningún caso el monto que no se compruebe ";
            declaracion += "exceda de $15,000.00 en el ejercicio fiscal de que se trate siempre que el monto restante de los viáticos se eroguen mediante tarjeta de crédito, de débito ";
            declaracion += "o de servicio del patrón. La parte que en su caso no se erogue deberá ser reintegrada por la persona física que reciba los viáticos o en caso contrario no le será aplicable lo dispuesto en este artículo.";

            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            Label1.Text = clsFuncionesGral.ConvertMayus("Nombre del servidor público comisionado:");
            lblNombres.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            Label2.Text = clsFuncionesGral.ConvertMayus("Objeto de la comision : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Principales actividades desarrolladas :");
            lblObjetivo.Text = clsFuncionesGral.ConvertMayus(poComision.Objetivo);
            Label4.Text = clsFuncionesGral.ConvertMayus("Evaluacion :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Comprobación de comisión oficio número :") + poComision.Archivo.Replace(".pdf", "");
            Label13.Text = clsFuncionesGral.ConvertMayus("CONCEPTO comprobante :");
            Label11.Text = clsFuncionesGral.ConvertMayus("relación de gastos");
            Label12.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION CON REQUISITOS FISCALES");
            //Label10.Text = clsFuncionesGral.ConvertMayus("referencia bancaria  para reintegro");
            //Label14.Text = clsFuncionesGral.ConvertMayus("Referencia Bancaria :");
            Label18.Text = clsFuncionesGral.ConvertMayus("concepto: ");
            Label20.Text = clsFuncionesGral.ConvertMayus("importe: ");
            Label21.Text = clsFuncionesGral.ConvertMayus("observaciones: ");
            Label23.Text = clsFuncionesGral.ConvertMayus("concepto comprobante");
            //Label21.Text = clsFuncionesGral.ConvertMayus("");//mensaje ya tiene cargado una atenta nota
            Label25.Text = clsFuncionesGral.ConvertMayus("importe");
            Label26.Text = clsFuncionesGral.ConvertMayus("factura (archivo pdf)");
            Label27.Text = clsFuncionesGral.ConvertMayus("factura (archivo xml)");
            Label28.Text = clsFuncionesGral.ConvertMayus("subir ticket(opcional)");
            Label24.Text = clsFuncionesGral.ConvertMayus("CONFIRMACION METODO PAGO");
            Label31.Text = clsFuncionesGral.ConvertMayus("LA SIGUIENTE FACTURA: ");
            Label32.Text = clsFuncionesGral.ConvertMayus("TIENE COMO METODO DE PAGO");
            Label33.Text = "¿Es correcto el MÉTODO DE PAGO o desea cambiar a pago por tarjeta de débito? (si es correcto favor presionar CONTINUAR o MODIFICAR para CAMBIAR el método a pago por tarjeta debito)";
            //Label28.Text = clsFuncionesGral.ConvertMayus("tickets escaneado en formato pdf que avala factura (opcional)");
            LblFechaReint.Text = UltimoDia;
            lnkAgregaNoFiscal.Text = clsFuncionesGral.ConvertMayus("Agregar importe");
            lnkAddFacturas.Text = clsFuncionesGral.ConvertMayus("Agregar factura");
            lnkAgregaNoFact.Text = clsFuncionesGral.ConvertMayus("AGREGAR COMPROBANTE");
            lnkContinuarMetodoPago.Text = clsFuncionesGral.ConvertMayus("continuar");
            lnkMoficarMetodoPago.Text = clsFuncionesGral.ConvertMayus("modificar");

            CBoxAcepto.Checked = false;
            tBoxDeclaracion.Text = declaracion;
            CBoxAcepto.Text = "Acepto los terminos";
            BtnCerrarComp.Visible = false;

            Valida_Informe(poComision);

            if ((poComision.Territorio == "3") && ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8")))
            {

                dplConceptoInt.Items.Clear();
                dplConceptoInt.Items.Add(new ListItem("ALIMENTACION", "5"));
                dplConceptoInt.Items.Add(new ListItem("HOSPEDAJE", "9"));
                dplConceptoInt.Items.Add(new ListItem("TRANSPORTE LOCAL", "11"));
                dplConceptoInt.Items.Add(new ListItem("NO FISCALES", "12"));
                dplConceptoInt.Items.Add(new ListItem("LAVANDERIA Y TINTORERIA", "20"));
                dplConceptoInt.AppendDataBoundItems = true;
                dplConceptoInt.DataBind();


                //Lista = null;
                Label6.Text = clsFunciones.ConvertMayus("cARGA DE COMPROBANTES DE LA COMISION ") + poComision.Archivo.Replace(".pdf", "");
                Label7.Text = clsFuncionesGral.ConvertMayus("monto total utilizado por el concepto seleccionado :");
                Label8.Text = clsFuncionesGral.ConvertMayus(" evidencias :");
                lnkAgregaCompInt.Text = clsFuncionesGral.ConvertMayus(" agregar comprobacion");
                clsFuncionesGral.Activa_Paneles(pnlInternacional, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlInternacional, false);
            }
        }
        //evento de boton para carga de informe de comision
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Sube_Informe_Comision();
        }
        //sube informe
        public void Sube_Informe_Comision()
        {
            bool fileOk = false;
            string[] lsCadena = new string[2];
            Comision DetalleComision = new Comision();
            if (fupdlComision.HasFile)
            {
                DetalleComision = (Comision)Session["DetallesComision"];

                Valida_Carpeta(DetalleComision.Ruta, true);

                String fileExtension = System.IO.Path.GetExtension(fupdlComision.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (fileOk)
            {
                try
                {
                    fupdlComision.PostedFile.SaveAs(Session["Crip_Ruta"] + "/" + fupdlComision.FileName);
                    MngNegocioComision.Inserta_Informe_Comision(DetalleComision.Oficio, DetalleComision.Ubicacion,   DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "1", fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, lsHoy, lsHoy, DetalleComision.Periodo);
                    
                    Valida_Informe(DetalleComision);
                    //   Carga_Detalle();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Informe de comisión ha subido exitosamente.');", true);
                    DetalleComision = null;
                    return;

                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
                    DetalleComision = null;
                    return;
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                DetalleComision = null;
                return;
            }

        }
        //valida informe de actividades de comision
        public void Valida_Informe(Comision poComision)
        {
            InapescaWeb.Entidades.comision_informe cc;
            InapescaWeb.Entidades.comision_informe cc2;
            string[] lsCadena = new string[5];
            lsCadena = poComision.Archivo.ToString().Split(new Char[] { '-' });
            
            ////cc2 = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), poComision.Ubicacion_Comisionado,/*Session["Crip_Ubicacion"].ToString(),*/ lsCadena[3], poComision.Dep_Proy, lsCadena[4].Replace(".pdf", ""),poComision.Ubicacion_Autoriza);
            cc = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), poComision.Ubicacion_Comisionado,/*Session["Crip_Ubicacion"].ToString(),*/ lsCadena[3], poComision.Dep_Proy, lsCadena[4].Replace(".pdf", ""), poComision.Proyecto);
            
               
            if ((cc.FOLIO != null) & (clsFuncionesGral.FormatFecha(cc.FECHA_FINAL) != Dictionary.FECHA_NULA))
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Ya tiene cargado un informe de comision , se validara este para proceder a termino de su comprobacion");
                clsFuncionesGral.Activa_Paneles(pnlInforme, true);
                fupdlComision.Visible = false;
                ImageButton1.Visible = false;
            }
            else if (clsFuncionesGral.FormatFecha(cc.FECHA_FINAL) == Dictionary.FECHA_NULA)
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Ya tiene un informe parcial en el servidor,espere a que sea validado .");

                clsFuncionesGral.Activa_Paneles(pnlInforme, true);
                fupdlComision.Visible = false;
                ImageButton1.Visible = false;
            }
            else
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Carga de Informe de Comision:") + "(formato pdf)";

                clsFuncionesGral.Activa_Paneles(pnlInforme, true);
            }


        }
        //valida carpeta
        public void Valida_Carpeta(string psRuta, bool pbInforme = false, bool pbComprobacionFiscales = false, bool pbOtros = false, bool pbComprobacionNoFiscales = false)
        {
            Ruta = "";
            UbicacionFile = "";
            string raiz = HttpContext.Current.Server.MapPath("..");
            if (pbInforme)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.INFORME)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.INFORME);
                //Ruta = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
                //UbicacionFile = psRuta + "/" + Dictionary.INFORME;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.INFORME;
            }

            if (pbComprobacionFiscales)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.FISCALES)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.FISCALES);
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.FISCALES;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.FISCALES;
            }

            if (pbOtros)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.OTROS)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.OTROS);
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.OTROS;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.OTROS;
            }

        }
        //carga listas en gridviews
        public void Crear_Tabla(Comision poComision)
        {
            Session["DatosInternacional"] = null;
            Session["DatosFiscales"] = null;
            Session["DatosNoFiscales"] = null;


            if (((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8")) && (poComision.Territorio == "3"))
            {
                Session["DatosInternacional"] = MngNegocioComprobacion.Regresa_ListComprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), poComision.Archivo, "0");
                GvInternacional.DataSource = Session["DatosInternacional"];
                GvInternacional.DataBind();
            }
            else
            {
                Session["DatosFiscales"] = MngNegocioComprobacion.Regresa_ListComprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), poComision.Archivo, "2");
                GvFiscales.DataSource = Session["DatosFiscales"];
                GvFiscales.DataBind();
            }
            Session["DatosNoFiscales"] = MngNegocioComprobacion.Regresa_ListComprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), poComision.Archivo, "3");
            GvNoFiscales.DataSource = Session["DatosNoFiscales"];
            GvNoFiscales.DataBind();

        }
        //valida carpeta
        public void Valida_Carpeta(string psRuta)
        {
            Ruta = "";
            UbicacionFile = "";
            string raiz = HttpContext.Current.Server.MapPath("..");

            if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.INFORME)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.INFORME);
            //Ruta = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
            Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
            //UbicacionFile = psRuta + "/" + Dictionary.INFORME;
            Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.INFORME;

            if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.FISCALES)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.FISCALES);
            Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.FISCALES;
            Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.FISCALES;

            if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.OTROS)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.OTROS);
            Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.OTROS;
            Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.OTROS;

        }
        //evento click de treeview menu
        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (Session["Crip_Rol"] == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Server.MapPath("./index.aspx"), true);
            }

            string lsModulo;
            string lsRol = Session["Crip_Rol"].ToString();

            if (tvMenu.SelectedNode != null)
            {
                lsModulo = Convert.ToString(tvMenu.SelectedNode.Value);

                WebPage objWebPage = MngNegocioMenu.MngNegocioURL(lsModulo, lsRol);

                if (objWebPage != null)
                {
                    if (objWebPage.URL != string.Empty)
                    {
                        if (objWebPage.Padre != "0")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                        if (objWebPage.Modulo == "99")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                    }
                    else
                    {
                        //message de error por hacer
                    }
                }
            }
        }
        //Evento Lnk Home 
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }
        //Evento Link Usuario datos personales
        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }
        //Carga Datos de Comprobantes en comision internacional
        protected void lnkAgregaCompInt_Click(object sender, EventArgs e)
        {
            if ((txtMonto.Text == null) | (txtMonto.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Monto no puede ser vacio.');", true);
                return;
            }

            if (!clsFuncionesGral.IsNumeric(txtMonto.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Monto debe ser númerico');", true);
                return;
            }
            else if (txtMonto.Text == Dictionary.NUMERO_CERO)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Monto no puede ser cero');", true);
                return;
            }
            bool fileOk = false;
            if (dplConceptoInt.SelectedValue.ToString() != "12")
            {
                if (!fupdEvidenciaInt.HasFile)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Archivo de evidencia de erogación es nesesario.');", true);
                    return;
                }
                else
                {
                    //Valida_Carpeta(DetalleComision.Ruta, false, true);
                    String fileExtension = System.IO.Path.GetExtension(fupdEvidenciaInt.FileName).ToLower();
                    String[] allowedExtensions = { ".pdf", ".PDF" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOk = true;
                        }
                    }
                }
            }
            else
            {
                fileOk = true;
            }

            if ((fileOk))
            {
                string[] lsCadena = new string[2];
                lsFolio = Request.QueryString["folio"];
                lsCadena = lsFolio.Split(new Char[] { '|' });
                Comision detalleComision = new Comision();
                detalleComision = (Comision)Session["DetallesComision"];
                // = MngNegocioComision.Obten_Detalle(lsCadena[0] + ".pdf", Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), lsCadena[1]);

                try
                {
                    bool sube;
                    string existe = MngNegocioComprobacion.Exist_UUUID(fupdEvidenciaInt.FileName);
                    if ((existe == "") | (existe == null))
                    {
                        if (dplConceptoInt.SelectedValue.ToString() != "12")
                        {
                            Valida_Carpeta(detalleComision.Ruta, false, true);

                            if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fupdEvidenciaInt.FileName))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ya existe una factura con ese mismo nombre favor de cambiarlo');", true);
                                detalleComision = null;
                                return;
                            }
                            else
                            {
                                fupdEvidenciaInt.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdEvidenciaInt.FileName);
                            }
                        }

                        if (dplConceptoInt.SelectedValue.ToString() != "12")
                        {
                            bool InstertaComp = MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, "2", dplConceptoInt.SelectedValue.ToString(), dplConceptoInt.SelectedItem.Text, fupdEvidenciaInt.FileName, clsFuncionesGral.ConvertString(txtMonto.Text), "", "01", "01", dplConceptoInt.SelectedItem.Text + "|Comprobante|" + fupdEvidenciaInt.FileName.Replace(".pdf", ""), fupdEvidenciaInt.FileName.Replace(".pdf", ""), "", fupdEvidenciaInt.FileName.Replace(".pdf", ""), detalleComision.Periodo);
                        }
                        else
                        {
                            string ImporteCalculado = clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(detalleComision.Total_Viaticos)) * 0.10));
                            string lsComprobado = MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), detalleComision.Archivo, "'12'");
                            if ((clsFuncionesGral.Convert_Double(txtMonto.Text) + clsFuncionesGral.Convert_Double(lsComprobado)) > clsFuncionesGral.Convert_Double(ImporteCalculado))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Monto de comprobacion no fiscal excede a su monto calculado, favor de validar');", true);
                                detalleComision = null;
                                return;
                            }
                            else
                            {
                                MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, "3", dplConceptoInt.SelectedValue.ToString(), dplConceptoInt.SelectedItem.Text, fupdEvidenciaInt.FileName, clsFuncionesGral.ConvertString(txtMonto.Text), "", "01", "01", dplConceptoInt.SelectedItem.Text + "|Comprobante|" + fupdEvidenciaInt.FileName.Replace(".pdf", ""), fupdEvidenciaInt.FileName.Replace(".pdf", ""), "", fupdEvidenciaInt.FileName.Replace(".pdf", ""), detalleComision.Periodo);
                            }
                        }
                        Crear_Tabla(detalleComision);
                        Carga_Detalle(detalleComision);
                        detalleComision = null;
                    }
                    else
                    {
                        Crear_Tabla(detalleComision);
                        detalleComision = null;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('la factura que intenta subir ya fue usada para otra comprobacion, favor de ingresar una valida');", true);
                        return;
                    }
                }
                catch (Exception x)
                {

                    fupdEvidenciaInt.Dispose();
                    Console.Write(x.Message);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);

                return;
            }
        }
        //Elimina de la comprobacion Internacional
        protected void GvInternacional_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] cadenas = new string[3];
            string fechaEliminar = clsFuncionesGral.FormatFecha(GvInternacional.Rows[Convert.ToInt32(GvInternacional.SelectedIndex.ToString())].Cells[0].Text.ToString());
            string lsImporte = GvInternacional.Rows[Convert.ToInt32(GvInternacional.SelectedIndex.ToString())].Cells[2].Text.ToString();
            cadenas = GvInternacional.Rows[Convert.ToInt32(GvInternacional.SelectedIndex.ToString())].Cells[3].Text.ToString().Split(new Char[] { '|' });
            lsImporte = lsImporte.Replace("$", "");
            Comision DetalleComision = new Comision();
            DetalleComision = (Comision)Session["DetallesComision"];
            //DetalleComision = MngNegocioComision.Obten_Detalle(NombreArchivo.ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            bool X = MngNegocioComision.Update_Estatus_Comprobacion(DetalleComision.Oficio, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), fechaEliminar, lsImporte, cadenas[2], "2");

            if (X)
            {
                Valida_Carpeta(DetalleComision.Ruta, false, true);

                string RUTA = Session["Crip_Ruta"].ToString();

                if (File.Exists(RUTA + "/" + cadenas[2] + ".pdf")) File.Delete(RUTA + "/" + cadenas[2] + ".pdf");

                MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), DetalleComision.Archivo, "9", "");
                MngNegocioComision.Update_estatus_Comision("9", DetalleComision.Comisionado, DetalleComision.Oficio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                Crear_Tabla(DetalleComision);
                Calcula_Reintegro(DetalleComision);
                DetalleComision = null;
            }

        }
        //Elimina de la lista de comprobantes sin requisitos fiscales
        protected void GvNoFiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fechaEliminar = clsFuncionesGral.FormatFecha(GvNoFiscales.Rows[Convert.ToInt32(GvNoFiscales.SelectedIndex.ToString())].Cells[0].Text.ToString());
            string lsImporte = GvNoFiscales.Rows[Convert.ToInt32(GvNoFiscales.SelectedIndex.ToString())].Cells[2].Text.ToString();
            lsImporte = lsImporte.Replace("$", "");
            string observaciones = GvNoFiscales.Rows[Convert.ToInt32(GvNoFiscales.SelectedIndex.ToString())].Cells[3].Text.ToString();
            Comision DetalleComision = new Comision();
            DetalleComision = (Comision)Session["DetallesComision"];
            //DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());
            bool T = MngNegocioComision.Update_Estatus_Comprobacion(DetalleComision.Oficio, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), fechaEliminar, lsImporte, observaciones, "3");
            if (T)
            {
                MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), DetalleComision.Archivo, "9", "");
                MngNegocioComision.Update_estatus_Comision("9", DetalleComision.Comisionado, DetalleComision.Oficio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                MngNegocioComision.Update_Status_ComisionDetalle("9", DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);
                Crear_Tabla(DetalleComision);
                Calcula_Reintegro(DetalleComision);
                DetalleComision = null;
            }

        }
        //evento que elimina factura de lista fiscales
        protected void GvFiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] lsCadena = new string[2];
            lsFolio = Request.QueryString["folio"];
            lsCadena = lsFolio.Split(new Char[] { '|' });
            if (lsCadena[1] != "7")
            {
                string[] cadenas = new string[3];
                string fechaEliminar = clsFuncionesGral.FormatFecha(GvFiscales.Rows[Convert.ToInt32(GvFiscales.SelectedIndex.ToString())].Cells[0].Text.ToString());
                string lsImporte = GvFiscales.Rows[Convert.ToInt32(GvFiscales.SelectedIndex.ToString())].Cells[2].Text.ToString();
                lsImporte = lsImporte.Replace("$", "");
                cadenas = GvFiscales.Rows[Convert.ToInt32(GvFiscales.SelectedIndex.ToString())].Cells[3].Text.ToString().Split(new Char[] { '|' });
                Comision DetalleComision = new Comision();
                DetalleComision = (Comision)Session["DetallesComision"];
                //DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());
                bool X = MngNegocioComision.Update_Estatus_Comprobacion(DetalleComision.Oficio, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), fechaEliminar, lsImporte, cadenas[2], "2");
                if (X)
                {
                    Valida_Carpeta(DetalleComision.Ruta, false, true);
                    string RUTA = Session["Crip_Ruta"].ToString();
                    if (File.Exists(RUTA + "/" + cadenas[2] + ".pdf")) File.Delete(RUTA + "/" + cadenas[2] + ".pdf");
                    if (File.Exists(RUTA + "/" + cadenas[2] + ".xml")) File.Delete(RUTA + "/" + cadenas[2] + ".xml");
                    MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), DetalleComision.Archivo, "9", "");
                    MngNegocioComision.Update_estatus_Comision("9", DetalleComision.Comisionado, DetalleComision.Oficio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                    MngNegocioComision.Update_Status_ComisionDetalle("9", DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);
                    Crear_Tabla(DetalleComision);
                    Calcula_Reintegro(DetalleComision);
                    DetalleComision = null;
                }
            }
            

        }
        //CARGA EVENTO DEL DROPDONWLIST FISCALES
        protected void dplFiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActRural = false;
            ActSingladura = false;
            ActPeajeNoFacturable = false;
            string[] lsCadena = new string[2];
            lsFolio = Request.QueryString["folio"];
            lsCadena = lsFolio.Split(new Char[] { '|' });

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(lsCadena[0] + ".pdf", Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), lsCadena[1]);
            clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, false);
            clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
            clsFuncionesGral.Activa_Paneles(pnlpdf, false);

            string lsTipoComprobante = dplFiscales.SelectedValue.ToString();
            switch (lsTipoComprobante)
            {
                //case "1":
                //    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                //    clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, false);
                //    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
                //    clsFuncionesGral.Activa_Paneles(pnlpdf, false);
                //    break;
                case "2":
                    Carga_Lista_Conceptos();
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, true);
                    break;
                case "3":
                    if ((oDatosNum.TotalViaticosRurales > 0) | (oDatosNum.TotalViaticosNavegados > 0) | (oDatosNum.TotalPeajeDev > 0) | (oDatosNum.TotalPeajeAnt > 0))
                    {
                        dplConcepto.Items.Clear();
                        dplConcepto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "1"));
                        if (oDatosNum.TotalViaticosNavegados > 0)
                        {
                            dplConcepto.Items.Add(new ListItem("SINGLADURA", "19"));
                        }
                        if (oDatosNum.TotalViaticosRurales > 0)
                        {
                            dplConcepto.Items.Add(new ListItem("CERTIFICADO DE TRANSITO", "18"));
                        }
                        if ((oDatosNum.TotalPeajeDev > 0) | (oDatosNum.TotalPeajeAnt != 0))
                        {
                            dplConcepto.Items.Add(new ListItem("PEAJE NO FACTURABLE", "16"));
                        }
                        if ((oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev) > 0)
                        {
                            Proyecto objProyecto = new Proyecto();
                            objProyecto = MngNegocioProyecto.ObtieneDatosProy(DetalleComision.Dep_Proy, DetalleComision.Proyecto, DetalleComision.Periodo);
                            Programa objPrograma = new Programa();
                            objPrograma = MngNegociosProgramas.Obtiene_Datos_Programa(objProyecto.Programa, objProyecto.Direccion);
                            if (objProyecto.Componente == "4KS" && objProyecto.Dependencia == Dictionary.DGAIA && objPrograma.Tipo == "3")
                            {

                            }
                            else 
                            {
                                dplConcepto.Items.Add(new ListItem("NO FISCALES", "12"));
                            }
                        }

                        dplConcepto.AppendDataBoundItems = true;
                        dplConcepto.DataBind();
                        clsFuncionesGral.Activa_Paneles(pnlFacturas, true);
                        clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, false);
                        clsFuncionesGral.Activa_Paneles(pnlpdf, false);
                        clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
                    }
                    else
                    {
                        if (oDatosNum.TotalComprobadoNoFiscal == (oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev))
                        {
                            //double SumaImport = TotalViaticosNavegados - TotalCompSingladura;
                            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, true, false);
                            Label17.Text = "USTED YA CUENTA CON EL IMPORTE MAXIMO DE COMPROBACION NO FISCAL DENTRO DEL CONCEPTO NO FISCALES";
                        }
                        if ((oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev) <= 0)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, true, false);
                            Label17.Text = "USTED CUENTA CON UNA COMPROBACION COMPLETA O SUPERADA A LA REQUERIDA";
                        }
                        else
                        {
                            double SumaViatAntDev=0.00;
                            SumaViatAntDev= oDatosNum.TotalViaticosAnt+oDatosNum.TotalViaticosDev;

                            double ImporteXcomprobar = 0.00;
                            ImporteXcomprobar = SumaViatAntDev - oDatosNum.TotalComprobadoViaticos;
                            if (oDatosNum.TotalComprobadoNoFiscal > 0)
                            {
                                double SumaImport = 0.00;
                                SumaImport=(oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev) - oDatosNum.TotalComprobadoNoFiscal;
                                if (SumaImport <= ImporteXcomprobar)
                                {
                                    txtImporte.Text = clsFuncionesGral.Convert_Decimales(SumaImport.ToString());
                                    Label17.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                                    txtImporte.Enabled = false;
                                }
                                else 
                                {
                                    txtImporte.Text = clsFuncionesGral.Convert_Decimales(ImporteXcomprobar.ToString());
                                    Label17.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                                    txtImporte.Enabled = false;
                                }
                                
                            }
                            else
                            {
                                double SumaImport = 0.00;
                                SumaImport = oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev;
                                if (SumaImport <= ImporteXcomprobar)
                                {
                                    txtImporte.Text = clsFuncionesGral.Convert_Decimales(SumaImport.ToString());
                                    Label17.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + (oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                                    txtImporte.Enabled = false;
                                }
                                else
                                {
                                    txtImporte.Text = clsFuncionesGral.Convert_Decimales(ImporteXcomprobar.ToString());
                                    Label17.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                                    txtImporte.Enabled = false;
                                }
                                
                            }
                            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, true);
                        }
                        clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                        clsFuncionesGral.Activa_Paneles(pnlpdf, false);
                        clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
                    }
                    break;
            }
        }
        //CARGA EVENTO DEL DROPDONWLIST TIPO COMPROBANTES
        protected void dplConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActRural = false;
            ActSingladura = false;
            ActPeajeNoFacturable = false;

            clsFuncionesGral.Activa_Paneles(pnlpdf, false);
            clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
            clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, false);

            string psConcepto = dplConcepto.SelectedValue.ToString();

            if ((psConcepto == "5") || (psConcepto == "6") || (psConcepto == "7") || (psConcepto == "8") || (psConcepto == "9") || (psConcepto == "11") /*|| (psConcepto == "14") || (psConcepto == "15")*/)
            {
                
                clsFuncionesGral.Activa_Paneles(pnlpdf, true);

            }
            //COMPRUEBA CONCEPTO PEAJE Y PEAJE NO FACTURABLE
            else if (psConcepto == "16")
            {
                if ((oDatosNum.TotalCompPeajeNoFact + oDatosNum.TotalComprobadoPeaje) == (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev))
                {
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, true, false);
                    lblMensajeNoFac.Text = "USTED YA CUENTA CON EL IMPORTE MAXIMO DE COMPROBACION  FISCAL Y NO FISCAL DENTRO DEL CONCEPTO DE PEAJE";
                }
                else
                {
                    if ((oDatosNum.TotalCompPeajeNoFact + oDatosNum.TotalComprobadoPeaje) > 0)
                    {
                        double SumaImport = (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev) - (oDatosNum.TotalCompPeajeNoFact + oDatosNum.TotalComprobadoPeaje);
                        txtImporteNoFacturable.Text = clsFuncionesGral.Convert_Decimales(SumaImport.ToString());
                        lblMensajeNoFac.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    else
                    {
                        txtImporteNoFacturable.Text = clsFuncionesGral.Convert_Decimales((oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev).ToString());
                        lblMensajeNoFac.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    ActPeajeNoFacturable = true;
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, true);
                }

            }
            //COMPRUEBA CONCEPTO SINGLAURAS
            /*else if (psConcepto == "19")
            {
                if (oDatosNum.TotalCompSingladura == oDatosNum.TotalViaticosNavegados)
                {
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, true, false);
                    lblMensajeNoFac.Text = "USTED YA CUENTA CON EL IMPORTE MAXIMO DE COMPROBACION NO FISCAL DENTRO DEL CONCEPTO DE SINGLADURA";
                }
                else
                {
                    if (oDatosNum.TotalCompSingladura > 0)
                    {
                        double SumaImport = oDatosNum.TotalViaticosNavegados - oDatosNum.TotalCompSingladura;
                        txtImporteNoFacturable.Text = clsFuncionesGral.Convert_Decimales(SumaImport.ToString());
                        txtImporteNoFacturable.Enabled = false;
                        lblMensajeNoFac.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    else
                    {
                        txtImporteNoFacturable.Text = clsFuncionesGral.Convert_Decimales(oDatosNum.TotalViaticosNavegados.ToString());
                        txtImporteNoFacturable.Enabled = false;
                        lblMensajeNoFac.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + oDatosNum.TotalViaticosNavegados.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    ActSingladura = true;
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, true);
                }

            }*/
            //COMPRUEBA CONCEPTO CERTIFICADO DE TRANSITO (RURALES)
            else if (psConcepto == "18")
            {
                if (oDatosNum.TotalCompRural == oDatosNum.TotalViaticosRurales)
                {
                    //double SumaImport = TotalViaticosNavegados - TotalCompSingladura;
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, true, false);
                    lblMensajeNoFac.Text = "USTED YA CUENTA CON EL IMPORTE MAXIMO DE COMPROBACION NO FISCAL DENTRO DEL CONCEPTO DE CERTIFICADO DE TRANSITO";
                }
                else
                {
                    if (oDatosNum.TotalCompRural > 0)
                    {
                        double SumaImport = oDatosNum.TotalViaticosRurales - oDatosNum.TotalCompRural;
                        txtImporteNoFacturable.Text = clsFuncionesGral.Convert_Decimales(SumaImport.ToString());
                        txtImporteNoFacturable.Enabled = false;
                        lblMensajeNoFac.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    else
                    {
                        txtImporteNoFacturable.Text = clsFuncionesGral.Convert_Decimales(oDatosNum.TotalViaticosRurales.ToString());
                        txtImporteNoFacturable.Enabled = false;
                        lblMensajeNoFac.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + oDatosNum.TotalViaticosRurales.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    ActRural = true;
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, true);
                }

            }
            //COMPRUEBA CONCEPTO NO FISCAL
            else if (psConcepto == "12")
            {
                if (oDatosNum.TotalComprobadoNoFiscal == (oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev))
                {
                    //double SumaImport = TotalViaticosNavegados - TotalCompSingladura;
                    clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, true, false);
                    Label17.Text = "USTED YA CUENTA CON EL IMPORTE MAXIMO DE COMPROBACION NO FISCAL DENTRO DEL CONCEPTO NO FISCALES";
                }
                else
                {
                    if (oDatosNum.TotalComprobadoNoFiscal > 0)
                    {
                        double SumaImport = (oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev) - oDatosNum.TotalComprobadoNoFiscal;
                        txtImporte.Text = clsFuncionesGral.Convert_Decimales(SumaImport.ToString());
                        Label17.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + SumaImport.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    else
                    {
                        txtImporte.Text = clsFuncionesGral.Convert_Decimales((oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev).ToString());
                        Label17.Text = "USTED CUENTA CON UN MONTO MAXIMO DE COMPROBACION:" + (oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    clsFuncionesGral.Activa_Paneles(pnlCompNoFiscal, true);
                }
            }

        }
        //AÑADE FACTURAS COMPROBABLES CON PDF Y XML
        protected void lnkAddFacturas_Click(object sender, EventArgs e)
        {
            string lsClvConcepto = dplConcepto.SelectedValue.ToString();
            string lsConcepto = dplConcepto.SelectedItem.ToString();
            ticket = "";
            TimbreFiscal = "";


            if (lsClvConcepto == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Concepto es nesesario.');", true);
                return;
            }

            bool xmlOK = false;
            bool fileOk = false;

            if (!fuplPDF.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Archivo pdf es nesesario.');", true);
                return;
            }
            else
            {
                //Valida_Carpeta(DetalleComision.Ruta, false, true);
                String fileExtension = System.IO.Path.GetExtension(fuplPDF.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (fupdTickets.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fupdTickets.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };
                bool fileticket = false;

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileticket = true;
                    }
                }

                if (fileticket)
                {
                    ticket = clsFuncionesGral.ConvertMinus(fupdTickets.FileName).Replace("pdf", "");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                    return;
                }
            }
            else
            {
                ticket = Dictionary.CADENA_NULA;
            }

            if (!fuplXML.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Archivo xml es nesesario');", true);
                return;
            }
            else
            {
                String fileExtension = System.IO.Path.GetExtension(fuplXML.FileName).ToLower();
                String[] allowedExtensions = { ".xml", ".XML" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        xmlOK = true;
                    }
                }
            }

            string nombrepdf = clsFuncionesGral.ConvertMinus(fuplPDF.FileName);
            nombrepdf = nombrepdf.Replace(".pdf", "");

            string nombreXml = clsFuncionesGral.ConvertMinus(fuplXML.FileName);
            nombreXml = nombreXml.Replace(".xml", "");

            if (nombrepdf != nombreXml)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Nombres de archivos pdf y xml deben ser iguales.');", true);
                return;
            }
            else if ((fileOk) & (xmlOK))
            {
                Comision detalleComision = new Comision();
                detalleComision = (Comision)Session["DetallesComision"];

                try
                {

                    Valida_CarpetaXML();

                    if (!File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName))
                    {
                        fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    }
                    else
                    {
                        if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                        fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    }

                    List<Entidad> llEntidad = new List<Entidad>();
                    Entidades.Xml oXml = new Entidades.Xml();
                    //LEE XML
                    clsFuncionesGral.Lee_XMl(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName, llEntidad, oXml);
                    
                    if (clsFuncionesGral.ConvertMayus(oXml.RFC_RECEPTOR) != Dictionary.RFC_INP)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('el RFC receptor no Corresponde al Instituto Nacional de Pesca');", true);
                        return;
                    }
                    else if (oXml.AÑOEXP != detalleComision.Periodo)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El añoi de expedición de la factura no Corresponde al año fscal en el cual se llevó a cabo su comisión');", true);
                        return;
                    }

                    //antes de subir checar que no este activo el uuid de factura si no insertar
                    string existe = MngNegocioComprobacion.Exist_UUUID(oXml.TIMBRE_FISCAL);

                    string sMetodoPago = "";

                    if ((existe == "") | (existe == null))
                    {
                        if ((lsClvConcepto == "5") || (lsClvConcepto == "9") || (lsClvConcepto == "11") || (lsClvConcepto == "14") || (lsClvConcepto == "15") || (lsClvConcepto == "17") || (lsClvConcepto == "12") || (lsClvConcepto == "20"))
                        {//((oXml.METODO_PAGO == "28") | (oXml.METODO_PAGO == "TARJETA DE DEBITO") | (oXml.METODO_PAGO == "TARJETADEDEBITO"))
                            if (((oXml.METODO_PAGO == "28") || (oXml.METODO_PAGO == "04") || (oXml.METODO_PAGO == "TARJETA DE DEBITO") || (oXml.METODO_PAGO == "TARJETADEDEBITO")) || ((oXml.FORMA_PAGO == "28") || (oXml.FORMA_PAGO == "04") || (oXml.FORMA_PAGO == "TARJETA DE DEBITO") || (oXml.FORMA_PAGO == "TARJETADEDEBITO")))
                            {
                                //inserta comision
                                InsertaComprobacionFiscal(detalleComision, llEntidad, oXml, lsClvConcepto, lsConcepto, ticket, "28");
                                detalleComision = null;
                                oXml = null;
                                llEntidad = null;

                            }
                            else
                            {
                                sMetodoPago = Regex.Replace(oXml.METODO_PAGO, @"[^\d]", "");
                                string formaPago = Regex.Replace(oXml.FORMA_PAGO, @"[^\d]", "");
                                if ((sMetodoPago == "28" || sMetodoPago == "04") || (formaPago == "28" || formaPago == "04"))
                                {
                                    //inserta comision
                                    InsertaComprobacionFiscal(detalleComision, llEntidad, oXml, lsClvConcepto, lsConcepto, ticket, "28");
                                    detalleComision = null;
                                    oXml = null;
                                    llEntidad = null;
                                }
                                else
                                {
                                    InsertaComprobacionFiscal(detalleComision, llEntidad, oXml, lsClvConcepto, lsConcepto, ticket, "01", true);
                                    TimbreFiscal = oXml.TIMBRE_FISCAL;
                                    LblTimbreFiscal.Text = TimbreFiscal;

                                    detalleComision = null;
                                    oXml = null;
                                    llEntidad = null;

                                }
                            }

                        }
                        else
                        {
                            InsertaComprobacionFiscal(detalleComision, llEntidad, oXml, lsClvConcepto, lsConcepto, ticket, sMetodoPago);
                            detalleComision = null;
                            oXml = null;
                            llEntidad = null;
                        }
                    }
                    else
                    {
                        Crear_Tabla(detalleComision);
                        Calcula_Reintegro(detalleComision);
                        detalleComision = null;
                        oXml = null;
                        llEntidad = null;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('la factura que intenta subir ya fue usada para otra comprobacion, favor de ingresar una valida');", true);
                        return;
                    }

                }
                catch (Exception x)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ocurrio un problema en la lectura de su CFDI, favor de enviar sus archivos al correo de soporte.smaf@gmail.com');", true);
                    return;
                    fuplXML.Dispose();
                    fuplPDF.Dispose();
                    Console.Write(x.Message);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }
            clsFuncionesGral.Activa_Paneles(pnlpdf, false);
            clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
            dplFiscales.SelectedIndex = 0;
        }

        //INSERTA COMPROBACIONES
        private void InsertaComprobacionFiscal(Comision detalleComision, List<Entidad> llEntidad, Entidades.Xml oXml, string lsClvConcepto, string lsConcepto, string ticket, string sMetPago, bool MetodoPagoDif = false)
        {
            Valida_Carpeta(detalleComision.Ruta, false, true);

            if (fupdTickets.HasFile)
            {
                fupdTickets.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdTickets.FileName);
            }

            double totalImporteXml = 0;

            totalImporteXml = clsFuncionesGral.Convert_Double(oXml.TOTAL);


            if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ya existe una factura con ese mismo nombre favor de cambiarlo');", true);
                return;
            }
            else
            {
                fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                fuplPDF.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplPDF.FileName);
            }


            bool InsertaCom = MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oXml.FECHA_TIMBRADO, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, lsConcepto, fuplPDF.FileName, clsFuncionesGral.ConvertString(totalImporteXml), fuplXML.FileName, oXml.METODO_PAGO, sMetPago, lsConcepto + "|factura|" + fuplPDF.FileName.Replace(".pdf", ""), fuplPDF.FileName.Replace(".pdf", ""), ticket, oXml.TIMBRE_FISCAL, detalleComision.Periodo, oXml.VERSION);

            foreach (Entidad x in llEntidad)
            {
                bool sube = MngNegocioXml.Inserta_DetalleXML(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), detalleComision.Archivo, fuplXML.FileName, oXml.TIMBRE_FISCAL, oXml.RFC_EMISOR, x.Descripcion, Dictionary.CADENA_NULA, oXml.FECHA_TIMBRADO, x.Codigo, oXml.IVA, oXml.TUA, oXml.ISR, oXml.IEPS, oXml.SFP, oXml.ISH, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, x.Codigo, clsFuncionesGral.ConvertString(totalImporteXml), Dictionary.NUMERO_CERO, oXml.TOTAL_IMPUESTOS_TRASLADADOS);
            }

            Crear_Tabla(detalleComision);
            Calcula_Reintegro(detalleComision);
            //combrueba metodo pago para modificarlo en caso contrario
            if (MetodoPagoDif == true)
            {
                CBoxAcepto.Checked = false;
                string sMetodoPago = Regex.Replace(oXml.FORMA_PAGO, @"[^\d]", "");

                if (!clsFuncionesGral.IsNumeric(oXml.FORMA_PAGO))
                {

                    if (!clsFuncionesGral.IsNumeric(sMetodoPago))
                    {
                        LblMetodoPago.Text = oXml.FORMA_PAGO;
                    }
                    else
                    {
                        string traeMetPago = MngNegocioComprobacion.Metodo_PagoComprobacion(sMetodoPago);
                        if (traeMetPago == "0")
                        {
                            LblMetodoPago.Text = oXml.FORMA_PAGO;
                        }
                        else
                        {
                            LblMetodoPago.Text = traeMetPago;
                        }
                    }
                }
                else
                {
                    string traeMetPago = MngNegocioComprobacion.Metodo_PagoComprobacion(oXml.FORMA_PAGO);
                    if (traeMetPago == "0")
                    {
                        LblMetodoPago.Text = oXml.FORMA_PAGO;
                    }
                    else
                    {
                        LblMetodoPago.Text = traeMetPago;
                    }
                }
                clsFuncionesGral.Activa_Paneles(pnlFiscales, false);
                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                clsFuncionesGral.Activa_Paneles(pnlMetodoPago, true);
            }
            if (ticket == "")
            {
                clsFuncionesGral.Activa_Paneles(Pnlticket2, true);
            }
        }
        //evento para agregar no fiscales
        protected void lnkAgregaNoFiscal_Click(object sender, EventArgs e)
        {
            string lsClvConcepto = "12";
            string lsConcepto = txtConcepto.Text;
            if ((txtConcepto.Text == null) | (txtConcepto.Text == Dictionary.CADENA_NULA))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Concepto obligatorio');", true);
                return;
            }
            if ((txtImporte.Text == null) | (txtImporte.Text == Dictionary.CADENA_NULA))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Importe obligatorio ');", true);
                return;
            }
            if ((txtObservaciones.Text == null) | (txtObservaciones.Text == Dictionary.CADENA_NULA))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('observaciones obligatorio');", true);
                return;
            }
            if (!clsFuncionesGral.IsNumeric(txtImporte.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Importe debe ser numerico');", true);
                return;
            }

            double TotalNofiscal = Convert.ToDouble(MngNegocioComprobacion.Total_AnualNoFiscal(Session["Crip_Usuario"].ToString()));

            double Suma = oDatosNum.TotalComprobadoNoFiscal + Convert.ToDouble(txtImporte.Text);
            if ((TotalNofiscal + Suma) >= 15000)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('usted ya ha superado el importe maximo de comprobacion no fiscal($15,000.00) de ejercicio en curso');", true);
                return;
            }

            if (Suma > Math.Round((oDatosNum.MaxNofiscalAnt + oDatosNum.MaxNofiscalDev), 2))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El valor del IMPORTE es mas grande del monto maximo otorgado');", true);
                Label17.ForeColor = Color.Red;
                return;
            }
            else 
            {
                //double SumaViatAntDev= oDatosNum.TotalViaticosAnt+ oDatosNum.TotalViaticosDev;
                //double DiferenciaViat= SumaViatAntDev - oDatosNum.TotalComprobadoViaticos;
                //if(Suma > )
                //{
                //}
                Comision detalleComision = new Comision();
                detalleComision = (Comision)Session["DetallesComision"];
                MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, clsFuncionesGral.ConvertMayus(lsConcepto), "", clsFuncionesGral.Convert_Decimales(txtImporte.Text), "", "01", "01", clsFuncionesGral.ConvertMayus(txtObservaciones.Text), "", "", "", detalleComision.Periodo);
                Crear_Tabla(detalleComision);
                Calcula_Reintegro(detalleComision);
                detalleComision = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Subido Con exito');", true);
            }

            //clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
            clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
            dplFiscales.SelectedIndex = 0;
        }
        //AÑADE COMPROBANTES NO FACTURABLES
        protected void lnkAgregaNoFact_Click(object sender, EventArgs e)
        {

            if (!clsFuncionesGral.IsNumeric(txtImporteNoFacturable.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El IMPORTES debe ser numerico');", true);
                return;
            }
            if ((txtImporteNoFacturable.Text == null) | (txtImporteNoFacturable.Text == Dictionary.CADENA_NULA) | (txtImporteNoFacturable.Text == Dictionary.NUMERO_CERO))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('La cantidad de IMPORTE no facturable es obligatoria y mayor a Cero');", true);
                return;
            }

            if (ActPeajeNoFacturable == true)
            {
                double Suma = oDatosNum.TotalComprobadoPeaje + Convert.ToDouble(txtImporteNoFacturable.Text);
                if (Suma > (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El valor del IMPORTE es mas grande del monto maximo otorgado');", true);
                    lblMensajeNoFac.ForeColor = Color.Red;
                    return;
                }
            }
            if (ActSingladura == true)
            {
                double Suma = oDatosNum.TotalCompSingladura + Convert.ToDouble(txtImporteNoFacturable.Text);
                if (Suma > oDatosNum.TotalViaticosNavegados)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El valor del IMPORTE es mas grande del monto maximo otorgado');", true);
                    lblMensajeNoFac.ForeColor = Color.Red;
                    return;
                }
            }
            if (ActRural == true)
            {
                double Suma = oDatosNum.TotalCompRural + Convert.ToDouble(txtImporteNoFacturable.Text);
                if (Suma > oDatosNum.TotalViaticosRurales)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El valor del IMPORTE es mas grande del monto maximo otorgado');", true);
                    lblMensajeNoFac.ForeColor = Color.Red;
                    return;
                }
            }

            bool fileOk = false;

            Comision DetalleComision = new Comision();
            DetalleComision = (Comision)Session["DetallesComision"];

            if (fupdNoFacturable.HasFile)
            {
                Valida_Carpeta(DetalleComision.Ruta, false, true, false, false);

                String fileExtension = System.IO.Path.GetExtension(fupdNoFacturable.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }


            if (fileOk)
            {
                try
                {

                    if (!File.Exists(Session["Crip_Ruta"].ToString() + "/" + fupdNoFacturable.FileName))
                    {
                        fupdNoFacturable.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdNoFacturable.FileName);
                    }
                    else
                    {
                        if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fupdNoFacturable.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fupdNoFacturable.FileName);
                        fupdNoFacturable.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdNoFacturable.FileName);
                    }

                    string mensaje = "";

                    if ((ActPeajeNoFacturable == true) && (ActSingladura == false) && (ActRural == false))
                    {
                        MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "3", "16", dplConcepto.SelectedItem.Text, fupdNoFacturable.FileName, clsFuncionesGral.Convert_Decimales(txtImporteNoFacturable.Text), "", "01", "01", "Peajes|Atenta Nota|" + clsFuncionesGral.ConvertMinus(fupdNoFacturable.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdNoFacturable.FileName).Replace(".pdf", ""), "", "", DetalleComision.Periodo);
                        mensaje = "Atenta Nota y Tickets de peaje subidos exitosamente";
                    }
                    if ((ActPeajeNoFacturable == false) && (ActSingladura == true) && (ActRural == false))
                    {
                        MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "3", "19", dplConcepto.SelectedItem.Text, fupdNoFacturable.FileName, clsFuncionesGral.Convert_Decimales(txtImporteNoFacturable.Text), "", "01", "01", "SINGLADURAS-" + clsFuncionesGral.ConvertMinus(fupdNoFacturable.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdNoFacturable.FileName).Replace(".pdf", ""), "", "", DetalleComision.Periodo);
                        mensaje = "Despacho de Capitania de puerto subido exitosamente, se encuentra a validacion por el area correspondiente";
                    }

                    if ((ActPeajeNoFacturable == false) && (ActSingladura == false) && (ActRural == true))
                    {
                        MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "3", "18", dplConcepto.SelectedItem.Text, fupdNoFacturable.FileName, clsFuncionesGral.Convert_Decimales(txtImporteNoFacturable.Text), "", "01", "01", "CERTIFICADO-" + clsFuncionesGral.ConvertMinus(fupdNoFacturable.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdNoFacturable.FileName).Replace(".pdf", ""), "", "", DetalleComision.Periodo);
                        mensaje = "Certificado de transito subido exitosamente, se encuentra a validacion por el area correspondiente";
                    }
                    Carga_Detalle(DetalleComision);
                    Crear_Tabla(DetalleComision);
                    Calcula_Reintegro(DetalleComision);
                    DetalleComision = null;
                    txtImporteNoFacturable.Text = Dictionary.CADENA_NULA;
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    dplFiscales.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('" + mensaje + "');", true);
                    return;

                }
                catch (Exception ex)
                {
                    Carga_Detalle(DetalleComision);
                    Crear_Tabla(DetalleComision);
                    DetalleComision = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su documento de comprobacion no facturable');", true);
                    clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    dplFiscales.SelectedIndex = 0;
                    return;
                }

            }
            else
            {
                Carga_Detalle(DetalleComision);
                Crear_Tabla(DetalleComision);
                DetalleComision = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                clsFuncionesGral.Activa_Paneles(pnlNoFacturable, false);
                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                dplFiscales.SelectedIndex = 0;
                return;
            }


        }
        //VALIDA CARPETA XML 
        public void Valida_CarpetaXML()
        {
            string raiz = HttpContext.Current.Server.MapPath("..");
            if (!Directory.Exists(raiz + "\\" + " XML")) Directory.CreateDirectory(raiz + "\\" + " XML"); ;
            Session["Crip_Ruta"] = raiz + "\\" + " XML";
        }
        //confirmacion Cambio forma de pago
        protected void lnkContinuarMetodoPago_Click(object sender, EventArgs e)
        {

            clsFuncionesGral.Activa_Paneles(pnlFiscales, true);
            clsFuncionesGral.Activa_Paneles(pnlMetodoPago, false);
            ticket = "";
            TimbreFiscal = "";

        }
        //Modificacion paty//MODIFICACION PEDRO 01-09-2017
        protected void LnkDetallesReintegro_Click(object sender, EventArgs e)
        {
            Comision detalleComision = new Comision();
            detalleComision = (Comision)Session["DetallesComision"];

            Crear_Tabla(detalleComision);
            Calcula_Reintegro(detalleComision);

            Actualizar = false;
            BtnCerrarComp.Visible = false;
            LinkDescarga.Visible = false;
            BtnGeneraRef.Visible = false;
            LinkDescarga.Visible = false;
            IDMaximo = "";
            ReferenciaExis = "";

            List<DetallesTotalCom> ListaDetalles = new List<DetallesTotalCom>();
            List<Entidad> ListaReferen = new List<Entidad>();

            if ((oDatosNum.TotalTotalDev > 0) && (oDatosNum.TotalTotalAnt > 0))
            {
                if (oDatosNum.TotalViaticosAnt != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();

                    objDetalles.Concepto = "VIATICOS ANT";
                    objDetalles.Total = oDatosNum.TotalViaticosAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoViaticosAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = oDatosNum.TotalViaticosAnt - oDatosNum.TotalComprobadoViaticosAnt;

                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        objDatosRef.Codigo = "VIATICOS";
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }

                if (oDatosNum.TotalViaticosDev != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();

                    objDetalles.Concepto = "VIATICOS DEV";
                    objDetalles.Total = oDatosNum.TotalViaticosDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoViaticosDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    objDetalles.TotalReintegro = "$0.00";

                    ListaDetalles.Add(objDetalles);
                }
                ////////////////////////////////////////////////////////////////
                if (oDatosNum.TotalCombustibleAnt != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();

                    objDetalles.Concepto = "COMBUSTIBLE ANT";
                    objDetalles.Total = oDatosNum.TotalCombustibleAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoCombustibleAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = oDatosNum.TotalCombustibleAnt - oDatosNum.TotalComprobadoCombustibleAnt;

                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        objDatosRef.Codigo = "COMBUSTIBLE";
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }

                if (oDatosNum.TotalCombustibleDev != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();

                    objDetalles.Concepto = "COMBUSTIBLE DEV";
                    objDetalles.Total = oDatosNum.TotalCombustibleDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoCombustibleDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    objDetalles.TotalReintegro = "$0.00";

                    ListaDetalles.Add(objDetalles);
                }
                ///////////////////////////////////////////////////////////////////////////////////
                if (oDatosNum.TotalPeajeAnt != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();

                    objDetalles.Concepto = "PEAJE ANT";
                    objDetalles.Total = oDatosNum.TotalPeajeAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoPeajeAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = oDatosNum.TotalPeajeAnt - oDatosNum.TotalComprobadoPeajeAnt;

                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        objDatosRef.Codigo = "PEAJE";
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }

                if (oDatosNum.TotalPeajeDev != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();

                    objDetalles.Concepto = "PEAJE DEV";
                    objDetalles.Total = oDatosNum.TotalPeajeDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoPeajeDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    objDetalles.TotalReintegro = "$0.00";

                    ListaDetalles.Add(objDetalles);
                }
                //////////////////////////////////////////////////////////////
                if (oDatosNum.TotalPasajeAnt != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();

                    objDetalles.Concepto = "PASAJE ANT";
                    objDetalles.Total = oDatosNum.TotalPasajeAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoPasajeAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = oDatosNum.TotalPasajeAnt - oDatosNum.TotalComprobadoPasajeAnt;

                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        objDatosRef.Codigo = "PASAJE";
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }

                if (oDatosNum.TotalPasajeDev != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();

                    objDetalles.Concepto = "PASAJE DEV";
                    objDetalles.Total = oDatosNum.TotalPasajeDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoPasajeDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    objDetalles.TotalReintegro = "$0.00";

                    ListaDetalles.Add(objDetalles);
                }
                /////////////////////////////////////////////////////////////

                if (oDatosNum.TotalCompReintegro > 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();
                    objDetalles.Concepto = "REINTEGRO";
                    objDetalles.Total = "-" + oDatosNum.TotalCompReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalCompReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));


                    objDetalles.TotalReintegro = "$0.00";
                    objDatosRef.Codigo = "REINTEGRO";
                    objDatosRef.Descripcion = "-" + oDatosNum.TotalCompReintegro.ToString();
                    ListaReferen.Add(objDatosRef);

                    ListaDetalles.Add(objDetalles);
                }
                /////////////////////////////////////////////////////////////
                GvDetalles.DataSource = ListaDetalles;
                GvDetalles.DataBind();

                if (detalleComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS || detalleComision.Forma_Pago_Viaticos == "0")
                {
                    //MODIFICACION DOMINGO
                    GvDetalles.Columns[2].Visible = false;
                    GvDetalles.Columns[3].HeaderText = "TOTAL LIMITE";
                    BtnGeneraRef.Visible = false;
                    divReint.Visible = false;
                    BtnCerrarComp.Visible = true;


                }
                else if (detalleComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_ANTICIPADOS)
                {
                    //MODIFICACION DOMINGO
                    GvDetalles.Columns[3].HeaderText = "TOTAL OTORGADO";
                    BtnGeneraRef.Visible = true;
                    divReint.Visible = true;
                    TbTotalReintegro.Text = lblMonto.Text.ToString();

                    TbTotalReintegro.Enabled = false;


                    //MODIFICACION PATTY
                    Session["ConceptosRef"] = ListaReferen;


                    //comprobar referencias existentes con respecto al archivo 
                    List<GenerarRef> ListaConcep = new List<GenerarRef>();

                    ListaConcep = MngNegocioGenerarRef.Obten_Informacion_Referencia(detalleComision.Archivo);

                    if (ListaConcep.Count == 0)
                    {
                        //IDMaximo=MngNegocioGenerarRef.Obtiene_Max_Referencia();
                        BtnGeneraRef.Text = "Generar Referencia";  // BOTON NUEVA REF//  
                    }
                    else
                    {
                        foreach (GenerarRef rec in ListaConcep)
                        {
                            if (Convert.ToDouble(rec.Importe) == Math.Round(oDatosNum.TotalImporteReintegro, 2))
                            {
                                BtnGeneraRef.Text = "Descargar Referencia"; // activar btn actualizar//                            
                                BtnGeneraRef.Visible = false;
                                LinkDescarga.Visible = true;
                                LinkDescarga.InnerText = "Descarga Referencia";
                                LinkDescarga.HRef = "~/Descarga.aspx?REFEREN=" + rec.Referencia + ".pdf";
                            }
                            else
                            {
                                BtnGeneraRef.Text = "Actualizar Referencia"; // activar btn actualizar//
                                Actualizar = true;
                                IDMaximo = rec.ID;
                                LinkDescarga.Visible = true;
                                LinkDescarga.InnerText = "Referencia Anterior";
                                //~/
                                LinkDescarga.HRef = "~/Descarga.aspx?REFEREN=" + rec.Referencia + ".pdf";
                                ReferenciaExis = rec.Referencia;
                            }
                        }
                    }
                }
                ////////////////////////
                if (Math.Round(oDatosNum.TotalImporteReintegro, 2) <= 0)
                {
                    BtnCerrarComp.Visible = true;
                    BtnGeneraRef.Visible = false;
                    LinkDescarga.Visible = false;
                }
                ModalPopupExtender1.Show();

                //cambiar en donde corresponda
                divComprDev.Visible = true;
                TbCompEnDev.Text = oDatosNum.TotalComprobadoDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

            }
            else
            {
                if ((oDatosNum.TotalViaticosAnt + oDatosNum.TotalViaticosDev) != 0)
                {

                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();

                    objDetalles.Concepto = "VIATICOS";
                    objDetalles.Total = (oDatosNum.TotalViaticosAnt + oDatosNum.TotalViaticosDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = (oDatosNum.TotalComprobadoViaticos + oDatosNum.TotalComprobadoNoFiscal).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = (oDatosNum.TotalViaticosAnt - (oDatosNum.TotalComprobadoViaticos + oDatosNum.TotalComprobadoNoFiscal + oDatosNum.TotalCompRural));
                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        objDatosRef.Codigo = "VIATICOS";
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }

                    ListaDetalles.Add(objDetalles);


                }

                if ((oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev) != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();
                    objDetalles.Concepto = "COMBUSTIBLE";
                    objDetalles.Total = (oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoCombustible.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = ((oDatosNum.TotalCombustibleAnt + oDatosNum.TotalCombustibleDev) - oDatosNum.TotalComprobadoCombustible);
                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Codigo = "COMBUSTIBLE";
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }
                if ((oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev) != 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();
                    objDetalles.Concepto = "PEAJE";
                    objDetalles.Total = (oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = (oDatosNum.TotalComprobadoPeaje + oDatosNum.TotalCompPeajeNoFact).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    double sTotalReint = ((oDatosNum.TotalPeajeAnt + oDatosNum.TotalPeajeDev) - (oDatosNum.TotalComprobadoPeaje + oDatosNum.TotalCompPeajeNoFact));
                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Codigo = "PEAJE";
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }
                if ((oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev) > 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();
                    objDetalles.Concepto = "PASAJE";
                    objDetalles.Total = (oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalComprobadoPasaje.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    double sTotalReint = ((oDatosNum.TotalPasajeAnt + oDatosNum.TotalPasajeDev) - oDatosNum.TotalComprobadoPasaje);
                    if (sTotalReint > 0)
                    {
                        objDetalles.TotalReintegro = sTotalReint.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                        objDatosRef.Codigo = "PASAJE";
                        objDatosRef.Descripcion = sTotalReint.ToString();
                        ListaReferen.Add(objDatosRef);
                    }
                    else
                    {
                        objDetalles.TotalReintegro = "$0.00";
                    }
                    ListaDetalles.Add(objDetalles);
                }
                if (oDatosNum.TotalCompReintegro > 0)
                {
                    DetallesTotalCom objDetalles = new DetallesTotalCom();
                    Entidad objDatosRef = new Entidad();
                    objDetalles.Concepto = "REINTEGRO";
                    objDetalles.Total = "-" + oDatosNum.TotalCompReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    objDetalles.TotalComprobado = oDatosNum.TotalCompReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));


                    objDetalles.TotalReintegro = "$0.00";
                    objDatosRef.Codigo = "REINTEGRO";
                    objDatosRef.Descripcion = "-" + oDatosNum.TotalCompReintegro.ToString();
                    ListaReferen.Add(objDatosRef);

                    ListaDetalles.Add(objDetalles);
                }

                GvDetalles.DataSource = ListaDetalles;
                GvDetalles.DataBind();

                if (detalleComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS || detalleComision.Forma_Pago_Viaticos == "0")
                {
                    //MODIFICACION DOMINGO
                    GvDetalles.Columns[2].Visible = false;
                    GvDetalles.Columns[3].HeaderText = "TOTAL LIMITE";
                    BtnGeneraRef.Visible = false;
                    divReint.Visible = false;
                    BtnCerrarComp.Visible = true;


                }
                else if (detalleComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_ANTICIPADOS)
                {
                    //MODIFICACION DOMINGO
                    GvDetalles.Columns[3].HeaderText = "TOTAL OTORGADO";
                    BtnGeneraRef.Visible = true;
                    divReint.Visible = true;
                    TbTotalReintegro.Text = lblMonto.Text.ToString();

                    //TotalImporteReintegro = lblMonto.Text.ToString().Replace("$", "");
                    TbTotalReintegro.Enabled = false;

                    if (((oDatosNum.TotalTotalAnt + oDatosNum.TotalTotalDev) - Math.Round(oDatosNum.TotalComprobado, 2)) <= 0)
                    {
                        BtnCerrarComp.Visible = true;

                    }

                    //MODIFICACION PATTY
                    Session["ConceptosRef"] = ListaReferen;


                    //comprobar referencias existentes con respecto al archivo 
                    List<GenerarRef> ListaConcep = new List<GenerarRef>();

                    ListaConcep = MngNegocioGenerarRef.Obten_Informacion_Referencia(detalleComision.Archivo);

                    if ((ListaConcep.Count == 0) || (ListaConcep.Count == null))
                    {
                        //IDMaximo=MngNegocioGenerarRef.Obtiene_Max_Referencia();
                        BtnGeneraRef.Text = "Generar Referencia";  // BOTON NUEVA REF//  
                    }
                    else
                    {
                        foreach (GenerarRef rec in ListaConcep)
                        {
                            if (Convert.ToDouble(rec.Importe) == Math.Round(oDatosNum.TotalImporteReintegro, 2))
                            {
                                BtnGeneraRef.Text = "Descargar Referencia"; // activar btn actualizar//                            
                                BtnGeneraRef.Visible = false;
                                LinkDescarga.Visible = true;
                                LinkDescarga.InnerText = "Descarga Referencia";
                                LinkDescarga.HRef = "~/Descarga.aspx?REFEREN=" + rec.Referencia + ".pdf";
                            }
                            else
                            {
                                BtnGeneraRef.Text = "Actualizar Referencia"; // activar btn actualizar//
                                Actualizar = true;
                                IDMaximo = rec.ID;
                                LinkDescarga.Visible = true;
                                LinkDescarga.InnerText = "Referencia Anterior";
                                //~/
                                LinkDescarga.HRef = "~/Descarga.aspx?REFEREN=" + rec.Referencia + ".pdf";
                                ReferenciaExis = rec.Referencia;
                            }
                        }
                    }
                }

                if (((oDatosNum.TotalTotalAnt + oDatosNum.TotalTotalDev) - oDatosNum.TotalComprobado) <= 0)
                {
                    BtnGeneraRef.Visible = false;
                    LinkDescarga.Visible = false;
                }
                ModalPopupExtender1.Show();



            }




        }
        //MODIFICACION PATTY
        protected void BtnGeneraRef_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {

                List<Entidad> ListConcepto = new List<Entidad>();
                ListConcepto = (List<Entidad>)Session["ConceptosRef"];
                //jalar session referencia y guardar lista para recorrer
                //if (((Actualizar == true) && (Descargar == true)) || ((Actualizar == false) && (Descargar == true)))
                //{
                //Descargar Referencia (PDF)sReferenciaExis

                //string script = "window.open('~/Descarga.aspx?REFEREN=" + sReferenciaExis + ".pdf', '')";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);


                //Response.Redirect("~/Descarga.aspx?REFEREN=" + sReferenciaExis + ".pdf", true);
                //Response.Close();
                //Response.End();
                //}
                if (Actualizar == true)
                {
                    bool EliminarRegistro = MngNegocioGenerarRef.Update_UpdateReferencia(IDMaximo, ReferenciaExis, "0");

                    if (EliminarRegistro)
                    {
                        InsertaDatosDetalles(ListConcepto, IDMaximo);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert(' Referencia no eliminada, Intentelo mas tarde');", true);
                        return;
                    }
                }
                else
                {
                    IDMaximo = MngNegocioGenerarRef.Obtiene_Max_Referencia();
                    InsertaDatosDetalles(ListConcepto, IDMaximo);
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }
        //MODIFICACION PATTY
        private void InsertaDatosDetalles(List<Entidad> ListaConceptosRef, string IdMaximo)
        {
            Comision detalleComision = new Comision();
            detalleComision = (Comision)Session["DetallesComision"];

            string ArchBus = Convert.ToString(detalleComision.Archivo);
            ArchBus = ArchBus.Replace(".PDF", "");
            ArchBus = ArchBus.Replace(".pdf", "");
            ArchBus = ArchBus.Replace(" ", "");

            ArchBus = clsFuncionesGral.ConvertMayus(ArchBus);
            ArchBus = ArchBus + ".pdf";

            string Archivo = ArchBus.ToString();

            string RefBancaria = clsFuncionesGral.Crea_ReferenciaBancaria(Archivo, oDatosNum.TotalImporteReintegro.ToString(), UltimoDia);


            Comision oComision = MngNegocioGenerarRef.Obten_Informacion_Comision(ArchBus);

            string Comisionado = oComision.Comisionado;


            int contador = 0;

            int ContConcep = 0;

            GenerarRef oGenerar = new GenerarRef();

            foreach (Entidad x in ListaConceptosRef)
            {
                contador++;

                oGenerar.ID = IdMaximo.ToString();
                oGenerar.Referencia = RefBancaria.ToString();
                oGenerar.Archivo = ArchBus.ToString();
                oGenerar.Comisionado = Comisionado.ToString();
                oGenerar.Estatus = "1";
                oGenerar.SecEff = contador.ToString();
                oGenerar.Concepto = x.Codigo;
                oGenerar.Importe = x.Descripcion;
                oGenerar.Fecha = lsHoy.ToString();


                bool Ref = MngNegocioGenerarRef.Inserta_Referencia_Detalles(oGenerar);
                if (Ref)
                {
                    ContConcep++;
                }
            }

            GenerarRef oTablaReferencia = new GenerarRef();

            oTablaReferencia.ID = IdMaximo.ToString();
            oTablaReferencia.Referencia = RefBancaria.ToString();
            oTablaReferencia.Archivo = Archivo.ToString();
            oTablaReferencia.Comisionado = Comisionado.ToString();
            oTablaReferencia.Estatus = "1";
            oTablaReferencia.Fecha = lsHoy.ToString();
            oTablaReferencia.FechaVencimiento = Year + "-12-31";
            oTablaReferencia.Importe = oDatosNum.TotalImporteReintegro.ToString();
            oTablaReferencia.SecEff = "1";
            oTablaReferencia.ImportePagado = "0";
            oTablaReferencia.FechaPago = "1900-01-01";


            if (ContConcep == ListaConceptosRef.Count)
            {
                bool GuardarTablaRef = MngNegocioGenerarRef.Inserta_Referencia(oTablaReferencia);

                if (GuardarTablaRef)
                {
                    clsPdf.Genera_ReferenciaPago(IdMaximo, RefBancaria, oDatosNum.TotalImporteReintegro.ToString(), UltimoDia, Archivo, ListaConceptosRef);
                    //"~/Descarga.aspx?REFEREN={0}"
                    LinkDescarga.Visible = true;
                    LinkDescarga.InnerText = "Descarga Referencia Nueva";
                    LinkDescarga.HRef = "~/Descarga.aspx?REFEREN=" + RefBancaria + ".pdf";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert(' Sus datos fueron guardados correctamente');", true);

                    return;
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Intente generarlo nuevamente');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('se han guardado solo " + ContConcep + " conceptos de " + contador + "');", true);

                return;
            }
            Crear_Tabla(detalleComision);
            Calcula_Reintegro(detalleComision);
            detalleComision = null;
        }
        //subir tiket para cambiar metodo pago
        protected void btnModMetodoPago_Click(object sender, EventArgs e)
        {
            bool boolticket = false;
            if (CBoxAcepto.Checked == true)
            {
                if (ticket == "")
                {
                    if (FileUploadTicket.HasFile)
                    {
                        String fileExtension = System.IO.Path.GetExtension(FileUploadTicket.FileName).ToLower();
                        String[] allowedExtensions = { ".pdf", ".PDF" };
                        bool fileticket = false;

                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            if (fileExtension == allowedExtensions[i])
                            {
                                fileticket = true;
                            }
                        }

                        if (fileticket)
                        {
                            ticket = clsFuncionesGral.ConvertMinus(FileUploadTicket.FileName).Replace("pdf", "");
                            boolticket = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                            return;
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Para MODIFICAR su Metodo de pago es necesario subir algun comprobante que compruebe dicha compra');", true);
                        return;
                    }

                }


                Comision DetalleComision = new Comision();
                DetalleComision = (Comision)Session["DetallesComision"];


                bool ActualizaPago;
                if (boolticket == true)
                {
                    Valida_Carpeta(DetalleComision.Ruta, false, true);

                    if (FileUploadTicket.HasFile)
                    {
                        FileUploadTicket.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + FileUploadTicket.FileName);
                    }
                    ActualizaPago = MngNegocioComprobacion.Update_MetodoPagoUsuario(DetalleComision.Comisionado, TimbreFiscal, DetalleComision.Archivo, ticket);
                }
                else
                {
                    ActualizaPago = MngNegocioComprobacion.Update_MetodoPagoUsuario(DetalleComision.Comisionado, TimbreFiscal, DetalleComision.Archivo, "");
                }
                if (ActualizaPago)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);
                    clsFuncionesGral.Activa_Paneles(pnlMetodoPago, false);
                    ticket = "";
                    TimbreFiscal = "";

                }
                Crear_Tabla(DetalleComision);
                Calcula_Reintegro(DetalleComision);
                DetalleComision = null;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Para MODIFICAR su Metodo de pago es necesario aceptar los terminos');", true);
                return;
            }
        }
        //MODIFICACION 01-09-2017
        protected void BtnCerrarComp_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                lblActividadesOb.Text = "";
                InapescaWeb.Entidades.comision_informe cc;
                string[] lsCadena = new string[5];
                Comision poComision = new Comision();
                poComision = (Comision)Session["DetallesComision"];
                lsCadena = poComision.Archivo.ToString().Split(new Char[] { '-' });

               
                cc = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), poComision.Ubicacion_Comisionado, lsCadena[3], poComision.Dep_Proy, lsCadena[4].Replace(".pdf", ""), poComision.Proyecto);

                

                if ((cc.FOLIO != null) & (clsFuncionesGral.FormatFecha(cc.FECHA_FINAL) != Dictionary.FECHA_NULA) || (Session["Crip_Rol"].ToString()) == Dictionary.DIRECTOR_GRAL)
                {
                    Comision DetalleComision = new Comision();
                    DetalleComision = (Comision)Session["DetallesComision"];
                    if (DetalleComision.Estatus == "5")
                    {
                        MngNegocioComprobacion.Update_Comprobacion(DetalleComision.Ubicacion_Comisionado, DetalleComision.Comisionado, DetalleComision.Folio, DetalleComision.Archivo, "5", "");
                        MngNegocioComision.Update_estatus_Comision("5", DetalleComision.Comisionado, DetalleComision.Folio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo, "", true);
                        MngNegocioComision.Update_Status_ComisionDetalle("5", DetalleComision.Folio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);

                    }
                    else 
                    {
                        MngNegocioComprobacion.Update_Comprobacion(DetalleComision.Ubicacion_Comisionado, DetalleComision.Comisionado, DetalleComision.Folio, DetalleComision.Archivo, "7", "");
                        MngNegocioComision.Update_estatus_Comision("7", DetalleComision.Comisionado, DetalleComision.Folio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo, "", true);
                        MngNegocioComision.Update_Status_ComisionDetalle("7", DetalleComision.Folio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);
                    }
                    

                    bool bConAmpl = false;
                    string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);


                    if ((folio_comprobante == Dictionary.NUMERO_CERO))
                    {
                        MngNegocioComision.Insert_Folio_Comprobante(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                        folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);

                        if ((oDatosNum.TotalTotalDev > 0) && (oDatosNum.TotalTotalAnt > 0))
                        {
                            clsPdf.Genera_MinistracionSmaf(DetalleComision, folio_comprobante, oDatosNum.TotalTotalDev, oDatosNum.TotalTotalAnt);
                        }
                        else
                        {
                            clsPdf.Genera_MinistracionSmaf(DetalleComision, folio_comprobante, 0, 0);
                        }
                        bConAmpl = true;

                    }
                    else
                    {
                        if ((oDatosNum.TotalTotalDev > 0) && (oDatosNum.TotalTotalAnt > 0))
                        {
                            clsPdf.Genera_MinistracionSmaf(DetalleComision, folio_comprobante, oDatosNum.TotalTotalDev, oDatosNum.TotalTotalAnt);
                        }
                        else
                        {
                            clsPdf.Genera_MinistracionSmaf(DetalleComision, folio_comprobante, 0, 0);
                        }
                        bConAmpl = true;

                    }

                    ///////////////////////////////////////////////////////////


                    if (bConAmpl == true)
                    {
                        List<Entidad> ListArchivosAmp = new List<Entidad>();
                        ListArchivosAmp = (List<Entidad>)Session["Ampliaciones"];

                        if (ListArchivosAmp != null)
                        {

                            foreach (Entidad r in ListArchivosAmp)
                            {
                                Comision DetalleComisionAmp = new Comision();

                                DetalleComisionAmp = MngNegocioComision.Obten_Detalle(r.Descripcion, Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), DetalleComision.Estatus);

                                if (DetalleComisionAmp.Archivo != null)
                                {
                                    //MngNegocioComprobacion.Update_Comprobacion(DetalleComisionAmp.Ubicacion_Comisionado, DetalleComisionAmp.Comisionado, DetalleComisionAmp.Oficio, DetalleComisionAmp.Archivo, "7", "");
                                    MngNegocioComision.Update_estatus_Comision("7", DetalleComisionAmp.Comisionado, DetalleComisionAmp.Oficio, DetalleComisionAmp.Ubicacion_Comisionado, DetalleComisionAmp.Archivo, "", false);
                                    //MngNegocioComision.Update_Status_ComisionDetalle("7", DetalleComisionAmp.Oficio, DetalleComisionAmp.Archivo, DetalleComisionAmp.Comisionado, DetalleComisionAmp.Ubicacion_Comisionado);

                                    DetalleComisionAmp = null;
                                }
                            }
                        }
                        clsFuncionesGral.Activa_Paneles(PanelTodo, true, false);
                        clsFuncionesGral.Activa_Paneles(pnlPdfDescarga, true);
                        //LinkDescargaMinistracion.InnerText = "DESCARGAR MINISTRACION";
                        //LinkDescargaMinistracion.HRef = "~/Descarga.aspx?Ministracion=" + DetalleComision.Archivo + "|" + DetalleComision.Ubicacion_Comisionado + "|" + DetalleComision.Territorio;
                        
                        LinkDescargaMinistracion.HRef = "~/Descarga.aspx?Ministracion=" + DetalleComision.Archivo + "|" + DetalleComision.Ruta ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('LA MINISTRACION FUE CREADA CON EXITO, FAVOR DE DESCARGAR EN LA PARTE INFERIOR DE LA PANTALLA');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('OCURRIO UN ERROR FAVOR DE ENVIAR UN CORREO A soporte.smaf@gmail.com');", true);
                    }
                }
                else
                {
                    lblActividadesOb.Text = clsFuncionesGral.ConvertMayus("El informe es Obligatorio para poder cerrar la comprobación");

                }



            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }

            ///////////////////////////////////////////////////////////////////////////////////
        }
    }
}