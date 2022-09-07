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
using System.IO;
using System.Web;

namespace InapescaWeb.Sustantivo
{
    public partial class ActualizaModuloConsulta : System.Web.UI.Page
    {
        static CultureInfo culture = new CultureInfo("es-MX");
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();
        string lsFolio;
        string Ruta;
        string UbicacionFile;

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
                        //Carga_Session();
                        Carga_Valores();

                        clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, Session["Crip_Rol"].ToString());
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
            clsFuncionesGral.Activa_Paneles(pnlDetalle, false);

            lblTitle.Text = "Actualización del Modulo de Consulta de Opiniones y Dictamenes Técnicos";
            Label53.Text = clsFuncionesGral.ConvertMayus("Busqueda - Folio de Solicitud");
            Label1.Text = clsFuncionesGral.ConvertMayus("Capturar Folio de Solicitud :");

            Label9.Text = clsFuncionesGral.ConvertMayus("Folio de Solicitud :  ");
            Label10.Text = clsFuncionesGral.ConvertMayus("Fecha de Solicitud:  ");
            Label19.Text = clsFuncionesGral.ConvertMayus("Oficio de Solicitud  :   ");
            Label11.Text = clsFuncionesGral.ConvertMayus("Solicitante : ");
            Label12.Text = clsFuncionesGral.ConvertMayus("Correo Electrónico : ");
            Label13.Text = clsFuncionesGral.ConvertMayus("Recurso : ");
            Label14.Text = clsFuncionesGral.ConvertMayus("Asunto de la Solicitud: ");
            lblObsrSol.Text = clsFuncionesGral.ConvertMayus("Tipo de Documento  :");

            Label22.Text = clsFuncionesGral.ConvertMayus("Actualizar Estatus  :  ");
            Label2.Text = clsFuncionesGral.ConvertMayus("Fecha de Actualización de Estatus : ");
            Label42.Text = clsFuncionesGral.ConvertMayus("Observaciones :");
            Label5.Text = clsFuncionesGral.ConvertMayus("No. de Oficio de Atención :");
            //Label4.Text = clsFuncionesGral.ConvertMayus("Subir Documento :");
            Session["Folio"] = TextFolio.Text;
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
            SolicitudMC detalleSolicitud1 = new SolicitudMC();
            string lsFolioAut = TextFolio.Text;

            if ((lsFolioAut == null) | (lsFolioAut == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe ingresar el Folio de Solicitud para poder continuar.');", true);
                return;
            }
            else
            {
                
               detalleSolicitud1 = MngNegocioSolicitud.Detalle_SolicitudMC(lsFolioAut, "");
                if(detalleSolicitud1.OfSolicitud == null )
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El Folio: "+lsFolioAut+" no existe');", true);
                    return;
                    
                }else
                {   
                    clsFuncionesGral.Activa_Paneles(pnlDetalle, true);
                    Session["Crip_Folio"] = lsFolioAut;

                    Carga_Panel(lsFolioAut, Dictionary.AUTORIZA, MngNegocioPermisos.ObtienePermisos(Session["Crip_Usuario"].ToString(), Dictionary.PERMISO_ENLACEDGAMC));
                
                }
            }
        
        }
        public void Carga_Panel(string psFolio, string psOpcion, string psPermisosAdmin)
        {
            SolicitudMC detalleSolicitud = new SolicitudMC();
            detalleSolicitud = MngNegocioSolicitud.Detalle_SolicitudMC(psFolio, "");
            btnAccion.Text = "ACTUALIZAR";
            Label8.Text = clsFuncionesGral.ConvertMayus("Detalle de Solicitud con Folio número :  ") + detalleSolicitud.Folio;
            lblNumSolicita.Text = detalleSolicitud.Folio;
            lblFechaSolicitud.Text = clsFuncionesGral.FormatFecha(detalleSolicitud.FechaSol);
            lblOfSolicitud.Text = detalleSolicitud.OfSolicitud;
            lblSolicitante.Text = detalleSolicitud.Solicitante;
            lblCorreo.Text = detalleSolicitud.Correo;
            lblRecurso.Text = detalleSolicitud.Recurso;
            lblAsunto.Text = detalleSolicitud.Asunto;
            lblObserSoli.Text = detalleSolicitud.Observaciones;
            

            dplEstatus.Visible = true;
            dplEstatus.DataSource = MngNegocioSolicitud.ObtieneListaEstatus(true);
            dplEstatus.DataTextField = Dictionary.DESCRIPCION;
            dplEstatus.DataValueField = Dictionary.CODIGO;
            dplEstatus.DataBind();
            //fupdlComision.Visible = true;
            //ImageButton2.Visible = true;

        }
        ////evento de boton para carga de Documento
        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    Sube_Documento();
        //}
        //sube Documento
        //public void Sube_Documento()
        //{
        //    bool fileOk = false;
        //    string[] lsCadena = new string[2];
        //    SolicitudMC DetalleSolicitud = new SolicitudMC();
        //    DetalleSolicitud = MngNegocioSolicitud.Detalle_SolicitudMC();
        //    string lsUbicacion = Session["Crip_Ubicacion"].ToString();
        //    string DGA = MngNegocioSolicitud.ObtieneDGA(lsUbicacion);
        //    if (fupdlComision.HasFile)
        //    {
        //        DetalleSolicitud = (SolicitudMC)Session["DetallesSolicitud"];
        //        Ruta = "Sustantivo/PDF/" + year + "/" + DGA + "/Solicitudes";
        //        Valida_Carpeta(Ruta, true);

        //        String fileExtension = System.IO.Path.GetExtension(fupdlComision.FileName).ToLower();
        //        String[] allowedExtensions = { ".pdf" };

        //        for (int i = 0; i < allowedExtensions.Length; i++)
        //        {
        //            if (fileExtension == allowedExtensions[i])
        //            {
        //                fileOk = true;
        //            }
        //        }
        //    }

        //    if (fileOk)
        //    {
        //        try
        //        {
        //            fupdlComision.PostedFile.SaveAs(Ruta + "/" + fupdlComision.FileName);

        //            //MngNegocioSolicitud.Inserta_Documento(DetalleSolicitud.Folio,fupdlComision.FileName, lsHoy);

        //            Valida_Informe(DetalleSolicitud);
        //            //   Carga_Detalle();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Informe de comisión ha subido exitosamente.');", true);
        //            DetalleSolicitud = null;
        //            return;

        //        }
        //        catch (Exception ex)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
        //            DetalleSolicitud = null;
        //            return;
        //        }
        //    }
        //    else
        //    {

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
        //        DetalleSolicitud = null;
        //        return;
        //    }

        //}
        //valida informe de actividades de comision
        //public void Valida_Informe(SolicitudMC poSolicitud)
        //{
        //    string cc ="";
        //    cc = MngNegocioSolicitud.ObtieneDocumento(Session["Folio"].ToString());

        //    if ((cc!="") && (cc!=Dictionary.CADENA_NULA))
        //    {
        //        lblDocumento.Text = clsFuncionesGral.ConvertMayus("Ya tiene cargado un informe de comision , se validara este para proceder a termino de su comprobacion");
        //        clsFuncionesGral.Activa_Paneles(pnlDocumento, true);
        //        fupdlComision.Visible = false;
        //        //ImageButton2.Visible = false;
        //    }
        //    else
        //    {
        //        lblDocumento.Text = clsFuncionesGral.ConvertMayus("Carga de Informe de Comision:") + "(formato pdf)";

        //        clsFuncionesGral.Activa_Paneles(pnlDocumento, true);
        //    }


        //}
        ////valida carpeta
        //public void Valida_Carpeta(string psRuta, bool pbInforme = false)
        //{
        //    Ruta = "";
        //    UbicacionFile = "";
        //    string raiz = HttpContext.Current.Server.MapPath("..");
        //    if (pbInforme)
        //    {
        //        if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Session["Folio"].ToString())) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Session["Folio"].ToString());
        //        //Ruta = raiz + "\\" + psRuta + "/" + Session["Folio"].ToString();
        //        Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Session["Folio"].ToString();
        //        //UbicacionFile = psRuta + "/" + Dictionary.INFORME;
        //        Session["Crip_UbicacionFile"] = psRuta + "/" + Session["Folio"].ToString();
        //    }

        //}
        protected void btnAccion_Click(object sender, EventArgs e) 
        {
            string lsFolio= TextFolio.Text;
            string lsEstatus= dplEstatus.SelectedValue.ToString();
            string lsFechaAct= txtInicioAut.Text;
            string lsObservaciones = txtObservaciones.Text;
            string lsOfResp = TextOfAtencion.Text;
            Boolean ResultadoReg = false;

            if(lsEstatus == "00")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar el nuevo estatus para poder continuar.');", true);
                return;

            }else if(lsFechaAct=="" || lsFechaAct==Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar la Fecha en que se Actualizo el Estatus para poder continuar.');", true);
                return;
            }
            else if (lsObservaciones == "" || lsObservaciones == Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe insertar el Tipo de Documento para poder continuar.');", true);
                return;
            }
            else if (lsOfResp == "" || lsOfResp == Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe insertar el No de Oficio de Atención para poder continuar.');", true);
                return;
            }
            else
            {

                SolicitudMC detalleSolicitud = new SolicitudMC();
                detalleSolicitud = MngNegocioSolicitud.Detalle_SolicitudMC(lsFolio, "");
                
                ResultadoReg = MngNegocioSolicitud.InsertNuevoRegSolicitud(detalleSolicitud, lsEstatus, lsFechaAct, lsObservaciones, Session["Crip_Usuario"].ToString(), lsOfResp);

                if (ResultadoReg == true)
                {
                    clsFuncionesGral.Activa_Paneles(pnlDetalle, false);

                    dplEstatus.DataSource = MngNegocioSolicitud.ObtieneListaEstatus(true);
                    dplEstatus.DataTextField = Dictionary.DESCRIPCION;
                    dplEstatus.DataValueField = Dictionary.CODIGO;
                    dplEstatus.DataBind();
                    txtInicioAut.Text = "";
                    txtObservaciones.Text = "";
                    TextFolio.Text = "";
                    TextOfAtencion.Text = "";

                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El registro se actualizo correctamente');", true);
                    return;
                }
                else 
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un Error en el registro.');", true);
                    return;
                }
            }





        
             
        //MngNegocioSolicitud.Actualiza_Solicitud(detalleSolicitud,);
            //Sube_Documento();
        
        }
    }
}