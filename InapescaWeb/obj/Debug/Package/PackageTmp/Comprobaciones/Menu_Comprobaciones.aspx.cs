using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Windows.Forms;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

namespace InapescaWeb.Comprobaciones
{
    public partial class Menu_Comprobaciones : System.Web.UI.Page
    {
        static string Year = DateTime.Today.Year.ToString();
        static clsDictionary Dictionary = new clsDictionary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    // Carga_Session();
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());
                    Carga_Valores();
                    Carga_Listas("1", Year);
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        public void Carga_Valores()
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            Label1.Text = clsFuncionesGral.ConvertMayus("Comisiones: ");
            Label2.Text = clsFuncionesGral.ConvertMayus("Año: ");

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();
            dplAnio.SelectedValue = Year;

            DropDownList1.DataSource = MngNegocioComision.ObtieneTipoComprobacion();
            DropDownList1.DataTextField = "Descripcion";
            DropDownList1.DataValueField = "Codigo";
            DropDownList1.DataBind();

            
        }


        /// <summary>
        /// Metodo que carga la session actual
        /// </summary>
        /*  public void Carga_Session()
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
        /// <summary>
        /// metodo evento de treeview menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    string lsANio = dplAnio.SelectedValue.ToString();

        //    if ((lsANio == string.Empty) | (lsANio == null))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
        //        return;
        //    }

        //    lsTipoComprobacion = DropDownList1.SelectedValue.ToString();

        //    switch (lsTipoComprobacion)
        //    {
        //        case "0"://Seleccione
        //            pnlNoComprob.Visible = false;
        //            PlaceHolder1.Controls.Clear();
        //            PlaceHolder2.Controls.Clear();

        //               ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione tipo de comision a buscar');", true);
        //        return;

        //            break;
        //        case "1"://No comprobadas
        //            DataSet ds3 = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsANio , "9");
        //            DataTable dt3 = ds3.Tables["DataSetArbol"];

        //            Controls = new StringBuilder();
        //            Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

        //            foreach (DataRow r in dt3.Rows)
        //            {
        //                if (Convert.ToString(r["ESTATUS"]) == "9")
        //                {
        //                    Controls.Append(" <tr>");

        //                    if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace (".pdf","") + "</a></td>");
        //                    }
        //                    else
        //                    {
        //                        Controls.Append("<td style='width: 25%; text-align: center'><a href='Comprobacion2017.aspx?folio=" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "|9'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "")+ "</a></td>");
        //                    }

        //                    Controls.Append("<td style='width: 20%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 20%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
        //                    }
        //                    else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1")//devengados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
        //                    }
        //                    else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2")//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }

        //                    if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")
        //                    {
        //                        Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
        //                    }
        //                    else
        //                    {
        //                        Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/rojo.png' alt='smile face' height='20px' width='20px'></td>");
        //                    }
        //                    Controls.Append("</tr>");
        //                }
        //                else if (Convert.ToString(r["ESTATUS"]) == "0")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }
        //            }

        //            Controls.Append("</table>");
        //            PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
        //            Controls = null;
        //            break;
        //        case "2"://Comprobadas sin validar

        //            DataSet ds2 = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsANio , "7");
        //            DataTable dt2 = ds2.Tables["DataSetArbol"];

        //            Controls = new StringBuilder();
        //            Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

        //            foreach (DataRow r in dt2.Rows)
        //            {
        //                if (Convert.ToString(r["ESTATUS"]) == "7")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 15%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width:10%; text-align: center'> Sin Viaticos </td>");
        //                    }
        //                    else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1")//devengados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
        //                    }
        //                    else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2")//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }


        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/amarillo.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }
        //                else if (Convert.ToString(r["ESTATUS"]) == "0")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }
        //            }

        //            Controls.Append("</table>");
        //            PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
        //            Controls = null;
        //            break;


        //        case "3"://Comprobadas  validadas
        //            DataSet ds1 = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsANio , "5");
        //            DataTable dt1 = ds1.Tables["DataSetArbol"];

        //            Controls = new StringBuilder();
        //            Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

        //            foreach (DataRow r in dt1.Rows)
        //            {
        //                if (Convert.ToString(r["ESTATUS"]) == "5")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width:10%; text-align: center'> Sin Viaticos </td>");
        //                    }
        //                    else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1")//devengados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
        //                    }
        //                    else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2")//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }


        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }
        //                else if (Convert.ToString(r["ESTATUS"]) == "0")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }
        //            }

        //            Controls.Append("</table>");
        //            PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
        //            Controls = null;
        //            break;

        //        case "4"://todas
        //            DataSet ds = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(),lsANio );
        //            DataTable dt = ds.Tables["DataSetArbol"];

        //            Controls = new StringBuilder();
        //            Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

        //            foreach (DataRow r in dt.Rows)
        //            {
        //                if (Convert.ToString(r["ESTATUS"]) == "9")
        //                {
        //                    Controls.Append(" <tr>");

        //                    if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0")) //sin viaticos
        //                    {
        //                        Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
        //                    }
        //                    else
        //                    {
        //                        Controls.Append("<td style='width: 25%; text-align: center'><a href='Comprobacion2017.aspx?folio=" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "|9'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
        //                    }

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")&(Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//devengados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }


        //                    if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))
        //                    {
        //                        Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
        //                    }
        //                    else
        //                    {
        //                        Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/rojo.png' alt='smile face' height='20px' width='20px'></td>");
        //                    }
        //                    Controls.Append("</tr>");
        //                }
        //                else if (Convert.ToString(r["ESTATUS"]) == "7")
        //                {
        //                    Controls.Append(" <tr>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")&(Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//devengados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }

        //                    if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))
        //                    {
        //                        Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
        //                    }
        //                    else
        //                    {
        //                        Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/amarillo.png' alt='smile face' height='20px' width='20px'></td>");
        //                    }
        //                    Controls.Append("</tr>");
        //                }
        //                else if (Convert.ToString(r["ESTATUS"]) == "5")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                   if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")&(Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))//sin viaticos
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//devengados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
        //                    }
        //                    else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
        //                    {
        //                        Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
        //                    }

        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }
        //                else if (Convert.ToString(r["ESTATUS"]) == "0")
        //                {
        //                    Controls.Append(" <tr>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
        //                    Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
        //                    Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

        //                    Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

        //                    Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
        //                    Controls.Append("</tr>");
        //                }



        //            }

        //            Controls.Append("</table>");
        //            PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
        //            Controls = null;

        //            break;
        //    }
        //    pruebaGrid();
        //}

        /// <summary>
        /// Evento click de link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsANio = dplAnio.SelectedValue.ToString();

            if ((lsANio == string.Empty) | (lsANio == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
                return;
            }

            string lsTipoComprobacion = DropDownList1.SelectedValue.ToString();

            if (lsTipoComprobacion == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione tipo de comision a buscar');", true);
                return;
            }

            Carga_Listas(lsTipoComprobacion,lsANio);


        }

        private void Carga_Listas(string lsTipoComprobacion, string lsANio)
        {
            clsFuncionesGral.Activa_Paneles(PanelNocomprobadas, false);
            clsFuncionesGral.Activa_Paneles(PanelOtros, false);
            GvMenuNocomprobadas.DataSource = null;
            GvMenuNocomprobadas.DataBind();
            GvMenuOtros.DataSource = null;
            GvMenuOtros.DataBind();

            List<Comision> ListaComprobaciones = new List<Comision>();
            if (lsTipoComprobacion == "1")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "9");
            }

            if (lsTipoComprobacion == "2")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "7");
                //GvMenuOtros.ShowHeader = true;
            }
            if (lsTipoComprobacion == "3")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "5");
                //GvMenuOtros.ShowHeader = true;
            }
            if (lsTipoComprobacion == "4")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "");
                //GvMenuOtros.ShowHeader = false;
            }

            List<Entidad> ListArchivosAmp = new List<Entidad>();

            foreach (Comision r in ListaComprobaciones)
            {
                if (r.Archivo_Ampliacion != "0")
                {
                    string[] psCadena;
                    psCadena = r.Archivo_Ampliacion.Split(new Char[] { '|' });

                    for (int i = 0; i < psCadena.Length; i++)
                    {
                        Entidad oEntidad = new Entidad();
                        oEntidad.Codigo = r.Archivo;
                        oEntidad.Descripcion = psCadena[i];
                        ListArchivosAmp.Add(oEntidad);
                    }
                }
            }
            List<Comision> ListaComprobacionesNoValidadas = new List<Comision>();
            List<Comision> ListaComprobacionesNueva = new List<Comision>();
            foreach (Comision r1 in ListaComprobaciones)
            {
                Comision oMenuComp = new Comision();

                bool existente = false;
                bool AmpliacionEx = false;
                int contador = 0;
                double totaldeviaticos = 0;
                string fechaFinal = "";

                foreach (Entidad r2 in ListArchivosAmp)
                {
                    if (r1.Archivo == r2.Codigo)
                    {
                        existente = true;
                        AmpliacionEx = true;
                        contador++;

                        Entidad objEntidad = new Entidad();
                        objEntidad = MngNegocioComision.Obten_DetalleArchivo(Session["Crip_Usuario"].ToString(), lsANio, r2.Descripcion);

                        totaldeviaticos = totaldeviaticos + Convert.ToDouble(objEntidad.Codigo);//cambiar pot metodo trae total viaticos r2.descripcion
                        fechaFinal = objEntidad.Descripcion;
                    }
                    if (r1.Archivo == r2.Descripcion)
                    {
                        AmpliacionEx = true;
                    }


                }
                oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "");
                oMenuComp.Folio = contador.ToString();
                oMenuComp.Lugar = r1.Lugar;

                if (r1.Estatus == "9")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "");
                    oMenuComp.Estatus = "../Resources/rojo.png";
                }
                if (r1.Estatus == "7")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "|7");
                    oMenuComp.Estatus = "../Resources/amarillo.png";
                }
                if (r1.Estatus == "5")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "|5");
                    oMenuComp.Estatus = "../Resources/verde.png";
                }
                if (r1.Estatus == "0")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "|0");
                    oMenuComp.Estatus = "../Resources/gris.png";
                }
                if (r1.Tipo_Pago_Viatico == "0")
                {
                    oMenuComp.Tipo_Pago_Viatico = "NA";
                    oMenuComp.Estatus = "../Resources/verde.png";
                }
                if (r1.Tipo_Pago_Viatico == "1")
                {
                    oMenuComp.Tipo_Pago_Viatico = "Dev";
                }
                if (r1.Tipo_Pago_Viatico == "2")
                {
                    oMenuComp.Tipo_Pago_Viatico = "Ant";
                }

                if (existente)
                {
                    oMenuComp.Periodo = r1.Fecha_Inicio + " AL " + fechaFinal;
                    oMenuComp.Total_Viaticos = (Convert.ToDouble(r1.Total_Viaticos) + totaldeviaticos).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    if (r1.Estatus == "9")
                    {
                        ListaComprobacionesNoValidadas.Add(oMenuComp);
                    }
                    if ((r1.Estatus == "5") || (r1.Estatus == "7") || (r1.Estatus == "0"))
                    {
                        ListaComprobacionesNueva.Add(oMenuComp);
                    }

                }
                else if (AmpliacionEx == false)
                {
                    oMenuComp.Periodo = r1.Fecha_Inicio + " AL " + r1.Fecha_Final;
                    oMenuComp.Total_Viaticos = Convert.ToDouble(r1.Total_Viaticos).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    if (r1.Estatus == "9")
                    {
                        ListaComprobacionesNoValidadas.Add(oMenuComp);
                    }
                    if ((r1.Estatus == "5") || (r1.Estatus == "7") || (r1.Estatus == "0"))
                    {
                        ListaComprobacionesNueva.Add(oMenuComp);
                    }
                }


            }
            if (ListaComprobacionesNoValidadas.Count != 0)
            {
                clsFuncionesGral.Activa_Paneles(PanelNocomprobadas, true);                
                GvMenuNocomprobadas.DataSource = ListaComprobacionesNoValidadas;
                GvMenuNocomprobadas.DataBind();
            }
            if (ListaComprobacionesNueva.Count != 0)
            {
                clsFuncionesGral.Activa_Paneles(PanelOtros, true);
                GvMenuOtros.DataSource = ListaComprobacionesNueva;
                GvMenuOtros.DataBind();
            }
            

        }

    }
}