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
    public partial class Alta_CuentaContable : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO) && (Session["Crip_Ubicacion"].ToString() == "5200"))
            {
                if (!IsPostBack)
                {
                    Carga_Valores();

                    clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
                    //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidas, true);
                }
            }
            else
            {
                Response.Redirect("../Home/Home.aspx", true);
            }
        }


        public void Carga_Valores()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus("Asignar Cuenta Contable a Usuarios por CRIAP");
            Label2.Text = clsFuncionesGral.ConvertMayus("Unidad Administrativa : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Usuario :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Inserta Cuenta Bancaria :");
            btnAceptar.Text = clsFuncionesGral.ConvertMayus("Guardar");


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
            string lsCuenta = Dictionary.CADENA_NULA ;
            
            lsCuenta = CuentaBanc.Text;
            bool resultado = false;
            if ((lsUsuario != string.Empty) || (lsCuenta != Dictionary.CADENA_NULA))
            {
                resultado = MngNegocioActualizaDatos.Update_ActualizaCuentaBancaria(lsUsuario, lsCuenta);

                if (resultado)
                {
                    lsCuenta = Dictionary.CADENA_NULA;
                    CuentaBanc.Text = Dictionary.CADENA_NULA;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se Actualizo la Cuenta Contable Correctamente');", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un Error al Actualizar la Cuenta Contable');", true);
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