using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Web.UI;

using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using InapescaWeb.Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using Telerik.Web.UI;
//using Telerik.
//using Telerik.Windows.Controls.Charting;
//using System.Windows.Controls;

namespace InapescaWeb.Reportes
{
    public partial class ReporteProgramas : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  Carga_Session();
                string lsSession = Session["Crip_Rol"].ToString();
               /* if ((lsSession == Dictionary.JEFE_CENTRO) | (lsSession == Dictionary.ADMINISTRADOR))
                {*/
                    clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());

                    cargaDatos();

                /*}
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No cuenta con los permisos nesesarios para realizar esta accion.');", true);
                    Response.Redirect("../Home/Home.aspx", true);
                }*/

            }
        }
        private void cargaDatos()
        {
            string lsSession = Session["Crip_Rol"].ToString();
            string lsUsuario = Session["Crip_Usuario"].ToString();
            string lsUbicacion = Session["Crip_Ubicacion"].ToString();


            DdlPeriodo.DataSource = MngNegocioAnio.ObtieneAnios();
            DdlPeriodo.DataTextField = Dictionary.DESCRIPCION;
            DdlPeriodo.DataValueField = Dictionary.CODIGO;
            DdlPeriodo.DataBind();


            /*   DdlUsuarios.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(lsUbicacion);
               DdlUsuarios.DataTextField = "Descripcion";
               DdlUsuarios.DataValueField = "Codigo";
               DdlUsuarios.DataBind();*/

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
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

        //evento de boton de busqueda
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string Periodo = DdlPeriodo.SelectedValue.ToString();//empty

            string Usuarios = DdlUsuarios.SelectedValue.ToString();//empty

            string[] lsCadena;
            lsCadena = DdlProgramas.SelectedValue.ToString().Split(new Char[] { '|' });
            string Programas = lsCadena[0];

            if (Periodo == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciona un Periodo');", true);
                return;
            }


            if (Programas == Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciona un Programa Valido');", true);
                return;
            }


            Proyecto Programa = MngNegocioProyecto.ObtieneDatosProy(Session["Crip_Ubicacion"].ToString(), Programas, Periodo);
            ComisionProyecto oComisionProyecto = MngNegocioComision.RegresaDatos(Programa.Periodo, Programa.Clv_Proy, Programa.Dependencia);

            Entidad TotalPagado = MngNegocioComprobacion.Obtiene_TotalPagado(Programa.Periodo, Programa.Clv_Proy, Programa.Dependencia,"2");

            string TotalComprobadoViaticos = MngNegocioComprobacion.Total_Comprobado(Programa.Periodo, Programa.Clv_Proy, "VIATICOS");
            string TotalComprobadoCombustible = MngNegocioComprobacion.Total_Comprobado(Programa.Periodo, Programa.Clv_Proy, "COMBUSTIBLE");
            string TotalComprobadoPeaje = MngNegocioComprobacion.Total_Comprobado(Programa.Periodo, Programa.Clv_Proy, "PEAJE");
            string TotalComprobadoPasaje = MngNegocioComprobacion.Total_Comprobado(Programa.Periodo, Programa.Clv_Proy, "PASAJE");
            string TotalComprobadoSingladuras = MngNegocioComprobacion.Total_Comprobado(Programa.Periodo, Programa.Clv_Proy, "SINGLADURAS");

            
            //string TotalComprobado = MngNegocioComprobacion.Total_Comprobado():
       


            /***************************GRAFICA DONA***************************
           //Telerik.Web.UI.RadPanelItem rp = new Telerik.Web.UI.RadPanelItem();
           Telerik.Web.UI.RadHtmlChart nw = new Telerik.Web.UI.RadHtmlChart();

            nw.Width = 800;
            nw.Height = 600;
            nw.Transitions = true;
            nw.Skin = "Silk";


            nw.ChartTitle.Text = "REPORTE PROGRAMAS";
            nw.ChartTitle.Appearance.Align = Telerik.Web.UI.HtmlChart.ChartTitleAlign.Center;
            nw.ChartTitle.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartTitlePosition.Top;

            nw.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Right;
            nw.Legend.Appearance.Visible = true;

            Telerik.Web.UI.DonutSeries ds = new Telerik.Web.UI.DonutSeries();
            ds.Appearance.FillStyle.BackgroundColor = Color.White;

            ds.LabelsAppearance.DataFormatString = "${0}";
            ds.StartAngle=90;
            ds.HoleSize=85;
            ds.LabelsAppearance.Visible = true;
            ds.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieAndDonutLabelsPosition.Center;

            ds.LabelsAppearance.DataFormatString = "${0}";
            ds.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieAndDonutLabelsPosition.Center;
            ds.TooltipsAppearance.DataFormatString = "${0}";

            Telerik.Web.UI.PieSeriesItem cm = new Telerik.Web.UI.PieSeriesItem();
            cm.Y = Convert.ToDecimal(oComisionProyecto.Combustible);
            cm.Name = "COMBUSTIBLE";
            cm.BackgroundColor = Color.LightPink;
            ds.SeriesItems.Add(cm);

            Telerik.Web.UI.PieSeriesItem pe = new Telerik.Web.UI.PieSeriesItem();
            pe.Y = Convert.ToDecimal(oComisionProyecto.Peaje);
            pe.Name = "PEAJE";
            pe.BackgroundColor = Color.LightSeaGreen;
            ds.SeriesItems.Add(pe);

            Telerik.Web.UI.PieSeriesItem pa = new Telerik.Web.UI.PieSeriesItem();
            pa.Y = Convert.ToDecimal(oComisionProyecto.Pasaje);
            pa.Name = "PASAJE";
            pa.BackgroundColor = Color.MediumPurple;
            ds.SeriesItems.Add(pa);

            Telerik.Web.UI.PieSeriesItem sg = new Telerik.Web.UI.PieSeriesItem();
            sg.Y = Convert.ToDecimal(oComisionProyecto.Singladuras);
            sg.Name = "SINGLADURAS";
            sg.BackgroundColor = Color.OliveDrab;
            ds.SeriesItems.Add(sg);

            Telerik.Web.UI.PieSeriesItem tv = new Telerik.Web.UI.PieSeriesItem();
            tv.Y = Convert.ToDecimal(oComisionProyecto.Totalviaticos);
            tv.Name = "TOTAL VIATICOS";
            tv.BackgroundColor = Color.Orange;
            ds.SeriesItems.Add(tv);

            Telerik.Web.UI.PieSeriesItem gt = new Telerik.Web.UI.PieSeriesItem();
            gt.Y = Convert.ToDecimal(oComisionProyecto.GranTotal);
            gt.Name = "TOTAL A LA FECHA";
            gt.BackgroundColor = Color.LightSlateGray;
            ds.SeriesItems.Add(gt);
            nw.PlotArea.Series.Add(ds);
            nw.Visible = true;
          pnlContenedor.Controls.Add(nw);
            */

            //*****************************GRAFICA DE BARRAS****************************//

            Telerik.Web.UI.RadHtmlChart gf = new Telerik.Web.UI.RadHtmlChart();

            gf.Width = 900;
            gf.Height = 400;
            gf.Transitions = true;
            gf.Skin = "Silk";

            gf.ChartTitle.Text = " REPORTE PROGRAMAS "+ Programa.Total;
            gf.ChartTitle.Appearance.Align = Telerik.Web.UI.HtmlChart.ChartTitleAlign.Center;
            gf.ChartTitle.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartTitlePosition.Top;
            gf.ChartTitle.Appearance.BackgroundColor = Color.LightBlue;

            gf.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Right;
            gf.Legend.Appearance.Visible = true;


            Telerik.Web.UI.ColumnSeries dn = new Telerik.Web.UI.ColumnSeries();
            dn.Appearance.FillStyle.BackgroundColor = Color.White;


            dn.LabelsAppearance.DataFormatString = "${0}";
            dn.LabelsAppearance.Visible = true;
            dn.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.BarColumnLabelsPosition.Center;

            gf.PlotArea.XAxis.TitleAppearance.Position = Telerik.Web.UI.HtmlChart.AxisTitlePosition.Center;
            gf.PlotArea.XAxis.TitleAppearance.Text = "TOTALES ";
            gf.PlotArea.XAxis.MajorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;
            gf.PlotArea.XAxis.MinorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;


            gf.PlotArea.YAxis.TitleAppearance.Position = Telerik.Web.UI.HtmlChart.AxisTitlePosition.Center;
            gf.PlotArea.YAxis.TitleAppearance.Text = " REPORTE PRUEBAS";
            gf.PlotArea.YAxis.MajorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;
            gf.PlotArea.YAxis.MinorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;
            gf.PlotArea.YAxis.MajorTickSize = 5;

            dn.TooltipsAppearance.BackgroundColor = Color.LawnGreen;
            dn.TooltipsAppearance.DataFormatString = "${0}";

            Telerik.Web.UI.CategorySeriesItem tc = new Telerik.Web.UI.CategorySeriesItem();
            tc.Y = Convert.ToDecimal(Programa.Total);
            tc.BackgroundColor = Color.LightSeaGreen;


            dn.SeriesItems.Add(tc);

            if (clsFuncionesGral.Convert_Double(oComisionProyecto.Combustible) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem cm = new Telerik.Web.UI.CategorySeriesItem();
                cm.Y = Convert.ToDecimal(oComisionProyecto.Combustible);
                cm.BackgroundColor = Color.LightBlue;

                dn.SeriesItems.Add(cm);
            }

            if (clsFuncionesGral.Convert_Double(oComisionProyecto.Peaje) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem pe = new Telerik.Web.UI.CategorySeriesItem();
                pe.Y = Convert.ToDecimal(oComisionProyecto.Peaje);
                pe.BackgroundColor = Color.LightPink;
                dn.SeriesItems.Add(pe);
            }

            if (clsFuncionesGral.Convert_Double(oComisionProyecto.Pasaje) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem pa = new Telerik.Web.UI.CategorySeriesItem();
                pa.Y = Convert.ToDecimal(oComisionProyecto.Pasaje);
                pa.BackgroundColor = Color.MediumPurple;
                dn.SeriesItems.Add(pa);
            }

            if (clsFuncionesGral.Convert_Double(oComisionProyecto.Singladuras) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem sg = new Telerik.Web.UI.CategorySeriesItem();
                sg.Y = Convert.ToDecimal(oComisionProyecto.Singladuras);
                sg.BackgroundColor = Color.OliveDrab;
                dn.SeriesItems.Add(sg);
            }

            if (clsFuncionesGral.Convert_Double(oComisionProyecto.Totalviaticos) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem tv = new Telerik.Web.UI.CategorySeriesItem();
                tv.Y = Convert.ToDecimal(oComisionProyecto.Totalviaticos);
                tv.BackgroundColor = Color.Yellow;
                dn.SeriesItems.Add(tv);
            }




            dn.Appearance.FillStyle.BackgroundColor = Color.Transparent;
            gf.PlotArea.Series.Add(dn);

            pnlContenedor.Controls.Add(gf);




            //********** GRAFICA TOTAL VIATICOS **********//

            Telerik.Web.UI.RadHtmlChart fg = new Telerik.Web.UI.RadHtmlChart();

            fg.Width = 900;
            fg.Height = 350;
            fg.Transitions = true;
            fg.Skin = "Silk";

            fg.ChartTitle.Text = "TOTAL SOLICITADO/TOTAL COMPROBADO "; // + Programa.Total
            fg.ChartTitle.Appearance.Align = Telerik.Web.UI.HtmlChart.ChartTitleAlign.Center;
            fg.ChartTitle.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartTitlePosition.Top;
            fg.ChartTitle.Appearance.BackgroundColor = Color.LightBlue;

            fg.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Right;
            fg.Legend.Appearance.Visible = true;

            Telerik.Web.UI.ColumnSeries cl = new Telerik.Web.UI.ColumnSeries();
            cl.Appearance.FillStyle.BackgroundColor = Color.White;

            cl.LabelsAppearance.DataFormatString = "${0}";
            cl.LabelsAppearance.Visible = true;
            cl.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.BarColumnLabelsPosition.Center;

            fg.PlotArea.XAxis.TitleAppearance.Position = Telerik.Web.UI.HtmlChart.AxisTitlePosition.Center;
            fg.PlotArea.XAxis.TitleAppearance.Text = "TOTALES COMPROBADOS";
            fg.PlotArea.XAxis.MajorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;
            fg.PlotArea.XAxis.MinorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;

            fg.PlotArea.YAxis.TitleAppearance.Position = Telerik.Web.UI.HtmlChart.AxisTitlePosition.Center;
            fg.PlotArea.YAxis.TitleAppearance.Text = "TOTALES ";
            fg.PlotArea.YAxis.MajorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;
            fg.PlotArea.YAxis.MinorTickType = Telerik.Web.UI.HtmlChart.TickType.Outside;
            fg.PlotArea.YAxis.MajorTickSize = 5;

            cl.TooltipsAppearance.BackgroundColor = Color.Aqua;
            cl.TooltipsAppearance.DataFormatString = "${0}";

            Telerik.Web.UI.CategorySeriesItem  cs = new Telerik.Web.UI.CategorySeriesItem();
            cs.Y = Convert.ToDecimal(Programa.Total);
            cs.BackgroundColor = Color.LightSeaGreen;
       


            cl.SeriesItems.Add(cs);

            if (clsFuncionesGral.Convert_Double(TotalPagado.Codigo) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem cm = new Telerik.Web.UI.CategorySeriesItem();
                cm.Y = Convert.ToDecimal(TotalPagado.Codigo);
                cm.BackgroundColor = Color.LightBlue;

                cl.SeriesItems.Add(cm);
            }

            if (clsFuncionesGral.Convert_Double(TotalComprobadoViaticos) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem pe = new Telerik.Web.UI.CategorySeriesItem();
                pe.Y = Convert.ToDecimal(TotalComprobadoViaticos);
                pe.BackgroundColor = Color.LightPink;
                cl.SeriesItems.Add(pe);
            }

            if (clsFuncionesGral.Convert_Double(TotalComprobadoCombustible) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem pa = new Telerik.Web.UI.CategorySeriesItem();
                pa.Y = Convert.ToDecimal(TotalComprobadoCombustible);
                pa.BackgroundColor = Color.MediumPurple;
                cl.SeriesItems.Add(pa);
            }

            if (clsFuncionesGral.Convert_Double(TotalComprobadoPeaje) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem sg = new Telerik.Web.UI.CategorySeriesItem();
                sg.Y = Convert.ToDecimal(TotalComprobadoPeaje);
                sg.BackgroundColor = Color.OliveDrab;
                cl.SeriesItems.Add(sg);
            }

            if (clsFuncionesGral.Convert_Double(TotalComprobadoPasaje) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem tv = new Telerik.Web.UI.CategorySeriesItem();
                tv.Y = Convert.ToDecimal(TotalComprobadoPasaje);
                tv.BackgroundColor = Color.Yellow;
                cl.SeriesItems.Add(tv);
            }

            if (clsFuncionesGral.Convert_Double(TotalComprobadoSingladuras) > 0)
            {
                Telerik.Web.UI.CategorySeriesItem tv = new Telerik.Web.UI.CategorySeriesItem();
                tv.Y = Convert.ToDecimal(TotalComprobadoSingladuras);
                tv.BackgroundColor = Color.Red;
                cl.SeriesItems.Add(tv);
            }



            cl.Appearance.FillStyle.BackgroundColor = Color.Transparent;
            fg.PlotArea.Series.Add(cl);

            Panel1.Controls.Add(fg);






        }


        protected void DdlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsUbicacion = Session["Crip_Ubicacion"].ToString();
            string Periodo = DdlPeriodo.SelectedValue.ToString();
            if (Periodo == string.Empty)
            {
                DdlProgramas.Items.Clear();

            }
            else
            {

                DdlProgramas.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsUbicacion);
                DdlProgramas.DataTextField = "Descripcion";
                DdlProgramas.DataValueField = "Codigo";
                DdlProgramas.DataBind();
            }


        }

        protected void DdlProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Periodo = DdlPeriodo.SelectedValue.ToString();//empty

            string Usuarios = DdlUsuarios.SelectedValue.ToString();//empty

            string[] lsCadena;
            lsCadena = DdlProgramas.SelectedValue.ToString().Split(new Char[] { '|' });
            string Programas = lsCadena[0];
            string Unidad = Session["Crip_Ubicacion"].ToString();

            DdlUsuarios.DataSource = MngNegocioUsuarios.UsuarioProyecto(Periodo, Programas, Unidad, "1");
            DdlUsuarios.DataTextField = Dictionary.DESCRIPCION;
            DdlUsuarios.DataValueField = Dictionary.CODIGO;
            DdlUsuarios.DataBind();


        }

        public string RotationAngle { get; set; }

    

  }
 

}



         