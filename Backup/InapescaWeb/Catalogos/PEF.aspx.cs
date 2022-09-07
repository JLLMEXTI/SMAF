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
using System.Drawing;
using System.IO;

namespace InapescaWeb.Catalogos
{
    public partial class PEF : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        //Datos Session
        static string lsUsuario;
        static string lsNivel;
        static string lsPlaza;
        static string lsPuesto;
        static string lsSecretaria;
        static string lsOrganismo;
        static string lsUbicacion;
        static string lsArea;
        static string lsNombre;
        static string lsApPat;
        static string lsApMat;
        static string lsRFC;
        static string lsCargo;
        static string lsEmail;
        static string lsRol;
        static string lsAbreviatura;
        static string Ruta;

        static DataTable ds;

        static Pef pef;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lsRol = Session["Crip_Rol"].ToString();
                lsPuesto = Session["Crip_Puesto"].ToString();

                if ((lsRol == Dictionary.DIRECTOR_ADMINISTRACION) | (lsRol == Dictionary.DIRECTOR_GRAL) |( (lsRol == Dictionary.SUBDIRECTOR_ADJUNTO ) &  (lsPuesto == Dictionary.PUESTO_FINANCIERO )))
                {
                    clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, lsRol);
                    Carga_Session();
                    Carga_Valores();

                    int objPef = MngNegocioPef.Obtiene_pef();

                    if (objPef >= 1)
                    {
                        clsFuncionesGral.Activa_Paneles(Panel1, false);
                        lblTitle.Text = clsFuncionesGral.ConvertMayus("Ya existe cargado un pef para este año , si desea agregar mas partidas al actual ingresar al modulo de adecuaciones Pef");
                    }
                    else
                    {
                        clsFuncionesGral.Activa_Paneles(Panel1, true);
                        lblTitle.Text = clsFuncionesGral.ConvertMayus("carga de archivo excel de pef anual") + "  " + Year;

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar un nombre de hoja como aparece en su archivo de excel.');", true);
                    Response.Redirect("../Home/Home.aspx", true);
                    
                }
            }

        }

        public void Carga_Valores()
        {

            Label1.Text = "Nombre de Hoja excel  a cargar";
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";

        }

        /// <summary>
        /// Metodo que carga datos de session de usuario
        /// </summary>
        public void Carga_Session()
        {
            lsUsuario = Session["Crip_Usuario"].ToString();
            lsNivel = Session["Crip_Nivel"].ToString();
            lsPlaza = Session["Crip_Plaza"].ToString();
            lsPuesto = Session["Crip_Puesto"].ToString();
            lsSecretaria = Session["Crip_Secretaria"].ToString();
            lsOrganismo = Session["Crip_Organismo"].ToString();
            lsUbicacion = Session["Crip_Ubicacion"].ToString();
            lsArea = Session["Crip_Area"].ToString();
            lsNombre = Session["Crip_Nombre"].ToString();
            lsApPat = Session["Crip_ApPat"].ToString();
            lsApMat = Session["Crip_ApMat"].ToString();
            lsRFC = Session["Crip_RFC"].ToString();
            lsCargo = Session["Crip_Cargo"].ToString();
            lsEmail = Session["Crip_Email"].ToString();
            lsRol = Session["Crip_Rol"].ToString();
            lsAbreviatura = Session["Crip_Abreviatura"].ToString();

        }

        /// <summary>
        /// Evento Click de linkbutton home , redirecciona al inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        /// <summary>
        /// Evento click de treeview menu
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


        public static void Valida_Carpeta()
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\CONTABLE\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);

            Ruta = raiz + Year;
            lsUbicacion = "CONTABLE/" + Year;
        }

        /// <summary>
        /// evento click de image button subir excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsHoja = TextBox1.Text;

            if ((lsHoja == null) | (lsHoja == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar un nombre de hoja como aparece en su archivo de excel.');", true);
                return;
            }

            bool fileOk = false;
            if ((FileUpload1.FileName == null) | (FileUpload1.FileName == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe cargar un archivo .');", true);
                return;
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    Valida_Carpeta();
                    String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                    String[] allowedExtensions = { ".xlsx", ".xls" };

                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOk = true;
                        }
                    }

                    if (fileOk)
                    {
                        try
                        {
                            FileUpload1.PostedFile.SaveAs(Ruta + "/" + FileUpload1.FileName);
                            ds = MngDatosPef.Lee_Excel_Pef(Ruta + "/" + FileUpload1.FileName, lsHoja, fileExtension, FileUpload1.FileName).Tables[0];

                            Carga_PEF(ds, FileUpload1.FileName);
                            
                            TextBox1.Text = Dictionary.CADENA_NULA;
                            ds.Rows.Clear();
                            pef = null;
                            clsFuncionesGral.Activa_Paneles(Panel1, false);
                            lblTitle.Text = clsFuncionesGral.ConvertMayus("Ya existe cargado un pef para este año , si desea agregar mas partidas al actual ingresar al modulo de adecuaciones Pef");

                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('PEF ha subido exitosamente.');", true);
                            return;

                        }
                        catch (Exception ex)
                        {

                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir PEF favor de reinterntar.');", true);
                            return;
                        }
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                        return;
                    }

                }
            }
        }

        /// <summary>
        /// Metodo que que lee tabla e inserta en la bd datos del excel pef
        /// </summary>
        public static void Carga_PEF(DataTable pdtTable, string psNombre)
        {

            int conta = 0;
            foreach (DataRow row in pdtTable.Rows)
            {
                pef = new Pef();
                conta = 0;


                foreach (var item in row.ItemArray)
                {
                    switch (conta)
                    {
                        case 0:
                            pef.UR = item.ToString();
                            break;
                        case 1:
                            pef.UE = item.ToString();
                            break;
                        case 2:
                            pef.EDO = item.ToString();
                            break;
                        case 3:
                            pef.FI = item.ToString();
                            break;
                        case 4:
                            pef.FU = item.ToString();
                            break;
                        case 5:
                            pef.SF = item.ToString();
                            break;
                        case 6:
                            pef.RG = item.ToString();
                            break;
                        case 7:
                            pef.AI = item.ToString();
                            break;
                        case 8:
                            pef.PP = item.ToString();
                            break;
                        case 9:
                            pef.CP = item.ToString();
                            break;
                        case 10:

                            pef.PARTIDA = item.ToString();
                            break;
                        case 11:
                            pef.TG = item.ToString();
                            break;
                        case 12:
                            pef.FF = item.ToString();
                            break;
                        case 13:
                            pef.PPI = item.ToString();
                            break;
                        case 14:
                            pef.PEF = item.ToString();
                            break;
                        case 15:
                            pef.ENERO = item.ToString();
                            break;
                        case 16:
                            pef.FEBRERO = item.ToString();
                            break;
                        case 17:
                            pef.MARZO = item.ToString();
                            break;
                        case 18:
                            pef.ABRIL = item.ToString();
                            break;
                        case 19:
                            pef.MAYO = item.ToString();
                            break;
                        case 20:
                            pef.JUNIO = item.ToString();
                            break;
                        case 21:
                            pef.JULIO = item.ToString();
                            break;
                        case 22:
                            pef.AGOSTO = item.ToString();
                            break;
                        case 23:
                            pef.SEPTIEMBRE = item.ToString();
                            break;
                        case 24:
                            pef.OCTUBRE = item.ToString();
                            break;
                        case 25:
                            pef.NOVIEMBRE = item.ToString();
                            break;
                        case 26:
                            pef.DICIEMBRE = item.ToString();
                            break;

                    }
                    conta++;
                }
                if ((pef.PARTIDA == null) | (pef.PARTIDA == Dictionary.CADENA_NULA))
                {
                    MngNegocioPef.Inserta_Pef_Detalle(pef, "1", lsUsuario, psNombre);

                }
                else
                {
                    MngNegocioPef.Inserta_Pef(pef, "1");
                    pef = null;
                }
            }

        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }
    }
}