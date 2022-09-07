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
namespace InapescaWeb.Oficios
{
    public partial class Comision : System.Web.UI.Page
    {
        static string CADENA_NULA = "";
        static string NUMERO_CERO = "0";

        private DataSet datasetArbol;
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
        static List<Entidades.GridView> ListaGrid;
        static string[] Etq;
      
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Version"] != null)
                {
                    Carga_Session();
                    Carga_Valores();
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();
                    //Crear_Tabla();
                }
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
            
        }

        public void Carga_Valores()
        {
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";
            lblTitle.Text = "Generar Oficio de Comisión";

            
            //Carga de Dropdown list
            dplComisionado.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsUbicacion, lsUsuario, lsRol, true);
            dplComisionado.DataTextField = "Descripcion";
            dplComisionado.DataValueField = "Codigo";
            dplComisionado.DataBind();

            //validar idioma por variable de session
          //  Etq = MngNegocioEtq.Obtiene_Etq("ESP");

            Label1.Text =clsFuncionesGral.ConvertMayus ( "Comisionado");
          /*  Label2.Text =clsFuncionesGral.ConvertMayus (  "Nombre :");
            Label3.Text = clsFuncionesGral.ConvertMayus ( "Cargo :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Clave de puesto :");
            */
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
    }
}