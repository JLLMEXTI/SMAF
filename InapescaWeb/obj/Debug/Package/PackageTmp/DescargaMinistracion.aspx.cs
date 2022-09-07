using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;

namespace InapescaWeb
{
    public partial class DescargaMinistracion : System.Web.UI.Page
    {
        static string Year = DateTime.Today.Year.ToString();
        static clsDictionary Dictionary = new clsDictionary();

        protected void Page_Load(object sender, EventArgs e)
        {

            string[] lsCadena = new string[2];
            string lsMinistracion = Request.QueryString["Ministracion"];
            lsCadena = lsMinistracion.Split(new Char[] { '|' });
            //lsCadena[0]= ARCHIVO
            //lsMinistracion[0]= UBICACION




            string raiz = HttpContext.Current.Server.MapPath(".") + "/PDF/";

            string Ruta = raiz + Year + "/" + MngNegocioDependencia.Centro_Descrip(lsCadena[1].ToString(), true) + "/" + Dictionary.COMISIONES + "/" + lsCadena[0].ToString().Replace(".pdf", "");
            string archivo = "Ministracion - " + lsCadena[0].ToString();
            string path = Ruta + "\\" + archivo;

            //System.IO.FileInfo toDownload = new System.IO.FileInfo(raiz + "/" + psReferencia);
            //  new System.IO.FileInfo(HttpContext.Current.Server.MapPath(patch));

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                       "attachment; filename=" + archivo);
            HttpContext.Current.Response.AddHeader("Content-Length",
                       archivo.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(path);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }
    }
}