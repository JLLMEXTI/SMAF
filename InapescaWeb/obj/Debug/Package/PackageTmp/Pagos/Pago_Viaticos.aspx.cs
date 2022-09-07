using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;
using InapescaWeb.Entidades;

namespace InapescaWeb.Pagos
{
    public partial class Pago_Viaticos : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static readonly string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
            }
        }

        public void Carga_Valores()
        {
            clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
            clsFuncionesGral.Activa_Paneles(pnlComisionSinNumero, false);
            clsFuncionesGral.Llena_Lista(dplTipoPago, clsFuncionesGral.ConvertMayus("cheque|deposito a cuenta"));
            clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
            clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
            clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
            clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
            clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);

            Label6.Text = clsFuncionesGral.ConvertMayus("Busqueda de Oficios de comison a pagar");
            Label1.Text = clsFuncionesGral.ConvertMayus(" Oficio :   ");
            Label2.Text = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-   ");
            Label5.Text = clsFuncionesGral.ConvertMayus("Captura de Pagos de viaticos");
            Label7.Text = clsFuncionesGral.ConvertMayus("ministracion número");
            Label77.Text = clsFuncionesGral.ConvertMayus("Tipo Ministracion");
            Label8.Text = clsFuncionesGral.ConvertMayus("tipo de pago");
            Label10.Text = clsFuncionesGral.ConvertMayus("banco destino");
            Label11.Text = clsFuncionesGral.ConvertMayus("cuenta destino ");
            Label12.Text = clsFuncionesGral.ConvertMayus("clabe destino ");
            Label29.Text = clsFuncionesGral.ConvertMayus("Fecha Pago:");
            Label9.Text = clsFuncionesGral.ConvertMayus("referencia bancaria");
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

           /* dplSiglas.DataSource = MngNegocioDependencia.ListaSiglas(Year);
            dplSiglas.DataTextField = Dictionary.DESCRIPCION;
            dplSiglas.DataValueField = Dictionary.CODIGO;
            dplSiglas.DataBind();
            */
            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();

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

                //aki
                string[] lsarchivo = new string[5];
                lsarchivo = Session["Crip_Oficio_Pagar"].ToString().Split(new Char[] { '-' });


                //  Comision DetalleComision = MngNegocioComision.DetalleComision_Pagos(Session["Crip_Oficio_Pagar"].ToString(), lsarchivo [2].ToString ());
                Comision oComision = MngNegocioComision.PagoComision(Session["Crip_Oficio_Pagar"].ToString(), lsarchivo[4].Replace(".pdf", ""), false);
                ComisionDetalle oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(oComision.Archivo, oComision.Comisionado, oComision.Ubicacion_Comisionado, Year);

                if ((oCdetalle.Oficio == null) | (oCdetalle.Oficio == ""))
                {
                    bool insertDetalle = MngNegocioComision.Insert_Detalle_Comision(oComision);
                }


                List<Entidad> lista = MngNegocioMinistracion.ListaMinistracionesSolicitadas(oComision.Archivo.Replace(".pdf", ""), dplMinistracion.SelectedValue.ToString());

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

                    if (ent.Codigo == "19")
                    {
                        clsFuncionesGral.Activa_Paneles(pnlSingladuras, true);
                    }
                    else
                    {
                        //      clsFuncionesGral.Activa_Paneles(pnlPasajes, false );
                    }
                }

                oCdetalle = null;
                oComision = null;
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

        //boton de busqueda
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //validacion de campos
            string lsAdscripcion = dplSiglas.Text ;
            string lsnOfico = txtNumOficio.Text;
            string lsPeriodo = "";
            string lsArchivo = "RJL-INAPESCA-";
            string lsSeparador = "-";
            bool bandera = false;
            txtFecha.Text = lsHoy;

            if (dplAnio.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un periodo a buscar');", true);
                return;
            }
            else
            {
                lsPeriodo = dplAnio.SelectedValue.ToString();
            }

            if ((lsnOfico == Dictionary.CADENA_NULA) | (lsnOfico == null))
            {
                lsArchivo += lsAdscripcion + lsSeparador;

                bandera = true;
            }
            else
            {
                if (!clsFuncionesGral.IsNumeric(lsnOfico))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('numero de oficio debe ser numerico');", true);
                    return;
                }
                else
                {
                    bandera = false;
                    lsArchivo += lsAdscripcion + lsSeparador + lsnOfico + lsSeparador + lsPeriodo + ".pdf";
                }
            }

            if (bandera)
            {
                List<Comision> listaComisiones = MngNegocioComision.ListaComisionesPagar(lsArchivo, lsPeriodo);

                foreach (Comision o in listaComisiones)
                {
                    listComisiones.Items.Add(o.Archivo + "/ " + MngNegocioUsuarios.Obtiene_Nombre(o.Comisionado) + " - " + o.Lugar);
                }
                listComisiones = null;
                clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                clsFuncionesGral.Activa_Paneles(pnlComisionSinNumero, true);
            }
            else
            {
                Session["Crip_Oficio_Pagar"] = lsArchivo;
                Comision oComision = MngNegocioComision.PagoComision(lsArchivo, lsPeriodo, bandera);

                if (oComision.Oficio == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Datos no encontrados');", true);
                    return;
                }
                else
                {
                    ComisionDetalle oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(oComision.Archivo, oComision.Comisionado, oComision.Ubicacion_Comisionado, oComision.Periodo);

                    if ((oCdetalle.Oficio == null) | (oCdetalle.Oficio == ""))
                    {
                        bool insertDetalle = MngNegocioComision.Insert_Detalle_Comision(oComision);
                    }

                    if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                    {
                        if (oComision.Dep_Proy != Session["Crip_Ubicacion"].ToString())
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision utuliza recurso de otro programa adscrito a una unidad aministrativa por lo que no podra realizar pagos en ella');", true);
                            return;

                        }
                    }

                    lblNombre.Text = clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(oComision.Comisionado));
                    lblLugarComison.Text = clsFuncionesGral.ConvertMayus(oComision.Lugar);
                    if (clsFuncionesGral.FormatFecha(oComision.Fecha_Inicio) == clsFuncionesGral.FormatFecha(oComision.Fecha_Final))
                    {
                        lblPeriodo.Text = clsFuncionesGral.FormatFecha(oComision.Fecha_Inicio);
                    }
                    else
                    {
                        lblPeriodo.Text = "Del " + clsFuncionesGral.FormatFecha(oComision.Fecha_Inicio) + " al " + clsFuncionesGral.FormatFecha(oComision.Fecha_Final);
                    }

                    lblObjetivo.Text = clsFuncionesGral.ConvertMayus(oComision.Objetivo);

                    //activa opanel de datos de comision
                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, true);
                    clsFuncionesGral.Activa_Paneles(pnlComisionSinNumero, false);

                    Label16.Text = clsFuncionesGral.ConvertMayus("Viaticos SOLICITADOS: $ ") + clsFuncionesGral.Convert_Decimales(oComision.Total_Viaticos);
                    Label20.Text = clsFuncionesGral.ConvertMayus("coMBUSTIBLE SOLICITADOS : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Combustible_Efectivo);
                    Label24.Text = clsFuncionesGral.ConvertMayus("Peaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Peaje);
                    Label32.Text = clsFuncionesGral.ConvertMayus("Pasaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Pasaje);
                    Label33.Text = clsFuncionesGral.ConvertMayus("Singladuras solicitadas : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Singladuras);

                    //OTORGADOS
                    lblViaticos.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Viaticos);
                    lblCombust.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Combustible);
                    lblPeaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Peaje);
                    lblPasaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Pasaje);
                    lblSingladuras.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Singladuras);

                    if (((oComision.Zona_Comercial == "0") & ((oComision.Combustible_Efectivo == Dictionary.NUMERO_CERO) & (oComision.Peaje == Dictionary.NUMERO_CERO) & (oComision.Pasaje == Dictionary.NUMERO_CERO))))
                    {
                        oCdetalle = null;
                        oComision = null;
                        clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta comision fue sin viaticos, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                        return;

                    }
                    else if (oCdetalle.Financieros == "2")
                    {
                        oCdetalle = null;
                        oComision = null;
                        clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision ya fue pagada, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                        return;
                    }
                    else
                    {
                        List<Entidad> ListaMinistracion = MngNegocioMinistracion.Obtiene_Ministracion(Year, oComision.Archivo.Replace(".pdf", ""));

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
                            //no existe entonces checar paneles visibles por solicitado :D
                            if ((oComision.Total_Viaticos != "") | (oComision.Total_Viaticos != null))
                            {
                                if (clsFuncionesGral.Convert_Double(oComision.Total_Viaticos) > 0)
                                {
                                    clsFuncionesGral.Activa_Paneles(pnlViaticos, true);
                                }
                            }
                            else
                            {
                                clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                            }

                            if ((oComision.Combustible_Efectivo != "") | (oComision.Combustible_Efectivo != null))
                            {
                                if (clsFuncionesGral.Convert_Double(oComision.Combustible_Efectivo) > 0)
                                {
                                    clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                                }

                            }
                            else
                            {
                                clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                            }

                            if ((oComision.Peaje != "") | (oComision.Peaje != null))
                            {
                                if (clsFuncionesGral.Convert_Double(oComision.Peaje) > 0)
                                {
                                    clsFuncionesGral.Activa_Paneles(pnlPeaje, true);
                                }
                            }
                            else
                            {
                                clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                            }

                            if ((oComision.Pasaje != "") | (oComision.Pasaje != null))
                            {
                                if (clsFuncionesGral.Convert_Double(oComision.Pasaje) > 0)
                                {
                                    clsFuncionesGral.Activa_Paneles(pnlPasajes, true);
                                }
                            }
                            else
                            {
                                clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                            }

                            if ((oComision.Singladuras != "") | (oComision.Singladuras != null))
                            {
                                if (clsFuncionesGral.Convert_Double(oComision.Singladuras) > 0)
                                {
                                    clsFuncionesGral.Activa_Paneles(pnlSingladuras, true);
                                }
                            }
                            else
                            {
                                clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                            }

                        }

                        clsFuncionesGral.Activa_Paneles(pnlDatosComision, true);



                        oCdetalle = null;
                        oComision = null;
                    }
                }
            }


        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string[] lsCadena = new string[2];
            lsCadena = listComisiones.SelectedItem.ToString().Split(new Char[] { '/' });

            string[] lsarchivo = new string[5];
            lsarchivo = lsCadena[0].Split(new Char[] { '-' });
            Comision oComision = MngNegocioComision.PagoComision(lsCadena[0], lsarchivo[4].Replace(".pdf", ""), false);
            Session["Crip_Oficio_Pagar"] = lsCadena[0];
            txtFecha.Text = lsHoy;
            if (oComision.Oficio == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Datos no encontrados');", true);
                return;
            }
            else
            {
                ComisionDetalle oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(oComision.Archivo, oComision.Comisionado, oComision.Ubicacion_Comisionado, oComision.Periodo);

                if ((oCdetalle.Oficio == null) | (oCdetalle.Oficio == ""))
                {
                    bool insertDetalle = MngNegocioComision.Insert_Detalle_Comision(oComision);
                }

                if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
                {
                    if (oComision.Dep_Proy != Session["Crip_Ubicacion"].ToString())
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision utuliza recurso de otro programa adscrito a una unidad aministrativa por lo que no podra realizar pagos en ella');", true);
                        return;

                    }
                }

                lblNombre.Text = clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(oComision.Comisionado));
                lblLugarComison.Text = clsFuncionesGral.ConvertMayus(oComision.Lugar);
                if (clsFuncionesGral.FormatFecha(oComision.Fecha_Inicio) == clsFuncionesGral.FormatFecha(oComision.Fecha_Final))
                {
                    lblPeriodo.Text = clsFuncionesGral.FormatFecha(oComision.Fecha_Inicio);
                }
                else
                {
                    lblPeriodo.Text = "Del " + clsFuncionesGral.FormatFecha(oComision.Fecha_Inicio) + " al " + clsFuncionesGral.FormatFecha(oComision.Fecha_Final);
                }

                lblObjetivo.Text = clsFuncionesGral.ConvertMayus(oComision.Objetivo);

                //activa opanel de datos de comision
                clsFuncionesGral.Activa_Paneles(pnlDatosComision, true);
                clsFuncionesGral.Activa_Paneles(pnlComisionSinNumero, false);

                Label16.Text = clsFuncionesGral.ConvertMayus("Viaticos SOLICITADOS: $ ") + clsFuncionesGral.Convert_Decimales(oComision.Total_Viaticos);
                Label20.Text = clsFuncionesGral.ConvertMayus("coMBUSTIBLE SOLICITADOS : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Combustible_Efectivo);
                Label24.Text = clsFuncionesGral.ConvertMayus("Peaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Peaje);
                Label32.Text = clsFuncionesGral.ConvertMayus("Pasaje SOLICITADO : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Pasaje);
                Label33.Text = clsFuncionesGral.ConvertMayus("Singladuras solicitadas : $ ") + clsFuncionesGral.Convert_Decimales(oComision.Singladuras);

                //OTORGADOS
                lblViaticos.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Viaticos);
                lblCombust.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Combustible);
                lblPeaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Peaje);
                lblPasaje.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Pasaje);
                lblSingladuras.Text = clsFuncionesGral.Convert_Decimales(oCdetalle.Singladuras);


                if (((oComision.Zona_Comercial == "0") & ((oComision.Combustible_Efectivo == Dictionary.NUMERO_CERO) & (oComision.Peaje == Dictionary.NUMERO_CERO) & (oComision.Pasaje == Dictionary.NUMERO_CERO))))
                {
                    oCdetalle = null;
                    oComision = null;
                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta comision fue sin viaticos, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                    return;

                }
                else if (oCdetalle.Financieros == "2")
                {

                    oCdetalle = null;
                    oComision = null;
                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Esta Comision ya fue pagada, si desea agregar pagos de reembolsos acceder al modulo correspondiente');", true);
                    return;
                }
                else
                {
                    List<Entidad> ListaMinistracion = MngNegocioMinistracion.Obtiene_Ministracion(Year, oComision.Archivo.Replace(".pdf", ""));

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

                        //no existe entonces checar paneles visibles por solicitado :D
                        if ((oComision.Total_Viaticos != "") | (oComision.Total_Viaticos != null))
                        {
                            if (clsFuncionesGral.Convert_Double(oComision.Total_Viaticos) > 0)
                            {
                                clsFuncionesGral.Activa_Paneles(pnlViaticos, true);
                            }
                        }
                        else
                        {
                            clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                        }

                        if ((oComision.Combustible_Efectivo != "") | (oComision.Combustible_Efectivo != null))
                        {
                            if (clsFuncionesGral.Convert_Double(oComision.Combustible_Efectivo) > 0)
                            {
                                clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                            }

                        }
                        else
                        {
                            clsFuncionesGral.Activa_Paneles(pnlCombustible, true);
                        }

                        if ((oComision.Peaje != "") | (oComision.Peaje != null))
                        {
                            if (clsFuncionesGral.Convert_Double(oComision.Peaje) > 0)
                            {
                                clsFuncionesGral.Activa_Paneles(pnlPeaje, true);
                            }
                        }
                        else
                        {
                            clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                        }

                        if ((oComision.Pasaje != "") | (oComision.Pasaje != null))
                        {
                            if (clsFuncionesGral.Convert_Double(oComision.Pasaje) > 0)
                            {
                                clsFuncionesGral.Activa_Paneles(pnlPasajes, true);
                            }
                        }
                        else
                        {
                            clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                        }

                        if ((oComision.Singladuras != "") | (oComision.Singladuras != null))
                        {
                            if (clsFuncionesGral.Convert_Double(oComision.Singladuras) > 0)
                            {
                                clsFuncionesGral.Activa_Paneles(pnlSingladuras, true);
                            }
                        }
                        else
                        {
                            clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                        }

                    }

                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, true);

                    oCdetalle = null;
                    oComision = null;
                }
            }

        }

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

                if (!clsFuncionesGral.IsNumeric(txtSingladuras.Text))
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

            if ((txtFecha.Text == null) | (txtFecha.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Fecha de pago obligatoria');", true);
                return;
            }
            else
            {
                lsFecha = clsFuncionesGral.FormatFecha(txtFecha.Text);
            }

            if ((txtCuentaOrigen.Text == null) | (txtCuentaOrigen.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Banco Origen Inapesca Obligatorio');", true);
                return;
            }
            else
            {
                lsbancOrigen = txtBancoOrigen.Text;
            }

            if ((txtCuentaOrigen.Text == null) | (txtCuentaOrigen.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Cuenta Origen Inapesca Obligatoria');", true);
                return;
            }
            else
            {
                lsCuentaOrigen = txtCuentaOrigen.Text;
            }

            if ((txtClabeOrigen.Text == null) | (txtClabeOrigen.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Clabe Origen Inapesca Obligatoria');", true);
                return;
            }
            else
            {
                lsclabeOrigen = txtClabeOrigen.Text;
            }

            if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
            {
                DetalleComision = MngNegocioComision.DetalleComision_Pagos(archivo + ".pdf", Session["Crip_Ubicacion"].ToString(), Usuario);
            }
            else
            {
                string[] lsarchivo = new string[5];
                lsarchivo = Session["Crip_Oficio_Pagar"].ToString().Split(new Char[] { '-' });
                DetalleComision = MngNegocioComision.PagoComision(Session["Crip_Oficio_Pagar"].ToString(), lsarchivo[4].Replace(".pdf", ""), false);

            }

            oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);

            //inicia desmadrito de pagos 
            if (Session["Crip_IsExistinMinistracion"].ToString() == "0")
            {
                string lsEstatusFinancieros;
                double viat = 0, peaje = 0, pasaje = 0, combustible = 0, totalPagado = 0;

                if (pnlViaticos.Visible)
                {
                    totalPagado += clsFuncionesGral.Convert_Double(txtViatpag.Text);
                }
                if (pnlCombustible.Visible)
                {
                    totalPagado += clsFuncionesGral.Convert_Double(txtCombust.Text);
                }
                if (pnlPeaje.Visible)
                {
                    totalPagado += clsFuncionesGral.Convert_Double(txtPeajePag.Text);
                }
                if (pnlPasajes.Visible)
                {
                    totalPagado += clsFuncionesGral.Convert_Double(txtPasaje.Text);
                }

                if (pnlSingladuras.Visible)
                {
                    totalPagado += clsFuncionesGral.Convert_Double(txtSingladuras.Text);
                }

                //insertar nueva ministracion detalle  y ministracion
                bool insert = MngNegocioMinistracion.Inserta_MInistracionDetalle(dplTipoMinistracion.SelectedValue.ToString(), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), "VIATICOS NACIONALES PARA LABORES EN CAMPO Y DE SUPERVISION", clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Decimales(totalPagado.ToString())), "1", "1", lsHoy, MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), "", "", "", "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);

                insert = false;
                Usuario objUsuario = MngNegocioUsuarios.Datos_Comisionado(DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado);
                Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(DetalleComision.Dep_Proy, DetalleComision.Proyecto,Year);

                double sec = 1;

                if (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) > 0)
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text)) <= clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos))
                    {
                        insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "17", "VIATICOS PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtViatpag.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                        sec += 1;
                        insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text)), txtFecha.Text, txtPartidaViaticos.Text, "17", DetalleComision.Archivo, DetalleComision.Comisionado);

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede realizar un pago de la partida de viaticos  mayor a lo solicitado');", true);
                        return;
                    }
                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) > 0)
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text)) <= clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo))
                    {
                        insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "6", "COMBUSTIBLE PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtCombust.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                        sec += 1;
                        //comentar despues d euso de mayre
                        //cambiar monto pagado por numero cero
                        insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text)), txtFecha.Text, txtPartidaCom.Text, "6", DetalleComision.Archivo, DetalleComision.Comisionado);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede realizar un pago de la partida de combustibles  mayor a lo solicitado');", true);
                        return;
                    }
                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) > 0)
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)) <= clsFuncionesGral.Convert_Double(DetalleComision.Pasaje))
                    {
                        insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "8", "PASAJES PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtPasaje.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                        sec += 1;
                        //comentar despues d euso de mayre
                        //cambiar monto pagado por numero cero
                        insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)), txtFecha.Text, txtPartPasaje.Text, "8", DetalleComision.Archivo, DetalleComision.Comisionado);

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede realizar un pago de la partida de pasajes  mayor a lo solicitado');", true);
                        return;
                    }
                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Peaje) > 0)
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text)) <= clsFuncionesGral.Convert_Double(DetalleComision.Peaje))
                    {
                        insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "7", "PEAJES PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtPeajePag.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartidaViaticos.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                        sec += 1;
                        //comentar despues d euso de mayre
                        //cambiar monto pagado por numero cero
                        insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text)), txtFecha.Text, txtPartidaPeaje.Text, "7", DetalleComision.Archivo, DetalleComision.Comisionado);

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede realizar un pago de la partida de pasajes  mayor a lo solicitado');", true);
                        return;

                    }
                }

                if (clsFuncionesGral.Convert_Double(DetalleComision.Singladuras) > 0)
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Singladuras) + clsFuncionesGral.Convert_Double(txtSingladuras.Text)) <= clsFuncionesGral.Convert_Double(DetalleComision.Singladuras))
                    {
                        insert = MngNegocioMinistracion.Inserta_Ministracion(dplTipoMinistracion.SelectedValue.ToString(), MngNegocioMinistracion.Obtiene_ClvPeriodo(lsHoy, Year), lsMinistracion, lsHoy, Year, Session["Crip_Usuario"].ToString(), Dictionary.FECHA_NULA, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), Dictionary.FECHA_NULA, Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), DetalleComision.Proyecto, DetalleComision.Dep_Proy, "CRIPSC08", DetalleComision.Archivo.Replace(".pdf", ""), lsFormaPago, lsReferencia, lsBanco, lsCuenta, lsClabe, DetalleComision.Comisionado, objUsuario.RFC, DetalleComision.Archivo.Replace(".pdf", ""), clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), "7", "SINGLADURAS  PARA LA COMISION A " + DetalleComision.Lugar + " DEL " + DetalleComision.Fecha_Inicio + " AL " + DetalleComision.Fecha_Final, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Singladuras)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(DetalleComision.Pasaje)), Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, Dictionary.NUMERO_CERO, txtSingladuras.Text, clsFuncionesGral.FormatFecha(DetalleComision.Fecha_Vobo), objProyecto.Componente, txtPartSingladuras.Text, "1", sec.ToString(), "", lsbancOrigen, lsCuentaOrigen, lsclabeOrigen);
                        sec += 1;
                        //comentar despues d euso de mayre
                        //cambiar monto pagado por numero cero
                        insert = MngNegocioComisionDetalle.UPdate_Monto_Pagado(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(oCdetalle.Singladuras) + clsFuncionesGral.Convert_Double(txtSingladuras.Text)), txtFecha.Text, txtPartSingladuras.Text, "18", DetalleComision.Archivo, DetalleComision.Comisionado);

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No puede realizar un pago de la partida de singladuras  mayor a lo solicitado');", true);
                        return;
                    }
                }

                if ((pnlViaticos.Visible))
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(txtViatpag.Text)) == clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos))
                    {
                        MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "17");
                    }
                }

                if ((pnlCombustible.Visible))
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(txtCombust.Text)) == clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo))
                    {
                        MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "6");
                    }
                }

                if ((pnlPeaje.Visible))
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(txtPeajePag.Text)) == clsFuncionesGral.Convert_Double(DetalleComision.Peaje))
                    {
                        MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "7");
                    }
                }

                if ((pnlPasajes.Visible))
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(txtPasaje.Text)) == clsFuncionesGral.Convert_Double(DetalleComision.Pasaje))
                    {
                        MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "8");
                    }
                }

                if ((pnlSingladuras.Visible))
                {
                    if ((clsFuncionesGral.Convert_Double(oCdetalle.Singladuras) + clsFuncionesGral.Convert_Double(txtSingladuras.Text)) == clsFuncionesGral.Convert_Double(DetalleComision.Singladuras))
                    {
                        MngNegocioMinistracion.UpdateEstatusMinistracion("2", DetalleComision.Archivo.Replace(".pdf", ""), lsMinistracion, "19");
                    }
                }


                oCdetalle = null;
                oCdetalle = MngNegocioComisionDetalle.Obtiene_detalle(DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, Year);
                
                if ((clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(oCdetalle.Singladuras)) == (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Singladuras)))
                {
                 
                    MngNegocioComisionDetalle.UpdateEstatusFinancieros("2", DetalleComision.Archivo.Replace(".pdf", "") + ".pdf", DetalleComision.Comisionado, Year);
                    clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                    clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se realizoi el pago de los viaticos');", true);
                    return;

                }//checar sumatoria y update de detalle ministracion
                else if ((clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(oCdetalle.Singladuras)) < (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Singladuras)))
                {
                    clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                    clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se realizo el pago parcial de la comision');", true);
                    return;

                }
                else if ((clsFuncionesGral.Convert_Double(oCdetalle.Viaticos) + clsFuncionesGral.Convert_Double(oCdetalle.Combustible) + clsFuncionesGral.Convert_Double(oCdetalle.Pasaje) + clsFuncionesGral.Convert_Double(oCdetalle.Peaje) + clsFuncionesGral.Convert_Double(oCdetalle.Singladuras)) > (clsFuncionesGral.Convert_Double(DetalleComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(DetalleComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(DetalleComision.Pasaje) + clsFuncionesGral.Convert_Double(DetalleComision.Peaje) + clsFuncionesGral.Convert_Double(DetalleComision.Singladuras)))
                {
                    clsFuncionesGral.Activa_Paneles(pnlViaticos, false);
                    clsFuncionesGral.Activa_Paneles(pnlPasajes, false);
                    clsFuncionesGral.Activa_Paneles(pnlPeaje, false);
                    clsFuncionesGral.Activa_Paneles(pnlCombustible, false);
                    clsFuncionesGral.Activa_Paneles(pnlSingladuras, false);
                    clsFuncionesGral.Activa_Paneles(pnlDatosComision, false);
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Los  pagos exceden lo solicitado, no se puede cargar pagos');", true);
                    return;
                }

               

            }

        }
    }

}
