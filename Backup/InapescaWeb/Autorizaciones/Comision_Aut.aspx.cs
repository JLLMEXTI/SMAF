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

namespace InapescaWeb.Autorizaciones
{
    public partial class Comision_Aut : System.Web.UI.Page
    {
        static CultureInfo culture = new CultureInfo("es-MX");
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string lsSession = Session["Crip_Rol"].ToString();

                if ((lsSession != Dictionary.INVESTIGADOR) & (lsSession != Dictionary.ENLACE))
                {
                    //Carga_Session();
                    Carga_Valores();

                    clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
                 //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidas, true);
                    Crear_Tabla(MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL));
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No cuenta con los permisos nesesarios para realizar esta accion.');", true);
                    Response.Redirect("../Home/Home.aspx", true);
                }
            }
        }

        public void Crear_Tabla(string psPermisos)
        {
            if (psPermisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL)
            {
                clsFuncionesGral.Activa_Paneles(pnlAutoriza, true);
                dplAutoriza.DataSource = MngNegocioComision.Obtiene_Solicitudes(Dictionary.AUTORIZA, Session["Crip_Usuario"].ToString(), "1");
                dplAutoriza.DataTextField = Dictionary.DESCRIPCION;
                dplAutoriza.DataValueField = Dictionary.CODIGO;
                dplAutoriza.DataBind();

                clsFuncionesGral.Activa_Paneles(pnlVobo, false);
                dplVobo.Items.Clear();

            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlAutoriza, true);
                dplAutoriza.DataSource = MngNegocioComision.Obtiene_Solicitudes(Dictionary.AUTORIZA, Session["Crip_Usuario"].ToString(), "1");
                dplAutoriza.DataTextField = Dictionary.DESCRIPCION;
                dplAutoriza.DataValueField = Dictionary.CODIGO;
                dplAutoriza.DataBind();


                clsFuncionesGral.Activa_Paneles(pnlVobo, true);
                dplVobo.DataSource = MngNegocioComision.Obtiene_Solicitudes(Dictionary.VOBO, Session["Crip_Usuario"].ToString(), "8");
                dplVobo.DataTextField = Dictionary.DESCRIPCION;
                dplVobo.DataValueField = Dictionary.CODIGO;
                dplVobo.DataBind();

            }
        }

        public void Destructor()
        {

        }

        public void Carga_Valores()
        {
            clsFuncionesGral.Activa_Paneles(pnlDetalle, false);
            clsFuncionesGral.Activa_Paneles(pnlValesComb, false);
            clsFuncionesGral.Activa_Paneles(Panel4, false);
            clsFuncionesGral.Activa_Paneles(pnlValesComb, false);
            clsFuncionesGral.Activa_Paneles(pnlCombustEfe, false);
            clsFuncionesGral.Activa_Paneles(pnlVehiculo, false);

            LblCombusEfe.Text = clsFuncionesGral.ConvertMayus("COmbustible en efectivo : $ ");
            lblCOmbusVales.Text = clsFuncionesGral.ConvertMayus("Combustible en vales  :  $ ");

            lblTitle.Text = "Autorización de Comisiónes";
            Label53.Text = clsFuncionesGral.ConvertMayus("Usuario Solicitante - Folio de Solicitud");
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            if (MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL) == Dictionary.PERMISO_ADMINISTRADOR_LOCAL) Label1.Text = clsFuncionesGral.ConvertMayus("Comisiones a Autorizar/MInistrar :");
            else Label1.Text = clsFuncionesGral.ConvertMayus("Comisiones a Autorizar :");

            chkMedioDia.Text = clsFuncionesGral.ConvertMayus("Agregar Medio Dia");
            Label32.Text = clsFuncionesGral.ConvertMayus(Dictionary.MEDIO_TRANSPORTE + " Autorizado : ");

            Label59.Text = clsFuncionesGral.ConvertMayus("OBservaciones de Autorizador");
            Label2.Text = clsFuncionesGral.ConvertMayus("Comisiones a Ministrar : ");
            Label9.Text = clsFuncionesGral.ConvertMayus("Solicitud de Comisión Número :  ");
            Label10.Text = clsFuncionesGral.ConvertMayus("Fecha de Solicitud:  ");
            Label19.Text = clsFuncionesGral.ConvertMayus("Unidad Administrativa solicitante  :   ");
            Label11.Text = clsFuncionesGral.ConvertMayus("Usuario Solicitante :");
            Label12.Text = clsFuncionesGral.ConvertMayus("Programa Radicacion de Recurso  :");
            lblObsrSol.Text = clsFuncionesGral.ConvertMayus("Observaciones de solicitud :");
            Label13.Text = clsFuncionesGral.ConvertMayus("Destino Comisión: ");
            Label14.Text = clsFuncionesGral.ConvertMayus("Objetivo de la comisión : ");
            Label61.Text = "Producto(s) Institucional(es)";
            Label62.Text = clsFuncionesGral.ConvertMayus("Recurso Pesquero:");
            Label15.Text = clsFuncionesGral.ConvertMayus("PERIODO SOLICITADO : ");
            Label7.Text = clsFuncionesGral.ConvertMayus("Periodo autorizado Inicial :");
            Label16.Text = clsFuncionesGral.ConvertMayus("Periodo Autorizado Final :");
            Label20.Text = clsFuncionesGral.ConvertMayus("Días Solicitados : ");
            Label21.Text = clsFuncionesGral.ConvertMayus("Días Autorizados : ");
            Label22.Text = clsFuncionesGral.ConvertMayus("Zona de Comision :  ");
            Label23.Text = clsFuncionesGral.ConvertMayus("Forma de Pago  :");
            Label17.Text = clsFuncionesGral.ConvertMayus("Tipo de viaticos :");
            Label18.Text = clsFuncionesGral.ConvertMayus("Tipo de Pago :");

            Label240.Text = clsFuncionesGral.ConvertMayus("Dias Rural a pagar :");
            Label241.Text = clsFuncionesGral.ConvertMayus("Dias Comprobables a pagar :");
            Label281.Text = clsFunciones.ConvertMayus("Dias  alimentacion para Personal en campo dentro de los 50 km en el municipio :");

            Label41.Text = clsFuncionesGral.ConvertMayus("Partida Presupuestal viaticos");
            Label24.Text = clsFuncionesGral.ConvertMayus(Dictionary.MEDIO_TRANSPORTE + " Solicitado : ");
            Label25.Text = clsFuncionesGral.ConvertMayus("Observaciones de Transporte :");
            Label43.Text = clsFuncionesGral.ConvertMayus("Recorrido Terrestre :");
            Label44.Text = clsFuncionesGral.ConvertMayus("Recorrido Aereo :");
            Label45.Text = clsFuncionesGral.ConvertMayus("Recorrido Acuatico :");
            Label35.Text = clsFuncionesGral.ConvertMayus("Combustible Solicitado");
            Label36.Text = clsFuncionesGral.ConvertMayus("Combustible Autorizado ");
            Label26.Text = clsFuncionesGral.ConvertMayus("Pago de Combustibles:");
            Label37.Text = clsFuncionesGral.ConvertMayus("Folio Vale Inicial : ");
            Label38.Text = clsFuncionesGral.ConvertMayus("FOlio Vale Final : ");
            lblPasajes.Text = clsFuncionesGral.ConvertMayus("Cuota Pasajes : ");
            lblPartidaPasaje.Text = clsFuncionesGral.ConvertMayus("Partida Pasaje :");
            lblPeaje.Text = clsFuncionesGral.ConvertMayus("Cuota Peaje :");
            lblPartidaPeaje.Text = clsFuncionesGral.ConvertMayus("Partida Peaje :");
            Label51.Text = clsFuncionesGral.ConvertMayus("Hora Salida Comisión : ");
            Label52.Text = clsFuncionesGral.ConvertMayus("Hora de regreso Comisión :");
            Label42.Text = clsFuncionesGral.ConvertMayus("Observaciones :");
            lblPartCombus.Text = clsFuncionesGral.ConvertMayus("Partida Combustibles :");

            chkTerrestre.Text = clsFuncionesGral.ConvertMayus("Modificar Vehiculo Oficial ");
            chkTerrestre.Checked = false;
            Label27.Text = clsFuncionesGral.ConvertMayus("Datos Transporte Vehiculo Oficial :");
            Label33.Text = clsFuncionesGral.ConvertMayus("Clase Transporte Autorizado : ");
            Label34.Text = clsFuncionesGral.ConvertMayus("Tipo Transporte Autorizado : ");
            Label31.Text = clsFuncionesGral.ConvertMayus("Vehiculo :");

            btnCancelar.Text = clsFuncionesGral.ConvertMayus("cancelar SOLICITUD DE COMISION");

            chkAgregarUsurios.Checked = false;
            chkAgregarUsurios.Text = clsFuncionesGral.ConvertMayus("Agregar Comisionados");
            Label50.Text = clsFuncionesGral.ConvertMayus("Comisionados locales :");
            lblComisionado.Text = clsFuncionesGral.ConvertMayus("Comisionado");
            lblRFC.Text = clsFuncionesGral.ConvertMayus("Usuario");

            txtDiasRural.Text = Dictionary.NUMERO_CERO;
            txtDiasComercial.Text = Dictionary.NUMERO_CERO;
            txt50km.Text = Dictionary.NUMERO_CERO;

            clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);
            chkModifi.Text = clsFuncionesGral.ConvertMayus("MODIFICAR DIAS A PAGAR");
            txtDiasTotalaPagar.Enabled = false;

        }

        protected void chkAgregarUsurios_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgregarUsurios.Checked)
            {
                Panel4.Visible = true;
                dplComisionados.DataSource = MngNegocioUsuarios.MngBussinesUssers(Session["Crip_Ubicacion"].ToString(), Session["Crip_Usuario"].ToString(), Session["Crip_Rol"].ToString());
                dplComisionados.DataTextField = Dictionary.DESCRIPCION;
                dplComisionados.DataValueField = Dictionary.CODIGO;
                dplComisionados.DataBind();
            }
            else
            {
                dplComisionados.Items.Clear();
                Panel4.Visible = false;
            }
        }

        public void Crea_ListaComisionados(string psFolio, string psOpcion, string psUsuario, string psEstatus, string psTipoCom = "", string psUbicacion = "")
        {
            GridView1.DataSource = MngNegocioComision.Comisionados(psFolio, psOpcion, psUsuario, psEstatus, psTipoCom, psUbicacion);
            GridView1.DataBind();
        }

        //por aca
        protected void dplComisionados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsCadena;
            string lsEstatus;
            bool lbBandera = true; ;

            if ((dplComisionados.SelectedValue != null) | (dplComisionados.SelectedValue != Dictionary.CADENA_NULA) | (dplComisionados.SelectedValue != ""))
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    lsCadena = row.Cells[1].Text;
                    if (lsCadena == dplComisionados.SelectedValue.ToString())
                    {
                        lbBandera = true;
                        break;
                    }
                    else
                    {
                        lbBandera = false;

                    }
                }


                if (lbBandera)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El usuario ya se encuentra agregado para esta comision!!!');", true);
                }
                else
                {
                    string ubica;
                    ubica = Session["Crip_Ubicacion"].ToString();

                  //  List<Entidades.GridView> comisionesSincomprobar = MngNegocioComision.Comisiones_Recurso(dplComisionados.SelectedValue.ToString(),ubica , "'9'");

                    string[] Vobo_Aut = new string[2];
                    bool x = false;

                    string lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(dplComisionados.SelectedValue.ToString(), "VIAT");
                    string configuracion_comprobaciones = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(Session["Crip_Ubicacion"].ToString(), "COMPROBACIONES");
                    string ultimaFechaComision = MngNegocioComision.Obtiene_Max_Comision_Comprobar(dplComisionados.SelectedValue.ToString());


                    if ((ultimaFechaComision == "") | (ultimaFechaComision == null))
                    {
                        x = true;
                        /*if ((comisionesSincomprobar.Count != 0))
                       {
                            if ((MngNegocioUsuarios.Obtiene_Rol(dplComisionados.SelectedValue.ToString())) != Dictionary.DIRECTOR_GRAL)
                            {
                                x = false;

                                if ((lsPermisosEspeciales == "VIAT"))
                                {
                                    x = true;
                                }
                                else if ((configuracion_comprobaciones == null) | (configuracion_comprobaciones == ""))
                                {
                                    x = false;
                                }
                                else
                                {
                                    x = true;
                                }

                            }
                            else
                            {
                                x = true;
                            }
                        //}
                        //else
                        //{
                          //  x = true;
                        }*/
                    }
                    else
                    {
                        if ((Convert.ToDateTime(lsHoy) > Convert.ToDateTime(Convert.ToDateTime(ultimaFechaComision).AddDays(10))))// | (comisionesSincomprobar.Count != 0))
                        {
                            if (((MngNegocioUsuarios.Obtiene_Rol(dplComisionados.SelectedValue.ToString())) != Dictionary.DIRECTOR_GRAL)| (MngNegocioUsuarios.Obtiene_Rol(dplComisionados.SelectedValue.ToString()) != Dictionary.DIRECTOR_JURIDICO )| (MngNegocioUsuarios.Obtiene_Rol(dplComisionados.SelectedValue.ToString()) != Dictionary.DIRECTOR_ADMINISTRACION )|(MngNegocioUsuarios.Obtiene_Rol(dplComisionados.SelectedValue.ToString()) != Dictionary.DIRECTOR_ADJUNTO ))
                            {
                                x = false;

                                if ((lsPermisosEspeciales == "VIAT"))
                                {
                                    x = true;
                                }
                                else if ((configuracion_comprobaciones == null) | (configuracion_comprobaciones == ""))
                                {
                                    x = false;
                                }
                                else
                                {
                                    x = true;
                                }

                            }
                            else
                            {
                                x = true;
                            }
                        }
                      //  else
                       // {
                        //    x = true;
                        //}
                    }

                    if (!x)
                    {
                        if (Convert.ToDateTime(lsHoy) > Convert.ToDateTime(ultimaFechaComision).AddDays(10))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplComisionados.SelectedItem.Text + "\\n tiene una comprobacion de comsion pendiente con fecha de terminacion de : " + clsFuncionesGral.FormatFecha(ultimaFechaComision) + "y que excede los 10 dias de tolerancia para comprobar \\n Por lo que no puede aplicar para esta solicitud \\n Deberá realizar la comprobacion o en su caso solicitar un permiso especial al area de Administracion' );", true);
                            return;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplComisionados.SelectedItem.Text + "\\n tiene una comprobacion de comsion pendiente\\n por lo que no puede aplicar para esta solicitud \\n Deberá de realizar la comprobacion o en su caso solicitar un permiso especial al area de Administracion' );", true);
                            return;
                        }

                    }
                    else
                    {
                        double totalDias = clsFuncionesGral.Convert_Double(MngNegocioComision.Dias_Acumulados(dplComisionados.SelectedValue.ToString(), year));

                        if (totalDias >= clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(Dictionary.DIAS_ANUALES)))
                        {
                            //extrae permisos de ofial mayor
                            lsPermisosEspeciales = MngNegocioPermisos.ObtienePermisos(dplComisionados.SelectedValue.ToString(), "OFMAY");
                           
                             if (lsPermisosEspeciales != "OFMAY")
                             {
                                 ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El comisionado : " + dplComisionados.SelectedValue.ToString() + "\\n Ha excedido el total de dias anuales permitidos por la norma de viaticos vigente\\n por lo que no puede aplicar para esta solicitud \\n solicitar un permiso especial al area de Administracion' );", true);
                                 return;
                             }
                             else
                             {
                                 //agregar a comision y a datagrid
                                 string max_comision = MngNegocioComision.Obtiene_SecEff_Comision(Session["Crip_Folio"].ToString());
                                 Usuario objUsuario = MngNegocioUsuarios.Obten_Datos(dplComisionados.SelectedValue.ToString(), true);
                                 Vobo_Aut = clsFuncionesGral.Obtiene_Jerarquico(objUsuario);
                                 Comision detalleComision = new Comision();
                                 if ((Session["Crip_Opcion"].ToString() == Dictionary.VOBO) | (MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL) == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))
                                 {
                                     lsEstatus = "8";
                                 }
                                 else
                                 {
                                     lsEstatus = "1";
                                 }

                                 if (Session["Crip_Opcion"].ToString() == Dictionary.AUTORIZA)
                                 {
                                     detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "");
                                 }
                                 else
                                 {
                                     detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "8");
                                 }

                                 if (detalleComision.Tipo_Comision == "3")
                                 {
                                     Vobo_Aut[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.SUBDIRECTOR_ADJUNTO);
                                     Vobo_Aut[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION);
                                 }

                                 string Ubicacion_Auoriza = "";

                                 string rol_comisionado = MngNegocioUsuarios.Obtiene_Rol(objUsuario.Usser);
                                 Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(objUsuario.Ubicacion);

                                 if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION))
                                 {
                                     Ubicacion_Auoriza = oDireccionTipo.Codigo;
                                 }
                                 else if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO)) && ((oDireccionTipo.Descripcion == Dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JURIDICA)))
                                 {
                                     Ubicacion_Auoriza = oDireccionTipo.Codigo;
                                 }
                                 else if (((rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.INVESTIGADOR)) && ((oDireccionTipo.Descripcion == Dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JURIDICA) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JEFE)))
                                 {
                                     Ubicacion_Auoriza = Session["Crip_Ubicacion"].ToString();
                                 }
                                 else if ((rol_comisionado == Dictionary.INVESTIGADOR) && (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION))
                                 {
                                     Ubicacion_Auoriza = Session["Crip_Ubicacion"].ToString();
                                 }

                                 MngNegocioComision.Inserta_Comision("CRIPSC01", "01", Convert.ToString(Session["Crip_Folio"].ToString()), Dictionary.NUMERO_CERO, clsFuncionesGral.FormatFecha(lsHoy), Dictionary.FECHA_NULA, detalleComision.Fecha_Vobo, clsFuncionesGral.FormatFecha(lsHoy), detalleComision.Usuario_Solicita, detalleComision.Ubicacion, detalleComision.Area, detalleComision.Proyecto, detalleComision.Dep_Proy, clsFuncionesGral.ConvertMayus(detalleComision.Lugar), Dictionary.NUMERO_CERO, Dictionary.CADENA_NULA, Dictionary.CADENA_NULA, Dictionary.CADENA_NULA, Convert.ToString(detalleComision.Partida_Presupuestal), detalleComision.Fecha_Inicio, detalleComision.Fecha_Final, detalleComision.Dias_Total, Convert.ToString(txtDiasAut.Text), clsFuncionesGral.ConvertMayus(detalleComision.Objetivo), detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Vehiculo_Solicitado, detalleComision.Vehiculo_Autorizado, clsFuncionesGral.ConvertMayus(detalleComision.Descrip_vehiculo), detalleComision.Ubicacion_Transporte, detalleComision.Origen_Terrestre, Dictionary.NUMERO_CERO, Dictionary.FECHA_NULA, Dictionary.HORA_NULA, Dictionary.NUMERO_CERO, Dictionary.FECHA_NULA, Dictionary.HORA_NULA, detalleComision.Origen_Aereo, detalleComision.Combustible_Solicitado, Dictionary.NUMERO_CERO, detalleComision.Partida_Combustible, detalleComision.Peaje, detalleComision.Partida_Peaje, detalleComision.Pasaje, detalleComision.Partida_Pasaje, detalleComision.Origen_Dest_Acu, Convert.ToString(max_comision), clsFuncionesGral.ConvertMayus(detalleComision.Equipo), clsFuncionesGral.ConvertMayus(detalleComision.Observaciones_Solicitud), clsFuncionesGral.ConvertMayus(detalleComision.Responsable_proyecto), Vobo_Aut[0], Vobo_Aut[1], objUsuario.Usser, objUsuario.Ubicacion, lsEstatus, detalleComision.Observaciones_Vobo, detalleComision.Observaciones_Autoriza, detalleComision.Pesqueria, detalleComision.Producto, detalleComision.Actividad, "2", Ubicacion_Auoriza, year);

                                 //Crea_ListaComisionados(Session["Crip_Folio"].ToString(),  Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), Dictionary.COMISION_ORDINARIA,Session["Crip_UbicacionAut"].ToString());


                                 if (Session["Crip_Opcion"].ToString() == Dictionary.AUTORIZA)
                                 {
                                     Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), "1", "", Session["Crip_UbicacionAut"].ToString());
                                 }
                                 else
                                 {
                                     Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), "8", "", Session["Crip_UbicacionAut"].ToString());
                                 }

                             }
                        }
                        else
                        {
                          //  comisionesSincomprobar = null;

                            string maxComision = MngNegocioComision.Obtiene_SecEff_Comision(Session["Crip_Folio"].ToString());

                            Usuario objUsuario = MngNegocioUsuarios.Obten_Datos(dplComisionados.SelectedValue.ToString(), true);

                            Vobo_Aut = clsFuncionesGral.Obtiene_Jerarquico(objUsuario);

                            Comision detalleComision = new Comision();

                            if ((Session["Crip_Opcion"].ToString() == Dictionary.VOBO) | (MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL) == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))
                            {
                                lsEstatus = "8";
                            }
                            else
                            {
                                lsEstatus = "1";
                            }

                            if (Session["Crip_Opcion"].ToString() == Dictionary.AUTORIZA)
                            {
                                detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "");
                            }
                            else
                            {
                                detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "8");
                            }


                            /*       if (detalleComision.Dep_Proy  != )//lsUbicacion
                                   {
                                       //agregar roles 
                                       if (objProyecto.Tipo_Dep == Dictionary .CENTROS_INVESTIGACION)
                                       {
                                           Vobo_Aut[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary .ADMINISTRADOR, objProyecto.Dependencia);
                                       }
                                       else if ((objProyecto.Tipo_Dep == Dictionary .DIRECCIONES_ADJUNTAS) | (objProyecto.Tipo_Dep == Dictionary .COORDINACIONES_INVESTIGACION) | (objProyecto.Tipo_Dep == dictionary.DIRECCION_ADMON) | (objProyecto.Tipo_Dep == dictionary.DIRECCION_JEFE) | (objProyecto.Tipo_Dep == Dictionary .SUBDIRECCIONES_GENERALES) | (objProyecto.Tipo_Dep == Dictionary .DEPARTAMENTOS))
                                       {
                                           // [0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION,MngNegocioUsuarios.Obtiene_Direccion ( objProyecto.Dependencia));
                                           Vobo_Aut[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary .SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(dictionary.PUESTO_FINANCIERO, dictionary.SUBDIRECTOR_ADJUNTO), dictionary.SUBDIRECTOR_ADJUNTO);
                                       }
                                       else if (objProyecto.Tipo_Dep == Dictionary .COMUNICACION)
                                       {

                                       }

                                   }

                              */
                            if (detalleComision.Tipo_Comision == "3")
                            {
                                Vobo_Aut[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.SUBDIRECTOR_ADJUNTO);
                                Vobo_Aut[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION);
                            }

                            string Ubicacion_Auoriza = "";

                            string rol_comisionado = MngNegocioUsuarios.Obtiene_Rol(objUsuario.Usser);
                            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(objUsuario.Ubicacion);

                            if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION))
                            {
                                Ubicacion_Auoriza = oDireccionTipo.Codigo;
                            }
                            else if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO)) && ((oDireccionTipo.Descripcion == Dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JURIDICA)))
                            {
                                Ubicacion_Auoriza = oDireccionTipo.Codigo;
                            }
                            else if (((rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.INVESTIGADOR)) && ((oDireccionTipo.Descripcion == Dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JURIDICA) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JEFE)))
                            {
                                Ubicacion_Auoriza = Session["Crip_Ubicacion"].ToString();
                            }
                            else if ((rol_comisionado == Dictionary.INVESTIGADOR) && (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION))
                            {
                                Ubicacion_Auoriza = Session["Crip_Ubicacion"].ToString();
                            }

                            MngNegocioComision.Inserta_Comision("CRIPSC01", "01", Convert.ToString(Session["Crip_Folio"].ToString()), Dictionary.NUMERO_CERO, clsFuncionesGral.FormatFecha(lsHoy), Dictionary.FECHA_NULA, detalleComision.Fecha_Vobo, clsFuncionesGral.FormatFecha(lsHoy), detalleComision.Usuario_Solicita, detalleComision.Ubicacion, detalleComision.Area, detalleComision.Proyecto, detalleComision.Dep_Proy, clsFuncionesGral.ConvertMayus(detalleComision.Lugar), Dictionary.NUMERO_CERO, Dictionary.CADENA_NULA, Dictionary.CADENA_NULA, Dictionary.CADENA_NULA, Convert.ToString(detalleComision.Partida_Presupuestal), detalleComision.Fecha_Inicio, detalleComision.Fecha_Final, detalleComision.Dias_Total, Convert.ToString(txtDiasAut.Text), clsFuncionesGral.ConvertMayus(detalleComision.Objetivo), detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Vehiculo_Solicitado, detalleComision.Vehiculo_Autorizado, clsFuncionesGral.ConvertMayus(detalleComision.Descrip_vehiculo), detalleComision.Ubicacion_Transporte, detalleComision.Origen_Terrestre, Dictionary.NUMERO_CERO, Dictionary.FECHA_NULA, Dictionary.HORA_NULA, Dictionary.NUMERO_CERO, Dictionary.FECHA_NULA, Dictionary.HORA_NULA, detalleComision.Origen_Aereo, detalleComision.Combustible_Solicitado, Dictionary.NUMERO_CERO, detalleComision.Partida_Combustible, detalleComision.Peaje, detalleComision.Partida_Peaje, detalleComision.Pasaje, detalleComision.Partida_Pasaje, detalleComision.Origen_Dest_Acu, Convert.ToString(maxComision), clsFuncionesGral.ConvertMayus(detalleComision.Equipo), clsFuncionesGral.ConvertMayus(detalleComision.Observaciones_Solicitud), clsFuncionesGral.ConvertMayus(detalleComision.Responsable_proyecto), Vobo_Aut[0], Vobo_Aut[1], objUsuario.Usser, objUsuario.Ubicacion, lsEstatus, detalleComision.Observaciones_Vobo, detalleComision.Observaciones_Autoriza, detalleComision.Pesqueria, detalleComision.Producto, detalleComision.Actividad, "2", Ubicacion_Auoriza, year);

                            //Crea_ListaComisionados(Session["Crip_Folio"].ToString(),  Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), Dictionary.COMISION_ORDINARIA,Session["Crip_UbicacionAut"].ToString());


                            if (Session["Crip_Opcion"].ToString() == Dictionary.AUTORIZA)
                            {
                                Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), "1", "", Session["Crip_UbicacionAut"].ToString());
                            }
                            else
                            {
                                Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), "8", "", Session["Crip_UbicacionAut"].ToString());
                            }
                        }
                    }

                }


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

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsFolioAut = dplAutoriza.SelectedValue.ToString();

            if ((lsFolioAut == null) | (lsFolioAut == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una solicitud para autorizar,\n En caso de no existir alguna en la lista no podra avanza .');", true);
                return;
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlDetalle, true);
                string[] lsCadena;
                lsCadena = lsFolioAut.Split(new Char[] { '|' });
                string lsFoliA = lsCadena[0];
                string lsUbicacionA = lsCadena[1];
                Session["Crip_Folio"] = lsFoliA;
                Session["Crip_Opcion"] = Dictionary.AUTORIZA;
                Session["Crip_UbicacionAut"] = lsUbicacionA;

                Carga_Panel(lsFoliA, lsUbicacionA, Dictionary.AUTORIZA, MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL));
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string lsFolioAut = dplVobo.SelectedValue.ToString();
            if ((lsFolioAut == null) | (lsFolioAut == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una solicitud para ministrar,\n En caso de no existir alguna en la lista no podra avanza .');", true);
                return;
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlDetalle, true);
                string[] lsCadena;
                lsCadena = lsFolioAut.Split(new Char[] { '|' });
                string lsFoliA = lsCadena[0];
                string lsUbicacionA = lsCadena[1];
                Session["Crip_Folio"] = lsFoliA;
                Session["Crip_Opcion"] = Dictionary.VOBO;
                Session["Crip_UbicacionAut"] = lsUbicacionA;

                Carga_Panel(lsFoliA, lsUbicacionA, Dictionary.VOBO, MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL));
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CadenaELiminar = GridView1.Rows[Convert.ToInt32(GridView1.SelectedIndex.ToString())].Cells[1].Text.ToString();
            /*Session["Crip_Folio"] = lsFoliA;
                              Session["Crip_Opcion"] = Dictionary.VOBO;
                              Session["Crip_UbicacionAut"] = lsUbicacionA;*/
            if (GridView1.Rows.Count > 1)
            {
                MngNegocioComision.Update_status(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), CadenaELiminar, Session["Crip_Opcion"].ToString(), clsFuncionesGral.ConvertMayus("baja por administrador"), lsHoy);

                //Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), Dictionary.COMISION_ORDINARIA);

                if (Session["Crip_Opcion"].ToString() == Dictionary.AUTORIZA)
                {
                    Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), "1", "", Session["Crip_UbicacionAut"].ToString());
                }
                else
                {
                    Crea_ListaComisionados(Session["Crip_Folio"].ToString(), Session["Crip_Opcion"].ToString(), Session["Crip_Usuario"].ToString(), "8", "", Session["Crip_UbicacionAut"].ToString());
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede eliminar todos los comisionados');", true);
            }
        }

        public void Carga_Panel(string psFolio, string psUbicacion, string psOpcion, string psPermisosAdmin)
        {
            string lsFechaVobo, lsFechaAut;

            Comision detalleComision = new Comision();
            string[] ListaProductos;
            string[] ListaNewProdductos;
            string[] DatosVehiculo = new string[9];
            string[] DatosVehiculoAut = new string[9];

            if (psOpcion == Dictionary.AUTORIZA)
            {
                detalleComision = MngNegocioComision.Detalle_Comision(psFolio, psUbicacion, "", "");
                btnAccion.Text = "AUTORIZAR";
                Label8.Text = clsFuncionesGral.ConvertMayus("Autorización de comisión número :  ") + detalleComision.Folio;
                lsFechaAut = clsFuncionesGral.FormatFecha(lsHoy);
                detalleComision.Fecha_Autoriza = lsFechaAut;
                //  Session["Detalle_comision"] = detalleComision ;

                if (psPermisosAdmin == Dictionary.PERMISO_ADMINISTRADOR_LOCAL)
                {
                    Session["Inicio_Comision"] = detalleComision.Fecha_Inicio;
                    Session["Fin_Comision"] = detalleComision.Fecha_Final;

                }

                if ((detalleComision.Observaciones_Autoriza == null) | (detalleComision.Observaciones_Autoriza == Dictionary.CADENA_NULA))
                {
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    txtObservaciones.BackColor = Color.Beige;
                    txtObservaciones.Enabled = true;
                }
                else
                {
                    txtObservaciones.Text = detalleComision.Observaciones_Autoriza;
                    txtObservaciones.BackColor = Color.Empty;
                    txtObservaciones.Enabled = false;
                }

            }
            else
            {
                btnAccion.Text = " Ministrar ";
                detalleComision = MngNegocioComision.Detalle_Comision(psFolio, psUbicacion, "", "8");
                Label8.Text = clsFuncionesGral.ConvertMayus("Visto Bueno de comisión número :  ") + detalleComision.Folio;
                lsFechaVobo = clsFuncionesGral.FormatFecha(lsHoy);
                detalleComision.Fecha_Vobo = lsFechaVobo;

                Session["Inicio_Comision"] = detalleComision.Fecha_Inicio;
                Session["Fin_Comision"] = detalleComision.Fecha_Final;


                if ((detalleComision.Observaciones_Vobo == null) | (detalleComision.Observaciones_Vobo == Dictionary.CADENA_NULA))
                {
                    txtObservaciones.Text = Dictionary.CADENA_NULA;
                    txtObservaciones.BackColor = Color.Beige;
                    txtObservaciones.Enabled = true;
                }
                else
                {
                    txtObservaciones.Text = detalleComision.Observaciones_Vobo;
                    txtObservaciones.BackColor = Color.Empty;
                    txtObservaciones.Enabled = false;
                }

            }


            if (psOpcion == Dictionary.AUTORIZA)
            {
                Crea_ListaComisionados(psFolio, psOpcion, Session["Crip_Usuario"].ToString(), "1", "", detalleComision.Ubicacion);
            }
            else
            {
                Crea_ListaComisionados(psFolio, psOpcion, Session["Crip_Usuario"].ToString(), "8", "", detalleComision.Ubicacion);
            }


            Ubicacion oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(detalleComision.Ubicacion_Transporte);

            if (detalleComision.Observaciones_Autoriza != "")
            {
                Label60.Text = detalleComision.Observaciones_Autoriza;
                clsFuncionesGral.Activa_Paneles(Panel1, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(Panel1, false);
            }

            lblNumSolicita.Text = detalleComision.Folio;
            lblFechaSolicitud.Text = clsFuncionesGral.FormatFecha(detalleComision.Fecha_Solicitud);
            lblAdscripcion.Text = MngNegocioDependencia.Centro_Descrip(detalleComision.Ubicacion);
            lblUsuarioSol.Text = clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(detalleComision.Usuario_Solicita));
            lbllugar.Text = detalleComision.Lugar;
            lblObjetivo.Text = detalleComision.Objetivo;
            lblObserSoli.Text = detalleComision.Observaciones_Solicitud;

            txtInicioAut.Text = detalleComision.Fecha_Inicio;
            txtFinAut.Text = detalleComision.Fecha_Final;

            lblDias.Text = detalleComision.Dias_Total;

            if ((detalleComision.Dias_Reales == Dictionary.NUMERO_CERO) | (detalleComision.Dias_Reales == Dictionary.CADENA_NULA) | (detalleComision.Dias_Reales == null))
            {
                double num1 = clsFuncionesGral.Convert_Double(detalleComision.Dias_Total);

                txtDiasAut.Text = clsFuncionesGral.ConvertString(num1 - medio);
                txtDiasAut.Enabled = false;
                txtDiasTotalaPagar.Text = txtDiasAut.Text;
            }
            else
            {
                txtDiasAut.Text = detalleComision.Dias_Reales;
                txtDiasAut.Enabled = false;
                txtDiasTotalaPagar.Text = txtDiasAut.Text;
            }

            //Validacion para proyecto
            Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(detalleComision.Dep_Proy, detalleComision.Proyecto);

            if (objProyecto.Clv_Proy != "000")
            {

                clsFuncionesGral.Activa_Paneles(pnlProyectoExt, false);
                lblProyecto.Text = objProyecto.Descripcion + "  , " + MngNegocioDependencia.Centro_Descrip(detalleComision.Dep_Proy);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlProyectoExt);
                lblProyecto.Text = objProyecto.Descripcion + "  , " + MngNegocioDependencia.Centro_Descrip(detalleComision.Dep_Proy);
                objProyecto = null;
                objProyecto = MngNegocioProyecto.ObtieneDatosProyExt(detalleComision.Folio);
            }

            ListBox1.Items.Clear();
            if (detalleComision.Producto != "00")
            {
                ListaProductos = detalleComision.Producto.Split(new Char[] { '|' });
                ListaNewProdductos = new string[3];

                for (int i = 0; i < ListaProductos.Length; i++)
                {
                    ListaNewProdductos[i] = MngNegocioProductos.Obtiene_Descripcion_Producto(ListaProductos[i],year);
                }

                for (int x = 0; x < ListaNewProdductos.Length; x++)
                {
                    if ((ListaNewProdductos[x] != null) | (ListaNewProdductos[x] != ""))
                    {
                        ListBox1.Items.Add(ListaNewProdductos[x]);
                    }
                }

                //ListBox1.DataSource = ListaNewProdductos;
                ListBox1.DataBind();
            }
            else
            {
                ListBox1.Visible = false;
                Label61.Visible = false;
            }

            if ((detalleComision.Pesqueria == "00") | (detalleComision.Pesqueria == "") | (detalleComision.Pesqueria == null))
            {
                Label62.Visible = false;
                lblEspecie.Visible = false;
            }
            else
            {
                lblEspecie.Text = MngNegocioEspecies.Obtiene_Descripcion_Pesqueria(objProyecto.Programa, detalleComision.Pesqueria, objProyecto.Direccion);
            }

            if (detalleComision.Fecha_Inicio == detalleComision.Fecha_Final)
            {
                lblPeriodo.Text = clsFuncionesGral.FormatFecha(detalleComision.Fecha_Inicio);
            }
            else
            {
                lblPeriodo.Text = "Del " + clsFuncionesGral.FormatFecha(detalleComision.Fecha_Inicio) + "  al  " + clsFuncionesGral.FormatFecha(detalleComision.Fecha_Final);
            }


            if ((detalleComision.Zona_Comercial == null) | (detalleComision.Zona_Comercial == Dictionary.CADENA_NULA) | (detalleComision.Zona_Comercial == Dictionary.NUMERO_CERO))
            {
                txtZonas.Visible = false;
                dplZonas.Visible = true;
                dplZonas.DataSource = MngNegocioViaticos.Obtiene_Zonas();
                dplZonas.DataTextField = Dictionary.DESCRIPCION;
                dplZonas.DataValueField = Dictionary.CODIGO;
                dplZonas.DataBind();
            }
            else
            {
                txtZonas.Visible = true;
                txtZonas.Enabled = false;
                //lsZonaComer = DetalleComision.Zona_Comercial;
                txtZonas.Text = MngNegocioViaticos.Descrip_Zona(detalleComision.Zona_Comercial);
                dplZonas.Visible = false;
                dplZonas.Items.Clear();
            }

            //forma pago viaticos (devengados anticipados)
            if ((detalleComision.Forma_Pago_Viaticos == null) | (detalleComision.Forma_Pago_Viaticos == Dictionary.CADENA_NULA) | (detalleComision.Forma_Pago_Viaticos == Dictionary.NUMERO_CERO))
            {
                txtPagos.Visible = false;
                dplPagos.Visible = true;
                dplPagos.DataSource = MngNegocioViaticos.Metodo_Viaticos(true);
                dplPagos.DataTextField = Dictionary.DESCRIPCION;
                dplPagos.DataValueField = Dictionary.CODIGO;
                dplPagos.DataBind();
            }
            else
            {
                txtPagos.Visible = true;
                txtPagos.Enabled = false;
                txtPagos.Text = MngNegocioViaticos.Descrip_Metodo_Viaticos(detalleComision.Forma_Pago_Viaticos);
                // lsFormaPagoviaticos = DetalleComision.Forma_Pago_Viaticos;
                dplPagos.Visible = false;
                dplPagos.Items.Clear();
            }

            //tipo viaticos (homologados, tarifarios)
            if ((detalleComision.Tipo_Viaticos == null) | (detalleComision.Tipo_Viaticos == Dictionary.CADENA_NULA) | (detalleComision.Tipo_Viaticos == Dictionary.NUMERO_CERO))
            {
                txtTipoViaticos.Visible = false;
                txtTipoViaticos.Enabled = true;
                dplTipoViaticos.Visible = true;
                clsFuncionesGral.Llena_Lista(dplTipoViaticos, clsFuncionesGral.ConvertMayus(" = s e l e c c i o n e= |tarifarios"));

            }
            else
            {
                txtTipoViaticos.Visible = true;
                txtTipoViaticos.Enabled = false;
                if (detalleComision.Tipo_Viaticos == "1") txtTipoViaticos.Text = "HOMOLOGADOS";
                else txtTipoViaticos.Text = "TARIFARIOS";

                dplTipoViaticos.Visible = false;
                dplTipoViaticos.Items.Clear();
            }

            //tipo pagos ( cehque , deposito a cuenta)
            if ((detalleComision.Tipo_Pago_Viatico == null) | (detalleComision.Tipo_Pago_Viatico == Dictionary.CADENA_NULA) | (detalleComision.Tipo_Pago_Viatico == Dictionary.NUMERO_CERO))
            {
                txtTipoPagoViaticos.Visible = false;
                dplTipoPagoViaticos.Visible = true;
                clsFuncionesGral.Llena_Lista(dplTipoPagoViaticos, clsFuncionesGral.ConvertMayus("cheque|deposito a cuenta"));

            }
            else
            {
                txtTipoPagoViaticos.Visible = true;
                txtTipoPagoViaticos.Enabled = false;
                if (detalleComision.Tipo_Pago_Viatico == "1") txtTipoPagoViaticos.Text = "CHEQUE";
                else txtTipoPagoViaticos.Text = "DEPOSITO A CUENTA";

                dplTipoPagoViaticos.Visible = false;
                dplTipoPagoViaticos.Items.Clear();
            }

            if ((detalleComision.Partida_Presupuestal == null) | (detalleComision.Partida_Presupuestal == Dictionary.CADENA_NULA) | (detalleComision.Partida_Presupuestal == Dictionary.NUMERO_CERO))
            {
                txtPartPresupuestal.Text = Dictionary.CADENA_NULA;
                txtPartPresupuestal.BackColor = Color.Red;
            }
            else
            {
                txtPartPresupuestal.Text = detalleComision.Partida_Presupuestal;
                txtPartPresupuestal.BackColor = Color.Empty;
            }

            //Transporte solicitado
            DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Ubicacion_Transporte, detalleComision.Vehiculo_Solicitado);

            for (int j = 0; j < DatosVehiculo.Length; j++)
            {
                if ((j == 2) | (j == 5)) separador = " <br>";
                else separador = "  ";
                lblVehiculo.Text += DatosVehiculo[j] + separador;
            }

            lblVehiculo.Text = lblVehiculo.Text + oUbicacionTrans.Desc_Corto;
            lblObserVehiculo.Text = detalleComision.Descrip_vehiculo;

            oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(detalleComision.Ubicacion_Trans_Aut);
            DatosVehiculoAut = clsFuncionesGral.Genera_Descripcion_Vehiculo(detalleComision.Clase_Aut, detalleComision.Tipo_Aut, detalleComision.Ubicacion_Trans_Aut, detalleComision.Vehiculo_Autorizado);

            separador = string.Empty;
            if (DatosVehiculoAut[0] == null)
            { lblVehiculoAut.Text = Dictionary.CADENA_NULA; }
            else
            {
                for (int j = 0; j < DatosVehiculoAut.Length; j++)
                {
                    if ((j == 2) | (j == 5)) separador = " <br>";
                    else separador = "  ";
                    lblVehiculoAut.Text += DatosVehiculoAut[j] + separador;
                }
                lblVehiculoAut.Text = lblVehiculoAut.Text + oUbicacionTrans.Desc_Corto;
            }

            if ((detalleComision.Tipo_Aut == null) | (detalleComision.Tipo_Aut == Dictionary.CADENA_NULA) | (detalleComision.Tipo_Aut == string.Empty))
            {
                Propertys_Paneles_Transporte(psOpcion, detalleComision.Tipo_Trans, psPermisosAdmin);
            }
            else
            {
                Propertys_Paneles_Transporte(psOpcion, detalleComision.Tipo_Aut, psPermisosAdmin);
            }

            txtOrigen_Ter.Text = detalleComision.Origen_Terrestre;
            txtOrigen_Aer.Text = detalleComision.Origen_Aereo;
            txtOrigen_Des_Acu.Text = detalleComision.Origen_Dest_Acu;

            txtCombustSol.Text = detalleComision.Combustible_Solicitado;
            txtCombusAut.Text = detalleComision.Combustible_Autorizado;
            txtPartCombust.Text = detalleComision.Partida_Combustible;

            //ACA COM
            string configuracion_combustible;
            /*= MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objProyecto.Dependencia, "COMBUSTIBLES");
            Session["Crip_Configuracion_Combustibles"] = configuracion_combustible;
            */
            if ((detalleComision.Pago_Combustible == null) | (detalleComision.Pago_Combustible == Dictionary.CADENA_NULA) | (detalleComision.Pago_Combustible == " "))
            {
                bool combEfe = false;

                txtPagoCombustibles.Text = Dictionary.CADENA_NULA;
                txtPagoCombustibles.Visible = false;
                dplPagoCombustibles.Visible = true;

                foreach (System.Web.UI.WebControls.GridViewRow r in GridView1.Rows)
                {
                    string usuario = r.Cells[1].Text.ToString();
                    Usuario ousuario = MngNegocioUsuarios.Obten_Datos(usuario, true);

                    configuracion_combustible = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(ousuario.Ubicacion, "COMBUSTIBLES");
                    Session["Crip_Configuracion_Combustibles"] = configuracion_combustible;

                    if (ousuario.Ubicacion == Session["Crip_Configuracion_Combustibles"].ToString())
                    {
                        dplPagoCombustibles.DataSource = MngNegocioConfiguraciones.Lista_Configuraciones(ousuario.Ubicacion, "COMBUSTIBLES");
                        dplPagoCombustibles.DataTextField = Dictionary.DESCRIPCION;
                        dplPagoCombustibles.DataValueField = Dictionary.CODIGO;
                        dplPagoCombustibles.DataBind();

                        break;
                    }
                    else
                    {
                        configuracion_combustible = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objProyecto.Dependencia, "COMBUSTIBLES");
                        Session["Crip_Configuracion_Combustibles"] = configuracion_combustible;

                        if ((objProyecto.Dependencia == configuracion_combustible))
                        {
                            dplPagoCombustibles.DataSource = MngNegocioConfiguraciones.Lista_Configuraciones(objProyecto.Dependencia, "COMBUSTIBLES");
                            dplPagoCombustibles.DataTextField = Dictionary.DESCRIPCION;
                            dplPagoCombustibles.DataValueField = Dictionary.CODIGO;
                            dplPagoCombustibles.DataBind();
                            break;
                        }
                        else
                        {
                            clsFuncionesGral.Llena_Lista(dplPagoCombustibles, clsFuncionesGral.ConvertMayus("= S E L E C C I O N E =|Vales"));
                            break;
                        }
                    }
                }

            }
            else
            {
                foreach (System.Web.UI.WebControls.GridViewRow r in GridView1.Rows)
                {
                    string usuario = r.Cells[1].Text.ToString();
                    Usuario ousuario = MngNegocioUsuarios.Obten_Datos(usuario, true);

                    configuracion_combustible = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(ousuario.Ubicacion, "COMBUSTIBLES");
                    Session["Crip_Configuracion_Combustibles"] = configuracion_combustible;

                    if (ousuario.Ubicacion == Session["Crip_Configuracion_Combustibles"].ToString())
                    {

                        if ((detalleComision.Pago_Combustible == "1"))
                        {
                            txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("Deposito a cuenta");
                            pnlValesComb.Visible = false;
                            clsFuncionesGral.Activa_Paneles(pnlCombustEfe, true);
                        }
                        else if (detalleComision.Pago_Combustible == "2")
                        {
                            txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("Vales");
                            pnlValesComb.Visible = true;
                            txtValeI.Text = detalleComision.Vale_Comb_I;
                            txtValeF.Text = detalleComision.Vale_Comb_F;

                            clsFuncionesGral.Activa_Paneles(pnlCombustEfe, false);
                            clsFuncionesGral.Activa_Paneles(pnlValesComb, true);
                        }
                        else if (detalleComision.Pago_Combustible == "3")
                        {
                            txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("vales y efectivo");
                            pnlValesComb.Visible = false;
                        }

                        break;
                    }
                    else
                    {
                        configuracion_combustible = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objProyecto.Dependencia, "COMBUSTIBLES");
                        Session["Crip_Configuracion_Combustibles"] = configuracion_combustible;

                        if ((objProyecto.Dependencia == configuracion_combustible))
                        {
                            if ((detalleComision.Pago_Combustible == "1"))
                            {
                                txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("VALES");
                                pnlValesComb.Visible = true;
                                txtValeI.Text = detalleComision.Vale_Comb_I;
                                txtValeF.Text = detalleComision.Vale_Comb_F;
                                pnlValesComb.Visible = false;
                                clsFuncionesGral.Activa_Paneles(pnlCombustEfe, false);
                            }
                            else if (detalleComision.Pago_Combustible == "2")
                            {
                                txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("Vales");
                                pnlValesComb.Visible = true;
                                txtValeI.Text = detalleComision.Vale_Comb_I;
                                txtValeF.Text = detalleComision.Vale_Comb_F;

                                clsFuncionesGral.Activa_Paneles(pnlCombustEfe, false);
                                clsFuncionesGral.Activa_Paneles(pnlValesComb, true);
                            }
                            /*
                        else if (detalleComision.Pago_Combustible == "3")
                        {
                            txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("vales y efectivo");
                            clsFuncionesGral.Activa_Paneles(pnlCombustEfe, true );
                            pnlValesComb.Visible = false;
                            pnlValesComb.Visible = true;
                            txtValeI.Text = Dictionary.NUMERO_CERO;
                            txtValeF.Text = Dictionary.NUMERO_CERO;

                            T
                        } */
                        }
                    }
                }
                //configuracion_combustible = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(ousuario.Ubicacion, "COMBUSTIBLES");
                //Session["Crip_Configuracion_Combustibles"] = configuracion_combustible;

                txtPagoCombustibles.Visible = true;
                dplPagoCombustibles.Items.Clear();
                dplPagoCombustibles.Visible = false;


                //    else if (detalleComision.Pago_Combustible == "4")
                //   {
                //     txtPagoCombustibles.Text = clsFuncionesGral.ConvertMayus("");
                //    pnlValesComb.Visible = true;
                //}
            }

            txtCombusVales.Text = detalleComision.Combustible_Vales;
            txtCombustEfe.Text = detalleComision.Combustible_Efectivo;

            txtValeI.Text = detalleComision.Vale_Comb_I;
            txtValeF.Text = detalleComision.Vale_Comb_F;

            txtPeaje.Text = detalleComision.Peaje;
            txtpartidaPeaje.Text = detalleComision.Partida_Peaje;
            txtPasajes.Text = detalleComision.Pasaje;
            txtPartidaPasaje.Text = detalleComision.Partida_Pasaje;

            if (chkTerrestre.Checked)
            {
                Propertys_Paneles_Transporte(psOpcion, dplTipoTAUt.SelectedValue.ToString(), psPermisosAdmin);
            }
            else
            {
                Propertys_Paneles_Transporte(psOpcion, detalleComision.Tipo_Trans, psPermisosAdmin);
            }

            txtHoraSalida.Text = detalleComision.Inicio_Comision;
            txtHoraRegreso.Text = detalleComision.Fin_Comision;

            if ((psOpcion == Dictionary.VOBO) | (psPermisosAdmin == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))//VOBO
            {
                pnlProyectoExt.Enabled = true;
                ListBox1.Enabled = true;
                txtNomProyectExt.Text = objProyecto.Descripcion;
                txtNumProyExt.Text = objProyecto.Clv_Proy;
                txtRespPoryExt.Text = objProyecto.Responsable;
                txtObjProyext.Text = objProyecto.Objetivo;
                pnlPeriodoautorizado.Enabled = true;
                chkMedioDia.Enabled = true;
                chkModifi.Enabled = true;
                dplZonas.Enabled = true;
                dplTipoPagoViaticos.Enabled = true;
                dplPagos.Enabled = true;
                dplTipoViaticos.Enabled = true;
                txtTipoViaticos.Enabled = true;
                txtZonas.Enabled = true;
                txtPagos.Enabled = true;
                txtTipoPagoViaticos.Enabled = true;
                txtPartPresupuestal.Enabled = true;
                pnlRecorridoTerrestre.Enabled = true;
                pnlRecorridoAereo.Enabled = true;
                pnlRecorridoAcuatico.Enabled = true;
                pnlCombustible.Enabled = true;
                pnlValesComb.Enabled = true;
                pnlPasajes.Enabled = true;
                pnlPeaje.Enabled = true;
                pnlChekModifiVehiculoOficial.Visible = true;
                pnlVehiculo.Visible = false;
                pnlAgregaComisionados.Visible = true;
                Panel4.Enabled = true;
                GridView1.Enabled = true;
                pnlHorario.Enabled = true;
            }
            else
            {
                pnlProyectoExt.Enabled = false;
                ListBox1.Enabled = false;
                txtNomProyectExt.Text = objProyecto.Descripcion;
                txtNumProyExt.Text = objProyecto.Clv_Proy;
                txtRespPoryExt.Text = objProyecto.Responsable;
                txtObjProyext.Text = objProyecto.Objetivo;
                pnlPeriodoautorizado.Enabled = false;
                chkMedioDia.Enabled = false;
                chkModifi.Enabled = false;
                dplZonas.Enabled = false;
                txtZonas.Enabled = false;
                dplPagos.Enabled = false;
                txtPagos.Enabled = false;
                dplTipoViaticos.Enabled = false;
                txtTipoViaticos.Enabled = false;
                dplTipoPagoViaticos.Enabled = false;
                txtTipoPagoViaticos.Enabled = false;
                txtPartPresupuestal.Enabled = false;
                pnlRecorridoTerrestre.Enabled = false;
                pnlRecorridoAereo.Enabled = false;
                pnlRecorridoAcuatico.Enabled = false;
                pnlCombustible.Enabled = false;
                pnlValesComb.Enabled = false;
                pnlPasajes.Enabled = false;
                pnlPeaje.Enabled = false;
                pnlVehiculo.Visible = false;
                pnlChekModifiVehiculoOficial.Visible = false;
                pnlAgregaComisionados.Visible = false;
                Panel4.Enabled = false;
                GridView1.Enabled = false;
                pnlHorario.Enabled = false;
            }



        }

        protected void dplVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsVehiculoAut = dplVehiculo.SelectedValue;
            string[] DatosVehiculoAut = new string[9];
            Ubicacion oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(Session["Crip_Ubicacion"].ToString());
            if ((dplTipoTAUt.SelectedValue.ToString() == "AV") | (dplTipoTAUt.SelectedValue.ToString() == "VB") | (dplTipoTAUt.SelectedValue.ToString() == "VP") | (dplTipoTAUt.SelectedValue.ToString() == "VA") | (dplTipoTAUt.SelectedValue.ToString() == "VAA") | (dplTipoTAUt.SelectedValue.ToString() == "VAB") | (dplTipoTAUt.SelectedValue.ToString() == "OAP") | (dplTipoTAUt.SelectedValue.ToString() == "VAP") | (dplTipoTAUt.SelectedValue.ToString() == "VO"))
            {

                DatosVehiculoAut = MngNegocioTransporte.Descrip_Trans("TER", "VO", Session["Crip_Ubicacion"].ToString(), lsVehiculoAut);

                lblVehiculoAut.Text = string.Empty;

                separador = string.Empty;
                if (DatosVehiculoAut[0] == null)
                { lblVehiculoAut.Text = Dictionary.CADENA_NULA; }
                else
                {
                    for (int j = 0; j < DatosVehiculoAut.Length; j++)
                    {
                        if ((j == 2) | (j == 5)) separador = " <br>";
                        else separador = "  ";
                        lblVehiculoAut.Text += DatosVehiculoAut[j] + separador;
                    }
                    lblVehiculoAut.Text = lblVehiculoAut.Text + oUbicacionTrans.Desc_Corto;
                }


            }


        }

        public void Propertys_Paneles_Transporte(string psOpcion, string psTipo, string psPermisosAdmin)
        {
            string Tipo;

            Tipo = psTipo;

            bool pbEnabled;

            if ((psOpcion == Dictionary.VOBO) | (psPermisosAdmin == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))//VOBO
            {
                pbEnabled = true;
            }
            else
            {
                pbEnabled = false;
            }

            switch (Tipo)
            {
                case "VO":
                    //vehiculo oficial
                    //  pnlVehiculo.Visible = true;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);

                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "VB": //vehiculo oficial - autobus

                    //pnlVehiculo.Visible = true;

                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    //pnlPartidaTrans.Visible = true;
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "VP": //vehiculo oficial - AUTO PROPIO

                    //pnlVehiculo.Visible = true;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);

                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);


                    break;
                case "AB"://autobus

                    //pnlVehiculo.Visible = false;
                    //clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "AP"://auto personal

                    //pnlVehiculo.Visible = false;
                    //pnlCombustible.Visible = true;
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);

                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "BA"://Autobus Aereo

                    //pnlVehiculo.Visible = false;


                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);

                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);


                    break;

                case "AV": //AVION - VEHICULO OFICIAL

                    //pnlVehiculo.Visible = true;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    //clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "VA"://vehiculo oficial - embarcacion

                    //pnlVehiculo.Visible = true;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, true, pbEnabled);

                    break;

                case "AC": //Embarcacion

                    //pnlVehiculo.Visible = false;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, true, pbEnabled);

                    break;

                case "AE": //avion
                    //pnlVehiculo.Visible = false;

                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "VAA": //VEHICULO OFICIAL - AVION - ACUATICO


                    //pnlVehiculo.Visible = true;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, true, pbEnabled);
                    break;

                case "ABP": //AUTOBUS  - AUTO PROPIO

                    //pnlVehiculo.Visible = false;
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "BEM": //AUTOBUS  - EMBARCACION

                    //pnlVehiculo.Visible = false;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                  // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, true, pbEnabled);

                    break;

                case "APE": //AUTO PROPIO   - EMBARCACION

                    //pnlVehiculo.Visible = false;
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);

                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;
                case "PP": //AUTO PROPIO - AVION

                    //pnlVehiculo.Visible = false;
                    //clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);
                    break;
                case "AAA"://AUTOBUS - AVION - ACUATICO

                    //pnlVehiculo.Visible = false;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, true, pbEnabled);
                    break;

                case "PAA"://AUTO PROPIO - AVION -ACUATICO

                    //pnlVehiculo.Visible = false;
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, true, pbEnabled);

                    break;

                case "VAP": //VEHICULO OFICIAL - AUTOBUS - AUTO PERSONAL

                    //pnlVehiculo.Visible = true;
                 //   clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, false);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "VAB": //VEHICULO OFICIAL - AVION - AUTOBUS 

                    //pnlVehiculo.Visible = true;
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;


                case "OAP": //VEHICULO OFICIAL - AVION - AUTO PROPIO

                    //pnlVehiculo.Visible = true;
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    //clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;

                case "AAP": //AUTOBUS - AVION - AUTO PROPIO

                    //pnlVehiculo.Visible = false;
                  //  clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartCombustibles, true);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true, pbEnabled);
                    //clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPeaje, true);
                   // clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvPartidasPasaje, true);

                    //paneles de recorridos
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoTerrestre, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAereo, true, pbEnabled);
                    clsFuncionesGral.Activa_Paneles(pnlRecorridoAcuatico, false);

                    break;
            }
        }

        protected void txtInicioAut_TextChanged(object sender, EventArgs e)
        {
            bool editado = false;
            string inicio = clsFuncionesGral.FormatFecha(Convert.ToString(txtInicioAut.Text));
            string Final = clsFuncionesGral.FormatFecha(Convert.ToString(txtFinAut.Text));

            string f_inicio = clsFuncionesGral.FormatFecha(Session["Inicio_Comision"].ToString());
            string f_final = clsFuncionesGral.FormatFecha(Session["Fin_Comision"].ToString());


            if (f_inicio != inicio)
            {
                editado = true;
            }
            else if (f_final != Final)
            {
                editado = true;
            }

            if (editado)
            {
                DateTime psFechI;
                DateTime psFechF;
                psFechI = Convert.ToDateTime(inicio);
                psFechF = Convert.ToDateTime(Final);

                if (psFechF < psFechI)
                {
                    //    error = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La fecha final de la comision no puede ser anterior a la de Inicio, favor de validar.');", true);
                    return;
                }
                else
                {
                    if (psFechI == psFechF)
                    {
                        txtDiasAut.Text = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(1 - 0.5));
                        txtDiasTotalaPagar.Text = txtDiasAut.Text;
                    }
                    else
                    {
                        TimeSpan ts = (psFechF - psFechI);
                        txtDiasAut.Text = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(ts.Days + 0.5));
                        txtDiasTotalaPagar.Text = txtDiasAut.Text;
                    }

                }
            }
        }

        protected void txtFinAut_TextChanged(object sender, EventArgs e)
        {
            bool editado = false;
            string inicio = clsFuncionesGral.FormatFecha(Convert.ToString(txtInicioAut.Text));
            string Final = clsFuncionesGral.FormatFecha(Convert.ToString(txtFinAut.Text));

            string f_inicio = clsFuncionesGral.FormatFecha(Session["Inicio_Comision"].ToString());
            string f_final = clsFuncionesGral.FormatFecha(Session["Fin_Comision"].ToString());


            if (f_inicio != inicio)
            {
                editado = true;
            }
            else if (f_final != Final)
            {
                editado = true;
            }

            if (editado)
            {
                DateTime psFechI;
                DateTime psFechF;
                psFechI = Convert.ToDateTime(inicio);
                psFechF = Convert.ToDateTime(Final);

                if (psFechF < psFechI)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La fecha final de la comision no puede ser anterior a la de Inicio, favor de validar.');", true);
                    return;
                }
                else
                {
                    if (psFechI == psFechF)
                    {
                        txtDiasAut.Text = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(1 - 0.5));
                        txtDiasTotalaPagar.Text = txtDiasAut.Text;
                    }
                    else
                    {
                        TimeSpan ts = (psFechF - psFechI);
                        txtDiasAut.Text = clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(ts.Days + 0.5));
                        txtDiasTotalaPagar.Text = txtDiasAut.Text;
                    }

                }
            }
        }

        protected void chkMedioDia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedioDia.Checked)
            {
                txtDiasAut.Text = clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(txtDiasAut.Text) + medio));
            }
            else
            {
                if ((Convert.ToDouble(txtDiasAut.Text) % 2) == 0)
                {
                    txtDiasAut.Text = clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(txtDiasAut.Text) - medio));
                }
            }

        }

        protected void dplZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDiasComercial.Text = Dictionary.NUMERO_CERO;
            txtDiasRural.Text = Dictionary.NUMERO_CERO;
            txt50km.Text = Dictionary.NUMERO_CERO;


            string zona = dplZonas.SelectedValue.ToString();

            if (zona == "16")//investigadores  rural
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, true);

                txt50km.Visible = false;
                Label281.Visible = false;

                chkMedioDia.Enabled = true;
                chkModifi.Enabled = false;

            }
            else if (zona == "17")//mandos medios y rural
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, true);

                txt50km.Visible = false;
                Label281.Visible = false;

                chkMedioDia.Enabled = true;
                chkModifi.Enabled = false;
            }
            else if (zona == "18")
            {
                //singladuras + investigadores
            }
            else if (zona == "19")//PERSONA EN CAMPO 50 KM DENTRO DEL MUNICIPIO
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);

                txtDiasAut.Text = lblDias.Text;
                chkMedioDia.Enabled = false;
            }
            else if (zona == "20") //MIXTO (INVESTIGADORES + PERSONAL EN CAMPO )
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);
                txtDiasRural.Visible = false;
                Label240.Visible = false;

                txtDiasComercial.Visible = true;
                Label241.Visible = true;

                txt50km.Visible = true;
                Label281.Visible = true;

            }
            else if (zona == "21")
            {
                //MIXTO(MANDOS MEDIOS +PERSONAL EN CAMPO)
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);
                txtDiasRural.Visible = false;
                Label240.Visible = false;

                txtDiasComercial.Visible = true;
                Label241.Visible = true;

                txt50km.Visible = true;
                Label281.Visible = true;

            }
            else if (zona == "22")//MIXTO (INVESTIGADORES +PERSONAL EN CAMPO + RURAL ) 
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, true);

                txtDiasRural.Visible = true;
                Label240.Visible = true;

                txtDiasComercial.Visible = true;
                Label241.Visible = true;

                txt50km.Visible = true;
                Label281.Visible = true;
            }
            else if (zona == "23")//MIXTO (MANDOS MEDIOS +PERSONAL EN CAMPO + RURAL ) 
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);

                txtDiasRural.Visible = true;
                Label240.Visible = true;

                txtDiasComercial.Visible = true;
                Label241.Visible = true;

                txt50km.Visible = true;
                Label281.Visible = true;
            }
            else if (zona == "15")
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);

                if ((Convert.ToDouble(txtDiasAut.Text) % 2) == 0)
                {
                    chkMedioDia.Enabled = false;
                }
                else
                {
                    txtDiasAut.Text = lblDias.Text;
                    txtDiasTotalaPagar.Text = txtDiasAut.Text;
                    chkMedioDia.Enabled = false;
                }
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);

                if (chkMedioDia.Checked)
                {
                    chkMedioDia.Enabled = false;
                    txtDiasTotalaPagar.Text = txtDiasAut.Text;

                }
                else
                {
                    txtDiasTotalaPagar.Text = txtDiasAut.Text;
                    chkMedioDia.Enabled = true;
                }

                /*
                if ((Convert.ToDouble(txtDiasAut.Text) % 2) == 0)
                {
                    txtDiasAut.Text = clsFuncionesGral.ConvertString((clsFuncionesGral.Convert_Double(txtDiasAut.Text) - medio));
                 
                }
                else
                {
                    ;
                }*/
            }

        }

        protected void txtDiasRural_TextChanged(object sender, EventArgs e)
        {/*
            string diasRural = txtDiasRural.Text;
            string diasComercial = txtDiasComercial.Text;

            if ((clsFuncionesGral.Convert_Double(diasRural) + clsFuncionesGral.Convert_Double(diasComercial)) > clsFuncionesGral.Convert_Double(txtDiasAut.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Los dias en zona rural personal de campo y dias con comprobante fiscal rebasan los dias autorizados');", true);
                return;
            }
            else if ((clsFuncionesGral.Convert_Double(diasRural) + clsFuncionesGral.Convert_Double(diasComercial)) < clsFuncionesGral.Convert_Double(txtDiasAut.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Los dias en zona rural personal de campo y dias con comprobante fiscal no son iguales a los dias autorizados');", true);
                return;
            }*/
        }

        protected void tvPartidas_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtPartPresupuestal.Text = Convert.ToString(tvPartidas.SelectedNode.Value);
        }

        protected void chkModifi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkModifi.Checked)
            {
                txtDiasTotalaPagar.Enabled = true;
                txtDiasTotalaPagar.Text = txtDiasAut.Text;
            }
            else
            {
                txtDiasTotalaPagar.Enabled = false;
                txtDiasTotalaPagar.Text = txtDiasAut.Text;
            }
        }

        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {
            txtpartidaPeaje.Text = Convert.ToString(tvPartidasPeaje.SelectedNode.Value);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtPartCombust.Text = Convert.ToString(tvPartCombustibles.SelectedNode.Value);

        }

        protected void dplPagoCombustibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsPagoCom = dplPagoCombustibles.SelectedValue;


            if ((lsPagoCom == "2"))
            {
                clsFuncionesGral.Activa_Paneles(pnlValesComb, true);
                clsFuncionesGral.Activa_Paneles(pnlCombustEfe, false);
            }
            else if (lsPagoCom == "3")
            {
                clsFuncionesGral.Activa_Paneles(pnlValesComb, true);
                clsFuncionesGral.Activa_Paneles(pnlCombustEfe, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlValesComb, false);
                clsFuncionesGral.Activa_Paneles(pnlCombustEfe, false);
            }

        }

        protected void tvPartidasPasaje_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtPartidaPasaje.Text = Convert.ToString(tvPartidasPasaje.SelectedNode.Value);
        }

        protected void chkTerrestre_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTerrestre.Checked)
            {
                pnlVehiculo.Visible = true;
                pnlVehiculo.Enabled = true;
                dplClaseAut.DataSource = MngNegocioTransporte.ObtieneClaseTrans(Session["Crip_Ubicacion"].ToString());
                dplClaseAut.DataTextField = Dictionary.DESCRIPCION;
                dplClaseAut.DataValueField = Dictionary.CODIGO;
                dplClaseAut.DataBind();

                dplTipoTAUt.Items.Clear();
                dplVehiculo.Items.Clear();
            }
            else
            {

                pnlVehiculo.Visible = false;
                pnlVehiculo.Enabled = false;
                dplClaseAut.Items.Clear();
                dplTipoTAUt.Items.Clear();
                dplVehiculo.Items.Clear();

            }
        }

        protected void imgClaseTAut_Click(object sender, ImageClickEventArgs e)
        {
            string lsClaseTAut = dplClaseAut.SelectedValue;
            if ((lsClaseTAut == "") | (lsClaseTAut == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Clase de Transporte Obligatorio, escoja uno para avanzar.');", true);
                return;
            }
            else
            {
                dplTipoTAUt.DataSource = MngNegocioTransporte.ObtieneTipoTrans(lsClaseTAut, Session["Crip_Ubicacion"].ToString());
                dplTipoTAUt.DataTextField = Dictionary.DESCRIPCION;
                dplTipoTAUt.DataValueField = Dictionary.CODIGO;
                dplTipoTAUt.DataBind();
                // DetalleComision.Clase_Aut = lsClaseTAut;
                dplVehiculo.Items.Clear();

            }
        }

        protected void imgTipoTAut_Click(object sender, ImageClickEventArgs e)
        {
            //Rellena vehiculo

            if ((dplTipoTAUt.SelectedItem.Value == null) | (dplTipoTAUt.SelectedItem.Value == "") | (dplTipoTAUt.SelectedItem.Value == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Transporte Obligatorio, escoja uno para avanzar.');", true);
                return;
            }
            else
            {
                string TipoTAut = dplTipoTAUt.SelectedValue;
                // DetalleComision.Tipo_Aut = TipoTAut;
                string claseT;
                string TipoT;
                string[] DatosVehiculoAut = new string[9];

                if ((TipoTAut == "AV") | (TipoTAut == "VB") | (TipoTAut == "VP") | (TipoTAut == "VA") | (TipoTAut == "VAA") | (TipoTAut == "VAB") | (TipoTAut == "OAP") | (TipoTAut == "VAP") | (TipoTAut == "VO"))
                {
                    claseT = Dictionary.TERRESTRE;
                    TipoT = Dictionary.TIPO_OFICIAL;
                    Label31.Visible = true;
                    dplVehiculo.Visible = true;
                    dplVehiculo.DataSource = MngNegocioTransporte.Descripcion_Transporte(claseT, TipoT, Session["Crip_Ubicacion"].ToString());
                    dplVehiculo.DataTextField = Dictionary.DESCRIPCION;
                    dplVehiculo.DataValueField = Dictionary.CODIGO;
                    dplVehiculo.DataBind();
                }
                else
                {

                    Ubicacion oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(Session["Crip_Ubicacion"].ToString());
                    Label31.Visible = false;
                    dplVehiculo.Visible = false;
                    dplVehiculo.Items.Clear();

                    string lsVehiculoAut = MngNegocioTransporte.Obtiene_Clave_Trans(dplClaseAut.SelectedValue.ToString(), TipoTAut);
                    // DetalleComision.Vehiculo_Autorizado = lsVehiculoAut;


                    DatosVehiculoAut = MngNegocioTransporte.Descrip_Trans(dplClaseAut.SelectedValue.ToString(), TipoTAut, Session["Crip_Ubicacion"].ToString(), Dictionary.CADENA_NULA);

                    lblVehiculoAut.Text = string.Empty;

                    separador = string.Empty;
                    if (DatosVehiculoAut[0] == null)
                    { lblVehiculoAut.Text = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculoAut.Length; j++)
                        {
                            if ((j == 2) | (j == 5)) separador = " <br>";
                            else separador = "  ";
                            lblVehiculoAut.Text += DatosVehiculoAut[j] + separador;
                        }
                        lblVehiculoAut.Text = lblVehiculoAut.Text + oUbicacionTrans.Desc_Corto;
                    }
                }


            }
        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            Comision detalleComision = new Comision();
            Proyecto objProyecto = new Proyecto();
            bool error = false;
            bool editado = false;
            string tarifa = "0", tarifa1 = "0", tarifa2 = "0";
            double resultado1 = 0, resultado = 0, resultado2 = 0;
            string lsTotalViaticos = "0";

            string permisos = MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL);

            if ((Session["Crip_Opcion"].ToString() == Dictionary.VOBO) | (permisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))
            {
                if (Session["Crip_Opcion"].ToString() == Dictionary.VOBO)
                {
                    detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "8");
                }
                else if (permisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL)
                {
                    detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "");
                }

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(detalleComision.Dep_Proy, detalleComision.Proyecto);
                //  Valida_Campos(error, permisos, detalleComision, objProyecto);
                /*Vlidacion de campos*/
                //  pbError = false;

                if ((Session["Crip_Opcion"].ToString() == Dictionary.VOBO) | (permisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))//VOBO
                {
                    //valida proyecto externo
                    if ((detalleComision.Proyecto == "000") & (pnlProyectoExt.Visible))
                    {
                        //valida si existe el proyecto y en caso que no valida campos 
                        if ((objProyecto.Clv_Proy == null) | (objProyecto.Clv_Proy == Dictionary.CADENA_NULA))
                        { //valida campos de datos de proyecto externo
                            if ((txtNomProyectExt.Text == null) | (txtNomProyectExt.Text == Dictionary.CADENA_NULA) | (txtNumProyExt.Text == null) | (txtNumProyExt.Text == Dictionary.CADENA_NULA) | (txtRespPoryExt.Text == null) | (txtRespPoryExt.Text == Dictionary.CADENA_NULA) | (txtObjProyext.Text == null) | (txtObjProyext.Text == Dictionary.CADENA_NULA))
                            {
                                error = true;
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe agregar Datos de programa externo');", true);
                                return;
                            }
                            else
                            {
                                //  pbError = false;
                                MngNegocioProyecto.Inserta_Proyectoexterno(txtNumProyExt.Text, txtNomProyectExt.Text, txtRespPoryExt.Text, txtObjProyext.Text, Session["Crip_Folio"].ToString());
                            }
                        }
                        else //validar que los campos sean iguales y agregar o hacer update
                        {
                            while (!editado)
                            {
                                if (objProyecto.Clv_Proy != txtNumProyExt.Text)
                                {
                                    editado = true;
                                    break;
                                }
                                else if (objProyecto.Descripcion != txtNomProyectExt.Text)
                                {
                                    editado = true;
                                    break;
                                }
                                else if (objProyecto.Responsable != txtRespPoryExt.Text)
                                {
                                    editado = true;
                                    break;
                                }
                                else if (objProyecto.Objetivo != txtObjProyext.Text)
                                {
                                    editado = true;
                                    break;
                                }
                                else
                                {
                                    editado = false;
                                    break;
                                }
                            }

                            if (editado)
                            {
                                error = true;
                                MngNegocioProyecto.Update_proyectoExterno(txtNumProyExt.Text, txtNomProyectExt.Text, txtRespPoryExt.Text, txtObjProyext.Text, Session["Crip_Folio"].ToString());
                            }
                        }

                    }

                    //panel comentarios
                    if (txtObservaciones.Text == Dictionary.CADENA_NULA)
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Comentarios   para aprobar/ministrar esta Cómision obligatorios.');", true);
                        return;
                    }
                    else
                    {
                        //  pbError = false;
                        editado = false;

                        if (permisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL)
                        {
                            if (detalleComision.Observaciones_Vobo != txtObservaciones.Text) editado = true;
                            if (editado) detalleComision.Observaciones_Vobo = txtObservaciones.Text;

                            if (detalleComision.Observaciones_Autoriza != txtObservaciones.Text) editado = true;
                            if (editado) detalleComision.Observaciones_Autoriza = txtObservaciones.Text;
                        }
                        else
                        {
                            if (detalleComision.Observaciones_Vobo != txtObservaciones.Text) editado = true;
                            if (editado) detalleComision.Observaciones_Vobo = txtObservaciones.Text;
                        }

                    }


                }
                else //rol Autorizador
                {
                    if ((detalleComision.Proyecto == "000") & (pnlProyectoExt.Visible))
                    {
                        //valida si existe el proyecto y en caso que no valida campos 
                        if ((objProyecto.Clv_Proy == null) | (objProyecto.Clv_Proy == Dictionary.CADENA_NULA))
                        { //valida campos de datos de proyecto externo
                            if ((txtNomProyectExt.Text == null) | (txtNomProyectExt.Text == Dictionary.CADENA_NULA) | (txtNumProyExt.Text == null) | (txtNumProyExt.Text == Dictionary.CADENA_NULA) | (txtRespPoryExt.Text == null) | (txtRespPoryExt.Text == Dictionary.CADENA_NULA) | (txtObjProyext.Text == null) | (txtObjProyext.Text == Dictionary.CADENA_NULA))
                            {
                                error = true;
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe agregar Datos de proyecto externo , o solicite al administrador agregarlos ');", true);
                                return;
                            }

                        }
                    }

                    //panel comentarios
                    if (txtObservaciones.Text == Dictionary.CADENA_NULA)
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Comentarios   para aprobar/ministrar esta Cómision obligatorios.');", true);
                        return;
                    }
                    else
                    {
                        //  pbError = false;
                        editado = false;

                        if (detalleComision.Observaciones_Autoriza != txtObservaciones.Text) editado = true;
                        if (editado) detalleComision.Observaciones_Autoriza = txtObservaciones.Text;
                    }

                }

                ///Validacion de los demas campos

                //fechas
                editado = false;
                string inicio = clsFuncionesGral.FormatFecha(Convert.ToString(txtInicioAut.Text));
                string Final = clsFuncionesGral.FormatFecha(Convert.ToString(txtFinAut.Text));

                if (detalleComision.Fecha_Inicio != inicio)
                {
                    editado = true;
                }
                else if (detalleComision.Fecha_Final != Final)
                {
                    editado = true;
                }

                if (editado)
                {
                    DateTime psFechaI;
                    DateTime psFechaF;
                    psFechaI = Convert.ToDateTime(inicio);
                    psFechaF = Convert.ToDateTime(Final);

                    if (psFechaF < psFechaI)
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La fecha final de la comision no puede ser anterior a la de Inicio, favor de validar.');", true);
                        return;
                    }
                    else
                    {
                        //   error = false;
                        detalleComision.Fecha_Inicio = inicio;
                        detalleComision.Fecha_Final = Final;
                    }
                }

                editado = false;
                if ((txtDiasAut.Text == Dictionary.NUMERO_CERO) | (txtDiasAut.Text == Dictionary.CADENA_NULA))
                {
                    error = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias Autorizados no pueden ser vacios o 0.');", true);
                    return;
                }
                else
                {
                    if (clsFuncionesGral.IsNumeric(txtDiasAut.Text))
                    {
                        //   error = false;
                        if ((detalleComision.Dias_Reales != txtDiasAut.Text))
                        {
                            editado = true;
                        }
                        if (editado)
                        {
                            detalleComision.Dias_Reales = txtDiasAut.Text;
                        }
                        else detalleComision.Dias_Reales = txtDiasAut.Text;

                    }
                    else
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor dias autorizados debe ser entero  decimal ejemplo : 3.5 ,1, 2');", true);
                        return;
                    }
                }

                if (chkModifi.Checked)
                {
                    if ((txtDiasTotalaPagar.Text == null) | (txtDiasTotalaPagar.Text == ""))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor dias a pagar no puede ser vacio');", true);
                        return;
                    }
                    else
                    {
                        if (Convert.ToDouble(txtDiasTotalaPagar.Text) > Convert.ToDouble(txtDiasAut.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias a pagar no pueden ser mayor a los autorizados');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Pagar = txtDiasTotalaPagar.Text;
                        }

                    }
                }
                else
                {
                    detalleComision.Dias_Pagar = txtDiasAut.Text;
                }

                //zonas economicas (0,1,2)
                if ((detalleComision.Zona_Comercial == null) | (detalleComision.Zona_Comercial == Dictionary.CADENA_NULA))// | (Detalle_Comision.Zona_Comercial == Dictionary.NUMERO_CERO))
                {
                    string lsZonaComer = dplZonas.SelectedValue.ToString();

                    detalleComision.Zona_Comercial = lsZonaComer;

                    if ((lsZonaComer == "16"))//mixto rural + investigadores
                    {
                        string totalpagar = clsFuncionesGral.ConvertString((Convert.ToDouble(txtDiasRural.Text) + Convert.ToDouble(txtDiasComercial.Text)));

                        if (clsFuncionesGral.Convert_Double(totalpagar) > clsFuncionesGral.Convert_Double(txtDiasTotalaPagar.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias a rurales y comerciales no pueden ser mayor a los autorizados por pagar');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Rural = txtDiasRural.Text;
                            detalleComision.Dias_Comercial = txtDiasComercial.Text;
                            detalleComision.Dias_50 = Dictionary.NUMERO_CERO;
                            detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;

                            txt50km.Text = Dictionary.NUMERO_CERO;
                            txtDiasTotalaPagar.Text = totalpagar;

                            tarifa1 = MngNegocioComision.Obtiene_Tarifa("14");
                            tarifa = MngNegocioComision.Obtiene_Tarifa("2");

                            resultado = ((Convert.ToDouble(detalleComision.Dias_Rural) * Convert.ToDouble(tarifa1)) + ((Convert.ToDouble(detalleComision.Dias_Comercial) * Convert.ToDouble(tarifa))));
                            lsTotalViaticos = resultado.ToString();
                            detalleComision.Total_Viaticos = resultado.ToString();
                            detalleComision.Dias_Pagar = totalpagar;
                        }
                    }
                    else if (lsZonaComer == "17")
                    {
                        string totalpagar = clsFuncionesGral.ConvertString((Convert.ToDouble(txtDiasRural.Text) + Convert.ToDouble(txtDiasComercial.Text)));

                        if (clsFuncionesGral.Convert_Double(totalpagar) > clsFuncionesGral.Convert_Double(txtDiasTotalaPagar.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias a rurales y comerciales no pueden ser mayor a los autorizados por pagar');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Rural = txtDiasRural.Text;
                            detalleComision.Dias_Comercial = txtDiasComercial.Text;
                            detalleComision.Dias_50 = Dictionary.NUMERO_CERO;
                            detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;
                            txt50km.Text = Dictionary.NUMERO_CERO;
                            txtDiasTotalaPagar.Text = totalpagar;

                            tarifa1 = MngNegocioComision.Obtiene_Tarifa("14");
                            tarifa = MngNegocioComision.Obtiene_Tarifa("4");

                            resultado = ((Convert.ToDouble(detalleComision.Dias_Rural) * Convert.ToDouble(tarifa1)) + ((Convert.ToDouble(detalleComision.Dias_Comercial) * Convert.ToDouble(tarifa))));
                            lsTotalViaticos = resultado.ToString();
                            detalleComision.Total_Viaticos = resultado.ToString();
                            detalleComision.Dias_Pagar = totalpagar;
                        }
                    }
                    else if (lsZonaComer == "18")
                    {
                        //agregar lo de singrladuras y viaticos
                    }
                    else if (lsZonaComer == "20")
                    {
                        string totalpagar = clsFuncionesGral.ConvertString((Convert.ToDouble(txt50km.Text) + Convert.ToDouble(txtDiasComercial.Text)));

                        if (clsFuncionesGral.Convert_Double(totalpagar) > clsFuncionesGral.Convert_Double(txtDiasTotalaPagar.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias de personal en campo y comerciales no pueden ser mayor a los autorizados por pagar');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Rural = Dictionary.NUMERO_CERO;
                            detalleComision.Dias_Comercial = txtDiasComercial.Text;
                            detalleComision.Dias_50 = txt50km.Text;
                            detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;

                            txtDiasTotalaPagar.Text = totalpagar;

                            tarifa1 = MngNegocioComision.Obtiene_Tarifa("19");
                            tarifa = MngNegocioComision.Obtiene_Tarifa("2");

                            resultado = ((Convert.ToDouble(detalleComision.Dias_50) * Convert.ToDouble(tarifa1)) + ((Convert.ToDouble(detalleComision.Dias_Comercial) * Convert.ToDouble(tarifa))));
                            lsTotalViaticos = resultado.ToString();
                            detalleComision.Total_Viaticos = resultado.ToString();
                            detalleComision.Dias_Pagar = totalpagar;
                        }
                    }
                    else if (lsZonaComer == "21")
                    {
                        string totalpagar = clsFuncionesGral.ConvertString((Convert.ToDouble(txt50km.Text) + Convert.ToDouble(txtDiasComercial.Text)));

                        if (clsFuncionesGral.Convert_Double(totalpagar) > clsFuncionesGral.Convert_Double(txtDiasTotalaPagar.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias de personal en campo y comerciales no pueden ser mayor a los autorizados por pagar');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Rural = Dictionary.NUMERO_CERO;
                            detalleComision.Dias_Comercial = txtDiasComercial.Text;
                            detalleComision.Dias_50 = txt50km.Text;
                            detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;

                            txtDiasTotalaPagar.Text = totalpagar;

                            tarifa1 = MngNegocioComision.Obtiene_Tarifa("19");
                            tarifa = MngNegocioComision.Obtiene_Tarifa("4");

                            resultado = ((Convert.ToDouble(detalleComision.Dias_50) * Convert.ToDouble(tarifa1)) + ((Convert.ToDouble(detalleComision.Dias_Comercial) * Convert.ToDouble(tarifa))));
                            lsTotalViaticos = resultado.ToString();
                            detalleComision.Total_Viaticos = resultado.ToString();
                            detalleComision.Dias_Pagar = totalpagar;
                        }
                    }
                    else if (lsZonaComer == "22")
                    {
                        string totalpagar = clsFuncionesGral.ConvertString((Convert.ToDouble(txtDiasRural.Text) + Convert.ToDouble(txtDiasComercial.Text) + +Convert.ToDouble(txt50km.Text)));

                        if (clsFuncionesGral.Convert_Double(totalpagar) > clsFuncionesGral.Convert_Double(txtDiasTotalaPagar.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias de personal en campo , comerciales y rurales no pueden ser mayor a los autorizados por pagar');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Rural = txtDiasRural.Text;
                            detalleComision.Dias_Comercial = txtDiasComercial.Text;
                            detalleComision.Dias_50 = txt50km.Text;
                            detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;

                            txtDiasTotalaPagar.Text = totalpagar;

                            tarifa = MngNegocioComision.Obtiene_Tarifa("2");
                            tarifa1 = MngNegocioComision.Obtiene_Tarifa("14");
                            tarifa2 = MngNegocioComision.Obtiene_Tarifa("19");

                            resultado = ((Convert.ToDouble(detalleComision.Dias_Comercial) * Convert.ToDouble(tarifa)) + (Convert.ToDouble(detalleComision.Dias_Rural) * Convert.ToDouble(tarifa1)) + ((Convert.ToDouble(detalleComision.Dias_50) * Convert.ToDouble(tarifa2))));
                            lsTotalViaticos = resultado.ToString();
                            detalleComision.Total_Viaticos = resultado.ToString();
                            detalleComision.Dias_Pagar = totalpagar;
                        }
                    }

                    else if (lsZonaComer == "23")
                    {
                        string totalpagar = clsFuncionesGral.ConvertString((Convert.ToDouble(txtDiasRural.Text) + Convert.ToDouble(txtDiasComercial.Text) + +Convert.ToDouble(txt50km.Text)));

                        if (clsFuncionesGral.Convert_Double(totalpagar) > clsFuncionesGral.Convert_Double(txtDiasTotalaPagar.Text))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Dias de personal en campo , comerciales y rurales no pueden ser mayor a los autorizados por pagar');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Dias_Rural = txtDiasRural.Text;
                            detalleComision.Dias_Comercial = txtDiasComercial.Text;
                            detalleComision.Dias_50 = txt50km.Text;
                            detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;

                            txtDiasTotalaPagar.Text = totalpagar;

                            tarifa = MngNegocioComision.Obtiene_Tarifa("4");
                            tarifa1 = MngNegocioComision.Obtiene_Tarifa("14");
                            tarifa2 = MngNegocioComision.Obtiene_Tarifa("19");

                            resultado = ((Convert.ToDouble(detalleComision.Dias_Comercial) * Convert.ToDouble(tarifa)) + (Convert.ToDouble(detalleComision.Dias_Rural) * Convert.ToDouble(tarifa1)) + ((Convert.ToDouble(detalleComision.Dias_50) * Convert.ToDouble(tarifa2))));
                            lsTotalViaticos = resultado.ToString();
                            detalleComision.Total_Viaticos = resultado.ToString();
                        }
                    }
                    else if (lsZonaComer == "0")
                    {
                        clsFuncionesGral.Activa_Paneles(pnlMixtosDias, false);

                        detalleComision.Dias_Rural = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Comercial = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_50 = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;
                        detalleComision.Total_Viaticos = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Pagar = Dictionary.NUMERO_CERO;

                        txtDiasTotalaPagar.Text = Dictionary.NUMERO_CERO;
                        txtDiasRural.Text = Dictionary.NUMERO_CERO;
                        txtDiasComercial.Text = Dictionary.NUMERO_CERO;
                        txt50km.Text = Dictionary.NUMERO_CERO;
                        detalleComision.Total_Viaticos = Dictionary.NUMERO_CERO;

                    }
                    else if (lsZonaComer == "15")
                    {
                        detalleComision.Dias_Rural = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Comercial = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Navegados = txtDiasAut.Text;
                        detalleComision.Dias_50 = Dictionary.NUMERO_CERO;

                        detalleComision.Dias_Pagar = txtDiasTotalaPagar.Text;
                        tarifa = MngNegocioComision.Obtiene_Tarifa("15");
                        resultado = ((Convert.ToDouble(detalleComision.Dias_Navegados) * Convert.ToDouble(tarifa)));
                        lsTotalViaticos = resultado.ToString();
                        detalleComision.Singladuras = lsTotalViaticos.ToString();
                        detalleComision.Total_Viaticos = Dictionary.NUMERO_CERO;

                    }
                    else if (lsZonaComer == "14")
                    {
                        detalleComision.Dias_Rural = txtDiasTotalaPagar .Text ;
                        detalleComision.Dias_Comercial = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Navegados = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_50 = Dictionary.NUMERO_CERO;

                        // txtDiasTotalaPagar.Text = txtDiasAut.Text;
                        detalleComision.Dias_Pagar = txtDiasTotalaPagar.Text;

                        tarifa = MngNegocioComision.Obtiene_Tarifa("14");

                        resultado = ((Convert.ToDouble(detalleComision.Dias_Rural) * Convert.ToDouble(tarifa)));
                        lsTotalViaticos = resultado.ToString();
                        detalleComision.Total_Viaticos = resultado.ToString();

                    }
                    else
                    {
                        detalleComision.Dias_Rural = Dictionary.NUMERO_CERO;
                        detalleComision.Dias_Comercial = txtDiasTotalaPagar.Text;
                        detalleComision.Dias_50 = Dictionary.NUMERO_CERO;

                        tarifa = MngNegocioComision.Obtiene_Tarifa(dplZonas.SelectedValue.ToString());

                        resultado = (Convert.ToDouble(detalleComision.Dias_Pagar) * Convert.ToDouble(tarifa));
                        lsTotalViaticos = resultado.ToString();
                        detalleComision.Total_Viaticos = resultado.ToString();

                        //txtDiasTotalaPagar.Text = txtDiasAut.Text;
                        detalleComision.Dias_Pagar = txtDiasTotalaPagar.Text;

                    }
                }

                //forma de pago viatico (no aplica = 0,anticipado 1, devengados 2)
                if ((detalleComision.Forma_Pago_Viaticos == null) | (detalleComision.Forma_Pago_Viaticos == Dictionary.CADENA_NULA) | (detalleComision.Forma_Pago_Viaticos == Dictionary.NUMERO_CERO))
                {
                    detalleComision.Forma_Pago_Viaticos = dplPagos.SelectedValue.ToString();
                }

                //tipo de viaticos (homologados 1, tarifarios 2)
                if ((detalleComision.Tipo_Viaticos == null) | (detalleComision.Tipo_Viaticos == Dictionary.CADENA_NULA))
                {
                    detalleComision.Tipo_Viaticos = dplTipoViaticos.SelectedValue.ToString();
                }

                //Tipo pagos viaticos
                if ((detalleComision.Tipo_Pago_Viatico == null) | (detalleComision.Tipo_Pago_Viatico == Dictionary.CADENA_NULA))
                {
                    detalleComision.Tipo_Pago_Viatico = dplTipoPagoViaticos.SelectedValue.ToString();
                }

                //partida presupuestal
               /* if ((detalleComision.Partida_Presupuestal == null) | (detalleComision.Partida_Presupuestal == Dictionary.CADENA_NULA))
                {
                    if ((txtPartPresupuestal.Text == null) | (txtPartPresupuestal.Text == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida Presupuestal no puede ser vacio');", true);
                        return;
                    }
                    else
                    {
                        //  error = false;
                        detalleComision.Partida_Presupuestal = txtPartPresupuestal.Text;
                    }
                }
                else
                {
                    editado = false;
                    if (detalleComision.Partida_Presupuestal != txtPartPresupuestal.Text) editado = true;
                    if (editado) detalleComision.Partida_Presupuestal = txtPartPresupuestal.Text;
                }*/

                //Recorridos terrestre aereo acuatico
                if (pnlRecorridoTerrestre.Visible)
                {
                    if ((txtOrigen_Ter.Text == null) | (txtOrigen_Ter.Text == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Recorrido transporte terrestre no puede ser vacio');", true);
                        return;
                    }
                    else
                    {
                        //    error = false;
                        editado = false;
                        if (detalleComision.Origen_Terrestre != txtOrigen_Ter.Text) editado = true;
                        if (editado) detalleComision.Origen_Terrestre = txtOrigen_Ter.Text;
                    }
                }

                if (pnlRecorridoAereo.Visible)
                {
                    if ((txtOrigen_Aer.Text == null) | (txtOrigen_Aer.Text == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Recorrido transporte aereo no puede ser vacio');", true);
                        return;
                    }
                    else
                    {
                        //   error = false;
                        editado = false;
                        if (detalleComision.Origen_Aereo != txtOrigen_Aer.Text) editado = true;
                        if (editado) detalleComision.Origen_Aereo = txtOrigen_Aer.Text;
                    }
                }

                if (pnlRecorridoAcuatico.Visible)
                {
                    if ((txtOrigen_Des_Acu.Text == null) | (txtOrigen_Des_Acu.Text == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Recorrido transporte acuatico no puede ser vacio');", true);
                        return;
                    }
                    else
                    {
                        //  error = false;
                        editado = false;
                        if (detalleComision.Origen_Dest_Acu != txtOrigen_Des_Acu.Text) editado = true;
                        if (editado) detalleComision.Origen_Dest_Acu = txtOrigen_Des_Acu.Text;
                    }
                }
                //panel combustibles ACA
                if (pnlCombustible.Visible)
                {
                    if ((txtCombusAut.Text == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El combustible autorizado no puede ser vacio');", true);
                        return;
                    }
                    else
                    {
                        if (clsFuncionesGral.IsNumeric(txtCombusAut.Text))
                        {
                            //   error = false;
                            if (txtCombusAut.Text == Dictionary.NUMERO_CERO)
                            {
                                ;
                            }
                            else
                            {
                                editado = false;
                                if (detalleComision.Combustible_Autorizado != txtCombusAut.Text) editado = true;
                                if (editado) detalleComision.Combustible_Autorizado = txtCombusAut.Text;
                            }
                        }
                        else
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('EL combustible autorizado debe ser numerico, incluso 0');", true);
                            return;
                        }
                    }


                    //partida combus
                   
                    /* if ((detalleComision.Partida_Combustible == null) | (detalleComision.Partida_Combustible == Dictionary.CADENA_NULA))
                    {
                        if (txtCombusAut.Text == Dictionary.NUMERO_CERO)
                        {
                            txtCombusAut.Text = "26102";
                            detalleComision.Partida_Combustible = "26102";
                        }
                        else
                        {
                            if ((txtPartCombust.Text == null) | (txtPartCombust.Text == Dictionary.CADENA_NULA))
                            {
                                error = true;
                                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida Presupuestal de combustibles no puede ser vacio');", true);
                                return;
                            }
                            else
                            {
                                // error = false;
                                detalleComision.Partida_Presupuestal = txtPartPresupuestal.Text;
                            }
                        }
                    }
                    else
                    {
                        editado = false;
                        if (detalleComision.Partida_Combustible != txtPartCombust.Text) editado = true;
                        if (editado) detalleComision.Partida_Combustible = txtPartCombust.Text;
                    }
                    */

                    //pago de combustible
                    if (objProyecto.Dependencia == Session["Crip_Configuracion_Combustibles"].ToString())
                    {
                        if ((detalleComision.Pago_Combustible == null) | (detalleComision.Pago_Combustible == Dictionary.CADENA_NULA))
                        {
                            detalleComision.Pago_Combustible = dplPagoCombustibles.SelectedValue.ToString();
                        }
                    }
                    else
                    {
                        if (objProyecto.Dependencia == Session["Crip_Configuracion_Combustibles"].ToString())
                        {
                            if ((detalleComision.Pago_Combustible == null) | (detalleComision.Pago_Combustible == Dictionary.CADENA_NULA))
                            {
                                detalleComision.Pago_Combustible = dplPagoCombustibles.SelectedValue.ToString();
                            }
                            else
                            {
                                detalleComision.Pago_Combustible = detalleComision.Pago_Combustible;
                            }
                        }
                        else
                        {
                            if ((detalleComision.Pago_Combustible == null) | (detalleComision.Pago_Combustible == Dictionary.CADENA_NULA))
                            {
                                if (txtCombusAut.Text == Dictionary.NUMERO_CERO)
                                {
                                    detalleComision.Pago_Combustible = dplPagoCombustibles.SelectedValue.ToString();
                                }
                                else
                                {
                                    if (dplPagoCombustibles.SelectedValue.ToString() == "1")
                                    {
                                        error = true;
                                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una opcion de pago de combustibles');", true);
                                        return;
                                    }
                                    else
                                    {
                                        detalleComision.Pago_Combustible = dplPagoCombustibles.SelectedValue.ToString();
                                    }
                                }
                            }
                            else
                            {
                                detalleComision.Pago_Combustible = detalleComision.Pago_Combustible;
                            }
                        }


                    }

                    //pago de combustibles por vales 
                    if (pnlValesComb.Visible)
                    {
                        if ((txtValeI.Text == null) | (txtValeI.Text == Dictionary.CADENA_NULA))
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Folio inicial de vales de combustible requerido');", true);
                            return;
                        }
                        else
                        {
                            //   error = false;
                            editado = false;
                            if (detalleComision.Vale_Comb_I != txtValeI.Text) editado = true;
                            if (editado) detalleComision.Vale_Comb_I = txtValeI.Text;

                            editado = false;
                            if (detalleComision.Vale_Comb_F != txtValeF.Text) editado = true;
                            if (editado) detalleComision.Vale_Comb_F = txtValeF.Text;
                        }
                    }

                    if (pnlCombustEfe.Visible)
                    {
                        if (!clsFuncionesGral.IsNumeric(txtCombustEfe.Text))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de Combustible en efectivo debe ser numerico');", true);
                            return;
                        }

                        if (!clsFuncionesGral.IsNumeric(txtCombusVales.Text))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de combustible en vales debe ser numerico');", true);
                            return;
                        }
                        if ((clsFuncionesGral.Convert_Double(txtCombustEfe.Text) + clsFuncionesGral.Convert_Double(txtCombusVales.Text)) > clsFuncionesGral.Convert_Double(txtCombusAut.Text))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Combustible efectivo y en vales no puede excederse de lo autorizado ');", true);
                            return;
                        }
                        else
                        {
                            detalleComision.Combustible_Efectivo = txtCombustEfe.Text;
                            detalleComision.Combustible_Vales = txtCombusVales.Text;
                        }
                    }
                    else
                    {
                        detalleComision.Combustible_Efectivo = Dictionary.NUMERO_CERO;
                        detalleComision.Combustible_Vales = txtCombusAut.Text;
                    }

                }
                else
                {
                    detalleComision.Combustible_Efectivo = Dictionary.NUMERO_CERO;
                    detalleComision.Combustible_Vales = Dictionary.NUMERO_CERO;

                }
                //panel peaje
                if (pnlPeaje.Visible)
                {
                    if ((txtPeaje.Text == Dictionary.CADENA_NULA) | (txtPeaje.Text == null))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de peaje requerido');", true);
                        return;
                    }
                    else
                    {
                        if (clsFuncionesGral.IsNumeric(txtPeaje.Text))
                        {
                            //  error = false;
                            editado = false;
                            if (detalleComision.Peaje != txtPeaje.Text) editado = true;
                            if (editado) detalleComision.Peaje = txtPeaje.Text;
                        }
                        else
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida presupuestal  de peaje requerido');", true);
                            return;
                        }

                    }

                   //if ((txtpartidaPeaje.Text == null) | (txtpartidaPeaje.Text == Dictionary.NUMERO_CERO) | (txtpartidaPeaje.Text == Dictionary.CADENA_NULA))
                    //{
                        //error = true;
                       // ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida presupuestal  de peaje requerido');", true);
                     //   return;
                   // }
                    //else
                    //{
                        //  error = false;
                        //editado = false;
                      //  if (detalleComision.Partida_Peaje != txtpartidaPeaje.Text) editado = true;
                       // if (editado) detalleComision.Partida_Peaje = txtpartidaPeaje.Text;
                    //}
                }
                else
               {
                   detalleComision.Peaje = Dictionary.NUMERO_CERO;
               }

                //panel pasajes
                if (pnlPasajes.Visible)
                {
                    if ((txtPasajes.Text == null) | (txtPasajes.Text == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad de pasajes requerido');", true);
                        return;
                    }
                    else
                    {

                        if (clsFuncionesGral.IsNumeric(txtPasajes.Text))
                        {
                            //   error = false;
                            editado = false;
                            if (detalleComision.Pasaje != txtPasajes.Text) editado = true;
                            if (editado) detalleComision.Pasaje = txtPasajes.Text;
                        }
                        else
                        {
                            error = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cantidad debe seer númerico.');", true);
                            return;
                        }
                    }

                 /*   if ((txtPartidaPasaje.Text == null) | (txtPartidaPasaje.Text == Dictionary.CADENA_NULA) | (txtPartidaPasaje.Text == Dictionary.NUMERO_CERO))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida pasajes requerido');", true);
                        return;
                    }
                    else
                    {
                        //  error = false;
                        editado = false;
                        if (detalleComision.Partida_Pasaje != txtPartidaPasaje.Text) editado = true;
                        if (editado) detalleComision.Partida_Pasaje = txtPartidaPasaje.Text;

                    }*/
                }
                else
                {
                    detalleComision.Pasaje = Dictionary.NUMERO_CERO;
                }



                if ((txtHoraSalida == null) | (txtHoraSalida.Text == Dictionary.CADENA_NULA))
                {
                    error = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un horario para la comision');", true);
                    return;
                }
                else
                {
                    //    error = false;
                    editado = false;
                    if (detalleComision.Inicio_Comision != txtHoraSalida.Text) editado = true;
                    if (editado) detalleComision.Inicio_Comision = txtHoraSalida.Text; ;
                }

                if ((txtHoraRegreso == null) | (txtHoraRegreso.Text == Dictionary.CADENA_NULA))
                {
                    error = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un horario para la comision');", true);
                    return;
                }
                else
                {
                    //  error = false;
                    editado = false;
                    if (detalleComision.Fin_Comision != txtHoraRegreso.Text) editado = true;
                    if (editado) detalleComision.Fin_Comision = txtHoraRegreso.Text; ;
                }

                //Panel modificar Vehiculo oficial local 
                if (pnlVehiculo.Visible)
                {
                    if ((dplClaseAut.SelectedValue.ToString() == null) | (dplClaseAut.SelectedValue.ToString() == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una clase transporte');", true);
                        return;
                    }
                    else
                    {
                        //   error = false;
                        editado = false;
                        if (detalleComision.Clase_Aut != dplClaseAut.SelectedValue.ToString()) editado = true;
                        if (editado) detalleComision.Clase_Aut = dplClaseAut.SelectedValue.ToString();
                    }

                    if ((dplTipoTAUt.SelectedValue.ToString() == null) | (dplTipoTAUt.SelectedValue.ToString() == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un tipo de  transporte');", true);
                        return;
                    }
                    else
                    {
                        //   error = false;
                        editado = false;
                        if (detalleComision.Tipo_Aut != dplTipoTAUt.SelectedValue.ToString()) editado = true;
                        if (editado) detalleComision.Tipo_Aut = dplTipoTAUt.SelectedValue.ToString();
                    }

                    if ((dplVehiculo.SelectedValue.ToString() == null) | (dplVehiculo.SelectedValue.ToString() == Dictionary.CADENA_NULA))
                    {
                        error = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un vehiculo para transporte');", true);
                        return;
                    }
                    else
                    {
                        //   error = false;
                        editado = false;
                        if (detalleComision.Vehiculo_Autorizado != dplVehiculo.SelectedValue.ToString()) editado = true;
                        if (editado) detalleComision.Vehiculo_Autorizado = dplVehiculo.SelectedValue.ToString();
                    }

                    detalleComision.Ubicacion_Trans_Aut = Session["Crip_Ubicacion"].ToString();
                }
                else
                {
                    if ((detalleComision.Clase_Aut == null) | (detalleComision.Clase_Aut == Dictionary.CADENA_NULA))
                    {
                        detalleComision.Clase_Aut = detalleComision.Clase_Trans;
                        detalleComision.Tipo_Aut = detalleComision.Tipo_Trans;
                        detalleComision.Vehiculo_Autorizado = detalleComision.Vehiculo_Solicitado;
                        detalleComision.Ubicacion_Trans_Aut = detalleComision.Ubicacion_Transporte;
                    }

                }

                ///////////////////////
            }
            else if (Session["Crip_Opcion"].ToString() == Dictionary.AUTORIZA)
            {
                detalleComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", "");

                if (txtObservaciones.Text == Dictionary.CADENA_NULA)
                {
                    error = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Comentarios   para aprobar/ministrar esta Cómision obligatorios.');", true);
                    return;
                }
                else
                {
                    error = false;
                    detalleComision.Observaciones_Autoriza = txtObservaciones.Text;
                }
            }

            //update por unidad administrativa folio usuario y opcion
            if (!error)
            {
                if ((Session["Crip_Opcion"].ToString() == Dictionary.VOBO) | (MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL) == Dictionary.PERMISO_ADMINISTRADOR_LOCAL))
                {
                    //Datos ubicacion de solicitud
                    //   objProyecto = MngNegocioProyecto.ObtieneDatosProy(detalleComision.Dep_Proy, detalleComision.Proyecto);
                    int i = 1;

                    foreach (System.Web.UI.WebControls.GridViewRow r in GridView1.Rows)
                    {
                        string Nombre = r.Cells[0].Text.ToString();
                        string usuario = r.Cells[1].Text.ToString();
                        //  string adscripcion = r.Cells[2].Text.ToString();
                        //  string[] ads = new string[2];
                        // ads = adscripcion.Split(new Char[] { '-' });
                        Comision objComision = new Comision();

                        if (Session["Crip_Opcion"].ToString() == Dictionary.VOBO)
                        {
                            objComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), detalleComision.Ubicacion, usuario, "8");
                        }
                        else if (permisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL)
                        {
                            objComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), detalleComision.Ubicacion, usuario, "1");
                        }

                        Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(objComision.Ubicacion_Comisionado);
                        
                        Usuario objUsuario = MngNegocioUsuarios.Datos_Comisionado(objComision.Comisionado, objComision.Ubicacion_Comisionado);

                        //Calculo para tabla de ministraciones
                        //viaticos
                        //combustible
                        if (i == 1)
                        {
                            if ((detalleComision.Combustible_Autorizado == "") | (detalleComision.Combustible_Autorizado == null) | (detalleComision.Combustible_Autorizado == Dictionary.NUMERO_CERO))
                            {
                                objComision.Combustible_Autorizado = "0";
                            }
                            else
                            {
                                objComision.Combustible_Autorizado = detalleComision.Combustible_Autorizado;
                            }
                        }
                        else
                        {
                            objComision.Combustible_Autorizado = "0";
                        }

                        //peajes
                        if (i == 1)
                        {
                            if ((detalleComision.Peaje == "0") | (detalleComision.Peaje == Dictionary.CADENA_NULA) | (detalleComision.Peaje == null))
                            {
                                objComision.Peaje = "0";
                            }
                            else
                            {
                                objComision.Peaje = detalleComision.Peaje;
                            }
                        }
                        else
                        {
                            objComision.Peaje = "0";
                        }
                        //pasajes
                        if (i == 1)
                        {
                            if ((detalleComision.Pasaje == "0") | (detalleComision.Pasaje == null))
                            {
                                objComision.Pasaje = "0";
                            }
                            else
                            {
                                objComision.Pasaje = detalleComision.Pasaje;
                            }
                        }
                        else
                        {
                            objComision.Pasaje = Dictionary.NUMERO_CERO;
                        }

                        //viaticos
                        if ((detalleComision.Total_Viaticos != "0") | (detalleComision.Total_Viaticos != null))
                        {
                            objComision.Total_Viaticos = detalleComision.Total_Viaticos;
                        }
                        else
                        {
                            objComision.Total_Viaticos = Dictionary.NUMERO_CERO;
                        }

                        objComision.Inicio_Comision = detalleComision.Inicio_Comision;
                        objComision.Fin_Comision = detalleComision.Fin_Comision;
                        objComision.Fecha_Inicio = detalleComision.Fecha_Inicio;
                        objComision.Fecha_Final = detalleComision.Fecha_Final;
                        objComision.Dias_Reales = detalleComision.Dias_Reales;
                        objComision.Partida_Presupuestal = detalleComision.Partida_Presupuestal;
                        objComision.Clase_Aut = detalleComision.Clase_Aut;
                        objComision.Tipo_Aut = detalleComision.Tipo_Aut;
                        objComision.Vehiculo_Autorizado = detalleComision.Vehiculo_Autorizado;
                        objComision.Ubicacion_Trans_Aut = detalleComision.Ubicacion_Trans_Aut;
                        objComision.Origen_Aereo = detalleComision.Origen_Aereo;
                        objComision.Origen_Dest_Acu = detalleComision.Origen_Dest_Acu;
                        objComision.Origen_Terrestre = detalleComision.Origen_Terrestre;
                        // objComision.Combustible_Autorizado = DetalleComision.Combustible_Autorizado;
                        objComision.Partida_Combustible = detalleComision.Partida_Combustible;
                        objComision.Vale_Comb_I = detalleComision.Vale_Comb_I;
                        objComision.Vale_Comb_F = detalleComision.Vale_Comb_F;
                        objComision.Pago_Combustible = detalleComision.Pago_Combustible;
                        objComision.Partida_Peaje = detalleComision.Partida_Peaje;
                        objComision.Partida_Pasaje = detalleComision.Partida_Pasaje;

                        objComision.Zona_Comercial = detalleComision.Zona_Comercial;
                        objComision.Forma_Pago_Viaticos = detalleComision.Forma_Pago_Viaticos;
                        objComision.Tipo_Viaticos = detalleComision.Tipo_Viaticos;
                        objComision.Tipo_Pago_Viatico = detalleComision.Tipo_Pago_Viatico;
                        objComision.Total_Viaticos = detalleComision.Total_Viaticos;
                        objComision.Observaciones_Autoriza = detalleComision.Observaciones_Autoriza;
                        objComision.Observaciones_Vobo = detalleComision.Observaciones_Vobo;
                        objComision.Territorio = detalleComision.Territorio;

                        objComision.Dias_Rural = detalleComision.Dias_Rural;
                        objComision.Dias_Comercial = detalleComision.Dias_Comercial;
                        objComision.Dias_Navegados = detalleComision.Dias_Navegados;
                        objComision.Dias_50 = detalleComision.Dias_50;
                        objComision.Dias_Pagar = detalleComision.Dias_Pagar;
                        objComision.Singladuras = detalleComision.Singladuras;

                        objComision.Ubicacion_Comisionado = detalleComision.Ubicacion_Comisionado;


                        if (objProyecto.Dependencia == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objProyecto.Dependencia, "COMBUSTIBLES"))
                        {
                            if (objComision.Pago_Combustible == "1")
                            {
                                objComision.Combustible_Efectivo = objComision.Combustible_Autorizado;
                                objComision.Combustible_Vales = Dictionary.NUMERO_CERO;
                            }
                            else if (objComision.Pago_Combustible == "2")
                            {
                                objComision.Combustible_Efectivo = Dictionary.NUMERO_CERO;
                                objComision.Combustible_Vales = objComision.Combustible_Autorizado;
                            }
                            else if (objComision.Pago_Combustible == "3")
                            {
                                objComision.Combustible_Efectivo = txtCombustEfe.Text.Trim ();
                                objComision.Combustible_Vales = detalleComision.Combustible_Vales;
                            }

                        }
                        else
                        {
                            objComision.Combustible_Efectivo = detalleComision.Combustible_Efectivo;
                            objComision.Combustible_Vales = detalleComision.Combustible_Vales;
                        }

                        if (i > 1)
                        {
                            objComision.Peaje = Dictionary.NUMERO_CERO;
                            objComision.Pasaje = Dictionary.NUMERO_CERO;
                            objComision.Combustible_Autorizado = Dictionary.NUMERO_CERO;
                            objComision.Combustible_Efectivo = Dictionary.NUMERO_CERO;
                            objComision.Combustible_Vales = Dictionary.NUMERO_CERO;
                            objComision.Vale_Comb_I = Dictionary.CADENA_NULA;
                            objComision.Vale_Comb_F = Dictionary.CADENA_NULA;
                        }

                        bool autorizacion = MngNegocioComision.Update_Comision(objComision, Session["Crip_Opcion"].ToString(), Session["Crip_Folio"].ToString(), Session["Crip_Usuario"].ToString(), permisos, true);

                        if ((objComision.Zona_Comercial == "0") & ((objComision.Combustible_Efectivo == Dictionary.NUMERO_CERO) & (objComision.Peaje == Dictionary.NUMERO_CERO) & (objComision.Pasaje == Dictionary.NUMERO_CERO)))
                        {
                            objComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), detalleComision.Ubicacion, usuario, "5");
                            objComision.Estatus = "5";
                        }
                        else
                        {
                            objComision = MngNegocioComision.Detalle_Comision(Session["Crip_Folio"].ToString(), detalleComision.Ubicacion, usuario, "9");
                            objComision.Estatus = "9";
                        }

                        clsPdf.Genera_Oficio_Comision(objComision, oUbicacion, objProyecto, clsFuncionesGral.ConvertMayus(Session["Crip_Abreviatura"].ToString() + "  " + Session["Crip_Nombre"].ToString() + "  " + Session["Crip_ApPat"].ToString() + "  " + Session["Crip_ApMat"].ToString()), Session["Crip_Cargo"].ToString(), true);
                     
                        
                        //clsPdf.Genera_Oficio_Comision(objComision, oUbicacion, objProyecto, clsFuncionesGral.ConvertMayus(lsAbreviatura + "  " + lsNombre + "  " + lsApPat + "  " + lsApMat), lsCargo);
                        /*
                        if (objComision.Pasaje == "0")
                        {
                            objComision.Pasaje = "0";
                        }
                        else
                        {
                            MngNegocioMinistracion.Inserta_Ministracion(MngNegocioMinistracion.Obtiene_Max_Ministracion(), Session["Crip_Usuario"].ToString(), "", Session["Crip_Ubicacion"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, "CRIPSC08", Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Comisionado, objUsuario.RFC, objComision.Archivo, lsHoy, "8", objComision.Pasaje, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Pasaje, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, lsHoy, objProyecto.Componente, objComision.Partida_Pasaje);
                        }

                        if ((objComision.Total_Viaticos != "0") | (objComision.Total_Viaticos != null))
                        {
                            MngNegocioMinistracion.Inserta_Ministracion(MngNegocioMinistracion.Obtiene_Max_Ministracion(), Session["Crip_Usuario"].ToString(), "", Session["Crip_Ubicacion"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, "CRIPSC08", Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Comisionado, objUsuario.RFC, objComision.Archivo, lsHoy, "11", objComision.Total_Viaticos, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Total_Viaticos, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, lsHoy, objProyecto.Componente, objComision.Partida_Presupuestal);
                        }

                        if (i == 1)
                        {
                            if ((objComision.Combustible_Autorizado != "0") & (objComision.Combustible_Efectivo != null ))
                             {
                                 //MngNegocioMinistracion.Inserta_Ministracion(MngNegocioMinistracion.Obtiene_Max_Ministracion(), Session["Crip_Usuario"].ToString(), "", Session["Crip_Ubicacion"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, "CRIPSC08", Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Comisionado, objUsuario.RFC, objComision.Archivo, lsHoy, "6", , Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(objComision.Combustible_Autorizado) - clsFuncionesGral.Convert_Double(vale))), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, lsHoy, objProyecto.Componente, objComision.Partida_Combustible);
                                 MngNegocioMinistracion.Inserta_Ministracion(MngNegocioMinistracion.Obtiene_Max_Ministracion(), Session["Crip_Usuario"].ToString(), "", Session["Crip_Ubicacion"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, "CRIPSC08", Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Comisionado, objUsuario.RFC, objComision.Archivo,lsHoy ,"6",objComision.Combustible_Efectivo ,Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO,Dictionary.NUMERO_CERO,objComision .Combustible_Efectivo  , Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, lsHoy, objProyecto.Componente, objComision.Partida_Combustible); 
                            }

                            if (objComision.Peaje != "0")
                            {
                                MngNegocioMinistracion.Inserta_Ministracion(MngNegocioMinistracion.Obtiene_Max_Ministracion(), Session["Crip_Usuario"].ToString(), "", Session["Crip_Ubicacion"].ToString(), objProyecto.Clv_Proy, objProyecto.Dependencia, "CRIPSC08", Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Comisionado, objUsuario.RFC, objComision.Archivo, lsHoy, "7", objComision.Peaje, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, objComision.Peaje, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, lsHoy, objProyecto.Componente, objComision.Partida_Peaje);
                            }
                        }
                        */
                        MngNegocioComision.Insert_Detalle_Comision(objComision);

                        i += 1;

                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha autorizado/ministrado correctamente la solicitud de comision número :" + Session["Crip_Folio"].ToString() + " , para iniciar tramite de viaticos correspondiente');", true);
                    Crear_Tabla(permisos);
                    //Obtener_TablaActualizada();
                    pnlDetalle.Visible = false;
                    //Al ultimo pa que se usen los valores
                    Destructor();
                }
                else
                {
                    bool autorizacion = MngNegocioComision.Update_Comision(detalleComision, Session["Crip_Opcion"].ToString(), Session["Crip_Folio"].ToString(), Session["Crip_Usuario"].ToString(), permisos);
                    //Solo messaje de  confirmacion para visto bueno 


                    List<Entidades.GridView> listaGrid = new List<Entidades.GridView>();

                    foreach (System.Web.UI.WebControls.GridViewRow r in GridView1.Rows)
                    {
                        Entidades.GridView gv = new Entidades.GridView();
                        gv.Comisionado = r.Cells[0].Text.ToString();
                        gv.RFC = r.Cells[1].Text.ToString();
                        gv.Adscripcion = r.Cells[2].Text.ToString();
                        listaGrid.Add(gv);

                    }

                    List<Entidad> ListaAutoriza = MngNegocioMail.Obtiene_Mail("VoBo", Convert.ToString(detalleComision.Folio), Session["Crip_Ubicacion"].ToString());

                    foreach (Entidad l in ListaAutoriza)
                    {
                        if (l.Codigo != "")
                        {
                            string TipoMail = "VOBO";
                            bool MailBandera = false;
                            Usuario usu = MngNegocioUsuarios.Obten_Datos(l.Codigo, true);
                            // lulu
                            bool envio_mail = clsMail.Mail_Comision(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), objProyecto.Dependencia, usu.Email, usu.Nombre, Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Ubicacion_Transporte, detalleComision.Vehiculo_Solicitado, detalleComision.Descrip_vehiculo, Convert.ToString(detalleComision.Folio), detalleComision.Fecha_Inicio, detalleComision.Fecha_Final, detalleComision.Lugar, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);

                            usu = null;
                            usu = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), true);

                            if (l.Codigo == usu.Usser )
                            {
                                usu = null;    
                                usu = MngNegocioUsuarios.Obten_Datos(Dictionary.USUARIO_MINISTRADOR , true);
                                envio_mail = clsMail.Mail_Comision(Session["Crip_Secretaria"].ToString(), Session["Crip_Organismo"].ToString(), Session["Crip_Ubicacion"].ToString(), objProyecto.Dependencia, usu.Email, usu.Nombre, Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString(), listaGrid, detalleComision.Clase_Trans, detalleComision.Tipo_Trans, detalleComision.Ubicacion_Transporte, detalleComision.Vehiculo_Solicitado, detalleComision.Descrip_vehiculo, Convert.ToString(detalleComision.Folio), detalleComision.Fecha_Inicio, detalleComision.Fecha_Final, detalleComision.Lugar, objProyecto.Responsable, objProyecto.Descripcion, TipoMail, Session["Crip_Rol"].ToString(), Session["Crip_Cargo"].ToString(), MailBandera);
                            }
                        }
                    }




                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha autorizado correctamente la solicitud de comision número :" + Session["Crip_Folio"].ToString() + "');", true);
                    Crear_Tabla(permisos);
                    //  Obtener_TablaActualizada();
                    pnlDetalle.Visible = false;
                    Destructor();
                }
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if ((txtObservaciones.Text == "") | (txtObservaciones.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe anotar motivo de cancelacion de comision en campo observaciones');", true);
                return;
            }
            else
            {
                MngNegocioComision.Update_estatus_Comision("0", "", Session["Crip_Folio"].ToString(), Session["Crip_UbicacionAut"].ToString(), "", txtObservaciones.Text, true);
                pnlDetalle.Visible = false;
                Crear_Tabla(MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ADMINISTRADOR_LOCAL));
                //  Obtener_TablaActualizada();

            }
        }
    }
}