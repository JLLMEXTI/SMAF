
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;

namespace InapescaWeb.Vuelos
{
    public partial class Costo_Vuelos : System.Web.UI.Page
    {


        static clsDictionary Dictionary = new clsDictionary();
       
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


        /// <summary>
        /// Evento load de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();
            }
        }



        /// <summary>
        /// Metodo de caraga de valores iniciales de pagina
        /// </summary>
        public void Carga_Valores()
        {
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";
        }


        /// <summary>
        /// Metodoe caga de daros de session
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
        /// Evento click de linkbutton datos de usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        /// <summary>
        /// Evento Click de link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        /// <summary>
        /// Evento click de tree view menu
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
    }
}