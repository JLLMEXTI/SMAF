using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;
using System.Drawing;
using Telerik.Charting;

namespace InapescaWeb.Reportes
{
    public partial class Comparativo_Mes : System.Web.UI.Page
    {
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static clsDictionary dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
                Carga_Valores();
            }
        }


        public void Carga_Valores()
        {
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lnkHome.Text = "INICIO";
            Label1.Text = "Comparativo estadistico por periodo mensual";
            Label2.Text = "Seleccione periodo mensual:";
            CheckBox1.Text = clsFuncionesGral.ConvertMayus("Comparativo por meses");
            clsFuncionesGral.Activa_Paneles(pnlMes1, true, true);
            clsFuncionesGral.Activa_Paneles(pnlMes2, true, false);
            clsFuncionesGral.Activa_Paneles(pnlResultados, false);

            clsFuncionesGral.Llena_Lista(dplMes1, "=SELECCIONE=|ENERO|FEBRERO|MARZO|ABRIL|MAYO|JUNIO|JULIO|AGOSTO|SEPTIEMBRE|OCTUBRE|NOVIEMBRE|DICIEMBRE");

            Label3.Text = clsFuncionesGral.ConvertMayus("Año fiscal");
            Label6.Text = clsFuncionesGral.ConvertMayus("Año fiscal");

            dplAnios.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnios.DataTextField = dictionary.DESCRIPCION;
            dplAnios.DataValueField = dictionary.CODIGO;
            dplAnios.DataBind();

            dplAnios1.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnios1.DataTextField = dictionary.DESCRIPCION;
            dplAnios1.DataValueField = dictionary.CODIGO;
            dplAnios1.DataBind();

            Label4.Text = "Seleccione periodo mensual";
            clsFuncionesGral.Llena_Lista(dplMes2, "=SELECCIONE=|ENERO|FEBRERO|MARZO|ABRIL|MAYO|JUNIO|JULIO|AGOSTO|SEPTIEMBRE|OCTUBRE|NOVIEMBRE|DICIEMBRE");

            Label5.Text = clsFuncionesGral.ConvertMayus("Tipo Viaticos:");
            dplTipoViaticos.DataSource = MngNegocioViaticos.Metodo_Viaticos(true);
            dplTipoViaticos.DataTextField = dictionary.DESCRIPCION;
            dplTipoViaticos.DataValueField = dictionary.CODIGO;
            dplTipoViaticos.DataBind();

            Label7.Text = clsFuncionesGral.ConvertMayus("Estatus Comprobacion :");
            dplEstatusComp.DataSource = MngNegocioComision.ObtieneTipoComprobacion();
            dplEstatusComp.DataTextField = dictionary.DESCRIPCION;
            dplEstatusComp.DataValueField = dictionary.CODIGO;
            dplEstatusComp.DataBind();

            Label8.Text = clsFuncionesGral.ConvertMayus("estatus Financiero :");
            clsFuncionesGral.Llena_Lista(dplFInanciero, clsFuncionesGral.ConvertMayus("= s e l e c c i o n e =|comprometido|prepagado|pagado|pagado pasivo"));
            Label9.Text = clsFuncionesGral.ConvertMayus("Adscripcion :");
            Label11.Text = clsFuncionesGral.ConvertMayus("Adscripcion :");

            Label14.Text = clsFuncionesGral.ConvertMayus("Programa");
            Label13.Text = clsFuncionesGral.ConvertMayus("Programa");

            Label10.Text = clsFuncionesGral.ConvertMayus("Usuario");
            Label12.Text = clsFuncionesGral.ConvertMayus("Usuario");

            Label15.Text = clsFuncionesGral.ConvertMayus("Filtros de busqueda ");

            if (Session["Crip_Rol"].ToString() == dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion.DataTextField = dictionary.DESCRIPCION;
                dplAdscripcion.DataValueField = dictionary.CODIGO;
                dplAdscripcion.DataBind();

                dplAdscripcion1.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion1.DataTextField = dictionary.DESCRIPCION;
                dplAdscripcion1.DataValueField = dictionary.CODIGO;
                dplAdscripcion1.DataBind();
            }
            else if ((Session["Crip_Rol"].ToString() == dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == dictionary.JEFE_CENTRO))
            {
                dplAdscripcion.Visible = false;
                dplAdscripcion1.Visible = false;
                Label9.Visible = false;
                Label11.Visible = false;

                dplUsuarios.DataSource = MngNegocioUsuarios.MngBussinesUssers(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), "", true);
                dplUsuarios.DataTextField = dictionary.DESCRIPCION;
                dplUsuarios.DataValueField = dictionary.CODIGO;
                dplUsuarios.DataBind();


                dplUsuarios1.DataSource = MngNegocioUsuarios.MngBussinesUssers(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), "", true);
                dplUsuarios1.DataTextField = dictionary.DESCRIPCION;
                dplUsuarios1.DataValueField = dictionary.CODIGO;
                dplUsuarios1.DataBind();

                dplPrograma.DataSource = MngNegocioProyecto.ObtieneProyectos("", "", Session["Crip_Ubicacion"].ToString());
                dplPrograma.DataTextField = dictionary.DESCRIPCION;
                dplPrograma.DataValueField = dictionary.CODIGO;
                dplPrograma.DataBind();


                dplPrograma1.DataSource = MngNegocioProyecto.ObtieneProyectos("", "", Session["Crip_Ubicacion"].ToString());
                dplPrograma1.DataTextField = dictionary.DESCRIPCION;
                dplPrograma1.DataValueField = dictionary.CODIGO;
                dplPrograma1.DataBind();

            }


            btnExportar.Text = "Exportar";
            btnBuscar.Text = "Buscar";
            btnLimpiar.Text = "Limpiar";
            btnExportar.Enabled = false;

        }

        protected void dplAdscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            string lsUsuarioAdscripcion;

            if (lsAdscripcion == string.Empty)
            {
                dplUsuarios.Items.Clear();
                lsUsuarioAdscripcion = dictionary.CADENA_NULA;
                dplPrograma.Items.Clear();
                dplPrograma1.Items.Clear();
            }
            else
            {

                if (MngNegociosProgramas.IsDireccion(lsAdscripcion))
                {
                    dplUsuarios.Items.Clear();

                    dplPrograma.DataSource = MngNegociosProgramas.Programas(lsAdscripcion, dplAnios.SelectedValue);
                    dplPrograma.DataTextField = dictionary.DESCRIPCION;
                    dplPrograma.DataValueField = dictionary.CODIGO;
                    dplPrograma.DataBind();

                    dplUsuarios.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
                    dplUsuarios.DataTextField = dictionary.DESCRIPCION;
                    dplUsuarios.DataValueField = dictionary.CODIGO;
                    dplUsuarios.DataBind();

                }
                else
                {
                    dplUsuarios.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
                    dplUsuarios.DataTextField = dictionary.DESCRIPCION;
                    dplUsuarios.DataValueField = dictionary.CODIGO;
                    dplUsuarios.DataBind();

                    //Label10.Visible = true;
                    //dplUsuarios.Visible = true;

                    dplPrograma.DataSource = MngNegocioProyecto.ObtieneProyectoEjecucion(lsAdscripcion, dplAnios.SelectedValue.ToString());
                    dplPrograma.DataTextField = dictionary.DESCRIPCION;
                    dplPrograma.DataValueField = dictionary.CODIGO;
                    dplPrograma.DataBind();
                }

            }
        }

        protected void dplAdscripcion1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsAdscripcion = dplAdscripcion1.SelectedValue.ToString();
            string lsUsuarioAdscripcion;
            if (lsAdscripcion == string.Empty)
            {
                dplUsuarios.Items.Clear();
                lsUsuarioAdscripcion = dictionary.CADENA_NULA;
                dplPrograma.Items.Clear();
                dplPrograma1.Items.Clear();
            }
            else
            {

                if (MngNegociosProgramas.IsDireccion(lsAdscripcion))
                {
                    dplUsuarios1.Items.Clear();
                    //                    Label12.Visible = false;
                    //dplUsuarios1.Visible = false;


                    dplPrograma1.DataSource = MngNegociosProgramas.Programas(lsAdscripcion, dplAnios.SelectedValue);
                    dplPrograma1.DataTextField = dictionary.DESCRIPCION;
                    dplPrograma1.DataValueField = dictionary.CODIGO;
                    dplPrograma1.DataBind();

                    dplUsuarios1.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
                    dplUsuarios1.DataTextField = dictionary.DESCRIPCION;
                    dplUsuarios1.DataValueField = dictionary.CODIGO;
                    dplUsuarios1.DataBind();

                }
                else
                {
                    dplUsuarios1.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
                    dplUsuarios1.DataTextField = dictionary.DESCRIPCION;
                    dplUsuarios1.DataValueField = dictionary.CODIGO;
                    dplUsuarios1.DataBind();

                    //  Label12.Visible = true;
                    // dplUsuarios1.Visible = true;

                    dplPrograma1.DataSource = MngNegocioProyecto.ObtieneProyectoEjecucion(lsAdscripcion, dplAnios1.SelectedValue.ToString());
                    dplPrograma1.DataTextField = dictionary.DESCRIPCION;
                    dplPrograma1.DataValueField = dictionary.CODIGO;
                    dplPrograma1.DataBind();
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

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                clsFuncionesGral.Activa_Paneles(pnlMes2, true, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlMes2, true, false);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            dplMes1.SelectedIndex = 0;
            dplMes2.SelectedIndex = 0;
            dplAdscripcion.SelectedIndex = 0;
            dplAdscripcion1.SelectedIndex = 0;
            dplPrograma.Items.Clear();
            dplPrograma1.Items.Clear();
            dplUsuarios.Items.Clear();
            dplUsuarios.Items.Clear();

            dplTipoViaticos.SelectedIndex = 0;
            dplEstatusComp.SelectedIndex = 0;
            dplFInanciero.SelectedIndex = 0;
            dplAnios.SelectedIndex = 0;
            dplAnios1.SelectedIndex = 0;

            clsFuncionesGral.Activa_Paneles(pnlResultados, false);
            btnExportar.Enabled = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            btnExportar.Enabled = true;
            clsFuncionesGral.Activa_Paneles(pnlResultados, true);
            bool isDireccion = false;
            bool isDireccion1 = false;
            string adscripcion = "", adscripcionPrograma = "", anio = "", mes = "", programa = "", usuario = "", formaPago = "", estatus = "", financieros = "";
            string[] lsCadena;

            isDireccion = MngNegociosProgramas.IsDireccion(dplAdscripcion.SelectedValue.ToString());

            if ((CheckBox1.Checked) && (pnlMes2.Enabled))
            {

                isDireccion1 = MngNegociosProgramas.IsDireccion(dplAdscripcion1.SelectedValue.ToString());

                if (dplAnios1.SelectedValue == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar el ejercicio fiscal para la busqueda ');", true);
                    return;
                }


                if (dplAnios.SelectedValue == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar el ejercicio fiscal para la busqueda ');", true);
                    return;
                }


                if ((dplMes1.SelectedIndex == 0) && (dplMes2.SelectedIndex != 0))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar un mes contra un año completo');", true);
                    return;
                }
                else if ((dplMes1.SelectedIndex != 0) && (dplMes2.SelectedIndex == 0))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar un mes contra un año completo');", true);
                    return;
                }

                if ((dplAdscripcion1.SelectedIndex == 0) | dplAdscripcion1.SelectedIndex != 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adscripcion para busqueda');", true);
                    return;
                }
                else if ((dplAdscripcion1.SelectedIndex != 0) | dplAdscripcion1.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adscripcion para busqueda');", true);
                    return;
                }


                if ((isDireccion) && (!isDireccion1))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los programas de una direccion contra proyecto de crip ');", true);
                    return;
                }
                else if ((!isDireccion) && (isDireccion1))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los programas de una direccion contra proyecto de crip ');", true);
                    return;
                }

                if ((isDireccion) && (isDireccion1))
                {
                    if ((dplPrograma.SelectedIndex == 0) && (dplPrograma1.SelectedIndex > 0))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los programas de una direccion en un mes contra uno solo de otro');", true);
                        return;
                    }
                    else if ((dplPrograma1.SelectedIndex == 0) && (dplPrograma.SelectedIndex > 0))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los programas de una direccion en un mes contra uno solo de otro');", true);
                        return;
                    }
                }

                if ((!isDireccion) && (!isDireccion1))
                {
                    if ((dplPrograma.SelectedIndex == 0) && (dplPrograma1.SelectedIndex > 0))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los programas de un crip en un mes contra uno solo de otro');", true);
                        return;
                    }
                    else if ((dplPrograma1.SelectedIndex == 0) && (dplPrograma.SelectedIndex > 0))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los programas de un crip en un mes contra uno solo de otro');", true);
                        return;
                    }
                }


                if ((dplUsuarios.SelectedIndex == 0) && (dplUsuarios1.SelectedIndex > 0))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los usuarios de un mes contra uno solo de otro');", true);
                    return;
                }
                else if ((dplUsuarios1.SelectedIndex == 0) && (dplUsuarios.SelectedIndex > 0))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede comparar todos los usuarios de un mes contra uno solo de otro');", true);
                    return;
                }




            }
            else
            {
                //cuando no es por comparativo

                if (dplAnios.SelectedValue == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar el ejercicio fiscal para la busqueda ');", true);
                    return;
                }

                if (dplAdscripcion.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adscripcion para busqueda');", true);
                    return;
                }


                //si es direccion calcula los progamas de las direcciones
                if (isDireccion)
                {
                    if (dplPrograma.SelectedIndex == 0)
                    {
                        List<Programa> listaProgramsDirecciones = MngNegociosProgramas.Obtiene_Programas(dplAdscripcion.SelectedValue.ToString());

                        foreach (Programa obj in listaProgramsDirecciones)
                        {

                            List<Entidad> ListaProyectoAdscripcion = MngNegocioProyecto.ListaProyectoAdcripcion(dplAdscripcion.SelectedValue.ToString(), obj.Id_Programa, dplAnios.SelectedValue.ToString());



                        }



                    }
                    else
                    {

                    }


                }
                //sino es direccion si es solo crip  calcula programas
                else
                {
                    anio = dplAnios.SelectedValue.ToString();

                    if (dplMes1.SelectedIndex == 0)
                    {
                        mes = dictionary.CADENA_NULA;
                    }
                    else
                    {
                        mes = dplMes1.SelectedValue.ToString();
                    }

                    adscripcion = dplAdscripcion.SelectedValue.ToString();

                    if (dplPrograma.SelectedIndex == 0)
                    {
                        programa = dictionary.CADENA_NULA;
                        adscripcionPrograma = dictionary.CADENA_NULA;
                    }
                    else
                    {
                        lsCadena = dplPrograma.SelectedValue.ToString().Split(new Char[] { '|' });
                        adscripcionPrograma = lsCadena[1];
                        programa = lsCadena[0];
                    }

                    if (dplUsuarios.SelectedIndex == 0)
                    {
                        usuario = dictionary.CADENA_NULA;
                    }
                    else
                    {
                        usuario = dplUsuarios.SelectedValue.ToString();
                    }

                    if (dplTipoViaticos.SelectedIndex == 0)
                    {
                        formaPago = dictionary.CADENA_NULA;
                    }
                    else
                    {
                        formaPago = dplTipoViaticos.SelectedValue.ToString();
                    }


                    if (dplEstatusComp.SelectedIndex == 0)
                    {
                        estatus = dictionary.CADENA_NULA;
                    }
                    else
                    {
                        estatus = dplEstatusComp.SelectedValue.ToString();
                    }

                    if (dplFInanciero.SelectedIndex == 0)
                    {
                        financieros = dictionary.CADENA_NULA;

                    }
                    else
                    {
                        financieros = dplFInanciero.SelectedValue.ToString();
                    }

                    if (programa == dictionary.CADENA_NULA)
                    {
                        List<Entidad> ListaComision = MngNegocioProyecto.ObtieneProyectoEjecucion(adscripcion, anio);

                        // List<Programa> listaProgramsDirecciones = MngNegociosProgramas.Obtiene_Programas(adscripcion, "", true);

                        pnlBarDatos.Items.Clear();

                        if (ListaComision.Count > 0)
                        {
                            foreach (Entidad x in ListaComision)
                            {

                                if ((x.Codigo == "") | (x.Codigo == null))
                                {
                                    ;
                                }
                                else
                                {
                                    StringBuilder controls = new StringBuilder();

                                    lsCadena = x.Codigo.Split(new Char[] { '|' });
                                    adscripcionPrograma = lsCadena[1];
                                    programa = lsCadena[0];

                                    List<ComisionComparativo> lstComparativo = MngNegocioComisionDetalle.ListaFiltros1(anio, adscripcion, mes,adscripcionPrograma, programa, usuario, formaPago, estatus, financieros);

                                    controls.Append("  <table cellpadding='0' cellspacing='0' runat='server' id='tbl2' border='2' width='100%'>");
                                    controls.Append(" <tr >");
                                    controls.Append("<td style='width: 100%; text-align: center' colspan = '9'> Viaticos otorgados </a></td>");
                                    controls.Append("<td style='width: 100%; text-align: center' colspan = '12'> Comprobacion de montos otorgados </a></td>");

                                    controls.Append("</tr>");
                                    controls.Append(" <tr >");
                                    //controls.Append("<td style='width: 15%; text-align: center'> Nombre</a></td>");
                                    controls.Append("<td style=' text-align: center'> Nombre</a></td>");
                                    controls.Append("<td style=' text-align: center'> Oficio </td>");
                                    controls.Append("<td style='text-align: center'>Lugar</td>");
                                    controls.Append("<td style=' text-align: center'>Periodo</td>");
                                    controls.Append("<td style=' text-align: center'>Viaticos Pagados</td>");
                                    controls.Append("<td style='text-align: center'>Combustible Pagados</td>");
                                    controls.Append("<td style=' text-align: center'>Peaje Pagados</td>");
                                    controls.Append("<td style=' text-align: center'>Pasaje Pagados</td>");
                                    controls.Append("<td style=' text-align: center'>Singladuras Pagadas</td>");


                                    controls.Append("<td style=' text-align: center'>Vaticos Comprobados</td>");
                                    controls.Append("<td style=' text-align: center'>Fecha Comprobacion</td>");
                                    controls.Append("<td style=' text-align: center'>Combustible Comprobado</td>");
                                    controls.Append("<td style=' text-align: center'>Fecha Comprobacion</td>");
                                    controls.Append("<td style=' text-align: center'>Peaje Comprobado</td>");
                                    controls.Append("<td style=' text-align: center'>Peaje Fecha</td>");
                                    controls.Append("<td style=' text-align: center'>Pasaje Comprobado</td>");
                                    controls.Append("<td style=' text-align: center'>Fecha Comprobacion</td>");

                                    controls.Append("<td style=' text-align: center'>Singladuras Comprobado</td>");
                                    controls.Append("<td style=' text-align: center'>Fecha Comprobacion</td>");


                                    controls.Append("<td style=' text-align: center'>Reintegro Efectuado</td>");
                                    controls.Append("<td style=' text-align: center'>Fecha Reintegro</td>");

                                    controls.Append("</tr>");

                                    foreach (ComisionComparativo cc in lstComparativo)
                                    {
                                     /*   if ((cc.Viaticos != dictionary.NUMERO_CERO) | (cc.Combustible != dictionary.NUMERO_CERO) | (cc.Peaje != dictionary.NUMERO_CERO) | (cc.Pasaje != dictionary.NUMERO_CERO))
                                        {*
                                      * descometar para puros pagados*/
                                            string arc = cc.Archivo.Replace("RJL-INAPESCA-", "");
                                            arc = arc.Replace("-" + anio + ".pdf", "");

                                            controls.Append(" <tr >");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + MngNegocioUsuarios.Obtiene_Nombre(cc.Comisionado) + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + arc + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + cc.Lugar + "</a></td>");

                                            if (cc.Inicio == cc.Final)
                                            {
                                                controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(cc.Inicio)) + "</a></td>");
                                            }
                                            else
                                            {
                                                controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.ConvertMayus("Del ") + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(cc.Inicio)) + clsFuncionesGral.ConvertMayus("  al  ") + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(cc.Final)) + "</a></td>");

                                            }

                                            controls.Append("<td style='width: 25%; text-align: center'>" + dictionary.PESOS + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(cc.Viaticos).ToString()) + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'>" + dictionary.PESOS + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(cc.Combustible).ToString()) + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'>" + dictionary.PESOS + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(cc.Peaje).ToString()) + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'>" + dictionary.PESOS + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(cc.Pasaje).ToString()) + "</a></td>");

                                            controls.Append("<td style='width: 25%; text-align: center'>" + dictionary.PESOS + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.Convert_Double(cc.Singladuras).ToString()) + "</a></td>");
                                            //viaticos
                                            Entidad oEntidad = MngNegocioComprobacion.Obtiene_Comprobaciones(anio, adscripcion, cc.Oficio, cc.Comisionado, "'5','9','10','11','12','14','15'");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + oEntidad.Codigo + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(oEntidad.Descripcion) + "</a></td>");
                                            //combustible
                                            oEntidad = null;
                                            oEntidad = MngNegocioComprobacion.Obtiene_Comprobaciones(anio, adscripcion, cc.Oficio, cc.Comisionado, "'6'");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + oEntidad.Codigo + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(oEntidad.Descripcion) + "</a></td>");
                                            //peaje
                                            oEntidad = null;
                                            oEntidad = MngNegocioComprobacion.Obtiene_Comprobaciones(anio, adscripcion, cc.Oficio, cc.Comisionado, "'7','16'");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + oEntidad.Codigo + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(oEntidad.Descripcion) + "</a></td>");
                                            //pasaje
                                            oEntidad = null;
                                            oEntidad = MngNegocioComprobacion.Obtiene_Comprobaciones(anio, adscripcion, cc.Oficio, cc.Comisionado, "'8'");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + oEntidad.Codigo + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(oEntidad.Descripcion) + "</a></td>");

                                            Comision c = MngNegocioComision.Detalle_Comision_Reimpresion(cc.Oficio, adscripcion,anio , cc.Comisionado);
                                            if ((c.Zona_Comercial == "15") | (c.Zona_Comercial == "18"))
                                            {
                                                oEntidad = null;
                                                oEntidad = MngNegocioComprobacion.Obtiene_Comprobaciones(anio, adscripcion, cc.Oficio, cc.Comisionado, "'12'");
                                                controls.Append("<td style='width: 25%; text-align: center'> " + oEntidad.Codigo + "</a></td>");
                                                controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(oEntidad.Descripcion) + "</a></td>");

                                            }
                                            else
                                            {
                                                controls.Append("<td style='width: 25%; text-align: center'> " + dictionary.NUMERO_CERO + "</a></td>");
                                                controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(dictionary.FECHA_NULA) + "</a></td>");
                                            }

                                            //reintegro
                                            oEntidad = null;
                                            oEntidad = MngNegocioComprobacion.Obtiene_Comprobaciones(anio, adscripcion, cc.Oficio, cc.Comisionado, "'13'");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + oEntidad.Codigo + "</a></td>");
                                            controls.Append("<td style='width: 25%; text-align: center'> " + clsFuncionesGral.FormatFecha(oEntidad.Descripcion) + "</a></td>");


                                            controls.Append("</tr>");

                                            oEntidad = null;
                                            c = null;
                                       /// }//descomentar para pagados unicamente
                                    }

                                    controls.Append(" <tr >");
                                    controls.Append("<td style='width: 100%; text-align: center' colspan = '21'>  </a></td>");
                                    controls.Append("</tr>");
                                    controls.Append("</table>");
                                    controls.Append("<br>");

                                    PlaceHolder ph = new PlaceHolder();
                                    ph.Controls.Add(new LiteralControl(controls.ToString()));

                                    Telerik.Web.UI.RadPanelItem r1 = new Telerik.Web.UI.RadPanelItem();
                                    r1.Text = x.Descripcion;
                                    r1.Expanded = false;

                                    Telerik.Web.UI.RadPanelItem r2 = new Telerik.Web.UI.RadPanelItem();
                                    r2.Controls.Add(ph);

                                    r1.Items.Add(r2);

                                    /*  Telerik.Web.UI.RadPanelItem rg = new Telerik.Web.UI.RadPanelItem();
                             
                                    Telerik.Web.UI.RadHtmlChart gf = new Telerik.Web.UI.RadHtmlChart();
                                    gf.Width = 900;
                                    gf.Height = 900;
                                    gf.Transitions = true;
                                    gf.Skin = "Silk";

                                    gf.Appearance.FillStyle.BackgroundColor = Color.Transparent;
                                
                                    gf.ChartTitle.Text = "Grafica Balance de recursos por proyecto " + x.Descripcion  ;
                                    gf.ChartTitle.Appearance.Align = Telerik.Web.UI.HtmlChart.ChartTitleAlign.Center;
                                    gf.ChartTitle.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartTitlePosition.Top;
                                    gf.ChartTitle.Appearance.BackgroundColor = Color.Transparent;

                                    gf.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Bottom ;
                                    gf.Legend.Appearance.Visible = true;

                                    Telerik.Web.UI.LineSeries  dn = new Telerik.Web.UI.LineSeries ();
                                    dn.Appearance.FillStyle.BackgroundColor = Color.Transparent;
                                    */


                                    pnlBarDatos.Items.Add(r1);

                                    controls = null;
                                }
                            }

                        }
                    }
                    else //si si quiere especificamente de un proyecto programa
                    {

                    }

                }




            }





        }
    }
}