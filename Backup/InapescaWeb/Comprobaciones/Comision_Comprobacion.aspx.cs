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

namespace InapescaWeb.Comprobaciones
{
    public partial class Comision_Comprobacion : System.Web.UI.Page
    {
        string lsFolio;

        //  Comision DetalleComision = new Comision();
        static clsDictionary Dictionary = new clsDictionary();

        string Ruta;
        string UbicacionFile;
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] lsCadena = new string[2];
            lsFolio = Request.QueryString["folio"];
            lsCadena = lsFolio.Split(new Char[] { '|' });

            Comision DetalleComision = new Comision();

            DetalleComision = MngNegocioComision.Obten_Detalle(lsCadena[0] + ".pdf", Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), lsCadena[1]);

            if (Convert.ToDateTime(DetalleComision.Fecha_Inicio) > Convert.ToDateTime(lsHoy))
            {
                DetalleComision = null;
                Response.Redirect("Menu_Comprobaciones.aspx", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    Session["Crip_Folio"] = lsCadena[0] + ".pdf";
                    Session["Crip_Estatus"] = lsCadena[1];
                    Crear_Tabla();
                    Carga_Valores(DetalleComision);
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());
                    Valida_Carpeta(DetalleComision.Ruta);
                }

                Carga_Detalle(DetalleComision);
            }

        }

        public void Carga_Detalle(Comision poComision)
        {
            double total = clsFuncionesGral.Convert_Double(Dictionary.NUMERO_CERO);
            int consecutivo = 1;

            TableRow tbencabezado = new TableRow();

            TableCell TcNum = new TableCell();
            TcNum.Text = clsFuncionesGral.ConvertMayus("num.");
            tbencabezado.Cells.Add(TcNum);

            TableCell TcLugar = new TableCell();
            TcLugar.Text = clsFuncionesGral.ConvertMayus("lugar de la comisión");
            tbencabezado.Cells.Add(TcLugar);


            TableCell TcPeriodo = new TableCell();
            TcPeriodo.Text = clsFuncionesGral.ConvertMayus("periodo de la comisión");
            tbencabezado.Cells.Add(TcPeriodo);

            TableCell TcCuota = new TableCell();
            TcCuota.Text = clsFuncionesGral.ConvertMayus("cuota diaria");
            tbencabezado.Cells.Add(TcCuota);

            TableCell TcDias = new TableCell();
            TcDias.Text = clsFuncionesGral.ConvertMayus("dias");
            tbencabezado.Cells.Add(TcDias);

            TableCell TcTotal = new TableCell();
            TcTotal.Text = clsFuncionesGral.ConvertMayus("importe OTORGADO");
            tbencabezado.Cells.Add(TcTotal);

            tblDetalle.Rows.Add(tbencabezado);

            //agregar datos de comsion por zonas comerciales sin combinacion 
            /*
             * zona 2 investigadores 980
             * zona 4 mandos medios 1700
             * zona 6 alta responsabilidad 3240
             * zona 10 medio dia investigadores 490
             * zona 12 medio dia mandos medios 850
             * zona 14 rural 550
             * zona 15 singladuras
             * zona 19 alimentacion en campo 250
             */

            if ((poComision.Zona_Comercial == "0"))
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tc4.Text = "$ " + clsFuncionesGral.Convert_Decimales(MngNegocioComision.Obtiene_Tarifa(poComision.Zona_Comercial));
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Reales;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos);
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }

                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO);//clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) * 0.10)));
                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));//clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) * 0.10))));

                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));

                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });

                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }

                //   double reintegra = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));


                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());

                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                Label20.Visible = true;
                Label9.Visible = true;

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    //   clsFuncionesGral.Activa_Paneles(Panel3, false);
                    //    clsFuncionesGral.Activa_Paneles(pnlCertificado, false);
                    //  clsFuncionesGral.Activa_Paneles(pnlDespacho, false);
                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    //  clsFuncionesGral.Activa_Paneles(Panel3, false);
                }
                //  clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                clsFuncionesGral.Activa_Paneles(pnlCertificado, false);
                clsFuncionesGral.Activa_Paneles(pnlDespacho, false);
            }


            if ((poComision.Zona_Comercial == "1") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "3") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "5") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "11") | (poComision.Zona_Comercial == "12") | (poComision.Zona_Comercial == "13") | (poComision.Zona_Comercial == "14") | (poComision.Zona_Comercial == "15") | (poComision.Zona_Comercial == "19"))
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tc4.Text = "$ " + clsFuncionesGral.Convert_Decimales(MngNegocioComision.Obtiene_Tarifa(poComision.Zona_Comercial));
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Reales;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos);
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }

                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) * 0.10)));

                if ((poComision.Zona_Comercial == "1") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "3") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "5") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "11") | (poComision.Zona_Comercial == "12") | (poComision.Zona_Comercial == "13"))
                {
                    Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) * 0.10))));
                }
                else if ((poComision.Zona_Comercial == "14") | (poComision.Zona_Comercial == "15") | (poComision.Zona_Comercial == "19"))
                {
                    Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));//clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) * 0.10))));
                }

                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));

                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });


                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12','18'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }

                //   double reintegra = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));
                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));
                //  double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena [3])));

                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }
                /*
                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                    clsFuncionesGral.Activa_Paneles(pnlCertificado, false);
                    clsFuncionesGral.Activa_Paneles(pnlDespacho, false);
                }*/

            }

            if ((poComision.Zona_Comercial == "16"))
            {
                //primer fila de datos de zona de investigadores
                string tarifa1, tarifa2;

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("2");
                tc4.Text = clsFuncionesGral.Convert_Decimales(tarifa1);
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Comercial;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)));
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                //segunda linea de datos de zona rural

                TableRow tzr = new TableRow();
                TableCell tcz1 = new TableCell();
                tcz1.Text = consecutivo.ToString();
                consecutivo += 1;
                tzr.Cells.Add(tcz1);

                TableCell tcz2 = new TableCell();
                tcz2.Text = poComision.Lugar;
                tzr.Cells.Add(tcz2);


                TableCell tcz3 = new TableCell();
                tcz3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tzr.Cells.Add(tcz3);

                TableCell tcz4 = new TableCell();
                tarifa2 = MngNegocioComision.Obtiene_Tarifa("14");
                tcz4.Text = clsFuncionesGral.Convert_Decimales(tarifa2);
                tzr.Cells.Add(tcz4);

                TableCell tcz5 = new TableCell();
                tcz5.Text = poComision.Dias_Rural;
                tzr.Cells.Add(tcz5);

                TableCell tcz6 = new TableCell();
                tcz6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Rural) * clsFuncionesGral.Convert_Double(tarifa2)));
                tzr.Cells.Add(tcz6);

                tblDetalle.Rows.Add(tzr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }


                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));

                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10)));

                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));

                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });

                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }

                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));
                // double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena[3])));

                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));


                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != Dictionary.NUMERO_CERO) | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;

                        Label20.Visible = true;
                        Label9.Visible = true;
                    }
                    else
                    {
                        Label20.Visible = false;
                        Label9.Visible = false;
                        Panel3.Visible = false;
                    }
                }
                else
                {

                    Label20.Visible = true;
                    Label9.Visible = true;
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                }


            }

            if ((poComision.Zona_Comercial == "17"))
            {
                //primer fila de datos de zona de investigadores
                string tarifa1, tarifa2;

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("4");
                tc4.Text = clsFuncionesGral.Convert_Decimales(tarifa1);
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Comercial;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)));
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                //segunda linea de datos de zona rural

                TableRow tzr = new TableRow();
                TableCell tcz1 = new TableCell();
                tcz1.Text = consecutivo.ToString();
                consecutivo += 1;
                tzr.Cells.Add(tcz1);

                TableCell tcz2 = new TableCell();
                tcz2.Text = poComision.Lugar;
                tzr.Cells.Add(tcz2);


                TableCell tcz3 = new TableCell();
                tcz3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tzr.Cells.Add(tcz3);

                TableCell tcz4 = new TableCell();
                tarifa2 = MngNegocioComision.Obtiene_Tarifa("14");
                tcz4.Text = clsFuncionesGral.Convert_Decimales(tarifa2);
                tzr.Cells.Add(tcz4);

                TableCell tcz5 = new TableCell();
                tcz5.Text = poComision.Dias_Rural;
                tzr.Cells.Add(tcz5);

                TableCell tcz6 = new TableCell();
                tcz6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Rural) * clsFuncionesGral.Convert_Double(tarifa2)));
                tzr.Cells.Add(tcz6);

                tblDetalle.Rows.Add(tzr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }


                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));
                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10)));


                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));
                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });

                // double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsCadena [3])));

                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }

                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));

                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                }

            }


            if ((poComision.Zona_Comercial == "18"))
            {
                //primer fila de datos de zona de investigadores
                string tarifa1, tarifa2;

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("2");
                tc4.Text = clsFuncionesGral.Convert_Decimales(tarifa1);
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Comercial;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)));
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                //segunda linea de datos de zona rural

                TableRow tzr = new TableRow();
                TableCell tcz1 = new TableCell();
                tcz1.Text = consecutivo.ToString();
                consecutivo += 1;
                tzr.Cells.Add(tcz1);

                TableCell tcz2 = new TableCell();
                tcz2.Text = poComision.Lugar;
                tzr.Cells.Add(tcz2);


                TableCell tcz3 = new TableCell();
                tcz3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tzr.Cells.Add(tcz3);

                TableCell tcz4 = new TableCell();
                tarifa2 = MngNegocioComision.Obtiene_Tarifa("15");
                tcz4.Text = clsFuncionesGral.Convert_Decimales(tarifa2);
                tzr.Cells.Add(tcz4);

                TableCell tcz5 = new TableCell();
                tcz5.Text = poComision.Dias_Rural;
                tzr.Cells.Add(tcz5);

                TableCell tcz6 = new TableCell();
                tcz6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Navegados) * clsFuncionesGral.Convert_Double(tarifa2)));
                tzr.Cells.Add(tcz6);

                tblDetalle.Rows.Add(tzr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }


                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));
                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10)));


                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));

                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });

                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }


                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));
                // double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsCadena [3])));

                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                }

            }


            if ((poComision.Zona_Comercial == "20"))
            {
                //primer fila de datos de zona de investigadores
                string tarifa1, tarifa2;

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("2");
                tc4.Text = clsFuncionesGral.Convert_Decimales(tarifa1);
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Comercial;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)));
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                //segunda linea de datos de zona rural

                TableRow tzr = new TableRow();
                TableCell tcz1 = new TableCell();
                tcz1.Text = consecutivo.ToString();
                consecutivo += 1;
                tzr.Cells.Add(tcz1);

                TableCell tcz2 = new TableCell();
                tcz2.Text = poComision.Lugar;
                tzr.Cells.Add(tcz2);


                TableCell tcz3 = new TableCell();
                tcz3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tzr.Cells.Add(tcz3);

                TableCell tcz4 = new TableCell();
                tarifa2 = MngNegocioComision.Obtiene_Tarifa("19");
                tcz4.Text = clsFuncionesGral.Convert_Decimales(tarifa2);
                tzr.Cells.Add(tcz4);

                TableCell tcz5 = new TableCell();
                tcz5.Text = poComision.Dias_Rural;
                tzr.Cells.Add(tcz5);

                TableCell tcz6 = new TableCell();
                tcz6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_50) * clsFuncionesGral.Convert_Double(tarifa2)));
                tzr.Cells.Add(tcz6);

                tblDetalle.Rows.Add(tzr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }


                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));
                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10)));


                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));

                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });
                // double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena [3])));

                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }


                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));
                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                }

            }



            if ((poComision.Zona_Comercial == "21"))
            {
                //primer fila de datos de zona de investigadores
                string tarifa1, tarifa2;

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("4");
                tc4.Text = clsFuncionesGral.Convert_Decimales(tarifa1);
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Comercial;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)));
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                //segunda linea de datos de zona rural

                TableRow tzr = new TableRow();
                TableCell tcz1 = new TableCell();
                tcz1.Text = consecutivo.ToString();
                consecutivo += 1;
                tzr.Cells.Add(tcz1);

                TableCell tcz2 = new TableCell();
                tcz2.Text = poComision.Lugar;
                tzr.Cells.Add(tcz2);


                TableCell tcz3 = new TableCell();
                tcz3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tzr.Cells.Add(tcz3);

                TableCell tcz4 = new TableCell();
                tarifa2 = MngNegocioComision.Obtiene_Tarifa("19");
                tcz4.Text = clsFuncionesGral.Convert_Decimales(tarifa2);
                tzr.Cells.Add(tcz4);

                TableCell tcz5 = new TableCell();
                tcz5.Text = poComision.Dias_Rural;
                tzr.Cells.Add(tcz5);

                TableCell tcz6 = new TableCell();
                tcz6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_50) * clsFuncionesGral.Convert_Double(tarifa2)));
                tzr.Cells.Add(tcz6);

                tblDetalle.Rows.Add(tzr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }


                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));
                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10)));


                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));
                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });

                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }


                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'")))); // double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena [3])));

                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                }

            }


            if ((poComision.Zona_Comercial == "22") | (poComision.Zona_Comercial == "23"))
            {
                //primer fila de datos de zona de investigadores
                string tarifa1 = "", tarifa2, tarifa3;

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = consecutivo.ToString();
                consecutivo += 1;
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = poComision.Lugar;
                tr.Cells.Add(tc2);


                TableCell tc3 = new TableCell();
                tc3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();


                if (poComision.Zona_Comercial == "22")
                {
                    tarifa1 = MngNegocioComision.Obtiene_Tarifa("2");
                }
                else if (poComision.Zona_Comercial == "23")
                {
                    tarifa1 = MngNegocioComision.Obtiene_Tarifa("4");
                }

                tc4.Text = clsFuncionesGral.Convert_Decimales(tarifa1);
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                tc5.Text = poComision.Dias_Comercial;
                tr.Cells.Add(tc5);

                TableCell tc6 = new TableCell();
                tc6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)));
                tr.Cells.Add(tc6);

                tblDetalle.Rows.Add(tr);

                //segunda linea de datos de zona rural

                TableRow tzr = new TableRow();
                TableCell tcz1 = new TableCell();
                tcz1.Text = consecutivo.ToString();
                consecutivo += 1;
                tzr.Cells.Add(tcz1);

                TableCell tcz2 = new TableCell();
                tcz2.Text = poComision.Lugar;
                tzr.Cells.Add(tcz2);


                TableCell tcz3 = new TableCell();
                tcz3.Text = poComision.Fecha_Inicio + " al " + poComision.Fecha_Final;
                tzr.Cells.Add(tcz3);

                TableCell tcz4 = new TableCell();
                tarifa2 = MngNegocioComision.Obtiene_Tarifa("19");
                tcz4.Text = clsFuncionesGral.Convert_Decimales(tarifa2);
                tzr.Cells.Add(tcz4);

                TableCell tcz5 = new TableCell();
                tcz5.Text = poComision.Dias_Rural;
                tzr.Cells.Add(tcz5);

                TableCell tcz6 = new TableCell();
                tcz6.Text = " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Dias_50) * clsFuncionesGral.Convert_Double(tarifa2)));
                tzr.Cells.Add(tcz6);

                tblDetalle.Rows.Add(tzr);

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tc11 = new TableCell();
                    tc11.ColumnSpan = 5;
                    tc11.HorizontalAlign = HorizontalAlign.Right;
                    tc11.Text = clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo");
                    tr1.Cells.Add(tc11);

                    TableCell tc12 = new TableCell();
                    tc12.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo);
                    tr1.Cells.Add(tc12);

                    tblDetalle.Rows.Add(tr1);
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.ColumnSpan = 5;
                    tc21.HorizontalAlign = HorizontalAlign.Right;
                    tc21.Text = clsFuncionesGral.ConvertMayus("peaje");
                    tr2.Cells.Add(tc21);

                    TableCell tc22 = new TableCell();
                    tc22.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
                    tr2.Cells.Add(tc22);

                    tblDetalle.Rows.Add(tr2);
                }


                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {

                    TableRow tr3 = new TableRow();
                    TableCell tc31 = new TableCell();
                    tc31.ColumnSpan = 5;
                    tc31.HorizontalAlign = HorizontalAlign.Right;
                    tc31.Text = clsFuncionesGral.ConvertMayus("pasaje");
                    tr3.Cells.Add(tc31);

                    TableCell tc32 = new TableCell();
                    tc32.Text = " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
                    tr3.Cells.Add(tc32);

                    tblDetalle.Rows.Add(tr3);
                }

                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                TableRow tr5 = new TableRow();
                TableCell tc51 = new TableCell();
                tc51.ColumnSpan = 5;
                tc51.HorizontalAlign = HorizontalAlign.Right;
                tc51.Text = clsFuncionesGral.ConvertMayus("efectivo total otorgado");
                tr5.Cells.Add(tc51);

                TableCell tc52 = new TableCell();
                tc52.Text = " $ " + clsFuncionesGral.Convert_Decimales(total.ToString());
                tr5.Cells.Add(tc52);

                tblDetalle.Rows.Add(tr5);


                TableRow tr4 = new TableRow();
                TableCell tc41 = new TableCell();
                tc41.ColumnSpan = 6;
                tc41.HorizontalAlign = HorizontalAlign.Center;
                tc41.Font.Bold = true;
                tc41.Text = clsFuncionesGral.ConvertMayus("recibí en efectivo la cantidad de :");
                tr4.Cells.Add(tc41);

                tblDetalle.Rows.Add(tr4);

                TableRow tr6 = new TableRow();
                TableCell tc61 = new TableCell();
                //tc61.ColumnSpan = 6;
                //tc61.HorizontalAlign = HorizontalAlign.Center;
                //tc61.Font.Bold = true;
                tc61.Text = "$" + clsFuncionesGral.Convert_Decimales(total.ToString()); //clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(total)));

                tr6.Cells.Add(tc61);

                TableCell tc62 = new TableCell();
                tc62.ColumnSpan = 5;
                tc62.HorizontalAlign = HorizontalAlign.Center;
                tc62.Font.Bold = true;
                tc62.Text = clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true);

                tr6.Cells.Add(tc62);

                tblDetalle.Rows.Add(tr6);


                TableRow tr7 = new TableRow();
                TableCell tc72 = new TableCell();
                tc72.ColumnSpan = 6;
                tc72.HorizontalAlign = HorizontalAlign.Center;
                tc72.Text = "Se deberá comporbar de manera fisica y con ticket de carga de gasolina la cantda de : $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales) + ", entregada en vales  del folio :" + poComision.Vale_Comb_I + " al " + poComision.Vale_Comb_F;
                tr7.Cells.Add(tc72);

                tblDetalle.Rows.Add(tr7);
                //LEYENDA DEL 10% CALCULADO
                Label15.Text = clsFuncionesGral.ConvertMayus("Comprobacion del 10 % del total de viaticos :  ") + " $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));
                Label19.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION SIN REQUISITOS FISCALES 10% de total de viaticos : $" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(poComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10)));


                double x = 0;

                x = (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje));
                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });


                double viaticos = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'5','9','11','14','15','17','12'"));

                if (viaticos >= (clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Singladuras)))
                {
                    viaticos = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos);
                }

                double peaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'7','16'"));

                if (peaje >= clsFuncionesGral.Convert_Double(poComision.Peaje))
                {
                    peaje = clsFuncionesGral.Convert_Double(poComision.Peaje);
                }

                double combustible = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'6'"));

                if (combustible >= clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))
                {
                    combustible = clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo);
                }

                double pasaje = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'8'"));

                if (pasaje >= clsFuncionesGral.Convert_Double(poComision.Pasaje))
                {
                    pasaje = clsFuncionesGral.Convert_Double(poComision.Pasaje);
                }


                double reintegro = (x - clsFuncionesGral.Convert_Double(viaticos + combustible + peaje + pasaje + clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"))));
                //   double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsCadena [3])));

                Label20.Text = clsFuncionesGral.ConvertMayus("REINTEGRO A EFECTUAR: ") + Dictionary.PESOS + clsFuncionesGral.Convert_Decimales(reintegro.ToString());
                Label9.Text = clsFuncionesGral.ConvertMayus(" Reintegro Efectuado :") + "$ " + clsFuncionesGral.Convert_Decimales(MngDatosComprobacion.Total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), poComision.Archivo, "'13'"));

                string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(poComision.Oficio, poComision.Archivo, poComision.Comisionado);

                if (reintegro > 0)
                {
                    if (((folio_comprobante != "") | (folio_comprobante != null)) & (poComision.Forma_Pago_Viaticos == "2"))
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel3.Visible = false;
                }

                if ((clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())) >= clsFuncionesGral.Convert_Double(x)) & poComision.Estatus == "9")
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true, false);
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(Panel3, false);
                }
            }

        }

        public void Crear_Tabla()
        {
            gvFiscales.DataSource = MngNegocioComprobacion.ListaComprobaciones(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Folio"].ToString(), "2").Tables[0];
            gvFiscales.DataBind();

            gvNofiscales.DataSource = MngNegocioComprobacion.ListaComprobaciones(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Folio"].ToString(), "3").Tables[0];
            gvNofiscales.DataBind();
        }

        public void Carga_Valores(Comision poComision)
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            Label6.Visible = false;
            Label1.Text = clsFuncionesGral.ConvertMayus("Nombre del servidor público comisionado:");
            lblNombres.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            Label2.Text = clsFuncionesGral.ConvertMayus("Objeto de la comision : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Principales actividades desarrolladas :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Evaluacion :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Comprobación de comisión oficio número :") + poComision.Archivo.Replace(".pdf", "");

            lblObjetivo.Text = poComision.Objetivo;
            Label13.Text = clsFuncionesGral.ConvertMayus("CONCEPTO comprobante :");
            Label14.Text = clsFuncionesGral.ConvertMayus("Tipo Comprobantes :");
            Label16.Text = clsFuncionesGral.ConvertMayus("concepto");
            Label17.Text = clsFuncionesGral.ConvertMayus("importe");
            Label18.Text = clsFuncionesGral.ConvertMayus("observaciones");
            Label11.Text = clsFuncionesGral.ConvertMayus("relación de gastos");
            Label12.Text = clsFuncionesGral.ConvertMayus("DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACION CON REQUISITOS FISCALES");
            Label21.Text = clsFuncionesGral.ConvertMayus("Monto de  Reintegro (.jpg) ");
            Label10.Text = clsFuncionesGral.ConvertMayus("Atenta Nota firmada y tickets escaneados formato pdf.");
            Label22.Text = clsFuncionesGral.ConvertMayus("Importe total Peajes : $ ");
            Label23.Text = clsFuncionesGral.ConvertMayus("Escaneo de tickets en formato PDF que avalan factura ") + " (Opcional)";
            Label24.Text = clsFuncionesGral.ConvertMayus("Certificado de transito :");
            Label25.Text = clsFuncionesGral.ConvertMayus("Despacho de capitania de puerto:");
            LinkButton1.Text = clsFuncionesGral.ConvertMayus("Agregar Certificado de transito");
            LinkButton2.Text = clsFuncionesGral.ConvertMayus("Agregar despacho de Singladuras");
            lnkAgregaAttNota.Text = clsFuncionesGral.ConvertMayus("AGregar atenta Nota.");
            Valida_Informe(poComision);

            Button1.Text = clsFuncionesGral.ConvertMayus("Salvar avance Comprobación");
            Button2.Text = clsFuncionesGral.ConvertMayus("Cerrar Comprobacion");

            fuplPDF.Dispose();
            fuplXML.Dispose();
            clsFuncionesGral.Llena_Lista(dplFiscales, clsFuncionesGral.ConvertMayus("= s e l e c ci o n e =|fiscales|no fiscales"));
            clsFuncionesGral.Activa_Paneles(Panel1, true);//tabla detalle

            if ((poComision.Zona_Comercial == "1") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "3") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "5") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "11") | (poComision.Zona_Comercial == "12") | (poComision.Zona_Comercial == "13") | (poComision.Zona_Comercial == "19"))
            {
                clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, false);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales
            }
            else if (poComision.Zona_Comercial == "14")
            {

                clsFuncionesGral.Activa_Paneles(pnlFiscales, false);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, true);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                }

                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                }


            }
            else if (poComision.Zona_Comercial == "15")
            {
                clsFuncionesGral.Activa_Paneles(pnlFiscales, false);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, true);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, false);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales

                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                }

                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                }

            }
            else if ((poComision.Zona_Comercial == "16") | (poComision.Zona_Comercial == "17"))
            {
                clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, true);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales
            }
            else if ((poComision.Zona_Comercial == "18") | (poComision.Zona_Comercial == "24"))
            {
                clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, true);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, false);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales
            }
            else if ((poComision.Zona_Comercial == "20") | (poComision.Zona_Comercial == "21"))
            {
                clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, false);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales
            }
            else if ((poComision.Zona_Comercial == "22") | (poComision.Zona_Comercial == "23"))
            {
                clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal

                clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                clsFuncionesGral.Activa_Paneles(pnlCertificado, true);//certidicafo para rural

                clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales

                clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales
            }

            if ((poComision.Forma_Pago_Viaticos == "1") | (poComision.Forma_Pago_Viaticos == "0"))
            {
                Label20.Visible = false;
                Label9.Visible = false;
                clsFuncionesGral.Activa_Paneles(Panel3, false);//no fiscales
            }
            else
            {
                Label20.Visible = true;
                Label9.Visible = true;
                clsFuncionesGral.Activa_Paneles(Panel3, true);//no fiscales
            }
            Label7.Text = clsFuncionesGral.ConvertMayus("archivo pdf");
            Label8.Text = clsFuncionesGral.ConvertMayus("archivo Xml");
            lnkAddFacturas.Text = "Agregar factura";
            lnkAgregaNoFiscal.Text = "Agregar importe";

            dplConcepto.DataSource = MngNegocioComision.ObtieneTipoComprobacion(true);
            dplConcepto.DataTextField = "Descripcion";
            dplConcepto.DataValueField = "Codigo";
            dplConcepto.DataBind();

        }

        public void Valida_Informe(Comision poComision)
        {
            InapescaWeb.Entidades.comision_informe cc;
            string[] lsCadena = new string[5];
            lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });
            cc = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena[3], poComision.Ubicacion, lsCadena[4].Replace(".pdf", ""));

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

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
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

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Sube_Informe_Comision();
        }

        public void Sube_Informe_Comision()
        {
            bool fileOk = false;
            string[] lsCadena = new string[2];
            Comision DetalleComision = new Comision();
            if (fupdlComision.HasFile)
            {
                DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

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

                    MngNegocioComision.Inserta_Informe_Comision(DetalleComision.Oficio, DetalleComision.Ubicacion, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "1", fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, lsHoy, lsHoy, DetalleComision.Periodo);

                    Valida_Informe(DetalleComision);
                    //   Carga_Detalle();
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Informe de comisión ha subido exitosamente.');", true);
                    return;

                }
                catch (Exception ex)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
                    return;
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }

        }

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

        public void Valida_CarpetaXML()
        {
            string raiz = HttpContext.Current.Server.MapPath("..");
            if (!Directory.Exists(raiz + "\\" + " XML")) Directory.CreateDirectory(raiz + "\\" + " XML"); ;
            Session["Crip_Ruta"] = raiz + "\\" + " XML";
        }

        protected void dplFiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsTipoComprobante = dplFiscales.SelectedValue.ToString();

            switch (lsTipoComprobante)
            {
                case "1":
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);
                    break;
                case "2":
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, true);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                    clsFuncionesGral.Activa_Paneles(pnlpdf, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);
                    break;
                case "3":
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, true, true);
                    clsFuncionesGral.Activa_Paneles(pnlpdf, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);
                    break;
            }
        }

        protected void lnkAgregaNoFiscal_Click(object sender, EventArgs e)
        {
            string lsClvConcepto = "12";
            string lsConcepto = txtConcepto.Text;

            if ((txtConcepto.Text == null) | (txtConcepto.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Concepto obligatorio');", true);
                return;
            }
            if ((txtImporte.Text == null) | (txtImporte.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Importe obligatorio ');", true);
                return;
            }
            if ((txtObservaciones.Text == null) | (txtObservaciones.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('observaciones obligatorio');", true);
                return;
            }
            if (!clsFuncionesGral.IsNumeric(txtImporte.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Importe debe ser numerico');", true);
                return;
            }

            Comision detalleComision = new Comision();
            detalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            string diezporciento = "", tarifa1 = "";

            if ((detalleComision.Zona_Comercial == "1") | (detalleComision.Zona_Comercial == "2") | (detalleComision.Zona_Comercial == "3") | (detalleComision.Zona_Comercial == "4") | (detalleComision.Zona_Comercial == "5") | (detalleComision.Zona_Comercial == "6") | (detalleComision.Zona_Comercial == "10") | (detalleComision.Zona_Comercial == "11") | (detalleComision.Zona_Comercial == "12") | (detalleComision.Zona_Comercial == "13"))
            {
                diezporciento = clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(detalleComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Tarifa(detalleComision.Zona_Comercial))) * 0.10));
            }
            else if ((detalleComision.Zona_Comercial == "14") | (detalleComision.Zona_Comercial == "15") | (detalleComision.Zona_Comercial == "19"))
            {
                diezporciento = clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO);
            }
            else if ((detalleComision.Zona_Comercial == "17") | (detalleComision.Zona_Comercial == "21") | (detalleComision.Zona_Comercial == "23"))
            {
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("4");
                diezporciento = clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(detalleComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));

            }
            else if ((detalleComision.Zona_Comercial == "16") | (detalleComision.Zona_Comercial == "18") | (detalleComision.Zona_Comercial == "20") | (detalleComision.Zona_Comercial == "22"))
            {
                tarifa1 = MngNegocioComision.Obtiene_Tarifa("2");
                diezporciento = clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(detalleComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(tarifa1)) * 0.10));
            }
            //   diezporciento = clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(detalleComision.Dias_Comercial) * clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Tarifa(detalleComision.Zona_Comercial))) * 0.10));

            double z = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(detalleComision.Comisionado, detalleComision.Ubicacion_Comisionado, detalleComision.Archivo, "'12'"));

            if ((detalleComision.Zona_Comercial == "1") | (detalleComision.Zona_Comercial == "2") | (detalleComision.Zona_Comercial == "3") | (detalleComision.Zona_Comercial == "4") | (detalleComision.Zona_Comercial == "5") | (detalleComision.Zona_Comercial == "6") | (detalleComision.Zona_Comercial == "10") | (detalleComision.Zona_Comercial == "11") | (detalleComision.Zona_Comercial == "12") | (detalleComision.Zona_Comercial == "13") | (detalleComision.Zona_Comercial == "18"))
            {
                if ((clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(z.ToString())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(txtImporte.Text)) > clsFuncionesGral.Convert_Double(diezporciento)))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El importe que intenta subir es el mayor al permitido.');", true);
                    return;
                }
                else
                {
                    MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, clsFuncionesGral.ConvertMayus(lsConcepto), "", clsFuncionesGral.Convert_Decimales(txtImporte.Text), "", clsFuncionesGral.ConvertMayus(txtObservaciones.Text), "", "", "", detalleComision.Periodo);

                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(detalleComision);
                    txtConcepto.Text = Dictionary.CADENA_NULA;
                    txtImporte.Text = Dictionary.CADENA_NULA;
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                }

            }
            else if ((detalleComision.Zona_Comercial == "14") | (detalleComision.Zona_Comercial == "15") | (detalleComision.Zona_Comercial == "19"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede subir el 10% ya que en estas tarifas no aplica.');", true);
                return;
            }
            else if ((detalleComision.Zona_Comercial == "16") | (detalleComision.Zona_Comercial == "17"))
            {
                string tt = MngNegocioComision.Obtiene_Tarifa("14");
                string TotalRural = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(detalleComision.Dias_Rural) * clsFuncionesGral.Convert_Double(tt));

                // ((clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(z.ToString())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(txtImporte.Text))) - clsFuncionesGral.Convert_Double (TotalRural ))

                if (((clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(z.ToString())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(txtImporte.Text))) - clsFuncionesGral.Convert_Double(TotalRural)) > clsFuncionesGral.Convert_Double(diezporciento))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El importe que intenta subir es el mayor al permitido.');", true);
                    return;
                }
                else
                {
                    MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, clsFuncionesGral.ConvertMayus(lsConcepto), "", clsFuncionesGral.Convert_Decimales(txtImporte.Text), "", clsFuncionesGral.ConvertMayus(txtObservaciones.Text), "", "", "", detalleComision.Periodo);

                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(detalleComision);
                    txtConcepto.Text = Dictionary.CADENA_NULA;
                    txtImporte.Text = Dictionary.CADENA_NULA;
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                }
            }
            else if ((detalleComision.Zona_Comercial == "20") | (detalleComision.Zona_Comercial == "21"))
            {
                string tarifa50 = MngNegocioComision.Obtiene_Tarifa("19");
                string Total50 = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(detalleComision.Dias_Rural) * clsFuncionesGral.Convert_Double(tarifa50));

                if (((clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(z.ToString())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(txtImporte.Text))) - clsFuncionesGral.Convert_Double(Total50)) > clsFuncionesGral.Convert_Double(diezporciento))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El importe que intenta subir es el mayor al permitido.');", true);
                    return;
                }
                else
                {
                    MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, clsFuncionesGral.ConvertMayus(lsConcepto), "", clsFuncionesGral.Convert_Decimales(txtImporte.Text), "", clsFuncionesGral.ConvertMayus(txtObservaciones.Text), "", "", "", detalleComision.Periodo);

                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(detalleComision);
                    txtConcepto.Text = Dictionary.CADENA_NULA;
                    txtImporte.Text = Dictionary.CADENA_NULA;
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                }

            }
            else if ((detalleComision.Zona_Comercial == "20") | (detalleComision.Zona_Comercial == "21"))
            {
                string tt = MngNegocioComision.Obtiene_Tarifa("14");
                string TotalRural = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(detalleComision.Dias_Rural) * clsFuncionesGral.Convert_Double(tt));

                string tarifa50 = MngNegocioComision.Obtiene_Tarifa("19");
                string Total50 = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(detalleComision.Dias_Rural) * clsFuncionesGral.Convert_Double(tarifa50));

                if (((clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(z.ToString())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(txtImporte.Text))) - clsFuncionesGral.Convert_Double(Total50 + TotalRural)) > clsFuncionesGral.Convert_Double(diezporciento))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El importe que intenta subir es el mayor al permitido.');", true);
                    return;
                }
                else
                {
                    MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, clsFuncionesGral.ConvertMayus(lsConcepto), "", clsFuncionesGral.Convert_Decimales(txtImporte.Text), "", clsFuncionesGral.ConvertMayus(txtObservaciones.Text), "", "", "", detalleComision.Periodo);

                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(detalleComision);
                    txtConcepto.Text = Dictionary.CADENA_NULA;
                    txtImporte.Text = Dictionary.CADENA_NULA;
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);
                }
            }

            detalleComision = null;
            //  Button2.Enabled = true;
        }

        public void Lee_XMl(string psArchivo, List<Entidad> plEntidad, Entidades.Xml poXml)
        {
            XmlTextReader reader = new XmlTextReader(psArchivo);

            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "cfdi:Comprobante":
                        if ((poXml.TOTAL == Dictionary.CADENA_NULA) & (reader["total"] != null))
                        {
                            poXml.TOTAL = reader["total"];
                        }

                        break;

                    case "cfdi:Emisor":
                        if ((poXml.RFC_EMISOR == Dictionary.CADENA_NULA) & (reader["rfc"] != null)) poXml.RFC_EMISOR = reader["rfc"];
                        break;

                    case "Emisor":
                        if ((poXml.RFC_EMISOR == Dictionary.CADENA_NULA) & (reader["rfc"] != null)) poXml.RFC_EMISOR = reader["rfc"];
                        break;

                    case "cfdi:Concepto":

                        if ((poXml.CONCEPTO == Dictionary.CADENA_NULA) & (reader["descripcion"] != null))
                        {
                            Entidad obj = new Entidad();
                            obj.Codigo = reader["importe"];
                            obj.Descripcion = reader["descripcion"];
                            plEntidad.Add(obj);
                            poXml.CONCEPTO = Dictionary.CADENA_NULA;
                            obj = null;
                        }
                        // if ((importe == Dictionary.CADENA_NULA) & (reader["importe"] != null)) importe = reader["importe"];

                        break;

                    case "Concepto":

                        if ((poXml.CONCEPTO == Dictionary.CADENA_NULA) & (reader["descripcion"] != null))
                        {
                            Entidad obj = new Entidad();
                            obj.Codigo = reader["importe"];
                            obj.Descripcion = reader["descripcion"];
                            plEntidad.Add(obj);
                            poXml.CONCEPTO = Dictionary.CADENA_NULA;
                            obj = null;
                        }
                        // if ((importe == Dictionary.CADENA_NULA) & (reader["importe"] != null)) importe = reader["importe"];

                        break;

                    case "cfdi:Impuestos":
                        if ((poXml.TOTAL_IMPUESTOS_TRASLADADOS == Dictionary.NUMERO_CERO) & (reader["totalImpuestosTrasladados"] != null)) poXml.TOTAL_IMPUESTOS_TRASLADADOS = reader["totalImpuestosTrasladados"];

                        if ((poXml.TOTAL_IMPUESTOS_RETENIDOS == Dictionary.NUMERO_CERO) & (reader["totalImpuestosRetenidos"] != null)) poXml.TOTAL_IMPUESTOS_RETENIDOS = reader["totalImpuestosRetenidos"];
                        break;

                    case "Impuestos":

                        if ((poXml.TOTAL_IMPUESTOS_TRASLADADOS == Dictionary.NUMERO_CERO) & (reader["totalImpuestosTrasladados"] != null)) poXml.TOTAL_IMPUESTOS_TRASLADADOS = reader["totalImpuestosTrasladados"];

                        if ((poXml.TOTAL_IMPUESTOS_RETENIDOS == Dictionary.NUMERO_CERO) & (reader["totalImpuestosRetenidos"] != null)) poXml.TOTAL_IMPUESTOS_RETENIDOS = reader["totalImpuestosRetenidos"];
                        break;

                    case "cfdi:Traslado":

                        if (((poXml.IVA == Dictionary.NUMERO_CERO) | (poXml.IEPS == Dictionary.NUMERO_CERO) | (poXml.ISR == Dictionary.NUMERO_CERO) | (poXml.TUA == Dictionary.NUMERO_CERO) | (poXml.SFP == Dictionary.NUMERO_CERO)) & (reader["impuesto"] != null))
                        {
                            if (reader["impuesto"] == "IVA")
                            {
                                poXml.IVA = reader["importe"];
                            }

                            if (reader["impuesto"] == "IEPS")
                            {
                                poXml.IEPS = reader["importe"];
                            }

                            if (reader["impuesto"] == "TUA")
                            {
                                poXml.TUA = reader["importe"];
                            }

                            if (reader["impuesto"] == "ISR")
                            {
                                poXml.ISR = reader["importe"];
                            }

                            if (reader["impuesto"] == "SFP")
                            {
                                poXml.SFP = reader["importe"];
                            }
                        }
                        break;

                    case "Traslado":

                        if (((poXml.IVA == Dictionary.NUMERO_CERO) | (poXml.IEPS == Dictionary.NUMERO_CERO) | (poXml.ISR == Dictionary.NUMERO_CERO) | (poXml.TUA == Dictionary.NUMERO_CERO) | (poXml.SFP == Dictionary.NUMERO_CERO)) & (reader["impuesto"] != null))
                        {
                            if (reader["impuesto"] == "IVA")
                            {
                                poXml.IVA = reader["importe"];
                            }

                            if (reader["impuesto"] == "IEPS")
                            {
                                poXml.IEPS = reader["importe"];
                            }

                            if (reader["impuesto"] == "TUA")
                            {
                                poXml.TUA = reader["importe"];
                            }

                            if (reader["impuesto"] == "ISR")
                            {
                                poXml.ISR = reader["importe"];
                            }

                            if (reader["impuesto"] == "SFP")
                            {
                                poXml.SFP = reader["importe"];
                            }
                        }
                        break;



                    case "cfdi:Retencion ":
                        if (((poXml.IVA_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.IEPS_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.ISR_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.TUA_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.SFP_RETENIDO == Dictionary.NUMERO_CERO)) & (reader["impuesto"] != null))
                        {

                            if (reader["impuesto"] == "IVA")
                            {
                                poXml.IVA_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "IEPS")
                            {
                                poXml.IEPS_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "TUA")
                            {
                                poXml.TUA_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "ISR")
                            {
                                poXml.ISR_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "SFP")
                            {
                                poXml.SFP_RETENIDO = reader["importe"];
                            }
                        }
                        break;

                    case "Retencion ":
                        if (((poXml.IVA_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.IEPS_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.ISR_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.TUA_RETENIDO == Dictionary.NUMERO_CERO) | (poXml.SFP_RETENIDO == Dictionary.NUMERO_CERO)) & (reader["impuesto"] != null))
                        {
                            if (reader["impuesto"] == "IVA")
                            {
                                poXml.IVA_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "IEPS")
                            {
                                poXml.IEPS_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "TUA")
                            {
                                poXml.TUA_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "ISR")
                            {
                                poXml.ISR_RETENIDO = reader["importe"];
                            }

                            if (reader["impuesto"] == "SFP")
                            {
                                poXml.SFP_RETENIDO = reader["importe"];
                            }
                        }
                        break;


                    case "tfd:TimbreFiscalDigital":
                        if ((poXml.TIMBRE_FISCAL == Dictionary.CADENA_NULA) & reader["UUID"] != null) poXml.TIMBRE_FISCAL = reader["UUID"];
                        if ((poXml.FECHA_TIMBRADO == Dictionary.CADENA_NULA) & reader["FechaTimbrado"] != null) poXml.FECHA_TIMBRADO = clsFunciones.FormatFecha(reader["FechaTimbrado"]);
                        break;

                    case "implocal:TrasladosLocales":
                        if ((poXml.ISH == Dictionary.NUMERO_CERO) & reader["Importe"] != null) poXml.ISH = reader["Importe"];
                        break;


                    case "TrasladosLocales":
                        if ((poXml.ISH == Dictionary.NUMERO_CERO) & reader["Importe"] != null) poXml.ISH = reader["Importe"];
                        break;
                }
            }

        }

        protected void lnkAddFacturas_Click(object sender, EventArgs e)
        {
            string lsClvConcepto = dplConcepto.SelectedValue.ToString();
            string lsConcepto = dplConcepto.SelectedItem.ToString();
            string ticket;

            if (lsClvConcepto == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Concepto es nesesario.');", true);
                return;
            }

            bool xmlOK = false;
            bool fileOk = false;

            if (!fuplPDF.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo pdf es nesesario.');", true);
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
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                    return;
                }
            }
            else
            {
                ticket = Dictionary.CADENA_NULA;
            }

            if (!fuplXML.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo xml es nesesario');", true);
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
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Nombres de archivos pdf y xml deben ser iguales.');", true);
                return;
            }
            else if ((fileOk) & (xmlOK))
            {
                Comision detalleComision = new Comision();
                detalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

                string totalConcepto = MngNegocioComprobacion.Totales(detalleComision.Ubicacion_Comisionado, detalleComision.Comisionado, detalleComision.Archivo, "2", lsClvConcepto);

                try
                {
                    bool sube;

                    Valida_CarpetaXML();

                    if (!File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName))
                    {
                        fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    }
                    else
                    {
                        if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                        // ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ya existe una factura con ese nombre en carpeta temporal');", true);
                        // return;
                        fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    }

                    List<Entidad> llEntidad = new List<Entidad>();
                    Entidades.Xml oXml = new Entidades.Xml();

                    Lee_XMl(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName, llEntidad, oXml);

                    //antes de subir checar que no este activo el uuid de factura si no insertar
                    string existe = MngNegocioComprobacion.Exist_UUUID(oXml.TIMBRE_FISCAL);

                    if ((existe == "") | (existe == null))
                    {
                        //  if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);

                        Valida_Carpeta(detalleComision.Ruta, false, true);


                        if (fupdTickets.HasFile)
                        {
                            fupdTickets.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdTickets.FileName);
                        }

                        double totalImporteXml = 0;
                        //  double totalRetenidoXml = 0;

                        /*
                         * 
                         * importe total quitado
                        foreach (Entidad ent in llEntidad)
                        {
                            totalImporteXml += clsFuncionesGral.Convert_Double(ent.Codigo);
                        }

                        if (clsFuncionesGral.Convert_Double(oXml.TOTAL_IMPUESTOS_TRASLADADOS) == 0)
                        {
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.IVA);
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.IEPS);
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.ISR);
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.ISH);
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.TUA);
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.SFP);
                        }
                        else
                        {
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.TOTAL_IMPUESTOS_TRASLADADOS);
                            totalImporteXml += clsFuncionesGral.Convert_Double(oXml.ISH);
                        }

                        if (clsFuncionesGral.Convert_Double(oXml.TOTAL_IMPUESTOS_RETENIDOS) == 0)
                        {
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.IVA_RETENIDO);
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.IEPS_RETENIDO);
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.ISR_RETENIDO);
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.ISH_RETENIDO);
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.TUA_RETENIDO);
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.SFP_RETENIDO);
                        }
                        else
                        {
                            totalRetenidoXml += clsFuncionesGral.Convert_Double(oXml.TOTAL_IMPUESTOS_RETENIDOS);
                        }

                        totalImporteXml = totalImporteXml - totalRetenidoXml;*/


                        totalImporteXml = clsFuncionesGral.Convert_Double(oXml.TOTAL);


                        /*  
                         * excedentes quitados
                         * 
                         * if ((lsClvConcepto == "5") | (lsClvConcepto == "9") | (lsClvConcepto == "11") | (lsClvConcepto == "14") | (lsClvConcepto == "15"))
                            {
                                if ((clsFuncionesGral.Convert_Double(totalConcepto) + totalImporteXml) > (clsFuncionesGral .Convert_Double ( detalleComision.Total_Viaticos) + 30))
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Su total de viaticos excede lo otrogado por lo cual no puede subir este comprobante fiscal');", true);
                                    return;
                                }
                            }
                            else if ((lsClvConcepto == "7"))
                            {
                                if ((clsFuncionesGral.Convert_Double(totalConcepto) + totalImporteXml) > clsFuncionesGral.Convert_Double(detalleComision.Peaje) + 30)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Su peajes de viaticos excede lo otrogado por lo cual no puede subir este comprobante fiscal');", true);
                                    return;
                                }
                            }
                            else if ((lsClvConcepto == "8"))
                            {
                                if ((clsFuncionesGral.Convert_Double(totalConcepto) + totalImporteXml) > clsFuncionesGral.Convert_Double(detalleComision.Pasaje) + 30)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Su total de pasajes excede lo otrogado por lo cual no puede subir este comprobante fiscal');", true);
                                    return;
                                }
                            }
                            else if ((lsClvConcepto == "6"))
                            {
                                if ((clsFuncionesGral.Convert_Double(totalConcepto) + totalImporteXml) > clsFuncionesGral.Convert_Double(detalleComision.Combustible_Efectivo ) + 30)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Su total de combustible en efectivo excede lo otrogado por lo cual no puede subir este comprobante fiscal');", true);
                                    return;
                                }
                            }
                            */
                        if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ya existe una factura con ese mismo nombre favor de cambiarlo');", true);
                            return;
                        }
                        else
                        {
                            fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                            fuplPDF.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplPDF.FileName);
                        }

                        MngNegocioComision.Inserta_Comprobacion_Comision(detalleComision.Oficio, detalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oXml.FECHA_TIMBRADO, detalleComision.Proyecto, detalleComision.Dep_Proy, dplFiscales.SelectedValue.ToString(), lsClvConcepto, lsConcepto, fuplPDF.FileName, clsFuncionesGral.ConvertString(totalImporteXml), fuplXML.FileName, lsConcepto + "|factura|" + fuplPDF.FileName.Replace(".pdf", ""), fuplPDF.FileName.Replace(".pdf", ""), ticket, oXml.TIMBRE_FISCAL, detalleComision.Periodo);

                        foreach (Entidad x in llEntidad)
                        {
                            sube = false;
                            sube = MngNegocioXml.Inserta_DetalleXML(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), detalleComision.Archivo, fuplXML.FileName, oXml.TIMBRE_FISCAL, oXml.RFC_EMISOR, x.Descripcion, Dictionary.CADENA_NULA, oXml.FECHA_TIMBRADO, x.Codigo, oXml.IVA, oXml.TUA, oXml.ISR, oXml.IEPS, oXml.SFP, oXml.ISH, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, x.Codigo, clsFuncionesGral.ConvertString(totalImporteXml), Dictionary.NUMERO_CERO, oXml.TOTAL_IMPUESTOS_TRASLADADOS);
                        }

                        Crear_Tabla();
                        tblDetalle.Rows.Clear();
                        Carga_Detalle(detalleComision);
                        detalleComision = null;
                        oXml = null;
                        llEntidad = null;


                        //Button2.Enabled = true;
                    }
                    else
                    {
                        detalleComision = null;
                        oXml = null;
                        llEntidad = null;
                        Crear_Tabla();

                        //   if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                        //  if (File.Exists(Ruta + "/" + fuplPDF.FileName)) File.Delete(Ruta + "/" + fuplPDF.FileName);

                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('la factura que intenta subir ya fue usada para otra comprobacion, favor de ingresar una valida');", true);
                        return;
                    }

                }
                catch (Exception x)
                {
                    detalleComision = null;
                    //   oXml = null;
                    //  llEntidad = null;

                    //Valida_Carpeta(DetalleComision.Ruta, false, true);
                    //   if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    // if (File.Exists(Ruta + "/" + fuplPDF.FileName)) File.Delete(Ruta + "/" + fuplPDF.FileName);

                    //    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('" + x.Message.ToString() + "');", true);
                    fuplXML.Dispose();
                    fuplPDF.Dispose();
                    Console.Write(x.Message);
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }
        }

        //FALTA
        protected void gvFiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] cadenas = new string[3];
            string fechaEliminar = clsFuncionesGral.FormatFecha(gvFiscales.Rows[Convert.ToInt32(gvFiscales.SelectedIndex.ToString())].Cells[0].Text.ToString());
            string lsImporte = gvFiscales.Rows[Convert.ToInt32(gvFiscales.SelectedIndex.ToString())].Cells[2].Text.ToString();
            cadenas = gvFiscales.Rows[Convert.ToInt32(gvFiscales.SelectedIndex.ToString())].Cells[3].Text.ToString().Split(new Char[] { '|' });

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            bool X = MngNegocioComision.Update_Estatus_Comprobacion(DetalleComision.Oficio, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), fechaEliminar, lsImporte, cadenas[2], "2");

            if (X)
            {
                Valida_Carpeta(DetalleComision.Ruta, false, true);

                string RUTA = Session["Crip_Ruta"].ToString();

                if (File.Exists(RUTA + "/" + cadenas[2] + ".pdf")) File.Delete(RUTA + "/" + cadenas[2] + ".pdf");
                if (File.Exists(RUTA + "/" + cadenas[2] + ".xml")) File.Delete(RUTA + "/" + cadenas[2] + ".xml");

                MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Folio"].ToString(), "9", "");
                MngNegocioComision.Update_estatus_Comision("9", DetalleComision.Comisionado, DetalleComision.Oficio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                MngNegocioComision.Update_Status_ComisionDetalle("9", DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);

                Crear_Tabla();
                tblDetalle.Rows.Clear();
                Carga_Detalle(DetalleComision);

                DetalleComision = null;
                Button2.Enabled = true;
            }
        }

        protected void gvNofiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fechaEliminar = clsFuncionesGral.FormatFecha(gvNofiscales.Rows[Convert.ToInt32(gvNofiscales.SelectedIndex.ToString())].Cells[0].Text.ToString());
            string lsImporte = gvNofiscales.Rows[Convert.ToInt32(gvNofiscales.SelectedIndex.ToString())].Cells[2].Text.ToString();
            string observaciones = gvNofiscales.Rows[Convert.ToInt32(gvNofiscales.SelectedIndex.ToString())].Cells[3].Text.ToString();

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());


            bool T = MngNegocioComision.Update_Estatus_Comprobacion(DetalleComision.Oficio, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), fechaEliminar, lsImporte, observaciones, "3");

            if (T)
            {
                MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Folio"].ToString(), "9", "");
                MngNegocioComision.Update_estatus_Comision("9", DetalleComision.Comisionado, DetalleComision.Oficio, DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                MngNegocioComision.Update_Status_ComisionDetalle("9", DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);

                Crear_Tabla();
                tblDetalle.Rows.Clear();
                Carga_Detalle(DetalleComision);
                DetalleComision = null;

                Button2.Enabled = true;
            }
        }

        protected void dplConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplConcepto.SelectedValue.ToString() == "16")
            {
                Comision DetalleComision = new Comision();

                DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

                double peaje = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Totales(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Folio"].ToString(), "2", "16"));

                if (peaje >= clsFuncionesGral.Convert_Double(DetalleComision.Peaje))
                {
                    txtImportePeaje.Enabled = false;
                    lnkAgregaAttNota.Enabled = false;
                    fupdPeajes.Enabled = false;
                    Label10.Text = clsFuncionesGral.ConvertMayus("Ya tiene cargado una atenta nota y tickets de peaje que avanlan el monto total de lo otrogado en esta partida");
                }
                else
                {
                    Label10.Text = clsFuncionesGral.ConvertMayus("Atenta Nota firmada y tickets escaneados formato pdf.");
                    txtImportePeaje.Enabled = true;
                    fupdPeajes.Enabled = true;
                    lnkAgregaAttNota.Enabled = true;

                }

                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, true);
                clsFuncionesGral.Activa_Paneles(pnlpdf, false);

                DetalleComision = null;

            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);
                clsFuncionesGral.Activa_Paneles(pnlpdf, true);
            }
        }

        //FALTA TERMINAR COMPROBACION
        protected void Button2_Click(object sender, EventArgs e)
        {
            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

       /*    double total = clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) + clsFuncionesGral.Convert_Double ( DetalleComision.Singladuras );

            double totalcomp = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Totales(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), DetalleComision.Archivo, Dictionary.NUMERO_CERO, ""));

            if (totalcomp > total)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede cerrar esta comprobacion devegada, ya que ha excedido el monto total permitido a pagar por día en la norma de viaticos. favor de hacer las modificaciones correspondientes. ')", true);
                return;
            }
            else
            {*/
                string[] lsCadena = new string[5];
                lsCadena = Session["Crip_Folio"].ToString().Split(new Char[] { '-' });

                if (DetalleComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS)
                {
                    clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                    clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                    clsFuncionesGral.Activa_Paneles(pnlCertificado, false);//certidicafo para rural
                    clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                    clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                    clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales
                    clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales

                    MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), lsCadena[3], Session["Crip_Folio"].ToString(), "7", "");
                    MngNegocioComision.Update_estatus_Comision("7", DetalleComision.Comisionado, lsCadena[3], DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                    MngNegocioComision.Update_Status_ComisionDetalle("7", lsCadena[3], DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);

                    DetalleComision.Estatus = "7";

                    //  Calcula();
                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(DetalleComision);

                    // cerrado = true;
                    string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);

                    Button2.Enabled = false;

                    if ((folio_comprobante == Dictionary.NUMERO_CERO))
                    {
                        MngNegocioComision.Insert_Folio_Comprobante(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                        folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                        //Validar mail
                        // clsMail.Mail_Cierre_Comprobacion(DetalleComision, folio_comprobante);

                        Button2.Enabled = false;
                        clsPdf.Genera_Ministracion(DetalleComision, folio_comprobante);
                    }
                    else
                    {
                        Button2.Enabled = false;
                        clsPdf.Genera_Ministracion(DetalleComision, folio_comprobante);

                    }

                }
                else
                {
                    double x = 0;
                    x = (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje));

                    double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena[3])));

                    reintegro = Math.Round(reintegro, 0);

                    if (reintegro > 0)
                    {
                        DetalleComision = null;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Aun no ha terminado de realizar el total de la comprobación de los recursos otorgados , por lo cual no puede cerrar esta comprobacion satisfactoriamente')", true);
                        return;
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlFiscales, true);//seleccionar si es fiscal o no fiscal
                        clsFuncionesGral.Activa_Paneles(pnlDespacho, false);//despacho para singladuras
                        clsFuncionesGral.Activa_Paneles(pnlCertificado, false);//certidicafo para rural
                        clsFuncionesGral.Activa_Paneles(pnlFacturas, false);//fiscales
                        clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);//dentro de fiscales
                        clsFuncionesGral.Activa_Paneles(pnlpdf, false);//dentro de fiscales
                        clsFuncionesGral.Activa_Paneles(pnlNoFiscales, false);//no fiscales

                        MngNegocioComprobacion.Update_Comprobacion(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), lsCadena[3], Session["Crip_Folio"].ToString(), "7", "");
                        MngNegocioComision.Update_estatus_Comision("7", DetalleComision.Comisionado, lsCadena[3], DetalleComision.Ubicacion_Comisionado, DetalleComision.Archivo);
                        MngNegocioComision.Update_Status_ComisionDetalle("7", lsCadena[3], DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);

                        DetalleComision.Estatus = "7";

                        //  Calcula();
                        Crear_Tabla();
                        tblDetalle.Rows.Clear();
                        Carga_Detalle(DetalleComision);

                        // cerrado = true;
                        string folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                        Button2.Enabled = false;

                        if ((folio_comprobante == Dictionary.NUMERO_CERO))
                        {
                            MngNegocioComision.Insert_Folio_Comprobante(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                            folio_comprobante = MngNegocioComision.Obtiene_Folio_Comprobacion(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado);
                            //Validar mail
                            // clsMail.Mail_Cierre_Comprobacion(DetalleComision, folio_comprobante);

                            Button2.Enabled = false;
                            clsPdf.Genera_Ministracion(DetalleComision, folio_comprobante);
                        }
                        else
                        {
                            Button2.Enabled = false;
                            clsPdf.Genera_Ministracion(DetalleComision, folio_comprobante);

                        }
                    }

             //   }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha guardado el avance de comprobacion de su comision');", true);
            return;
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if ((txtreintegro.Text == "") | (txtreintegro.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de colocar el importe del reintegro realizado');", true);
                return;
            }
            if (!fupdReintegros.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe colocar scan de baucher de reintegro en formato jpg o png');", true);
                return;
            }

            if (!clsFuncionesGral.IsNumeric(txtreintegro.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Monto de reintegro debe  ser númerico');", true);
                return;
            }

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            /*
            double x = 0;
            x = (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje));

            double reintegro = (x - clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Importe_total(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Folio"].ToString())));

            if (clsFuncionesGral.Convert_Double(txtreintegro.Text) > clsFuncionesGral.Convert_Double(reintegro))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Monto de reintegro debe  ser igual al que se indica e la leyenda reintegro a efectuar');", true);
                return;
            }
            */

            Sube_Reintegro(DetalleComision);
            tblDetalle.Rows.Clear();
            Carga_Detalle(DetalleComision);

            txtreintegro.Text = Dictionary.CADENA_NULA;
            DetalleComision = null;

        }

        public void Sube_Reintegro(Comision poComision)
        {
            bool fileOk = false;

            if (fupdReintegros.HasFile)
            {
                Valida_Carpeta(poComision.Ruta, false, false, true, false);

                String fileExtension = System.IO.Path.GetExtension(fupdReintegros.FileName).ToLower();
                String[] allowedExtensions = { ".jpeg", ".jpg", ".png", ".bit", ".pdf", ".PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (!clsFuncionesGral.IsNumeric(txtreintegro.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de reintegro debe ser numerica');", true);
                return;
            }

            if (fileOk)
            {
                try
                {
                    fupdReintegros.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdReintegros.FileName);

                    MngNegocioComision.Inserta_Comprobacion_Comision(poComision.Oficio, poComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, poComision.Proyecto, poComision.Dep_Proy, "3", "13", "REINTEGRO", clsFuncionesGral.ConvertMinus(fupdReintegros.FileName).Replace(".pdf", ""), clsFuncionesGral.Convert_Decimales(txtreintegro.Text), "", "Reintegro-" + clsFuncionesGral.ConvertMinus(fupdReintegros.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdReintegros.FileName).Replace(".pdf", ""), "", "", poComision.Periodo);

                    // clsMail.Mail_Reintegro(poComision, txtreintegro.Text);
                    Carga_Detalle(poComision);
                    poComision = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Reintegro subido exitosamente, se encuentra a validacion por el area correspondiente');", true);
                    return;

                }
                catch (Exception ex)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
                    return;
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }
        }

        protected void lnkAgregaAttNota_Click(object sender, EventArgs e)
        {
            bool fileOk = false;

            Comision DetalleComision = new Comision();

            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            string totalConcepto = MngNegocioComprobacion.Totales(DetalleComision.Ubicacion_Comisionado, DetalleComision.Comisionado, DetalleComision.Oficio, "2", "7");
            string TotalAttNota = MngNegocioComprobacion.Totales(DetalleComision.Ubicacion_Comisionado, DetalleComision.Comisionado, DetalleComision.Oficio, "2", "16");

            if (fupdPeajes.HasFile)
            {
                Valida_Carpeta(DetalleComision.Ruta, false, true, false, false);

                String fileExtension = System.IO.Path.GetExtension(fupdPeajes.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if ((txtImportePeaje.Text == null) | (txtImportePeaje.Text == Dictionary.CADENA_NULA) | (txtImportePeaje.Text == Dictionary.NUMERO_CERO))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de peaje no facturable obligatoria y mayor a Cero');", true);
                return;
            }
            else if (clsFuncionesGral.Convert_Double(txtImportePeaje.Text) > clsFuncionesGral.Convert_Double(DetalleComision.Peaje))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de peaje no puede ser mayor ala otorgada');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtImportePeaje.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de peaje debe ser numerica');", true);
                return;
            }

            // double peaje = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Totales(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Folio"].ToString(), "2", "16"));

            if ((clsFuncionesGral.Convert_Double(totalConcepto) + clsFuncionesGral.Convert_Double(TotalAttNota) + clsFuncionesGral.Convert_Double(txtImportePeaje.Text)) > clsFuncionesGral.Convert_Double(DetalleComision.Peaje))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de peaje que intenta subir , junto con lo agregado anteriormente es superior al otorgado');", true);
                return;
            }

            if (fileOk)
            {
                try
                {

                    fupdPeajes.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdPeajes.FileName);

                    MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "2", "16", dplConcepto.SelectedItem.Text, fupdPeajes.FileName, clsFuncionesGral.Convert_Decimales(txtImportePeaje.Text), "", "Peajes|Atenta Nota|" + clsFuncionesGral.ConvertMinus(fupdPeajes.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdPeajes.FileName).Replace(".pdf", ""), "", "", DetalleComision.Periodo);

                    tblDetalle.Rows.Clear();
                    Carga_Detalle(DetalleComision);
                    Crear_Tabla();
                    DetalleComision = null;
                    txtImportePeaje.Text = Dictionary.CADENA_NULA;

                    clsFuncionesGral.Activa_Paneles(pnlPeajeNoFacturable, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Atenta Nota y Tickets de peaje subidos exitosamente');", true);
                    return;

                }
                catch (Exception ex)
                {
                    DetalleComision = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
                    return;
                }

            }
            else
            {
                DetalleComision = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            bool fileOk = false;

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            if (fupdCertificado.HasFile)
            {
                Valida_Carpeta(DetalleComision.Ruta, false, false, false, true);

                String fileExtension = System.IO.Path.GetExtension(fupdCertificado.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", "PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (!clsFuncionesGral.IsNumeric(txtImporteCertificado.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de certificado debe ser numerica');", true);
                return;
            }

            if (clsFuncionesGral.Convert_Double(txtImporteCertificado.Text) > (clsFuncionesGral.Convert_Double(DetalleComision.Dias_Rural) * 550))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Importe insertado es superior al otorgado en esta partida');", true);
                return;
            }


            if (fileOk)
            {
                try
                {
                    fupdCertificado.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdCertificado.FileName);

                    MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "3", "18", "CERTIFICADO DE TRANSITO", clsFuncionesGral.ConvertMinus(fupdCertificado.FileName).Replace(".pdf", ""), clsFuncionesGral.Convert_Decimales(txtImporteCertificado.Text), "", "CERTIFICADO-" + clsFuncionesGral.ConvertMinus(fupdCertificado.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdCertificado.FileName).Replace(".pdf", ""), "", "", DetalleComision.Periodo);
                    // Calcula();

                    //   clsMail.Mail_Reintegro(DetalleComision , txtreintegro.Text);
                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(DetalleComision);

                    DetalleComision = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Reintegro subido exitosamente, se encuentra a validacion por el area correspondiente');", true);
                    return;

                }
                catch (Exception ex)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
                    return;
                }
            }
            else
            {
                DetalleComision = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            bool fileOk = false;

            Comision DetalleComision = new Comision();
            DetalleComision = MngNegocioComision.Obten_Detalle(Session["Crip_Folio"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Estatus"].ToString());

            if (fupdDespacho.HasFile)
            {
                Valida_Carpeta(DetalleComision.Ruta, false, false, false, true);

                String fileExtension = System.IO.Path.GetExtension(fupdDespacho.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", "PDF" };

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
                    fupdDespacho.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fupdDespacho.FileName);

                    string tarifa = clsFuncionesGral.Convert_Decimales(MngNegocioComision.Obtiene_Tarifa("15"));

                    double grandTotal = clsFuncionesGral.Convert_Double(DetalleComision.Dias_Navegados) * clsFuncionesGral.Convert_Double(tarifa);

                    MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "3", "12", "SINGLADURAS ", clsFuncionesGral.ConvertMinus(fupdDespacho.FileName).Replace(".pdf", ""), clsFuncionesGral.Convert_Decimales(grandTotal.ToString()), "", "SINGLADURAS-" + clsFuncionesGral.ConvertMinus(fupdDespacho.FileName).Replace(".pdf", ""), clsFuncionesGral.ConvertMinus(fupdDespacho.FileName).Replace(".pdf", ""), "", "", DetalleComision.Periodo);
                    // Calcula();

                    //   clsMail.Mail_Reintegro(DetalleComision , txtreintegro.Text);

                    Crear_Tabla();
                    tblDetalle.Rows.Clear();
                    Carga_Detalle(DetalleComision);

                    DetalleComision = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Despacho de Capitania de puerto subido exitosamente, se encuentra a validacion por el area correspondiente');", true);
                    return;

                }
                catch (Exception ex)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su Despacho de Capitania de puerto  favor de reinterntar.');", true);
                    return;
                }
            }
            else
            {
                DetalleComision = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }
        }
    }
}