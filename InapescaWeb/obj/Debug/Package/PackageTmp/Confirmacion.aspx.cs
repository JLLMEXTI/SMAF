using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Windows.Forms;
using System.Globalization;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;

namespace InapescaWeb
{
    public partial class Confirmacion : System.Web.UI.Page
    {
        static string CADENA_NULA = "";
        static string NUMERO_CERO = "0";
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
        static  string  Parametro;
        static string url;
        static bool  Cruze_Inicio;
        static bool  Cruze_Fin;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Version"] != null)
                {
                    Carga_Session();
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();
                    Carga_Valores();

                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Inicie Session para poder navegar en el sitio..');", true);
                Session.Abandon();
                HttpContext.Current.Response.Redirect("Index.aspx", true);

            }
        }

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
            Parametro = Session["Parametro"].ToString ();
        }



        public void Carga_Valores()
        {
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";


            if (clsFuncionesGral.IsNumeric(Parametro))
            {
               // Cruze_Inicio =  Convert.ToBoolean ( Session["Inicio"].ToString()) ;
               // Cruze_Fin = Convert.ToBoolean(Session["Fin"].ToString());
   
                lblTitle.Text = "Su Solicitud de Comision con numero de folio " + Parametro + ", fue generada exitosamente. Se le envio un correo a UD y su jefe inmediato para la autorizacion de la misma.";

                //if ((Cruze_Inicio) | (Cruze_Fin))
                //{
                  //  lblTitle.Text += "<br><br><br>" + "  Existe un cruze de comisiones autorizadas para algun miembro de la comision, se ha enviado una notificacion a su jefe inmediato y a la Direccion Adjunta de Administracion para su evaluacion y autorizacion o adecuacion";
                //}
            }
            else if (Parametro == "UPDATE")
            {
                lblTitle.Text = clsFuncionesGral.ConvertMayus ("Se ha autorizado correctamente la solicitud de comision");
            }
            else if (Parametro == "NOT_UPDATE")
            {
                lblTitle.Text = clsFuncionesGral.ConvertMayus("ocurrio un error al autorizar la solicitud de comision, favor de validar con el administrador de sistema");
            }

        }

        protected void btnRedirige_Click(object sender, EventArgs e)
        {
            if (clsFuncionesGral.IsNumeric(Parametro))
            {
                Response.Redirect("Home/Home.aspx", true);
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home/Home.aspx", true);
        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string lsModulo;
          //  string lsRol = Session["Crip_Rol"].ToString();

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