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
using System.Text.RegularExpressions;

namespace InapescaWeb
{
    public partial class _default : System.Web.UI.Page
    {
        static Usuario objUsuario;
        static string[] Etq;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIniciar_Clic(object sender, EventArgs e)
        {
            //Con este patrón simple, prevenimos uso de reservadas de SGBD.
            string patron = "@|\\*|drop|select|union|delete|update|alter|insert|from|where|database|table|instance|user";
            string idUs = txtIdUsuario.Text;
            string psUs = txtPassword.Text;
            //Si encontramos algo sospechoso en el input del usuario
            if (Regex.IsMatch(idUs, patron, RegexOptions.IgnorePatternWhitespace))
            {
                lblOutput.Text = "Violación de tipo expresión regular.";
                // Aquí puede aprovecharse para realizar un registro de actividad sospechosa. Capturando la IP del cliente y almacenando en BBDD.
                return;
            }
            else
            {
                // Segunda validación, si a caso, encontramos algo sospechoso en el input de la contraseña
                if (Regex.IsMatch(psUs, patron, RegexOptions.IgnorePatternWhitespace))
                {
                    lblOutput.Text = "Violación de tipo expresión regular.";
                    // Aquí puede aprovecharse para realizar un registro de actividad sospechosa. Capturando la IP del cliente y almacenando en BBDD.
                    return;
                }
                // Si se superan ambas validaciones, entonces vamos a capa de DATOS
                else
                {
                    Entidades.Login objLogin = new Entidades.Login();
                    string cad = clsFuncionesGral.ConvertMayus(txtIdUsuario.Text);
                    objLogin.Usuario = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(txtIdUsuario.Text));
                    objLogin.Password = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(txtPassword.Text));
                    Valida_Session(objLogin);
                }
            }
        }
        public void Valida_Session(Entidades.Login objLogin)
        {
            Entidades.Usuario oUsuario = new Usuario();
            oUsuario = MngNegocioLogin.Acceso_Smaf(objLogin.Ubicacion, objLogin.Usuario, objLogin.Password);
            if (!oUsuario.Usser.Equals("0"))
            {
                Session.Timeout = 20;
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
                Response.Redirect("Home/Home.aspx", true);
            }
            else
            {
                lblOutput.Text = "Lo sentimos, acceso no autorizado";
                return;
            }
        }
    }
}