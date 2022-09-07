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


namespace InapescaWeb.Home
{
    public partial class Home : System.Web.UI.Page
    {

        private DataSet datasetArbol;
      /*  private string lsUsuario;
        private string lsNivel;
        private string lsPlaza;
        private string lsPuesto;
        private string lsSecretaria;
        private string lsOrganismo;
        private string lsUbicacion;
        private string lsArea;
        private string lsNombre;
        private string lsApPat;
        private string lsApMat;
        private string lsRFC;
        private string lsCargo;
        private string lsEmail;
        private string lsRol;
        private string lsAbreviatura;*/
        private DataTable tblTabla;
        static clsDictionary Dictionary = new clsDictionary();
        private  string DP = " : ";
     //   Entidad oInfo = new Entidad();
    ///   static HttpContext ctx = HttpContext.Current;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (!IsPostBack)
                {
                    //  Carga_Session();
                    Carga_Controles();
                    //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();

                    clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
       
        }

    /*    public void Carga_Session()
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
        public void Carga_Controles()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus(Dictionary.UBICACION_ADSCRIPCION + DP);
            Label2.Text = clsFuncionesGral.ConvertMayus(Dictionary.RFC + DP);
            Label3.Text = clsFuncionesGral.ConvertMayus(Dictionary.NOMBRE + DP);
            Label4.Text = clsFuncionesGral.ConvertMayus(Dictionary.CARGO + DP);
            Label5.Text = clsFuncionesGral.ConvertMayus(Dictionary.CLAVE_PUESTO + DP);
            Label6.Text = clsFuncionesGral.ConvertMayus(Dictionary.NOMBRE_PUESTO + DP);

            lblBienvenido.Text = "Bienvenido";
            lblRFC.Text = Session["Crip_RFC"].ToString();
            lblNombre.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lblCargo.Text = Session["Crip_Cargo"].ToString();

      //   oInfo = MngDatosLogin.MngInfo(lsNivel, lsPuesto, lsPlaza);
            lblCvlPuesto.Text = Session["Crip_Nivel"].ToString() + " " + Session["Crip_Plaza"].ToString();
            lblNomPuesto.Text = Session["Crip_Puesto"].ToString();
            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(Session["Crip_Ubicacion"].ToString());
            lblUbicacion.Text = oUbicacion.Descripcion + "," + oUbicacion.Estado + ".<br> Tel: " + oUbicacion.Telefono;

            lnkDatosPersonales.Text = "Modificar Datos Personales.";

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

        protected void lnkDatosPersonales_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

    }
}