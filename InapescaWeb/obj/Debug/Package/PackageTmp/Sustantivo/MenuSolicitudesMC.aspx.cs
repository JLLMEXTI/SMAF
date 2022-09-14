using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Windows.Forms;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

namespace InapescaWeb.Sustantivo
{
    public partial class MenuSolicitudesMC : System.Web.UI.Page
    {
        static string Year = DateTime.Today.Year.ToString();
        static clsDictionary Dictionary = new clsDictionary();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    string lsSession = Session["Crip_Rol"].ToString();

                    string lsPermiso = MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ENLACEDIR);

                    if ((lsSession == Dictionary.DIRECTOR_ADJUNTO) || ((lsSession == Dictionary.JEFE_DEPARTAMENTO) && (lsPermiso == Dictionary.PERMISO_ENLACEDIR)))
                    {
                        // Carga_Session();
                        clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());
                        Carga_Valores();
                        Carga_Listas("1", Year);
                    }
                    else 
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No cuenta con los permisos nesesarios para realizar esta accion.');", true);
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
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

            Label1.Text = clsFuncionesGral.ConvertMayus("Busqueda por: ");
            Label2.Text = clsFuncionesGral.ConvertMayus("Año: ");

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();
            dplAnio.SelectedValue = Year;
            //aqui me quede
            DropDownList1.Items.Insert(0,"S E L E C C I O N E");
            DropDownList1.Items.Insert(1, "FOLIO DE SOLICITUD");
            DropDownList1.Items.Insert(2, "NO. OFICIO");
            DropDownList1.Items.Insert(3, "AÑO");
            DropDownList1.Items.Insert(4, "ESTATUS");
            DropDownList1.Items.Insert(5, "RECURSO");
            DropDownList1.DataTextField = "Descripcion";
            DropDownList1.DataValueField = "Codigo";
            DropDownList1.DataBind();


        }
        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (Session["Crip_Rol"] == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Server.MapPath("./index.aspx"), true);
            }
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
        private void Carga_Listas(string lsTipoComprobacion, string lsANio)
        {
            clsFuncionesGral.Activa_Paneles(PanelNoAtendidas, false);
            clsFuncionesGral.Activa_Paneles(PanelOtros, false);
            GvMenuNoAtendidas.DataSource = null;
            GvMenuNoAtendidas.DataBind();
            GvMenuOtros.DataSource = null;
            GvMenuOtros.DataBind();

            List<Comision> ListaComprobaciones = new List<Comision>();
            if (lsTipoComprobacion == "1")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "9");
            }

            if (lsTipoComprobacion == "2")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "7");
                //GvMenuOtros.ShowHeader = true;
            }
            if (lsTipoComprobacion == "3")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "5");
                //GvMenuOtros.ShowHeader = true;
            }
            if (lsTipoComprobacion == "4")
            {
                ListaComprobaciones = MngNegocioComision.Regresa_ListComision(Session["Crip_Usuario"].ToString(), lsANio, "");
                //GvMenuOtros.ShowHeader = false;
            }

            List<Entidad> ListArchivosAmp = new List<Entidad>();

            foreach (Comision r in ListaComprobaciones)
            {
                if (r.Archivo_Ampliacion != "0")
                {
                    string[] psCadena;
                    psCadena = r.Archivo_Ampliacion.Split(new Char[] { '|' });

                    for (int i = 0; i < psCadena.Length; i++)
                    {
                        Entidad oEntidad = new Entidad();
                        oEntidad.Codigo = r.Archivo;
                        oEntidad.Descripcion = psCadena[i];
                        ListArchivosAmp.Add(oEntidad);
                    }
                }
            }
            List<Comision> ListaComprobacionesNoValidadas = new List<Comision>();
            List<Comision> ListaComprobacionesNueva = new List<Comision>();
            foreach (Comision r1 in ListaComprobaciones)
            {
                Comision oMenuComp = new Comision();

                bool existente = false;
                bool AmpliacionEx = false;
                int contador = 0;
                double totaldeviaticos = 0;
                string fechaFinal = "";

                foreach (Entidad r2 in ListArchivosAmp)
                {
                    if (r1.Archivo == r2.Codigo)
                    {
                        existente = true;
                        AmpliacionEx = true;
                        contador++;

                        Entidad objEntidad = new Entidad();
                        objEntidad = MngNegocioComision.Obten_DetalleArchivo(Session["Crip_Usuario"].ToString(), lsANio, r2.Descripcion);

                        totaldeviaticos = totaldeviaticos + Convert.ToDouble(objEntidad.Codigo);//cambiar pot metodo trae total viaticos r2.descripcion
                        fechaFinal = objEntidad.Descripcion;
                    }
                    if (r1.Archivo == r2.Descripcion)
                    {
                        AmpliacionEx = true;
                    }


                }
                oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "");
                oMenuComp.Folio = contador.ToString();
                oMenuComp.Lugar = r1.Lugar;

                if (r1.Estatus == "1")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "");
                    oMenuComp.Estatus = "../Resources/rojo.png";
                }
                if (r1.Estatus == "2" || r1.Estatus == "3")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "|" + r1.Estatus);
                    oMenuComp.Estatus = "../Resources/amarillo.png";
                }
                if (r1.Estatus == "4" || r1.Estatus == "5")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "|" + r1.Estatus);
                    oMenuComp.Estatus = "../Resources/verde.png";
                }
                if (r1.Estatus == "0")
                {
                    oMenuComp.Archivo = r1.Archivo.Replace(".pdf", "|0");
                    oMenuComp.Estatus = "../Resources/gris.png";
                }
                if (r1.Tipo_Pago_Viatico == "0")
                {
                    oMenuComp.Tipo_Pago_Viatico = "NA";
                    oMenuComp.Estatus = "../Resources/verde.png";
                }
                if (r1.Tipo_Pago_Viatico == "1")
                {
                    oMenuComp.Tipo_Pago_Viatico = "Dev";
                }
                if (r1.Tipo_Pago_Viatico == "2")
                {
                    oMenuComp.Tipo_Pago_Viatico = "Ant";
                }

                if (existente)
                {
                    oMenuComp.Periodo = r1.Fecha_Inicio + " AL " + fechaFinal;
                    oMenuComp.Total_Viaticos = (Convert.ToDouble(r1.Total_Viaticos) + totaldeviaticos).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                    if (r1.Estatus == "9")
                    {
                        ListaComprobacionesNoValidadas.Add(oMenuComp);
                    }
                    if ((r1.Estatus == "5") || (r1.Estatus == "7") || (r1.Estatus == "0"))
                    {
                        ListaComprobacionesNueva.Add(oMenuComp);
                    }

                }
                else if (AmpliacionEx == false)
                {
                    oMenuComp.Periodo = r1.Fecha_Inicio + " AL " + r1.Fecha_Final;
                    oMenuComp.Total_Viaticos = Convert.ToDouble(r1.Total_Viaticos).ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));

                    if (r1.Estatus == "9")
                    {
                        ListaComprobacionesNoValidadas.Add(oMenuComp);
                    }
                    if ((r1.Estatus == "5") || (r1.Estatus == "7") || (r1.Estatus == "0"))
                    {
                        ListaComprobacionesNueva.Add(oMenuComp);
                    }
                }


            }
            if (ListaComprobacionesNoValidadas.Count != 0)
            {
                clsFuncionesGral.Activa_Paneles(PanelNoAtendidas, true);
                GvMenuNoAtendidas.DataSource = ListaComprobacionesNoValidadas;
                GvMenuNoAtendidas.DataBind();
            }
            if (ListaComprobacionesNueva.Count != 0)
            {
                clsFuncionesGral.Activa_Paneles(PanelOtros, true);
                GvMenuOtros.DataSource = ListaComprobacionesNueva;
                GvMenuOtros.DataBind();
            }


        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsANio = dplAnio.SelectedValue.ToString();

            if ((lsANio == string.Empty) | (lsANio == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
                return;
            }

            string lsEstatusSolicitud = DropDownList1.SelectedValue.ToString();

            if (lsEstatusSolicitud == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione estatus de solicitud a buscar');", true);
                return;
            }

            Carga_Listas(lsEstatusSolicitud, lsANio);


        }

    }
}