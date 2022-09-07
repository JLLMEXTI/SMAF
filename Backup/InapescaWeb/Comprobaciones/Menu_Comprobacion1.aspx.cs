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
    public partial class Menu_Comprobacion1 : System.Web.UI.Page
    {
        /*  static string lsUsuario;
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

        static string lsTipoComprobacion;
        static List<Entidades.GridView> Comisiones_Recurso;
        static List<Entidad> ComisionesComprobadas;
        static List<comprobacion> TotalComisionesComprobadas;
        static StringBuilder Controls;
        static string[] lsCadena;

        static clsDictionary Dictionary = new clsDictionary();

        /// <summary>
        /// Metodo Inicial de page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carga_Session();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());
                Carga_Valores();

            }
        }

        /// <summary>
        /// Mretodo cargar valores iniciales
        /// </summary>
        public void Carga_Valores()
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            Label1.Text = clsFuncionesGral.ConvertMayus("Comisiones :");
            Label2.Text = clsFuncionesGral.ConvertMayus("Año :");

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();

            DropDownList1.DataSource = MngNegocioComision.ObtieneTipoComprobacion();
            DropDownList1.DataTextField = "Descripcion";
            DropDownList1.DataValueField = "Codigo";
            DropDownList1.DataBind();
        }

        /// <summary>
        /// Metodo destructor de objetos
        /// </summary>
        public void Destructor()
        {

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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsANio = dplAnio.SelectedValue.ToString();

            if ((lsANio == string.Empty) | (lsANio == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
                return;
            }

            lsTipoComprobacion = DropDownList1.SelectedValue.ToString();

            switch (lsTipoComprobacion)
            {
                case "0"://Seleccione
                    pnlNoComprob.Visible = false;
                    PlaceHolder1.Controls.Clear();
                    PlaceHolder2.Controls.Clear();

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione tipo de comision a buscar');", true);
                    return;

                    break;
                case "1"://No comprobadas
                    DataSet ds3 = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsANio, "9");
                    DataTable dt3 = ds3.Tables["DataSetArbol"];

                    Controls = new StringBuilder();
                    Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

                    foreach (DataRow r in dt3.Rows)
                    {
                        if (Convert.ToString(r["ESTATUS"]) == "9")
                        {
                            Controls.Append(" <tr>");

                            if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
                            {
                                Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
                            }
                            else
                            {
                                Controls.Append("<td style='width: 25%; text-align: center'><a href='Comision_Comprobacion.aspx?folio=" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "|9'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
                            }

                            Controls.Append("<td style='width: 20%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 20%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
                            }
                            else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1")//devengados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
                            }
                            else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2")//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }

                            if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")
                            {
                                Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
                            }
                            else
                            {
                                Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/rojo.png' alt='smile face' height='20px' width='20px'></td>");
                            }
                            Controls.Append("</tr>");
                        }
                        else if (Convert.ToString(r["ESTATUS"]) == "0")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }
                    }

                    Controls.Append("</table>");
                    PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
                    Controls = null;
                    break;
                case "2"://Comprobadas sin validar

                    DataSet ds2 = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsANio, "7");
                    DataTable dt2 = ds2.Tables["DataSetArbol"];

                    Controls = new StringBuilder();
                    Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

                    foreach (DataRow r in dt2.Rows)
                    {
                        if (Convert.ToString(r["ESTATUS"]) == "7")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 15%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
                            {
                                Controls.Append("<td style='width:10%; text-align: center'> Sin Viaticos </td>");
                            }
                            else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1")//devengados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
                            }
                            else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2")//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }


                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/amarillo.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }
                        else if (Convert.ToString(r["ESTATUS"]) == "0")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }
                    }

                    Controls.Append("</table>");
                    PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
                    Controls = null;
                    break;


                case "3"://Comprobadas  validadas
                    DataSet ds1 = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsANio, "5");
                    DataTable dt1 = ds1.Tables["DataSetArbol"];

                    Controls = new StringBuilder();
                    Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

                    foreach (DataRow r in dt1.Rows)
                    {
                        if (Convert.ToString(r["ESTATUS"]) == "5")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0")//sin viaticos
                            {
                                Controls.Append("<td style='width:10%; text-align: center'> Sin Viaticos </td>");
                            }
                            else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1")//devengados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
                            }
                            else if (Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2")//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }


                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }
                        else if (Convert.ToString(r["ESTATUS"]) == "0")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }
                    }

                    Controls.Append("</table>");
                    PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
                    Controls = null;
                    break;

                case "4"://todas
                    DataSet ds = MngNegocioComision.Comisiones(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsANio);
                    DataTable dt = ds.Tables["DataSetArbol"];

                    Controls = new StringBuilder();
                    Controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='table2' border='0' width='100%'>");

                    foreach (DataRow r in dt.Rows)
                    {
                        if (Convert.ToString(r["ESTATUS"]) == "9")
                        {
                            Controls.Append(" <tr>");

                            if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0")) //sin viaticos
                            {
                                Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
                            }
                            else
                            {
                                Controls.Append("<td style='width: 25%; text-align: center'><a href='Comision_Comprobacion.aspx?folio=" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "|9'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
                            }

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))//sin viaticos
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//devengados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }


                            if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))
                            {
                                Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
                            }
                            else
                            {
                                Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/rojo.png' alt='smile face' height='20px' width='20px'></td>");
                            }
                            Controls.Append("</tr>");
                        }
                        else if (Convert.ToString(r["ESTATUS"]) == "7")
                        {
                            Controls.Append(" <tr>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))//sin viaticos
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//devengados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }

                            if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))
                            {
                                Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
                            }
                            else
                            {
                                Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/amarillo.png' alt='smile face' height='20px' width='20px'></td>");
                            }
                            Controls.Append("</tr>");
                        }
                        else if (Convert.ToString(r["ESTATUS"]) == "5")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "0") & (Convert.ToString(r["TOTAL_VIATICOS"]) == "0"))//sin viaticos
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Sin Viaticos </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "1") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//devengados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Devengados </td>");
                            }
                            else if ((Convert.ToString(r["FORMA_PAGO_VIATICOS"]) == "2") & (Convert.ToString(r["TOTAL_VIATICOS"]) != "0"))//anticipados
                            {
                                Controls.Append("<td style='width: 10%; text-align: center'> Anticipados </td>");
                            }

                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/verde.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }
                        else if (Convert.ToString(r["ESTATUS"]) == "0")
                        {
                            Controls.Append(" <tr>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["OFICIO"]).Replace(".pdf", "") + "</a></td>");

                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["DESTINO"]) + "</td>");
                            Controls.Append("<td style='width: 25%; text-align: center'>" + Convert.ToString(r["PERIODO"]) + "</td>");
                            Controls.Append("<td style='width: 10%; text-align: center'>" + Convert.ToString(r["TOTAL_VIATICOS"]) + "</td>");

                            Controls.Append("<td style='width: 10%; text-align: center'> Cancelada </td>");

                            Controls.Append("<td style='width: 5%; text-align: center'><img src='../Resources/gris.png' alt='smile face' height='20px' width='20px'></td>");
                            Controls.Append("</tr>");
                        }



                    }

                    Controls.Append("</table>");
                    PlaceHolder1.Controls.Add(new LiteralControl(Controls.ToString()));
                    Controls = null;
                    break;
            }

        }

        /// <summary>
        /// Evento click de link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }
    }
}