using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Text;
using System.Drawing;

namespace InapescaWeb.Reportes
{
    public partial class ReporteCrip : System.Web.UI.Page
    {
        static string lsUsuario;
        static string lsNivel;
        static string lsPlaza;
        static string lsPuesto;
        static string lsSecretaria;
        static string lsOrganismo;
        static string lsUbicacion;
        static string lsArea;
        static string lsNombre;
        static string lsApPat;
        static string lsApMat;
        static string lsRFC;
        static string lsCargo;
        static string lsEmail;
        static string lsRol;
        static string lsAbreviatura;
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        static string lsInicio;
        static string lsFin;

        static List<Programa> listaProgramsDirecciones;
        static List<Entidad> ListaProyectoAdscripcion;
        static List<ComisionDetalle> ListaComisiones;
        static List<ComisionDetalle> ListaSolicitado;
        static StringBuilder controls;
        static StringBuilder controls1;

        static double presupuestado;
        static double otorgado;
        static double ejercido;

        static double totalPresupuestado;
        static double totalOtorgado;
        static double totalEjercido;
        static int liDias;
        static double ldTarifa;

        static double TotalSolicitado;
        static double TotalPagado;
        static double TotalComprobado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();

            }
        }

        public void Carga_Valores()
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            btnBuscar.Text = "Buscar";

            Label1.Text = clsFuncionesGral.ConvertMayus("Reporte de viaticos Costo Beneficio Proyectos por unidad Administrativa");
            Label4.Text = clsFuncionesGral.ConvertMayus("Inicio");
            Label5.Text = clsFuncionesGral.ConvertMayus("Final");

            clsFuncionesGral.Activa_Paneles(pnlContenedor, false );

        }

        /// <summary>
        /// Metodo que carag valores de session()
        /// </summary>
        public void Carga_Session()
        {
            lsUsuario = Session["Crip_Usuario"].ToString();
            lsNivel = Session["Crip_Nivel"].ToString();
            lsPlaza = Session["Crip_Plaza"].ToString();
            lsPuesto = Session["Crip_Puesto"].ToString();
            lsSecretaria = Session["Crip_Secretaria"].ToString();
            lsOrganismo = Session["Crip_Organismo"].ToString();
            lsUbicacion = Session["Crip_Ubicacion"].ToString();
            lsArea = Session["Crip_Area"].ToString();
            lsNombre = Session["Crip_Nombre"].ToString();
            lsApPat = Session["Crip_ApPat"].ToString();
            lsApMat = Session["Crip_ApMat"].ToString();
            lsRFC = Session["Crip_RFC"].ToString();
            lsCargo = Session["Crip_Cargo"].ToString();
            lsEmail = Session["Crip_Email"].ToString();
            lsRol = Session["Crip_Rol"].ToString();
            lsAbreviatura = Session["Crip_Abreviatura"].ToString();
        }
        /// <summary>
        /// Evento click de link button Home
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }
        /// <summary>
        /// Evento click de link button usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }
        /// <summary>
        /// Evento click de treview tvmenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            clsFuncionesGral.Activa_Paneles(pnlContenedor,true );

            if ((txtInicio.Text == "") | (txtInicio.Text == null))
            {
                lsInicio = clsFuncionesGral.FormatFecha(MngNegocioAnio.Anio(Year));
            }
            else
            {
                lsInicio = txtInicio.Text;
            }

            if ((txtFin.Text == "") | (txtFin.Text == null))
            {
                lsFin = lsHoy;

            }
            else
            {
                lsFin = txtFin.Text;
            }

            listaProgramsDirecciones = MngNegociosProgramas.Obtiene_Programas(lsUbicacion , "", true);

            pnlBarDatos.Items.Clear();

            if (listaProgramsDirecciones.Count > 0)
            {
                foreach (Programa x in listaProgramsDirecciones)
                {
                    TotalSolicitado = 0;
                    TotalPagado = 0;
                    TotalComprobado = 0;

                    totalPresupuestado = 0;
                    totalOtorgado = 0;
                    totalEjercido = 0;

                    controls = new StringBuilder();
                    controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='tbl2' border='2' width='100%'>");
                    controls.Append(" <tr >");
                    controls.Append("<td style='width: 25%; text-align: center'> Adcripcion</a></td>");
                    controls.Append("<td style='width: 25%; text-align: center'> Solicitado </td>");
                    controls.Append("<td style='width: 25%; text-align: center'>Otorgado</td>");
                    controls.Append("<td style='width: 25%; text-align: center'>Ejercido - Comprobado</td>");
                    controls.Append("</tr>");
                    controls.Append("</tr>");

                    ListaComisiones = MngNegocioComisionDetalle.ListaComisiones(x.Id_Programa, lsUbicacion, "2", lsInicio, lsFin);

                        //proceso de solicitado
                        ListaSolicitado = MngNegocioComisionDetalle.ListaComisiones(x.Id_Programa, lsUbicacion, "", lsInicio, lsFin);

                        controls1 = new StringBuilder();
                        controls1.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='tbl2' border='2' width='100%'>");
                        controls1.Append(" <tr >");
                        controls1.Append("<td style='text-align: center'> Comisionado</td>");
                        controls1.Append("<td style='text-align: center'> Periodo</td>");
                        controls1.Append("<td style='text-align: center'> lugar</td>");
                        controls1.Append("<td style='text-align: center'> Objetivo</td>");
                        controls1.Append("<td style='text-align: center'> Tipo </td>");
                        controls1.Append("<td style='text-align: center'> Importe Total Calculado (SMaF)</td>");
                        controls1.Append("<td style='text-align: center'> Importe Total Comprobado </td>");
                        controls1.Append("<td style='text-align: center'> Dias Total Comision</td>");
                        controls1.Append("<td style='text-align: center'> Dias calculados periodo</td>");
                        controls1.Append("<td style='text-align: center'> Importe calculado periodo</td>");
                        // controls1.Append("<td style='text-align: center'> Importe Comprobado Calculado </td>");
                        controls1.Append("<td style='text-align: center'> Fecha_Comprobacion </td>");
                        controls1.Append("</tr>");
                    
                        foreach (ComisionDetalle mx in ListaSolicitado)
                        {
                            string comprobado = MngDatosComprobacion.Total(mx.Comisionado, mx.Ubicacion_Comisionado, mx.Archivo, "'5','6','7','8','9','13','12'");

                            if ((mx.Tipo == "1") & (comprobado != "0"))
                            {
                                controls1.Append(" <tr >");
                                controls1.Append("<td style='text-align: center'>" + MngNegocioUsuarios.Obtiene_Nombre(mx.Comisionado) + "</td>");
                                if (clsFuncionesGral.FormatFecha(mx.Inicio) == clsFuncionesGral.FormatFecha(mx.Final))
                                {
                                    controls1.Append("<td style='text-align: center'>" + mx.Inicio + " </td>");
                                }
                                else
                                {
                                    controls1.Append("<td style='text-align: center'>Del " + mx.Inicio + " al " + mx.Final + " </td>");
                                }
                                controls1.Append("<td style='text-align: center'> " + mx.Lugar + "</td>");
                                controls1.Append("<td style='text-align: center'> " + mx.Objetivo + "</td>");
                                controls1.Append("<td style='text-align: center'> Devengado </td>");
                                controls1.Append("<td style='text-align: center'> " + mx.Viaticos + "</td>");
                                controls1.Append("<td style='text-align: center'>" + comprobado + " </td>");
                                controls1.Append("<td style='text-align: center'> " + mx.Dias_Reales + "</td>");
                            }
                            else if (mx.Tipo == "2")
                            {
                                controls1.Append(" <tr >");
                                controls1.Append("<td style='text-align: center'>" + MngNegocioUsuarios.Obtiene_Nombre(mx.Comisionado) + "</td>");
                                if (clsFuncionesGral.FormatFecha(mx.Inicio) == clsFuncionesGral.FormatFecha(mx.Final))
                                {
                                    controls1.Append("<td style='text-align: center'>" + mx.Inicio + " </td>");
                                }
                                else
                                {
                                    controls1.Append("<td style='text-align: center'>Del " + mx.Inicio + " al " + mx.Final + " </td>");
                                }
                                controls1.Append("<td style='text-align: center'> " + mx.Lugar + "</td>");
                                controls1.Append("<td style='text-align: center'> " + mx.Objetivo + "</td>");
                                controls1.Append("<td style='text-align: center'> Anticipados </td>");
                                controls1.Append("<td style='text-align: center'> $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(mx.Viaticos) + clsFuncionesGral.Convert_Double(mx.Combustible) + clsFuncionesGral.Convert_Double(mx.Peaje) + clsFuncionesGral.Convert_Double(mx.Pasaje))) + "</td>");
                                controls1.Append("<td style='text-align: center'> $ " + comprobado + " </td>");
                                controls1.Append("<td style='text-align: center'> " + mx.Dias_Reales + "</td>");
                            }

                            presupuestado = 0;
                            otorgado = 0;
                            ejercido = 0;
                            liDias = 0;

                            if (Convert.ToDateTime(mx.Inicio) < Convert.ToDateTime(lsInicio))
                            {
                                TimeSpan ts = (Convert.ToDateTime(mx.Final) - Convert.ToDateTime(lsInicio));
                                liDias = ts.Days + 1;
                                ldTarifa = clsFuncionesGral.Convert_Double(mx.Viaticos) / clsFuncionesGral.Convert_Double(mx.Dias_Reales);
                                presupuestado = ((ldTarifa * liDias) - (ldTarifa / 2));
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Combustible);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Pasaje);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Peaje);

                                controls1.Append("<td style='text-align: center'> " + clsFuncionesGral.Convert_Double(liDias - 0.5) + "</td>");
                                controls1.Append("<td style='text-align: center'> $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(presupuestado)) + "</td>");
                                //    controls1.Append("<td style='text-align: center'> " + ((clsFuncionesGral.Convert_Double(comprobado) / presupuestado) * clsFuncionesGral.Convert_Double(liDias - 0.5)) + " </td>");

                                totalPresupuestado += presupuestado;

                            }//si inciio de comision es mayor o igual  a  inicio de parametro y final es menor o igual  a final cumple y se grega tal cual total de viaticos
                            else if ((Convert.ToDateTime(mx.Inicio) >= Convert.ToDateTime(lsInicio)) & ((Convert.ToDateTime(mx.Final) <= Convert.ToDateTime(lsFin))))
                            {
                                presupuestado = clsFuncionesGral.Convert_Double(mx.Viaticos);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Combustible);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Pasaje);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Peaje);

                                controls1.Append("<td style='text-align: center'> " + mx.Dias_Reales + "</td>");
                                controls1.Append("<td style='text-align: center'> $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(presupuestado)) + "</td>");
                                //   controls1.Append("<td style='text-align: center'> " + clsFuncionesGral.Convert_Double(comprobado) + " </td>");

                                totalPresupuestado += presupuestado;
                            }
                            else if ((Convert.ToDateTime(mx.Final) >= Convert.ToDateTime(lsFin)))
                            {
                                TimeSpan ts = (Convert.ToDateTime(lsFin) - Convert.ToDateTime(mx.Inicio));

                                liDias = ts.Days + 1;
                                ldTarifa = clsFuncionesGral.Convert_Double(mx.Viaticos) / clsFuncionesGral.Convert_Double(mx.Dias_Reales);
                                presupuestado = (ldTarifa * (liDias));
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Combustible);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Pasaje);
                                presupuestado += clsFuncionesGral.Convert_Double(mx.Peaje);


                                controls1.Append("<td style='text-align: center'> " + clsFuncionesGral.Convert_Double(liDias) + "</td>");
                                controls1.Append("<td style='text-align: center'> $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(presupuestado)) + "</td>");
                                //controls1.Append("<td style='text-align: center'> " + clsFuncionesGral.Convert_Double(comprobado) / presupuestado + " </td>");

                                totalPresupuestado += presupuestado;
                            }
                            TotalSolicitado += presupuestado;
                            controls1.Append("<td style='text-align: center'> 1900-01-01 </td>");
                            controls1.Append("</tr>");
                        }
                        controls1.Append("</table>");

                 //       controls.Append("<td > " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(totalPresupuestado)) + "</td>");

                        //proceso de otorgado
                        foreach (ComisionDetalle cd in ListaComisiones)
                        {
                            presupuestado = 0;
                            otorgado = 0;
                            ejercido = 0;
                            liDias = 0;

                            //si incio de comisio es menor a inicio de parametro
                            if (Convert.ToDateTime(cd.Inicio) < Convert.ToDateTime(lsInicio))
                            {
                                TimeSpan ts = (Convert.ToDateTime(cd.Final) - Convert.ToDateTime(lsInicio));
                                liDias = ts.Days + 1;
                                ldTarifa = clsFuncionesGral.Convert_Double(cd.Viaticos) / clsFuncionesGral.Convert_Double(cd.Dias_Reales);
                                otorgado = ((ldTarifa * liDias) - (ldTarifa / 2)); // presupuestado = ((ldTarifa * liDias) - (ldTarifa / 2));
                                otorgado += clsFuncionesGral.Convert_Double(cd.Combustible);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Pasaje);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Peaje);

                                totalOtorgado += otorgado;

                            }//si inciio de comision es mayor o igual  a  inicio de parametro y final es menor o igual  a final cumple y se grega tal cual total de viaticos
                            else if ((Convert.ToDateTime(cd.Inicio) >= Convert.ToDateTime(lsInicio)) & ((Convert.ToDateTime(cd.Final) <= Convert.ToDateTime(lsFin))))
                            {
                                otorgado = clsFuncionesGral.Convert_Double(cd.Viaticos);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Combustible);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Pasaje);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Peaje);

                                totalOtorgado += otorgado;
                            }
                            else if ((Convert.ToDateTime(cd.Final) >= Convert.ToDateTime(lsFin)))
                            {
                                TimeSpan ts = (Convert.ToDateTime(lsFin) - Convert.ToDateTime(cd.Inicio));

                                liDias = ts.Days + 1;
                                ldTarifa = clsFuncionesGral.Convert_Double(cd.Viaticos) / clsFuncionesGral.Convert_Double(cd.Dias_Reales);
                                otorgado = ((ldTarifa * liDias));
                                otorgado += clsFuncionesGral.Convert_Double(cd.Combustible);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Pasaje);
                                otorgado += clsFuncionesGral.Convert_Double(cd.Peaje);
                                totalOtorgado += otorgado;
                            }

                            if (cd.EstatuS == "5")
                            {
                                ejercido += otorgado;
                                totalEjercido += ejercido;
                            }

                            TotalPagado += otorgado;
                            TotalComprobado += ejercido;
                        }

                     //   controls.Append("<td > " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(totalOtorgado)) + "</td>");
                      //  controls.Append("<td > " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(totalEjercido)) + "</td>");
                   // }


                  //  controls.Append("</tr>");


                    // termina calculos

                   controls.Append(" <tr >");
                    controls.Append("<td > Totales :</td>");
                    controls.Append("<td > $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(TotalSolicitado)) + "</td>");
                    controls.Append("<td > $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(TotalPagado)) + "</td>");
                    controls.Append("<td > $ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(TotalComprobado)) + "</td>");
                    controls.Append("</tr>");
                    controls.Append("</table>");

                    controls.Append("<br>");

                    PlaceHolder ph = new PlaceHolder();
                    ph.Controls.Add(new LiteralControl(controls.ToString()));

                    PlaceHolder p1 = new PlaceHolder();
                    p1.Controls.Add(new LiteralControl(controls1.ToString()));
                    
                    Telerik.Web.UI.RadPanelItem bb8 = new Telerik.Web.UI.RadPanelItem();
                    bb8.Controls.Add(p1);



                    Telerik.Web.UI.RadPanelItem rd = new Telerik.Web.UI.RadPanelItem();
                    rd.Text = x.Descripcion;

                    Telerik.Web.UI.RadPanelItem r8 = new Telerik.Web.UI.RadPanelItem();
                    r8.Controls.Add(ph);

                    rd.Items.Add(r8);
                    rd.Expanded = false;

                    //grafica
                    Telerik.Web.UI.RadPanelItem rg = new Telerik.Web.UI.RadPanelItem();
                    
                    Telerik.Web.UI.RadHtmlChart gf = new Telerik.Web.UI.RadHtmlChart();
                   
                    

                    gf.Width = 900;
                    gf.Height = 300;
                    gf.Transitions = true;
                    gf.Skin = "Silk";

                    gf.ChartTitle.Text = "Grafica Balance de proyecto " + x.Descripcion;
                    gf.ChartTitle.Appearance.Align = Telerik.Web.UI.HtmlChart.ChartTitleAlign.Center;
                    gf.ChartTitle.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartTitlePosition.Top;

                    gf.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Right;
                    gf.Legend.Appearance.Visible = true;

                    Telerik.Web.UI.BarSeries dn = new Telerik.Web.UI.BarSeries();
                    dn.Appearance.FillStyle.BackgroundColor = Color.White;

                    dn.LabelsAppearance.DataFormatString = "${0}";
                    dn.LabelsAppearance.Visible = true;
                    dn.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.BarColumnLabelsPosition.Center;

                    dn.LabelsAppearance.DataFormatString = "${0}";
                    dn.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.BarColumnLabelsPosition.Center;

                    //  dn.TooltipsAppearance.BackgroundColor  = Color.White;
                    dn.TooltipsAppearance.DataFormatString = "${0}";
                    //  dn.TooltipsAppearance.Color = Color.White;


                    Telerik.Web.UI.CategorySeriesItem tc = new Telerik.Web.UI.CategorySeriesItem();
                    tc.Y = Convert.ToDecimal(x.Presupuesto );
                    tc.BackgroundColor = Color.LightSeaGreen;
                    dn.SeriesItems.Add(tc);

                    Telerik.Web.UI.CategorySeriesItem to = new Telerik.Web.UI.CategorySeriesItem();
                    to.Y = Convert.ToDecimal(TotalPagado);
                    to.BackgroundColor = Color.LightBlue;
                    dn.SeriesItems.Add(to);

                    Telerik.Web.UI.CategorySeriesItem te = new Telerik.Web.UI.CategorySeriesItem();
                    te.Y = Convert.ToDecimal(TotalComprobado);
                    te.BackgroundColor = Color.Yellow ;
                    dn.SeriesItems.Add(te);

                    dn.Appearance.FillStyle.BackgroundColor = Color.Transparent;
                    gf.PlotArea.Series.Add(dn);
                    rg.Controls.Add(gf);
                    rd.Items.Add(rg);

                    //Grafica de porcentajes

                    Telerik.Web.UI.RadPanelItem rs = new Telerik.Web.UI.RadPanelItem();
                    Telerik.Web.UI.RadHtmlChart gd = new Telerik.Web.UI.RadHtmlChart();
                    gd.Width = 1000;
                    gd.Height = 600;
                    gd.Transitions = true;
                    gd.Skin = "Silk";

                    gd.ChartTitle.Text = "Presupuestado/Ejercido/Comprobado";
                    gd.ChartTitle.Appearance.Align = Telerik.Web.UI.HtmlChart.ChartTitleAlign.Center;
                    gd.ChartTitle.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartTitlePosition.Top;

                    gd.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Right;
                    gd.Legend.Appearance.Visible = true;

                    Telerik.Web.UI.DonutSeries ds = new Telerik.Web.UI.DonutSeries();
                    ds.StartAngle = 90;
                    ds.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieAndDonutLabelsPosition.Center;
                    ds.LabelsAppearance.DataFormatString = "{0} %";
                    ds.LabelsAppearance.Visible = false;

                    ds.TooltipsAppearance.Color = Color.Black;

                    ds.TooltipsAppearance.DataFormatString = "{0} %";

                    Telerik.Web.UI.PieSeriesItem di = new Telerik.Web.UI.PieSeriesItem();
                    di.BackgroundColor = Color.LightBlue;
                    di.Exploded = false;
                    di.Name = "Restante por comprobar $:" + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(TotalPagado - TotalComprobado));

                    di.Y = Convert.ToDecimal(((TotalPagado - TotalComprobado) * 100) / clsFuncionesGral.Convert_Double(x.Presupuesto));// - (((TotalComprobado * 100) / TotalPagado)));


                    ds.SeriesItems.Add(di);

                    Telerik.Web.UI.PieSeriesItem d2 = new Telerik.Web.UI.PieSeriesItem();
                    d2.BackgroundColor = Color.LightSeaGreen;
                    d2.Exploded = false;
                    d2.Name = "Por ejercer del Programa: ";//: " + "$ " + clsFuncionesGral.Convert_Decimales (obj.Presupuesto );

                    d2.Y = Convert.ToDecimal(100 - (TotalPagado * 100) / (clsFuncionesGral.Convert_Double(x.Presupuesto)));

                    ds.SeriesItems.Add(d2);

                    Telerik.Web.UI.PieSeriesItem d3 = new Telerik.Web.UI.PieSeriesItem();
                    d3.BackgroundColor = Color.Yellow;
                    d3.Exploded = false;
                    d3.Name = "Costo Comprobado respecto de lo otorgado:" + "$ " + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(TotalComprobado));

                    if (TotalPagado == 0) d3.Y = 0;
                    else d3.Y = Convert.ToDecimal((TotalComprobado * 100) / clsFuncionesGral.Convert_Double(x.Presupuesto));

                    ds.SeriesItems.Add(d3);

                    gd.PlotArea.Series.Add(ds);
                    rs.Controls.Add(gd);

                    rd.Items.Add(rs);


                    rd.Items.Add(bb8);

                    pnlBarDatos.Items.Add(rd);

                    controls = null;
                }
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlContenedor, false);
                pnlBarDatos.Items.Clear();
                txtFin.Text = Dictionary.CADENA_NULA;
                txtInicio.Text = Dictionary.CADENA_NULA;
                lsInicio = Dictionary.CADENA_NULA;
                lsFin = Dictionary.CADENA_NULA;
                if (listaProgramsDirecciones != null) listaProgramsDirecciones.Clear();
                if (ListaProyectoAdscripcion != null) ListaProyectoAdscripcion.Clear();
                if (ListaComisiones != null) ListaComisiones.Clear();
                if (ListaSolicitado != null) ListaSolicitado.Clear();
               
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No existen proyectos para esta Unidad Administrativa ');", true);
                return;
            }
        }
    }
}