using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;

namespace InapescaWeb
{
    public partial class Encriptador : System.Web.UI.Page
    {
        #region Declaraciones
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
        static clsDictionary Dictionary = new clsDictionary();

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    if (Session["Version"] != null)
                    {
                        Carga_Session();
                        Carga_Valores();
                        clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, lsRol);
                    }
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
            lnkUsuario.Text = clsFuncionesGral .ConvertMayus ( lsNombre + " " + lsApPat + " " + lsApMat);

            Label1.Text = clsFuncionesGral .ConvertMayus ( "Cadena Encriptar :");
            Label2.Text =  clsFuncionesGral .ConvertMayus ("Cadena encriptada :");
            txtCadenaEncriptada.Enabled = false;
            btnEncriptar.Text = clsFuncionesGral.ConvertMayus("Encriptar");
            Label3.Text = clsFuncionesGral.ConvertMayus("Cadena a Decriptar :");
            Label4.Text = clsFuncionesGral .ConvertMayus ("Cadena Decriptada :");
            btgnDecriptar.Text = clsFuncionesGral.ConvertMayus("Decriptar");

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

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
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
                    if (objWebPage.URL != Dictionary.CADENA_NULA)
                    {
                        if (objWebPage.Padre != Dictionary.NUMERO_CERO)
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                        if (objWebPage.Modulo == Dictionary.CERRAR_SESSION)
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

        protected void btnEncriptar_Click(object sender, EventArgs e)
        {
            string lsCadena = txtCadenaEncriptar.Text;
            txtCadenaEncriptada.Text = MngNegocioEncriptacion.Encriptacion(lsCadena );


        }

        protected void btgnDecriptar_Click(object sender, EventArgs e)
        {
            string lsCadena = txtCadenaDecriptar.Text;
            txtCadenaDecriptada.Text = MngNegocioEncriptacion.Decriptacion(lsCadena);
        }

    }
}