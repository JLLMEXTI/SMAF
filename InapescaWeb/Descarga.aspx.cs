using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;
using InapescaWeb.Entidades;

namespace InapescaWeb
{
    public partial class Descarga : System.Web.UI.Page
    {
        string lsFolio;
        static clsDictionary Dictionary = new clsDictionary();
        string Ruta;
        string UbicacionFile;
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] sCadena = new string[2];
            string sRuta = "";
            string sArchivo = "";
            string sPath = "";
            string raiz = HttpContext.Current.Server.MapPath("~");

            string psMinistracion = Request.QueryString["MINISTRACION"];
            string psReferencia = Request.QueryString["REFEREN"];

            if (psMinistracion != null)
            {
                sCadena = psMinistracion.Split(new Char[] { '|' });
                sRuta = raiz + "\\" + sCadena[1].ToString();
                sArchivo = "Ministracion - " + sCadena[0].ToString();
                sPath = sRuta + "\\" + sArchivo;
            }
            if (psReferencia != null)
            {
                sRuta = raiz + "\\PDF\\" + "REFERENCIAS\\";
                sArchivo = psReferencia.ToString();
                sPath = sRuta + sArchivo;
            }
            System.IO.FileInfo toDownload = new System.IO.FileInfo(sPath);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                       "attachment; filename=" + toDownload.Name);
            HttpContext.Current.Response.AddHeader("Content-Length",
                       toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.WriteFile(sPath);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }

    }
}