using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Text;
using System.Drawing;

namespace InapescaWeb.Reportes
{
    public partial class ReporteUsuario : System.Web.UI.Page
    {
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
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static string lsComisionado;
        static List<ComisionDetalle> ListaComisiones;
        static List<ComisionDetalle> ListaSolicitado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();
            }
        }

        public void Carga_Valores()
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            Label1.Text = clsFuncionesGral.ConvertMayus("Reporte Balance de Viaticos por usuarios");
            Label2.Text = clsFuncionesGral.ConvertMayus("Usuarios Inapesca");

            chkUsuarioGral.Text = clsFuncionesGral.ConvertMayus("Usuarios Inapesca");
            chkFiltros.Text = clsFuncionesGral.ConvertMayus("Filtrar por unidad administrativa");

            chkFiltros.Checked = false;
            chkUsuarioGral.Checked = false;
            clsFuncionesGral.Activa_Paneles(pnlUsuarios, false);

            dplusuarios.DataSource = MngNegocioUsuarios.ListaUsuarios();
            dplusuarios.DataTextField = Dictionary.DESCRIPCION;
            dplusuarios.DataValueField = Dictionary.CODIGO;
            dplusuarios.DataBind();



        }

        /// <summary>
        /// Metodo que carag valores de session()
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

        protected void chkUsuarioGral_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsuarioGral.Checked)
            {
                chkFiltros.Checked = false;
                clsFuncionesGral.Activa_Paneles(pnlUsuarios, true);
                dplusuarios.SelectedIndex = 0;
            }
            else
            {
                chkFiltros.Checked = false;
                chkUsuarioGral.Checked = false;
                clsFuncionesGral.Activa_Paneles(pnlUsuarios, false);
                dplusuarios.SelectedIndex = 0;
            }
        }

        protected void chkFiltros_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltros.Checked)
            {
                chkUsuarioGral.Checked = false;
                clsFuncionesGral.Activa_Paneles(pnlUsuarios, false);
                dplusuarios.SelectedIndex = 0;
            }
            else
            {
                chkFiltros.Checked = false;
                chkUsuarioGral.Checked = false;
                clsFuncionesGral.Activa_Paneles(pnlUsuarios, false);
                dplusuarios.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// evento click de dropdonw list usuarios inapesca
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplusuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsComisionado = dplusuarios.SelectedValue.ToString();

            if (lsComisionado == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un usuario para poder continuar');", true);
                return;
            }
            else
            { 
            
            
            }
        }
    }
}