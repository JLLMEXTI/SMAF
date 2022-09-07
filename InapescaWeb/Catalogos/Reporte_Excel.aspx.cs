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
    public partial class Reporte_Excel : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static string Ruta;
        static string lsUbicacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitle.Text = clsFuncionesGral.ConvertMayus("carga de archivo excel");
                Carga_Valores();

            }
        }

        public void Carga_Valores()
        {

            Label1.Text = "Nombre de Hoja excel  a cargar";
            // lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";

        }
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

    
        public static void Valida_Carpeta()
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\CONTABLE\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);

            Ruta = raiz + Year;
            lsUbicacion = "CONTABLE/" + Year;

        }

        //metodo de lectura y carga de datos de excel a pantalla y esportacion
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsHoja = TextBox1.Text;

            if ((lsHoja == null) | (lsHoja == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar un nombre de hoja como aparece en su archivo de excel.');", true);
                return;
            }
         /*   else if (!clsFuncionesGral.IsNumeric(lsHoja))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Numero de hoja no es numerico');", true);
                return;
            }
            */
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
                    String[] allowedExtensions = { ".xlsx", ".xls", ".XLSX", ".XLS" };

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
                            //carga de archivo
                            FileUpload1.PostedFile.SaveAs(Ruta + "/" + FileUpload1.FileName);
                            //obtiene tabla de dataset de hoja de excel
                            DataTable ds = MngDatosPef.Lee_Excel_Pef(Ruta + "/" + FileUpload1.FileName, lsHoja, fileExtension, FileUpload1.FileName).Tables[0];
                           //insert de datos a tabla
                            bool insert = MngNegocioExcel.Inserta_Excel(ds);

                            //asignacion de datos de excel a Grid
                            GridView1.DataSource = ds;
                            GridView1.DataBind();
                        
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir PEF favor de reinterntar."+ ex.Message + "');", true);
                            return;
                        }
                    }
                }
            }
        }
    }
}