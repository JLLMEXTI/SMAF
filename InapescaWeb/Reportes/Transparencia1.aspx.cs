/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb/Reportes
	FileName:	Reporte_Viaticos.aspx.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - DGAA
	Date:		Diciembre 2017
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Web;
using System.Text;

namespace InapescaWeb.Reportes
{
    public partial class Transparencia1 : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsAnio;
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
            }
        }

        public void Carga_Valores()
        {
//            linkdes.Visible = false;
  //          LnkReport.Visible = false;
            Label1.Text = clsFuncionesGral.ConvertMayus("Generar Reporte de Transparencia Viaticos");
            Label2.Text = clsFuncionesGral.ConvertMayus("Periodo Fiscal :");
            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();
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
                        //message de error por hacer
                    }
                }
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {

            string lsAnio = dplAnio.SelectedValue.ToString();
            if (lsAnio == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe serleccionar un ejercicio fiscal para poder avanzar');", true);
                return;
            }
            else
            {
                List<Entidad> ListArchivosAmp = MngNegocioTransparencia.Transparencia_Ampliaciones(lsAnio);
                List<Entidad> ListArchivosAmpNueva = new List<Entidad>();



            }
        }




    }
}