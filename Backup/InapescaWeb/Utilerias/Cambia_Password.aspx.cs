using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.Catalogos
{
    public partial class Cambia_Password : System.Web.UI.Page
    {
        #region Declaraciones
        /* string lsUsuario;
         string lsNivel;
         string lsPlaza;
         string lsPuesto;
         string lsSecretaria;
         string lsOrganismo;
         string lsUbicacion;
         string lsArea;
         string lsNombre;
         string lsApPat;
         string lsApMat;
         string lsRFC;
         string lsCargo;
         string lsEmail;
         string lsRol;
         string lsPassword;
         string lsNuevaContraseña;
         string lsConfirmaContraseña;
 */
        clsDictionary Dictionary = new clsDictionary();


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //                 Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
            }
        }

        public void Carga_Valores()
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = clsFuncionesGral.ConvertMayus(Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString());
            Label1.Text = clsFuncionesGral.ConvertMayus("Cambia Contraseña");
            Label2.Text = clsFuncionesGral.ConvertMayus("Contraseña Actual");
            Label3.Text = clsFuncionesGral.ConvertMayus("Nueva Contraseña");
            Label6.Text = clsFuncionesGral.ConvertMayus("Confirma Nueva Contraseña");
            Button1.Text = clsFuncionesGral.ConvertMayus("Cambiar contraseña");
            Button2.Text = clsFuncionesGral.ConvertMayus("Cancelar");
        }

        /*
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
            lsPassword  = Session["Crip_Password"].ToString();
            
        }*/

        protected void Button1_Click(object sender, EventArgs e)
        {
            string password = Session["Crip_Password"].ToString();
            string passwordIntroducido = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(txtPassword.Text));

            if (((passwordIntroducido != Dictionary.CADENA_NULA) | (passwordIntroducido != null )) && (password == passwordIntroducido))
            {
                if (((txtPassNew.Text == null) | (txtPassNew.Text == Dictionary.CADENA_NULA)) | ((txtPassConfirm .Text  == null) | (txtPassConfirm .Text  == Dictionary.CADENA_NULA)))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Contraseña nueva y confirmacion son campos obligatorioas');", true);
                    return;
                }
                else if (MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus ( txtPassNew.Text)) ==  passwordIntroducido)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La contraseña nueva no puede ser igual a la anterior');", true);
                    return;
                }
                else
                {
                    if (txtPassNew.Text == txtPassConfirm.Text)
                    {
                        bool resultado = MngNegocioPassword.Cambia_Password(MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(txtPassNew.Text)), Session["Crip_Usuario"].ToString());

                        if (resultado)
                        {
                            Session["Crip_Password"] = MngEncriptacion.encryptString ( txtPassNew.Text);

                            txtPassword.Text = Dictionary.CADENA_NULA;
                            txtPassNew.Text = Dictionary.CADENA_NULA;
                            txtPassConfirm.Text = Dictionary.CADENA_NULA;

                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha cambiado exitosamente su contraseña');", true);
                            return;
                        }
                        else
                        {
                            txtPassword.Text = Dictionary.CADENA_NULA;
                            txtPassNew.Text = Dictionary.CADENA_NULA;
                            txtPassConfirm.Text = Dictionary.CADENA_NULA;

                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ha ocurrido algun error al actualizar su contraseña, intente de nuevo por favor');", true);
                            return;
                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Contraseña nueva y confirmacion no coinciden, favor de corregir.');", true);
                        return;
                    }

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Contraseña introducida como actual no es correcta, favor de vaidar.');", true);
                return;
            }

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

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }


    }
}