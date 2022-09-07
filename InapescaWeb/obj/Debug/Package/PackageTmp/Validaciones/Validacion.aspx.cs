
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using Telerik.Web.UI;


namespace InapescaWeb.Validaciones
{

    public partial class Validacion : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        string pUsuario;
        string psPeriodo;

        protected void Page_Load(object sender, EventArgs e)
        {
            //  Carga_Session();
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();

            }
        }

        private void Carga_Valores()
        {


            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion.DataTextField = "Descripcion";
                dplAdscripcion.DataValueField = "Codigo";
                dplAdscripcion.DataBind();

            }

            string lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            string lsAdscripcion1 = dplAdscripcion.SelectedValue.ToString();


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
                        //message de error 
                    }
                }
            }
        }


        protected void dplAdscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dplUsuarios.Items.Clear();
            string lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            dplUsuarios.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
            dplUsuarios.DataTextField = Dictionary.DESCRIPCION;
            dplUsuarios.DataValueField = Dictionary.CODIGO;
            dplUsuarios.DataBind();

            DdlPeriodo.DataSource = MngNegocioAnio.ObtieneAnios();
            DdlPeriodo.DataTextField = "Descripcion";
            DdlPeriodo.DataValueField = "Codigo";
            DdlPeriodo.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pUsuario = dplUsuarios.SelectedValue.ToString();
            psPeriodo = DdlPeriodo.SelectedValue.ToString();


            if (dplUsuarios.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Seleccione un Usuario');", true);
                return;
            }

            if (DdlPeriodo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Seleccione Periodo (Año)');", true);
                return;
            }

            Session["CargaDatos"] = null;
            Session["CargaDatos"] = MngNegocioViaticos.TraerComision(pUsuario, psPeriodo);
            GvComision.DataSource = Session["CargaDatos"];
            GvComision.DataBind();

        }


      

        protected void GvComision_SelectedIndexChanged(object sender, EventArgs e)

        {

         string Archivo = GvComision.Rows[Convert.ToInt32(GvComision.SelectedIndex.ToString())].Cells[1].Text.ToString();


         Session["CargaDatos"] = null;
         Session["CargaDatos"] = MngNegocioComision.Obtiene_Detalles_ComisionUsuarioSolicitado(Archivo, pUsuario, psPeriodo);
         GVSolicitado.DataBind();
         GVSolicitado.DataSource = Session["CargaDatos"];
         GVSolicitado.DataBind();


        }
    }

}