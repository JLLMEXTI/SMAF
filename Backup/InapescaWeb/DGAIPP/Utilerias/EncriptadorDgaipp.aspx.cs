using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;

namespace InapescaWeb.DGAIPP.Utilerias
{
    public partial class EncriptadorDgaipp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              Carga_Valores();
              clsFuncionesGral.LlenarTreeView_DGAIPP("0", tvMenu, false, Session["Crip_RolDGAIPP"].ToString());// ConstruyeMenu();
            }
        }
        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string lsModulo;
            string lsRol = Session["Crip_RolDGAIPP"].ToString();

            if (tvMenu.SelectedNode != null)
            {
                lsModulo = Convert.ToString(tvMenu.SelectedNode.Value);

                WebPage objWebPage = MngNegocioMenu.MngNegocioURLDGAIPP(lsModulo, lsRol);

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
        public void Carga_Valores()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus("Cadena Encriptar :");
            Label2.Text = clsFuncionesGral.ConvertMayus("Cadena encriptada :");
            txtCadenaEncriptada.Enabled = false;
            btnEncriptar.Text = clsFuncionesGral.ConvertMayus("Encriptar");
            Label3.Text = clsFuncionesGral.ConvertMayus("Cadena a Decriptar :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Cadena Decriptada :");
            btgnDecriptar.Text = clsFuncionesGral.ConvertMayus("Decriptar");

        }

        protected void btnEncriptar_Click(object sender, EventArgs e)
        {
            string lsCadena = txtCadenaEncriptar.Text;
            txtCadenaEncriptada.Text = MngNegocioEncriptacionDGAIPP.Encriptacion(lsCadena);


        }

        protected void btgnDecriptar_Click(object sender, EventArgs e)
        {
            string lsCadena = txtCadenaDecriptar.Text;
            txtCadenaDecriptada.Text = MngNegocioEncriptacionDGAIPP.Decriptacion(lsCadena);
        }

    }
}