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
using System.Text.RegularExpressions;
namespace InapescaWeb.Utilerias
{
    public partial class ProyeccionViaticosPorProyecto : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();
        static CultureInfo culture = new CultureInfo("es-MX");
        static private Regex r;
        string ExisteReg;
        double TarifaDiaria;
        int lsZona = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    if (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO || Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR || Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION || (Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO && Session["Crip_Ubicacion"].ToString() == Dictionary.SRF) || Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADJUNTO || Session["Crip_Rol"].ToString() == Dictionary.INVESTIGADOR || Session["Crip_Rol"].ToString() == Dictionary.JEFE_DEPARTAMENTO || Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO)
                    {
                        Carga_Valores();

                        clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
                        //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidas, true);
                    }
                    else
                    {
                        Response.Redirect("../Home/Home.aspx", true);
                    }

                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }

        }
        public void Carga_Valores()
        {
            
            Label1.Text = clsFuncionesGral.ConvertMayus("Proyección de Viaticos Eseciales por Proyecto Mensual");
            Label3.Text = clsFuncionesGral.ConvertMayus("Proyecto :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Adscripción :");
            Label2.Text = clsFuncionesGral.ConvertMayus("Comisionado :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Mes de Proyección :");
            Label6.Text = clsFuncionesGral.ConvertMayus("Total de Días a Comisionar:");
            Label7.Text = clsFuncionesGral.ConvertMayus("Gasto por Concepto de:");
            btnAceptar.Text = clsFuncionesGral.ConvertMayus("Agregar Selección");
            btnGuardar.Text = clsFuncionesGral.ConvertMayus("Guardar Proyección");
            txtCantDias.Text = "0.0";
            if (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO || Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR || (Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO && Session["Crip_Ubicacion"].ToString() == Dictionary.SRF) || Session["Crip_Rol"].ToString() == Dictionary.INVESTIGADOR || Session["Crip_Rol"].ToString() == Dictionary.JEFE_DEPARTAMENTO || Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO)
            {
                dplUnidadAdministrativa.DataSource = MngNegocioDependencia.ObtieneCentro1(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString());
                dplUnidadAdministrativa.DataTextField = "Descripcion";
                dplUnidadAdministrativa.DataValueField = "Codigo";
                dplUnidadAdministrativa.DataBind();
                dplUnidadAdministrativa.Enabled = false;

                if (Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO && Session["Crip_Ubicacion"].ToString() == Dictionary.SRF)
                {
                    dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Dictionary.DIRECTOR_ADMINISTRACION, Dictionary.DGAA);
                    dplProyectos.DataTextField = "Descripcion";
                    dplProyectos.DataValueField = "Codigo";
                    dplProyectos.DataBind();
                }
                else 
                {
                    dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), Session["Crip_Ubicacion"].ToString());
                    dplProyectos.DataTextField = "Descripcion";
                    dplProyectos.DataValueField = "Codigo";
                    dplProyectos.DataBind();
                }
                

                dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(Session["Crip_Ubicacion"].ToString());
                dplPersonal.DataTextField = "Descripcion";
                dplPersonal.DataValueField = "Codigo";
                dplPersonal.DataBind();
                string[] Mes = lsHoy.Split('-');

                dplMes.DataSource = MngNegocioProyeccionViaticos.Obtener_Lista_Meses(Mes[1]);
                dplMes.DataTextField = "Descripcion";
                dplMes.DataValueField = "Codigo";
                dplMes.DataBind();

            }
            else 
            {



                

                if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADJUNTO)
                {
                    if(Session["Crip_Ubicacion"].ToString()==Dictionary.DGAIPP)
                    {
                        dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneProyectosSustantivos("3", Session["Crip_Ubicacion"].ToString(),"1");
                        dplUnidadAdministrativa.DataTextField = "Descripcion";
                        dplUnidadAdministrativa.DataValueField = "Codigo";
                        dplUnidadAdministrativa.DataBind();

                        dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(Session["Crip_Ubicacion"].ToString());
                        dplPersonal.DataTextField = "Descripcion";
                        dplPersonal.DataValueField = "Codigo";
                        dplPersonal.DataBind();
                    }
                    else if (Session["Crip_Ubicacion"].ToString() == Dictionary.DGAIPA)
                    {
                        dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneProyectosSustantivos("3", Session["Crip_Ubicacion"].ToString(), "3");
                        dplUnidadAdministrativa.DataTextField = "Descripcion";
                        dplUnidadAdministrativa.DataValueField = "Codigo";
                        dplUnidadAdministrativa.DataBind();

                        dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(Session["Crip_Ubicacion"].ToString());
                        dplPersonal.DataTextField = "Descripcion";
                        dplPersonal.DataValueField = "Codigo";
                        dplPersonal.DataBind();
                    }
                    else 
                    {
                        dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneProyectosSustantivos("3", Session["Crip_Ubicacion"].ToString(),"1");
                        dplUnidadAdministrativa.DataTextField = "Descripcion";
                        dplUnidadAdministrativa.DataValueField = "Codigo";
                        dplUnidadAdministrativa.DataBind();

                        dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia2(Session["Crip_Ubicacion"].ToString());
                        dplPersonal.DataTextField = "Descripcion";
                        dplPersonal.DataValueField = "Codigo";
                        dplPersonal.DataBind();
                    
                    }


                    string[] Mes = lsHoy.Split('-');


                    dplMes.DataSource = MngNegocioProyeccionViaticos.Obtener_Lista_Meses(Mes[1]);
                    dplMes.DataTextField = "Descripcion";
                    dplMes.DataValueField = "Codigo";
                    dplMes.DataBind();

                    dplProyectos.Items.Clear();
                }
                else 
                {
                    dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                    dplUnidadAdministrativa.DataTextField = "Descripcion";
                    dplUnidadAdministrativa.DataValueField = "Codigo";
                    dplUnidadAdministrativa.DataBind();
                    dplPersonal.Items.Clear();
                    dplProyectos.Items.Clear();
                    string[] Mes = lsHoy.Split('-');
                    dplMes.DataSource = MngNegocioProyeccionViaticos.Obtener_Lista_Meses(Mes[1]);
                    dplMes.DataTextField = "Descripcion";
                    dplMes.DataValueField = "Codigo";
                    dplMes.DataBind();
                }
            }

            dplConceptoGasto.Items.Clear();
            dplConceptoGasto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
            dplConceptoGasto.Items.Add(new ListItem("VIATICOS", "1"));
            //dplConceptoGasto.Items.Add(new ListItem("SINGLADURAS", "2"));
            dplConceptoGasto.AppendDataBoundItems = true;
            dplConceptoGasto.DataBind();

        }
        protected void gvComisionados_SelectedIndexChanged(object sender, EventArgs e)
        {

            string CadenaELiminar = gvComisionados.Rows[Convert.ToInt32(gvComisionados.SelectedIndex.ToString())].Cells[1].Text.ToString();
            string CadenaELiminar2 = gvComisionados.Rows[Convert.ToInt32(gvComisionados.SelectedIndex.ToString())].Cells[6].Text.ToString();
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
                    if ((CadenaELiminar != cadena )|| (CadenaELiminar2 != r.Cells[6].Text.ToString()))
                    {
                        InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();
                        objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                        objGrid.RFC = r.Cells[1].Text.ToString();
                        objGrid.Adscripcion = r.Cells[2].Text.ToString();
                        objGrid.Lugar = r.Cells[3].Text.ToString();
                        objGrid.Ubicacion = r.Cells[4].Text.ToString();
                        objGrid.Rol = r.Cells[5].Text.ToString();
                        objGrid.Puesto = r.Cells[6].Text.ToString();
                        ListaGrid.Add(objGrid);
                    }
                }
                Obtener_TablaActualizada(ListaGrid);
                ListaGrid = null;
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
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            
            if ((dplUnidadAdministrativa.SelectedValue.ToString() == null) | (dplUnidadAdministrativa.SelectedValue.ToString() == Dictionary.CADENA_NULA) | (dplUnidadAdministrativa.SelectedValue.ToString() == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede agregar un campo en blanco , seleccione alguna Unidad Administrativa para agregar');", true);
                return;
            } 
            else if ((dplPersonal.SelectedValue.ToString() == null) | (dplPersonal.SelectedValue.ToString() == Dictionary.CADENA_NULA) | (dplPersonal.SelectedValue.ToString() == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede agregar un campo en blanco , seleccione algun comisionado para agregar');", true);
                return;
            }
            else if ((dplConceptoGasto.SelectedValue.ToString() == null) | (dplConceptoGasto.SelectedValue.ToString() == Dictionary.CADENA_NULA) | (dplConceptoGasto.SelectedValue.ToString() == string.Empty) | (dplConceptoGasto.SelectedValue.ToString() == Dictionary.NUMERO_CERO))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede agregar un campo en blanco , seleccione un Concepto del tipo de gasto para agregar');", true);
                return;
            }  
            else if ((dplProyectos.SelectedValue.ToString()==null) | (dplProyectos.SelectedValue.ToString() == Dictionary.CADENA_NULA) | (dplProyectos.SelectedValue.ToString() == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede agregar un campo en blanco , seleccione un proyecto para agregar');", true);
                return;
            }
            else if ((txtCantDias.Text == null) | (txtCantDias.Text == Dictionary.CADENA_NULA) | (txtCantDias.Text == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede agregar un campo en blanco , Inserte un valor númerico para continuar');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(txtCantDias.Text))
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La cantidad de días a viáticar debe ser númerico : enteros o un solo decimal');", true);
                return;
                
            }
            else
            {
                ExisteReg = Dictionary.NUMERO_CERO;
                string CadenaProyUbic = dplProyectos.SelectedValue;
                string[] CadProy = CadenaProyUbic.Split('|');
                string Proyecto = CadProy[0];
                string DepProy = CadProy[1];
                Usuario ObjUsuario1 = MngNegocioUsuarios.DatosComisionado1(dplPersonal.SelectedValue.ToString(), year);
                string ubica1 = ObjUsuario1.Ubicacion;
                if (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO || Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR)
                {
                    ExisteReg = MngNegocioProyeccionViaticos.Obtener_Existen_Registro(dplPersonal.SelectedValue.ToString(), Proyecto, DepProy, dplUnidadAdministrativa.SelectedValue.ToString(), dplMes.SelectedValue.ToString(), dplConceptoGasto.SelectedValue.ToString());
                }
                else if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADJUNTO) 
                {
                    ExisteReg = MngNegocioProyeccionViaticos.Obtener_Existen_Registro(dplPersonal.SelectedValue.ToString(), Proyecto, DepProy, ubica1, dplMes.SelectedValue.ToString(), dplConceptoGasto.SelectedValue.ToString());
                }
                else if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION || (Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO && Session["Crip_Ubicacion"].ToString() == Dictionary.SRF))
                {
                    ExisteReg = MngNegocioProyeccionViaticos.Obtener_Existen_Registro(dplPersonal.SelectedValue.ToString(), Proyecto, DepProy, dplUnidadAdministrativa.SelectedValue.ToString(), dplMes.SelectedValue.ToString(), dplConceptoGasto.SelectedValue.ToString());
                }
                
                if (ExisteReg == Dictionary.NUMERO_CERO)
                {
                    string CantidadDias = txtCantDias.Text;
                    string[] CantDias = CantidadDias.Split('.');
                    int entero = CantDias[0].Length;
                    if (CantDias.Length > 1)
                    {
                        if (dplConceptoGasto.SelectedValue.ToString() == "2")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Para el Concepto de SINGLADURAS solo se permiten días completos');", true);
                            return;
                        }
                        else if (dplConceptoGasto.SelectedValue.ToString() == "1")
                        {
                            int decimales = CantDias[1].Length;
                            if (decimales > 1)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La cantidad de días a viaticar debe ser númerico con un solo decimal');", true);
                                return;
                            }
                            else if (decimales == 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de insertar una cantidad valida de días a viaticar');", true);
                                return;
                            }
                            else 
                            {
                                Boolean Existe = false;
                                string patron = "1|2|3|4|6|7|8|9";

                                if (Regex.IsMatch(CantDias[1], patron))
                                {
                                    Existe = true;

                                }

                                if (!Existe)
                                {

                                    string DIFERENCIA = "0";
                                    DIFERENCIA = Convert.ToString(Convert.ToDateTime(year + "-12-31") - Convert.ToDateTime(lsHoy));
                                    string[] Dif = DIFERENCIA.Split('.');
                                    string Dif1 = Dif[0];
                                    string Dif2 = Dif[1];
                                    int DiasFaltantes = 0;
                                    DiasFaltantes = Convert.ToInt32(Dif[0]);
                                    string DiasAcumulados = MngNegocioProyeccionViaticos.Dias_Acumulados(dplPersonal.SelectedValue.ToString(), year);
                                    string CantDiasInsert = txtCantDias.Text;
                                    double TotalDiasAcum = (Convert.ToDouble(DiasAcumulados)) + (Convert.ToDouble(CantDiasInsert));
                                    double DifDiasAcum = DiasFaltantes - (Convert.ToDouble(DiasAcumulados));
                                    if (TotalDiasAcum > DiasFaltantes)
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días de los días faltantes para que culmine el año ');", true);
                                        return;

                                    }
                                    else
                                    {
                                        string DiasAcumuladosMes = MngNegocioProyeccionViaticos.Dias_AcumuladosMes(dplPersonal.SelectedValue.ToString(), year, dplMes.SelectedValue.ToString());
                                        string CantDiasInsertMes= txtCantDias.Text;
                                        double TotalDiasAcumMes = (Convert.ToDouble(DiasAcumuladosMes)) + (Convert.ToDouble(CantDiasInsertMes));
                                        string DifDiasMes = "0";
                                        string[] Mes = lsHoy.Split('-');
                                        int lsMes = Convert.ToInt32(Mes[1]);
                                        int lsYear = Convert.ToInt32(year);
                                        int lsDiasMes = DateTime.DaysInMonth(lsYear, lsMes);
                                        double CantDiasMesInsert = Convert.ToDouble(CantDiasInsert);

                                        if (Mes[1] == dplMes.SelectedValue.ToString())
                                        {

                                            DifDiasMes = Convert.ToString(Convert.ToDateTime(year + "-" + Mes[1] + "-" + lsDiasMes) - Convert.ToDateTime(lsHoy));

                                        }
                                        else
                                        {

                                            string psMes = dplMes.SelectedValue.ToString();
                                            lsDiasMes = DateTime.DaysInMonth(lsYear, Convert.ToInt32(psMes));
                                            DifDiasMes = Convert.ToString(Convert.ToDateTime(year + "-" + psMes + "-" + lsDiasMes) - Convert.ToDateTime(year + "-" + psMes + "-01"));
                                        }

                                        string[] DifMes = DifDiasMes.Split('.');
                                        string DifMes1 = DifMes[0];
                                        string DifMes2 = DifMes[1];
                                        int DiasMesFaltantes = 0;
                                        if (Mes[1] == dplMes.SelectedValue.ToString())
                                        {
                                            DiasMesFaltantes = Convert.ToInt32(DifMes[0]);
                                        }
                                        else
                                        {
                                            DiasMesFaltantes = Convert.ToInt32(DifMes[0]) + 1;
                                        }

                                        if (TotalDiasAcumMes > DiasMesFaltantes)
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días a los que contiene el mes ');", true);
                                            return;
                                        }
                                        else
                                        {
                                            if (CantDiasMesInsert > DiasMesFaltantes)
                                            {
                                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días a los que contiene el mes ');", true);
                                                return;
                                            }
                                            else
                                            {
                                                string ubica;

                                                Usuario ObjUsuario = MngNegocioUsuarios.DatosComisionado1(dplPersonal.SelectedValue.ToString(), year);
                                                ubica = ObjUsuario.Ubicacion;
                                                //ubica = dplUnidadAdministrativa.SelectedValue.ToString();

                                                List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                                foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                                {
                                                    string cadena = r.Cells[1].Text.ToString();
                                                    if (cadena == dplPersonal.SelectedValue.ToString() && r.Cells[6].Text.ToString() == dplConceptoGasto.SelectedItem.ToString())
                                                    {
                                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para este proyecto, si desea modificarlo hacer clic en eliminar para volver ha agregarlo!!!');", true);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                                        objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                                        objGrid.RFC = r.Cells[1].Text.ToString();
                                                        objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                                        objGrid.Lugar = r.Cells[3].Text.ToString();
                                                        objGrid.Ubicacion = r.Cells[4].Text.ToString();
                                                        objGrid.Rol = r.Cells[5].Text.ToString();
                                                        objGrid.Puesto = r.Cells[6].Text.ToString();
                                                        ListaGrid.Add(objGrid);
                                                    }

                                                }


                                                InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                                obj.Comisionado = dplPersonal.SelectedItem.Text;
                                                obj.RFC = dplPersonal.SelectedValue;
                                                obj.Adscripcion = dplProyectos.SelectedItem.Text;
                                                obj.Lugar = txtCantDias.Text;
                                                Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                                obj.Ubicacion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;
                                                obj.Rol = dplMes.SelectedItem.Text;
                                                obj.Puesto = dplConceptoGasto.SelectedItem.Text;
                                                ListaGrid.Add(obj);


                                                Obtener_TablaActualizada(ListaGrid);
                                                dplUnidadAdministrativa.Enabled = false;
                                                dplProyectos.Enabled = false;
                                                dplMes.Enabled = false;
                                                dplConceptoGasto.Enabled = false;
                                                txtCantDias.Text = "0.0";
                                                ListaGrid = null;
                                                oUbicacion = null;
                                                //dplConceptoGasto.Items.Clear();
                                                //dplConceptoGasto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
                                                //dplConceptoGasto.Items.Add(new ListItem("VIATICOS", "1"));
                                                //dplConceptoGasto.Items.Add(new ListItem("SINGLADURAS", "2"));
                                                //dplConceptoGasto.AppendDataBoundItems = true;
                                                //dplConceptoGasto.DataBind();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El valor decimal que esta insertando es invalido, En caso de requerir decimal solo se permite el .5 debido a que se trata de cantidad de días');", true);
                                    return;
                                }


                            }

                        }
                        else 
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se debe seleccionar un Concepto de Gasto para continuar');", true);
                            return;
                        }
                    } // termina if (CantDias.Length > 1)
                    else
                    {
                        string CantidadDias1 = txtCantDias.Text;
                        string[] CantDias1 = CantidadDias.Split(',');
                        int entero1 = CantDias1[0].Length;
                        int entero2;
                        
                        if (CantDias1.Length > 1)
                        {
                            entero2 = CantDias1[1].Length;
                            if (entero2 < 3)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de insertar un numero de días valido!!!');", true);
                                return;

                            }
                            else 
                            {
                                string DIFERENCIA = "0";
                                DIFERENCIA = Convert.ToString(Convert.ToDateTime(year + "-12-31") - Convert.ToDateTime(lsHoy));
                                string[] Dif = DIFERENCIA.Split('.');
                                string Dif1 = Dif[0];
                                string Dif2 = Dif[1];
                                int DiasFaltantes = 0;
                                DiasFaltantes = Convert.ToInt32(Dif[0]);
                                string DiasAcumulados = MngNegocioProyeccionViaticos.Dias_Acumulados(dplPersonal.SelectedValue.ToString(), year);
                                string CantDiasInsert = txtCantDias.Text;
                                double TotalDiasAcum = (Convert.ToDouble(DiasAcumulados)) + (Convert.ToDouble(CantDiasInsert));
                                double DifDiasAcum = DiasFaltantes - (Convert.ToDouble(DiasAcumulados));
                                if (TotalDiasAcum > DiasFaltantes)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días de los días faltantes para que culmine el año ');", true);
                                    return;

                                }
                                else
                                {
                                    string DiasAcumuladosMes = MngNegocioProyeccionViaticos.Dias_AcumuladosMes(dplPersonal.SelectedValue.ToString(), year, dplMes.SelectedValue.ToString());
                                    string CantDiasInsertMes = txtCantDias.Text;
                                    double TotalDiasAcumMes = (Convert.ToDouble(DiasAcumuladosMes)) + (Convert.ToDouble(CantDiasInsertMes));
                                    string DifDiasMes = "0";
                                    string[] Mes = lsHoy.Split('-');
                                    int lsMes = Convert.ToInt32(Mes[1]);
                                    int lsYear = Convert.ToInt32(year);
                                    int lsDiasMes = DateTime.DaysInMonth(lsYear, lsMes);
                                    double CantDiasMesInsert = Convert.ToDouble(CantDiasInsert);
                                    if (Mes[1] == dplMes.SelectedValue.ToString())
                                    {

                                        DifDiasMes = Convert.ToString(Convert.ToDateTime(year + "-" + Mes[1] + "-" + lsDiasMes) - Convert.ToDateTime(lsHoy));

                                    }
                                    else
                                    {

                                        string psMes = dplMes.SelectedValue.ToString();
                                        lsDiasMes = DateTime.DaysInMonth(lsYear, Convert.ToInt32(psMes));
                                        DifDiasMes = Convert.ToString(Convert.ToDateTime(year + "-" + psMes + "-" + lsDiasMes) - Convert.ToDateTime(year + "-" + psMes + "-01"));
                                    }

                                    string[] DifMes = DifDiasMes.Split('.');
                                    string DifMes1 = DifMes[0];
                                    string DifMes2 = DifMes[1];
                                    int DiasMesFaltantes = 0;
                                    if (Mes[1] == dplMes.SelectedValue.ToString())
                                    {
                                        DiasMesFaltantes = Convert.ToInt32(DifMes[0]);
                                    }
                                    else
                                    {
                                        DiasMesFaltantes = Convert.ToInt32(DifMes[0]) + 1;
                                    }

                                    if (TotalDiasAcumMes > DiasMesFaltantes)
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días a los que contiene el mes ');", true);
                                        return;
                                    }
                                    else
                                    {
                                        if (CantDiasMesInsert > DiasMesFaltantes)
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días a los que contiene el mes ');", true);
                                            return;
                                        }
                                        else
                                        {
                                            string ubica;

                                            Usuario ObjUsuario = MngNegocioUsuarios.DatosComisionado1(dplPersonal.SelectedValue.ToString(), year);
                                            ubica = ObjUsuario.Ubicacion;
                                            //ubica = dplUnidadAdministrativa.SelectedValue.ToString();

                                            List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                            {
                                                string cadena = r.Cells[1].Text.ToString();
                                                if (cadena == dplPersonal.SelectedValue.ToString() && r.Cells[6].Text.ToString() == dplConceptoGasto.SelectedItem.ToString())
                                                {
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para este proyecto, si desea modificarlo hacer clic en eliminar para volver ha agregarlo!!!');", true);
                                                    return;
                                                }
                                                else
                                                {
                                                    InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                                    objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                                    objGrid.RFC = r.Cells[1].Text.ToString();
                                                    objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                                    objGrid.Lugar = r.Cells[3].Text.ToString();
                                                    objGrid.Ubicacion = r.Cells[4].Text.ToString();
                                                    objGrid.Rol = r.Cells[5].Text.ToString();
                                                    objGrid.Puesto = r.Cells[6].Text.ToString();
                                                    ListaGrid.Add(objGrid);
                                                }

                                            }


                                            InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                            obj.Comisionado = dplPersonal.SelectedItem.Text;
                                            obj.RFC = dplPersonal.SelectedValue;
                                            obj.Adscripcion = dplProyectos.SelectedItem.Text;
                                            obj.Lugar = txtCantDias.Text;
                                            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                            obj.Ubicacion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;
                                            obj.Rol = dplMes.SelectedItem.Text;
                                            obj.Puesto = dplConceptoGasto.SelectedItem.Text;
                                            ListaGrid.Add(obj);


                                            Obtener_TablaActualizada(ListaGrid);
                                            dplUnidadAdministrativa.Enabled = false;
                                            dplProyectos.Enabled = false;
                                            dplMes.Enabled = false;
                                            dplConceptoGasto.Enabled = false;
                                            txtCantDias.Text = "0.0";
                                            ListaGrid = null;
                                            oUbicacion = null;
                                            //dplConceptoGasto.Items.Clear();
                                            //dplConceptoGasto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
                                            //dplConceptoGasto.Items.Add(new ListItem("VIATICOS", "1"));
                                            //dplConceptoGasto.Items.Add(new ListItem("SINGLADURAS", "2"));
                                            //dplConceptoGasto.AppendDataBoundItems = true;
                                            //dplConceptoGasto.DataBind();
                                        }
                                    }
                                }

                            
                            }


                        }
                        else 
                        {
                            string DIFERENCIA = "0";
                            DIFERENCIA = Convert.ToString(Convert.ToDateTime(year + "-12-31") - Convert.ToDateTime(lsHoy));
                            string[] Dif = DIFERENCIA.Split('.');
                            string Dif1 = Dif[0];
                            string Dif2 = Dif[1];
                            int DiasFaltantes = 0;
                            DiasFaltantes = Convert.ToInt32(Dif[0]);
                            string DiasAcumulados = MngNegocioProyeccionViaticos.Dias_Acumulados(dplPersonal.SelectedValue.ToString(), year);
                            string CantDiasInsert = txtCantDias.Text;
                            double TotalDiasAcum = (Convert.ToDouble(DiasAcumulados)) + (Convert.ToDouble(CantDiasInsert));
                            double DifDiasAcum = DiasFaltantes - (Convert.ToDouble(DiasAcumulados));
                            if (TotalDiasAcum > DiasFaltantes)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días de los días faltantes para que culmine el año ');", true);
                                return;

                            }
                            else
                            {
                                string DiasAcumuladosMes = MngNegocioProyeccionViaticos.Dias_AcumuladosMes(dplPersonal.SelectedValue.ToString(), year, dplMes.SelectedValue.ToString());
                                string CantDiasInsertMes = txtCantDias.Text;
                                double TotalDiasAcumMes = (Convert.ToDouble(DiasAcumuladosMes)) + (Convert.ToDouble(CantDiasInsertMes));
                                string DifDiasMes = "0";
                                string[] Mes = lsHoy.Split('-');
                                int lsMes = Convert.ToInt32(Mes[1]);
                                int lsYear = Convert.ToInt32(year);
                                int lsDiasMes = DateTime.DaysInMonth(lsYear, lsMes);
                                double CantDiasMesInsert = Convert.ToDouble(CantDiasInsert);
                                if (Mes[1] == dplMes.SelectedValue.ToString())
                                {

                                    DifDiasMes = Convert.ToString(Convert.ToDateTime(year + "-" + Mes[1] + "-" + lsDiasMes) - Convert.ToDateTime(lsHoy));

                                }
                                else
                                {

                                    string psMes = dplMes.SelectedValue.ToString();
                                    lsDiasMes = DateTime.DaysInMonth(lsYear, Convert.ToInt32(psMes));
                                    DifDiasMes = Convert.ToString(Convert.ToDateTime(year + "-" + psMes + "-" + lsDiasMes) - Convert.ToDateTime(year + "-" + psMes + "-01"));
                                }

                                string[] DifMes = DifDiasMes.Split('.');
                                string DifMes1 = DifMes[0];
                                string DifMes2 = DifMes[1];
                                int DiasMesFaltantes = 0;
                                if (Mes[1] == dplMes.SelectedValue.ToString())
                                {
                                    DiasMesFaltantes = Convert.ToInt32(DifMes[0]);
                                }
                                else
                                {
                                    DiasMesFaltantes = Convert.ToInt32(DifMes[0]) + 1;
                                }

                                if (TotalDiasAcumMes > DiasMesFaltantes)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días a los que contiene el mes ');", true);
                                    return;
                                }
                                else
                                {
                                    if (CantDiasMesInsert > DiasMesFaltantes)
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Es imposible comisionar un usuario mayor cantidad de días a los que contiene el mes ');", true);
                                        return;
                                    }
                                    else
                                    {
                                        string ubica;

                                        Usuario ObjUsuario = MngNegocioUsuarios.DatosComisionado1(dplPersonal.SelectedValue.ToString(), year);
                                        ubica = ObjUsuario.Ubicacion;
                                        //ubica = dplUnidadAdministrativa.SelectedValue.ToString();

                                        List<Entidades.GridView> ListaGrid = new List<InapescaWeb.Entidades.GridView>();

                                        foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
                                        {
                                            string cadena = r.Cells[1].Text.ToString();
                                            if (cadena == dplPersonal.SelectedValue.ToString() && r.Cells[6].Text.ToString() == dplConceptoGasto.SelectedItem.ToString())
                                            {
                                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para este proyecto, si desea modificarlo hacer clic en eliminar para volver ha agregarlo!!!');", true);
                                                return;
                                            }
                                            else
                                            {
                                                InapescaWeb.Entidades.GridView objGrid = new InapescaWeb.Entidades.GridView();

                                                objGrid.Comisionado = r.Cells[0].Text.ToString(); ;
                                                objGrid.RFC = r.Cells[1].Text.ToString();
                                                objGrid.Adscripcion = r.Cells[2].Text.ToString();
                                                objGrid.Lugar = r.Cells[3].Text.ToString();
                                                objGrid.Ubicacion = r.Cells[4].Text.ToString();
                                                objGrid.Rol = r.Cells[5].Text.ToString();
                                                objGrid.Puesto = r.Cells[6].Text.ToString();
                                                ListaGrid.Add(objGrid);
                                            }

                                        }


                                        InapescaWeb.Entidades.GridView obj = new InapescaWeb.Entidades.GridView();
                                        obj.Comisionado = dplPersonal.SelectedItem.Text;
                                        obj.RFC = dplPersonal.SelectedValue;
                                        obj.Adscripcion = dplProyectos.SelectedItem.Text;
                                        obj.Lugar = txtCantDias.Text;
                                        Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(ubica);
                                        obj.Ubicacion = oUbicacion.Dependencia + "-" + oUbicacion.Desc_Corto;
                                        obj.Rol = dplMes.SelectedItem.Text;
                                        obj.Puesto = dplConceptoGasto.SelectedItem.Text;
                                        ListaGrid.Add(obj);


                                        Obtener_TablaActualizada(ListaGrid);
                                        dplUnidadAdministrativa.Enabled = false;
                                        dplProyectos.Enabled = false;
                                        dplMes.Enabled = false;
                                        dplConceptoGasto.Enabled = false;
                                        txtCantDias.Text = "0.0";
                                        ListaGrid = null;
                                        oUbicacion = null;
                                        //dplConceptoGasto.Items.Clear();
                                        //dplConceptoGasto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
                                        //dplConceptoGasto.Items.Add(new ListItem("VIATICOS", "1"));
                                        //dplConceptoGasto.Items.Add(new ListItem("SINGLADURAS", "2"));
                                        //dplConceptoGasto.AppendDataBoundItems = true;
                                        //dplConceptoGasto.DataBind();
                                    }
                                }
                            }
                        }
                        
                            

                                
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El Usuario : " + dplPersonal.SelectedItem.ToString() + ", ya cuenta con registros guardados para este Proyecto en el mes y concepto seleccionado');", true);
                    return;
                }
            }//termina else

        }
        public void Obtener_TablaActualizada(List<Entidades.GridView> plLista)
        {
            gvComisionados.DataSource = plLista;
            gvComisionados.DataBind();

        }
        protected void dplUnidadAdministrativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsUnidad = dplUnidadAdministrativa.SelectedValue.ToString();

            if ((lsUnidad == null) | (lsUnidad == Dictionary.CADENA_NULA) |(lsUnidad == string.Empty))
            {
                dplPersonal.Items.Clear();
                dplProyectos.Items.Clear();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una Unidad Administrativa para poder avanzar');", true);
                return;
                

            }
            else
            {
                if (Session["Crip_Rol"].ToString() != Dictionary.DIRECTOR_ADJUNTO)
                {
                    dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(lsUnidad);
                    dplPersonal.DataTextField = "Descripcion";
                    dplPersonal.DataValueField = "Codigo";
                    dplPersonal.DataBind();

                    dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), Session["Crip_Ubicacion"].ToString());
                    dplProyectos.DataTextField = "Descripcion";
                    dplProyectos.DataValueField = "Codigo";
                    dplProyectos.DataBind();


                }
                else 
                {
                        dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), lsUnidad, Session["Crip_Ubicacion"].ToString());
                        dplProyectos.DataTextField = "Descripcion";
                        dplProyectos.DataValueField = "Codigo";
                        dplProyectos.DataBind();
                    
                }
                

                
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Boolean Resultado=false;
            List<Entidades.GridView> listaGrid = new List<Entidades.GridView>();

            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
            {
                Entidades.GridView gv = new Entidades.GridView();
                gv.Comisionado = r.Cells[0].Text.ToString();
                gv.RFC = r.Cells[1].Text.ToString();
                gv.Adscripcion = r.Cells[2].Text.ToString();
                gv.Lugar = r.Cells[3].Text.ToString();
                gv.Ubicacion = r.Cells[4].Text.ToString();
                gv.Rol = r.Cells[5].Text.ToString();
                gv.Puesto = r.Cells[6].Text.ToString();
                listaGrid.Add(gv);
            }
            int x = 0;
            foreach (System.Web.UI.WebControls.GridViewRow r in gvComisionados.Rows)
            {
                
                Resultado = false;
                    
                    string usuario = r.Cells[1].Text.ToString();
                    string CadenaProyUbic = dplProyectos.SelectedValue;
                    string[] CadProy = CadenaProyUbic.Split('|');
                    string Proyecto= CadProy[0];
                    double CantDiasViat = Convert.ToDouble(r.Cells[3].Text.ToString());
                    string Ubicacion_Usu;
                    if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADJUNTO)
                    {
                        Usuario ObjUsuario = MngNegocioUsuarios.DatosComisionado1(usuario,year);

                        Ubicacion_Usu = ObjUsuario.Ubicacion; 
                    }
                    else 
                    {
                        Ubicacion_Usu = dplUnidadAdministrativa.SelectedValue;
                    }
                    
                    string DepProy = CadProy[1];
                    string Rol = MngNegocioUsuarios.Obtiene_Rol(r.Cells[1].Text.ToString());
                    
                    if (r.Cells[6].Text.ToString() == Dictionary.dplConceptoGasto1)
                    {
                        if (Rol == "INVEST")
                        {
                            lsZona = Dictionary.ZONA_INVESTIGADOR;
                        }
                        else
                        {
                            lsZona = Dictionary.ZONA_MANDOS_MEDIOS;
                        }

                    }
                    else if (r.Cells[6].Text.ToString() == Dictionary.dplConceptoGasto2)
                    {
                       
                          lsZona = Dictionary.ZONA_SINGLADURAS;
                          
                       
                    }
                    TarifaDiaria = MngNegocioZona.obtieneTarifa(lsZona);
                    
                    double TotalViat = TarifaDiaria * CantDiasViat;
                    string Clv_Mes = dplMes.SelectedValue.ToString();
                   
                    Resultado = MngNegocioProyeccionViaticos.Insert_Proyecion_Viaticos(usuario, Proyecto, DepProy, Ubicacion_Usu, CantDiasViat, Rol, lsZona, TotalViat, Session["Crip_Usuario"].ToString(), Clv_Mes);
                    if (Resultado == true)
                    {
                        x++;
                    }
                  
            
            }
            if (listaGrid.LongCount()== x)
            {
                gvComisionados.DataBind();
                txtCantDias.Text = "0.0";
                if (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO || Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR || (Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO && Session["Crip_Ubicacion"].ToString() == Dictionary.SRF))
                {
                    dplUnidadAdministrativa.DataSource = MngNegocioDependencia.ObtieneCentro1(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString());
                    dplUnidadAdministrativa.DataTextField = "Descripcion";
                    dplUnidadAdministrativa.DataValueField = "Codigo";
                    dplUnidadAdministrativa.DataBind();
                    dplUnidadAdministrativa.Enabled = false;

                    if (Session["Crip_Rol"].ToString() == Dictionary.SUBDIRECTOR_ADJUNTO && Session["Crip_Ubicacion"].ToString() == Dictionary.SRF)
                    {
                        dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Dictionary.DIRECTOR_ADMINISTRACION, Dictionary.DGAA);
                        dplProyectos.DataTextField = "Descripcion";
                        dplProyectos.DataValueField = "Codigo";
                        dplProyectos.DataBind();
                        dplProyectos.Enabled = true;
                    }
                    else
                    {
                        dplProyectos.DataSource = MngNegocioProyecto.ObtieneProyectos(Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString(), Session["Crip_Ubicacion"].ToString());
                        dplProyectos.DataTextField = "Descripcion";
                        dplProyectos.DataValueField = "Codigo";
                        dplProyectos.DataBind();
                        dplProyectos.Enabled = true;
                    }


                    dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(Session["Crip_Ubicacion"].ToString());
                    dplPersonal.DataTextField = "Descripcion";
                    dplPersonal.DataValueField = "Codigo";
                    dplPersonal.DataBind();
                    string[] Mes = lsHoy.Split('-');


                    dplMes.DataSource = MngNegocioProyeccionViaticos.Obtener_Lista_Meses(Mes[1]);
                    dplMes.DataTextField = "Descripcion";
                    dplMes.DataValueField = "Codigo";
                    dplMes.DataBind();
                    dplMes.Enabled = true;
                }
                else
                {


                    if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADJUNTO)
                    {
                        if (Session["Crip_Ubicacion"].ToString() == Dictionary.DGAIPP)
                        {
                            dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneProyectosSustantivos("3", Session["Crip_Ubicacion"].ToString(), "1");
                            dplUnidadAdministrativa.DataTextField = "Descripcion";
                            dplUnidadAdministrativa.DataValueField = "Codigo";
                            dplUnidadAdministrativa.DataBind();
                            dplUnidadAdministrativa.Enabled = true;

                            dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(Session["Crip_Ubicacion"].ToString());
                            dplPersonal.DataTextField = "Descripcion";
                            dplPersonal.DataValueField = "Codigo";
                            dplPersonal.DataBind();
                        }
                        else if (Session["Crip_Ubicacion"].ToString() == Dictionary.DGAIPA)
                        {
                            dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneProyectosSustantivos("3", Session["Crip_Ubicacion"].ToString(), "3");
                            dplUnidadAdministrativa.DataTextField = "Descripcion";
                            dplUnidadAdministrativa.DataValueField = "Codigo";
                            dplUnidadAdministrativa.DataBind();
                            dplUnidadAdministrativa.Enabled = true;

                            dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia(Session["Crip_Ubicacion"].ToString());
                            dplPersonal.DataTextField = "Descripcion";
                            dplPersonal.DataValueField = "Codigo";
                            dplPersonal.DataBind();
                        }
                        else
                        {
                            dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneProyectosSustantivos("3", Session["Crip_Ubicacion"].ToString(), "1");
                            dplUnidadAdministrativa.DataTextField = "Descripcion";
                            dplUnidadAdministrativa.DataValueField = "Codigo";
                            dplUnidadAdministrativa.DataBind();
                            dplUnidadAdministrativa.Enabled = true;

                            dplPersonal.DataSource = MngNegocioUsuarios.ListaUsuariosDependencia2(Session["Crip_Ubicacion"].ToString());
                            dplPersonal.DataTextField = "Descripcion";
                            dplPersonal.DataValueField = "Codigo";
                            dplPersonal.DataBind();

                        }



                        string[] Mes = lsHoy.Split('-');


                        dplMes.DataSource = MngNegocioProyeccionViaticos.Obtener_Lista_Meses(Mes[1]);
                        dplMes.DataTextField = "Descripcion";
                        dplMes.DataValueField = "Codigo";
                        dplMes.DataBind();
                        dplMes.Enabled = true;
                        dplProyectos.Items.Clear();
                        dplProyectos.Enabled = true;
                    }
                    else
                    {
                        dplUnidadAdministrativa.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                        dplUnidadAdministrativa.DataTextField = "Descripcion";
                        dplUnidadAdministrativa.DataValueField = "Codigo";
                        dplUnidadAdministrativa.DataBind();
                        dplUnidadAdministrativa.Enabled = true;
                        dplPersonal.Items.Clear();
                        dplProyectos.Items.Clear();
                        dplProyectos.Enabled = true;
                        string[] Mes = lsHoy.Split('-');


                        dplMes.DataSource = MngNegocioProyeccionViaticos.Obtener_Lista_Meses(Mes[1]);
                        dplMes.DataTextField = "Descripcion";
                        dplMes.DataValueField = "Codigo";
                        dplMes.DataBind();
                        dplMes.Enabled = true;
                    }
                }
                dplConceptoGasto.Items.Clear();
                dplConceptoGasto.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
                dplConceptoGasto.Items.Add(new ListItem("VIATICOS", "1"));
                //dplConceptoGasto.Items.Add(new ListItem("SINGLADURAS", "2"));
                dplConceptoGasto.AppendDataBoundItems = true;
                dplConceptoGasto.DataBind();
                dplConceptoGasto.Enabled = true;

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Los registros se Guardaron correctamente');", true);
                return;


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Algo salio mal al Guardar los Registros');", true);
                return;
            }
            
            

        }
    }
}