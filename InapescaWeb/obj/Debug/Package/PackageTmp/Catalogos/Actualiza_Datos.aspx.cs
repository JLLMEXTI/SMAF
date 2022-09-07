using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;


namespace InapescaWeb.Catalogos
{
    public partial class Actualiza_Datos : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        

        //Datos de session
      /* static string lsUsuario;
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
        static string lsAbreviatura;*/
        static Usuario Y;
        static bool editado;
        static string lsEstado;
        static string lsCiudad;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //  Carga_Session();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
                Carga_Valores();
                //Genera_Lista();             
            }
        }
        /// <summary>
        /// Metodo que caraga valores iniciales postback de pagina
        /// </summary>
        public void Carga_Valores()
        {
            clsFuncionesGral.Activa_Paneles(Panel1, true);
            clsFuncionesGral.Activa_Paneles(Panel2, false);

            Label7.Text = Dictionary.CADENA_NULA;
            LinkButton1.Text = Dictionary.CADENA_NULA;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lnkHome.Text = "INICIO";

            Label2.Text = clsFuncionesGral.ConvertMayus("NOMBRES");
            Label3.Text = clsFuncionesGral.ConvertMayus("APELLIDO PATERNO");
            Label6.Text = clsFuncionesGral.ConvertMayus("APELLIDO MATERNO");
            Label10.Text = clsFuncionesGral.ConvertMayus("FECHA DE NACIMIENTO");
            Label11.Text = clsFuncionesGral.ConvertMayus("DOMICILIO CALLE");
            Label4.Text = clsFuncionesGral.ConvertMayus("NUMERO INTERNO");
            Label5.Text = clsFuncionesGral.ConvertMayus("NUMERO EXTERNO");
            Label12.Text = clsFuncionesGral.ConvertMayus("COLONIA");
            Label13.Text = clsFuncionesGral.ConvertMayus("DELEGACION O MUNICIPIO");
            Label18.Text = clsFuncionesGral.ConvertMayus("RFC");
            Label15.Text = clsFuncionesGral.ConvertMayus("CURP");
            Label17.Text = clsFuncionesGral.ConvertMayus("EMAIL");
            Label19.Text = clsFuncionesGral.ConvertMayus("ESTADO");
            Label20.Text = clsFuncionesGral.ConvertMayus("CIUDAD");

            Y = MngNegocioActualizaDatos.Obten_DatosUser(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), year);

            Nombres.Text = Y.Nombre;
            ApellidoPaterno.Text = Y.ApPat;
            Apellido_Materno.Text = Y.ApMat;
            Fecha_Nacimiento.Text = clsFuncionesGral.FormatFecha(Y.fech_nac);
            Domicilio_Calle.Text = Y.calle;
            Numero_Interno.Text = Y.num_int;
            Numero_Externo.Text = Y.numext;
            Colonia.Text = Y.colonia;
            Delegacion.Text = Y.delegacion;
            RFC.Text = Y.RFC;
            CURP.Text = Y.CURP;
            EMAIL.Text = Y.Email;
            txtEdo.Text = MngNegocioDependencia.Obtiene_Descripcion_Estado(Y.Estado);
            txtEdo.Enabled = false;
            txtCiudad.Text = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(Y.CD, Y.Estado);
            txtCiudad.Enabled = false;

            if ((Y.Estado == Dictionary.CADENA_NULA) | (Y.Estado == null))
            {
                clsFuncionesGral.Activa_Paneles(pnlEdo, false);
                clsFuncionesGral.Activa_Paneles(pnlCiudad, false);
                lsEstado = Dictionary.CADENA_NULA;
                lsCiudad = Dictionary.CADENA_NULA;
            }
            else
            {
                lsEstado = Y.Estado;
                clsFuncionesGral.Activa_Paneles(pnlEdo, false);

                if ((Y.CD == Dictionary.CADENA_NULA) | (Y.CD == null))
                {
                    clsFuncionesGral.Activa_Paneles(pnlCiudad, true);

                    dplCiudad.DataSource = MngNegocioDependencia.Obtiene_ciudades(Y.Estado);
                    dplCiudad.DataTextField = Dictionary.DESCRIPCION;
                    dplCiudad.DataValueField = Dictionary.CODIGO;
                    dplCiudad.DataBind();

                    lsCiudad = Dictionary.CADENA_NULA;
                }
                else
                {
                    lsCiudad = Y.CD;
                }

            }

        }



        /// <summary>
        /// Metodo que carga datos de sesssion de usuario
        /// </summary>
     /*    public void Carga_Session()
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
        */

        /// <summary>
        /// Evento de boton regreso a HOME
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }


        /// <summary>
        /// Metodo que redirije opciones de mennu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            validacampos();

            if (editado)
            {
                Y = new Usuario();

                Y.Nombre = Nombres.Text;
                Y.ApPat = ApellidoPaterno.Text;
                Y.ApMat = Apellido_Materno.Text;
                Y.fech_nac = Fecha_Nacimiento.Text;
                Y.calle = Domicilio_Calle.Text;
                Y.num_int = Numero_Interno.Text;
                Y.numext = Numero_Externo.Text;
                Y.colonia = Colonia.Text;
                Y.delegacion = Delegacion.Text;
                Y.RFC = RFC.Text;
                Y.CURP = CURP.Text;
                Y.Email = EMAIL.Text;

                string lsgrado_academico = " ";
                string lstitulo = " ";
                bool UPDATE = MngNegocioActualizaDatos.Update_ActualizaDatosUser(Y, Session["Crip_Usuario"].ToString());
                //UPDATE = MngNegocioActualizaDatos.Inserta_Datos(lsUsuario, Y.Nombre, Y.ApPat, Y.ApMat, lsAbreviatura, lsgrado_academico, lstitulo, Y.fech_nac, Y.calle, Y.numext, Y.num_int, Y.colonia, Y.delegacion, lsCiudad, lsEstado, "MEX", Y.RFC, Y.CURP, Y.Email);

                Y = null;
                if (UPDATE)
                {
                    Limpia_Cajas();
                    clsFuncionesGral.Activa_Paneles(Panel1, false);
                    clsFuncionesGral.Activa_Paneles(Panel2, true);
                    clsFuncionesGral.Activa_Paneles(pnlEdo, false);
                    clsFuncionesGral.Activa_Paneles(pnlCiudad, false);
                    Label7.Text = clsFuncionesGral.ConvertMayus("Sus datos se han actualizado correctamente.");
                }

            }

        }

        public void Limpia_Cajas()
        {
            Nombres.Text = Dictionary.CADENA_NULA;
            ApellidoPaterno.Text = Dictionary.CADENA_NULA;
            Apellido_Materno.Text = Dictionary.CADENA_NULA;
            Fecha_Nacimiento.Text = Dictionary.CADENA_NULA;
            Domicilio_Calle.Text = Dictionary.CADENA_NULA;
            Numero_Interno.Text = Dictionary.CADENA_NULA;
            Numero_Externo.Text = Dictionary.CADENA_NULA;
            Colonia.Text = Dictionary.CADENA_NULA;
            Delegacion.Text = Dictionary.CADENA_NULA;
            RFC.Text = Dictionary.CADENA_NULA;
            CURP.Text = Dictionary.CADENA_NULA;
            EMAIL.Text = Dictionary.CADENA_NULA;
            txtCiudad.Text = Dictionary.CADENA_NULA;
            txtEdo.Text = Dictionary.CADENA_NULA;
        }

        /// <summary>
        /// metrodo que valida campos
        /// </summary>
        public void validacampos()
        {
            editado = false;

            if ((Nombres.Text == Dictionary.CADENA_NULA) | (Nombres.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('NOMBRE ES OBLIGATORIO');", true);
                return;
            }
            if ((ApellidoPaterno.Text == Dictionary.CADENA_NULA) | (ApellidoPaterno.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('APELLIDO PATERNO ES OBLIGATORIO');", true);
                return;
            }

            if ((Apellido_Materno.Text == Dictionary.CADENA_NULA) | (Apellido_Materno.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('APELLIDO MATERNO ES OBLIGATORIO');", true);
                return;
            }

            if ((Fecha_Nacimiento.Text == Dictionary.CADENA_NULA) | (Fecha_Nacimiento.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('FECHA DE NACIMIENTO ES OBLIGATORIO');", true);
                return;
            }


            if ((Domicilio_Calle.Text == Dictionary.CADENA_NULA) | (Domicilio_Calle.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('DOMICILIO CALLE ES OBLIGATORIO');", true);
                return;
            }
            /*
         if ((Numero_Interno.Text == Dictionary.CADENA_NULA) | (Numero_Interno.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('NUMERO INTERNO ES OBLIGATORIO');", true);
                return;
            }*/

            if ((Numero_Externo.Text == Dictionary.CADENA_NULA) | (Numero_Externo.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('NUMERO EXTERNO ES OBLIGATORIO');", true);
                return;
            }

            if ((Colonia.Text == Dictionary.CADENA_NULA) | (Colonia.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('COLONIA ES OBLIGATORIO');", true);
                return;
            }
            if ((Delegacion.Text == Dictionary.CADENA_NULA) | (Delegacion.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('DELEGACION ES OBLIGATORIO');", true);
                return;
            }

            if ((RFC.Text == Dictionary.CADENA_NULA) | (RFC.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('RFC ES OBLIGATORIO');", true);
                return;
            }


            if ((CURP.Text == Dictionary.CADENA_NULA) | (CURP.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('CURP ES OBLIGATORIO');", true);
                return;
            }

            if ((EMAIL.Text == Dictionary.CADENA_NULA) | (EMAIL.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('EMAIL ES OBLIGATORIO');", true);
                return;
            }


            if ((txtEdo.Text == null) | (txtEdo.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('ESTADO ES OBLIGATORIO');", true);
                return;
            }




            if ((txtCiudad.Text == null) | (txtCiudad.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('CIUDAD ES OBLIGATORIO');", true);
                return;
            }




            while (!editado)
            {
                if ((Nombres.Text != Y.Nombre))
                {
                    editado = true;
                    break;
                }

                if ((ApellidoPaterno.Text != Y.ApPat))
                {
                    editado = true;
                    break;
                }
                if ((Apellido_Materno.Text != Y.ApMat))
                {
                    editado = true;
                    break;
                }

                if ((Fecha_Nacimiento.Text != Y.fech_nac))
                {
                    editado = true;
                    break;
                }


                if ((Domicilio_Calle.Text != Y.calle))
                {
                    editado = true;
                    break;
                }


                if ((Numero_Interno.Text != Y.num_int))
                {
                    editado = true;
                    break;
                }


                if ((Numero_Externo.Text != Y.numext))
                {
                    editado = true;
                    break;
                }

                if ((Colonia.Text != Y.colonia))
                {
                    editado = true;
                    break;
                }
                if ((Delegacion.Text != Y.delegacion))
                {
                    editado = true;
                    break;
                }


                if ((RFC.Text != Y.RFC))
                {
                    editado = true;
                    break;
                }

                if ((CURP.Text != Y.CURP))
                {
                    editado = true;
                    break;
                }

                if ((EMAIL.Text != Y.Email))
                {
                    editado = true;
                    break;
                }

                if (lsEstado != Y.Estado)
                {
                    editado = true;
                    break;
                }

                if (lsCiudad != Y.CD)
                {
                    editado = true;
                    break;
                }
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void imgChangeEdo_Click(object sender, ImageClickEventArgs e)
        {
            dplEstado.DataSource = MngNegocioDependencia.Obtiene_Estados("MX");
            dplEstado.DataTextField = Dictionary.DESCRIPCION;
            dplEstado.DataValueField = Dictionary.CODIGO;
            dplEstado.DataBind();
            clsFuncionesGral.Activa_Paneles(pnlEdo, true);

        }

        protected void dplEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsEstado = dplEstado.SelectedValue.ToString();
            if (lsEstado == "00")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un estado para continuar');", true);
                return;
            }
            else
            {
                if (Y.Estado != lsEstado)
                {
                    clsFuncionesGral.Activa_Paneles(pnlCiudad, true);
                    txtEdo.Text = MngNegocioDependencia.Obtiene_Descripcion_Estado(lsEstado);
                    txtCiudad.Text = Dictionary.CADENA_NULA;

                    dplCiudad.DataSource = MngNegocioDependencia.Obtiene_ciudades(lsEstado);
                    dplCiudad.DataTextField = Dictionary.DESCRIPCION;
                    dplCiudad.DataValueField = Dictionary.CODIGO;
                    dplCiudad.DataBind();
                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlEdo, false);
                    clsFuncionesGral.Activa_Paneles(pnlCiudad, false);
                    dplEstado.Items.Clear();
                    dplCiudad.Items.Clear();
                }
            }


        }

        protected void dplCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsCiudad = dplCiudad.SelectedValue.ToString();
            if (lsCiudad == "00")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una ciudad para continuar');", true);
                return;
            }
            else
            {
                txtCiudad.Text = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(lsCiudad, lsEstado);
                clsFuncionesGral.Activa_Paneles(pnlEdo, false);
                clsFuncionesGral.Activa_Paneles(pnlCiudad, false);
                dplEstado.Items.Clear();
                dplCiudad.Items.Clear();
            }
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Limpia_Cajas();
            Destructor();
            Response.Redirect("../Home/Home.aspx", true);
        }

        public void Destructor()
        {
            Y = null;
            dplCiudad.Items.Clear();
            dplEstado.Items.Clear();
        }

    }
}


