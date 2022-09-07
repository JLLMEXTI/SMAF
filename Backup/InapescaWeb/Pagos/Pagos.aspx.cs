﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;
using InapescaWeb.Entidades;

namespace InapescaWeb.Pagos
{
    public partial class Pagos : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static readonly string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
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

        public void Carga_Valores()
        {
            clsFuncionesGral.Activa_Paneles(pnlPagos, false);
            clsFuncionesGral.Llena_Lista(dplTipoPago, clsFuncionesGral.ConvertMayus("cheque|deposito a cuenta"));

            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            Label6.Text = clsFuncionesGral.ConvertMayus("Oficios de comison pendientes a pagar");
            Label1.Text = clsFuncionesGral.ConvertMayus("Num. Oficio : ");
            Label2.Text = clsFuncionesGral.ConvertMayus("Adscripcion :");
            btnBuscar.Text = clsFuncionesGral.ConvertMayus("Buscar comision a pagar");
            chkBusquedaManual.Text = clsFuncionesGral.ConvertMayus("Busqueda por oficio");

            Label3.Text = clsFuncionesGral.ConvertMayus("Num. Oficio :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Adscripcion del Comisionado :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Captura de Pagos de Viaticos");

            Label7.Text = clsFuncionesGral.ConvertMayus("ministracion número");
            Label77.Text = clsFuncionesGral.ConvertMayus("Tipo Ministracion");
            Label8.Text = clsFuncionesGral.ConvertMayus("tipo de pago");
            Label9.Text = clsFuncionesGral.ConvertMayus("referencia bancaria");
            Label10.Text = clsFuncionesGral.ConvertMayus("banco destino");
            Label11.Text = clsFuncionesGral.ConvertMayus("cuenta destino ");
            Label12.Text = clsFuncionesGral.ConvertMayus("clabe destino ");
            Label29.Text = clsFuncionesGral.ConvertMayus("Fecha Pago:");

            Label26.Text = clsFuncionesGral.ConvertMayus("banco Origen");
            Label27.Text = clsFuncionesGral.ConvertMayus("Cuenta Origen");
            Label28.Text = clsFuncionesGral.ConvertMayus("clabe origen");

            Label13.Text = clsFuncionesGral.ConvertMayus("viaticos pagados a la fecha :");
 Label14.Text = clsFuncionesGral.ConvertMayus("Nuevo Pago :");
 Label15.Text = clsFuncionesGral.ConvertMayus("Partida Viaticos:");
            Label17.Text = clsFuncionesGral.ConvertMayus("Combustible pagado a la fecha :");
             Label18.Text = clsFuncionesGral.ConvertMayus("Nuevo Pago :");
             Label19.Text = clsFuncionesGral.ConvertMayus("partida combustibles : ");

            Label21.Text = clsFuncionesGral.ConvertMayus("peaje pagado a la fecha : ");
            Label22.Text = clsFuncionesGral.ConvertMayus("Nuevo Pago :");
 Label23.Text = clsFuncionesGral.ConvertMayus("partida peajes :");


            Label25.Text = clsFuncionesGral.ConvertMayus("pasaje pagado a la fecha :");
            Label30.Text = clsFuncionesGral.ConvertMayus("Nuevo Pago :");
            Label31.Text = clsFuncionesGral.ConvertMayus("partida pasajes :");

            Label34.Text = clsFuncionesGral.ConvertMayus("Singladuras pagadas a la fecha :");
            Label36.Text = clsFuncionesGral.ConvertMayus("Nuevo Pago :");
            Label37.Text = clsFuncionesGral.ConvertMayus("partida singladuras");
            
            btnPagar.Text = clsFuncionesGral.ConvertMayus("Registrar PagoS");
            clsFuncionesGral.Activa_Paneles(pnlManual, false);
            clsFuncionesGral.Activa_Paneles(pnlListas, true, true);

            if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
            {
                //llenar datos de oficios en items
                dplAdscripcion.DataSource = MngNegocioAdscripcion.Obtiene_Adscripcion_Ejecutante(Session["Crip_Ubicacion"].ToString(), Year);
                dplAdscripcion.DataTextField = Dictionary.DESCRIPCION;
                dplAdscripcion.DataValueField = Dictionary.CODIGO;
                dplAdscripcion.DataBind();


                dplAdscripcion1.DataSource = MngNegocioAdscripcion.Obtiene_Adscripcion_Ejecutante(Session["Crip_Ubicacion"].ToString(), Year);
                dplAdscripcion1.DataTextField = Dictionary.DESCRIPCION;
                dplAdscripcion1.DataValueField = Dictionary.CODIGO;
                dplAdscripcion1.DataBind();

            }
            else
            {
                //llenar adscripciones
                dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion.DataTextField = "Descripcion";
                dplAdscripcion.DataValueField = "Codigo";
                dplAdscripcion.DataBind();

                dplAdscripcion1.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion1.DataTextField = "Descripcion";
                dplAdscripcion1.DataValueField = "Codigo";
                dplAdscripcion1.DataBind();

                dplOficios.Items.Clear();
            }
        }

        protected void dplAdscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            if (dplAdscripcion.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccionar una adscripcion');", true);
                return;
            }
            else
            {

                if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                {
                    dplOficios.DataSource = MngNegocioPago.ListaComisionesPagar(lsAdscripcion, Session["Crip_Ubicacion"].ToString(), Year);
                    dplOficios.DataTextField = Dictionary.DESCRIPCION;
                    dplOficios.DataValueField = Dictionary.CODIGO;
                    dplOficios.DataBind();
                }
                else
                {
                    dplOficios.DataSource = MngNegocioPago.ListaComisionesPagar(lsAdscripcion, "", Year, true);
                    dplOficios.DataTextField = Dictionary.DESCRIPCION;
                    dplOficios.DataValueField = Dictionary.CODIGO;
                    dplOficios.DataBind();
                }
            }
        }

        protected void chkBusquedaManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBusquedaManual.Checked)
            {
                clsFuncionesGral.Activa_Paneles(pnlManual, true);
                clsFuncionesGral.Activa_Paneles(pnlListas, true, false);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlManual, false);
                clsFuncionesGral.Activa_Paneles(pnlListas, true, true);
                txtNumOficio.Text = Dictionary.CADENA_NULA;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string[] lsCadena;

            string archivo = "";
            string Usuario = "";

            clsFuncionesGral.Activa_Paneles(pnlPagos, true);
            Comision DetalleComision = new Comision();
            ComisionDetalle oCdetalle = new ComisionDetalle();

            clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
            clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
            clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
            clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
            clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);

            if (chkBusquedaManual.Checked)
            {
                if ((txtNumOficio.Text == "") | (txtNumOficio.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ingrese un numero de oficio');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtNumOficio.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Numero de oficio debe ser numerico');", true);
                    return;
                }

                if (dplAdscripcion1.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adscripcion del comisionado');", true);
                    return;
                }

                DetalleComision = MngNegocioComision.Detalle_Comision_Reimpresion(txtNumOficio.Text, dplAdscripcion1.SelectedValue.ToString(),Year );

                oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);

                if ((oCdetalle.Oficio == null) | (oCdetalle.Oficio == ""))
                {
                    bool insertDetalle = MngNegocioComision.Insert_Detalle_Comision(DetalleComision);
                }


                //valida si es admin o jefe de centro y que si puede pagara esta comision
                if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                {
                    if (DetalleComision.Dep_Proy != Session["Crip_Ubicacion"].ToString())
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision utuliza recurso de otro programa adscrito a una unidad aministrativa por lo que no podra realizar pagos en ella');", true);
                        return;

                    }
                }
                
                lblNombre.Text = clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(DetalleComision.Comisionado));
                lblLugarComison.Text = clsFuncionesGral.ConvertMayus(DetalleComision.Lugar);
                if (clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Inicio) == clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Final))
                {
                    lblPeriodo.Text = clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Inicio);
                }
                else
                {
                    lblPeriodo.Text = "Del " + clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Inicio) + " al " + clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Final);
                }

                lblObjetivo.Text = clsFuncionesGral.ConvertMayus(DetalleComision.Objetivo);

                //SOLICITADO
                Label16.Text = clsFuncionesGral.ConvertMayus("Viaticos SOLICITADOS: $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Total_Viaticos);
                Label20.Text = clsFuncionesGral.ConvertMayus("coMBUSTIBLE SOLICITADOS : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Combustible_Efectivo);
                Label24.Text = clsFuncionesGral.ConvertMayus("Peaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Peaje);
                Label32.Text = clsFuncionesGral.ConvertMayus("Pasaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Pasaje);
                Label33.Text = clsFuncionesGral.ConvertMayus("Singladuras solicitadas : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Singladuras);

                //OTORGADOS
                lblViaticos.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Viaticos);
                lblCombust.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Combustible);
                lblPeaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Peaje);
                lblPasaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Pasaje);
                lblSingladuras.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Singladuras);

                if (((DetalleComision.Zona_Comercial == "0") & ((DetalleComision.Combustible_Efectivo == Dictionary.NUMERO_CERO) & (DetalleComision.Peaje == Dictionary.NUMERO_CERO) & (DetalleComision.Pasaje == Dictionary.NUMERO_CERO))))
                {
                    oCdetalle = null;
                    DetalleComision = null;
                    clsFuncionesGral.Activa_Paneles(pnlPagos, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta comisiona fue sin viaticos, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                    return;

                }
                else if (oCdetalle.Financieros == "2")
                {

                    oCdetalle = null;
                    DetalleComision = null;
                    clsFuncionesGral.Activa_Paneles(pnlPagos, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision ya fue pagada, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                    return;
                }
                else
                {
                    List<Entidad> ListaMinistracion = MngNegocioMinistracion.Obtiene_Ministracion(Year, DetalleComision.Archivo.Replace(".pdf",""));

                    if (ListaMinistracion.Count > 0)
                    {
                        dplMinistracion.DataSource = ListaMinistracion;
                        dplMinistracion.DataTextField = Dictionary.DESCRIPCION;
                        dplMinistracion.DataValueField = Dictionary.CODIGO;
                        dplMinistracion.DataBind();

                        dplTipoMinistracion.DataSource = MngNegocioMinistracion.ListaTipoMinistracion(" ");
                        dplTipoMinistracion.DataValueField = Dictionary.CODIGO;
                        dplTipoMinistracion.DataTextField = Dictionary.DESCRIPCION;
                        dplTipoMinistracion.DataBind();
                        Session["Crip_IsExistinMinistracion"] = "1";
                    }
                    else
                    {
                        ListaMinistracion = null;
                        clsFuncionesGral.Llena_Lista(dplMinistracion, clsFuncionesGral.ConvertMayus(" = s e l e c c i o n e= |Nueva Ministracion"));
                        Session["Crip_IsExistinMinistracion"] = "0";
                    }

                    clsFuncionesGral.Activa_Paneles(pnlPagos, true);

                    oCdetalle = null;
                    DetalleComision = null;
                }
            }
            else //busqueda de listas
            {
                if (dplAdscripcion.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una aadscripcion');", true);
                    return;
                }
                else
                {

                    lsCadena = dplOficios.SelectedValue.ToString().Split(new Char[] { '|' });
                    archivo = lsCadena[0];
                    Usuario = lsCadena[1];

                    if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                    {
                        DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", Session["Crip_Ubicacion"].ToString(), Usuario,true );
                    }
                    else
                    {
                        DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf",dplAdscripcion.SelectedValue.ToString (), Usuario,true );
                    }


                    oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);

                    if ((oCdetalle.Oficio == null) | (oCdetalle.Oficio == ""))
                    {
                        bool insertDetalle = MngNegocioComision.Insert_Detalle_Comision(DetalleComision);
                    }

                    lblNombre.Text = clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(DetalleComision.Comisionado));
                    lblLugarComison.Text = clsFuncionesGral.ConvertMayus(DetalleComision.Lugar);
                   
                    if (clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Inicio) == clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Final))
                    {
                        lblPeriodo.Text = clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Inicio);
                    }
                    else
                    {
                        lblPeriodo.Text = "Del " + clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Inicio) + " al " + clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Final);
                    }

                    lblObjetivo.Text = clsFuncionesGral.ConvertMayus(DetalleComision.Objetivo);

                    //SOLICITADO
                    Label16.Text = clsFuncionesGral.ConvertMayus("Viaticos SOLICITADOS: $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Total_Viaticos);
                    Label20.Text = clsFuncionesGral.ConvertMayus("coMBUSTIBLE SOLICITADOS : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Combustible_Efectivo);
                    Label24.Text = clsFuncionesGral.ConvertMayus("Peaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Peaje);
                    Label32.Text = clsFuncionesGral.ConvertMayus("Pasaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Pasaje);
                    Label33.Text = clsFuncionesGral.ConvertMayus("Singladuras solicitadas : $ ") + clsFuncionesGral.Convert_Decimales(DetalleComision.Singladuras);


                    //OTORGADOS

                    lblViaticos.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Viaticos);
                    lblCombust.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Combustible);
                    lblPeaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Peaje);
                    lblPasaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Pasaje);
                    lblSingladuras.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Singladuras );


                    if (((DetalleComision.Zona_Comercial == "0") & ((DetalleComision.Combustible_Efectivo == Dictionary.NUMERO_CERO) & (DetalleComision.Peaje == Dictionary.NUMERO_CERO) & (DetalleComision.Pasaje == Dictionary.NUMERO_CERO))))
                    {
                        oCdetalle = null;
                        DetalleComision = null;
                        clsFuncionesGral.Activa_Paneles(pnlPagos, false);
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta comision se realizo sin viaticos, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                        return;

                    }
                    else if (oCdetalle.Financieros == "2")
                    {

                        oCdetalle = null;
                        DetalleComision = null;
                        clsFuncionesGral.Activa_Paneles(pnlPagos, false);
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision ya fue pagada, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                        return;
                    }
                    else
                    {
                        List<Entidad> ListaMinistracion = MngNegocioMinistracion.Obtiene_Ministracion(Year, DetalleComision.Archivo.Replace(".pdf", ""));

                        if (ListaMinistracion.Count > 0)
                        {
                            dplMinistracion.DataSource = ListaMinistracion;
                            dplMinistracion.DataTextField = Dictionary.DESCRIPCION;
                            dplMinistracion.DataValueField = Dictionary.CODIGO;
                            dplMinistracion.DataBind();

                            dplTipoMinistracion.DataSource = MngNegocioMinistracion.ListaTipoMinistracion(" ");
                            dplTipoMinistracion.DataValueField = Dictionary.CODIGO;
                            dplTipoMinistracion.DataTextField = Dictionary.DESCRIPCION;
                            dplTipoMinistracion.DataBind();
                            Session["Crip_IsExistinMinistracion"] = "1";
                        }
                        else
                        {
                            ListaMinistracion = null;
                            clsFuncionesGral.Llena_Lista(dplMinistracion, clsFuncionesGral.ConvertMayus(" = s e l e c c i o n e= |Nueva Ministracion"));
                            Session["Crip_IsExistinMinistracion"] = "0";
                        }
                        clsFuncionesGral.Activa_Paneles(pnlPagos, true);

                        oCdetalle = null;
                       DetalleComision = null;
                    }
                }
            }


        }

        protected void dplMinistracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsMinistracion = dplMinistracion.SelectedValue.ToString();
            string Is_Exist = Session["Crip_IsExistinMinistracion"].ToString();

            if (Is_Exist == "0")
            {
                if (lsMinistracion == "1")
                {
                    dplTipoMinistracion.Items.Clear();
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un tipo de ministracion');", true);
                    return;
                }
                else
                {
                    dplTipoMinistracion.DataSource = MngNegocioMinistracion.ListaTipoMinistracion("00");
                    dplTipoMinistracion.DataValueField = Dictionary.CODIGO;
                    dplTipoMinistracion.DataTextField = Dictionary.DESCRIPCION;
                    dplTipoMinistracion.DataBind();
                }

            }
            else if (Is_Exist == "1")
            {
                dplTipoMinistracion.DataSource = MngNegocioMinistracion.ListaTipoMinistracion(" ");
                dplTipoMinistracion.DataValueField = Dictionary.CODIGO;
                dplTipoMinistracion.DataTextField = Dictionary.DESCRIPCION;
                dplTipoMinistracion.DataBind();
            }
        }

        protected void dplBancoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string banco = dplBancoOrigen.SelectedValue.ToString();
            CuentasBancarias ListaCuentas = MngNegocioCuentas.ListaCuentas(Session["Crip_Ubicacion"].ToString(), banco);

            txtCuentaOrigen .Text = ListaCuentas.Cuenta;
            txtClabeOrigen.Text = ListaCuentas.Clabe;
        }

        protected void dplTipoMinistracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsTipoMinistracion = dplTipoMinistracion.SelectedValue.ToString();
            string[] lsCadena;

            string archivo = "";
            string Usuario = "";
            clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
            clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
            clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
            clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
            clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
            Comision DetalleComision = new Comision();
            ComisionDetalle oCdetalle = new ComisionDetalle();
            Ministracion oMinistracion = new Ministracion();

            if (lsTipoMinistracion == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccionar tipo de Ministracion');", true);
                return;
            }
            else
            {
                if (Session["Crip_IsExistinMinistracion"].ToString() == "0")//no existe 
                {
                    if (chkBusquedaManual.Checked)
                    {
                        DetalleComision = MngNegocioComision.Detalle_Comision_Reimpresion(txtNumOficio.Text, dplAdscripcion1.SelectedValue.ToString(),Year );
                        oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);
                    }
                    else
                    {
                        lsCadena = dplOficios.SelectedValue.ToString().Split(new Char[] { '|' });
                        archivo = lsCadena[0];
                        Usuario = lsCadena[1];

                        if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                        {
                            DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", Session["Crip_Ubicacion"].ToString(), Usuario);
                        }
                        else
                        {
                            DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", dplAdscripcion.SelectedValue.ToString(), Usuario, true);
                        }

                        oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);

                    }

                    txtBancoOrigen.Visible = false;
                    dplBancoOrigen.Visible = true;
                    if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                    {
                        dplBancoOrigen.DataSource = MngNegocioCuentas.ListaBancos(Session["Crip_Ubicacion"].ToString());
                    }
                    else
                    {
                        dplBancoOrigen.DataSource = MngNegocioCuentas.ListaBancos("",true );
                    }

                    dplBancoOrigen.DataValueField = Dictionary.CODIGO;
                    dplBancoOrigen.DataTextField = Dictionary.DESCRIPCION;
                    dplBancoOrigen.DataBind();

                    if ((DetalleComision.Total_Viaticos != "") | (DetalleComision.Total_Viaticos != null))
                    {
                        if (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) > 0)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlViaticos, true);
                        }
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                    }

                    if ((DetalleComision.Combustible_Efectivo != "") | (DetalleComision.Combustible_Efectivo != null))
                    {
                        if (clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) > 0)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                        }

                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                    }

                    if ((DetalleComision.Peaje != "") | (DetalleComision.Peaje != null))
                    {
                        if (clsFuncionesGral.Convert_Double(DetalleComision.Peaje) > 0)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlPeaje, true);
                        }
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    }

                    if ((DetalleComision.Pasaje != "") | (DetalleComision.Pasaje != null))
                    {
                        if (clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) > 0)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlPasajes, true);
                        }
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    }

                    if ((DetalleComision.Singladuras != "") | (DetalleComision.Singladuras != null))
                    {
                        if (clsFuncionesGral.Convert_Double(DetalleComision.Singladuras) > 0)
                        {
                            clsFuncionesGral.Activa_Paneles(pnlSingladuras, true);
                        }
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                    }
                    oCdetalle = null;
                    DetalleComision = null;
                }
                else if (Session["Crip_IsExistinMinistracion"].ToString() == "1")//si existe en ministracion
                {
                    if (chkBusquedaManual.Checked)
                    {
                        DetalleComision = MngNegocioComision.Detalle_Comision_Reimpresion(txtNumOficio.Text, dplAdscripcion1.SelectedValue.ToString(),Year );
                        oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);
                          //  oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo .Replace (".pdf",""), dplMinistracion .SelectedValue.ToString(), "", true );
                    }
                    else
                    {
                        lsCadena = dplOficios.SelectedValue.ToString().Split(new Char[] { '|' });
                        archivo = lsCadena[0];
                        Usuario = lsCadena[1];

                        if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                        {
                            DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", Session["Crip_Ubicacion"].ToString(), Usuario);
                        }
                        else
                        {
                            DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", dplAdscripcion.SelectedValue.ToString(), Usuario, true);
                        }

                        oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);
                    }


                    txtBancoOrigen.Visible = false;
                    dplBancoOrigen.Visible = true;
                    if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                    {
                        dplBancoOrigen.DataSource = MngNegocioCuentas.ListaBancos(Session["Crip_Ubicacion"].ToString());
                    }
                    else
                    {
                        dplBancoOrigen.DataSource = MngNegocioCuentas.ListaBancos("", true);
                    }

                    dplBancoOrigen.DataValueField = Dictionary.CODIGO;
                    dplBancoOrigen.DataTextField = Dictionary.DESCRIPCION;
                    dplBancoOrigen.DataBind();

                    List<Entidad> lista = MngNegocioMinistracion.ListaMinistracionesSolicitadas(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString());

                    foreach (Entidad ent in lista)
                    {
                        if ((ent.Codigo == "17"))
                        {
                            clsFuncionesGral.Activa_Paneles(pnlViaticos, true);
                        }
                        else
                        {
                            //  clsFuncionesGral.Activa_Paneles(pnlViaticos, false );
                        }

                        if (ent.Codigo == "6")
                        {
                            clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                        }
                        else
                        {
                            //    clsFuncionesGral.Activa_Paneles(pnlCombustible, false );
                        }

                        if (ent.Codigo == "7")
                        {
                            clsFuncionesGral.Activa_Paneles(pnlPeaje, true);
                        }
                        else
                        {
                            //  clsFuncionesGral.Activa_Paneles(pnlPeaje, false );
                        }

                        if (ent.Codigo == "8")
                        {
                            clsFuncionesGral.Activa_Paneles(pnlPasajes, true);
                        }
                        else
                        {
                            //      clsFuncionesGral.Activa_Paneles(pnlPasajes, false );
                        }
                    }

                    oCdetalle = null;
                    DetalleComision = null;

                }
            }
        }
        
        //action de btoon que registra pagos
        protected void btnPagar_Click(object sender, EventArgs e)
        {
            string lsMinistracion = "", lsTipoMinistracion = "", lsFormaPago = "", lsBanco = "", lsCuenta = "", lsClabe = "", lsReferencia = "", lsbancOrigen = "", lsCuentaOrigen = "", lsclabeOrigen = "", lsFecha = "";

            string[] lsCadena;
            string archivo = "";
            string Usuario = "";
            Comision DetalleComision = new Comision();
            ComisionDetalle oCdetalle = new ComisionDetalle();
            Ministracion oMinistracion = new Ministracion();

            if (pnlViaticos.Visible)
            {
                if ((txtViatpag.Text == "") | (txtViatpag.Text == ""))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un valor de viaticos a pagar');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtViatpag.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de viaticos debe ser numerico.');", true);
                    return;
                }

                if ((txtPartidaViaticos.Text == "") | (txtPartidaViaticos.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida de Viaticos obligatoria');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtViatpag.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de  partida de viaticos debe ser numerico.');", true);
                    return;
                }
            }


            if (pnlCombustible.Visible)
            {
                if ((txtCombust.Text == "") | (txtCombust.Text == ""))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un valor de combustible en efectivo a pagar');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtCombust.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de combustible en efectivo debe ser numerico.');", true);
                    return;
                }

                if ((txtPartidaCom.Text == "") | (txtPartidaCom.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida de combustibles obligatoria');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtPartidaCom.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de  partida de combustibles debe ser numerico.');", true);
                    return;
                }
            }

            if (pnlPeaje.Visible)
            {
                if ((txtPeajePag.Text == "") | (txtPeajePag.Text == ""))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un valor de peajes  a pagar');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtPeajePag.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Peajes debe ser numerico.');", true);
                    return;
                }

                if ((txtPartidaPeaje.Text == "") | (txtPartidaPeaje.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida de Peajes obligatoria');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtPartidaPeaje.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de  partida de Peajes debe ser numerico.');", true);
                    return;
                }
            }

            if (pnlPasajes.Visible)
            {
                if ((txtPasaje.Text == "") | (txtPasaje.Text == ""))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un valor de Pasajes a pagar');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtPasaje.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de Pasajes debe ser numerico.');", true);
                    return;
                }

                if ((txtPartPasaje.Text == "") | (txtPartPasaje.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida de Pasajes obligatoria');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtPartPasaje.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de  partida de Pasajes debe ser numerico.');", true);
                    return;
                }
            }

            if (pnlSingladuras.Visible)
            {
                if ((txtSingladuras.Text == "") | (txtSingladuras.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar un valor de singladuras a pagar');", true);
                    return;
                }

                if(!clsFuncionesGral.IsNumeric (txtSingladuras .Text ))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de singladuras debe ser numerico.');", true);
                    return;
                }

                if ((txtPartSingladuras.Text == "") | (txtPartSingladuras.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Partida de singladuras obligatoria');", true);
                    return;
                }

                if (!clsFuncionesGral.IsNumeric(txtPartSingladuras.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de  partida de singladuras debe ser numerico.');", true);
                    return;
                }

            }


            //validacion de campos generales
            if (Session["Crip_IsExistinMinistracion"].ToString() == "0")
            {
                if (dplMinistracion.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una ministracion');", true);
                    return;
                }
                else
                {
                    lsMinistracion = MngNegocioMinistracion.Obtiene_Max_Ministracion();
                }
            }
            else if (Session["Crip_IsExistinMinistracion"].ToString() == "1")
            {
                lsMinistracion = dplMinistracion.SelectedValue.ToString();
            }

            if (dplTipoMinistracion.Items.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar una ministracion, para cargar tipos ');", true);
                return;
            }
            else
            {
                if (dplTipoMinistracion.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un tipo  ministracion');", true);
                    return;
                }
                else
                {
                    lsTipoMinistracion = dplTipoMinistracion.SelectedValue.ToString();
                }
            }

            lsFormaPago = dplTipoPago.SelectedValue.ToString();

            if ((txtBanco.Text == null) | (txtBanco.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Banco destino Obligatorio');", true);
                return;
            }
            else
            {
                lsBanco = txtBanco.Text;
            }

            if ((txtCuenta.Text == "") | (txtCuenta.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cuenta destino Obligatorio');", true);
                return;
            }
            else
            {
                lsCuenta = txtCuenta.Text;
            }

            if ((txtClabe.Text == null) | (txtClabe.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Clabe destino Obligatorio');", true);
                return;
            }
            else
            {
                lsClabe = txtClabe.Text;
            }
            if ((txtReferencia.Text == null) | (txtReferencia.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Referencia bancaria Obligatorio');", true);
                return;
            }
            else
            {
                lsReferencia = txtReferencia.Text;
            }

            lsFecha = clsFuncionesGral.FormatFecha(txtFecha.Text);

            if (dplBancoOrigen.Items.Count > 0)
            {
                if (dplBancoOrigen.SelectedValue.ToString() == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Banco Origen  Obligatorio');", true);
                    return;
                }
                else
                {
                    lsbancOrigen = dplBancoOrigen.SelectedItem.Text;
                }
            }
            else
            {
                lsbancOrigen = txtBancoOrigen.Text;
            }

            lsCuentaOrigen = txtCuentaOrigen.Text;
            lsclabeOrigen = txtClabeOrigen.Text; ;
            
            //inicio de insert
            if (chkBusquedaManual.Checked)
            {
                DetalleComision = MngNegocioComision.Detalle_Comision_Reimpresion(txtNumOficio.Text, dplAdscripcion1.SelectedValue.ToString(),Year );
                oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);
            }
            else
            {
                lsCadena = dplOficios.SelectedValue.ToString().Split(new Char[] { '|' });
                archivo = lsCadena[0];
                Usuario = lsCadena[1];

                if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                {
                    DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", Session["Crip_Ubicacion"].ToString(), Usuario);
                }
                else
                {
                    DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", dplAdscripcion.SelectedValue.ToString(), Usuario, true);
                }

                oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);
            }

            //pagos
            if (Session["Crip_IsExistinMinistracion"].ToString() == "0")
            {
                string lsEstatusFinancieros;
                double viat = 0, peaje = 0, pasaje = 0, combustible = 0;

                //insertar nueva ministracion detalle  y ministracion
                bool insert = MngNegocioMinistracion.Inserta_MInistracionDetalle(dplTipoMinistracion.SelectedValue.ToString(), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), "VIATICOS NACIONALES PARA LABORES EN CAMPO Y DE SUPERVISION", clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), "0", "1", "1", lsHoy, MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), "", "", "", "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);

                insert = false;
                Usuario objUsuario = MngNegocioUsuarios.Datos_Comisionado(DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);
                Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(DetalleComision.Dep_Proy, DetalleComision.Proyecto);
                double sec = 1;

                if (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) > 0)
                {
                    insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "17", "VIATICOS PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtViatpag.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                    sec += 1;
                    //comentar despues d euso de mayre
                    //cambiar monto pagado por numero cero
                    insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text)), txtFecha.Text, txtPartidaViaticos.Text, "17", DetalleComision.Archivo, DetalleComision.Comisionado);

                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) > 0)
                {
                    insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "6", "COMBUSTIBLE PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtCombust.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                    sec += 1;
                    //comentar despues d euso de mayre
                    //cambiar monto pagado por numero cero
                    insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text)), txtFecha.Text, txtPartidaCom.Text, "6", DetalleComision.Archivo, DetalleComision.Comisionado);

                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) > 0)
                {
                    insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "8", "PASAJES PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtPasaje.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                    sec += 1;
                    //comentar despues d euso de mayre
                    //cambiar monto pagado por numero cero
                    insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)), txtFecha.Text, txtPartPasaje.Text, "8", DetalleComision.Archivo, DetalleComision.Comisionado);
                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Peaje) > 0)
                {
                    insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "7", "PEAJES PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtPeajePag.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                    sec += 1;
                    //comentar despues d euso de mayre
                    //cambiar monto pagado por numero cero
                    insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text)), txtFecha.Text, txtPartidaPeaje.Text, "7", DetalleComision.Archivo, DetalleComision.Comisionado);
                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Singladuras) > 0)
                {
                    insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "7", "SINGLADURAS  PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Singladuras)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtSingladuras.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartSingladuras.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                    sec += 1;
                    //comentar despues d euso de mayre
                    //cambiar monto pagado por numero cero
                    insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Singladuras) + clsFuncionesGral.Convert_Double(txtSingladuras.Text)), txtFecha.Text, txtPartSingladuras.Text, "18", DetalleComision.Archivo, DetalleComision.Comisionado);
                }

                //temporal para mayre sacar para go live
                MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "17");
                MngNegocioComisionDetalle.UpdateEstatusFinancieros("2", DetalleComision.Archivo.Replace(".pdf", "") + ".pdf", DetalleComision.Comisionado, Year);

                clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                clsFuncionesGral.Activa_Paneles(pnlPagos, false);

            }
            else if (Session["Crip_IsExistinMinistracion"].ToString() == "1")
            {
                string lsEstatusFinancieros;
                double viat = 0, peaje = 0, pasaje = 0, combustible = 0, Singladuras = 0;

                if (pnlViaticos.Visible)
                {
                    oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo .Replace (".pdf",""), dplMinistracion.SelectedValue.ToString(), "17");

                    if ((clsFuncionesGral.Convert_Double(txtViatpag.Text)) > 0)
                    {
                        viat = clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text);

                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) == viat)
                        {
                            lsEstatusFinancieros = "2";
                        }
                        else
                        {
                            lsEstatusFinancieros = "1";
                        }

                        //estatus y pagado
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) == 0)
                        {
                            bool update = MngNegocioMinistracion.Update_MInistracion(txtPartidaViaticos.Text, lsEstatusFinancieros, txtViatpag.Text, lsFecha, Session["Crip_Usuario"].ToString(), lsbancOrigen, lsCuentaOrigen, lsclabeOrigen, lsBanco, lsCuenta, lsClabe, lsReferencia, DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "17", DetalleComision.Comisionado );
                            //update de fecha de pago y total pagado en detalle comision
                            update = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text)), txtFecha.Text, txtPartidaViaticos.Text, "17", DetalleComision.Archivo, DetalleComision.Comisionado );

                        }
                        else
                        {
                            //inserta nueva secuencia en ministracion;
                            bool insert = MngNegocioMinistracion.Inserta_Ministracion(oMinistracion.Tipo_Ministracion, oMinistracion.Clave_Periodo, oMinistracion.Folio, oMinistracion.Fecha, oMinistracion.Periodo, oMinistracion.Usuario_Genera, oMinistracion.Fecha_Autoriza, oMinistracion.Autoriza, txtFecha.Text, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oMinistracion.Proyecto, oMinistracion.Ubicacion_Proyecto, oMinistracion.Documento, DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, oMinistracion.Razon_Social, oMinistracion.RFC, oMinistracion.Folio_Factura, oMinistracion.Fecha_Emision, "17", oMinistracion.Concepto, oMinistracion.Subtotal, oMinistracion.IVA, oMinistracion.TUA, oMinistracion.SFP, oMinistracion.ISR, Dictionary.NUMERO_CERO, oMinistracion.Iva_Retenido, oMinistracion.ISR_Retenido, oMinistracion.Tua_Retenido, oMinistracion.SFP_Retenido, oMinistracion.Descuentos, txtViatpag.Text, oMinistracion.Fecha_Recepcion, oMinistracion.Proceso, oMinistracion.Partida, lsEstatusFinancieros, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oMinistracion.Secuencia) + 1), oMinistracion.Observaciones, lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                            //actualiza en detalle commision fecha partida monto pagado
                            insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text)), txtFecha.Text, txtPartidaViaticos.Text, "17", DetalleComision.Archivo,DetalleComision.Comisionado );

                        }
                        //matamos el objeto
                        oMinistracion = null;
                        //creamos nuevamente el objeto con datos totales
                        oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "17");
                        //validamos si lo pagado con lo comprobado es igual o mayor
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) >= clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado))
                        {
                            //asigamos valores de 
                            lsEstatusFinancieros = "2";
                            //update de estatus en ministracion
                            MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "17");

                        }

                        viat = 0;
                        viat = clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado);
                        oMinistracion = null;
                    }
                }

                //actualizar combustible
                if (pnlCombustible.Visible)
                {
                    oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo .Replace (".pdf",""), dplMinistracion.SelectedValue.ToString(), "6");
                    if ((clsFuncionesGral.Convert_Double(txtCombust.Text)) > 0)
                    {
                        combustible = clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text);

                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) == combustible)
                        {
                            lsEstatusFinancieros = "2";
                        }
                        else
                        {
                            lsEstatusFinancieros = "1";
                        }


                        //estatus y pagado
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) == 0)
                        {
                            bool update = MngNegocioMinistracion.Update_MInistracion(txtPartidaCom.Text, lsEstatusFinancieros, txtCombust.Text, lsFecha, Session["Crip_Usuario"].ToString(), lsbancOrigen, lsCuentaOrigen, lsclabeOrigen, lsBanco, lsCuenta, lsClabe, lsReferencia, DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "6", DetalleComision .Comisionado );
                            update = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text)), txtFecha.Text, txtPartidaCom.Text, "6", DetalleComision.Archivo, DetalleComision.Comisionado );

                        }
                        else
                        {
                            bool insert = MngNegocioMinistracion.Inserta_Ministracion(oMinistracion.Tipo_Ministracion, oMinistracion.Clave_Periodo, oMinistracion.Folio, oMinistracion.Fecha, oMinistracion.Periodo, oMinistracion.Usuario_Genera, oMinistracion.Fecha_Autoriza, oMinistracion.Autoriza, txtFecha.Text, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oMinistracion.Proyecto, oMinistracion.Ubicacion_Proyecto, oMinistracion.Documento, DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, oMinistracion.Razon_Social, oMinistracion.RFC, oMinistracion.Folio_Factura, oMinistracion.Fecha_Emision, "6", oMinistracion.Concepto, oMinistracion.Subtotal, oMinistracion.IVA, oMinistracion.TUA, oMinistracion.SFP, oMinistracion.ISR, Dictionary.NUMERO_CERO, oMinistracion.Iva_Retenido, oMinistracion.ISR_Retenido, oMinistracion.Tua_Retenido, oMinistracion.SFP_Retenido, oMinistracion.Descuentos, txtViatpag.Text, oMinistracion.Fecha_Recepcion, oMinistracion.Proceso, oMinistracion.Partida, lsEstatusFinancieros, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oMinistracion.Secuencia) + 1), oMinistracion.Observaciones, lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);

                            insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text)), txtFecha.Text, txtPartidaViaticos.Text, "6", DetalleComision.Archivo, DetalleComision.Comisionado );
                        }
                        //matamos el objeto
                        oMinistracion = null;
                        //creamos nuevamente el objeto con datos totales
                        oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "6");
                        //validamos si lo pagado con lo comprobado es igual o mayor
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) >= clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado))
                        {
                            //asigamos valores de 
                            lsEstatusFinancieros = "2";
                            //update de estatus en ministracion
                            MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "6");
                        }

                        combustible = 0;
                        combustible = clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado);
                        oMinistracion = null;
                    }
                }


                //actualizar peaje 
                if (pnlPeaje.Visible)
                {
                    oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf",""), dplMinistracion.SelectedValue.ToString(), "7");
                    if ((clsFuncionesGral.Convert_Double(txtPeajePag.Text)) > 0)
                    {
                        peaje = clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text);

                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) == peaje)
                        {
                            lsEstatusFinancieros = "2";
                        }
                        else
                        {
                            lsEstatusFinancieros = "1";
                        }


                        //estatus y pagado
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) == 0)
                        {
                            bool update = MngNegocioMinistracion.Update_MInistracion(txtPartidaCom.Text, lsEstatusFinancieros, txtPeajePag.Text, lsFecha, Session["Crip_Usuario"].ToString(), lsbancOrigen, lsCuentaOrigen, lsclabeOrigen, lsBanco, lsCuenta, lsClabe, lsReferencia, DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "7", DetalleComision.Comisionado);
                            update = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text)), txtFecha.Text, txtPartidaPeaje.Text, "7", DetalleComision.Archivo, DetalleComision.Comisionado);

                        }
                        else
                        {
                            bool insert = MngNegocioMinistracion.Inserta_Ministracion(oMinistracion.Tipo_Ministracion, oMinistracion.Clave_Periodo, oMinistracion.Folio, oMinistracion.Fecha, oMinistracion.Periodo, oMinistracion.Usuario_Genera, oMinistracion.Fecha_Autoriza, oMinistracion.Autoriza, txtFecha.Text, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oMinistracion.Proyecto, oMinistracion.Ubicacion_Proyecto, oMinistracion.Documento, DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, oMinistracion.Razon_Social, oMinistracion.RFC, oMinistracion.Folio_Factura, oMinistracion.Fecha_Emision, "7", oMinistracion.Concepto, oMinistracion.Subtotal, oMinistracion.IVA, oMinistracion.TUA, oMinistracion.SFP, oMinistracion.ISR, Dictionary.NUMERO_CERO, oMinistracion.Iva_Retenido, oMinistracion.ISR_Retenido, oMinistracion.Tua_Retenido, oMinistracion.SFP_Retenido, oMinistracion.Descuentos, txtViatpag.Text, oMinistracion.Fecha_Recepcion, oMinistracion.Proceso, oMinistracion.Partida, lsEstatusFinancieros, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oMinistracion.Secuencia) + 1), oMinistracion.Observaciones, lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);

                            insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text)), txtFecha.Text, txtPartidaViaticos.Text, "7", DetalleComision.Archivo,DetalleComision.Comisionado );
                        }
                        //matamos el objeto
                        oMinistracion = null;
                        //creamos nuevamente el objeto con datos totales
                        oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "7");
                        //validamos si lo pagado con lo comprobado es igual o mayor
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) >= clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado))
                        {
                            //asigamos valores de 
                            lsEstatusFinancieros = "2";
                            //update de estatus en ministracion
                            MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "7");
                        }

                        peaje = 0;
                        peaje = clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado);
                        oMinistracion = null;
                    }
                }


                //actualizacion de  pasajes
                if (pnlPasajes.Visible)
                {
                    oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "8");

                    if ((clsFuncionesGral.Convert_Double(txtPasaje.Text)) > 0)
                    {
                        pasaje = clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text);

                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) == pasaje )
                        {
                            lsEstatusFinancieros = "2";
                        }
                        else
                        {
                            lsEstatusFinancieros = "1";
                        }

                        //estatus y pagado
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) == 0)
                        {
                            bool update = MngNegocioMinistracion.Update_MInistracion(txtPartidaCom.Text, lsEstatusFinancieros, txtPasaje.Text, lsFecha, Session["Crip_Usuario"].ToString(), lsbancOrigen, lsCuentaOrigen, lsclabeOrigen, lsBanco, lsCuenta, lsClabe, lsReferencia, DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "8", DetalleComision.Comisionado);
                            update = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)), txtFecha.Text, txtPartidaPeaje.Text, "8", DetalleComision.Archivo, DetalleComision.Comisionado);
                        }
                        else
                        {
                            bool insert = MngNegocioMinistracion.Inserta_Ministracion(oMinistracion.Tipo_Ministracion, oMinistracion.Clave_Periodo, oMinistracion.Folio, oMinistracion.Fecha, oMinistracion.Periodo, oMinistracion.Usuario_Genera, oMinistracion.Fecha_Autoriza, oMinistracion.Autoriza, txtFecha.Text, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oMinistracion.Proyecto, oMinistracion.Ubicacion_Proyecto, oMinistracion.Documento, DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, oMinistracion.Razon_Social, oMinistracion.RFC, oMinistracion.Folio_Factura, oMinistracion.Fecha_Emision, "8", oMinistracion.Concepto, oMinistracion.Subtotal, oMinistracion.IVA, oMinistracion.TUA, oMinistracion.SFP, oMinistracion.ISR, Dictionary.NUMERO_CERO, oMinistracion.Iva_Retenido, oMinistracion.ISR_Retenido, oMinistracion.Tua_Retenido, oMinistracion.SFP_Retenido, oMinistracion.Descuentos, txtViatpag.Text, oMinistracion.Fecha_Recepcion, oMinistracion.Proceso, oMinistracion.Partida, lsEstatusFinancieros, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oMinistracion.Secuencia) + 1), oMinistracion.Observaciones, lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                            insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)), txtFecha.Text, txtPartidaViaticos.Text, "8", DetalleComision.Archivo, DetalleComision.Comisionado);
                        }
                        //matamos el objeto
                        oMinistracion = null;
                        //creamos nuevamente el objeto con datos totales
                        oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "8");
                        //validamos si lo pagado con lo comprobado es igual o mayor
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) >= clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado))
                        {
                            //asigamos valores de 
                            lsEstatusFinancieros = "2";
                            //update de estatus en ministracion
                            MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "8");
                        }
                        pasaje = 0;
                        pasaje = clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado);
                        oMinistracion = null;
                    }
                }


                 //falta singladuras
                if (pnlSingladuras.Visible)
                {
                    oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "18");

                    if ((clsFuncionesGral.Convert_Double(txtSingladuras .Text )) > 0)
                    {
                        Singladuras  = clsFuncionesGral.Convert_Double(oCdetalle.Singladuras) + clsFuncionesGral.Convert_Double(txtSingladuras.Text );

                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) == Singladuras )
                        {
                            lsEstatusFinancieros = "2";
                        }
                        else
                        {
                            lsEstatusFinancieros = "1";
                        }

                        //estatus y pagado
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado) == 0)
                        {
                            bool update = MngNegocioMinistracion.Update_MInistracion(txtPartidaCom.Text, lsEstatusFinancieros, txtPasaje.Text, lsFecha, Session["Crip_Usuario"].ToString(), lsbancOrigen, lsCuentaOrigen, lsclabeOrigen, lsBanco, lsCuenta, lsClabe, lsReferencia, DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "18", DetalleComision.Comisionado);
                            update = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)), txtFecha.Text, txtPartidaPeaje.Text, "18", DetalleComision.Archivo, DetalleComision.Comisionado);
                        }
                        else
                        {
                            bool insert = MngNegocioMinistracion.Inserta_Ministracion(oMinistracion.Tipo_Ministracion, oMinistracion.Clave_Periodo, oMinistracion.Folio, oMinistracion.Fecha, oMinistracion.Periodo, oMinistracion.Usuario_Genera, oMinistracion.Fecha_Autoriza, oMinistracion.Autoriza, txtFecha.Text, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), oMinistracion.Proyecto, oMinistracion.Ubicacion_Proyecto, oMinistracion.Documento, DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, oMinistracion.Razon_Social, oMinistracion.RFC, oMinistracion.Folio_Factura, oMinistracion.Fecha_Emision, "18", oMinistracion.Concepto, oMinistracion.Subtotal, oMinistracion.IVA, oMinistracion.TUA, oMinistracion.SFP, oMinistracion.ISR, Dictionary.NUMERO_CERO, oMinistracion.Iva_Retenido, oMinistracion.ISR_Retenido, oMinistracion.Tua_Retenido, oMinistracion.SFP_Retenido, oMinistracion.Descuentos, txtViatpag.Text, oMinistracion.Fecha_Recepcion, oMinistracion.Proceso, oMinistracion.Partida, lsEstatusFinancieros, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oMinistracion.Secuencia) + 1), oMinistracion.Observaciones, lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                            insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)), txtFecha.Text, txtPartidaViaticos.Text, "18", DetalleComision.Archivo, DetalleComision.Comisionado);
                        }
                        //matamos el objeto
                        oMinistracion = null;
                        //creamos nuevamente el objeto con datos totales
                        oMinistracion = MngNegocioMinistracion.Objeto_Ministracion(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "18");
                        //validamos si lo pagado con lo comprobado es igual o mayor
                        if (clsFuncionesGral.Convert_Double(oMinistracion.Total_Solicitado) >= clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado))
                        {
                            //asigamos valores de 
                            lsEstatusFinancieros = "2";
                            //update de estatus en ministracion
                            MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString(), "18");
                        }
                        pasaje = 0;
                        pasaje = clsFuncionesGral.Convert_Double(oMinistracion.Total_Pagado);
                        oMinistracion = null;

                    }

                }
                //actualizar detalle comision con  y fecha y si ya se cerro pago

                Entidad oEntidad = MngNegocioMinistracion.PagadoVsPagar(DetalleComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString());

                if (clsFuncionesGral.Convert_Double(oEntidad.Codigo) == clsFuncionesGral.Convert_Double(oEntidad.Descripcion))
                {
                    //update de detalle comision
                    MngNegocioComisionDetalle.UpdateEstatusFinancieros("2", DetalleComision.Archivo,DetalleComision.Comisionado , Year);

                    clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                    clsFuncionesGral.Activa_Paneles(pnlPagos, false);
                }
                else
                {

                }
                oEntidad = null;

            }
            oCdetalle = null;
            DetalleComision = null;

            //desactiva el paneles segun lo pagado

        }
    }
}