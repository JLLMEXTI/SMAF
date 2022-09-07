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
    public partial class ReporteXproyecto : System.Web.UI.Page
    {

        /*
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
        static string lsAbreviatura;*/

        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static string lsDep;
        static string lsAnio;
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
        static double TotalPrograma;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
            }
        }

        public void Carga_Valores()
        {
            clsFuncionesGral.Activa_Paneles(pnlDirecciones, false);
            clsFuncionesGral.Activa_Paneles(pnlUnidades, false);

            dplDireccion.DataSource = MngNegocioDirecciones.ObtieneDireccion(Year);
            dplDireccion.DataValueField = Dictionary.CODIGO;
            dplDireccion.DataTextField = Dictionary.DESCRIPCION;
            dplDireccion.DataBind();

            dplUnidades.DataSource = MngNegocioAdscripcion.ObtieneCrips();
            dplUnidades.DataTextField = Dictionary.DESCRIPCION;
            dplUnidades.DataValueField = Dictionary.CODIGO;
            dplUnidades.DataBind();


            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();

            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            chkDirecciones.Text = clsFuncionesGral.ConvertMayus("Desglose por direcciones ");
            chkUnidad.Text = clsFuncionesGral.ConvertMayus("Desglose por CRIAP´s ");
            btnBuscar.Text = "Buscar";
            btnBuscar.Enabled = false;

            Label1.Text = clsFuncionesGral.ConvertMayus("filtros de reporte");
            Label2.Text = clsFuncionesGral.ConvertMayus("Direcciones :");
            Label3.Text = clsFuncionesGral.ConvertMayus("CRIAP´s :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Año :");
            //  Label5.Text = clsFuncionesGral.ConvertMayus("Final : ");
        }

        /* public void Carga_Session()
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
          */

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

        protected void chkDirecciones_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirecciones.Checked)
            {
                chkUnidad.Checked = false;
                clsFuncionesGral.Activa_Paneles(pnlDirecciones, true);
                clsFuncionesGral.Activa_Paneles(pnlUnidades, false);
                pnlBarDatos.Items.Clear();
                btnBuscar.Enabled = true;

                dplUnidades.SelectedIndex = 0;
                dplDireccion.SelectedIndex = 0;
            }
            else
            {
                pnlBarDatos.Items.Clear();
                chkUnidad.Checked = false;
                btnBuscar.Enabled = false;

                clsFuncionesGral.Activa_Paneles(pnlDirecciones, false);
                clsFuncionesGral.Activa_Paneles(pnlUnidades, false);
                dplDireccion.SelectedIndex = 0;
                dplUnidades.SelectedIndex = 0;
            }

        }

        protected void chkUnidad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnidad.Checked)
            {
                chkDirecciones.Checked = false;
                btnBuscar.Enabled = true;
                clsFuncionesGral.Activa_Paneles(pnlDirecciones, false);
                clsFuncionesGral.Activa_Paneles(pnlUnidades, true);
                dplUnidades.SelectedIndex = 0;
                dplDireccion.SelectedIndex = 0;
                pnlBarDatos.Items.Clear();
            }
            else
            {
                pnlBarDatos.Items.Clear();
                chkDirecciones.Checked = false;
                btnBuscar.Enabled = false;
                clsFuncionesGral.Activa_Paneles(pnlDirecciones, false);
                clsFuncionesGral.Activa_Paneles(pnlUnidades, false);
                dplUnidades.SelectedIndex = 0;
                dplDireccion.SelectedIndex = 0;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lsAnio = dplAnio.SelectedValue.ToString();

            if (lsAnio == string.Empty)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un periodo fiscal valido');", true);
                return;
            }

            if (pnlDirecciones.Visible)
            {

                lsDep = dplDireccion.SelectedValue.ToString();

                if (lsDep == Dictionary.NUMERO_CERO)
                {
                    clsFuncionesGral.Activa_Paneles(pnlContenedor, false);
                    pnlBarDatos.Items.Clear();
                    //  txtFin.Text = Dictionary.CADENA_NULA;
                    // txtInicio.Text = Dictionary.CADENA_NULA;
                    lsDep = Dictionary.CADENA_NULA;
                    lsInicio = Dictionary.CADENA_NULA;
                    lsFin = Dictionary.CADENA_NULA;
                    if (listaProgramsDirecciones != null) listaProgramsDirecciones.Clear();
                    if (ListaProyectoAdscripcion != null) ListaProyectoAdscripcion.Clear();
                    if (ListaComisiones != null) ListaComisiones.Clear();
                    if (ListaSolicitado != null) ListaSolicitado.Clear();

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe escoger una opcion para avanzar');", true);
                    return;
                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlContenedor, true);
                    //   lsDep = "'" + lsDep + "'";
                }

                //valida si es direccion general trae todos los progemas del ejhercicio fiscal que selecciona
                if (lsDep == Dictionary.DG)
                {
                    listaProgramsDirecciones = MngNegociosProgramas.ObtieneProgramas1(lsAnio);

                }
                else //si no trae los programas de la direccion seleccionada
                {
                    listaProgramsDirecciones = MngNegociosProgramas.Obtiene_Programas(lsDep, lsAnio);
                }

                pnlBarDatos.Items.Clear();

                if (listaProgramsDirecciones.Count > 0)
                {
                    //recorrido de lista 
                    foreach (Programa obj in listaProgramsDirecciones)
                    {

                        //crea item principal donde agregaras los items dentro del item
                        Telerik.Web.UI.RadPanelItem rd = new Telerik.Web.UI.RadPanelItem();
                        string abrev = Dictionary.CADENA_NULA;

                        if (obj.Direccion == "1000")
                        {
                            abrev = "D. GENERAL";
                        }
                        else if (obj.Direccion == "2000")
                        {
                            abrev = "D.G.A.I. ACUACULTURA";
                        }
                        else if (obj.Direccion == "3000")
                        {
                            abrev = "D.G.A.I.P. ATLANTICO";
                        }
                        else if (obj.Direccion == "4000")
                        {
                            abrev = "D.G.A.I.P. PACIFICO";
                        }
                        else if (obj.Direccion == "5000")
                        {
                            abrev = "D.G.A. ADMINISTRACION";
                        }
                        else if (obj.Direccion == "6000")
                        {
                            abrev = "D. JURIDICO";
                        }

                        rd.Text = obj.Descripcion + " - " + abrev;
                        rd.Expanded = false  ;
                        //crea lista para datos a reportar
                        List<Reporte> ListaReporte = new List<Reporte>();
                        //crear lista de proyectos dl programa para traer datos
                        List<Proyecto> ListaProyecto = MngNegocioProyecto.ListaProyecto(obj.Id_Programa, obj.Direccion, lsAnio);
                        //recorrer lista proyectos del programa
                        if (ListaProyecto.Count > 0)
                        {
                            //creamos objeto reporte y cargamos datos de lista programas de direccion
                            Reporte ObjetoReporte = new Reporte();

                            //ObjetoReporte.Programa = clsFuncionesGral.ConvertMayus(obj.Descripcion);
                            // ObjetoReporte.Objetivo = clsFuncionesGral.ConvertMayus(obj.Objetivo);
                            //   ObjetoReporte.Direccion = clsFuncionesGral.ConvertMayus(MngNegocioDirecciones.NombreDireccion(obj.Direccion));
                            double Solicitado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));
                            double Comprobado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));

                            foreach (Proyecto pp in ListaProyecto)
                            {
                                Solicitado += clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(MngNegocioProyecto.Total_Solicitado(lsAnio, pp.Clv_Proy, pp.Dependencia,"","")));

                                Comprobado += clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(MngNegocioProyecto.Total_Comprobado(pp.Clv_Proy, pp.Dependencia, lsAnio)));
                                /* ObjetoReporte.Total_Solicitado = "$" + clsFuncionesGral.ConvertString(Solicitado);
                                ObjetoReporte.Total_Comprobado = "$" + clsFuncionesGral.ConvertString(Comprobado);*/
                                ObjetoReporte.Total_Solicitado = clsFuncionesGral.ConvertString(Solicitado);
                                ObjetoReporte.Total_Comprobado = clsFuncionesGral.ConvertString(Comprobado);
                            }

                            ListaReporte.Add(ObjetoReporte);
                            ObjetoReporte = null;
                        }
                        //RECORRER LISTA DE REPORTE PARA ASIGNAR VALORES A GRAFICA
                        if (ListaReporte.Count > 0)
                        {
                            foreach (Reporte r in ListaReporte)
                            {
                                Telerik.Web.UI.RadPanelItem rg = new Telerik.Web.UI.RadPanelItem();
                                Telerik.Web.UI.RadHtmlChart gf = new Telerik.Web.UI.RadHtmlChart();
                            //    gf.Width = 500;
                              //  gf.Height = 500;
                                gf.Transitions = true;
                                gf.Skin = "Silk";
                                rg.Expanded = true;

                                gf.ChartTitle.Text = "Grafica de proyecto " + obj.Descripcion;
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

                                Telerik.Web.UI.CategorySeriesItem tp = new Telerik.Web.UI.CategorySeriesItem();
                                tp.Y = Convert.ToDecimal(r.Total_Solicitado);
                                tp.BackgroundColor = Color.LightSeaGreen;

                                dn.SeriesItems.Add(tp);

                                Telerik.Web.UI.CategorySeriesItem tc = new Telerik.Web.UI.CategorySeriesItem();
                                tc.Y = Convert.ToDecimal(r.Total_Comprobado);


                                tc.BackgroundColor = Color.Yellow;

                                dn.SeriesItems.Add(tc);


                                dn.Appearance.FillStyle.BackgroundColor = Color.Transparent;
                                gf.PlotArea.Series.Add(dn);

                                rg.Controls.Add(gf);

                                rd.Items.Add(rg);

                            }
                        }

                        //agrega item principal al contenedor
                        pnlBarDatos.Items.Add(rd);
                        controls = null;
                    }


                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlContenedor, false);
                    pnlBarDatos.Items.Clear();
                    //   txtFin.Text = Dictionary.CADENA_NULA;
                    //  txtInicio.Text = Dictionary.CADENA_NULA;
                    lsDep = Dictionary.CADENA_NULA;
                    lsInicio = Dictionary.CADENA_NULA;
                    lsFin = Dictionary.CADENA_NULA;
                    if (listaProgramsDirecciones != null) listaProgramsDirecciones.Clear();
                    if (ListaProyectoAdscripcion != null) ListaProyectoAdscripcion.Clear();
                    if (ListaComisiones != null) ListaComisiones.Clear();
                    if (ListaSolicitado != null) ListaSolicitado.Clear();
                    dplDireccion.SelectedIndex = 0;

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No existen programas para esta direccion');", true);
                    return;
                }

            }
            else if (pnlUnidades.Visible)
            {
                lsDep = dplUnidades.SelectedValue.ToString();

                if (lsDep == string.Empty)
                {
                    clsFuncionesGral.Activa_Paneles(pnlContenedor, false);
                    pnlBarDatos.Items.Clear();
                    //txtFin.Text = Dictionary.CADENA_NULA;
                    //txtInicio.Text = Dictionary.CADENA_NULA;
                    lsDep = Dictionary.CADENA_NULA;
                    lsInicio = Dictionary.CADENA_NULA;
                    lsFin = Dictionary.CADENA_NULA;
                    if (listaProgramsDirecciones != null) listaProgramsDirecciones.Clear();
                    if (ListaProyectoAdscripcion != null) ListaProyectoAdscripcion.Clear();
                    if (ListaComisiones != null) ListaComisiones.Clear();
                    if (ListaSolicitado != null) ListaSolicitado.Clear();
                    dplUnidades.SelectedIndex = 0;

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe escoger una opcion para avanzar');", true);
                    return;
                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlContenedor, true);
                    //aca mero
                    listaProgramsDirecciones = MngNegociosProgramas.Obtiene_Programas(lsDep, lsAnio , true);

                    pnlBarDatos.Items.Clear();

                    if (listaProgramsDirecciones.Count > 0)
                    {
                        //RECORRER LA LISTA
                        double Solicitado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));
                        double Comprobado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));
                            
                        foreach (Programa obj in listaProgramsDirecciones)
                        {

                            //crea item principal donde agregaras los items dentro del item
                            Telerik.Web.UI.RadPanelItem rd = new Telerik.Web.UI.RadPanelItem();
                            rd.Text = obj.Descripcion + " - " + MngNegocioDependencia.Obtiene_Siglas(lsDep);
                            rd.Expanded = false;
                            //obtiene costos de comision por proyectos
                         
                            Solicitado += clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(MngNegocioProyecto.Total_Solicitado(lsAnio, obj.Id_Programa, lsDep,"","")));
                            Comprobado += clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(MngNegocioProyecto.Total_Comprobado( obj.Id_Programa, lsDep,lsAnio )));
                            //crea grafica 
                            Telerik.Web.UI.RadPanelItem rg = new Telerik.Web.UI.RadPanelItem();
                            Telerik.Web.UI.RadHtmlChart gf = new Telerik.Web.UI.RadHtmlChart();
                          //  gf.Width = 500;
                          //  gf.Height = 500;
                            gf.Transitions = true;
                            gf.Skin = "Silk";

                            gf.ChartTitle.Text = "Grafica de proyecto " + obj.Descripcion;
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

                            Telerik.Web.UI.CategorySeriesItem tp = new Telerik.Web.UI.CategorySeriesItem();
                            tp.Y = Convert.ToDecimal(Solicitado );
                            tp.BackgroundColor = Color.LightSeaGreen;

                            dn.SeriesItems.Add(tp);

                            Telerik.Web.UI.CategorySeriesItem tc = new Telerik.Web.UI.CategorySeriesItem();
                            tc.Y = Convert.ToDecimal(Comprobado );


                            tc.BackgroundColor = Color.Yellow;

                            dn.SeriesItems.Add(tc);


                            dn.Appearance.FillStyle.BackgroundColor = Color.Transparent;
                            gf.PlotArea.Series.Add(dn);

                            rg.Controls.Add(gf);

                            rd.Items.Add(rg);
                            Solicitado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));
                             Comprobado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO));
                            
                            //agrega item principal al contenedor
                            pnlBarDatos.Items.Add(rd);
                          
                            controls = null;
                        }
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlContenedor, false);
                        pnlBarDatos.Items.Clear();
                        //  txtFin.Text = Dictionary.CADENA_NULA;
                        // txtInicio.Text = Dictionary.CADENA_NULA;
                        lsDep = Dictionary.CADENA_NULA;
                        lsInicio = Dictionary.CADENA_NULA;
                        lsFin = Dictionary.CADENA_NULA;
                        if (listaProgramsDirecciones != null) listaProgramsDirecciones.Clear();
                        if (ListaProyectoAdscripcion != null) ListaProyectoAdscripcion.Clear();
                        if (ListaComisiones != null) ListaComisiones.Clear();
                        if (ListaSolicitado != null) ListaSolicitado.Clear();
                        dplUnidades.SelectedIndex = 0;

                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No existen proyectos para esta Unidad Administrativa ');", true);
                        return;
                    }

                }
            }
        }
    }
}