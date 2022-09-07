using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace InapescaWeb.Ministraciones
{
    public partial class Ministracion_2020 : System.Web.UI.Page
    {
        string lsFolio;
        static clsDictionary Dictionary = new clsDictionary();
        string Ruta;
        string UbicacionFile;
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static string UltimoDia = "31-12-" + Year;
        private static DatosComprobacion oDatosNum = new DatosComprobacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Crip_Rol"].ToString() != "ADMCRIPSC")
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Server.MapPath("~/index.aspx"), true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('sin acceso a esta pagina.');", true);
                return;
            }

            if (Session["Crip_Usuario"] == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Server.MapPath("~/index.aspx"), true);
            }
            if (Session["DetallesComision"] != null)
            {
                Comision DetalleComision = new Comision();
                DetalleComision = (Comision)Session["DetallesComision"];
                Carga_Detalle(DetalleComision);
            }
            if (!IsPostBack)
            {
                Session["DetallesComision"] = null;
                //Contadores en Cero de La Comprobacion
                if (Session["Crip_Usuario"] == null)
                {
                    HttpContext.Current.Response.Redirect("index.aspx", true);
                }
                Carga_ValoresInicio();
                clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
            }
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
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Sube_Informe_Comision();


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
        //sube oficio
        public void Sube_Informe_Comision()
        {
            bool fileOk = false;
            string[] lsCadena = new string[2];
            Comision DetalleComision = new Comision();
            bool ExistArchivo = fupdlComision.HasFile;
            if (ExistArchivo)
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

                    MngNegocioComisionDetalle.Insertar_Detalle(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Tipo_Comision, DetalleComision.Fecha_Inicio, DetalleComision.Fecha_Final, DetalleComision.Proyecto, DetalleComision.Dep_Proy, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, DetalleComision.Forma_Pago_Viaticos, DetalleComision.Periodo, DetalleComision.Territorio, fupdlComision.FileName, DetalleComision.Total_Viaticos, DetalleComision.Singladuras, DetalleComision.Combustible_Efectivo, DetalleComision.Peaje, DetalleComision.Pasaje);

                    Valida_Informe(DetalleComision);
                    //   Carga_Detalle();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Oficio de comisión ha subido exitosamente.');", true);
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
            string[] lsCadena = new string[5];
            lsCadena = poComision.Archivo.ToString().Split(new Char[] { '-' });
            cc = MngNegocioComisionDetalle.Obtiene_OficioFirmado(poComision.Comisionado, poComision.Ubicacion_Comisionado, poComision.Oficio, poComision.Dep_Proy, poComision.Periodo);

            if ((cc.FOLIO != null) & (clsFuncionesGral.FormatFecha(cc.FECHA_FINAL) != Dictionary.FECHA_NULA))
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Ya tiene cargado un oficio de comision");
                clsFuncionesGral.Activa_Paneles(pnlOficio, true);
                fupdlComision.Visible = false;
                ImageButton1.Visible = false;
                if (cc.FECHA_AUT == "1900-01-01")
                {
                    btnAccion.Visible = true;
                }
                else if (cc.FECHA_AUT == Dictionary.CADENA_NULA || cc.FECHA_AUT==null)
                {
                    btnAccion.Visible = false;
                }
                else
                {
                    Label6.Text = clsFuncionesGral.ConvertMayus("Ya fue solicitado el pago para este Oficio de comisión");
                }
                


            }
            else
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Carga de Oficio de Comisión:") + "(formato pdf)";

                clsFuncionesGral.Activa_Paneles(pnlOficio, true);
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
        public void Carga_Detalle(Comision poComision)
        {
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
            if ((poComision.Zona_Comercial == "0") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "8") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "12") | (poComision.Zona_Comercial == "14") | (poComision.Zona_Comercial == "15") | (poComision.Zona_Comercial == "19") | (poComision.Zona_Comercial == "7"))
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
                if (poComision.Zona_Comercial != "7")
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

                double tarifaComercial = 0;
                double tarifaRural = 0;
                double tarifaNavegable = 0;
                double tarifa50km = 0;

                double tViatComercial = 0;
                double tViatRural = 0;
                double tViatNavegados = 0;
                double tViat50km = 0;

                switch (poComision.Zona_Comercial)
                {

                    case "16":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        tarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * tarifaRural;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                    case "17":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        tarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * tarifaRural;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                    case "18":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        tarifaNavegable = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("15"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViatNavegados = Convert.ToDouble(poComision.Dias_Navegados) * tarifaNavegable;
                        oDatosNum.TotalViaticosNavegados = oDatosNum.TotalViaticosNavegados + tViatNavegados;
                        break;
                    case "20":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * tarifa50km;
                        break;
                    case "21":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * tarifa50km;
                        break;
                    case "22":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        tarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * tarifaRural;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * tarifa50km;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                    case "23":
                        tarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        tarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * tarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * tarifaRural;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * tarifa50km;
                        oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + tViatRural;
                        break;
                }

                string FechaComision = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;

                if (tViatComercial != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, tarifaComercial, poComision.Dias_Comercial, tViatComercial);
                    consecutivo += 1;
                }
                if (tViatRural != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, tarifaRural, poComision.Dias_Rural, tViatRural);
                    consecutivo += 1;
                }
                if (tViatNavegados != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, tarifaNavegable, poComision.Dias_Navegados, tViatNavegados);
                    consecutivo += 1;
                }
                if (tViat50km != 0)
                {
                    InsertarTrTable(consecutivo, poComision.Lugar, FechaComision, tarifa50km, poComision.Dias_50, tViat50km);
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

                    DetalleComisionAmp = MngNegocioComision.Obten_Detalle(r.Descripcion, poComision.Ubicacion_Comisionado, poComision.Comisionado, "9");

                    if (DetalleComisionAmp.Archivo != null)
                    {
                        if ((DetalleComisionAmp.Zona_Comercial == "0") | (DetalleComisionAmp.Zona_Comercial == "2") | (DetalleComisionAmp.Zona_Comercial == "4") | (DetalleComisionAmp.Zona_Comercial == "6") | (DetalleComisionAmp.Zona_Comercial == "8") | (DetalleComisionAmp.Zona_Comercial == "10") | (DetalleComisionAmp.Zona_Comercial == "12") | (DetalleComisionAmp.Zona_Comercial == "14") | (DetalleComisionAmp.Zona_Comercial == "15") | (DetalleComisionAmp.Zona_Comercial == "19"))
                        {
                            string FechaComision = DetalleComisionAmp.Fecha_Inicio + " al " + DetalleComisionAmp.Fecha_Final;
                            double TViaticos = Convert.ToDouble(DetalleComisionAmp.Total_Viaticos);
                            double TarifaDias = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa(DetalleComisionAmp.Zona_Comercial));

                            InsertarTrTable(consecutivo, DetalleComisionAmp.Lugar, FechaComision, TarifaDias, DetalleComisionAmp.Dias_Reales, TViaticos);
                            consecutivo += 1;


                            if (DetalleComisionAmp.Zona_Comercial == "14")
                            {
                                oDatosNum.TotalViaticosRurales = oDatosNum.TotalViaticosRurales + Convert.ToDouble(DetalleComisionAmp.Total_Viaticos);
                            }
                            if (DetalleComisionAmp.Zona_Comercial == "15")
                            {
                                oDatosNum.TotalViaticosNavegados = oDatosNum.TotalViaticosNavegados + Convert.ToDouble(DetalleComisionAmp.Singladuras);
                            }

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
        public void Carga_Valores(Comision poComision)
        {

            string declaracion = "Se hace de su conocimiento que las comprobaciones serán validadas por el área financiera  con respaldo de los estados de cuenta débito,";
            declaracion += "tickets,  en caso de infringir en falsedad de datos con respecto a su comprobación.";
            declaracion += " El área correspondiente será la encargada de  tomar las medidas necesarias, conforme al artículo 93,";
            declaracion += " Fracc. XVII. Podrán no presentar comprobantes fiscales hasta por un 20% del total de viáticos";
            declaracion += " erogados cuando no existan servicios para emitir los mismos, sin que en ningún caso el monto que no se compruebe ";
            declaracion += "exceda de $15,000.00 en el ejercicio fiscal de que se trate siempre que el monto restante de los viáticos se eroguen mediante tarjeta de crédito, de débito ";
            declaracion += "o de servicio del patrón. La parte que en su caso no se erogue deberá ser reintegrada por la persona física que reciba los viáticos o en caso contrario no le será aplicable lo dispuesto en este artículo.";


            Label1.Text = clsFuncionesGral.ConvertMayus("Nombre del servidor público comisionado:");

            //nOMBRE DEL COMISIONADO
            lblNombres.Text = MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado);

            Label2.Text = clsFuncionesGral.ConvertMayus("Objeto de la comision : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Oficio de Comisión firmado :");
            lblActividades.Text = "Cargar Oficio de Comisión firmado";
            lblObjetivo.Text = clsFuncionesGral.ConvertMayus(poComision.Objetivo);
            Label4.Text = clsFuncionesGral.ConvertMayus("Evaluacion :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Comprobación de comisión oficio número :") + poComision.Archivo.Replace(".pdf", "");
            
            

            Valida_Informe(poComision);

            
        }
        public void Carga_ValoresInicio()
        {

            clsFuncionesGral.Activa_Paneles(PanelTodo, false);


            //DdlCrip.DataSource = MngNegocioDependencia.ListaSiglasDependencias(Year);
            //DdlCrip.DataTextField = Dictionary.DESCRIPCION;
            //DdlCrip.DataValueField = Dictionary.CODIGO;
            //DdlCrip.DataBind();

            TbCRIP.Text = MngNegocioDependencia.Obtiene_Siglas(Session["Crip_Ubicacion"].ToString());
            TbCRIP.Enabled = false;
            TbArchivo2.Enabled = false;
            TbArchivo1.Enabled = false;
            DdlPeriodo.DataSource = MngNegocioAnio.ObtieneAnios();
            DdlPeriodo.DataTextField = Dictionary.DESCRIPCION;
            DdlPeriodo.DataValueField = Dictionary.CODIGO;
            DdlPeriodo.DataBind();
            DdlPeriodo.SelectedValue = Year;
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            
        }
        public void Valida_Carpeta(string psRuta, bool pbInforme = false, bool pbComprobacionFiscales = false, bool pbOtros = false, bool pbComprobacionNoFiscales = false)
        {
            Ruta = "";
            UbicacionFile = "";
            string raiz = HttpContext.Current.Server.MapPath("..");
            if (pbInforme)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.OFICIO_FIRMADO)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.OFICIO_FIRMADO);
                //Ruta = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.OFICIO_FIRMADO;
                //UbicacionFile = psRuta + "/" + Dictionary.INFORME;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.OFICIO_FIRMADO;
            }


        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            
            Session["DetallesComision"] = null;
            //
            string sArchivo = clsFuncionesGral.ConvertMayus(TbArchivo1.Text.ToString() + "-" + TbArchivo2.Text.ToString() + "-" + TbCRIP.Text.ToString() + "-" + TbArchivo3.Text.ToString() + "-" + DdlPeriodo.SelectedValue.ToString()) + ".pdf";

            Comision oComision = MngNegocioGenerarRef.Obten_Informacion_Comision(sArchivo);

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(oComision.Archivo, oComision.Ubicacion_Comisionado, oComision.Comisionado, oComision.Estatus);
            Session["DetallesComision"] = DetalleComision;


            if (oComision.Comisionado != "0")
            {

                oDatosNum = new DatosComprobacion();
                Carga_Detalle(DetalleComision);
                Carga_Valores(DetalleComision);
                Valida_Carpeta(DetalleComision.Ruta);
                btnAccion.Text= clsFuncionesGral.ConvertMayus("Agregar Solicitud de Pago");
                btnAccion.Visible = false;
                Label6.Visible = false;
                InapescaWeb.Entidades.comision_informe cc;
                cc = MngNegocioComisionDetalle.Obtiene_OficioFirmado(DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, DetalleComision.Oficio, DetalleComision.Dep_Proy, DetalleComision.Periodo);

                if (cc.FECHA_AUT == "1900-01-01")
                {
                    btnAccion.Visible = true;
                }
                else if (cc.FECHA_AUT == Dictionary.CADENA_NULA || cc.FECHA_AUT == null)
                {
                    btnAccion.Visible = false;
                    fupdlComision.Visible = true;
                    ImageButton1.Visible = true;
                }
                else
                {
                    Label6.Text = clsFuncionesGral.ConvertMayus("Ya fue solicitado el pago para este Oficio de comisión");
                }
                clsFuncionesGral.Activa_Paneles(PanelTodo, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(PanelTodo, false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('FAVOR DE VERIFICAR EL NOMBRE DEL ARCHIVO.');", true);
                return;
            }
        }
        protected void btnAccion_Click(object sender, EventArgs e)
        {
            Session["DetallesComision"] = null;
            string sArchivo = clsFuncionesGral.ConvertMayus(TbArchivo1.Text.ToString() + "-" + TbArchivo2.Text.ToString() + "-" + TbCRIP.Text.ToString() + "-" + TbArchivo3.Text.ToString() + "-" + DdlPeriodo.SelectedValue.ToString()) + ".pdf";
            Comision oComision = MngNegocioGenerarRef.Obten_Informacion_Comision(sArchivo);
            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(oComision.Archivo, oComision.Ubicacion_Comisionado, oComision.Comisionado, oComision.Estatus);
            Session["DetallesComision"] = DetalleComision;

            if(DetalleComision.Forma_Pago_Viaticos== Dictionary.VIATICOS_ANTICIPADOS)
            {


            }
        }
    }
}