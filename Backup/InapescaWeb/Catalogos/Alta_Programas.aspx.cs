using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;

namespace InapescaWeb.Catalogos
{
    public partial class Alta_Programas : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");
       
        static string Year = DateTime.Today.Year.ToString();
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

        static string lsAdscripcion;
        static string lsnumcomponente;
        static string lsPrograma;
        static string lsdescripcion;
        static string lsdescripcorta;
        static string lsobjetivo;
        static string lscoordinador;
        static string lsdireccion;
        static string lspresupuestototal;
        static string lsPresupuestoLocal;
        static bool Bandera;

        /// <summary>
        /// evento page load que carga al iniciar la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Session();
                if ((lsRol == Dictionary.DIRECTOR_ADJUNTO) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION) | (lsRol == Dictionary.DIRECTOR_GRAL) | (lsRol == Dictionary.JEFE_CENTRO))
                {
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);
                    Carga_Valores();
                }
                else
                {
                    Response.Redirect("../Home/Home.aspx", true);
                }

            }
        }

        /// <summary>
        /// Metodo que caraga valores iniciales postback de pagina
        /// </summary>
        public void Carga_Valores()
        {

            Bandera = true;
            chkLocal.Text = clsFuncionesGral.ConvertMayus("agregar presupuesto local");
            chkLocal.Checked = false;
            btnAgregar.Text = clsFuncionesGral.ConvertMayus("Agregar");
            btnCancelar.Text = clsFuncionesGral.ConvertMayus("Limpiar");
            if ((lsRol == Dictionary.DIRECTOR_GRAL) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION) | (lsRol == Dictionary.DIRECTOR_ADJUNTO))
            {
                Label1.Text = clsFuncionesGral.ConvertMayus("ALTA DE PROGRAMAS globales de direccion");
                Label8.Text = clsFuncionesGral.ConvertMayus("PRESUPUESTO GLOBAL");
            }
            else if (lsRol == Dictionary.JEFE_CENTRO)
            {
                Label1.Text = clsFuncionesGral.ConvertMayus("ALTA DE PROGRAMAS por crip");
                Label8.Text = clsFuncionesGral.ConvertMayus("PRESUPUESTO anual");
            }
            Label16.Text = clsFuncionesGral.ConvertMayus("NUMERO DE COMPONENTE");
            //  Label2.Text = clsFuncionesGral.ConvertMayus("CLAVE DE PROGRAMA");
            Label3.Text = clsFuncionesGral.ConvertMayus("DESCRIPCION");
            Label4.Text = clsFuncionesGral.ConvertMayus("DESCRIPCION CORTA");
            Label5.Text = clsFuncionesGral.ConvertMayus("OBJETIVO");
            Label6.Text = clsFuncionesGral.ConvertMayus("COORDINADOR");
            Label7.Text = clsFuncionesGral.ConvertMayus("ADSCRIPCION");

            Label10.Text = clsFuncionesGral.ConvertMayus("Programa global");
            Label11.Text = clsFuncionesGral.ConvertMayus("Direcciones adjuntas");
            Label2.Text = clsFuncionesGral.ConvertMayus("Tipo Usuario :");
            Label9.Text = clsFuncionesGral.ConvertMayus("Presupuesto local");

            clsFuncionesGral.Activa_Paneles(pnlPresupuestoLocal, false);

            if ((lsRol == Dictionary.DIRECTOR_GRAL))// | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION))
            {
                //llenar direcciones adjuntas
                clsFuncionesGral.Activa_Paneles(pnlDireccion, true);
                dplDireccion.DataSource = MngNegocioDirecciones.ObtienenDirecciones( Year ,true);
                dplDireccion.DataValueField = "Codigo";
                dplDireccion.DataTextField = "Descripcion";
                dplDireccion.DataBind();

                txtCoordinador.Visible = true;
                dplCoordinador.Visible = false;
                clsFuncionesGral.Activa_Paneles(pnlComponente, true);
                dplComponente.DataSource = MngNegocioComponente.Obtienencomponente();
                dplComponente.DataValueField = "Codigo";
                dplComponente.DataTextField = "Descripcion";
                dplComponente.DataBind();

                clsFuncionesGral.Activa_Paneles(pnlProgramasDir, false);
                clsFuncionesGral.Activa_Paneles(pnlAdscripcion, false);
                clsFuncionesGral.Activa_Paneles(pnlTipoUsuarios, false);
                chkLocal.Visible = true;
            }
            else if ((lsRol == Dictionary.DIRECTOR_ADJUNTO) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION))
            {
                clsFuncionesGral.Llena_Lista(dplTipoUsuario, "= S E L E C C I O N E =|Usuarios Locales|Usuarios Crips");

                clsFuncionesGral.Activa_Paneles(pnlDireccion, false);
                txtCoordinador.Visible = false;
                dplCoordinador.Visible = true;

                clsFuncionesGral.Activa_Paneles(pnlComponente, true);
                clsFuncionesGral.Activa_Paneles(pnlProgramasDir, false);
                clsFuncionesGral.Activa_Paneles(pnlTipoUsuarios, true);
                clsFuncionesGral.Activa_Paneles(pnlAdscripcion, false);

                dplComponente.DataSource = MngNegocioComponente.Obtienencomponente();
                dplComponente.DataValueField = "Codigo";
                dplComponente.DataTextField = "Descripcion";
                dplComponente.DataBind();
                chkLocal.Visible = true;

            }
            else
            {

                Descripcion.Enabled = false;
                DescripcionCorta.Enabled = false;
                Objetivo.Enabled = false;

                txtCoordinador.Visible = false;
                dplCoordinador.Visible = true;
                clsFuncionesGral.Activa_Paneles(pnlDireccion, false);
                clsFuncionesGral.Activa_Paneles(pnlComponente, false);
                clsFuncionesGral.Activa_Paneles(pnlProgramasDir, true);
                clsFuncionesGral.Activa_Paneles(pnlAdscripcion, false);
                clsFuncionesGral.Activa_Paneles(pnlTipoUsuarios, false);
                dplProgramas.DataSource = MngNegociosProgramas.Obtiene_Programas_Direccion(lsUbicacion);
                dplProgramas.DataValueField = "Codigo";
                dplProgramas.DataTextField = "Descripcion";
                dplProgramas.DataBind();

                dplCoordinador.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsUbicacion, lsUsuario, "", true);
                dplCoordinador.DataValueField = "Codigo";
                dplCoordinador.DataTextField = "Descripcion";
                dplCoordinador.DataBind();
                chkLocal.Visible = false;
                //llenar usuarios de crip
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
        /// Evento de boton regreso a HOME
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
        /// Metodo que valida Campos
        /// </summary>
        /// <returns></returns>
        public void validacampos()
        {
            if (lsnumcomponente == "0")
            {
                Bandera = false;
            }

            if ((lsdescripcion == null) | (lsdescripcion == ""))
            {
                Bandera = false;
            }


            if ((lsdescripcorta == null) | (lsdescripcorta == ""))
            {
                Bandera = false;
            }

            if ((lsobjetivo == null) | (lsobjetivo == ""))
            {
                Bandera = false;
            }

            if ((lscoordinador == null) | (lscoordinador == ""))
            {
                Bandera = false;
            }

            if ((lsdireccion == null) | (lsdireccion == ""))
            {
                Bandera = false;
            }


            if ((lspresupuestototal == null) | (lspresupuestototal == ""))
            {
                Bandera = false;
            }

            if (!clsFuncionesGral.IsNumeric(lspresupuestototal))
            {
                Bandera = false;
            }

            if (pnlPresupuestoLocal.Visible)
            {
                if ((lsPresupuestoLocal == null) | (lsPresupuestoLocal == Dictionary.CADENA_NULA))
                {
                    Bandera = false;
                }
                else if (!clsFuncionesGral.IsNumeric(lsPresupuestoLocal))
                {
                    Bandera = false;
                }
            }

        }

        /// <summary>
        /// Evento click de boton agregar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Label9.Text = "";
            if (lsRol == Dictionary.DIRECTOR_GRAL)
            {
                lsdireccion = dplDireccion.SelectedValue.ToString();
                lsnumcomponente = dplComponente.SelectedValue.ToString();
            }
            else if ((lsRol == Dictionary.DIRECTOR_ADJUNTO) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION))
            {
                lsdireccion = lsUbicacion;
                lsnumcomponente = dplComponente.SelectedValue.ToString();

            }
            else if (lsRol == Dictionary.JEFE_CENTRO)
            {
                lsdireccion = MngNegocioDependencia.Obtiene_Direccion(lsUbicacion);
            }

            lscoordinador = dplCoordinador.SelectedValue.ToString();
            lsdescripcion = Descripcion.Text;

            lsdescripcorta = DescripcionCorta.Text;

            lsobjetivo = Objetivo.Text;

            lspresupuestototal = PrpTotal.Text;

            if (pnlPresupuestoLocal.Visible)
            {
                lsPresupuestoLocal = txtPresupuestoLocal.Text;
            }

            validacampos();
            bool Resultado;
            if (Bandera)
            {
                if ((lsRol == Dictionary.DIRECTOR_GRAL) | (lsRol == Dictionary.DIRECTOR_ADJUNTO) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION))
                {
                    lsPrograma = MngNegociosProgramas.Obtiene_Max_Programa(lsdireccion);
                    Resultado = MngNegociosProgramas.Insert_Programas(lsnumcomponente, lsPrograma, clsFuncionesGral.ConvertMayus(lsdescripcion), clsFuncionesGral.ConvertMayus(lsdescripcorta), clsFuncionesGral.ConvertMayus(lsobjetivo), lscoordinador, lsdireccion, lspresupuestototal);

                    //CONFIRMACION
                    if (Resultado) ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('SE HA AGREGADO EL PROGRAMA CORRECTAMENTE.');", true);
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al insertar el programa favor de validar.');", true);
                        return;
                    }

                    if (pnlPresupuestoLocal.Visible)
                    {
                        Entidad ent = MngNegocioDependencia.Obtiene_Tipo_Region(lsdireccion);
                        Resultado = MngNegocioProyecto.Inserta_Proyecto(lsnumcomponente, lsPrograma,lsdireccion , clsFuncionesGral.ConvertMayus(lsdescripcion), clsFuncionesGral.ConvertMayus(lsdescripcorta), lscoordinador, clsFuncionesGral.ConvertMayus(lsobjetivo), lsdireccion, ent.Descripcion, lsArea, lsPresupuestoLocal);
                    }

                    if (Resultado) ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('SE HA AGREGADO EL PROYECTO LOCAL  CORRECTAMENTE.');", true);
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al insertar el proyecto local favor de validar.');", true);
                        return;
                    }

                    Limpia_Datos();
                }
                else if (lsRol == Dictionary.JEFE_CENTRO)
                {
                    Entidad ent = MngNegocioDependencia.Obtiene_Tipo_Region(lsUbicacion);
                    Resultado = MngNegocioProyecto.Inserta_Proyecto(lsnumcomponente, lsPrograma, lsdireccion, clsFuncionesGral.ConvertMayus(lsdescripcion), clsFuncionesGral.ConvertMayus(lsdescripcorta), lscoordinador, clsFuncionesGral.ConvertMayus(lsobjetivo), lsUbicacion, ent.Descripcion, lsArea, lspresupuestototal);
                    Limpia_Datos();
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Todos los campos son obligatorios, presupuesto numerico.');", true);
                return;
                //                Label9.Text = clsFuncionesGral.ConvertMayus("Todos los campos son obligatorios; El Presupuesto Total es de valor Numerico");
            }
        }

        /// <summary>
        /// metodo que limpia datos en cache para volver a capturar
        /// </summary>
        public void Limpia_Datos()
        {
            txtCoordinador.Text = Dictionary.CADENA_NULA;
            Descripcion.Text = Dictionary.CADENA_NULA;
            DescripcionCorta.Text = Dictionary.CADENA_NULA;
            dplTipoUsuario.SelectedIndex = 0;
            dplCoordinador.Items.Clear();
            PrpTotal.Text = Dictionary.CADENA_NULA;
            Objetivo.Text = Dictionary.CADENA_NULA;

            lspresupuestototal = Dictionary.CADENA_NULA;
            lsdescripcion = Dictionary.CADENA_NULA;
            lsdescripcorta = Dictionary.CADENA_NULA;
            lscoordinador = Dictionary.CADENA_NULA;
            lsobjetivo = Dictionary.CADENA_NULA;

            clsFuncionesGral.Activa_Paneles(pnlPresupuestoLocal, false);
            chkLocal.Checked = false;
            dplComponente.SelectedIndex = 0;
            dplAdscripcion.Items.Clear();
            clsFuncionesGral.Activa_Paneles(pnlAdscripcion, false);

        }

        /// <summary>
        /// Evento click de boton cancelar o limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpia_Datos();
        }

        /// <summary>
        /// Evento select de lista direcciones adjuntas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsdireccion = dplDireccion.SelectedValue.ToString();
            if (lsdireccion == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una direccion para continuar');", true);
                return;
            }
            else
            {
                lsAdscripcion = lsdireccion;
                Entidad obj = MngNegocioUsuarios.Datos_DirAdjunto(lsdireccion);
                txtCoordinador.Text = obj.Descripcion;
                lscoordinador = obj.Codigo;
            }

        }

        /// <summary>
        /// metodo que valida seleccion de adscripcion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplAdscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            if (lsAdscripcion == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adcripcion para continuar');", true);
                return;
            }
            else
            {
                dplCoordinador.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, lsUsuario, lsRol, true);
                dplCoordinador.DataValueField = "Codigo";
                dplCoordinador.DataTextField = "Descripcion";
                dplCoordinador.DataBind();
            }
        }

        /// <summary>
        /// metodo que obtiene y valida tipo usuarios a caragar en coordinadores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstipo = dplTipoUsuario.SelectedValue.ToString();
            if (lstipo == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione untipo de usuario para continuar');", true);
                return;
            }
            else
            {
                if (lstipo == "2")
                {
                    clsFuncionesGral.Activa_Paneles(pnlAdscripcion, false);
                    dplAdscripcion.Items.Clear();
                    dplCoordinador.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsUbicacion, lsUsuario, lsRol, true);
                    dplCoordinador.DataValueField = "Codigo";
                    dplCoordinador.DataTextField = "Descripcion";
                    dplCoordinador.DataBind();
                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlAdscripcion, true);

                    if (lsUbicacion == Dictionary.DGAIA)
                    {
                        dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                    }
                    else
                    {
                        dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion(lsUbicacion);
                    }
                    dplCoordinador.Items.Clear();
                    dplAdscripcion.DataValueField = "Codigo";
                    dplAdscripcion.DataTextField = "Descripcion";
                    dplAdscripcion.DataBind();
                }
            }

        }

        /// <summary>
        /// evento click de select programas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsPrograma = dplProgramas.SelectedValue.ToString();
            Programa objPrograma = MngNegociosProgramas.Obtiene_Datos_Programa(lsPrograma, MngNegocioDependencia.Obtiene_Direccion(lsUbicacion));
            if ((objPrograma.Id_Programa != null) | (objPrograma.Id_Programa != Dictionary.CADENA_NULA))
            {
                lsnumcomponente = objPrograma.Componente;

                Descripcion.Text = objPrograma.Descripcion;
                DescripcionCorta.Text = objPrograma.Descripcion_Corta;
                Objetivo.Text = objPrograma.Objetivo;

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('programa no existente');", true);
                return;
            }
        }

        /// <summary>
        /// evento click de check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocal.Checked)
            {
                clsFuncionesGral.Activa_Paneles(pnlPresupuestoLocal, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlPresupuestoLocal, false);
            }
        }

    }
}
