using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;

namespace InapescaWeb.Solicitudes
{
    public partial class Solicitud_Comision : System.Web.UI.Page
    {
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static clsDictionary dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");
        bool DIFERENCIA = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    clsFuncionesGral.LlenarTreeViews(dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
                    Carga_Valores();
                    CrearTabla();
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        public void Carga_Valores()
        {
            Label6.Text = "SI ESTA COMISION ES UNA AMPLIACIÓN FAVOR DE SELECCIONAR EL NUMERO DE OFICIO DE SMAF PARA LIGAR LA COMPROBACION Y SOLICITUD ACTUAL </br> NOTA: LAS AMPLIACIONES DE COMISIÓN SON PERSONALES POR LO QUE EN LA LISTA DESPLEGABLE SOLO MOSTRARÁ LAS COMISIONES DEL USUARIO EN SESSION. ";
            Label12.Text = year;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lnkHome.Text = "INICIO";
            lblTitle.Text = " Programas ";
            lblDep.Text = "Ubicacion del Programa:";
            //btnDep.Text = dictionary.FIND;
            lblProyecto.Text = "Programa :";
            Label3.Text = "Especies / Pesqueria :";
            Label4.Text = " Metas Institucionales :";
            Label5.Text = "Actividad Especifica : ";
            lblObserTrans.Text = "Observaciones de Transporte :";
            clsFuncionesGral.Activa_Paneles(pnlEspecie, false);
            clsFuncionesGral.Activa_Paneles(pnlExternos, false);
            clsFuncionesGral.Activa_Paneles(pnlAmpliacion, false);
            if ((Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_ADMINISTRACION) || (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_GRAL))
            {
                clsFuncionesGral.Llena_Lista(dplTipoComision, "= S E L E C C I O N E =|NACIONAL|INTERNACIONAL");
            }
            else
            {
                clsFuncionesGral.Llena_Lista(dplTipoComision, "= S E L E C C I O N E =|NACIONAL");
            }

            Llena_Tipou(dplUsuarios);

            btnProyectos.Text = dictionary.SIGUIENTE;
            lblTituloComisionado.Text = "Agregar Comisionados ";
            Label2.Text = "Tipo comisión:";
            lblusuarios.Text = "Tipo de Comisionados";
            btnUsuarios.Text = " Buscar ";
            btnUbicacion.Text = " Buscar ";
            btnAdd.Text = "Agregar";

            btnAntProy.Text = dictionary.ATRAS;
            btnSigUbic.Text = dictionary.SIGUIENTE;


            lblUbicacion.Text = "Centro : ";
            lblComisionados.Text = "Nombre del Comisionado : ";

            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());
            Session["Crip_Tipo"] = oDireccionTipo.Descripcion;
            string lsDep;

            if ((Session["Crip_Rol"].ToString() == "ADMGR") | (Session["Crip_Rol"].ToString() == dictionary.ADMINISTRADOR_INAPESCA) | (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_GRAL) | (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_ADMINISTRACION) | (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_ADJUNTO) | ((MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), dictionary.PERMISO_EXTERNO_PROYECTO) == dictionary.PERMISO_EXTERNO_PROYECTO)))
            {
                clsFuncionesGral.Activa_Paneles(pnlproyectos, true);
                dplDep.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplDep.DataTextField = "Descripcion";
                dplDep.DataValueField = "Codigo";
                dplDep.DataBind();
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlproyectos, false);
                lsDep = Session["Crip_Ubicacion"].ToString();
                List<Entidad> List = new List<Entidad>();

                List = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsDep);
                if ((Session["Crip_Rol"].ToString() == dictionary.JEFE_CENTRO) | (Session["Crip_Rol"].ToString() == dictionary.ADMINISTRADOR))
                {
                    List<Entidad> List2 = new List<Entidad>();
                    List2 = MngNegocioProyecto.ObtieneProyectosJefeyAdminCriaps(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsDep);
                    List = List.Concat(List2).ToList();
                }
                dplProyectos.DataSource = List;


                dplProyectos.DataTextField = "Descripcion";
                dplProyectos.DataValueField = "Codigo";
                dplProyectos.DataBind();


            }

            lbltitleUbicacion.Text = "Destino de la Comision";
            lblLugar.Text = "Destino :";
            lblDel.Text = "Del día :";
            lblAl.Text = "Al día :";
            lblObjetivo.Text = "Objetivo de la comisión:";
            Calendar1.Visible = false;
            Calendar2.Visible = false;
            btnAntComisionado.Text = dictionary.ATRAS;
            btnSigTransporte.Text = dictionary.SIGUIENTE;
            btnAntUbicacion.Text = dictionary.ATRAS;
            btnSigEquipo.Text = dictionary.SIGUIENTE;
            lblTransporte.Text = "Transporte a utilizar";
            lblClase.Text = "Clase:";
            lblTipo.Text = "Tipo:";
            lblVehiculo.Text = "Vehiculo:";
            lblDeps.Text = "Ubicación del Transporte :";

            lblTransporte.Text = "Transporte a utilizar";
            lblClase.Text = "Clase:";
            lblTipo.Text = "Tipo:";
            lblVehiculo.Text = "Vehiculo:";
            lblDeps.Text = "Ubicación del Transporte :";
            lblPasajes.Text = "Pasajes :";
            lblPeaje.Text = "Peaje :";

            lblCombus.Text = "Combustible :";

            lblRecorridoTerrestre.Text = "Recorrido Terrestre";
            lblRecorridoAereo.Text = "Recorrido Aereo";
            lblRecorridoAcuatico.Text = "Recorrido Acuatico";
            pnlRecorridoTerrestre.Visible = false;
            pnlRecorridoAcuatico.Visible = false;
            pnlRecorridoAereo.Visible = false;

            lblEquipo.Text = "Equipo";
            lblEquipo1.Text = "Equipo a utilizar:";
            lblObservacionesCom.Text = "Observaciones de Comisión:";
            btnAntTransp.Text = dictionary.ATRAS;
            btnGenerar.Text = "GENERAR.";

            clsFuncionesGral.Activa_Paneles(pnlVehiculo, false);
            clsFuncionesGral.Activa_Paneles(pnl11, false);
            clsFuncionesGral.Activa_Paneles(pnlVehiculo, false);
            clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
            clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
            clsFuncionesGral.Activa_Paneles(pnlPasajes, false);

            clsFuncionesGral.Activa_Paneles(pnlPais, false);
            clsFuncionesGral.Activa_Paneles(pnlEstado, false);


            dplEstado.DataSource = MngNegocioEstado.Estados();
            dplEstado.DataTextField = dictionary.DESCRIPCION;
            dplEstado.DataValueField = dictionary.CODIGO;
            dplEstado.DataBind();


            mvComision.ActiveViewIndex = 0;
        }

        protected void dplProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsCentro = dictionary.CADENA_NULA;
            string lsProyecto = dplProyectos.SelectedValue.ToString();

            /*
            if ((Session["Crip_Rol"].ToString() == "ADMGR") | (Session["Crip_Rol"].ToString() == dictionary.ADMINISTRADOR_INAPESCA) | (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_GRAL) | (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_ADMINISTRACION) | (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_ADJUNTO) | ((MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), dictionary.PERMISO_EXTERNO) == dictionary.PERMISO_EXTERNO)))
            {
                lsCentro = dplDep.SelectedValue.ToString();
            }
            else
            {
                lsCentro = Session["Crip_Ubicacion"].ToString();
            }
            */
            string[] ads = new string[2];
            ads = lsProyecto.Split(new Char[] { '|' });

            lsProyecto = ads[0];
            lsCentro = ads[1];

            if ((lsProyecto != string.Empty))
            {
                Proyecto objProyecto = new Proyecto();
                objProyecto = MngNegocioProyecto.ObtieneDatosProy(lsCentro, lsProyecto, year);

                if (objProyecto.Componente == "602")
                {
                    clsFuncionesGral.Activa_Paneles(pnlEspecie, false);
                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlEspecie, true);

                    dplEspecies.DataSource = MngNegocioEspecies.Obtiene_Especies(objProyecto.Programa, objProyecto.Direccion);
                    dplEspecies.DataValueField = dictionary.CODIGO;
                    dplEspecies.DataTextField = dictionary.DESCRIPCION;
                    dplEspecies.DataBind();

                    dplProductos.DataSource = MngNegocioProductos.Obtiene_Productos(year, objProyecto.Direccion, objProyecto.Componente);
                    dplProductos.DataTextField = dictionary.DESCRIPCION;
                    dplProductos.DataValueField = dictionary.CODIGO;
                    dplProductos.DataBind();

                    dplActividad.DataSource = MngNegocioActividades.Obtiene_Actividades(objProyecto.Componente, objProyecto.Direccion);
                    dplActividad.DataTextField = dictionary.DESCRIPCION;
                    dplActividad.DataValueField = dictionary.CODIGO;
                    dplActividad.DataBind();
                }

                objProyecto = null;
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlEspecie, false);
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar un Proyecto al cual cargar el egreso.');", true);
            }
        }

        protected void dplDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsCentro = dplDep.SelectedValue.ToString();

            if ((lsCentro == null) | (lsCentro == dictionary.CADENA_NULA) | (lsCentro == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar una unidad administrativa para buscar sus programas');", true);
                return;
            }
            else
            {

                if (MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), dictionary.PERMISO_EXTERNO) == dictionary.PERMISO_EXTERNO)
                {
                    dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), "", lsCentro);
                }
                else
                {
                    dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsCentro);
                }
                // dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsCentro);

                dplProyectos.DataTextField = "Descripcion";
                dplProyectos.DataValueField = "Codigo";
                dplProyectos.DataBind();
            }
        }

        /* protected void btnDep_Click(object sender, EventArgs e)
         {
             string lsCentro = dplDep.SelectedValue.ToString();

             if ((lsCentro == null) | (lsCentro == dictionary.CADENA_NULA) | (lsCentro == string.Empty))
             {
                 ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar una unidad administrativa para buscar sus programas');", true);
                 return;
             }
             else
             {

                 if (MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), dictionary.PERMISO_EXTERNO) == dictionary.PERMISO_EXTERNO)
                 {
                     dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), "", lsCentro);
                 }
                 else
                 {
                     dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsCentro);
                 }
                 // dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsCentro);

                 dplProyectos.DataTextField = "Descripcion";
                 dplProyectos.DataValueField = "Codigo";
                 dplProyectos.DataBind();
             }

         }*/

        public void CrearTabla()
        {
            List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

            InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

            if ((Session["Crip_Rol"].ToString() == dictionary.INVESTIGADOR) | (Session["Crip_Rol"].ToString() == dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == dictionary.JEFE_CENTRO) | (Session["Crip_Rol"].ToString() == dictionary.ENLACE))
            {
                objGrid.Comisionado = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
                objGrid.RFC = Session["Crip_Usuario"].ToString();

                Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(Session["Crip_Ubicacion"].ToString());
                objGrid.Adscripcion = Session["Crip_Ubicacion"].ToString() + "-" + oUbicacion.Desc_Corto;

                ListaGrid.Add(objGrid);
                Obtener_TablaActualizada(ListaGrid);
                oUbicacion = null;
            }
            ListaGrid = null;

        }

        public void Obtener_TablaActualizada(List<Entidades.GridView> plLista)
        {
            gvComisionados.DataSource = plLista;
            gvComisionados.DataBind();

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
                            if (!clsFuncionesGral.IsSessionTimedOut())
                            {
                                Response.Redirect(objWebPage.URL, true);
                            }
                            else
                            {
                                Response.Redirect("../Index.aspx", true);
                            }
                        }
                        if (objWebPage.Modulo == "99")
                        {
                            if (!clsFuncionesGral.IsSessionTimedOut())
                            {
                                Response.Redirect(objWebPage.URL, true);
                                Response.Redirect(objWebPage.URL, true);
                            }
                            else
                            {
                                Response.Redirect("../Index.aspx", true);
                            }
                        }
                    }
                    else
                    {
                        //message de error por hacer
                    }


                }


            }
        }

        protected void dplProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsProductos = dictionary.CADENA_NULA;

            Entidad ent = new Entidad();
            ent.Codigo = dplProductos.SelectedValue.ToString();
            ent.Descripcion = ent.Codigo + "-" + dplProductos.SelectedItem.Text;

            if (ent.Codigo == "00")
            {
                ent = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Deebe seleccionar un valor valido);", true);
                return;
            }
            else
            {
                if ((gvProductos.Rows.Count >= 0) & (gvProductos.Rows.Count <= 2))
                {
                    if (gvProductos.Rows.Count == 0)
                    {
                        List<Entidad> listaProductos = new List<Entidad>();
                        listaProductos.Add(ent);
                        gvProductos.DataSource = listaProductos;
                        gvProductos.DataBind();
                    }
                    else
                    {
                        List<Entidad> listaProductos = new List<Entidad>();
                        bool x = false;
                        string[] productos = new string[2];
                        foreach (System.Web.UI.WebControls.GridViewRow r in gvProductos.Rows)
                        {
                            string cadena = r.Cells[0].Text.ToString();
                            productos = cadena.Split(new Char[] { '-' });

                            if (productos[0] == ent.Codigo)
                            {
                                x = true;
                            }
                            else
                            {
                                Entidad oEnt = new Entidad();
                                oEnt.Codigo = productos[0];
                                oEnt.Descripcion = productos[0] + "-" + productos[1];
                                listaProductos.Add(oEnt);
                            }
                        }
                        if (!x)
                        {
                            listaProductos.Add(ent);
                            gvProductos.DataSource = listaProductos;
                            gvProductos.DataBind();
                        }

                    }
                }
                else
                {
                    ent = null;
                }
            }
        }

        protected void btnProyectos_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (dplProyectos.SelectedValue != string.Empty)
                {
                    Proyecto objProyecto = new Proyecto();

                    string[] ads = new string[2];
                    string programa = dplProyectos.SelectedValue.ToString();
                    string lsUbicPrograma = "";

                    ads = programa.Split(new Char[] { '|' });

                    programa = ads[0];
                    lsUbicPrograma = ads[1];

                    /*    if (pnlproyectos.Visible)
                        {
                            objProyecto = MngNegocioProyecto.ObtieneDatosProy(dplDep.SelectedValue.ToString(), programa);
                        }
                        else
                        {
                            objProyecto = MngNegocioProyecto.ObtieneDatosProy(Session["Crip_Ubicacion"].ToString(), programa);
                        }
                        */
                    objProyecto = MngNegocioProyecto.ObtieneDatosProy(lsUbicPrograma, programa, year);

                    if (objProyecto.Componente == "602")
                    {
                        clsFuncionesGral.Activa_Paneles(pnlEspecie, false);

                        //mvComision.ActiveViewIndex = 1;
                        mvComision.ActiveViewIndex = 2;
                    }
                    else
                    {
                        if (gvProductos.Rows.Count == 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un producto especifico para el programa ');", true);
                            return;
                        }

                        if (dplActividad.SelectedValue.ToString() == "00")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una actividad especifica para el programa a ejecutar');", true);
                            return;
                        }

                        //mvComision.ActiveViewIndex = 1;

                        mvComision.ActiveViewIndex = 2;
                    }

                }
                else
                {
                    clsFuncionesGral.Activa_Paneles(pnlEspecie, false);
                    dplProyectos.SelectedIndex = 0;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar un programa al cual cargar el egreso.');", true);

                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void gvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            dplProductos.SelectedIndex = 0;
            gvProductos.DataSource = null;
            gvProductos.DataBind();
        }

        public void Limpia_Grid(System.Web.UI.WebControls.GridView gv)
        {
            gv.DataSource = null;
            gv.DataBind();
        }

        public void Llena_Tipou(DropDownList pdplList)
        {
            string lsTipo;
            string lsDescripcion;
            List<Entidad> TipoUsuario = new List<Entidad>();

            for (int i = 0; i < 2; i++)
            {
                Entidad objetoEntidad = new Entidad();
                if (i == 0)
                {
                    lsTipo = "INT";
                    lsDescripcion = "Comisionados locales";
                }
                else
                {
                    lsTipo = "EXT";
                    lsDescripcion = "Comisionados Otros Centros";
                }


                objetoEntidad.Codigo = lsTipo;
                objetoEntidad.Descripcion = lsDescripcion;
                TipoUsuario.Add(objetoEntidad);
            }

            pdplList.DataSource = TipoUsuario;
            pdplList.DataTextField = "Descripcion";
            pdplList.DataValueField = "Codigo";
            pdplList.DataBind();

        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                string lsTipo;
                //  lsUsuario = Session["Crip_Usuario"].ToString();
                // lsUbicacion = Session["Crip_Ubicacion"].ToString();
                bool Bandera = false;

                lsTipo = dplUsuarios.SelectedValue;

                if (lsTipo != string.Empty)
                {
                    if (lsTipo == "INT")
                    {

                        if (pnlExternos.Visible)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlExternos, false);
                        }

                        if (gvComisionados.Rows.Count > 0)
                        {
                            Bandera = true;
                        }

                        dplPersonal.DataSource = MngNegocioUsuarios.MngBussinesUssers(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), Bandera);
                        dplPersonal.DataTextField = "Descripcion";
                        dplPersonal.DataValueField = "Codigo";
                        dplPersonal.DataBind();

                    }
                    else if (lsTipo == "EXT")
                    {
                        string lsPermisos = MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), dictionary.PERMISO_EXTERNO);

                        if ((Session["Crip_Rol"].ToString() == dictionary.INVESTIGADOR) | (Session["Crip_Rol"].ToString() == dictionary.ENLACE) | (Session["Crip_Rol"].ToString() == dictionary.SUBDIRECTOR_ADJUNTO) | (Session["Crip_Rol"].ToString() == dictionary.JEFE_DEPARTAMENTO))
                        {
                            dplPersonal.Items.Clear();

                            if (lsPermisos == "EXT")
                            {
                                clsFuncionesGral.Activa_Paneles(pnlExternos, true);

                                dplUbicacion.DataSource = MngNegocioDependencia.ObtieneCentro(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString());
                                dplUbicacion.DataTextField = "Descripcion";
                                dplUbicacion.DataValueField = "Codigo";
                                dplUbicacion.DataBind();

                            }
                            else
                            {
                                dplPersonal.Items.Clear();

                                dplUsuarios.SelectedIndex = 0;
                                dplPersonal.DataSource = MngNegocioUsuarios.MngBussinesUssers(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString());
                                dplPersonal.DataTextField = "Descripcion";
                                dplPersonal.DataValueField = "Codigo";
                                dplPersonal.DataBind();
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No cuenta con los permisos nesesarios o No es Responsable del proyecto seleccionado por lo que exclusivamente podra solicitar comisionados locales');", true);
                                return;
                            }
                        }
                        else
                        {
                            pnlExternos.Visible = true;

                            dplUbicacion.DataSource = MngNegocioDependencia.ObtieneCentro(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString());
                            dplUbicacion.DataTextField = "Descripcion";
                            dplUbicacion.DataValueField = "Codigo";
                            dplUbicacion.DataBind();
                        }


                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe escojer un tipo de Usuario');", true);

                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void btnUbicacion_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                string lsCentro1 = dplUbicacion.SelectedValue.ToString();

                dplPersonal.DataSource = MngNegocioUsuarios.MngDatosUsuariosExt(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), lsCentro1, Session["Crip_Rol"].ToString());
                dplPersonal.DataTextField = "Descripcion";
                dplPersonal.DataValueField = "Codigo";
                dplPersonal.DataBind();
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int CountPVIAT = 0;
            int CountSVIAT = 0;
            int CountPOFMAY = 0;
            if ((dplPersonal.SelectedValue.ToString() == null) | (dplPersonal.SelectedValue.ToString() == dictionary.CADENA_NULA) | (dplPersonal.SelectedValue.ToString() == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede agregar un campo en blanco , cargue los usuarios Locales o Externos y seleccione alguno para agregar');", true);
                return;
            }
            else
            {
                //ampliacion aka
                if (chkAMpliacion.Checked)
                {
                    string lsArchivo = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-") + txtCentroCosto.Text + "-" + txtNumOfiAmpliacion.Text + "-" + year + ".pdf";
                    Comision DetalleComision = new Comision();
                    DetalleComision = MngNegocioComision.Obten_Detalle(lsArchivo, Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), dictionary.NUMERO_CERO);

                    if (dplPersonal.SelectedValue.ToString() == DetalleComision.Comisionado)
                    {
                        ;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El personal que desea agregar no pertenece a la comision que desea ampliar ');", true);
                        return;
                    }
                }

                string ubica;

                bool x = true;

                if (dplUbicacion.Items.Count == 0)
                {
                    ubica = Session["Crip_Ubicacion"].ToString();
                }
                else
                {
                    ubica = dplUbicacion.SelectedValue.ToString();
                }

                //   List<Entidades.GridView> comisionesSincomprobar = MngNegocioComision.Comisiones_Recurso(dplPersonal.SelectedValue.ToString(), ubica, "'9'");

                string configuracion_comprobaciones = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(ubica, "COMPROBACIONES");

                //if (dplUbicacion.Items.Count == 0)
                //{
                //configuracion_comprobaciones = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(Session["Crip_Ubicacion"].ToString(), "COMPROBACIONES");
                //}
                //else
                //{
                //configuracion_comprobaciones = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(dplUbicacion.SelectedValue.ToString(), "COMPROBACIONES");
                //}


                //
                bool DIFERENCIA = false;
                string lsPermisosEspeciales = "";
                string lsPermisosEspecialesVIAT = MngNegocioPermisos.ObtienePermisos(dplPersonal.SelectedValue.ToString(), "VIAT");
                string ultimaFechaComision = MngNegocioComision.Obtiene_Max_Comision_Comprobar(dplPersonal.SelectedValue.ToString());
                if ((ultimaFechaComision != ""))
                {
                    DIFERENCIA = (Convert.ToDateTime(lsHoy) > Convert.ToDateTime(Convert.ToDateTime(ultimaFechaComision).AddDays(10)));
                }

                //if ((ultimaFechaComision == "") | (ultimaFechaComision == null))
                //{
                //    x = true;
                //}
                //else
                //{
                //    if ((Convert.ToDateTime(lsHoy) > Convert.ToDateTime(Convert.ToDateTime(ultimaFechaComision).AddDays(10)))) // | (comisionesSincomprobar.Count != 0))
                //    {
                //        if (((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) != dictionary.DIRECTOR_GRAL) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_JURIDICO) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADMINISTRACION) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADJUNTO))
                //        {
                //            x = false;

                //            if ((lsPermisosEspeciales == "VIAT"))
                //            {
                //                x = true;
                //            }
                //            else if ((configuracion_comprobaciones == null) | (configuracion_comprobaciones == ""))
                //            {
                //                x = false;
                //            }
                //            else
                //            {
                //                x = true;
                //            }

                //        }
                //        else
                //        {
                //            x = true;
                //        }
                //    }
                //    else
                //    {
                //        x = true;
                //    }
                //}


                string anio_calendarizado = MngNegocioAnio.Obtiene_Periodo(clsFuncionesGral.FormatFecha(txtInicio.Text), clsFuncionesGral.FormatFecha(txtFinal.Text));

                if (anio_calendarizado != "")
                {
                    if (anio_calendarizado == year)
                    {
                        year = DateTime.Today.Year.ToString();
                    }
                    else
                    {
                        year = anio_calendarizado;
                    }
                }
                else
                {
                    year = DateTime.Today.Year.ToString();
                }

                //aqui comienza modificacion

                int contNoComp = Convert.ToInt16(MngNegocioComision.Comisiones_SinComprobar(dplPersonal.SelectedValue.ToString(), year));
                int contSolRegistradas = Convert.ToInt16(MngNegocioComision.Solicitudes_Registradas(dplPersonal.SelectedValue.ToString(), lsHoy));
                if (((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) != dictionary.DIRECTOR_GRAL) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_JURIDICO) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADMINISTRACION) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADJUNTO))
                {
                    if ((contNoComp >= 3) && (lsPermisosEspecialesVIAT != "VIAT"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.Text + "\\n tiene 3 o mas comprobaciones de comsion pendiente\\n por lo que no puede aplicar para esta solicitud \\n Deberá de realizar la comprobaciones o en su caso solicitar un permiso especial al area de Administracion' );", true);
                        return;
                    }
                }


                double totalDias = clsFuncionesGral.Convert_Double(MngNegocioComision.Dias_Acumulados(dplPersonal.SelectedValue.ToString(), year));

                if (totalDias >= clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(dictionary.DIAS_ANUALES)))
                {
                    lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(dplPersonal.SelectedValue.ToString(), "OFMAY");

                    if (lsPermisosEspeciales != "OFMAY")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n Ha excedido el total de dias anuales permitidos por la norma de viaticos vigente\\n por lo que no puede aplicar para esta solicitud \\n solicitar un permiso especial al area de Administracion' );", true);
                        return;
                    }
                    else
                    {
                        int lsCantPermisos = MngNegocioPermisos.obtieneCantPermisos(dplPersonal.SelectedValue.ToString(), "OFMAY");
                        if (contSolRegistradas < lsCantPermisos)
                        {
                            if (((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) == dictionary.DIRECTOR_GRAL) || (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) == dictionary.DIRECTOR_JURIDICO) || (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) == dictionary.DIRECTOR_ADMINISTRACION) || (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) == dictionary.DIRECTOR_ADJUNTO))
                            {
                                List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                {
                                    string cadena = r.Cells[1].Text.ToString();
                                    if (cadena == dplPersonal.SelectedValue.ToString())
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                        return;
                                    }
                                    else
                                    {
                                        InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                        objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                        objGrid.RFC = r.Cells[1].Text.ToString();
                                        objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                        ListaGrid.Add(objGrid);
                                    }

                                }


                                InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                obj.Comisionado = dplPersonal.SelectedItem.Text;
                                obj.RFC = dplPersonal.SelectedValue;
                                Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                ListaGrid.Add(obj);

                                Obtener_TablaActualizada(ListaGrid);
                                ListaGrid = null;
                                oUbicacion = null;
                            }
                            else
                            {

                                if (contNoComp > 0)
                                {
                                    if (DIFERENCIA)
                                    {
                                        if ((lsPermisosEspecialesVIAT != dictionary.CADENA_NULA))
                                        {
                                            List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                            {
                                                string cadena = r.Cells[1].Text.ToString();
                                                if (cadena == dplPersonal.SelectedValue.ToString())
                                                {
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                                    return;
                                                }
                                                else
                                                {
                                                    InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                                    objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                                    objGrid.RFC = r.Cells[1].Text.ToString();
                                                    objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                                    ListaGrid.Add(objGrid);
                                                }

                                            }


                                            InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                            obj.Comisionado = dplPersonal.SelectedItem.Text;
                                            obj.RFC = dplPersonal.SelectedValue;
                                            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                            obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                            ListaGrid.Add(obj);

                                            Obtener_TablaActualizada(ListaGrid);


                                            ListaGrid = null;
                                            oUbicacion = null;
                                        }
                                        else
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente (s) de las cuales al menos una ya ha excedido los 10 días permitidos para comprobar \\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                                        }

                                    }
                                    else
                                    {
                                        List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                        foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                        {
                                            string cadena = r.Cells[1].Text.ToString();
                                            if (cadena == dplPersonal.SelectedValue.ToString())
                                            {
                                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                                return;
                                            }
                                            else
                                            {
                                                InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                                objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                                objGrid.RFC = r.Cells[1].Text.ToString();
                                                objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                                ListaGrid.Add(objGrid);
                                            }

                                        }


                                        InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                        obj.Comisionado = dplPersonal.SelectedItem.Text;
                                        obj.RFC = dplPersonal.SelectedValue;
                                        Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                        obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                        ListaGrid.Add(obj);

                                        Obtener_TablaActualizada(ListaGrid);


                                        ListaGrid = null;
                                        oUbicacion = null;

                                    }



                                }
                                else
                                {

                                    List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                    {
                                        string cadena = r.Cells[1].Text.ToString();
                                        if (cadena == dplPersonal.SelectedValue.ToString())
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                            return;
                                        }
                                        else
                                        {
                                            InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                            objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                            objGrid.RFC = r.Cells[1].Text.ToString();
                                            objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                            ListaGrid.Add(objGrid);
                                        }

                                    }


                                    InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                    obj.Comisionado = dplPersonal.SelectedItem.Text;
                                    obj.RFC = dplPersonal.SelectedValue;
                                    Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                    obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                    ListaGrid.Add(obj);

                                    Obtener_TablaActualizada(ListaGrid);


                                    ListaGrid = null;
                                    oUbicacion = null;

                                }
                                //if ((lsPermisosEspecialesVIAT != dictionary.CADENA_NULA))
                                //{
                                //    List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                //    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                //    {
                                //        string cadena = r.Cells[1].Text.ToString();
                                //        if (cadena == dplPersonal.SelectedValue.ToString())
                                //        {
                                //            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                //            return;
                                //        }
                                //        else
                                //        {
                                //            InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                //            objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                //            objGrid.RFC = r.Cells[1].Text.ToString();
                                //            objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                //            ListaGrid.Add(objGrid);
                                //        }

                                //    }


                                //    InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                //    obj.Comisionado = dplPersonal.SelectedItem.Text;
                                //    obj.RFC = dplPersonal.SelectedValue;
                                //    Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                //    obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                //    ListaGrid.Add(obj);

                                //    Obtener_TablaActualizada(ListaGrid);


                                //    ListaGrid = null;
                                //    oUbicacion = null;
                                //}
                                //else
                                //{
                                //    if (contNoComp > 0)
                                //    {
                                //        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente\\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                                //    }
                                //    else
                                //    {

                                //        List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                //        foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                //        {
                                //            string cadena = r.Cells[1].Text.ToString();
                                //            if (cadena == dplPersonal.SelectedValue.ToString())
                                //            {
                                //                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                //                return;
                                //            }
                                //            else
                                //            {
                                //                InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                //                objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                //                objGrid.RFC = r.Cells[1].Text.ToString();
                                //                objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                //                ListaGrid.Add(objGrid);
                                //            }

                                //        }


                                //        InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                //        obj.Comisionado = dplPersonal.SelectedItem.Text;
                                //        obj.RFC = dplPersonal.SelectedValue;
                                //        Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                //        obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                //        ListaGrid.Add(obj);

                                //        Obtener_TablaActualizada(ListaGrid);


                                //        ListaGrid = null;
                                //        oUbicacion = null;

                                //    }
                                //}


                            }

                            //  comisionesSincomprobar = null;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n Ha excedido el total de dias anuales permitidos por la norma de viaticos vigente\\n por lo que no puede aplicar para esta solicitud \\n solicitar un permiso especial al area de Administracion' );", true);
                            return;
                        }

                    }
                }
                else
                {
                    if (((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) == dictionary.DIRECTOR_GRAL) || (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) == dictionary.DIRECTOR_JURIDICO) || (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) == dictionary.DIRECTOR_ADMINISTRACION) || (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) == dictionary.DIRECTOR_ADJUNTO))
                    {
                        if (contNoComp > 0)
                        {
                            if (DIFERENCIA)
                            {
                                if ((lsPermisosEspecialesVIAT != dictionary.CADENA_NULA))
                                {
                                    List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                    {
                                        string cadena = r.Cells[1].Text.ToString();
                                        if (cadena == dplPersonal.SelectedValue.ToString())
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                            return;
                                        }
                                        else
                                        {
                                            InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                            objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                            objGrid.RFC = r.Cells[1].Text.ToString();
                                            objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                            ListaGrid.Add(objGrid);
                                        }

                                    }


                                    InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                    obj.Comisionado = dplPersonal.SelectedItem.Text;
                                    obj.RFC = dplPersonal.SelectedValue;
                                    Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                    obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                    ListaGrid.Add(obj);

                                    Obtener_TablaActualizada(ListaGrid);
                                    ListaGrid = null;
                                    oUbicacion = null;
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente(s) de las cuales al menos una ya ha excedido los 10 días permitidos para comprobar \\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                                }
                            }
                            else
                            {
                                List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                {
                                    string cadena = r.Cells[1].Text.ToString();
                                    if (cadena == dplPersonal.SelectedValue.ToString())
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                        return;
                                    }
                                    else
                                    {
                                        InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                        objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                        objGrid.RFC = r.Cells[1].Text.ToString();
                                        objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                        ListaGrid.Add(objGrid);
                                    }

                                }


                                InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                obj.Comisionado = dplPersonal.SelectedItem.Text;
                                obj.RFC = dplPersonal.SelectedValue;
                                Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                ListaGrid.Add(obj);

                                Obtener_TablaActualizada(ListaGrid);
                                ListaGrid = null;
                                oUbicacion = null;
                            }
                        }
                        else
                        {
                            List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                            {
                                string cadena = r.Cells[1].Text.ToString();
                                if (cadena == dplPersonal.SelectedValue.ToString())
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                    return;
                                }
                                else
                                {
                                    InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                    objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                    objGrid.RFC = r.Cells[1].Text.ToString();
                                    objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                    ListaGrid.Add(objGrid);
                                }

                            }


                            InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                            obj.Comisionado = dplPersonal.SelectedItem.Text;
                            obj.RFC = dplPersonal.SelectedValue;
                            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                            obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                            ListaGrid.Add(obj);

                            Obtener_TablaActualizada(ListaGrid);
                            ListaGrid = null;
                            oUbicacion = null;

                        }
                    }
                    else
                    {
                        if (contNoComp > 0)
                        {
                            if (DIFERENCIA)
                            {
                                if ((lsPermisosEspecialesVIAT != dictionary.CADENA_NULA))
                                {
                                    List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                    {
                                        string cadena = r.Cells[1].Text.ToString();
                                        if (cadena == dplPersonal.SelectedValue.ToString())
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                            return;
                                        }
                                        else
                                        {
                                            InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                            objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                            objGrid.RFC = r.Cells[1].Text.ToString();
                                            objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                            ListaGrid.Add(objGrid);
                                        }

                                    }


                                    InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                    obj.Comisionado = dplPersonal.SelectedItem.Text;
                                    obj.RFC = dplPersonal.SelectedValue;
                                    Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                    obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                    ListaGrid.Add(obj);

                                    Obtener_TablaActualizada(ListaGrid);


                                    ListaGrid = null;
                                    oUbicacion = null;
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente(s) de las cuales al menos una ya ha excedido los 10 días permitidos para comprobar \\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                                }

                            }
                            else
                            {
                                List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                {
                                    string cadena = r.Cells[1].Text.ToString();
                                    if (cadena == dplPersonal.SelectedValue.ToString())
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                        return;
                                    }
                                    else
                                    {
                                        InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                        objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                        objGrid.RFC = r.Cells[1].Text.ToString();
                                        objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                        ListaGrid.Add(objGrid);
                                    }

                                }


                                InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                obj.Comisionado = dplPersonal.SelectedItem.Text;
                                obj.RFC = dplPersonal.SelectedValue;
                                Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                                ListaGrid.Add(obj);

                                Obtener_TablaActualizada(ListaGrid);


                                ListaGrid = null;
                                oUbicacion = null;

                            }


                        }
                        else
                        {

                            List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                            {
                                string cadena = r.Cells[1].Text.ToString();
                                if (cadena == dplPersonal.SelectedValue.ToString())
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                                    return;
                                }
                                else
                                {
                                    InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                    objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                    objGrid.RFC = r.Cells[1].Text.ToString();
                                    objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                    ListaGrid.Add(objGrid);
                                }

                            }


                            InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                            obj.Comisionado = dplPersonal.SelectedItem.Text;
                            obj.RFC = dplPersonal.SelectedValue;
                            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                            obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                            ListaGrid.Add(obj);

                            Obtener_TablaActualizada(ListaGrid);


                            ListaGrid = null;
                            oUbicacion = null;

                        }
                        //if ((lsPermisosEspeciales != dictionary.CADENA_NULA))
                        //{
                        //    List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                        //    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                        //    {
                        //        string cadena = r.Cells[1].Text.ToString();
                        //        if (cadena == dplPersonal.SelectedValue.ToString())
                        //        {
                        //            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                        //            objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                        //            objGrid.RFC = r.Cells[1].Text.ToString();
                        //            objGrid.Adscripcion = r.Cells[2].Text.ToString();
                        //            ListaGrid.Add(objGrid);
                        //        }

                        //    }


                        //    InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                        //    obj.Comisionado = dplPersonal.SelectedItem.Text;
                        //    obj.RFC = dplPersonal.SelectedValue;
                        //    Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                        //    obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                        //    ListaGrid.Add(obj);

                        //    Obtener_TablaActualizada(ListaGrid);


                        //    ListaGrid = null;
                        //    oUbicacion = null;
                        //}
                        //else
                        //{
                        //    if (contNoComp > 0)
                        //    {
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplPersonal.SelectedItem.ToString() + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente\\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                        //    }
                        //    else
                        //    {

                        //        List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                        //        foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                        //        {
                        //            string cadena = r.Cells[1].Text.ToString();
                        //            if (cadena == dplPersonal.SelectedValue.ToString())
                        //            {
                        //                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                        //                return;
                        //            }
                        //            else
                        //            {
                        //                InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                        //                objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                        //                objGrid.RFC = r.Cells[1].Text.ToString();
                        //                objGrid.Adscripcion = r.Cells[2].Text.ToString();
                        //                ListaGrid.Add(objGrid);
                        //            }

                        //        }


                        //        InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                        //        obj.Comisionado = dplPersonal.SelectedItem.Text;
                        //        obj.RFC = dplPersonal.SelectedValue;
                        //        Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                        //        obj.Adscripcion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;

                        //        ListaGrid.Add(obj);

                        //        Obtener_TablaActualizada(ListaGrid);


                        //        ListaGrid = null;
                        //        oUbicacion = null;

                        //    }
                        //}



                    }

                    //  comisionesSincomprobar = null;
                }

            }
        }

        protected void gvComisionados_SelectedIndexChanged(object sender, EventArgs e)
        {

            string CadenaELiminar = gvComisionados.Rows[Convert.ToInt32(gvComisionados.SelectedIndex.ToString())].Cells[1].Text.ToString();

            if (gvComisionados.Rows.Count == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La solicitud no puede estar sin comisionados.');", true);
                return;
            }
            else
            {
                List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();
                foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                {
                    string cadena = r.Cells[1].Text.ToString();
                    if (CadenaELiminar != cadena)
                    {
                        InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();
                        objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                        objGrid.RFC = r.Cells[1].Text.ToString();
                        objGrid.Adscripcion = r.Cells[2].Text.ToString();
                        ListaGrid.Add(objGrid);
                    }
                }
                Obtener_TablaActualizada(ListaGrid);
                ListaGrid = null;
            }
        }

        protected void btnAntProy_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                // mvComision.ActiveViewIndex = 0;
                mvComision.ActiveViewIndex = 2;
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void btnSigUbic_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                /*
                if (dplTipoComision.SelectedValue.ToString() == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un tipo de comision para poder continuar');", true);
                    return;
                }
                else*/
                if (gvComisionados.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede tener una solicitud sin comisionados');", true);
                    return;
                }
                else
                {
                    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                    {
                        DIFERENCIA = false;

                        string Nombre = r.Cells[0].Text.ToString();
                        string usuario = r.Cells[1].Text.ToString();
                        string adscripcion = r.Cells[2].Text.ToString();
                        string[] ads = new string[2];
                        ads = adscripcion.Split(new Char[] { '-' });
                        bool x = true;

                        //  List<Entidades.GridView> comisionesSincomprobar = MngNegocioComision.Comisiones_Recurso(usuario, ads[0], "'9'");

                        //string configuracion_comprobaciones;

                        //configuracion_comprobaciones = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(ads[0], "COMPROBACIONES");

                        string lsPermisosEspeciales = "";

                        string lsPermisosEspecialesVIAT = MngNegocioPermisos.ObtienePermisos(usuario, "VIAT");
                        string ultimaFechaComision = MngNegocioComision.Obtiene_Max_Comision_Comprobar(usuario);
                        if ((ultimaFechaComision != ""))
                        {
                            DIFERENCIA = (Convert.ToDateTime(lsHoy) > Convert.ToDateTime(Convert.ToDateTime(ultimaFechaComision).AddDays(10)));
                        }

                        //if ((ultimaFechaComision == "") | (ultimaFechaComision == null))
                        //{
                        //    x = true;
                        //    /*  if ((comisionesSincomprobar.Count != 0))
                        //      {
                        //          if ((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) != dictionary.DIRECTOR_GRAL)
                        //          {
                        //              x = false;

                        //              if ((lsPermisosEspeciales == "VIAT"))
                        //              {
                        //                  x = true;
                        //              }
                        //              else if ((configuracion_comprobaciones == null) | (configuracion_comprobaciones == ""))
                        //              {
                        //                  x = false;
                        //              }
                        //              else
                        //              {
                        //                  x = true;
                        //              }

                        //          }
                        //          else
                        //          {
                        //              x = true;
                        //          }
                        //      }
                        //      else
                        //      {
                        //          x = true;
                        //      }*/
                        //}
                        //else
                        //{
                        //    if ((Convert.ToDateTime(lsHoy) > Convert.ToDateTime(Convert.ToDateTime(ultimaFechaComision).AddDays(10))))// | (comisionesSincomprobar.Count != 0))
                        //    {
                        //        if (((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) != dictionary.DIRECTOR_GRAL) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_JURIDICO) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADMINISTRACION) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADJUNTO))
                        //        {
                        //            x = false;

                        //            if ((lsPermisosEspeciales == "VIAT"))
                        //            {
                        //                x = true;
                        //            }
                        //            else if ((configuracion_comprobaciones == null) | (configuracion_comprobaciones == ""))
                        //            {
                        //                x = false;
                        //            }
                        //            else
                        //            {
                        //                x = true;
                        //            }

                        //        }
                        //        else
                        //        {
                        //            x = true;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        x = true;
                        //    }
                        //}

                        //if (!x)
                        //{
                        //    if (Convert.ToDateTime(lsHoy) > Convert.ToDateTime(Convert.ToDateTime(ultimaFechaComision).AddDays(10)))
                        //    {
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n tiene una comprobacion de comsion pendiente con fecha de terminacion de : " + clsFuncionesGral.FormatFecha(ultimaFechaComision) + "y que excede los 10 dias de tolerancia para comprobar \\n Por lo que no puede aplicar para esta solicitud \\n Deberá realizar la comprobacion o en su caso solicitar un permiso especial al area de Administracion' );", true);
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n tiene una comprobacion de comsion pendiente\\n por lo que no puede aplicar para esta solicitud \\n Deberá de realizar la comprobacion o en su caso solicitar un permiso especial al area de Administracion' );", true);
                        //        return;
                        //    }
                        //}

                        //Modificacion pedro
                        int contNoComp = Convert.ToInt16(MngNegocioComision.Comisiones_SinComprobar(usuario, year));

                        if (((MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString())) != dictionary.DIRECTOR_GRAL) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_JURIDICO) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADMINISTRACION) | (MngNegocioUsuarios.Obtiene_Rol(dplPersonal.SelectedValue.ToString()) != dictionary.DIRECTOR_ADJUNTO))
                        {
                            if ((contNoComp >= 3) && (lsPermisosEspecialesVIAT != "VIAT"))
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n tiene 3 o mas comprobaciones de comsion pendiente\\n por lo que no puede aplicar para esta solicitud \\n Deberá de realizar la comprobaciones o en su caso solicitar un permiso especial al area de Administracion' );", true);
                                return;
                            }
                        }
                        //termina modificacion pedro
                        //else
                        //{                   

                        double totalDias = clsFuncionesGral.Convert_Double(MngNegocioComision.Dias_Acumulados(usuario, year));
                        int contSolRegistradas = Convert.ToInt16(MngNegocioComision.Solicitudes_Registradas(usuario, lsHoy));
                        if (totalDias >= clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(dictionary.DIAS_ANUALES)))
                        {
                            lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(usuario, "OFMAY");

                            if (lsPermisosEspeciales != "OFMAY")
                            {
                                //  comisionesSincomprobar = null;
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n Ha excedido el total de dias anuales permitidos por la norma de viaticos vigente\\n por lo que no puede aplicar para esta solicitud \\n solicitar un permiso especial al area de Administracion' );", true);
                                return;
                            }
                            else
                            {
                                int lsCantPermisos = MngNegocioPermisos.obtieneCantPermisos(usuario, "OFMAY");
                                if (contSolRegistradas < lsCantPermisos)
                                {

                                    if (contNoComp > 0)
                                    {

                                        if (DIFERENCIA)
                                        {
                                            if ((lsPermisosEspecialesVIAT == dictionary.CADENA_NULA))
                                            {
                                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente de las cuales al menos 1 ya ha excedido los 10 días permitidos para comprobar\\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                                                return;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n Ha excedido el total de dias anuales permitidos por la norma de viaticos vigente\\n por lo que no puede aplicar para esta solicitud \\n solicitar un permiso especial al area de Administracion' );", true);
                                    return;

                                }
                            }
                        }
                        else
                        {
                            if (contNoComp > 0)
                            {
                                if (DIFERENCIA)
                                {
                                    if ((lsPermisosEspecialesVIAT == dictionary.CADENA_NULA))
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n cuenta con " + contNoComp + " comprobaciones de comsion pendiente de las cuales al menos 1 ya ha excedido los 10 días permitidos para comprobar\\n FAVOR de realizar las comprobaciones correspondiente a sus comisiones' );", true);
                                        return;
                                    }
                                }
                            }
                        }
                        //comisionesSincomprobar = null;

                        //}
                    }

                    Calendar2.Visible = false;
                    Calendar1.Visible = false;
                    //mvComision.ActiveViewIndex = 2;
                    mvComision.ActiveViewIndex = 3;
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
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

        protected void cal2_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtInicio.Text = clsFuncionesGral.FormatFecha(Convert.ToString(Calendar1.SelectedDate.ToShortDateString(), culture));
            Calendar1.Visible = false;
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            if (Calendar2.SelectedDate < Calendar1.SelectedDate)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La fecha de final no puede ser menor a la fecha inicial, Favor de validar');", true);
            }
            else
            {
                txtFinal.Text = clsFuncionesGral.FormatFecha(Convert.ToString(Calendar2.SelectedDate.ToShortDateString(), culture));
                Calendar2.Visible = false;
            }

        }

        protected void btnAntComisionado_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                mvComision.ActiveViewIndex = 0;
                //mvComision.ActiveViewIndex = 1;
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void btnSigTransporte_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (chkAMpliacion.Checked)
                {
                    if ((txtCentroCosto.Text == string.Empty) | (txtCentroCosto.Text == null) | (txtCentroCosto.Text == dictionary.CADENA_NULA))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Centro de costos para ampliacion obligatorio');", true);
                        return;
                    }

                    if ((txtNumOfiAmpliacion.Text == string.Empty) | (txtNumOfiAmpliacion.Text == null) | (txtNumOfiAmpliacion.Text == dictionary.CADENA_NULA) | (txtNumOfiAmpliacion.Text == dictionary.NUMERO_CERO))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Núm. de Oficio  para ampliacion obligatorio');", true);
                        return;
                    }
                    else if (!clsFuncionesGral.IsNumeric(txtNumOfiAmpliacion.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Núm. de Oficio  para ampliacion numerico obligatorio');", true);
                        return;
                    }
                    //juancho
                    string lsArchivo = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-") + txtCentroCosto.Text.Trim() + "-" + txtNumOfiAmpliacion.Text.Trim() + "-" + year + ".pdf";

                    Comision DetalleComision = new Comision();

                    DetalleComision = MngNegocioComision.Obten_Detalle(lsArchivo, Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), dictionary.NUMERO_CERO);

                    DateTime FechaFinAmpliacion = Convert.ToDateTime(DetalleComision.Fecha_Final);

                    if ((DetalleComision.Archivo_Ampliacion == "") | (DetalleComision.Archivo_Ampliacion == null) | (DetalleComision.Archivo_Ampliacion == dictionary.CADENA_NULA))
                    {
                        if ((FechaFinAmpliacion == Convert.ToDateTime(txtInicio.Text)) | (FechaFinAmpliacion.AddDays(1) == Convert.ToDateTime(txtInicio.Text)))
                        {
                            DetalleComision = null;
                        }
                        else
                        {
                            DetalleComision = null;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Inicio de Comision de Ampliacion No inicia en la fecha o un dia adicional al que se señala');", true);
                            return;
                        }
                    }
                    else
                    {
                        string[] ads = DetalleComision.Archivo_Ampliacion.Split(new Char[] { '|' });
                        Comision comisionAmpliacion = new Comision();
                        int x = ads.Length - 1;

                        comisionAmpliacion = MngNegocioComision.Obten_Detalle(ads[x], DetalleComision.Ubicacion_Comisionado, DetalleComision.Comisionado, dictionary.NUMERO_CERO);
                        DateTime FechaFinAmpliacion1 = Convert.ToDateTime(comisionAmpliacion.Fecha_Final);

                        if ((FechaFinAmpliacion1 == Convert.ToDateTime(txtInicio.Text)) | (FechaFinAmpliacion1.AddDays(1) == Convert.ToDateTime(txtInicio.Text)))
                        {
                            DetalleComision = null;
                            comisionAmpliacion = null;
                        }
                        else
                        {
                            DetalleComision = null;
                            comisionAmpliacion = null;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El inicio de la comision no corresponde a fechas de incio de las demas ampliaciones.');", true);
                            return;
                        }

                    }

                }

                if (dplTipoComision.SelectedValue.ToString() == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un tipo de comision para poder continuar');", true);
                    return;
                }

                if (pnlPais.Visible)
                {
                    if (txtPais.Text == dictionary.CADENA_NULA)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Indicar el pais al que va de comisión Internacional');", true);
                        return;
                    }
                    else if (clsFuncionesGral.Exp_Regular(txtPais.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Campo País');", true);
                        return;
                    }
                }
                else if (pnlEstado.Visible)
                {
                    if (dplEstado.SelectedIndex == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccionar el estado al que va de comisiónNacional');", true);
                        return;
                    }
                }

                if (txtLugar.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar a que lugar va de comisión.');", true);
                    return;
                }
                else if (clsFuncionesGral.Exp_Regular(txtLugar.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Destino');", true);
                    return;
                }
                else if (txtInicio.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar una fecha inicial de la comisión.');", true);
                    return;
                }
                else if (txtFinal.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar una fecha final de la comisión.');", true);
                    return;
                }
                else if (Calendar2.SelectedDate < Calendar1.SelectedDate)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La fecha de final no puede ser menor a la fecha inicial, Favor de validar.');", true);
                    return;
                }
                else if (txtObjetivo.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El objetivo de la comision es obligatoria y no debe rebasar las dos lineas.');", true);
                    return;
                }
                else if (clsFuncionesGral.Exp_Regular(txtObjetivo.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Objetivo de la Comisión');", true);
                    return;
                }
                else
                {
                    string lsPermisosEspeciales = "";
                    string ultimaFechaComision = "";

                    if (dplTipoComision.SelectedValue.ToString() == "2")
                    {
                        TimeSpan ts = (Calendar2.SelectedDate - Calendar1.SelectedDate);

                        if ((ts.Days + 1) > 7)
                        {
                            DateTime fechaC = Convert.ToDateTime(txtInicio.Text);
                            fechaC = fechaC.AddDays(-1);

                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                            {
                                string Nombre = r.Cells[0].Text.ToString();
                                string usuario = r.Cells[1].Text.ToString();
                                string adscripcion = r.Cells[2].Text.ToString();
                                string[] ads = new string[2];
                                ads = adscripcion.Split(new Char[] { '-' });


                                double totalDiasNac = clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Dias_Continuos("'" + clsFuncionesGral.FormatFecha(fechaC.ToString()) + "','" + txtInicio.Text + "'", usuario, "2"));
                                lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(usuario, "VIAT24");

                                if (totalDiasNac >= clsFuncionesGral.Convert_Double(dictionary.DIAS_NACIONALES))
                                {
                                    if ((lsPermisosEspeciales != "VIAT24"))
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ud. Esta solicitando mas de 20 dias naturales  en territorio internacional permitidos por la Norma Oficial de Viaticos Vigente,  para  poder  aplicar para esta solicitud \\n  solicitar un permiso especial al area de Administracion para todos los comisionados');", true);
                                        return;
                                    }
                                }

                            }
                        }
                        else if ((ts.Days + 1) <= 7)
                        {
                            DateTime fechaC = Convert.ToDateTime(txtInicio.Text);
                            fechaC = fechaC.AddDays(-1);

                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                            {
                                string Nombre = r.Cells[0].Text.ToString();
                                string usuario = r.Cells[1].Text.ToString();
                                string adscripcion = r.Cells[2].Text.ToString();
                                string[] ads = new string[2];
                                ads = adscripcion.Split(new Char[] { '-' });

                                double totalDiasNac = clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Dias_Continuos("'" + clsFuncionesGral.FormatFecha(fechaC.ToString()) + "','" + txtInicio.Text + "'", usuario, "2"));
                                lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(usuario, "VIAT24");
                                if (totalDiasNac >= clsFuncionesGral.Convert_Double(dictionary.DIAS_NACIONALES))
                                {
                                    if ((lsPermisosEspeciales != "VIAT24"))
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n Ha excedido el total de dias continuos nacionales \\n por lo que no puede aplicar para esta solicitud \\n  solicitar un permiso especial al area de Administracion');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    ///Internacional
                    if (dplTipoComision.SelectedValue.ToString() == "3")
                    {
                        TimeSpan ts = (Calendar2.SelectedDate - Calendar1.SelectedDate);

                        if ((ts.Days + 1) > 10)
                        {
                            DateTime fechaC = Convert.ToDateTime(txtInicio.Text);
                            fechaC = fechaC.AddDays(-1);

                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                            {
                                string Nombre = r.Cells[0].Text.ToString();
                                string usuario = r.Cells[1].Text.ToString();
                                string adscripcion = r.Cells[2].Text.ToString();
                                string[] ads = new string[2];
                                ads = adscripcion.Split(new Char[] { '-' });

                                double totalDiasInter = clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Dias_Continuos("'" + clsFuncionesGral.FormatFecha(fechaC.ToString()) + "','" + txtInicio.Text + "'", usuario, "3"));
                                lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(usuario, "VIAT20");

                                if (totalDiasInter >= clsFuncionesGral.Convert_Double(dictionary.DIAS_INTERNACIONALES))
                                {
                                    if ((lsPermisosEspeciales != "VIAT20"))
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ud. Esta solicitando mas de 20 dias naturales Internacionales permitidos por la Norma Oficial de Viaticos Vigente y//El comisionado : " + Nombre + "\\n Ha excedido el total de dias continuos Internacionales \\n por lo que no puede aplicar para esta solicitud \\n  solicitar un permiso especial al area de Administracion');", true);
                                        return;
                                    }
                                }

                            }
                        }
                        else if ((ts.Days + 1) <= 10)
                        {
                            DateTime fechaC = Convert.ToDateTime(txtInicio.Text);
                            fechaC = fechaC.AddDays(-1);

                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                            {
                                string Nombre = r.Cells[0].Text.ToString();
                                string usuario = r.Cells[1].Text.ToString();
                                string adscripcion = r.Cells[2].Text.ToString();
                                string[] ads = new string[2];
                                ads = adscripcion.Split(new Char[] { '-' });

                                double totalDiasInter = clsFuncionesGral.Convert_Double(MngNegocioComision.Obtiene_Dias_Continuos("'" + clsFuncionesGral.FormatFecha(fechaC.ToString()) + "','" + txtInicio.Text + "'", usuario, "3"));
                                lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(usuario, "VIAT20");
                                if (totalDiasInter >= clsFuncionesGral.Convert_Double(dictionary.DIAS_INTERNACIONALES))
                                {
                                    if ((lsPermisosEspeciales != "VIAT20"))
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + Nombre + "\\n Ha excedido el total de dias continuos nacionales \\n por lo que no puede aplicar para esta solicitud \\n  solicitar un permiso especial al area de Administracion');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), "EXT");
                    /*if ((Session["Crip_Rol"].ToString() == dictionary.INVESTIGADOR) | (Session["Crip_Rol"].ToString() == dictionary.ENLACE) | (Session["Crip_Rol"].ToString() == dictionary.JEFE_DEPARTAMENTO) | (Session["Crip_Rol"].ToString() == dictionary.SUBDIRECTOR_ADJUNTO))
                    {*/
                    if (lsPermisosEspeciales == dictionary.PERMISO_EXTERNO)
                    {
                        clsFuncionesGral.Activa_Paneles(pnl11, false);
                        //dplDep1.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                        //dplDep1.DataTextField = "Descripcion";
                        //dplDep1.DataValueField = "Codigo";
                        //dplDep1.DataBind();
                        clsFuncionesGral.Activa_Paneles(pnl11, false);
                        dplClase.DataSource = MngNegocioTransporte.ObtieneClaseTrans("1000");//oDireccionTipo.Codigo);
                        dplClase.DataTextField = "Descripcion";
                        dplClase.DataValueField = "Codigo";
                        dplClase.DataBind();
                        //mvComision.ActiveViewIndex = 3;
                        mvComision.ActiveViewIndex = 1;
                    }
                    else
                    {
                        // Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                        //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                        //{
                        clsFuncionesGral.Activa_Paneles(pnl11, false);
                        dplClase.DataSource = MngNegocioTransporte.ObtieneClaseTrans("1000");//oDireccionTipo.Codigo);
                        dplClase.DataTextField = "Descripcion";
                        dplClase.DataValueField = "Codigo";
                        dplClase.DataBind();
                        // oDireccionTipo = null;
                        //mvComision.ActiveViewIndex = 3;
                        mvComision.ActiveViewIndex = 1;
                        //}
                        //else
                        //{
                        //    clsFuncionesGral.Activa_Paneles(pnl11, false);
                        //    dplClase.DataSource = MngNegocioTransporte.ObtieneClaseTrans("1000");
                        //    dplClase.DataTextField = "Descripcion";
                        //    dplClase.DataValueField = "Codigo";
                        //    dplClase.DataBind();
                        //    oDireccionTipo = null;
                        //    //mvComision.ActiveViewIndex = 3;
                        //    mvComision.ActiveViewIndex = 1;
                        //}

                    }

                    //}
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void dplDep1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string lsDepTrans = dplDep1.SelectedValue.ToString();
            dplTipo.Items.Clear();
            dplVehiculo.Items.Clear();

            if (lsDepTrans == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Escoja una unidad administrativa valida');", true);
                return;
            }
            else
            {
                dplClase.DataSource = MngNegocioTransporte.ObtieneClaseTrans("1000");//lsDepTrans);
                dplClase.DataTextField = "Descripcion";
                dplClase.DataValueField = "Codigo";
                dplClase.DataBind();
            }
        }

        protected void dplClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSigEquipo.Enabled = false;
            string lsClaseT = dplClase.SelectedValue.ToString();

            if ((lsClaseT == dictionary.CADENA_NULA) | (lsClaseT == null) | (lsClaseT == string.Empty) | (lsClaseT == "00"))
            {
                dplTipo.Items.Clear();
                clsFuncionesGral.Activa_Paneles(pnlVehiculo, false);
                clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                clsFuncionesGral.Activa_Paneles(pnlPasajes, false);

                txtCombust.Text = dictionary.CADENA_NULA;
                txtPasajes.Text = dictionary.CADENA_NULA;
                txtPeaje.Text = dictionary.CADENA_NULA;
                txtRecorridoAcuatico.Text = dictionary.CADENA_NULA;
                TxtRecorridoAereo.Text = dictionary.CADENA_NULA;
                txtRecorridoTerrestre.Text = dictionary.CADENA_NULA;

            }
            else
            {
                //if (lsClaseT == "NUL")
                //{
                //    dplTipo.DataSource = MngNegocioTransporte.ObtieneTipoTrans(lsClaseT, MngNegocioDependencia.Obtiene_DIrector_Financieros(dictionary.PUESTO_DIRECTOR_GRAL, dictionary.DIRECTOR_GRAL));
                //    dplTipo.DataTextField = "Descripcion";
                //    dplTipo.DataValueField = "Codigo";
                //    dplTipo.DataBind();
                //}
                //else
                //{
                //if (pnl11.Visible)
                //{
                //    dplTipo.DataSource = MngNegocioTransporte.ObtieneTipoTrans(lsClaseT, "1000");//dplDep1.SelectedValue.ToString());
                //    dplTipo.DataTextField = "Descripcion";
                //    dplTipo.DataValueField = "Codigo";
                //    dplTipo.DataBind();
                //}
                //else
                //{
                //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                //{
                dplTipo.DataSource = MngNegocioTransporte.ObtieneTipoTrans(lsClaseT, "1000");// oDireccionTipo.Codigo);
                dplTipo.DataTextField = "Descripcion";
                dplTipo.DataValueField = "Codigo";
                dplTipo.DataBind();
                //    oDireccionTipo = null;
                //}
                //else
                //{
                //    dplTipo.DataSource = MngNegocioTransporte.ObtieneTipoTrans(lsClaseT,"1000");// Session["Crip_Ubicacion"].ToString());
                //    dplTipo.DataTextField = "Descripcion";
                //    dplTipo.DataValueField = "Codigo";
                //    dplTipo.DataBind();
                //    oDireccionTipo = null;
                //}

                //}
            }


            clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
            clsFuncionesGral.Activa_Paneles(pnlVehiculo, false);
            clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
            clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
            txtCombust.Text = dictionary.CADENA_NULA;
        }
        // }

        protected void dplTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TipoT = dplTipo.SelectedValue.ToString();

            txtCombust.Text = dictionary.CADENA_NULA;
            txtPasajes.Text = dictionary.CADENA_NULA;
            txtPeaje.Text = dictionary.CADENA_NULA;
            txtRecorridoAcuatico.Text = dictionary.CADENA_NULA;
            TxtRecorridoAereo.Text = dictionary.CADENA_NULA;
            txtRecorridoTerrestre.Text = dictionary.CADENA_NULA;

            if ((dplTipo.SelectedItem == null) | (dplTipo.SelectedValue == dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Transporte Obligatorio, escoja uno para avanzar.');", true);
                return;
            }
            else
            {
                string Vehiculo;

                switch (TipoT)
                {
                    case "NUL": //vehiculo oficial
                        clsFuncionesGral.Activa_Paneles(pnlVehiculo, false);
                        clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                        //  clsFuncionesGral.Activa_Paneles(pnlPartidaTrans, false);
                        clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                        clsFuncionesGral.Activa_Paneles(pnlPasajes, false);

                        //paneles de recorridos
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, false);
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                        txtCombust.Text = dictionary.CADENA_NULA;
                        txtPasajes.Text = dictionary.CADENA_NULA;
                        txtPeaje.Text = dictionary.CADENA_NULA;
                        txtRecorridoAcuatico.Text = dictionary.CADENA_NULA;
                        TxtRecorridoAereo.Text = dictionary.CADENA_NULA;
                        txtRecorridoTerrestre.Text = dictionary.CADENA_NULA;

                        break;
                    case "VO": //vehiculo oficial
                        clsFuncionesGral.Activa_Paneles(pnlVehiculo, true);
                        clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                        //  clsFuncionesGral.Activa_Paneles(pnlPartidaTrans, true);
                        clsFuncionesGral.Activa_Paneles(pnlPeaje, true);
                        clsFuncionesGral.Activa_Paneles(pnlPasajes, false);

                        //paneles de recorridos
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true);
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                        //if (pnl11.Visible)
                        //{
                        //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, "1000");//dplDep1.SelectedValue.ToString());
                        //}
                        //else
                        //{
                        //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                        //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                        //{
                        //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, oDireccionTipo.Codigo);
                        //}
                        //else
                        //{

                        dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, "1000");//Session["Crip_Ubicacion"].ToString());
                        //}
                        //oDireccionTipo = null;
                        //}

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();



                        break;

                    case "VB": //vehiculo oficial - autobus

                        clsFuncionesGral.Activa_Paneles(pnlVehiculo, true);
                        clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                        //  clsFuncionesGral.Activa_Paneles(pnlPartidaTrans, true);
                        clsFuncionesGral.Activa_Paneles(pnlPeaje, true);
                        clsFuncionesGral.Activa_Paneles(pnlPasajes, true);

                        //paneles de recorridos
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true);
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                        clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {

                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                            //}
                            //else
                            //{

                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;

                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();

                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;

                    case "VP": //vehiculo oficial - AUTO PROPIO

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = false;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                            //}
                            //else
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;
                            // dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;
                    case "AB"://autobus

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = false;
                        //  pnlPartidaTrans.Visible = false;
                        pnlPeaje.Visible = false;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = false;

                        List<Entidad> ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }

                        dplVehiculo.Items.Clear();

                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;

                    case "AP"://auto personal

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = false;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }

                        dplVehiculo.Items.Clear();

                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;

                    case "BA"://Autobus Aereo

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = false;
                        //  pnlPartidaTrans.Visible = false;
                        pnlPeaje.Visible = false;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }

                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;

                    case "AV": //AVION - VEHICULO OFICIAL

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", oDireccionTipo.Codigo);
                            //}
                            //else
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                            //}
                            //oDireccionTipo = null;

                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();

                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;

                    case "VA"://vehiculo oficial - embarcacion

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = true;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                            //}
                            //else
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;

                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "AC": //Embarcacion

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = false;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = false;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = true;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;

                        break;

                    case "AE": //avion
                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = false;
                        // pnlPartidaTrans.Visible = false;
                        pnlPeaje.Visible = false;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = false;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "VAA": //VEHICULO OFICIAL - AVION - ACUATICO

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        //   pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = true;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                            //}
                            //else
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;
                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "ABP": //AUTOBUS  - AUTO PROPIO

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = false;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "BEM": //AUTOBUS  - EMBARCACION

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = false;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = true;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "APE": //AUTO PROPIO   - EMBARCACION

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = true;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "PP": //AUTO PROPIO - AVION

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, "1000"); //Session["Crip_Ubicacion"].ToString());
                        //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;
                    case "AAA"://AUTOBUS - AVION - ACUATICO

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = false;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = true;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, "1000"); //Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "PAA"://AUTO PROPIO - AVION -ACUATICO

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        //    pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = true;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, "1000");// Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "VAP": //VEHICULO OFICIAL - AUTOBUS - AUTO PERSONAL

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = false;
                        pnlRecorridoAcuatico.Visible = false;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", oDireccionTipo.Codigo);
                            //}
                            //else
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");
                            //}
                            //oDireccionTipo = null;
                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "VAB": //VEHICULO OFICIAL - AVION - AUTOBUS 

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", oDireccionTipo.Codigo);
                            //}
                            //else
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");//Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;
                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;


                    case "OAP": //VEHICULO OFICIAL - AVION - AUTO PROPIO

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = false;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", oDireccionTipo.Codigo);
                            //}
                            //else
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");//Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;
                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "AAP": //AUTOBUS - AVION - AUTO PROPIO

                        pnlVehiculo.Visible = false;
                        pnlCombustible.Visible = true;
                        //  pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = false;

                        ListEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), TipoT, Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad gv in ListEntidad)
                        {
                            Vehiculo = gv.Codigo;
                        }
                        dplVehiculo.Items.Clear();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;

                    case "AVE": // AUTOBUS - AVION - VEHICULO OFICIAL - EMBARCACION

                        pnlVehiculo.Visible = true;
                        pnlCombustible.Visible = true;
                        // pnlPartidaTrans.Visible = true;
                        pnlPeaje.Visible = true;
                        pnlPasajes.Visible = true;

                        //paneles de recorridos
                        pnlRecorridoTerrestre.Visible = true;
                        pnlRecorridoAereo.Visible = true;
                        pnlRecorridoAcuatico.Visible = true;

                        if (pnl11.Visible)
                        {
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", dplDep1.SelectedValue.ToString());
                        }
                        else
                        {
                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            //    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", oDireccionTipo.Codigo);
                            //}
                            //else
                            //{
                            dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", "1000");//Session["Crip_Ubicacion"].ToString());
                            //}
                            //oDireccionTipo = null;
                            //dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte("TER", "VO", Session["Crip_Ubicacion"].ToString());
                        }

                        dplVehiculo.DataTextField = "Descripcion";
                        dplVehiculo.DataValueField = "Codigo";
                        dplVehiculo.DataBind();
                        txtCombust.Text = dictionary.CADENA_NULA;
                        break;




                }

            }

            if (btnSigEquipo.Enabled == false)
            {
                btnSigEquipo.Enabled = true;
            }
        }

        protected void btnAntUbicacion_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                //mvComision.ActiveViewIndex = 2;
                mvComision.ActiveViewIndex = 1;
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void btnSigEquipo_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (pnlVehiculo.Visible)
                {
                    string Vehiculo = dplVehiculo.SelectedValue.ToString();

                    if ((Vehiculo == null) | (Vehiculo == dictionary.NUMERO_CERO) | (Vehiculo == dictionary.CADENA_NULA))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Vehiculo Obligatorio');", true);
                        return;
                    }
                }

                if (pnlCombustible.Visible)
                {
                    if ((txtCombust.Text == null) | (txtCombust.Text == dictionary.CADENA_NULA))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Combustble obligatorio');", true);
                        return;
                    }
                    else if (!clsFuncionesGral.IsNumeric(txtCombust.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El monto del Combustible debe ser numérico.');", true);
                        return;
                    }

                }
                else
                {
                    txtCombust.Text = dictionary.NUMERO_CERO;

                }

                if (pnlPasajes.Visible)
                {
                    if ((txtPasajes.Text == null) | (txtPasajes.Text == dictionary.CADENA_NULA))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar una cantidad para pasajes');", true);
                        return;
                    }
                    else if (!clsFuncionesGral.IsNumeric(txtPasajes.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El monto para pasajes debe ser de tipo númerico');", true);
                        return;
                    }

                }

                if (pnlPeaje.Visible)
                {
                    if ((txtPeaje.Text == null) | (txtPeaje.Text == dictionary.CADENA_NULA))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un monto estimado  para los paeajes');", true);
                        return;
                    }
                    else if (!clsFuncionesGral.IsNumeric(txtPeaje.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El monto del peaje debe ser numerico verifique.');", true);
                        return;
                    }
                }

                if (txtRecorridoAcuatico.Text != string.Empty)
                {
                    if (clsFuncionesGral.Exp_Regular(txtRecorridoAcuatico.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Recorrido Acuatico');", true);
                        return;
                    }
                }
                if (TxtRecorridoAereo.Text != string.Empty)
                {
                    if (clsFuncionesGral.Exp_Regular(TxtRecorridoAereo.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Recorrido Aereo');", true);
                        return;
                    }
                }
                if (txtRecorridoTerrestre.Text != string.Empty)
                {
                    if (clsFuncionesGral.Exp_Regular(txtRecorridoTerrestre.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en el Recorrido Terrestre');", true);
                        return;
                    }
                }
                if (txtObserVehiculo.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Observaciones de vehiculo obligatoria');", true);
                    return;
                }
                else if (clsFuncionesGral.Exp_Regular(txtObserVehiculo.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en las observaciones de Transporte');", true);
                    return;
                }

                mvComision.ActiveViewIndex = 4;
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void btnAntTransp_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                mvComision.ActiveViewIndex = 3;
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            //bool ActSession = clsFuncionesGral.IsSessionTimedOut();
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (txtEquipo.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote el equipo que utilizara en la comisión.');", true);
                    return;
                }
                else if (clsFuncionesGral.Exp_Regular(txtEquipo.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en Equipo');", true);
                    return;

                }
                else if (txtObservCom.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote las observaciones, sugerencias o recomendaciones que tiene para esta solicitud.');", true);
                    return;
                }
                else if (clsFuncionesGral.Exp_Regular(txtObservCom.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta insertando un caracter o cadena invalida en Observaciones de Comisión');", true);
                    return;

                }
                else
                {
                    // PROCESO DE INSERTS A LA TABLA DE COMISIONES
                    int SecEff = 1;
                    int liOficio = Convert.ToInt32(MngNegocioComision.ObtieneMaxComision(Session["Crip_Ubicacion"].ToString(), year));
                    string lsCentro;
                    string lsUbicacionProyecto;
                    string vehiculo = "";
                    string lsDepTrans = "";
                    string lsEspecies = "", lsProductos = "", lsActividad = "", TipoMail;
                    bool MailBandera, envio_mail;
                    string Ubicacion_Auoriza = "";
                    string lsPais = dictionary.CADENA_NULA;
                    string lsestado = dictionary.CADENA_NULA;

                    string fechaActual = clsFuncionesGral.FormatFecha(Convert.ToString(DateTime.Today));

                    if (dplTipoComision.SelectedValue.ToString() == "3")
                    {
                        lsPais = clsFuncionesGral.ConvertMayus(txtPais.Text);
                        lsestado = dictionary.CADENA_NULA;
                    }
                    else if (dplTipoComision.SelectedValue.ToString() == "2")
                    {
                        lsPais = dictionary.MIXTO;
                        lsestado = dplEstado.SelectedValue.ToString();
                    }

                    /*   if (dplDep.Items.Count == 0)
                        {
                            lsCentro = Session["Crip_Ubicacion"].ToString();
                        }
                        else
                        {
                            lsCentro = dplDep.SelectedValue.ToString();
                        }
                        */
                    string[] ads1 = new string[2];
                    ads1 = dplProyectos.SelectedValue.ToString().Split(new Char[] { '|' });

                    string lsProyecto = ads1[0];
                    lsCentro = ads1[1];

                    Proyecto objProyecto = new Proyecto();
                    objProyecto = MngNegocioProyecto.ObtieneDatosProy(lsCentro, lsProyecto, year);

                    if (objProyecto.Componente == "602")
                    {
                        lsEspecies = "00";
                        lsProductos = "00";
                        lsActividad = "00";
                    }
                    else
                    {
                        lsEspecies = dplEspecies.SelectedValue.ToString();

                        string[] productos = new string[2];

                        foreach (System.Web.UI.WebControls.GridViewRow r in gvProductos.Rows)
                        {
                            string cadena = r.Cells[0].Text.ToString();
                            productos = cadena.Split(new Char[] { '-' });

                            lsProductos += productos[0] + "|";
                        }
                        lsProductos = lsProductos.Substring(0, lsProductos.Length - 1);
                        lsActividad = dplActividad.SelectedValue.ToString();
                    }
                    //INICIA PROCESO DE INSERCION 
                    lsUbicacionProyecto = objProyecto.Dependencia;

                    if (pnl11.Visible)
                    {
                        vehiculo = dplVehiculo.SelectedValue.ToString();
                        if (pnl11.Visible)
                        {
                            lsDepTrans = dplDep1.SelectedValue.ToString();
                        }
                        else
                        {
                            lsDepTrans = Session["Crip_Ubicacion"].ToString();
                        }
                    }
                    else
                    {
                        if (dplTipo.SelectedValue.ToString() == "NUL")
                        {
                            vehiculo = "T000";
                            lsDepTrans = "1000";
                        }
                        else
                        {

                            //Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

                            //if (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
                            //{
                            //    lsDepTrans = oDireccionTipo.Codigo;
                            //}
                            //else
                            //{
                            lsDepTrans = "1000";//Session["Crip_Ubicacion"].ToString();
                            //}


                            if (dplVehiculo.SelectedValue.ToString() == dictionary.CADENA_NULA)
                            {

                                List<Entidades.Entidad> listEntidad = MngNegocioTransporte.Descripcion_Transporte(dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), lsDepTrans);
                                foreach (Entidad gv in listEntidad)
                                {
                                    vehiculo = gv.Codigo;
                                }
                            }
                            else
                            {
                                vehiculo = dplVehiculo.SelectedValue.ToString();
                            }
                            //  lsDepTrans = Session["Crip_Ubicacion"].ToString();
                        }
                    }


                    List<Entidades.GridView> listaGrid = new List<Entidades.GridView>();

                    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                    {
                        Entidades.GridView gv = new Entidades.GridView();
                        gv.Comisionado = r.Cells[0].Text.ToString();
                        gv.RFC = r.Cells[1].Text.ToString();
                        gv.Adscripcion = r.Cells[2].Text.ToString();
                        listaGrid.Add(gv);
                    }

                    foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                    {
                        //string Nombre = r.Cells[0].Text.ToString();
                        string usuario = r.Cells[1].Text.ToString();
                        string adscripcion = r.Cells[2].Text.ToString();
                        string[] ads = new string[2];
                        ads = adscripcion.Split(new Char[] { '-' });   //proceso que obtiene adcripcion y clave

                        //Proseso que define VoBo y Autorizacion para la comision
                        Usuario objUsuario = MngNegocioUsuarios.Obten_Datos(usuario, true);
                        string[] vobo_aut = clsFuncionesGral.Obtiene_Jerarquico(objUsuario);
                        string vobo, autoriza, lsTipoComision;

                        if (lsUbicacionProyecto != ads[0])//lsUbicacion
                        {
                            //agregar roles 
                            if (objProyecto.Tipo_Dep == dictionary.CENTROS_INVESTIGACION)
                            {
                                vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.ADMINISTRADOR, objProyecto.Dependencia);
                            }
                            else if ((objProyecto.Tipo_Dep == dictionary.DIRECCIONES_ADJUNTAS) | (objProyecto.Tipo_Dep == dictionary.COORDINACIONES_INVESTIGACION) | (objProyecto.Tipo_Dep == dictionary.DIRECCION_ADMON) | (objProyecto.Tipo_Dep == dictionary.DIRECCION_JEFE) | (objProyecto.Tipo_Dep == dictionary.SUBDIRECCIONES_GENERALES) | (objProyecto.Tipo_Dep == dictionary.DEPARTAMENTOS))
                            {
                                // [0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION,MngNegocioUsuarios.Obtiene_Direccion ( objProyecto.Dependencia));
                                //vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(dictionary.PUESTO_FINANCIERO, dictionary.SUBDIRECTOR_ADJUNTO), dictionary.PUESTO_FINANCIERO);
                                vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.SUBDIRECTOR_ADJUNTO, dictionary.SRF);
                                if (((vobo_aut[0] == null) | (vobo_aut[0] == dictionary.CADENA_NULA)))
                                {
                                    vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.DIRECTOR_ADMINISTRACION, dictionary.DGAA);
                                    

                                }
                                
                            }
                            //else if (objProyecto.Tipo_Dep == dictionary.COMUNICACION)
                            //{

                            //}

                        }

                        if (dplTipoComision.SelectedValue.ToString() == "3")
                        {
                            //vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(dictionary.PUESTO_FINANCIERO, dictionary.SUBDIRECTOR_ADJUNTO), dictionary.PUESTO_FINANCIERO);
                            vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.SUBDIRECTOR_ADJUNTO, dictionary.SRF);
                            if (((vobo_aut[0] == null) | (vobo_aut[0] == dictionary.CADENA_NULA)))
                            {
                                vobo_aut[0] = MngNegocioUsuarios.Obten_Usuario(dictionary.DIRECTOR_ADMINISTRACION, dictionary.DGAA);


                            }
                            vobo_aut[1] = MngNegocioUsuarios.Obten_Usuario(dictionary.DIRECTOR_GRAL);
                        }

                        vobo = vobo_aut[0];
                        autoriza = vobo_aut[1];

                        if (((vobo == null) | (vobo == dictionary.CADENA_NULA)) & ((autoriza != null) | (autoriza != dictionary.CADENA_NULA)))
                        {
                            if ((objUsuario.Ubicacion == dictionary.DG) | (objUsuario.Ubicacion == dictionary.SPDG) | (objUsuario.Ubicacion == dictionary.SIDG) | (objUsuario.Ubicacion == dictionary.DGAIA) | (objUsuario.Ubicacion == dictionary.SUBACUA) | (objUsuario.Ubicacion == dictionary.DGAIPA) | (objUsuario.Ubicacion == dictionary.DGAIPP) | (objUsuario.Ubicacion == dictionary.DGAA) | (objUsuario.Ubicacion == dictionary.SINF) | (objUsuario.Ubicacion == dictionary.SRF) | (objUsuario.Ubicacion == dictionary.SRM) | (objUsuario.Ubicacion == dictionary.SRH) | (objUsuario.Ubicacion == dictionary.DGAJ) | (objUsuario.Ubicacion == dictionary.SUBDJ))
                            {

                                vobo = MngNegocioUsuarios.Obten_Usuario(dictionary.DIRECTOR_ADMINISTRACION, dictionary.DGAA);
                            }
                            else 
                            {
                                vobo = autoriza;
                            }
                            

                        }
                        else if (((autoriza == null) | (autoriza == dictionary.CADENA_NULA)) && ((vobo != dictionary.CADENA_NULA) | (vobo != null)))
                        {
                            autoriza = vobo;
                        }

                        int liDias;

                        if (txtInicio.Text == txtFinal.Text)
                        {
                            liDias = 1;
                        }
                        else
                        {
                            TimeSpan ts = (Convert.ToDateTime(txtFinal.Text) - Convert.ToDateTime(txtInicio.Text));
                            liDias = ts.Days + 1;
                        }

                        bool Cruze_Inicio = MngNegocioComision.Comision_Extraordinaria(objUsuario.Usser, txtInicio.Text, txtFinal.Text, "1");
                        bool Cruze_Fin = MngNegocioComision.Comision_Extraordinaria(objUsuario.Usser, txtInicio.Text, txtFinal.Text, "2");

                        if ((Cruze_Inicio) | (Cruze_Fin))
                        {
                            lsTipoComision = "02";
                            //  clsMail.Mail_Notificacion_Comision_Extraordinaria(liOficio.ToString (), lsFechaI, lsFechaF, objUsuario, objProyecto, autoriza);
                        }
                        else lsTipoComision = "01";

                        string rol_comisionado = MngNegocioUsuarios.Obtiene_Rol(usuario);

                        if (dplTipoComision.SelectedValue.ToString() == "3")
                        {
                            Entidad oEntidad = MngNegocioDependencia.Obtiene_Dependencia_porTipo(dictionary.DIRECCION_JEFE, year);
                            Ubicacion_Auoriza = oEntidad.Codigo;
                            oEntidad = null;
                        }
                        else
                        {
                            Usuario objAdministrador = new Usuario();

                            objAdministrador = MngNegocioUsuarios.Datos_Administrador(MngNegocioUsuarios.Obtiene_Rol(autoriza), autoriza);
                            Ubicacion_Auoriza = objAdministrador.Ubicacion;
                            objAdministrador = null;
                        }

                        /*   Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(ads[0]);
                         if (dplTipoComision.SelectedValue.ToString() == "3")
                          {
                              Entidad oEntidad = MngNegocioDependencia.Obtiene_Dependencia_porTipo(dictionary.DIRECCION_JEFE, year);
                              Ubicacion_Auoriza = oEntidad.Codigo;
                              oEntidad = null;
                          }
                          else if (rol_comisionado == dictionary.DIRECTOR_GRAL)
                          {
                              Ubicacion_Auoriza = ads[0];
                          }
                          else if (((rol_comisionado == dictionary.JEFE_CENTRO) | (rol_comisionado == dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Descripcion == dictionary.CENTROS_INVESTIGACION))
                          {
                              Ubicacion_Auoriza = oDireccionTipo.Codigo;
                          }
                          else if (((rol_comisionado == dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == dictionary.DIRECTOR_JURIDICO) ) && ((oDireccionTipo.Descripcion == dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == dictionary.DIRECCION_JURIDICA)))
                          {
                              Ubicacion_Auoriza = oDireccionTipo.Codigo;
                          }
                          else if ((rol_comisionado == dictionary.SUBDIRECTOR_ADJUNTO) & (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES))
                          {
                              if (ads[0] == "43")
                              {
                                  Ubicacion_Auoriza = "43";
                              }
                              else if (ads[0] == "44")
                                  {
                                      Ubicacion_Auoriza = "44";
                            
                              }
                              else if (ads[0] == "45")
                              {
                                  Ubicacion_Auoriza = "45";

                              }
                              else 
                              {
                                  Ubicacion_Auoriza = oDireccionTipo.Codigo;
                              }
                          }
                          else if (((rol_comisionado == dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == dictionary.ENLACE) | (rol_comisionado == dictionary.INVESTIGADOR)) && ((oDireccionTipo.Descripcion == dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == dictionary.DIRECCION_JURIDICA) | (oDireccionTipo.Descripcion == dictionary.DIRECCION_JEFE) | (oDireccionTipo.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)))
                          {
                              Ubicacion_Auoriza = ads[0];

                          }
                          else if (((rol_comisionado == dictionary.INVESTIGADOR) | (rol_comisionado == dictionary.ADMINISTRADOR) | (rol_comisionado == dictionary.ENLACE)) && (oDireccionTipo.Descripcion == dictionary.CENTROS_INVESTIGACION))
                          {
                              Ubicacion_Auoriza = ads[0];
                          }
                          */
                        //Proseso de Insercion ala Base de Datos.
                        bool pbResult = false;
                        string AutExt = "";
                        if ((Ubicacion_Auoriza == dictionary.DG) || (Ubicacion_Auoriza == dictionary.DGAIPP) || (Ubicacion_Auoriza == "4003"))
                        {
                            if ((Ubicacion_Auoriza == dictionary.DGAIPP))
                            {
                                if ((objProyecto.Dependencia == dictionary.DGAIA) || (objProyecto.Dependencia == dictionary.DGAA))
                                {
                                    if ((objProyecto.Dependencia == dictionary.DGAIA))
                                    {
                                        AutExt = MngNegocioUsuarios.Obten_Usuario(dictionary.DIRECTOR_ADJUNTO, objProyecto.Dependencia);
                                    }
                                    else
                                    {
                                        AutExt = MngNegocioUsuarios.Obten_Usuario(dictionary.DIRECTOR_ADMINISTRACION, objProyecto.Dependencia);
                                    }

                                    pbResult = MngNegocioComision.Inserta_Comision("CRIPSC01", lsTipoComision, Convert.ToString(liOficio), "0", fechaActual, dictionary.FECHA_NULA, dictionary.FECHA_NULA, fechaActual, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Area"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, clsFuncionesGral.ConvertMayus(txtLugar.Text), dictionary.NUMERO_CERO, dictionary.CADENA_NULA, dictionary.CADENA_NULA, dictionary.CADENA_NULA, Convert.ToString("37501"), txtInicio.Text, txtFinal.Text, Convert.ToString(liDias), dictionary.NUMERO_CERO, clsFuncionesGral.ConvertMayus(txtObjetivo.Text), dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), vehiculo, dictionary.CADENA_NULA, clsFuncionesGral.ConvertMayus(txtObserVehiculo.Text), lsDepTrans, txtRecorridoTerrestre.Text, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, TxtRecorridoAereo.Text, txtCombust.Text, dictionary.NUMERO_CERO, "26102", txtPeaje.Text, "39202", txtPasajes.Text, dictionary.NUMERO_CERO, txtRecorridoAcuatico.Text, Convert.ToString(SecEff), clsFuncionesGral.ConvertMayus(txtEquipo.Text), clsFuncionesGral.ConvertMayus(txtObservCom.Text), clsFuncionesGral.ConvertMayus(objProyecto.Responsable), vobo, AutExt, objUsuario.Usser, ads[0], "8", dictionary.CADENA_NULA, dictionary.CADENA_NULA, lsEspecies, lsProductos, lsActividad, dplTipoComision.SelectedValue.ToString(), objProyecto.Dependencia, year, lsPais, lsestado);
                                }
                                else
                                {
                                    pbResult = MngNegocioComision.Inserta_Comision("CRIPSC01", lsTipoComision, Convert.ToString(liOficio), "0", fechaActual, dictionary.FECHA_NULA, dictionary.FECHA_NULA, fechaActual, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Area"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, clsFuncionesGral.ConvertMayus(txtLugar.Text), dictionary.NUMERO_CERO, dictionary.CADENA_NULA, dictionary.CADENA_NULA, dictionary.CADENA_NULA, Convert.ToString("37501"), txtInicio.Text, txtFinal.Text, Convert.ToString(liDias), dictionary.NUMERO_CERO, clsFuncionesGral.ConvertMayus(txtObjetivo.Text), dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), vehiculo, dictionary.CADENA_NULA, clsFuncionesGral.ConvertMayus(txtObserVehiculo.Text), lsDepTrans, txtRecorridoTerrestre.Text, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, TxtRecorridoAereo.Text, txtCombust.Text, dictionary.NUMERO_CERO, "26102", txtPeaje.Text, "39202", txtPasajes.Text, dictionary.NUMERO_CERO, txtRecorridoAcuatico.Text, Convert.ToString(SecEff), clsFuncionesGral.ConvertMayus(txtEquipo.Text), clsFuncionesGral.ConvertMayus(txtObservCom.Text), clsFuncionesGral.ConvertMayus(objProyecto.Responsable), vobo, autoriza, objUsuario.Usser, ads[0], "8", dictionary.CADENA_NULA, dictionary.CADENA_NULA, lsEspecies, lsProductos, lsActividad, dplTipoComision.SelectedValue.ToString(), Ubicacion_Auoriza, year, lsPais, lsestado);
                                }

                            }
                            else
                            {
                                pbResult = MngNegocioComision.Inserta_Comision("CRIPSC01", lsTipoComision, Convert.ToString(liOficio), "0", fechaActual, dictionary.FECHA_NULA, dictionary.FECHA_NULA, fechaActual, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Area"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, clsFuncionesGral.ConvertMayus(txtLugar.Text), dictionary.NUMERO_CERO, dictionary.CADENA_NULA, dictionary.CADENA_NULA, dictionary.CADENA_NULA, Convert.ToString("37501"), txtInicio.Text, txtFinal.Text, Convert.ToString(liDias), dictionary.NUMERO_CERO, clsFuncionesGral.ConvertMayus(txtObjetivo.Text), dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), vehiculo, dictionary.CADENA_NULA, clsFuncionesGral.ConvertMayus(txtObserVehiculo.Text), lsDepTrans, txtRecorridoTerrestre.Text, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, TxtRecorridoAereo.Text, txtCombust.Text, dictionary.NUMERO_CERO, "26102", txtPeaje.Text, "39202", txtPasajes.Text, dictionary.NUMERO_CERO, txtRecorridoAcuatico.Text, Convert.ToString(SecEff), clsFuncionesGral.ConvertMayus(txtEquipo.Text), clsFuncionesGral.ConvertMayus(txtObservCom.Text), clsFuncionesGral.ConvertMayus(objProyecto.Responsable), vobo, autoriza, objUsuario.Usser, ads[0], "8", dictionary.CADENA_NULA, dictionary.CADENA_NULA, lsEspecies, lsProductos, lsActividad, dplTipoComision.SelectedValue.ToString(), Ubicacion_Auoriza, year, lsPais, lsestado);
                            }

                        }
                        else
                        {
                            pbResult = MngNegocioComision.Inserta_Comision("CRIPSC01", lsTipoComision, Convert.ToString(liOficio), "0", fechaActual, dictionary.FECHA_NULA, dictionary.FECHA_NULA, dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Session["Crip_Area"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, clsFuncionesGral.ConvertMayus(txtLugar.Text), dictionary.NUMERO_CERO, dictionary.CADENA_NULA, dictionary.CADENA_NULA, dictionary.CADENA_NULA, Convert.ToString("37501"), txtInicio.Text, txtFinal.Text, Convert.ToString(liDias), dictionary.NUMERO_CERO, clsFuncionesGral.ConvertMayus(txtObjetivo.Text), dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), vehiculo, dictionary.CADENA_NULA, clsFuncionesGral.ConvertMayus(txtObserVehiculo.Text), lsDepTrans, txtRecorridoTerrestre.Text, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, dictionary.NUMERO_CERO, dictionary.FECHA_NULA, dictionary.HORA_NULA, TxtRecorridoAereo.Text, txtCombust.Text, dictionary.NUMERO_CERO, "26102", txtPeaje.Text, "39202", txtPasajes.Text, dictionary.NUMERO_CERO, txtRecorridoAcuatico.Text, Convert.ToString(SecEff), clsFuncionesGral.ConvertMayus(txtEquipo.Text), clsFuncionesGral.ConvertMayus(txtObservCom.Text), clsFuncionesGral.ConvertMayus(objProyecto.Responsable), vobo, autoriza, objUsuario.Usser, ads[0], "1", dictionary.CADENA_NULA, dictionary.CADENA_NULA, lsEspecies, lsProductos, lsActividad, dplTipoComision.SelectedValue.ToString(), Ubicacion_Auoriza, year, lsPais, lsestado);
                        }

                        //Asigna valores nulos a vobo y autorizacion
                        vobo = dictionary.CADENA_NULA;
                        autoriza = dictionary.CADENA_NULA;
                        Ubicacion_Auoriza = dictionary.CADENA_NULA;

                        //Aunmenta la Secuencia para la comision.
                        SecEff += 1;

                        if (!pbResult)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al insertar en la Bd Favor de validar sus datos.');", true);
                            return;
                        }
                        else
                        {
                            TipoMail = "SOLICITA";
                            MailBandera = false;
                            //        envio_mail = clsMail.Mail_Comision(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), ads[0], objUsuario.Usser, Session["Crip_Usuario"].ToString(), Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), lsDepTrans, vehiculo, txtObserVehiculo.Text, Convert.ToString(liOficio), txtInicio.Text, txtFinal.Text, txtLugar.Text, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);
                        }

                    }

                    /*
                                    TipoMail = "RESP_PROYECT";
                                    MailBandera = false;

                                    envio_mail = clsMail.Mail_Comision(lsSecretaria, lsOrganismo, lsUbicacion, objProyecto.Dependencia, "", lsUsuario, lsNombre + " " + lsApPat + " " + lsApMat, ListaGrid, lsClaseT, TipoT, lsDepTrans, vehiculo, lsDescVehiculo, Convert.ToString(liOficio), lsFechaI, lsFechaF, lsLugar, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, lsRol, lsCargo, MailBandera);
               

                    //Proseso de envio de correos a jerarquico de las solicitud en rol VOBO
                    List<Entidad> ListaAutoriza = MngNegocioMail.Obtiene_Mail("VoBo", Convert.ToString(liOficio), Session["Crip_Ubicacion"].ToString());

                    foreach (Entidad l in ListaAutoriza)
                    {
                        if (l.Codigo != "")
                        {
                            TipoMail = "VOBO";
                            MailBandera = false;
                            Usuario usu = MngNegocioUsuarios.Obten_Datos(l.Codigo, true);
                            //   envio_mail = clsMail.Mail_Comision(usu.Secretaria                        ,usu.Organismo                       , usu.Ubicacion                       , objProyecto.Dependencia, usu.Email, usu.Nombre, lsNombre + " " + lsApPat + " " + lsApMat                                                                           , ListaGrid, lsClaseT                         , TipoT                           , lsDepTrans, vehiculo, lsDescVehiculo       , Convert.ToString(liOficio), lsFechaI      , lsFechaF     , lsLugar      , objProyecto.Responsable, objProyecto.Descripcion, TipoMail, lsRol                         , lsCargo                         , MailBandera);
                            envio_mail = clsMail.Mail_Comision(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), objProyecto.Dependencia, usu.Email, usu.Nombre, Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), lsDepTrans, vehiculo, txtObserVehiculo.Text, Convert.ToString(liOficio), txtInicio.Text, txtFinal.Text, txtLugar.Text, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);
                        }
                    }

                    ListaAutoriza.Clear(); 
                     */
                    Comision detalleComision = new Comision();

                    detalleComision = MngNegocioComision.Detalle_Comision(Convert.ToString(liOficio), Session["Crip_Ubicacion"].ToString(), "", "8");


                    if ((detalleComision.Ubicacion_Autoriza == dictionary.DGAIPP) || (detalleComision.Ubicacion_Autoriza == dictionary.DG) || (detalleComision.Ubicacion_Autoriza == "4003"))
                    {


                        List<Entidad> ListaAutoriza = MngNegocioMail.Obtiene_Mail("VoBo", Convert.ToString(detalleComision.Folio), Session["Crip_Ubicacion"].ToString());
                        string usuJefe = Convert.ToString(detalleComision.Autoriza);
                        // List<Entidad> ListaAutoriza = MngNegocioMail.Obtiene_Mail("V", Convert.ToString(liOficio), Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad l in ListaAutoriza)
                        {
                            if (l.Codigo != "")
                            {
                                TipoMail = "VOBO";
                                MailBandera = false;
                                Usuario usu = MngNegocioUsuarios.Obten_Datos(l.Codigo, true);
                                // lulu

                                envio_mail = clsMail.Mail_ComisionEsp(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), objProyecto.Dependencia, usu.Email, usu.Nombre, Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Ubicacion_Transporte, detalleComision.Vehiculo_Solicitado, detalleComision.Descrip_vehiculo, Convert.ToString(detalleComision.Folio), detalleComision.Fecha_Inicio, detalleComision.Fecha_Final, detalleComision.Lugar, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);

                                //usu = null;
                                //usu = MngNegocioUsuarios.Datos_Administrador(dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(dictionary.PUESTO_FINANCIERO, dictionary.SUBDIRECTOR_ADJUNTO), true);

                                //if (l.Codigo == usu.Usser)
                                //{
                                //    usu = null;
                                //    usu = MngNegocioUsuarios.Obten_Datos(dictionary.USUARIO_MINISTRADOR, true);
                                //    envio_mail = clsMail.Mail_Comision(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), objProyecto.Dependencia, usu.Email, usu.Nombre, Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Ubicacion_Transporte, detalleComision.Vehiculo_Solicitado, detalleComision.Descrip_vehiculo, Convert.ToString(detalleComision.Folio), detalleComision.Fecha_Inicio, detalleComision.Fecha_Final, detalleComision.Lugar, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);
                                //}
                            }
                        }
                    }
                    else
                    {
                        List<Entidad> ListaAutoriza = MngNegocioMail.Obtiene_Mail("AUTORIZA", Convert.ToString(liOficio), Session["Crip_Ubicacion"].ToString());

                        foreach (Entidad l in ListaAutoriza)
                        {
                            if (l.Codigo != "")
                            {
                                TipoMail = "AUTORIZA";
                                MailBandera = false;
                                Usuario usu = MngNegocioUsuarios.Obten_Datos(l.Codigo, true);
                                //        envio_mail = clsMail.Mail_Comision(usu.Secretaria                       , usu.Organismo                       , usu.Ubicacion                       , objProyecto.Dependencia, usu.Email, usu.Nombre, lsNombre + " " + lsApPat + " " + lsApMat                                                                           , ListaGrid, lsClaseT                         , TipoT                           , lsDepTrans, vehiculo, lsDescVehiculo       , Convert.ToString(liOficio), lsFechaI      , lsFechaF     , lsLugar      , objProyecto.Responsable, objProyecto.Descripcion, TipoMail, lsRol                         , lsCargo                         , MailBandera);
                                envio_mail = clsMail.Mail_Comision(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), objProyecto.Dependencia, usu.Email, usu.Nombre, Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, dplClase.SelectedValue.ToString(), dplTipo.SelectedValue.ToString(), lsDepTrans, vehiculo, txtObserVehiculo.Text, Convert.ToString(liOficio), txtInicio.Text, txtFinal.Text, txtLugar.Text, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);

                            }
                        }
                    }

                    string parametro = dictionary.CADENA_NULA;
                    parametro = Convert.ToString(liOficio);
                    Session["Parametro"] = parametro;
                    Response.Redirect("../Confirmacion.aspx");
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }

        }

        protected void lnkHome_Click1(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                Response.Redirect("../Home/Home.aspx", true);
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }


        //evento de check de ampliacion
        protected void chkAMpliacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAMpliacion.Checked)
            {
                txtCentroCosto.Text = dictionary.CADENA_NULA;
                txtNumOfiAmpliacion.Text = dictionary.CADENA_NULA;
                clsFuncionesGral.Activa_Paneles(pnlAmpliacion, true);
            }
            else
            {
                txtCentroCosto.Text = dictionary.CADENA_NULA;
                txtNumOfiAmpliacion.Text = dictionary.CADENA_NULA;
                clsFuncionesGral.Activa_Paneles(pnlAmpliacion, false);
            }
        }

        //activa o desactiva paneles de paises y estados
        protected void dplTipoComision_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsTipo = dplTipoComision.SelectedValue.ToString();

            if (lsTipo == "1")
            {
                clsFuncionesGral.Activa_Paneles(pnlPais, false);
                clsFuncionesGral.Activa_Paneles(pnlEstado, false);

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un tipo de comisión.');", true);
                return;
            }
            else if (lsTipo == "2")
            {
                dplEstado.SelectedIndex = 0;
                txtPais.Text = dictionary.MIXTO;
                clsFuncionesGral.Activa_Paneles(pnlPais, false);
                clsFuncionesGral.Activa_Paneles(pnlEstado, true);
            }
            else if (lsTipo == "3")
            {
                dplEstado.SelectedIndex = 0;
                txtPais.Text = dictionary.CADENA_NULA;
                clsFuncionesGral.Activa_Paneles(pnlPais, true);
                clsFuncionesGral.Activa_Paneles(pnlEstado, false);
            }
        }

    }
}