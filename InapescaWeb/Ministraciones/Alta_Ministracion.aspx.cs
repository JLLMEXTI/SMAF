
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace InapescaWeb.Ministraciones
{
    public partial class Alta_Ministracion : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static bool lbExisteProovedor = false;
        static string lsExisteFactura = "";
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();

                /*
                RadTreeList1.ExpandedIndexes.Add(new TreeListHierarchyIndex { NestedLevel = 0, LevelIndex = 0 });

                DataSet datasetArbol = MngNegocioMenu.MngDatosMenu(Session["Crip_Rol"].ToString(), "0");
               DataTable  tblTabla = new DataTable();
                tblTabla = datasetArbol.Tables["DataSetArbol"];

                RadTreeList1.DataSource = tblTabla;
                RadTreeList1.DataBind();*/
            }
        }


        /// <summary>
        /// Metodo de caraga de valores iniciales de pagina
        /// </summary>
        public void Carga_Valores()
        {
            string lsSession = Session["Crip_Rol"].ToString();
            if ((lsSession == Dictionary.ADMINISTRADOR ) | (lsSession == Dictionary.JEFE_CENTRO ))
            {
                Label36.Visible = false;
                dplAdscripcion.Visible = false;

                dplPrograma.DataSource = MngNegocioProyecto.ObtieneProyectos ("","",Session["Crip_Ubicacion"] .ToString ());
                dplPrograma.DataTextField = Dictionary.DESCRIPCION;
                dplPrograma.DataValueField = Dictionary.CODIGO;
                dplPrograma.DataBind();
            }
            else
            {
                Label36.Visible = true ;
                dplAdscripcion.Visible = true ;

                dplAdscripcion.DataSource = MngNegocioDirecciones.ObtienenDirecciones(Year);
                dplAdscripcion.DataTextField = Dictionary.DESCRIPCION;
                dplAdscripcion.DataValueField = Dictionary.CODIGO;
                dplAdscripcion.DataBind();
            }
            
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lnkHome.Text = "INICIO";
            Label1.Text = clsFuncionesGral.ConvertMayus("Alta de Ministraciones :");
            Label2.Text = clsFuncionesGral.ConvertMayus("RFC : ");
            Label4.Text = clsFuncionesGral.ConvertMayus("RFC : ");
            Label7.Text = clsFuncionesGral.ConvertMayus("Razon social :");
            Label9.Text = clsFuncionesGral.ConvertMayus("Calle : ");
            Label11.Text = clsFuncionesGral.ConvertMayus("Num. Ext. ");
            Label13.Text = clsFuncionesGral.ConvertMayus("Num. Int.");
            Label15.Text = clsFuncionesGral.ConvertMayus("Colonia : ");
            Label17.Text = clsFuncionesGral.ConvertMayus("Municipio / Delegacion");
            Label19.Text = clsFuncionesGral.ConvertMayus("Ciudad");
            Label21.Text = clsFuncionesGral.ConvertMayus("Estado : ");
            Label23.Text = clsFuncionesGral.ConvertMayus("Pais");
            Label5.Text = clsFuncionesGral.ConvertMayus("C.P.");
            Label8.Text = clsFuncionesGral.ConvertMayus("email :");
            Label10.Text = clsFuncionesGral.ConvertMayus("Contacto :");
            Label12.Text = clsFuncionesGral.ConvertMayus("Telefono Contacto :");
            Label14.Text = clsFuncionesGral.ConvertMayus("Telefono de Empresa 1 :");
            Label16.Text = clsFuncionesGral.ConvertMayus("Telefono de Empresa 2 :");
            Label20.Text = clsFuncionesGral.ConvertMayus("Regimen fiscal : ");
            Label22.Text = clsFuncionesGral.ConvertMayus("Servcios :");

            Label6.Text = clsFuncionesGral.ConvertMayus("Tipo MInistracion  : ");

            dplTipoMinistracion.DataSource = MngNegocioMinistracion.ListaTipoMinistracion("00");
            dplTipoMinistracion.DataValueField = Dictionary.CODIGO;
            dplTipoMinistracion.DataTextField = Dictionary.DESCRIPCION;
            dplTipoMinistracion.DataBind();

            Label18.Text = clsFuncionesGral.ConvertMayus("TIPO DE PAGO : ");
            CheckBox1.Text = clsFuncionesGral.ConvertMayus(" buscar proovedor leyendo CFDI ");
            Label27.Text = clsFuncionesGral.ConvertMayus("buscar proovedor por rfc");
            Label28.Text = clsFuncionesGral.ConvertMayus("buscar proovedor leyendo CFDI ");

            clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, false);
            clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
            clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, false);
            clsFuncionesGral.Activa_Paneles(pnlBusquedaProv, false);
            clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);

            Label24.Text = clsFuncionesGral.ConvertMayus("archivo pdf");
            Label25.Text = clsFuncionesGral.ConvertMayus("archivo xml");
            Label26.Text = clsFuncionesGral.ConvertMayus("Documentacion comprobatoria adicional en formato PDF que avalan factura ") + " (Opcional)";
            lnkBuscarProovedor.Text = "Leer Factura ";

            Label3.Text = clsFuncionesGral.ConvertMayus(" Agregar factura ");
            Label29.Text = clsFuncionesGral.ConvertMayus("archivo pdf");
            Label30.Text = clsFuncionesGral.ConvertMayus("archivo xml");
            Label31.Text = clsFuncionesGral.ConvertMayus("Documentacion comprobatoria adicional en formato PDF que avalan factura ") + " (Opcional)";
            btnCargarPago.Text = clsFuncionesGral.ConvertMayus("Cargar Pago a ministracion");
            btnCargarPago.Enabled = false;
            Label32.Text = clsFuncionesGral.ConvertMayus("Datos Bancarios de Proveedor");
            Label33.Text = clsFuncionesGral.ConvertMayus("cuenta bancaria ");
            Label34.Text = clsFuncionesGral.ConvertMayus("banco ");
            Label35.Text = clsFuncionesGral.ConvertMayus("clabe interbancaria ");
            lnkUpdateProovedor.Text = clsFuncionesGral.ConvertMayus("ACtualizar Datos de Proveedor");
            Label36.Text = clsFuncionesGral.ConvertMayus("Adscripcion");
            Label37.Text = clsFuncionesGral.ConvertMayus(" programa ");
        }

        /// <summary>
        /// Evemto click de linhomme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        /// <summary>
        /// Evento click de link buttton de datos personales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        /// <summary>
        /// Evento click de menu tree view
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
        /// BUSCA PROOVEDOR DESDE RFC 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if ((txtRfcFind.Text == "") | (txtRfcFind.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote RFC valido');", true);
                return;
            }
            else
            {
                string lsRfcFind = txtRfcFind.Text;
                Proovedor loProovedor = mngNegocioProovedor.Proveedor(txtRfcFind.Text);

                clsFuncionesGral.Activa_Paneles(pnlProovedor, true);
                clsFuncionesGral.Activa_Paneles(pnlAgregaFac, true);
                btnCargarPago.Enabled = true;

                if ((loProovedor.RFC == null) | (loProovedor.RFC == ""))
                {
                    loProovedor = null;
                    txtRfc.Enabled = true;
                    txtRfc.Text = clsFuncionesGral.ConvertMayus(txtRfcFind.Text);
                    txtCp.Text = Dictionary.CADENA_NULA;
                    txtRazonSocial.Text = Dictionary.CADENA_NULA;
                    txtEmail.Text = Dictionary.CADENA_NULA;
                    txtCalle.Text = Dictionary.CADENA_NULA;
                    txtContacto.Text = Dictionary.CADENA_NULA;
                    txtNumExt.Text = Dictionary.CADENA_NULA;
                    txtTelefonoCon.Text = Dictionary.CADENA_NULA;
                    txtNumInt.Text = Dictionary.CADENA_NULA;
                    txtTelefonoEmpresa.Text = Dictionary.CADENA_NULA;
                    txtColonia.Text = Dictionary.CADENA_NULA;
                    txtTelefonoEmpresa2.Text = Dictionary.CADENA_NULA;
                    txtMunicipio.Text = Dictionary.CADENA_NULA;
                    txtRegimenFiscal.Text = Dictionary.CADENA_NULA;
                    txtCuidad.Text = Dictionary.CADENA_NULA;
                    txtServicio.Text = Dictionary.CADENA_NULA;
                    txtEstado.Text = Dictionary.CADENA_NULA;
                    txtPais.Text = Dictionary.CADENA_NULA;

                }
                else
                {

                    txtRfc.Text = loProovedor.RFC;
                    txtRfc.Enabled = false;
                    txtCp.Text = loProovedor.CP;
                    txtRazonSocial.Text = loProovedor.Razon_Social;
                    txtEmail.Text = loProovedor.Email;
                    txtCalle.Text = loProovedor.Calle;
                    txtContacto.Text = loProovedor.Contacto;
                    txtNumExt.Text = loProovedor.Num_Ext;
                    txtTelefonoCon.Text = loProovedor.Telefono;
                    txtNumInt.Text = loProovedor.Num_int;
                    txtTelefonoEmpresa.Text = loProovedor.Telefono1;
                    txtColonia.Text = loProovedor.Colonia;
                    txtTelefonoEmpresa2.Text = loProovedor.Telefono2;
                    txtMunicipio.Text = loProovedor.Municipio;
                    txtRegimenFiscal.Text = loProovedor.Regimen_Fiscal;
                    txtBanco.Text = loProovedor.Banco;
                    txtCuentaBancaria.Text = loProovedor.Cuenta;
                    txtClabe.Text = loProovedor.Clabe;

                    if ((MngNegocioDependencia.Obtiene_ciudad(loProovedor.Ciudad) == "") | (MngNegocioDependencia.Obtiene_ciudad(loProovedor.Ciudad) == null))
                    {
                        txtCuidad.Text = loProovedor.Ciudad;
                    }
                    else
                    {
                        txtCuidad.Text = MngNegocioDependencia.Obtiene_ciudad(loProovedor.Ciudad);
                    }

                    txtServicio.Text = loProovedor.Servicio;

                    if ((MngNegocioEstado.Estado(loProovedor.Estado) == null) | (MngNegocioEstado.Estado(loProovedor.Estado) == ""))
                    {
                        txtEstado.Text = loProovedor.Estado;
                    }
                    else
                    {
                        txtEstado.Text = MngNegocioEstado.Estado(loProovedor.Estado);
                    }

                    if ((MngNegocioDependencia.Obtiene_Pais(loProovedor.Pais) == "") | (MngNegocioDependencia.Obtiene_Pais(loProovedor.Pais) == null))
                    {
                        txtPais.Text = loProovedor.Pais;
                    }
                    else
                    {
                        txtPais.Text = MngNegocioDependencia.Obtiene_Pais(loProovedor.Pais);
                    }
                }
                loProovedor = null;
            }
        }

        protected void dplTipoMinistracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoministracion = dplTipoMinistracion.SelectedValue.ToString();
            if (tipoministracion == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione tipo de ministracion para avanzar');", true);
                return;
            }
            else
            {

                clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, false);
                clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, false);
                clsFuncionesGral.Activa_Paneles(pnlBusquedaProv, false);

                dplTipoPago.DataSource = MngNegocioMinistracion.Lista_tipo_Pagos();
                dplTipoPago.DataTextField = Dictionary.DESCRIPCION;
                dplTipoPago.DataValueField = Dictionary.CODIGO;
                dplTipoPago.DataBind();

            }
        }

        protected void dplTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TipoPago = dplTipoPago.SelectedValue.ToString();

            if ((TipoPago == "17") | (TipoPago == "18"))
            {
                //agregar metodo de activacion de panel de of de comision que no esten en ministracion

                clsFuncionesGral.Activa_Paneles(pnlBusquedaProv, false);
                clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, false);
                clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, false);
                clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);
                Limpia_Campos();
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlBusquedaProv, true);
                clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, true);
                clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, false);
                clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);
                Limpia_Campos();
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnCargarPago.Enabled = false;

            if (CheckBox1.Checked)
            {
                clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, false);
                clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, true);
                Limpia_Campos();

                clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, true);
                clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, false);
                Limpia_Campos();

                clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);
            }
        }

        public void Limpia_Campos()
        {
            txtRfcFind.Text = Dictionary.CADENA_NULA;
            txtRfc.Text = Dictionary.CADENA_NULA;
            txtRazonSocial.Text = Dictionary.CADENA_NULA;
            txtCalle.Text = Dictionary.CADENA_NULA;
            txtNumExt.Text = Dictionary.CADENA_NULA;
            txtNumInt.Text = Dictionary.CADENA_NULA;
            txtColonia.Text = Dictionary.CADENA_NULA;
            txtMunicipio.Text = Dictionary.CADENA_NULA;
            txtCuidad.Text = Dictionary.CADENA_NULA;
            txtEstado.Text = Dictionary.CADENA_NULA;
            txtPais.Text = Dictionary.CADENA_NULA;
            txtCp.Text = Dictionary.CADENA_NULA;
            txtEmail.Text = Dictionary.CADENA_NULA;
            txtContacto.Text = Dictionary.CADENA_NULA;
            txtTelefonoCon.Text = Dictionary.CADENA_NULA;
            txtTelefonoEmpresa.Text = Dictionary.CADENA_NULA;
            txtTelefonoEmpresa2.Text = Dictionary.CADENA_NULA;
            txtRegimenFiscal.Text = Dictionary.CADENA_NULA;
            txtServicio.Text = Dictionary.CADENA_NULA;
            txtCuentaBancaria.Text = Dictionary.CADENA_NULA;
            txtClabe.Text = Dictionary.CADENA_NULA;
            txtBanco.Text = Dictionary.CADENA_NULA;

            FileUpload1.Dispose();
            FileUpload2.Dispose();
            FileUpload3.Dispose();

            fuplPDF.Dispose();
            fuplXML.Dispose();
            fupdTickets.Dispose();
           // dplAdscripcion.SelectedIndex = 0;
            //dplPrograma.Items.Clear();
        }

        public void Valida_CarpetaXML()
        {
            string raiz = HttpContext.Current.Server.MapPath("..");
            if (!Directory.Exists(raiz + "\\" + " XML")) Directory.CreateDirectory(raiz + "\\" + " XML"); ;
            Session["Crip_Ruta"] = raiz + "\\" + " XML";
        }

        //leer xml para datos de proovedor
        protected void lnkBuscarProovedor_Click(object sender, EventArgs e)
        {
            bool xmlOK = false;
            bool fileOk = false;
            string ticket;

            if (!fuplPDF.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo pdf es nesesario.');", true);
                return;
            }
            else
            {
                String fileExtension = System.IO.Path.GetExtension(fuplPDF.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (fupdTickets.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fupdTickets.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };
                bool fileticket = false;

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileticket = true;
                    }
                }

                if (fileticket)
                {
                    ticket = clsFuncionesGral.ConvertMinus(fupdTickets.FileName).Replace("pdf", "");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                    return;
                }
            }
            else
            {
                ticket = Dictionary.CADENA_NULA;
            }

            if (!fuplXML.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo xml es nesesario');", true);
                return;
            }
            else
            {
                String fileExtension = System.IO.Path.GetExtension(fuplXML.FileName).ToLower();
                String[] allowedExtensions = { ".xml", ".XML" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        xmlOK = true;
                    }
                }
            }

            string nombrepdf = clsFuncionesGral.ConvertMinus(fuplPDF.FileName);
            nombrepdf = nombrepdf.Replace(".pdf", "");

            string nombreXml = clsFuncionesGral.ConvertMinus(fuplXML.FileName);
            nombreXml = nombreXml.Replace(".xml", "");

            if (nombrepdf != nombreXml)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Nombres de archivos pdf y xml deben ser iguales.');", true);
                return;
            }
            else if ((fileOk) & (xmlOK))
            {
                try
                {
                    bool sube;

                    Valida_CarpetaXML();

                    if (!File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName))
                    {
                        fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    }
                    else
                    {
                        if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                        fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    }

                    List<Entidad> llEntidad = new List<Entidad>();
                    Entidades.Xml oXml = new Entidades.Xml();

                    clsFuncionesGral.Lee_XMl(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName, llEntidad, oXml);

                    //antes de subir checar que no este activo el uuid de factura si no insertar
                    string existe = MngNegocioComprobacion.Exist_UUUID(oXml.TIMBRE_FISCAL);
                    string existe1 = MngNegocioComprobacion.Exist_UUUID_Ministracion(oXml.TIMBRE_FISCAL);


                    if (((existe == "") | (existe == null)) && ((existe1 == "") | (existe1 == null)))
                    {
                        Proovedor loProovedor = mngNegocioProovedor.Proveedor(oXml.RFC_EMISOR);
                        clsFuncionesGral.Activa_Paneles(pnlProovedor, true);
                        btnCargarPago.Enabled = true;

                        if ((loProovedor.RFC == null) | (loProovedor.RFC == ""))
                        {
                            lbExisteProovedor = false;
                            txtEmail.Text = Dictionary.CADENA_NULA ;
                            txtContacto.Text = Dictionary.CADENA_NULA ;
                            txtTelefonoCon.Text = Dictionary.CADENA_NULA;
                            txtTelefonoEmpresa.Text = Dictionary.CADENA_NULA;
                            txtTelefonoEmpresa2.Text = Dictionary.CADENA_NULA;
                            txtRegimenFiscal.Text = Dictionary.CADENA_NULA;
                            txtServicio.Text = Dictionary.CADENA_NULA;
                            txtCuentaBancaria.Text = Dictionary.CADENA_NULA;
                            txtBanco.Text = Dictionary.CADENA_NULA;
                            txtClabe.Text = Dictionary.CADENA_NULA;
                        }
                        else
                        {
                            lbExisteProovedor = true;
                            txtEmail.Text = loProovedor.Email;
                            txtContacto.Text = loProovedor.Contacto;
                            txtTelefonoCon.Text = loProovedor.Telefono;
                            txtTelefonoEmpresa.Text = loProovedor.Telefono1;
                            txtTelefonoEmpresa2.Text = loProovedor.Telefono2;
                            txtRegimenFiscal.Text = loProovedor.Regimen_Fiscal;
                            txtServicio.Text = loProovedor.Servicio;
                            txtCuentaBancaria.Text = loProovedor.Cuenta ;
                            txtBanco.Text = loProovedor.Banco; 
                            txtClabe.Text = loProovedor .Clabe ;
                        }

                        txtRfc.Enabled = false ;
                        txtRfc.Text = clsFuncionesGral.ConvertMayus(oXml.RFC_EMISOR);
                        txtRazonSocial.Text = clsFuncionesGral.ConvertMayus(oXml.NOMBRE_EMISOR);
                        txtCalle.Text = oXml.CALLE_EMISOR ;
                        txtNumExt.Text = oXml.NO_EXTERIOR_EMMISOR ;
                        txtNumInt.Text = oXml.NO_INTERIOR_EMISOR ;
                        txtColonia.Text = oXml.COLONIA_EMISOR ;
                        txtMunicipio.Text  = oXml.MUNICIPIO_EMISOR ;
                        txtCuidad.Text = oXml.LOCALIDAD_EMISOR;
                        txtEstado.Text = oXml.ESTADO_EMISOR ;
                        txtPais.Text = oXml.PAIS_EMISOR ;
                        txtCp.Text = oXml.CP_EMISOR;
                        txtRegimenFiscal.Text = oXml.REGIMENFISCAL_EMISOR;

                        if (oXml.RFC_RECEPTOR  != Dictionary.RFC_INP)
                        {
                            Limpia_Campos();
                            clsFuncionesGral.Activa_Paneles(pnlBusquedaProv, true );
                            clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, false);
                            clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, true );
                            clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                            clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);
                            CheckBox1.Checked = true ;
                            Limpia_Campos();
                           
                            loProovedor = null;
                            oXml = null;
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El CFDI que intenta cargar no fue expedidad para el Instituto Nacional de Pesca , intente con otra.');", true);
                            return;
                        }
                        else
                        {
                            //insert ministracion

                         oXml = null;
                        } loProovedor = null;
                       
                    }
                    else
                    {
                        //mensaje de que esta factura ya esta utilizada
                        oXml = null;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Este CFDI ya fue utilizadfo en otra ministracion');", true);
                        return;
                    }

                }
                catch (Exception x)
                {
                    Limpia_Campos();
                    Console.Write(x.Message);
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                return;
            }

        }


        protected void btnCargarAGrid_Click(object sender, EventArgs e)
        {
            bool xmlOK = false;
            bool fileOk = false;
            string ticket;

            if (!fuplPDF.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo pdf es nesesario.');", true);
                return;
            }
            else
            {
                String fileExtension = System.IO.Path.GetExtension(fuplPDF.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (fupdTickets.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fupdTickets.FileName).ToLower();
                String[] allowedExtensions = { ".pdf", ".PDF" };
                bool fileticket = false;

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileticket = true;
                    }
                }

                if (fileticket)
                {
                    ticket = clsFuncionesGral.ConvertMinus(fupdTickets.FileName).Replace("pdf", "");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                    return;
                }
            }
            else
            {
                ticket = Dictionary.CADENA_NULA;
            }

            if (!fuplXML.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo xml es nesesario');", true);
                return;
            }
            else
            {
                String fileExtension = System.IO.Path.GetExtension(fuplXML.FileName).ToLower();
                String[] allowedExtensions = { ".xml", ".XML" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        xmlOK = true;
                    }
                }
            }

            string nombrepdf = clsFuncionesGral.ConvertMinus(fuplPDF.FileName);
            nombrepdf = nombrepdf.Replace(".pdf", "");

            string nombreXml = clsFuncionesGral.ConvertMinus(fuplXML.FileName);
            nombreXml = nombreXml.Replace(".xml", "");

            if (nombrepdf != nombreXml)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Nombres de archivos pdf y xml deben ser iguales.');", true);
                return;
            }
            else if ((fileOk) & (xmlOK))
            {

                bool sube;

                Valida_CarpetaXML();

                if (!File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName))
                {
                    fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                }
                else
                {
                    if (File.Exists(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName)) File.Delete(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                    fuplXML.PostedFile.SaveAs(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName);
                }

                List<Entidad> llEntidad = new List<Entidad>();
                Entidades.Xml oXml = new Entidades.Xml();

                clsFuncionesGral.Lee_XMl(Session["Crip_Ruta"].ToString() + "/" + fuplXML.FileName, llEntidad, oXml);

                //antes de subir checar que no este activo el uuid de factura si no insertar
                string existe = MngNegocioComprobacion.Exist_UUUID(oXml.TIMBRE_FISCAL);

                if ((existe == "") | (existe == null))
                { 
                    btnCargarPago.Enabled = true;
                    if (txtRfc.Text != oXml.RFC_EMISOR)
                    {
                        oXml = null;
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('RFC escrito y de CFDI no son los mismos.');", true);
                        return;
                    }
                    else 
                    {

                    }
                }
                else
                {
                    //mensaje de que esta factura ya esta utilizada
                    oXml = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                    return;
                }
            }
        }

        protected void lnkUpdateProovedor_Click(object sender, EventArgs e)
        {
            Proovedor loProovedor = new Proovedor ();

            loProovedor.RFC = txtRfc.Text;
            loProovedor.Razon_Social = txtRazonSocial.Text;
            loProovedor.Calle = txtCalle.Text;
            loProovedor.Num_Ext = txtNumExt.Text;
            loProovedor.Num_int = txtNumInt.Text;
            loProovedor.Colonia = txtColonia.Text;
            loProovedor.Municipio = txtMunicipio.Text;
            loProovedor.Ciudad = txtCuidad.Text;
            loProovedor.Estado = txtEstado.Text;
            loProovedor.Pais = txtPais.Text;
            loProovedor.CP = txtCp.Text;
            loProovedor.Email = txtEmail.Text;
            loProovedor.Contacto = txtContacto.Text;
            loProovedor.Telefono = txtTelefonoCon.Text;
            loProovedor.Telefono1 = txtTelefonoEmpresa.Text;
            loProovedor.Telefono2 = txtTelefonoEmpresa2.Text;
            loProovedor.Regimen_Fiscal = txtRegimenFiscal.Text;
            loProovedor.Servicio = txtServicio.Text;
            loProovedor.Cuenta = txtCuentaBancaria.Text;
            loProovedor.Banco = txtBanco.Text;
            loProovedor.Clabe = txtClabe.Text;

            bool update = mngNegocioProovedor.Update_Proveedor(loProovedor);

            if (update)
            {
               // Limpia_Campos();
               // clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
                //clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Datos Actualizado correctamente');", true);
                //return;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al actualizar favor de intentar de nuevo');", true);
                //return;
            }
        }

        protected void dplAdscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ads = dplAdscripcion.SelectedValue.ToString();

            if ((ads == "1000") | (ads == "5000"))
            {
                dplPrograma.DataSource = MngNegociosProgramas.ListaProgramas("'43','44','45','24','42'",Year );
                dplPrograma.DataTextField = Dictionary.DESCRIPCION;
                dplPrograma.DataValueField = Dictionary.CODIGO;
                dplPrograma.DataBind();
            }
            else
            {

                dplPrograma.DataSource = MngNegociosProgramas.ListaProgramas("'" + ads + "'", Year);
                dplPrograma.DataTextField = Dictionary.DESCRIPCION;
                dplPrograma.DataValueField = Dictionary.CODIGO;
                dplPrograma.DataBind();
            }
        }
    }
}