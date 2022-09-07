using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;
using System.Drawing;

namespace InapescaWeb.Catalogos
{
    public partial class ProyectoInvestigador : System.Web.UI.Page
    {

        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Carga_Valores();

                clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
                //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidas, true);
            }
        }


        public void Carga_Valores()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus("Asignar proyectos a usuarios por crip");
            Label2.Text = clsFuncionesGral.ConvertMayus("Unidad Administrativa : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Proyecto :");


            dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneCrips("1");
            dplUnidadAdministrativa.DataTextField = "Descripcion";
            dplUnidadAdministrativa.DataValueField = "Codigo";
            dplUnidadAdministrativa.DataBind();
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


        protected void dplUnidadAdministrativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsUnidad = dplUnidadAdministrativa.SelectedValue.ToString();

            if (lsUnidad != string.Empty)
            {
                dplProyectos.DataSource = MngNegocioProyecto.ListaProyectoCrip(lsUnidad);
                dplProyectos.DataTextField = "Descripcion";
                dplProyectos.DataValueField = "Codigo";
                dplProyectos.DataBind();

            }
            else
            {
                dplProyectos.Items.Clear();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una Unidad Administrativa para poder avanzar');", true);
                return;
            }
        }

      

    }
}