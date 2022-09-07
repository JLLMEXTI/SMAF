using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
namespace InapescaWeb.Comprobaciones
{
    public partial class Comprobacion2018 : System.Web.UI.Page
    {

        string lsFolio;
        static clsDictionary Dictionary = new clsDictionary();
        string Ruta;
        string UbicacionFile;
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static string UltimoDia = "31-12-" + Year;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Evento Lnk Home 
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }
        //Evento Link Usuario datos personales
        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        //evento click de treeview menu
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

        //evento de boton para carga de informe de comision
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Sube_Informe_Comision();
        }
        //sube informe
        public void Sube_Informe_Comision()
        {
            bool fileOk = false;
            string[] lsCadena = new string[2];
            Comision DetalleComision = new Comision();
            if (fupdlComision.HasFile)
            {
                DetalleComision = (Comision)Session["DetallesComision"];

                Valida_Carpeta(DetalleComision.Ruta, true);

                String fileExtension = System.IO.Path.GetExtension(fupdlComision.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            if (fileOk)
            {
                try
                {
                    fupdlComision.PostedFile.SaveAs(Session["Crip_Ruta"] + "/" + fupdlComision.FileName);

                    if (Convert.ToDateTime(lsHoy) < Convert.ToDateTime("2021-05-05"))
                    {
                        MngNegocioComision.Inserta_Informe_Comision(DetalleComision.Oficio, DetalleComision.Ubicacion, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "1", fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, lsHoy, lsHoy, DetalleComision.Periodo);
                    }
                    else
                    {
                        MngNegocioComision.Inserta_Informe_Comision(DetalleComision.Oficio, DetalleComision.Ubicacion_Autoriza, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "1", fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, fupdlComision.FileName, lsHoy, lsHoy, DetalleComision.Periodo);
                    } 
                    Valida_Informe(DetalleComision);
                    //   Carga_Detalle();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Informe de comisión ha subido exitosamente.');", true);
                    DetalleComision = null;
                    return;

                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir su informe de comision favor de reinterntar.');", true);
                    DetalleComision = null;
                    return;
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                DetalleComision = null;
                return;
            }

        }
        //valida informe de actividades de comision
        public void Valida_Informe(Comision poComision)
        {
            InapescaWeb.Entidades.comision_informe cc;
            string[] lsCadena = new string[5];
            lsCadena = poComision.Archivo.ToString().Split(new Char[] { '-' });

            if (Convert.ToDateTime(lsHoy) < Convert.ToDateTime("2021-05-05"))
            {
                cc = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena[3], poComision.Ubicacion, lsCadena[4].Replace(".pdf", ""));
            }
            else
            {
                cc = MngNegocioComision.Obtiene_Informe(Session["Crip_Usuario"].ToString(), Session["Crip_Ubicacion"].ToString(), lsCadena[3], poComision.Ubicacion, lsCadena[4].Replace(".pdf", ""),poComision.Ubicacion_Autoriza);
            }
            if ((cc.FOLIO != null) & (clsFuncionesGral.FormatFecha(cc.FECHA_FINAL) != Dictionary.FECHA_NULA))
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Ya tiene cargado un informe de comision , se validara este para proceder a termino de su comprobacion");
                clsFuncionesGral.Activa_Paneles(pnlInforme, true);
                fupdlComision.Visible = false;
                ImageButton1.Visible = false;
            }
            else if (clsFuncionesGral.FormatFecha(cc.FECHA_FINAL) == Dictionary.FECHA_NULA)
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Ya tiene un informe parcial en el servidor,espere a que sea validado .");

                clsFuncionesGral.Activa_Paneles(pnlInforme, true);
                fupdlComision.Visible = false;
                ImageButton1.Visible = false;
            }
            else
            {
                lblActividades.Text = clsFuncionesGral.ConvertMayus("Carga de Informe de Comision:") + "(formato pdf)";

                clsFuncionesGral.Activa_Paneles(pnlInforme, true);
            }


        }
        //valida carpeta
        public void Valida_Carpeta(string psRuta, bool pbInforme = false, bool pbComprobacionFiscales = false, bool pbOtros = false, bool pbComprobacionNoFiscales = false)
        {
            Ruta = "";
            UbicacionFile = "";
            string raiz = HttpContext.Current.Server.MapPath("..");
            if (pbInforme)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.INFORME)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.INFORME);
                //Ruta = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.INFORME;
                //UbicacionFile = psRuta + "/" + Dictionary.INFORME;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.INFORME;
            }

            if (pbComprobacionFiscales)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.FISCALES)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.FISCALES);
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.FISCALES;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.FISCALES;
            }

            if (pbOtros)
            {
                if (!Directory.Exists(raiz + "\\" + psRuta + "/" + Dictionary.OTROS)) Directory.CreateDirectory(raiz + "\\" + psRuta + "/" + Dictionary.OTROS);
                Session["Crip_Ruta"] = raiz + "\\" + psRuta + "/" + Dictionary.OTROS;
                Session["Crip_UbicacionFile"] = psRuta + "/" + Dictionary.OTROS;
            }

        }
      
    }
}