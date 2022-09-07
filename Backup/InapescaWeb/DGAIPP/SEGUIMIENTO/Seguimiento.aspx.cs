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

namespace InapescaWeb.DGAIPP.SEGUIMIENTO
{
    public partial class Seguimiento : System.Web.UI.Page
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
            Label4.Text = clsFuncionesGral.ConvertMayus("Oficio : ");
            Label5.Text = clsFuncionesGral.ConvertMayus("Complemento : ");
            Label7.Text = clsFuncionesGral.ConvertMayus("archivo : ");
            Label8.Text = clsFuncionesGral.ConvertMayus("documento referencia : ");
            Label9.Text = clsFuncionesGral.ConvertMayus("destinatario : ");
            Label10.Text = clsFuncionesGral.ConvertMayus("Oficina");

            Label3.Text = clsFuncionesGral.ConvertMayus(" aÑO");
            Label2.Text = clsFuncionesGral.ConvertMayus("Reservados : ");
            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = dictionary.DESCRIPCION;
            dplAnio.DataValueField = dictionary.CODIGO;
            dplAnio.DataBind();

            btnReservado.Text = clsFuncionesGral.ConvertMayus("cancelar reservado");
            btnOficio.Text = clsFuncionesGral.ConvertMayus("cancelar oficio");
            btnLimpiar.Text = clsFuncionesGral.ConvertMayus("limpiar busqueda");
            clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, false);
            clsFuncionesGral.Activa_Paneles(pnlTreview, false);
            Label1.Text = clsFuncionesGral.ConvertMayus("Oficios :");
            Label12.Text = clsFuncionesGral.ConvertMayus("reservados en oficio :");

            btnReservado.Enabled = false;

            btnOficio.Enabled = false;
            btnLimpiar.Enabled = false;

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

        protected void dplAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Anio = dplAnio.SelectedValue.ToString();
            if (Anio == string.Empty)
            {
                clsFuncionesGral.Activa_Paneles(pnlTreview, false);
                clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, false);
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione anio fiscal a buscar' );", true);
                return;
            }
            else
            {
                //   clsFuncionesGral.LlenarTreeViews (Anio  , tvOficios , false,"Seguimiento","DGAIPP", Session["Crip_RolDGAIPP"].ToString());// ConstruyeMenu();
                dplOficios.DataSource = MngNegocioMinutario.Lista_Oficios(Anio);
                dplOficios.DataTextField = dictionary.DESCRIPCION;
                dplOficios.DataValueField = dictionary.CODIGO;
                dplOficios.DataBind();

                //cargar lista de reservados activos
                List<Entidad> ListaReservados = MngNegocioMinutario.ListaReservados1(Anio, dictionary.NUMERO_CERO, dictionary.NUMERO_CERO, "", true);
                List<Entidad> ListaNew = new List<Entidad>();

                foreach (Entidad ent in ListaReservados)
                {
                    string[] ads = new string[2];
                    ads = ent.Codigo.Split(new Char[] { '|' });
                    Entidad enti = new Entidad();
                    enti.Codigo = ads[0].ToString();
                    enti.Descripcion = ent.Descripcion;
                    ListaNew.Add(enti);
                    enti = null;
                }

                ListaReservados = null;
                dplReservados.DataSource = ListaNew;
                dplReservados.DataTextField = dictionary.DESCRIPCION;
                dplReservados.DataValueField = dictionary.CODIGO;
                dplReservados.DataBind();

                clsFuncionesGral.Activa_Paneles(pnlTreview, true);
                clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, false);

            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string oficio = dplOficios.SelectedValue.ToString();

            string anio = dplAnio.SelectedValue.ToString();

            Minutario oMinutario = MngNegocioMinutario.oMinutario(anio, oficio, "");

            if (oMinutario.Oficio != null)
            {
                txtOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
                txtComplemento.Text = oMinutario.Complemento;
                lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
                txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
                txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
                txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);

                List<Entidad> ListaReservados = MngNegocioMinutario.ListaReservados1(anio, oficio, dictionary.NUMERO_CERO, "", false);
                ListBox1.Items.Clear();

                if (ListaReservados.Count > 0)
                {
                    foreach (Entidad ent in ListaReservados)
                    {
                        string[] ads = new string[2];
                        ads = ent.Codigo.Split(new Char[] { '|' });

                        if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                        {
                            // ListBox1.Items.Add(ads[0].ToString());
                            ListItem liRed = new ListItem(ads[0].ToString(), ads[0].ToString()); //Create a Red item
                            liRed.Attributes.Add("style",
                                 "background-color: RED");
                            ListBox1.Items.AddRange(new ListItem[] { liRed });
                        }
                        else
                        {
                            ListItem liRed1 = new ListItem(ads[0].ToString(), ads[0].ToString());
                            ListBox1.Items.AddRange(new ListItem[] { liRed1 });
                        }
                    }
                }

                oMinutario = null;

                if (ListaReservados.Count > 0)
                {
                    btnReservado.Enabled = true;
                }
                else
                {
                    btnReservado.Enabled = false;
                }

                btnOficio.Enabled = true;
                btnLimpiar.Enabled = true;
                clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, true);

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se encuentran resultados Intente nuevamente' );", true);
                return;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string lsreservado = dplReservados.SelectedValue.ToString();
            string anio = dplAnio.SelectedValue.ToString();

            Minutario oMinutario = MngNegocioMinutario.oMinutarioOficio(lsreservado, anio);

            if (oMinutario.Oficio != null)
            {
                txtOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
                txtComplemento.Text = oMinutario.Complemento;
                lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
                txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
                txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
                txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);

                List<Entidad> ListaReservados = new List<Entidad>();

                if (oMinutario.Oficio == dictionary.NUMERO_CERO)
                {
                    ListaReservados = MngNegocioMinutario.ListaReservados1(anio, dictionary.NUMERO_CERO, lsreservado, "", true);
                }
                else
                {
                    ListaReservados = MngNegocioMinutario.ListaReservados1(anio, oMinutario.Oficio, dictionary.NUMERO_CERO, "", false);
                }

                ListBox1.Items.Clear();

                if (ListaReservados.Count > 0)
                {
                    foreach (Entidad ent in ListaReservados)
                    {
                        string[] ads = new string[2];
                        ads = ent.Codigo.Split(new Char[] { '|' });

                        if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                        {
                            // ListBox1.Items.Add(ads[0].ToString());
                            ListItem liRed = new ListItem(ads[0].ToString(), ads[0].ToString()); //Create a Red item
                            liRed.Attributes.Add("style",
                                 "background-color: RED");
                            ListBox1.Items.AddRange(new ListItem[] { liRed });
                        }
                        else
                        {
                            ListItem liRed1 = new ListItem(ads[0].ToString(), ads[0].ToString());
                            ListBox1.Items.AddRange(new ListItem[] { liRed1 });
                        }
                    }
                }

                oMinutario = null;

                if (ListaReservados.Count > 0)
                {
                    btnReservado.Enabled = true;
                }
                else
                {
                    btnReservado.Enabled = false;
                }

                btnOficio.Enabled = false;
                btnLimpiar.Enabled = true;
                clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, true);

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se encuentran resultados Intente nuevamente' );", true);
                return;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            clsFuncionesGral.Activa_Paneles(pnlTreview, false);
            clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, false);
        }

        public void Limpiar()
        {
            txtOficio.Text = dictionary.CADENA_NULA;
            txtComplemento.Text = dictionary.CADENA_NULA;
            lblarchivo.Text = dictionary.CADENA_NULA;
            ListBox1.Items.Clear();
            txtDocRef.Text = dictionary.CADENA_NULA;
            txtDestinatario.Text = dictionary.CADENA_NULA;
            txtOficina.Text = dictionary.CADENA_NULA;

            dplOficios.Items.Clear();
            dplReservados.Items.Clear();
            dplAnio.SelectedIndex = 0;

            btnLimpiar.Enabled = false;
            btnOficio.Enabled = false;
            btnReservado.Enabled = false;
        }

        protected void btnReservado_Click(object sender, EventArgs e)
        {
            string lsReservado = ListBox1.SelectedItem.Text;
            string anio = dplAnio.SelectedValue.ToString();
          
            if (lsReservado == dictionary.NUMERO_CERO)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se puede cancelar un reservado cero, escoja otro ' );", true);
                return;
            }
            else
            {
                Minutario oMinutario = MngNegocioMinutario.oMinutarioOficio(lsReservado, anio);
               
                    List<Entidad> ListaReservados = new List<Entidad>();

                    if (oMinutario.Oficio == dictionary.NUMERO_CERO)
                    {
                        ListaReservados = MngNegocioMinutario.ListaReservados1(anio, dictionary.NUMERO_CERO, lsReservado, "", true);
                    }
                    else
                    {
                        ListaReservados = MngNegocioMinutario.ListaReservados1(anio, oMinutario.Oficio, dictionary.NUMERO_CERO, "", false);
                    }

                    ListBox1.Items.Clear();

                    if (ListaReservados.Count > 0)
                    {
                        foreach (Entidad ent in ListaReservados)
                        {
                            string[] ads = new string[2];
                            ads = ent.Codigo.Split(new Char[] { '|' });

                            if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                            {
                                // ListBox1.Items.Add(ads[0].ToString());
                                ListItem liRed = new ListItem(ads[0].ToString(), ads[0].ToString()); //Create a Red item
                                liRed.Attributes.Add("style",
                                     "background-color: RED");
                                ListBox1.Items.AddRange(new ListItem[] { liRed });
                            }
                            else
                            {
                                ListItem liRed1 = new ListItem(ads[0].ToString(), ads[0].ToString());
                                ListBox1.Items.AddRange(new ListItem[] { liRed1 });
                            }
                        }
                   

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El reservado ya se encuentra cancelado.' );", true);   
                }
                else
                {
                    bool resultado = MngNegocioMinutario.Update_Estatus_Reservado(lsReservado, anio, dictionary.NUMERO_CERO, txtOficio.Text,false );

                    oMinutario = MngNegocioMinutario.oMinutarioOficio(lsReservado, anio);

                    if (oMinutario.Oficio != null)
                    {
                        txtOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
                        txtComplemento.Text = oMinutario.Complemento;
                        lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
                        txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
                        txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
                        txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);

                       // List<Entidad> ListaReservados = new List<Entidad>();

                        if (oMinutario.Oficio == dictionary.NUMERO_CERO)
                        {
                            ListaReservados = MngNegocioMinutario.ListaReservados1(anio, dictionary.NUMERO_CERO, lsReservado, "", true);
                        }
                        else
                        {
                            ListaReservados = MngNegocioMinutario.ListaReservados1(anio, oMinutario.Oficio, dictionary.NUMERO_CERO, "", false);
                        }

                        ListBox1.Items.Clear();

                        if (ListaReservados.Count > 0)
                        {
                            foreach (Entidad ent in ListaReservados)
                            {
                                string[] ads = new string[2];
                                ads = ent.Codigo.Split(new Char[] { '|' });

                                if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                                {
                                    // ListBox1.Items.Add(ads[0].ToString());
                                    ListItem liRed = new ListItem(ads[0].ToString(), ads[0].ToString()); //Create a Red item
                                    liRed.Attributes.Add("style",
                                         "background-color: RED");
                                    ListBox1.Items.AddRange(new ListItem[] { liRed });
                                }
                                else
                                {
                                    ListItem liRed1 = new ListItem(ads[0].ToString(), ads[0].ToString());
                                    ListBox1.Items.AddRange(new ListItem[] { liRed1 });
                                }
                            }
                        }

                        oMinutario = null;

                        if (ListaReservados.Count > 0)
                        {
                            btnReservado.Enabled = true;
                        }
                        else
                        {
                            btnReservado.Enabled = false;
                        }

                        btnOficio.Enabled = false;
                        btnLimpiar.Enabled = true;
                        clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, true);

                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha cancelado correctamente el reservado seleccionado' );", true);
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se encuentran resultados Intente nuevamente' );", true);
                        return;
                    }
                }
            }
        }

        protected void btnOficio_Click(object sender, EventArgs e)
        {

            string lsOficio = txtOficio.Text;
            string anio = dplAnio.SelectedValue.ToString();

            if (lsOficio == dictionary.NUMERO_CERO)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se puede cancelar un oficio cero, escoja otro ' );", true);
                return;
            }
            else
            {
               
                bool resultado = MngNegocioMinutario.Update_Estatus_Reservado(dictionary.NUMERO_CERO , anio, dictionary.NUMERO_CERO, txtOficio.Text  , true );

                Minutario oMinutario = MngNegocioMinutario.oMinutario(anio, lsOficio , "");
                
                if (oMinutario.Oficio != null)
                {
                    txtOficio.Text = clsFuncionesGral.ConvertMayus(oMinutario.Oficio + "  " + oMinutario.Complemento);
                    txtComplemento.Text = oMinutario.Complemento;
                    lblarchivo.Text = clsFuncionesGral.ConvertMayus(oMinutario.Archivo);
                    txtDocRef.Text = clsFuncionesGral.ConvertMayus(oMinutario.Docuemnto_Referencia);
                    txtDestinatario.Text = clsFuncionesGral.ConvertMayus(oMinutario.Usuario_destino);
                    txtOficina.Text = clsFuncionesGral.ConvertMayus(oMinutario.Descripcion);

                    List<Entidad> ListaReservados = MngNegocioMinutario.ListaReservados1(anio, oMinutario.Oficio , dictionary.NUMERO_CERO, "", false);
                    ListBox1.Items.Clear();

                    if (ListaReservados.Count > 0)
                    {
                        foreach (Entidad ent in ListaReservados)
                        {
                            string[] ads = new string[2];
                            ads = ent.Codigo.Split(new Char[] { '|' });

                            if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                            {
                                // ListBox1.Items.Add(ads[0].ToString());
                                ListItem liRed = new ListItem(ads[0].ToString(), ads[0].ToString()); //Create a Red item
                                liRed.Attributes.Add("style",
                                     "background-color: RED");
                                ListBox1.Items.AddRange(new ListItem[] { liRed });
                            }
                            else
                            {
                                ListItem liRed1 = new ListItem(ads[0].ToString(), ads[0].ToString());
                                ListBox1.Items.AddRange(new ListItem[] { liRed1 });
                            }
                        }
                    }

                    oMinutario = null;

                    if (ListaReservados.Count > 0)
                    {
                        btnReservado.Enabled = true;
                    }
                    else
                    {
                        btnReservado.Enabled = false;
                    }

                    btnOficio.Enabled = true;
                    btnLimpiar.Enabled = true;
                    clsFuncionesGral.Activa_Paneles(pnlDetalleOficio, true);

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha cancelado correctamente el oficio  seleccionado' );", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se encuentran resultados Intente nuevamente' );", true);
                    return;
                }

            }
        }



    }
}