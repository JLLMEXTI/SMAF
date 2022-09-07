using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;

namespace InapescaWeb.Catalogos
{
    public partial class Proyecto_Partida : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");
            //Datos de session
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
        //Datos de pagina
        static string lsDep;
        static string lsComponente;
        static string lsCapitulo;
        static string lsSubcapitulo;
        static List<Entidad> ListPartidas;
        static string lsProyecto;
        
        static string lsPartidas;
        static string lsEnero, lsFebrero, lsMarzo, lsAbril, lsMayo, lsJunio, lsJulio, lsAgosto, lsSeptiembre, lsOctubre, lsNoviembre, lsDiciembre;


        /// <summary>
        /// metodo principal de carga de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Session();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();
                Carga_Valores();
                Genera_Lista();
              
            }
        }

        /// <summary>
        /// Metodo que valida rol y activa paneles y llena dropdonwlist segun este 
        /// </summary>
        public void Genera_Lista()
        {

            if ((lsRol == Dictionary.DIRECTOR_ADMINISTRACION) | (lsRol == Dictionary.DIRECTOR_GRAL))
            {
                pnlUniAdmin.Visible = true;
                dplUnidadesAdministrativas.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplUnidadesAdministrativas.DataTextField = "Descripcion";
                dplUnidadesAdministrativas.DataValueField = "Codigo";
                dplUnidadesAdministrativas.DataBind();
            }
            else if (lsRol == Dictionary.DIRECTOR_ADJUNTO)
            {
                pnlUniAdmin.Visible = true;
                dplUnidadesAdministrativas.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion(lsUbicacion);
                dplUnidadesAdministrativas.DataTextField = "Descripcion";
                dplUnidadesAdministrativas.DataValueField = "Codigo";
                dplUnidadesAdministrativas.DataBind();

            }
            else
            {
                pnlProyectos.Visible = true;
                
               clsFuncionesGral .  Llena_Capitulo(dplCapitulos);

                dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(lsUsuario, lsRol, lsUbicacion);
                dplProyectos.DataTextField = "Descripcion";
                dplProyectos.DataValueField = "Codigo";
                dplProyectos.DataBind();

                dplComponente.DataSource = MngNegocioPresupuestarios.ObtienePresupuestarios();
                dplComponente.DataTextField = "Descripcion";
                dplComponente.DataValueField = "Codigo";
                dplComponente.DataBind();
            }
        }

      
        /// <summary>
        /// Metodo que carga datos de sesssion de usuario
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

        /// <summary>
        /// Metodo que caraga valores iniciales postback de pagina
        /// </summary>
        public void Carga_Valores()
        {
            Propiedades_Paneles(false);
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";
            Label1.Text = clsFuncionesGral.ConvertMayus("Catalogo de Carga de proyectos y partidas mensuales ");
            Label2.Text = clsFuncionesGral.ConvertMayus("Unidades Administrativas :");
            Label3.Text = clsFuncionesGral.ConvertMayus("Proyectos :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Componente Presupuestario:");
            Label5.Text = clsFuncionesGral.ConvertMayus("Partida Presupuestal :");
            Label6.Text = clsFuncionesGral.ConvertMayus("Capitulos Presupuestarios:");
            Label7.Text = clsFuncionesGral.ConvertMayus("SubCapitulos Presupuestales");
            Label8.Text = clsFuncionesGral.ConvertMayus("Enero : ");
            Label9.Text = clsFuncionesGral.ConvertMayus("Febrero : ");
            Label10.Text = clsFuncionesGral.ConvertMayus("Marzo : ");
            Label11.Text = clsFuncionesGral.ConvertMayus("Abril : ");
            Label12.Text = clsFuncionesGral.ConvertMayus("Mayo  : ");
            Label13.Text = clsFuncionesGral.ConvertMayus("Junio : ");
            Label14.Text = clsFuncionesGral.ConvertMayus("Julio : ");
            Label15.Text = clsFuncionesGral.ConvertMayus("Agosto : ");
            Label16.Text = clsFuncionesGral.ConvertMayus("Septiembre : ");
            Label17.Text = clsFuncionesGral.ConvertMayus("Octubre : ");
            Label18.Text = clsFuncionesGral.ConvertMayus("Noviembre : ");
            Label19.Text = clsFuncionesGral.ConvertMayus("Diciembre : ");
            Button1.Text = clsFuncionesGral.ConvertMayus("Agregar");
            Button2.Text = clsFuncionesGral.ConvertMayus(" Limpiar Campos ");

        }

        /// <summary>
        /// Metodo que asigna valores de propiedades iniciales a paneles
        /// </summary>
        /// <param name="pbBandera"></param>
        public void Propiedades_Paneles(bool pbBandera)
        {
            pnlUniAdmin.Visible = pbBandera;
            pnlProyectos.Visible = pbBandera;
            pnlSubpartidas.Visible = false;
        }

        /// <summary>
        /// Metodo que redirige a home desde button inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
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

        /// <summary>
        /// metodo que carga datos de proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            lsDep = dplUnidadesAdministrativas.SelectedValue;
            if ((lsDep == null) | (lsDep == Dictionary.CADENA_NULA ))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una Unidad Administrativa para poder avanzar');", true);
                return;
            }
            else
            {
                pnlProyectos.Visible = true;
                clsFuncionesGral .  Llena_Capitulo(dplCapitulos);
                dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(lsUsuario, lsRol, lsDep );
                dplProyectos.DataTextField = "Descripcion";
                dplProyectos.DataValueField = "Codigo";
                dplProyectos.DataBind();

                dplComponente.DataSource = MngNegocioPresupuestarios.ObtienePresupuestarios();
                dplComponente.DataTextField = "Descripcion";
                dplComponente.DataValueField = "Codigo";
                dplComponente.DataBind();
            }
        }
        /// <summary>
        /// Obtiene dato y asigna numero de partida a agregar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvPartidasPeaje_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtPartida.Text = Convert.ToString(tvPartidasPeaje .SelectedNode.Value);
            lsPartidas = Convert.ToString(tvPartidasPeaje.SelectedNode.Value); ;
        }

        /// <summary>
        /// Obtiene capitulo y carga partidas de ese capitulo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplCapitulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsCapitulo = dplCapitulos.SelectedValue;
            txtPartida.Text = Dictionary.CADENA_NULA;
            ListPartidas = MngNegocioPartidas.ListaPartidas(lsCapitulo);
            if (ListPartidas.Count == 0)
            {
                clsFuncionesGral.LlenarTreeView(lsCapitulo, tvPartidasPeaje, true);//   ConstruyePartidas();
                pnlSubpartidas.Visible = false;
                dplSUbCapitulo.Items.Clear();
            }
            else
            {
            pnlSubpartidas.Visible = true ;
            dplSUbCapitulo.DataSource = ListPartidas;
            dplSUbCapitulo.DataTextField = "Descripcion";
            dplSUbCapitulo.DataValueField = "Codigo";
            dplSUbCapitulo.DataBind();
            }
        }

        /// <summary>
        /// Caega partidas de sub partidas!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplSUbCapitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsSubcapitulo = dplSUbCapitulo.SelectedValue;
            clsFuncionesGral.LlenarTreeView(lsSubcapitulo  , tvPartidasPeaje, true);//   ConstruyePartidas();
            txtPartida.Text = Dictionary.CADENA_NULA;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Inserta en la bd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            valida_campos();
            try
            {
                MngNegocioProyecto.Inserta_Detalle_Presupuesto(lsProyecto, lsComponente, lsCapitulo, lsSubcapitulo, lsPartidas, lsDep, lsEnero, lsFebrero, lsMarzo, lsAbril, lsMayo, lsJunio, lsJulio, lsAgosto, lsSeptiembre, lsOctubre, lsNoviembre, lsDiciembre);
                Destructor();
            }
            catch 
            {
                
            }
            finally
            { 
            
            }
        }

        public void Destructor()
        {
            pnlSubpartidas.Visible = false;
            dplSUbCapitulo.Items.Clear();
            dplCapitulos.Items.Clear();
            dplProyectos.Items.Clear();
            dplSUbCapitulo.Items.Clear();
            dplComponente.Items.Clear();

            lsDep = Dictionary.CADENA_NULA;
            lsProyecto = Dictionary.CADENA_NULA;
            lsComponente = Dictionary.CADENA_NULA;
            lsCapitulo = Dictionary.CADENA_NULA;
            lsSubcapitulo = Dictionary.CADENA_NULA  ;
            lsPartidas = Dictionary.CADENA_NULA ;

            lsEnero = Dictionary.CADENA_NULA; 
            lsFebrero = Dictionary.CADENA_NULA;
            lsMarzo = Dictionary.CADENA_NULA;
            lsAbril = Dictionary.CADENA_NULA;
            lsMayo = Dictionary.CADENA_NULA;
            lsJunio = Dictionary.CADENA_NULA;
            lsJulio = Dictionary.CADENA_NULA;
            lsAgosto = Dictionary.CADENA_NULA;
            lsSeptiembre = Dictionary.CADENA_NULA;
            lsOctubre = Dictionary.CADENA_NULA;
            lsNoviembre = Dictionary.CADENA_NULA;
            lsDiciembre = Dictionary.CADENA_NULA;

            txtEnero.Text = Dictionary.CADENA_NULA;
            txtFebrero.Text = Dictionary.CADENA_NULA;
            txtMarzo.Text = Dictionary.CADENA_NULA;
            txtAbril.Text = Dictionary.CADENA_NULA;
            txtMayo.Text = Dictionary.CADENA_NULA;
            txtJunio.Text = Dictionary.CADENA_NULA;
            txtJulio.Text = Dictionary.CADENA_NULA;
            txtAgosto.Text = Dictionary.CADENA_NULA;
            txtSeptiembre.Text = Dictionary.CADENA_NULA;
            txtOctubre.Text = Dictionary.CADENA_NULA;
            txtNoviembre.Text = Dictionary.CADENA_NULA;
            txtDiciembre.Text = Dictionary.CADENA_NULA;

            txtPartida.Text = Dictionary.CADENA_NULA;

            tvPartidasPeaje.Nodes.Clear();
            Genera_Lista();

        }
       
        /// <summary>
        /// Metodo que valida campos de insert
        /// </summary>
        public void valida_campos()
        {
            lsProyecto = dplProyectos.SelectedValue;
            lsComponente = dplComponente.SelectedValue;
            lsCapitulo = dplCapitulos.SelectedValue;
            lsEnero = txtEnero.Text;
            lsFebrero = txtFebrero.Text;
            lsMarzo = txtMarzo.Text;
            lsAbril = txtAbril.Text;
            lsMayo = txtMayo.Text;
            lsJunio = txtJunio.Text;
            lsJulio = txtJulio.Text;
            lsAgosto = txtAgosto.Text;
            lsSeptiembre = txtSeptiembre.Text;
            lsOctubre = txtOctubre.Text;
            lsNoviembre = txtNoviembre.Text;
            lsDiciembre = txtDiciembre.Text;

            if (pnlUniAdmin.Visible)
            {
                lsDep = dplUnidadesAdministrativas.SelectedValue;

                if ((lsDep == null) | (lsDep == Dictionary.CADENA_NULA))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Unidad Administrativa no puede estar vacia. Seleccione alguno de la lista');", true);
                    return;
                }
            }
            else
            {
                lsDep = lsUbicacion;   
            }

            if ((lsProyecto == null) | (lsProyecto == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Proyecto no puede estar vacio. Seleccione alguno de la lista');", true);
                return;
            }


            if ((lsComponente == null) | (lsComponente == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Componente Presupuestario no puede estar vacio. Seleccione alguno de la lista');", true);
                return;
            }

            if ((lsCapitulo == null) | (lsCapitulo == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Capitulo Presupuestario no puede estar vacio. Seleccione alguno de la lista');", true);
                return;
            }


            if (pnlSubpartidas.Visible)
            {
                lsSubcapitulo = dplSUbCapitulo.SelectedValue;

                if ((lsSubcapitulo == null) | (lsSubcapitulo == Dictionary.CADENA_NULA))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('SubCapituloPresupuestario no puede estar vacio. Seleccione alguno de la lista');", true);
                    return;
                }
            }
            else
            {
                lsSubcapitulo = Dictionary.NUMERO_CERO;
            }

            if ((lsPartidas  == null) | (lsPartidas  == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partidas presupuestarias no pueden estar vacio. Seleccione alguno de la lista');", true);
                return;
            }


            if ((txtEnero.Text == null) | (txtEnero.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric (txtEnero.Text ))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

        
            if ((txtFebrero.Text == null) | (txtFebrero.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtFebrero.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtMarzo.Text == null) | (txtMarzo.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtMarzo.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtAbril.Text == null) | (txtAbril.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtAbril.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtJunio.Text == null) | (txtJunio.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtJunio.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtJulio.Text == null) | (txtJulio.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtJulio.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtAgosto.Text == null) | (txtAgosto.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtAgosto.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtSeptiembre.Text == null) | (txtSeptiembre.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtSeptiembre.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtOctubre.Text == null) | (txtOctubre.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtOctubre.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtNoviembre.Text == null) | (txtNoviembre.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtNoviembre.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

            if ((txtDiciembre.Text == null) | (txtDiciembre.Text == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Campo Enero no puede ser vacio.');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtDiciembre.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Campo Enero no es numerico');", true);
                return;
            }

        }

    }
}