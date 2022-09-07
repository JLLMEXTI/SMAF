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

namespace InapescaWeb.Soporte
{
    public partial class Alta_Permisos : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();
        static CultureInfo culture = new CultureInfo("es-MX");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
                    {
                        Carga_Valores();

                        clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
                        //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidas, true);
                    }
                    else
                    {
                        Response.Redirect("../Home/Home.aspx", true);
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
            Label1.Text = clsFuncionesGral.ConvertMayus("Asignar Cuenta Contable a Usuarios por CRIAP");
            Label2.Text = clsFuncionesGral.ConvertMayus("Unidad Administrativa : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Usuario :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Tipo de Permiso:");
            Label5.Text = clsFuncionesGral.ConvertMayus("Fecha Permiso:");
            Label6.Text = clsFuncionesGral.ConvertMayus("Observaciones:");
            Label7.Text = clsFuncionesGral.ConvertMayus("Cantidad de Permisos:");
            btnAceptar.Text = clsFuncionesGral.ConvertMayus("Habilitar Permiso");
            Calendar1.Visible = false;

            dplUnidadAdministrativa.DataSource = MngNegocioDependencia.ObtieneCentro(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString());
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
        protected void cal1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }

        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtFecha.Text = clsFuncionesGral.FormatFecha(Convert.ToString(Calendar1.SelectedDate.ToShortDateString(), culture));
            Calendar1.Visible = false;
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
                dplUsuarios.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(lsUnidad);
                dplUsuarios.DataTextField = "Descripcion";
                dplUsuarios.DataValueField = "Codigo";
                dplUsuarios.DataBind();

                dplPermisos.DataSource = MngNegocioPermisos.ObtieneListPermisos();
                dplPermisos.DataTextField = "Descripcion";
                dplPermisos.DataValueField = "Codigo";
                dplPermisos.DataBind();


            }
            else
            {
                dplUsuarios.Items.Clear();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una Unidad Administrativa para poder avanzar');", true);
                return;
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string lsUsuario= dplUsuarios.SelectedValue.ToString();
            string lsPermiso = dplPermisos.SelectedValue.ToString();
            string lsFecha = Dictionary.CADENA_NULA;
            lsFecha = txtFecha.Text;
            string lsObservaciones = Dictionary.CADENA_NULA;
            string lsCantPermisos = Dictionary.CADENA_NULA;
            lsObservaciones = txtObservaciones.Text;
            lsCantPermisos = TextCantPermisos.Text;

            
            bool resultado = false;
            if ((lsUsuario != string.Empty) || (lsPermiso != string.Empty) || (lsFecha != Dictionary.CADENA_NULA))
            {
                resultado = MngNegocioPermisos.Insert_NuevoPermiso(lsUsuario, lsPermiso, Session["Crip_Usuario"].ToString(),lsFecha,lsObservaciones, lsCantPermisos);

                if (resultado)
                {
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El Permiso fue habilitado Correctamente');", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un Error al otorgar el permiso');", true);
                    return;
                }
            }
            else 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Falta seleccionar o llenar campos vacios');", true);
                return;
            }
        
        }
    }
}