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
using System.IO;

namespace InapescaWeb
{
    public partial class Login : System.Web.UI.Page
    {

        static Usuario objUsuario;
        static string[] Etq;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
            }
        }


        public void Carga_Valores()
        {
            btnAceptar.Text = clsFuncionesGral.ConvertMayus("Aceptar");
            btnAceptar.Width = 100;
            btnAceptar.Height = 25;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //validacion por expresion regular
            //string patron = "@|\\ sd*|drop|select|union|delete|update|alter|insert|from|where|database|table|instance|user|'|/|//|\\|--";
            if (!clsFuncionesGral.Exp_Regular(txtUsuario.Text))
            {
                if (!clsFuncionesGral.Exp_Regular(txtPassword.Text))
                {
                    Entidades.Login objLogin = new Entidades.Login();
                    objLogin.Usuario = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(txtUsuario.Text));
                    objLogin.Password = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(txtPassword.Text));
                    Valida_Session(objLogin);
                }
                else 
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Password');", true);
                    return;
                }
            }
            else 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Usuario');", true);
            }

        }


        public void Valida_Session(Entidades.Login objLogin)
        {
          //  Boolean sessionOn = MngDatosLogin.MngDatosSession(objLogin.Ubicacion, objLogin.Usuario, objLogin.Password);

        //  Boolean sessionOn = MngNegocioLogin.Session(objLogin.Ubicacion, objLogin.Usuario, objLogin.Password);
          
            Entidades.Usuario oUsuario = new Usuario();

            oUsuario = MngNegocioLogin.Acceso_Smaf(objLogin.Ubicacion, objLogin.Usuario, objLogin.Password);
          
            Response.ContentType = "text/xml";

            if (!oUsuario.Usser.Equals("0"))
            {
              //  objUsuario = new Usuario();
               // objUsuario =MngNegocioLogin.DatosUsuario (); 
                Session.Timeout = 30;
                Session.LCID = 2057;
                Session["Version"] = "1.0";
                Session["Crip_Usuario"] = oUsuario.Usser;
                Session["Crip_Password"] = oUsuario.Password;
                Session["Crip_Nivel"] = oUsuario.Nivel;
                Session["Crip_Plaza"] = oUsuario.Plaza;
                Session["Crip_Puesto"] = oUsuario.Puesto;
                Session["Crip_Secretaria"] = oUsuario.Secretaria;
                Session["Crip_Organismo"] = oUsuario.Organismo;
                Session["Crip_Ubicacion"] = oUsuario.Ubicacion;
                Session["Crip_Area"] = oUsuario.Area;
                Session["Crip_Nombre"] = oUsuario.Nombre;
                Session["Crip_ApPat"] = oUsuario.ApPat;
                Session["Crip_ApMat"] = oUsuario.ApMat;
                Session["Crip_RFC"] = oUsuario.RFC;
                Session["Crip_Cargo"] = oUsuario.Cargo;
                Session["Crip_Email"] = oUsuario.Email;
                Session["Crip_Rol"] = oUsuario.Rol;
                Session["Crip_Abreviatura"] = oUsuario.Abreviatura;
             
                Response.Redirect ("Home/Home.aspx",true );
           
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