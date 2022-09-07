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

namespace InapescaWeb.DGAIPP
{
    public partial class Default : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            Carga_Valores();
        }


        public void Carga_Valores()
        {
            lblTitle.Text = clsFuncionesGral.ConvertMayus("Dgaipp.login.web");
            lblUsuario.Text = clsFuncionesGral.ConvertMayus("Usuario : ");
            lblPassword.Text = clsFuncionesGral.ConvertMayus("password : ");
            btnAceptar.Text = clsFuncionesGral.ConvertMayus("aceptar");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Entidades.Login objLogin = new Entidades.Login();
            // string cad = clsFuncionesGral.ConvertMayus(txtPassword.Text);
            objLogin.Usuario = clsFuncionesGral.ConvertMayus(txtUsuario.Text);
            objLogin.Password = MngEncriptacionDgaipp .encryptString (clsFuncionesGral.ConvertMayus(txtPassword.Text));
            objLogin.Ubicacion = MngEncriptacionDgaipp .encryptString ("43");

            Valida_Session(objLogin);
        }

        public void Valida_Session(Entidades.Login objLogin)
        {
            Entidades.usuariosDgaipp oUsuario = new usuariosDgaipp();

            oUsuario = MngNegocioLogin.Acceso_Dgaipp( objLogin.Usuario, objLogin.Password);

            Response.ContentType = "text/xml";
         
            if (!oUsuario.Usuario.Equals("0"))
            {
                Session.Timeout = 20;
                Session.LCID = 2057;
                Session["Version"] = "1.0";
                Session["Crip_UsuarioDGAIPP"] = oUsuario.Usuario;
                Session["Crip_PasswordDGAIPP"] = oUsuario.Password;
                Session["Crip_UbicacionDGAIPP"] = objLogin.Ubicacion;
                Session["Crip_NombreDGAIPP"] = oUsuario.Nombre;
                Session["Crip_ApPatDGAIPP"] = oUsuario.ApellidoPaterno;
                Session["Crip_ApMatDGAIPP"] = oUsuario.ApellidoMaterno;
                Session["Crip_RFCDGAIPP"] = oUsuario.RFC;
                Session["Crip_CargoDGAIPP"] = oUsuario.Cargo;
                Session["Crip_EmailDGAIPP"] = oUsuario.Email;
                Session["Crip_RolDGAIPP"] = oUsuario.Rol;
                Session["Crip_AbreviaturaDGAIPP"] = oUsuario.Abreviatura;

                Response.Redirect("Home/Home_DGAIPP.aspx", true);

            }
            else
            {
                // string Error = MngDatosLogin.ReturnError();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Error de autenticacion, intente de nuevo');", true);
                return;
            }
        }

    }
}