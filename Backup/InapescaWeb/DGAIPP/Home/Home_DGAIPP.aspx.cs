using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;

namespace InapescaWeb.DGAIPP
{
    public partial class Home_DGAIPP : System.Web.UI.Page
    {
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static clsDictionary dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView_DGAIPP("0", tvMenu, false, Session["Crip_RolDGAIPP"].ToString());// ConstruyeMenu();
            }
        }

        public void Carga_Valores()
        {
            lblTitle.Text = clsFuncionesGral.ConvertMayus("MInutario DGAIPP");
            Label1.Text = clsFuncionesGral.ConvertMayus("Tipo de consecutivo a Agregar");

            dplTipo.DataSource = MngNegocioMinutario.Lista_Tipos_Oficios(Session["Crip_RolDGAIPP"].ToString());
            dplTipo.DataValueField = "Codigo";
            dplTipo.DataTextField = "Descripcion";
            dplTipo.DataBind();

            // Label2.Text = clsFuncionesGral.ConvertMayus("asunto / expediente");
            Label3.Text = clsFuncionesGral.ConvertMayus(" aÑO");
            Label4.Text = clsFuncionesGral.ConvertMayus("Numero de Oficio :");
            Label5.Text = clsFuncionesGral.ConvertMayus("complemento :");

            //Label6.Visible = false;

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = dictionary.DESCRIPCION;
            dplAnio.DataValueField = dictionary.CODIGO;
            dplAnio.DataBind();

            clsFuncionesGral.Activa_Paneles(pnlOficio, false);
            // chkAddReserv.Visible = false;
            // chkAddReserv.Text = clsFuncionesGral.ConvertMayus("aGREGAR  NUEVO RESERVADO A OFICIO");

            //Label6.Visible = false;
            lblOficio.Visible = false;
            lblarchivo.Visible = false;
            txtDocRef.Visible = false;
            txtDestinatario.Visible = false;
            txtOficina.Visible = false;

            txtDocRef.Enabled = false;
            txtDestinatario.Enabled = false;
            txtOficina.Enabled = false;

            Label2.Text = clsFuncionesGral.ConvertMayus(" Numero de Oficio :");
            Label7.Text = clsFuncionesGral.ConvertMayus("Archivo : ");
            Label8.Text = clsFuncionesGral.ConvertMayus("Documento de Referencia :");
            Label9.Text = clsFuncionesGral.ConvertMayus("Destinatario : ");
            Label10.Text = clsFuncionesGral.ConvertMayus("Oficina o Crip :");
            Label11.Text = clsFuncionesGral.ConvertMayus("Agregar Registro ");
            Label18.Text = clsFuncionesGral.ConvertMayus("Agregar Registro ");


            Label12.Text = clsFunciones.ConvertMayus("NúmeroS de reservado :");

            chkDepInp.Text = clsFuncionesGral.ConvertMayus("Oficina externa a Inapesca");
            CheckBox1.Text = clsFuncionesGral.ConvertMayus("Oficina externa a Inapesca");
            Label13.Text = clsFuncionesGral.ConvertMayus("adscripcion de inapesca");

            dplDep.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
            dplDep.DataTextField = "Descripcion";
            dplDep.DataValueField = "Codigo";
            dplDep.DataBind();

            dplDep1.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
            dplDep1.DataTextField = "Descripcion";
            dplDep1.DataValueField = "Codigo";
            dplDep1.DataBind();

            Label14.Text = clsFuncionesGral.ConvertMayus("adscripcion externa a inapesca");
            Label15.Text = clsFuncionesGral.ConvertMayus("Documento de referencia");
            Label16.Text = clsFuncionesGral.ConvertMayus("destinatario ");
            Label17.Text = clsFuncionesGral.ConvertMayus("Reservado");

            clsFuncionesGral.Activa_Paneles(pnlreservado, false);
            clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
            clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
            clsFuncionesGral.Activa_Paneles(Panel1, false);
            clsFuncionesGral.Activa_Paneles(Panel2, false);
            clsFuncionesGral.Activa_Paneles(pnlOficios, false);
            clsFuncionesGral.Activa_Paneles(Panel3, false);

            Label6.Text = clsFuncionesGral.ConvertMayus("Oficios sin reservado :");
            Label19.Text = clsFuncionesGral.ConvertMayus("Expediente : ");
            Label20.Text = clsFuncionesGral.ConvertMayus("Núm. Oficio");
            Label21.Text = clsFuncionesGral.ConvertMayus("Complemento : ") + " Ej. (BIS, BisA, etc)";
            Label22.Text = clsFuncionesGral.ConvertMayus("adscripcion de inapesca");
            Label23.Text = clsFuncionesGral.ConvertMayus("adscripcion esxterna a inapesca : ");
            Label24.Text = clsFuncionesGral.ConvertMayus("Expediente : ");
            Label25.Text = clsFuncionesGral.ConvertMayus("Documento referencia :");
            Label26.Text = clsFuncionesGral.ConvertMayus("Destinatario :");
            Label27.Text = clsFuncionesGral.ConvertMayus("Agregar registro ");
            Label28.Text = clsFuncionesGral.ConvertMayus("reservado sin  Oficio");
            Label29.Text = clsFuncionesGral.ConvertMayus("Oficio DGAIPP :");
            Label30.Text = clsFuncionesGral.ConvertMayus("Complemento") + "( Ej. Bis, BisA, etc )";
            Label31.Text = clsFuncionesGral.ConvertMayus("Relatoria :");
            Label32.Text = clsFuncionesGral.ConvertMayus("expediente");
            Label33.Text = clsFuncionesGral.ConvertMayus("Documento referencia");
            Label34.Text = clsFuncionesGral.ConvertMayus("destinatario");
            Label35.Text = clsFuncionesGral.ConvertMayus("Agregar registro");
            chkOficoNew.Text = clsFuncionesGral.ConvertMayus(" Generar nuevo número de Oficio ");
        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string lsModulo;
            string lsRol = Session["Crip_RolDGAIPP"].ToString();

            if (tvMenu.SelectedNode != null)
            {
                lsModulo = Convert.ToString(tvMenu.SelectedNode.Value);

                WebPage objWebPage = MngNegocioMenu.MngNegocioURLDGAIPP(lsModulo, lsRol);

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


        //action del selector de tipo de oficios
        protected void dplTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstipo = dplTipo.SelectedValue.ToString();
            ListBox1.Items.Clear();
            //oficio de comision valida acceso smaf crea nuevas variables de session de smaf.sytes
            if (lstipo == "OC")
            {
                Entidades.Login objLogin = new Entidades.Login();
                // string cad = clsFuncionesGral.ConvertMayus(txtPassword.Text);
                objLogin.Usuario = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(Session["Crip_UsuarioDGAIPP"].ToString()));
                objLogin.Password = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus(MngEncriptacionDgaipp.decripString(Session["Crip_PasswordDGAIPP"].ToString())));
                // objLogin.Ubicacion = MngEncriptacion.encryptString(clsFuncionesGral.ConvertMayus("43"));

                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);

                Valida_Session(objLogin);

            }
            else if (lstipo == "RO") //reservado de oficio
            {
                clsFuncionesGral.Activa_Paneles(pnlOficio, true);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);

            }
            else if (lstipo == "RI") //activar paneles de otros oficios y reservados
            {
                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, true);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, true);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);
                txtReservado.Text = MngNegocioMinutario.Obtiene_Max_Reservado(year);

            }
            else if ((lstipo == "DT") | (lstipo == "II") | (lstipo == "OF") | (lstipo == "OP") | (lstipo == "SI"))
            {
                clsFuncionesGral.Activa_Paneles(pnlOficios, true);
                txtOficio1.Text = MngNegocioMinutario.Obtiene_Max_Oficio(year);
                clsFuncionesGral.Activa_Paneles(Panel1, true);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);
            }
            else if ((lstipo == "OR"))
            {
                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(Panel3, true);
                //  txtoficio.Text = MngNegocioMinutario.Obtiene_Max_Oficio(year);

                dplReservado.DataSource = MngNegocioMinutario.ListaReservados(year, "0");
                dplReservado.DataTextField = dictionary.DESCRIPCION;
                dplReservado.DataValueField = dictionary.CODIGO;
                dplReservado.DataBind();

                dplOficios.DataSource = MngNegocioMinutario.Lista_Oficios_Sin_Reservado(year);
                dplOficios.DataTextField = dictionary.DESCRIPCION;
                dplOficios.DataValueField = dictionary.CODIGO;
                dplOficios.DataBind();

                clsFuncionesGral.Activa_Paneles(pnlNewOficio, false);

            }

        }

        public void Valida_Session(Entidades.Login objLogin)
        {
            Entidades.Usuario oUsuario = new Usuario();

            oUsuario = MngNegocioLogin.Acceso_Smaf(objLogin.Ubicacion, objLogin.Usuario, objLogin.Password);

            Response.ContentType = "text/xml";

            if (!oUsuario.Usser.Equals("0"))
            {
                //  objUsuario = new Usuario();
                // objUsuario =MngNegocioLogin.DatosUsuario (); 
                Session.Timeout = 20;
                Session.LCID = 2057;
                Session["Version"] = "1.0";
                Session["Crip_Usuario"] = oUsuario.Usser;
                Session["Crip_Password"] = oUsuario.Password;
                Session["Crip_Nivel"] = oUsuario.Nivel;
                Session["Crip_Plaza"] = oUsuario.Plaza;
                Session["Crip_Puesto"] = oUsuario.Puesto;
                Session["Crip_Secretaria"] = oUsuario.Secretaria;
                Session["Crip_Organismo"] = oUsuario.Organismo;
                Session["Crip_Ubicacion"] = oUsuario.Ubicacion;
                Session["Crip_Area"] = oUsuario.Area;
                Session["Crip_Nombre"] = oUsuario.Nombre;
                Session["Crip_ApPat"] = oUsuario.ApPat;
                Session["Crip_ApMat"] = oUsuario.ApMat;
                Session["Crip_RFC"] = oUsuario.RFC;
                Session["Crip_Cargo"] = oUsuario.Cargo;
                Session["Crip_Email"] = oUsuario.Email;
                Session["Crip_Rol"] = oUsuario.Rol;
                Session["Crip_Abreviatura"] = oUsuario.Abreviatura;

                Response.Redirect("../../Home/Home.aspx", true);
            }
            else
            {
                // string Error = MngDatosLogin.ReturnError();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Error de autenticacion en smaf, su usuario con cuenta con permisos para esta aplicacion');", true);
                return;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string oficio = dplNumOficio.SelectedValue.ToString();
            string complemento = txtComplemento.Text;
            string anio = dplAnio.SelectedValue.ToString();

            if (anio == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un anio a buscar');", true);
                return;
            }
            if (!clsFuncionesGral.IsNumeric(oficio))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Numero de oficio debe ser numérico);", true);
                return;
            }

            if ((txtComplemento.Text == null) | (txtComplemento.Text == ""))
            {
                complemento = "";
            }

            Minutario oMinutario = MngNegocioMinutario.oMinutario(anio, oficio, complemento);

            //string resultado =  MngNegocioMinutario.ObtieneExpediente(anio, oficio, complemento);

            if ((oMinutario.Oficio != "") | (oMinutario.Oficio != null))
            {
                //  Label6.Text = oMinutario.Expediente ;
                // Label6.Visible = true;

                //if ((oMinutario.Reservado == null) | (oMinutario.Reservado == "") | (oMinutario.Reservado == "0"))
                //{
                //  chkAddReserv.Visible = false;
                //  }
                // else
                //{
                //    chkAddReserv.Visible = true ;
                //}

                //AGREGAR TABLA DE DATOS DE OFICIO Y RESERVADOS
                lblOficio.Visible = true;
                lblarchivo.Visible = true;
                txtDocRef.Visible = true;
                txtDestinatario.Visible = true;
                txtOficina.Visible = true;
                lblOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
                lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
                txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
                txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
                txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);
                // lblReservado.Text = clsFuncionesGral.ConvertMayus(oMinutario .Reservado );

                List<Entidad> ListaReservados = MngNegocioMinutario.ListaReservados(anio, oficio, complemento);
                ListBox1.Items.Clear();
                foreach (Entidad ent in ListaReservados)
                {
                    if (clsFuncionesGral.Convert_Double(ent.Codigo) > 0)
                    {
                        ListBox1.Items.Add(ent.Codigo);
                    }
                }

                txtDocRef.Enabled = true;
                txtDestinatario.Enabled = true;
                txtOficina.Enabled = true;
                oMinutario = null;
            }
            else
            {

                lblOficio.Visible = false;
                lblarchivo.Visible = false;
                txtDocRef.Visible = false;
                txtDestinatario.Visible = false;
                txtOficina.Visible = false;
                oMinutario = null;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se encontraron registros favor de validar);", true);
                return;
            }

        }

        //agrega reservado a oficio
        protected void imgAddReservaOficio_Click(object sender, ImageClickEventArgs e)
        {
            string oficio = dplNumOficio.SelectedValue.ToString();
            string complemento = txtComplemento.Text;
            string anio = dplAnio.SelectedValue.ToString();

            if ((txtComplemento.Text == null) | (txtComplemento.Text == ""))
            {
                complemento = "";
            }

            Minutario oMinutario = MngNegocioMinutario.oMinutario(anio, oficio, complemento);

            if (clsFuncionesGral.Convert_Double(oMinutario.Reservado) > clsFuncionesGral.Convert_Double(dictionary.NUMERO_CERO))
            {
                //actualiza estatus de para nuevo reservado
                //   Boolean update = MngNegocioMinutario.Update_Reservado(MngNegocioMinutario.Obtiene_Max_Reservado(anio, oficio, complemento), anio, oficio, oMinutario.Secuencia, complemento, true);
                //insertar nuevo registro de minutario
                Boolean update = MngDatosMinutario.Inserta_Oficio_Comision(oMinutario.Oficio, oMinutario.Archivo, oMinutario.Tipo, clsFuncionesGral.ConvertString(clsFuncionesGral.ConvertInteger(oMinutario.Reservado) + 1), oMinutario.Expediente, txtDocRef.Text, oMinutario.Ubicacion_Destino, oMinutario.Descripcion, oMinutario.Usuario_destino, oMinutario.Usuario_Captura, clsFuncionesGral.ConvertString(clsFuncionesGral.ConvertInteger(oMinutario.Secuencia) + 1), oMinutario.Periodo);
            }
            else
            {
                Boolean update = MngNegocioMinutario.Update_Reservado(MngNegocioMinutario.Obtiene_Max_Reservado(anio, oficio, complemento), anio, oficio, oMinutario.Secuencia, complemento);
            }


            oMinutario = null;
            oMinutario = MngNegocioMinutario.oMinutario(anio, oficio, complemento);

            lblOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
            lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
            txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
            txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
            txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);
            //         lblReservado.Text = clsFuncionesGral.ConvertMayus(oMinutario.Reservado);

            List<Entidad> ListaReservados = MngNegocioMinutario.ListaReservados(anio, oficio, complemento);
            ListBox1.Items.Clear();
            foreach (Entidad ent in ListaReservados)
            {
                if (clsFuncionesGral.Convert_Double(ent.Codigo) > 0)
                {
                    ListBox1.Items.Add(ent.Codigo);
                }
            }
            oMinutario = null;

            clsFuncionesGral.Activa_Paneles(pnlOficios, false);
            clsFuncionesGral.Activa_Paneles(Panel1, false);
            clsFuncionesGral.Activa_Paneles(Panel2, false);
            clsFuncionesGral.Activa_Paneles(pnlOficio, false);
            clsFuncionesGral.Activa_Paneles(pnlreservado, false);
            clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
            clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
            clsFuncionesGral.Activa_Paneles(Panel3, false );
            dplTipo.SelectedIndex = 0;


            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha agregado correctamente el registro' );", true);
       


            /*       else
                  { 
                  //actualizar linea de maximo reservado
                      string reservado = MngNegocioMinutario.Obtiene_Max_Reservado(anio, oficio, complemento);
                
                      bool modificado = false;

                      Minutario oMinutario = MngNegocioMinutario.oMinutario(anio, oficio, complemento);
                      if (oMinutario.Docuemnto_Referencia != txtDocRef.Text)
                      {
                          modificado = true;
                      }

                      if (oMinutario.Usuario_destino != txtDestinatario.Text)
                      {
                          modificado = true;
                      }

                      if (oMinutario.Descripcion != txtOficina.Text)
                      {
                          modificado = true;
                      }

                      if (modificado)
                      {

                          Boolean update = MngNegocioMinutario.Update_Reservado(MngNegocioMinutario.Obtiene_Max_Reservado(anio, oficio, complemento), anio, oficio,oMinutario.Secuencia , complemento);

                          oMinutario = null;
                          oMinutario = MngNegocioMinutario.oMinutario(anio, oficio, complemento);

                          lblOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
                          lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
                          txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
                          txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
                          txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);
                          lblReservado.Text = clsFuncionesGral.ConvertMayus(oMinutario.Reservado);

                          if ((oMinutario.Reservado == null) | (oMinutario.Reservado == "") | (oMinutario.Reservado == "0"))
                          {
                              chkAddReserv.Visible = false;
                          }
                          else
                          {
                              chkAddReserv.Visible = true;
                          }

                          oMinutario = null;
                      }
                  }
                  */
        }
        /*
                protected void chkAddReserv_CheckedChanged(object sender, EventArgs e)
                {
                    if (chkAddReserv.Checked)
                    {
                        txtDocRef.Enabled = true;
                        txtDestinatario.Enabled = true;
                        txtOficina.Enabled = true;
                    }
                    else
                    {
                        txtDocRef.Enabled = false ;
                        txtDestinatario.Enabled = false ;
                        txtOficina.Enabled = false ;
                    }
                }
                */
        protected void chkDepInp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDepInp.Checked)
            {
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlDepInp, true);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //validar campos obligatorios
            if (!chkDepInp.Checked)
            {
                if (dplDep.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adscripcion a buscar');", true);
                    return;
                }
            }
            else
            {
                if ((txtExtInp.Text == "") | (txtExtInp.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote una oficina externa a inapesca a enviar oficio');", true);
                    return;
                }
            }

            if ((txtExpediente1.Text == null) | (txtExpediente1.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un expediente o asunto para reservado');", true);
                return;
            }

            if ((txtDocRef1.Text == null) | (txtDocRef1.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un documento de referencia para reservado');", true);
                return;
            }

            if ((txtDestinatario1.Text == null) | (txtDestinatario1.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un destinatario de referencia para reservado');", true);
                return;
            }

            string lsDep = dplDep.SelectedValue.ToString();
            string lsDeldes = dplDep.SelectedItem.Text;


            if (!chkDepInp.Checked)
            {
                bool update = MngDatosMinutario.Inserta_Oficio_Comision(dictionary.NUMERO_CERO, dictionary.CADENA_NULA, "RI", txtReservado.Text, txtExpediente1.Text, txtDocRef1.Text, lsDep, lsDeldes, txtDestinatario1.Text, Session["Crip_NombreDGAIPP"].ToString() + " " + Session["Crip_ApPatDGAIPP"].ToString() + " " + Session["Crip_ApMatDGAIPP"].ToString(), "1", year);
                //limpiar datos
                txtExtInp.Text = "";
                txtExpediente1.Text = "";
                txtDocRef1.Text = "";
                txtDestinatario1.Text = "";
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, true);
                dplDep.SelectedIndex = 0;
                txtReservado.Text = MngNegocioMinutario.Obtiene_Max_Reservado(year);

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha isertado un nuevo reservado');", true);
                return;
            }
            else
            {
                bool update = MngDatosMinutario.Inserta_Oficio_Comision(dictionary.NUMERO_CERO, dictionary.CADENA_NULA, "RI", txtReservado.Text, txtExpediente1.Text, txtDocRef1.Text, "0", txtExtInp.Text, txtDestinatario1.Text, Session["Crip_NombreDGAIPP"].ToString() + " " + Session["Crip_ApPatDGAIPP"].ToString() + " " + Session["Crip_ApMatDGAIPP"].ToString(), "1", year);

                txtExtInp.Text = "";
                txtExpediente1.Text = "";
                txtDocRef1.Text = "";
                txtDestinatario1.Text = "";
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, true);
                dplDep.SelectedIndex = 0;
                txtReservado.Text = MngNegocioMinutario.Obtiene_Max_Reservado(year);


                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);
                dplTipo.SelectedIndex = 0;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha isertado un nuevo reservado');", true);
                return;
            }
        }

        //agrega ofiicio sin reservado
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

            if (!CheckBox1.Checked)
            {
                if (dplDep1.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una adscripcion a buscar');", true);
                    return;
                }
            }
            else
            {
                if ((txtExtInp1.Text == "") | (txtExtInp1.Text == null))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote una oficina externa a inapesca a enviar oficio');", true);
                    return;
                }
            }


            if ((txtExp1.Text == null) | (txtExp1.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un expediente o asunto para reservado');", true);
                return;
            }

            if ((txtDoc1.Text == null) | (txtDoc1.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un documento de referencia para reservado');", true);
                return;
            }

            if ((txtDesti1.Text == null) | (txtDesti1.Text == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un destinatario de referencia para reservado');", true);
                return;
            }

            string lsDep = dplDep1.SelectedValue.ToString();
            string lsDeldes = dplDep1.SelectedItem.Text;

            if (!CheckBox1.Checked)
            {
                bool update = MngDatosMinutario.Inserta_Oficio_Comision(txtOficio1.Text, txtExp1.Text, dplTipo.SelectedValue.ToString(), dictionary.NUMERO_CERO, txtExp1.Text, txtDoc1.Text, lsDep, lsDeldes, txtDesti1.Text, Session["Crip_NombreDGAIPP"].ToString() + " " + Session["Crip_ApPatDGAIPP"].ToString() + " " + Session["Crip_ApMatDGAIPP"].ToString(), "1", year);
                //limpiar datos
                txtExtInp1.Text = "";
                txtExp1.Text = "";
                txtDoc1.Text = "";
                txtDesti1.Text = "";
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(Panel1, true);
                dplDep1.SelectedIndex = 0;
                txtOficio1.Text = MngNegocioMinutario.Obtiene_Max_Oficio(year);

                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);
                dplTipo.SelectedIndex = 0;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha insertado un nuevo folio de oficio : " + dplTipo.SelectedItem.Text + "');", true);
                return;
            }
            else
            {
                bool update = MngDatosMinutario.Inserta_Oficio_Comision(txtOficio1.Text, txtExp1.Text, dplTipo.SelectedValue.ToString(), dictionary.NUMERO_CERO, txtExp1.Text, txtDoc1.Text, dictionary.NUMERO_CERO, txtExtInp1.Text, txtDesti1.Text, Session["Crip_NombreDGAIPP"].ToString() + " " + Session["Crip_ApPatDGAIPP"].ToString() + " " + Session["Crip_ApMatDGAIPP"].ToString(), "1", year);

                txtExtInp1.Text = "";
                txtExp1.Text = "";
                txtDoc1.Text = "";
                txtDesti1.Text = "";
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(Panel1, true);
                dplDep1.SelectedIndex = 0;
                txtOficio1.Text = MngNegocioMinutario.Obtiene_Max_Oficio(year);

                clsFuncionesGral.Activa_Paneles(pnlOficios, false);
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
                clsFuncionesGral.Activa_Paneles(pnlOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlreservado, false);
                clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
                clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
                clsFuncionesGral.Activa_Paneles(Panel3, false);
                dplTipo.SelectedIndex = 0;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha insertado un nuevo folio de oficio : " + dplTipo.SelectedItem.Text + "');", true);
                return;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                clsFuncionesGral.Activa_Paneles(Panel1, false);
                clsFuncionesGral.Activa_Paneles(Panel2, true);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(Panel1, true);
                clsFuncionesGral.Activa_Paneles(Panel2, false);
            }
        }

        //acualiza datos de oficio a reservado
        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {/*
             if ((txtReervado.Text == "") | (txtReervado.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un numero de reservado a asignar oficio');", true);
                return;
            }

            if (!clsFuncionesGral.IsNumeric(txtReervado.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de reservado debe ser numerico');", true);
                return;
            }
            */

            if (((txtExpe1.Text == "") | (txtExpe1.Text == null)) | ((txtUsuarioDestino.Text == "") | (txtUsuarioDestino.Text == null)))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Realize una busqueda de reservado para cargar datos a actualizar');", true);
                return;
            }

            string reservado = dplReservado.SelectedValue.ToString();
            string lsOficio = "";
            Minutario oMinutario = MngNegocioMinutario.oMinutarioOficio(reservado, year);

            if (chkOficoNew.Checked)
            {
                lsOficio = txtoficio.Text;
            }
            else
            {
                lsOficio = dplOficios.SelectedValue.ToString();
            }

            oMinutario.Oficio = lsOficio;
            oMinutario.Complemento = txtComple.Text;
            oMinutario.Archivo = txtRelatoria.Text;
            oMinutario.Usuario_destino = txtUsuarioDestino.Text;
            oMinutario.Docuemnto_Referencia = txtDocReferen.Text;
            bool update = MngNegocioMinutario.Update_Oficio(oMinutario);

            oMinutario = null;


            txtComple.Text = "";
            txtRelatoria.Text = "";
            txtExpe1.Text = "";
            txtDocReferen.Text = "";
            txtUsuarioDestino.Text = "";

            dplReservado.DataSource = MngNegocioMinutario.ListaReservados(year, "0");
            dplReservado.DataTextField = dictionary.DESCRIPCION;
            dplReservado.DataValueField = dictionary.CODIGO;
            dplReservado.DataBind();

            dplOficios.DataSource = MngNegocioMinutario.Lista_Oficios_Sin_Reservado(year);
            dplOficios.DataTextField = dictionary.DESCRIPCION;
            dplOficios.DataValueField = dictionary.CODIGO;
            dplOficios.DataBind();

            clsFuncionesGral.Activa_Paneles(pnlNewOficio, false);
            clsFuncionesGral.Activa_Paneles(pnlOficioSinReservado, true);
            txtoficio.Text = dictionary.CADENA_NULA;
            chkOficoNew.Checked = false;
    txtoficio.Text = "";

    clsFuncionesGral.Activa_Paneles(pnlOficios, false);
    clsFuncionesGral.Activa_Paneles(Panel1, false);
    clsFuncionesGral.Activa_Paneles(Panel2, false);
    clsFuncionesGral.Activa_Paneles(pnlOficio, false);
    clsFuncionesGral.Activa_Paneles(pnlreservado, false);
    clsFuncionesGral.Activa_Paneles(pnlDepInp, false);
    clsFuncionesGral.Activa_Paneles(pnlDepExt, false);
    clsFuncionesGral.Activa_Paneles(Panel3, false);
    dplTipo.SelectedIndex = 0;
            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha agregado correctamente el oficio :" + lsOficio + " al  reservado : " + reservado + "' );", true);
            //txtoficio.Text = MngNegocioMinutario.Obtiene_Max_Oficio(year); 

            // txtReervado.Text = "";
           

            return;

        }

        //obtiene datos de reservado
        /*  protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
           {
               if ((txtReervado.Text == "") | (txtReervado.Text == null))
               {
                   ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Anote un numero de reservado a asignar oficio');", true);
                   return;
               }

               if (!clsFuncionesGral.IsNumeric(txtReervado.Text))
               {
                   ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de reservado debe ser numerico');", true);
                   return;
               }

               Minutario oMinutario = MngNegocioMinutario.oMinutarioOficio(txtReervado.Text,year );

               txtExpe1.Text = clsFuncionesGral.ConvertMayus(oMinutario.Expediente );
               txtDocReferen.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia );
               txtUsuarioDestino.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino );
               oMinutario = null;
           }*/

        //obtiene datos de reservado
        protected void dplReservado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsReservado = dplReservado.SelectedValue.ToString();

            Minutario oMinutario = MngNegocioMinutario.oMinutarioOficio(lsReservado, year);

            txtExpe1.Text = clsFuncionesGral.ConvertMayus(oMinutario.Expediente);
            txtDocReferen.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
            txtUsuarioDestino.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
            oMinutario = null;
        }

        protected void chkOficoNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOficoNew.Checked)
            {
                clsFuncionesGral.Activa_Paneles(pnlNewOficio, true);
                clsFuncionesGral.Activa_Paneles(pnlOficioSinReservado, false);
                txtoficio.Text = MngNegocioMinutario.Obtiene_Max_Oficio(year);
            }
            else
            {
                clsFuncionesGral.Activa_Paneles(pnlNewOficio, false);
                clsFuncionesGral.Activa_Paneles(pnlOficioSinReservado, true);
                txtoficio.Text = dictionary.CADENA_NULA;
            }
        }

        protected void dplAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            dplNumOficio.DataSource = MngNegocioMinutario.Lista_Oficios(dplAnio.SelectedValue.ToString());
            dplNumOficio.DataTextField = dictionary.DESCRIPCION;
            dplNumOficio.DataValueField = dictionary.CODIGO;
            dplNumOficio.DataBind();

        }



    }
}