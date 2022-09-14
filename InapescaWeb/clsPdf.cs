using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
//using System.Drawing;
using System.Diagnostics;
using System.Web.SessionState;
using InapescaWeb.DAL;
using System.Data;
using System.Globalization;

namespace InapescaWeb
{
    public class clsPdf
    {
        static string lsExtencion = ".pdf";
        static Document document;
        static clsDictionary Dictionary = new clsDictionary();
        static clsMail email = new clsMail();
        static string ClaseT;
        static string TipoT;

        static string Year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string CLave_Oficio;
        static string separador;
        static Ubicacion oUbicacionTrans = new Ubicacion();
        static string lsVehiculo;
        static string lsUbicacion;
        static string Ruta;
        static int tPequeño = 5;
        static int tmediano = 8;
        static int tMedia = 7;
        static int tGrande = 12;
        static string lsRol_Admin;
        static int vale;
        static string comprobado;
        static double TotalComprobado;
        static double TotalOtorgado;
        static string[] ListaProductos;
        static string[] ListaNewProdductos;

        public static Document Crea_Pdf_Reporte_Viaticos(string psOpcion)
        {
            Document document;

            if (psOpcion == "1")
            {
                document = new Document(PageSize.A4);//, 23, 23, 36, 23);
            }
            else
            {
                document = new Document(PageSize.LEGAL.Rotate());//, 23, 23, 36, 23);
            }

            //Agregamos Propiedades al documento
            document.AddTitle(clsFuncionesGral.ConvertMayus("PDFReporte Viatios"));
            document.AddCreator(clsFuncionesGral.ConvertMayus("Ing. Juan Antonio López López"));

            return document;
        }

        //Crea documento PDF y propiedades
        public static Document Crea_Pdf()
        {
            Document document = new Document(PageSize.A4);//, 23, 23, 36, 23);
            //Agregamos Propiedades al documento
            document.AddTitle(clsFuncionesGral.ConvertMayus("PDF COMISION SMAF"));
            document.AddCreator(clsFuncionesGral.ConvertMayus("Ing. Juan Antonio López López"));
            return document;
        }

        //Metodos que crear paragraph y asigan fuente , tamaño , y Color al mismo 
        public static Paragraph Agrega_Parrafo(string psCadena, string psFuente, string psColor, string psAligment, int piTamaño)
        {
            Paragraph paragraph = new Paragraph();
            switch (psAligment)
            {
                case "ALIGN_CENTER":
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    break;
                case "ALIGN_JUSTIFIED":
                    paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                    break;
                case "ALIGN_LEFT":
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    break;
                case "ALIGN_RIGHT":
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    break;
            }

            Configura_Fuente(paragraph, psFuente, psColor, piTamaño);
            paragraph.Add(psCadena);

            return paragraph;
        }

        public static void Configura_Fuente(Paragraph pParagraph, string psTipoFuente, string psColor, int piTamaño)
        {
            int lsTipoFuente = iTextSharp.text.Font.NORMAL;
            switch (psTipoFuente)
            {
                case "NORMAL":
                    lsTipoFuente = iTextSharp.text.Font.NORMAL;
                    break;

                case "ITALIC":
                    lsTipoFuente = iTextSharp.text.Font.ITALIC;
                    break;

                case "UNDERLINE":
                    lsTipoFuente = iTextSharp.text.Font.UNDERLINE;
                    break;

                case "BOLD":
                    lsTipoFuente = iTextSharp.text.Font.BOLD;
                    break;

                case "BOLDITALIC":
                    lsTipoFuente = iTextSharp.text.Font.BOLDITALIC;
                    break;

            }

            pParagraph.Font = FontFactory.GetFont("Adobe Caslon Pro", piTamaño, lsTipoFuente);

            switch (psColor)
            {
                case "LIGHT_GRAY":
                    pParagraph.Font.Color = BaseColor.LIGHT_GRAY;
                    break;

                case "BLACK":
                    pParagraph.Font.Color = BaseColor.BLACK;
                    break;

                case "GRAY":
                    pParagraph.Font.Color = BaseColor.GRAY;
                    break;
            }

        }

        //Metodo que genera tabla con columnas porcentaje y alineacion correspondiente 
        public static PdfPTable Genera_Tabla_TamañoPersonalizado(int psNum_Colums, int piPercente, string psAling, float[] psMedidaCelda)//float [] psMedidaCelda = { 0.0f ,0.0f}
        {
            PdfPTable tabla = new PdfPTable(psNum_Colums);
            tabla.WidthPercentage = piPercente;
            
            tabla.SetWidths(psMedidaCelda);

            switch (psAling)
            {
                case "LEFT":
                    tabla.HorizontalAlignment = Element.ALIGN_LEFT;
                    break;
                case "RIGHT":
                    tabla.HorizontalAlignment = Element.ALIGN_RIGHT;
                    break;
                case "CENTER":
                    tabla.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                    tabla.HorizontalAlignment = Element.ALIGN_CENTER;
                    break;

                case "JUSTIFIED":
                    tabla.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    break;
            }
            return tabla;
        }

        public static PdfPTable Genera_Tabla_SinEncabezado(int psNumColums, int piPercente, string psAling)
        {
            PdfPTable tabla = new PdfPTable(psNumColums);
            tabla.WidthPercentage = piPercente;

            switch (psAling)
            {
                case "LEFT":
                    tabla.HorizontalAlignment = Element.ALIGN_LEFT;
                    break;
                case "RIGHT":
                    tabla.HorizontalAlignment = Element.ALIGN_RIGHT;
                    break;
                case "CENTER":
                    tabla.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                    tabla.HorizontalAlignment = Element.ALIGN_CENTER;
                    break;

                case "JUSTIFIED":
                    tabla.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    break;
            }
            return tabla;
        }

        //Metodos que crean celda y phrase asiganan fuente , tamaño y color 
        public static void AgregaDatosTabla(PdfPTable ptTabla, string psContenido, int piBorde, string psTipoFuente, string psColor, int psTamaño)
        {
            PdfPCell CeldaPdf = new PdfPCell(Agrega_Parrafo(psContenido, psTipoFuente, psColor, psTamaño));
            CeldaPdf.Border = piBorde;
            // CeldaPdf.BorderColor = BaseColor.BLACK;

            ptTabla.AddCell(CeldaPdf);
        }

        public static void AgregaDatosTabla_Colspan(PdfPTable ptTabla, string psContenido, int piBorde, string psTipoFuente, string psColor, int psTamaño, int piNumColums)
        {
            PdfPCell CeldaPdf = new PdfPCell(Agrega_Parrafo(psContenido, psTipoFuente, psColor, psTamaño));
            CeldaPdf.Border = piBorde;
            // CeldaPdf.BorderColor = BaseColor.BLACK;
            CeldaPdf.Colspan = piNumColums;
            ptTabla.AddCell(CeldaPdf);
        }

        public static Phrase Agrega_Parrafo(string psCadena, string psTipoFuente, string psColor, int psTamaño)
        {
            Phrase paragraph = new Phrase();
            Configura_Fuente_Celda(paragraph, psTipoFuente, psColor, psTamaño);
            paragraph.Add(psCadena);
            return paragraph;
        }

        public static Phrase Agrega_Parrafo_Center(string psCadena, string psTipoFuente, string psColor, int psTamaño)
        {
            Phrase paragraph = new Phrase();
            Configura_Fuente_Celda(paragraph, psTipoFuente, psColor, psTamaño);
            paragraph.Add(psCadena);
            return paragraph;
        }

        public static void AgregaDatosTabla_Center(PdfPTable ptTabla, string psContenido, int piBorde, string psTipoFuente, string psColor, int psTamaño)
        {
            PdfPCell CeldaPdf = new PdfPCell(Agrega_Parrafo(psContenido, psTipoFuente, psColor, psTamaño));
            CeldaPdf.Border = piBorde;
            // CeldaPdf.BorderColor = BaseColor.BLACK;
            CeldaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            ptTabla.AddCell(CeldaPdf);
        }

        public static void Configura_Fuente_Celda(Phrase pParagraph, string psTipoFuente, string psColor, int piTamaño)
        {
            int lsTipoFuente = iTextSharp.text.Font.NORMAL;
            switch (psTipoFuente)
            {
                case "NORMAL":
                    lsTipoFuente = iTextSharp.text.Font.NORMAL;
                    break;

                case "ITALIC":
                    lsTipoFuente = iTextSharp.text.Font.ITALIC;
                    break;

                case "UNDERLINE":
                    lsTipoFuente = iTextSharp.text.Font.UNDERLINE;
                    break;

                case "BOLD":
                    lsTipoFuente = iTextSharp.text.Font.BOLD;
                    break;

                case "BOLDITALIC":
                    lsTipoFuente = iTextSharp.text.Font.BOLDITALIC;
                    break;


            }

            pParagraph.Font = FontFactory.GetFont("Adobe Caslon Pro", piTamaño, lsTipoFuente);

            switch (psColor)
            {
                case "LIGHT_GRAY":
                    pParagraph.Font.Color = BaseColor.LIGHT_GRAY;
                    break;

                case "BLACK":
                    pParagraph.Font.Color = BaseColor.BLACK;
                    break;

                case "GRAY":
                    pParagraph.Font.Color = BaseColor.GRAY;
                    break;
            }
        }

        //Agrega Datos celda color
        public static void AgregaDatosCeldaColor(PdfPTable ptTabla, string psContenido, int piBorde, string psTipoFuente, string psColor, int tamaño, bool pBandera)
        {
            PdfPCell CeldaPdf = new PdfPCell(Agrega_Parrafo(psContenido, psTipoFuente, psColor, tmediano));
            CeldaPdf.Border = piBorde;
            CeldaPdf.BorderColor = BaseColor.BLACK;
            if (pBandera) CeldaPdf.BackgroundColor = BaseColor.LIGHT_GRAY;
            ptTabla.AddCell(CeldaPdf);
        }

        //Agregar tabla en celda
        public static void Agrega_TablainCelda_Drawing(PdfPTable ptTablaOriginal, int piBorde, PdfPTable subtabla)//int psNum_Colums, int piPercente ,string psContenido,string psTipo)
        {
            PdfPCell CeldaPdf = new PdfPCell(subtabla);
            CeldaPdf.Border = piBorde;
            CeldaPdf.BorderColor = BaseColor.BLACK;
            ptTablaOriginal.AddCell(CeldaPdf);
        }

        public static void Agrega_Imagen_CeldaCentrada(PdfPTable ptTabla, string psUsuario, int piLargo, int piAncho, int piBorde)
        {
            string imageFilePath = "";
            imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\" + psUsuario + ".png";
            iTextSharp.text.Image objImageFondo = iTextSharp.text.Image.GetInstance(imageFilePath);
            objImageFondo.ScaleToFit(piLargo, piAncho);
            objImageFondo.Alignment = iTextSharp.text.Image.UNDERLYING;

            PdfPCell CeldaPdf = new PdfPCell(objImageFondo);
            CeldaPdf.Border = piBorde;

            CeldaPdf.HorizontalAlignment = 1;

            ptTabla.AddCell(CeldaPdf);
        }

        public static void Agrega_Imagen_Celda(PdfPTable ptTabla, string psUsuario, int piLargo, int piAncho, int piBorde)
        {
            string imageFilePath = "";
            imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\" + psUsuario + ".png";
            iTextSharp.text.Image objImageFondo = iTextSharp.text.Image.GetInstance(imageFilePath);
            objImageFondo.ScaleToFit(piLargo, piAncho);
            objImageFondo.Alignment = iTextSharp.text.Image.UNDERLYING;

            PdfPCell CeldaPdf = new PdfPCell(objImageFondo);
            CeldaPdf.Border = piBorde;
            // CeldaPdf.BorderColor = BaseColor.BLACK;
            ptTabla.AddCell(CeldaPdf);
        }

        public static void Agrega_Imagen_CeldaSeleccionada(PdfPTable ptTabla, string psUsuario, int piLargo, int piAncho, int piBorde)
        {
            string imageFilePath = "";
            imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\" + psUsuario + ".png";
            iTextSharp.text.Image objImageFondo = iTextSharp.text.Image.GetInstance(imageFilePath);
            objImageFondo.ScaleToFit(piLargo, piAncho);
            objImageFondo.Alignment = iTextSharp.text.Image.RECTANGLE;

            PdfPCell CeldaPdf = new PdfPCell(objImageFondo);
            CeldaPdf.Border = piBorde;
            // CeldaPdf.BorderColor = BaseColor.BLACK;
            ptTabla.AddCell(CeldaPdf);
        }

        public static void Imagen_Fondo(Document pdDocument, string psType, int piLargo, int piAncho, string psTratamiento)
        {
            string imageFilePath = "";

            switch (psType)
            {
                case "SAGARPA":
                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\SAGARPA.PNG";
                    break;
                case "mexico":
                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\zapata.PNG";

                    break;
                case "aguila":
                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\mexico.PNG";

                    break;
                case "agricultura":
                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\agricultura.PNG";

                    break;

                case "cenefa_baja_zapata":
                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\cenefa_baja_zapata.PNG";

                    break;

                case "cenefa_baja_leona":

                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\cenefa_baja_leona.PNG";

                    break;
                case "cenefa_baja_independencia":

                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\cenefa_baja_independencia.PNG";

                    break;
                case "cenefa_baja_2022":

                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\cenefa_baja_2022.PNG";

                    break;
            }

            iTextSharp.text.Image objImageFondo = iTextSharp.text.Image.GetInstance(imageFilePath);
            // Cambia el tamaño de la imagen
            objImageFondo.ScaleToFit(piLargo, piAncho);

            // Se indica que la imagen debe almacenarse como fondo
            objImageFondo.Alignment = iTextSharp.text.Image.UNDERLYING;

            // Coloca la imagen en una posición absoluta
            switch (psType)
            {

                case "mexico":

                    objImageFondo.SetAbsolutePosition(90, 10);//155
                    break;
                case "aguila":
                    objImageFondo.SetAbsolutePosition(55, 155);

                    break;

                case "cenefa_baja_zapata":
                    objImageFondo.SetAbsolutePosition(55, 20);
                    break;

                case "cenefa_baja_leona":
                    objImageFondo.SetAbsolutePosition(30, 10);
                    break;

                case "cenefa_baja_independencia":
                    objImageFondo.SetAbsolutePosition(30, 10);
                    break;

                case "cenefa_baja_2022":
                    objImageFondo.SetAbsolutePosition(40, 18);
                    break;

            }
            //   objImageFondo.SetAbsolutePosition (-100, 15);
            // Imprime la imagen como fondo de página
            pdDocument.Add(objImageFondo);

        }

        /// <summary>
        /// Metodo que valida y crea carpetas de las comisonse  año/unidadAdministrativa/comisiones/carpetas
        /// </summary>
        public static void Valida_Carpeta(string psDescripcion, string psFolio)
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio);
            Ruta = raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio;
            lsUbicacion = "PDF/" + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio;
        }

        public static void Valida_Carpeta_Reimpresion(string psDescripcion, string psFolio)
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\";
            if (!Directory.Exists(raiz + psDescripcion)) Directory.CreateDirectory(raiz + psDescripcion);
            Ruta = raiz + psDescripcion;
            lsUbicacion = psDescripcion;
        }

        public static void Valida_Carpeta_Reportes_Viaticos(string psRuta)
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);
            if (!Directory.Exists(raiz + Year + "/" + lsHoy)) Directory.CreateDirectory(raiz + Year + "/" + lsHoy);
            psRuta = raiz + Year + "/" + lsHoy;
        }


        public static void DonwloadFile(string raiz, string patch)
        {
            //   string  ruta = raiz + Year + "/" + lsHoy ;
            System.IO.FileInfo toDownload = new System.IO.FileInfo(raiz + "/" + patch);
            //  new System.IO.FileInfo(HttpContext.Current.Server.MapPath(patch));

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                       "attachment; filename=" + toDownload.Name);
            HttpContext.Current.Response.AddHeader("Content-Length",
                       toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(raiz + "/" + patch);
            HttpContext.Current.Response.End();
        }

        public static void Genera_Ministracion(Comision poComision, string psFolio)
        {
            float[] medidaCeldas;
            //  string Ruta = "";
            Year = poComision.Periodo;
            Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Comisionado, true), poComision.Archivo.Replace(".pdf", ""));
            string path = Dictionary.CADENA_NULA;
            string archivo = "Ministracion - " + poComision.Archivo;
            path = Ruta + "\\" + archivo;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            double total = clsFuncionesGral.Convert_Double(Dictionary.NUMERO_CERO);
            int consecutivo = 1;

            //Creamos el dodumento con sus caracteristicas
            document = Crea_Pdf();
            //  Fuente_Normal();

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            //Apertura del documento
            document.Open();
            //CREA TABLA ENCABEZADO
            PdfPTable table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

            //DATOS ENCABEZADO
            //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
            Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 400, 100, 0);
            document.Add(table);
            table = null;

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            PdfPTable subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("detalle de la comisión"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            Agrega_TablainCelda_Drawing(table, 0, subtabla);
            //Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
            subtabla = null;
            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("nOMBRE DEL SERVIDOR PUBLICO COMISONADO : " + clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado))), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            //agergar si es ampliacion pff 

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objeto de la comisión "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Principales actividades desarrolladas"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "Se anexa informe de Comisión", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Evaluación"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "Comprobación de Comision Oficio Número : " + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Folio Comprobacion SMAF.WEB :" + archivo.Replace(".pdf", "") + " / " + psFolio), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Fecha cierre comprobacion SMaf.web :" + lsHoy), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Documentos de Comprobación :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "( ) Programa de Trabajo", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, " ( X ) Otros", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);

            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "( ) Acta Circunstanciada", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);


            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "( ) Diploma o Constancia de Participación", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);

            document.Add(table);

            table = null;

            //agregar las demas zonas
            if ((poComision.Zona_Comercial == "1") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "3") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "5") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "11") | (poComision.Zona_Comercial == "12") | (poComision.Zona_Comercial == "13") | (poComision.Zona_Comercial == "14") | (poComision.Zona_Comercial == "15") | (poComision.Zona_Comercial == "19"))
            {
                table = Genera_Tabla_SinEncabezado(6, 100, Dictionary.ALIGN_CENTER);

                //encabezados
                PdfPCell cNum = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Num"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cNum.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cNum);

                PdfPCell cLugar = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Lugar de la comisión"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cLugar.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cLugar);

                PdfPCell cPeriodo = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("periodo de la comision"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cPeriodo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cPeriodo);

                PdfPCell cCuota = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("cuota diaria"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cCuota.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cCuota);

                PdfPCell cDias = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("días"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cDias.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cDias);

                PdfPCell cImporte = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("importe otorgado"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cImporte.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cImporte);


                //datos zona normalaza
                PdfPCell cConsecutivo = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(consecutivo.ToString()), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cConsecutivo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cConsecutivo);

                PdfPCell cLugar1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(poComision.Lugar), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cLugar1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cLugar1);

                PdfPCell cPeriodo1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(poComision.Fecha_Inicio + " al " + poComision.Fecha_Final), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cPeriodo1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cPeriodo1);

                PdfPCell cCuota1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("$ " + clsFuncionesGral.Convert_Decimales(MngNegocioComision.Obtiene_Tarifa(poComision.Zona_Comercial))), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cCuota1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cCuota1);

                PdfPCell cDias1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(poComision.Dias_Reales), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cDias1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cDias1);

                PdfPCell cImporte1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos)), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cImporte1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cImporte1);


                if (poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO)
                {
                    PdfPCell cellCombustible = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" combustible en efectivo"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                    cellCombustible.Colspan = 5;
                    cellCombustible.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cellCombustible);
                    table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo)), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));

                }

                if (poComision.Peaje != Dictionary.NUMERO_CERO)
                {
                    PdfPCell cellPeaje = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" peaje"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                    cellPeaje.Colspan = 5;
                    cellPeaje.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cellPeaje);
                    table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje)), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));

                }

                if (poComision.Pasaje != Dictionary.NUMERO_CERO)
                {
                    PdfPCell cellPasaje = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" pasaje"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                    cellPasaje.Colspan = 5;
                    cellPasaje.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cellPasaje);
                    table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje)), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                }


                total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

                PdfPCell cell = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" efectivo total otorgado"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                cell.Colspan = 5;
                cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cell);
                table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ " + clsFuncionesGral.Convert_Decimales(total.ToString())), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));

                PdfPCell recibi = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("recibi en efectivo la cantidad de :"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                recibi.Colspan = 6;
                recibi.HorizontalAlignment = 1;
                table.AddCell(recibi);

                if ((poComision.Forma_Pago_Viaticos == "1") | (poComision.Forma_Pago_Viaticos == Dictionary.NUMERO_CERO))
                {
                    table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));

                    PdfPCell recibiLetter = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("  D  e  v  e  n  g  a  d  o  s  "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                    recibiLetter.Colspan = 5;
                    recibiLetter.HorizontalAlignment = 1;
                    table.AddCell(recibiLetter);
                }
                else
                {
                    table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ " + clsFuncionesGral.Convert_Decimales(total.ToString())), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));

                    PdfPCell recibiLetter = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(total), true), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
                    recibiLetter.Colspan = 5;
                    recibiLetter.HorizontalAlignment = 0;
                    table.AddCell(recibiLetter);
                }


                document.Add(table);

            }

            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("relación de gastos "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("desglose de erogaciones comprobadas con documentacion con requisitos fiscales "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(4, 100, Dictionary.ALIGN_CENTER);

            PdfPCell cFecha = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("fecha"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cFecha.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cFecha);

            PdfPCell cConcepto = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cConcepto.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cConcepto);

            PdfPCell cImporteT = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("importe"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cImporteT.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cImporteT);

            PdfPCell cObservaciones = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("observaciones"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cObservaciones.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cObservaciones);

            //crea subtabla
            subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            //obtiene lista de conceptos
            List<Entidad> oConceptos = MngNegocioComprobacion.Obtiene_Lista_Conceptos();
            //recorrelista y agrega a subtabla fecha maxima de facturas de ese concepto
            foreach (Entidad oent in oConceptos)
            {
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioComprobacion.Fecha_Max_Factura(poComision.Comisionado, poComision.Ubicacion_Comisionado, oent.Codigo, poComision.Oficio, poComision.Archivo, "2")), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
            }
            //crea celda y agrega subtabla
            PdfPCell cdetalleFech = new PdfPCell(subtabla);
            cdetalleFech.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            //agrega celda con subtabla a tabla principal
            table.AddCell(cdetalleFech);


            subtabla = null;

            subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            foreach (Entidad oent in oConceptos)
            {
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(oent.Descripcion), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
            }
            //crea celda y agrega subtabla
            PdfPCell cDescripcion = new PdfPCell(subtabla);
            cDescripcion.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            //agrega celda con subtabla a tabla principal
            table.AddCell(cDescripcion);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            foreach (Entidad oent in oConceptos)
            {
                AgregaDatosTabla(subtabla, "$   " + clsFuncionesGral.Convert_Decimales(MngNegocioComprobacion.Sum_Importe(poComision.Comisionado, poComision.Ubicacion_Comisionado, oent.Codigo, poComision.Oficio, poComision.Archivo)), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
            }
            //crea celda y agrega subtabla
            PdfPCell cImportes = new PdfPCell(subtabla);
            cImportes.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
            //agrega celda con subtabla a tabla principal
            table.AddCell(cImportes);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            foreach (Entidad oent in oConceptos)
            {
                List<Entidad> LlFacturas = MngNegocioComprobacion.ObtieneListaImportes(poComision.Comisionado, oent.Codigo, poComision.Oficio, poComision.Archivo, poComision.Ubicacion_Comisionado, "2");

                if (LlFacturas.Count > 1)
                {
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Se anexa relacion de facturas"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                }
                else
                {
                    if (LlFacturas.Count == 1)
                    {
                        foreach (Entidad x in LlFacturas)
                        {
                            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(x.Descripcion), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                        }
                    }
                    else if (LlFacturas.Count == 0)
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("   "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                    }
                }
            }

            PdfPCell cOdesglose = new PdfPCell(subtabla);
            cOdesglose.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            //agrega celda con subtabla a tabla principal
            table.AddCell(cOdesglose);

            PdfPCell cellTotal = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("total: "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
            cellTotal.Colspan = 2;
            cellTotal.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cellTotal);

            string totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "2");

            double totalOtorgado = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);


            if (clsFuncionesGral.Convert_Double(totalComprobad) > totalOtorgado)
            {
                totalComprobad = totalOtorgado.ToString();
            }

            PdfPCell cellTotalNum = new PdfPCell(new Phrase(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(totalComprobad), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
            cellTotalNum.Colspan = 2;
            cellTotal.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cellTotalNum);

            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("desglose de erogaciones comprobadas con documentacion sin requisitos fiscales "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(4, 100, Dictionary.ALIGN_CENTER);

            PdfPCell cFechaNoFac = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("fecha"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cFechaNoFac.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cFechaNoFac);

            PdfPCell cConceptoNoFact = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cConceptoNoFact.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cConceptoNoFact);

            PdfPCell cImporteNoFact = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("importe"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cImporteNoFact.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cImporteNoFact);

            PdfPCell cObservacionesNoFac = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("observaciones"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cObservacionesNoFac.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cObservacionesNoFac);

            List<Entidad> oNofacturable = MngNegocioComprobacion.Obtiene_No_Facturables(poComision.Comisionado, poComision.Archivo, "3", false);

            if (oNofacturable.Count > 0)
            {
                foreach (Entidad oEntidad in oNofacturable)
                {
                    string[] ads = new string[2];
                    ads = oEntidad.Codigo.Split(new Char[] { '|' });
                    string[] ads1 = new string[2];
                    ads1 = oEntidad.Descripcion.Split(new Char[] { '|' });

                    PdfPCell cFech = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.FormatFecha(ads[0]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cFech.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cFech);

                    PdfPCell cConcep = new PdfPCell(Agrega_Parrafo(ads1[0], Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cConcep.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cConcep);

                    PdfPCell cImpor = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.Convert_Decimales(ads[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cImpor.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cImpor);

                    PdfPCell cObser = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(ads1[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cObser.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cObser);
                }
            }
            else
            {
                PdfPCell cFech = new PdfPCell(Agrega_Parrafo("1900-01-01", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cFech.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cFech);

                PdfPCell cConcep = new PdfPCell(Agrega_Parrafo("-", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cConcep.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cConcep);

                PdfPCell cImpor = new PdfPCell(Agrega_Parrafo("0", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cImpor.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cImpor);

                PdfPCell cObser = new PdfPCell(Agrega_Parrafo("-", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cObser.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cObser);
            }


            PdfPCell cellNOTotal = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("total: "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
            cellNOTotal.Colspan = 2;
            cellNOTotal.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cellNOTotal);


            totalComprobad = Dictionary.NUMERO_CERO;
            if ((poComision.Zona_Comercial == "15"))
            {
                totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "19");
            }
            else if (poComision.Zona_Comercial == "18")
            {
                totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "19");
                totalComprobad = (clsFuncionesGral.Convert_Double(totalComprobad) + clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12"))).ToString();
            }
            else
            {
                totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12");
            }
            PdfPCell cellNOTotalNum = new PdfPCell(new Phrase(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(totalComprobad), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
            cellNOTotalNum.Colspan = 2;
            cellNOTotalNum.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cellNOTotalNum);

            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("reintegro efectuado "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            document.Add(table);

            table = null;

            table = Genera_Tabla_SinEncabezado(4, 100, Dictionary.ALIGN_CENTER);

            PdfPCell cFechaReintegro = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("fecha"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cFechaReintegro.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cFechaReintegro);

            PdfPCell cConceptoReintegro = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cConceptoReintegro.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cConceptoReintegro);

            PdfPCell cImporteReintegro = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("importe"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cImporteReintegro.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cImporteReintegro);

            PdfPCell cObservacionesreintegro = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("observaciones"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
            cObservacionesreintegro.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cObservacionesreintegro);

            oNofacturable = null;
            oNofacturable = MngNegocioComprobacion.Obtiene_No_Facturables(poComision.Comisionado, poComision.Archivo, "3", true);

            if (oNofacturable.Count > 0)
            {
                foreach (Entidad oEntidad in oNofacturable)
                {
                    string[] ads = new string[2];
                    ads = oEntidad.Codigo.Split(new Char[] { '|' });
                    string[] ads1 = new string[2];
                    ads1 = oEntidad.Descripcion.Split(new Char[] { '|' });

                    PdfPCell cFech = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.FormatFecha(ads[0]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cFech.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cFech);

                    PdfPCell cConcep = new PdfPCell(Agrega_Parrafo(ads1[0], Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cConcep.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cConcep);

                    PdfPCell cImpor = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.Convert_Decimales(ads[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cImpor.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cImpor);

                    PdfPCell cObser = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(ads1[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño));
                    cObser.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cObser);
                }
            }
            else
            {
                PdfPCell cFech = new PdfPCell(Agrega_Parrafo("1900-01-01", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cFech.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cFech);

                PdfPCell cConcep = new PdfPCell(Agrega_Parrafo("-", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cConcep.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cConcep);

                PdfPCell cImpor = new PdfPCell(Agrega_Parrafo("0", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cImpor.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cImpor);

                PdfPCell cObser = new PdfPCell(Agrega_Parrafo("-", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cObser.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cObser);
            }

            PdfPCell cellRETotal = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("total: "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
            cellRETotal.Colspan = 2;
            cellRETotal.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cellRETotal);

            totalComprobad = Dictionary.NUMERO_CERO;
            totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "13");

            PdfPCell cellRETotalIm = new PdfPCell(new Phrase(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(totalComprobad), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
            cellRETotalIm.Colspan = 2;
            cellRETotalIm.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cellRETotalIm);
            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

            /*  AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño );
              AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
              AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("          "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
              */
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("certificado de transito"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("          "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Certifico que el c.             "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Ha permanecido en esta ciudad del                         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("al                   "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("observaciones                                        "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("ciudad o localidad"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);


            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("sello"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Certifica:nombre ,firma y puesto "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

            /* AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
             AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
             AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
             */
            document.Add(table);

            table = null;

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("declaro bajo protesta de decir la verdad, que los datos contenidos en este formato son veridicos y manifiesto tener conocimiento de las sanciones que se aplicaran en caso contrario.  "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
            document.Add(table);

            table = null;
            subtabla = null;

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informa y entrega"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("comisionado"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            PdfPCell cInforma = new PdfPCell(subtabla);
            cInforma.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cInforma);

            /*Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(poComision.Dep_Proy, poComision.Proyecto, Year);
            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(objProyecto.Dependencia);

            if (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION)
            {
                subtabla = null;
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("valida"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);

                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("titular de la unidad responsable"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                PdfPCell cValida = new PdfPCell(subtabla);
                cValida.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cValida);

                subtabla = null;

                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("recibe y acepta"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Vobo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("titular del area administrativa"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                PdfPCell cacepta = new PdfPCell(subtabla);
                cacepta.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cacepta);
            }
            else
            {
                //checar subdirecciones y aut y validador iguales
                subtabla = null;
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("valida"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);

                if (poComision.Autoriza == MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION))
                {
                    Usuario objAdministrador = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), true);

                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(objAdministrador.Cargo), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);//A.K.A
                    objAdministrador = null;
                }
                else
                {
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("titular de la unidad responsable"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }

                PdfPCell cValida = new PdfPCell(subtabla);
                cValida.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cValida);

                subtabla = null;

                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("recibe y acepta"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_DIRECTOR_ADMON, Dictionary.DIRECTOR_ADMINISTRACION)))), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("titular del area administrativa"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                PdfPCell cacepta = new PdfPCell(subtabla);
                cacepta.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cacepta);
            }
            */
            document.Add(table);

            subtabla = null;
            table = null;
            if ((poComision.Zona_Comercial != "15") && (poComision.Zona_Comercial != "18"))
            {
                oNofacturable = MngNegocioComprobacion.Obtiene_No_Facturables(poComision.Comisionado, poComision.Archivo, "3", false);

                if (oNofacturable.Count > 0)
                {
                    document.NewPage();
                    table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

                    //DATOS ENCABEZADO
                    //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                    Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 400, 100, 0);
                    document.Add(table);
                    table = null;
                    table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                    subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("cOMPRoBANTE DE GASTOS NO FISCALES"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 0, subtabla);
                    //Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
                    subtabla = null;
                    document.Add(table);
                    table = null;

                    table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Bueno por : $ " + clsFuncionesGral.Convert_Decimales(MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12"))), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    document.Add(table);

                    table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12")), true)), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                    document.Add(table);

                    table = null;

                    /*tabla de conceptos*/
                    table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

                    PdfPCell No1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    No1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(No1);

                    PdfPCell No2 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("importe"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    No2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(No2);

                    PdfPCell No3 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Observaciones"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    No3.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(No3);

                    foreach (Entidad oEntidad in oNofacturable)
                    {
                        string[] ads = new string[2];
                        ads = oEntidad.Codigo.Split(new Char[] { '|' });
                        string[] ads1 = new string[2];
                        ads1 = oEntidad.Descripcion.Split(new Char[] { '|' });

                        /*PdfPCell cFech = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.FormatFecha(ads[0]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                        cFech.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                        table.AddCell(cFech);*/

                        PdfPCell cConcep = new PdfPCell(Agrega_Parrafo(ads1[0], Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                        cConcep.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                        table.AddCell(cConcep);

                        PdfPCell cImpor = new PdfPCell(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(ads[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                        cImpor.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                        table.AddCell(cImpor);

                        PdfPCell cObser = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(ads1[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                        cObser.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                        table.AddCell(cObser);
                    }

                    PdfPCell cTotality = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("total :"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cTotality.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cTotality);

                    PdfPCell cImporteTotality = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("$ " + clsFuncionesGral.Convert_Decimales(MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12"))), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cImporteTotality.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cImporteTotality);

                    PdfPCell Nulo = new PdfPCell(Agrega_Parrafo("  ", Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    Nulo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(Nulo);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                    document.Add(table);
                    table = null;

                    table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);
                    PdfPCell Periodo = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Periodo de la comision "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    Periodo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(Periodo);

                    AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

                    PdfPCell cPeriodo11 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(poComision.Fecha_Inicio + " al " + poComision.Fecha_Final), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cPeriodo11.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cPeriodo11);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" Lugar de la Comision :" + clsFuncionesGral.ConvertMayus(poComision.Lugar)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                    document.Add(table);

                    table = null;
                    table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                    subtabla = null;
                    subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Nombre y firma del comisionado"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                    PdfPCell cInforma1 = new PdfPCell(subtabla);
                    cInforma1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cInforma1);
                    document.Add(table);
                    subtabla = null;
                    table = null;
                }

            }
            List<GridView> listaComp = MngNegocioComprobacion.ListaComprobantes(poComision.Oficio, poComision.Comisionado, poComision.Archivo, poComision.Ubicacion_Comisionado);

            if (listaComp.Count > 0)
            {
                document.NewPage();

                table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

                //DATOS ENCABEZADO
                //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 400, 100, 0);
                document.Add(table);
                table = null;
                table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("relacion de comprobantes"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                Agrega_TablainCelda_Drawing(table, 0, subtabla);
                //Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
                subtabla = null;
                document.Add(table);

                table = null;


                table = Genera_Tabla_SinEncabezado(6, 100, Dictionary.ALIGN_CENTER);

                PdfPCell titulo1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo1);

                PdfPCell titulo2 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("PDF"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo2);

                PdfPCell titulo3 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("XML"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo3.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo3);

                PdfPCell titulo4 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("TICKET"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo4.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo4);

                PdfPCell titulo5 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("IMPORTE"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo5.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo5);

                PdfPCell titulo6 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("FOLIO FISCAL"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo6.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo6);

                foreach (GridView obj in listaComp)
                {
                    PdfPCell cConcep1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Adscripcion), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cConcep1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cConcep1);

                    PdfPCell cPDF = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Comisionado), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cPDF.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cPDF);

                    PdfPCell cXML = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Lugar), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cXML.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cXML);

                    PdfPCell cTICKET = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Rol), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cTICKET.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cTICKET);

                    PdfPCell cImport = new PdfPCell(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(obj.RFC), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cImport.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cImport);

                    PdfPCell cFolioF = new PdfPCell(Agrega_Parrafo(obj.Ubicacion, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cFolioF.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cFolioF);
                }
                document.Add(table);
                table = null;
            }
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("INAPESCA " + Year), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("generado via web por Smaf.inapesca.gob.mx/index.aspx"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

            document.Add(table);

            table = null;
            oNofacturable = null;
            oConceptos = null;

            document.Close();
            //proceso de descarga
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\" + Year + "\\" + MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Comisionado, true) + "\\" + Dictionary.COMISIONES + "\\" + poComision.Archivo.Replace(".pdf", "");

            /*
              string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio);
            Ruta = raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio;
            lsUbicacion = "PDF/" + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio;
             */

            DonwloadFile(raiz, archivo);
            //  Process.Start(path);
        }

        //metodo que genera reporte pdf
        public static void Genera_Reporte_Viaticos(List<ComisionDetalle> plReporte, string psInicio, string psFinal, string psDescripTipoviaticos, string psEstatusComprobacion, string psDescripFinanciero, string psNombreArchivo, string psUsuario, string psAdscripcion, string psComisionado, string psOpcion)
        {
            float[] medidaCeldas;
            string Ruta = "";
            Valida_Carpeta_Reportes_Viaticos(Ruta);
            document = Crea_Pdf_Reporte_Viaticos(psOpcion);

            string lsArchivo = psNombreArchivo + lsExtencion;
            string path = Ruta + "\\" + lsArchivo;


            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            //Apertura del documento
            document.Open();
            if (psOpcion == "1")
            {
                PdfPTable table = new PdfPTable(3);
                table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

                Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 100, 80, 0);

                PdfPTable subtabla = new PdfPTable(3);

                subtabla = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("   Reporte generado por :"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                string usuariosG = MngNegocioUsuarios.Obtiene_Nombre(psUsuario);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(usuariosG), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Fecha :  " + lsHoy), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                //   AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("  Año : " + psAnio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Periodo : " + psInicio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("al :" + psFinal), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("  Tipo Viaticos : " + psDescripTipoviaticos), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Tipo COmprobacion : " + psEstatusComprobacion), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" Estatus Financiero : " + psDescripFinanciero), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("  Adscripcion : " + psAdscripcion), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Usuario : " + psComisionado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                Agrega_TablainCelda_Drawing(table, 0, subtabla);
                // AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Reporte de Viaticos Generado el dia : " + lsHoy ), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                Agrega_Imagen_Celda(table, "firma-inapesca", 100, 80, 0);

                AgregaDatosTabla(table, "", 1, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, "", 1, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, "", 1, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                document.Add(table);
                foreach (ComisionDetalle cd in plReporte)
                {
                    TotalOtorgado = 0;
                    TotalComprobado = 0;
                    comprobado = "";
                    table = null;
                    medidaCeldas = new float[2];
                    medidaCeldas[0] = 1.65f;
                    medidaCeldas[1] = 3.35f;

                    table = Genera_Tabla_TamañoPersonalizado(2, 100, "LEFT", medidaCeldas);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("comisionado : "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    //subtabla 
                    subtabla = null;
                    subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(cd.Comisionado)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(cd.Archivo), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    Agrega_TablainCelda_Drawing(table, 1, subtabla);
                    //termina subtabla
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("adcripcion : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioDependencia.Centro_Descrip(cd.Ubicacion_Comisionado)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Lugar de la comision : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(cd.Lugar), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Priodo de la Comision : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                    if (cd.Inicio == cd.Final)
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(cd.Inicio), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("del " + cd.Inicio + " al " + cd.Final), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }


                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objetivo de la comision : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(cd.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Tipo Viaticos : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    if (cd.Tipo == "1")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Devengado"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (cd.Tipo == "2")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Anticipado"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Viaticos Calculados: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(cd.Viaticos), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalOtorgado += clsFuncionesGral.Convert_Double(cd.Viaticos);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Viaticos Comprobados: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    comprobado = MngDatosComprobacion.Total(cd.Comisionado, cd.Ubicacion_Comisionado, cd.Archivo, "'5','9'");
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(comprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalComprobado += clsFuncionesGral.Convert_Double(comprobado);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Combustible Calculados: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(cd.Combustible), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalOtorgado += clsFuncionesGral.Convert_Double(cd.Combustible);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Combustible Comprobado: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    comprobado = MngDatosComprobacion.Total(cd.Comisionado, cd.Ubicacion_Comisionado, cd.Archivo, "'6'");
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(comprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalComprobado += clsFuncionesGral.Convert_Double(comprobado);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("peaje Calculados: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(cd.Peaje), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalOtorgado += clsFuncionesGral.Convert_Double(cd.Peaje);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("peaje Comprobado: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    comprobado = MngDatosComprobacion.Total(cd.Comisionado, cd.Ubicacion_Comisionado, cd.Archivo, "'7'");
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(comprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalComprobado += clsFuncionesGral.Convert_Double(comprobado);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("pasaje Calculados: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(cd.Pasaje), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalOtorgado += clsFuncionesGral.Convert_Double(cd.Pasaje);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("pasaje Comprobado: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    comprobado = MngDatosComprobacion.Total(cd.Comisionado, cd.Ubicacion_Comisionado, cd.Archivo, "'8'");
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(comprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalComprobado += clsFuncionesGral.Convert_Double(comprobado);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("10 % no Comprobable: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    comprobado = MngDatosComprobacion.Total(cd.Comisionado, cd.Ubicacion_Comisionado, cd.Archivo, "'12'");
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(comprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalComprobado += clsFuncionesGral.Convert_Double(comprobado);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Reintegro efectuado: "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    comprobado = MngDatosComprobacion.Total(cd.Comisionado, cd.Ubicacion_Comisionado, cd.Archivo, "'13'");
                    AgregaDatosTabla(table, " $ " + clsFuncionesGral.Convert_Decimales(comprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    TotalComprobado += clsFuncionesGral.Convert_Double(comprobado);

                    if (cd.Tipo == "1")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("total Calculado : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else if (cd.Tipo == "2")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("total otorgado : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales("$ " + TotalOtorgado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("total comprobado : "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales("$ " + TotalComprobado), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Estatus comprobacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    if (cd.EstatuS == "5")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("comprobado y validado"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (cd.EstatuS == "7")
                    {
                        if (TotalOtorgado > TotalComprobado)
                        {
                            if (cd.Tipo == "1")
                            {
                                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("comprobado"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                            }
                            else
                            {
                                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Deudor"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                            }
                        }
                        else if (TotalOtorgado < TotalComprobado)
                        {
                            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("comprobado sin validar"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        }
                    }
                    else if (cd.EstatuS == "9")
                    {
                        if (cd.Tipo == "1")
                        {
                            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("comprometido"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        }
                        else if (cd.Tipo == "2")
                        {
                            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("deudor"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        }

                    }

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("estatus financiero :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                    if (cd.Financieros == "2")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("pagado"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (cd.Financieros == "3")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("pagado pasivo"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (cd.Financieros == "9")
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("comprometido"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }

                    AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, "   ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    document.Add(table);
                }

            }
            else
            {


                /* float[] medidaCeldas = { 2f, 0.55f };
                float[] medidaCeldas = new float[3];//{ 1.65f, 3.35f };
                medidaCeldas[0] = 1.50f;
                medidaCeldas[1] = 6f;
                medidaCeldas[2] = 1.50f;

                PdfPTable table = new PdfPTable(3);
                table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

                Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 125, 80, 0);

                PdfPTable subtabla = new PdfPTable(3);

                subtabla = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("   Reporte generado por :"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                string usuariosG = MngNegocioUsuarios.Obtiene_Nombre(psUsuario);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(usuariosG), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Fecha :  " + lsHoy), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                //   AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("  Año : " + psAnio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Periodo : " + psInicio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("al :" + psFinal), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("  Tipo Viaticos : " + psDescripTipoviaticos), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Tipo COmprobacion : " + psEstatusComprobacion), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" Estatus Financiero : " + psDescripFinanciero), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("  Adscripcion : " + psAdscripcion), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Usuario : " + psComisionado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                Agrega_TablainCelda_Drawing(table, 0, subtabla);
                // AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Reporte de Viaticos Generado el dia : " + lsHoy ), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);

                AgregaDatosTabla(table, "", 1, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, "", 1, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, "", 1, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                document.Add(table);


                table = null;
                table = Genera_Tabla_SinEncabezado(14, 100, Dictionary.ALIGN_LEFT);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Comisionado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Viaticos Otorgados"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Viaticos Comprobados"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Combustible Otorgado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Combustible Comprobado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Peaje Otorgado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Peaje Comprobado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Pasaje Otorgado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Pasaje Comprobado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Reintegro"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Reintegro Efectuado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Situacion"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Costo Total Comision"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);


                /*     foreach (Reporte_Viaticos rv in plReporte)
                     {
                         if (rv.Tipo == "1")
                         {
                             AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(rv.Oficio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(rv.Comisionado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Viaticos_Otorgados), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Viaticos_Comprobados), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Combustible_Otorgado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Combustible_Comprobado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Peaje_Otorgado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Peaje_Comprobado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Pasaje_Otorgado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Pasaje_Comprobado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Reintegro), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Reintegro_Efectuado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                             if (rv.Financiero == "9")
                             {
                                 AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("por pagar"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             }
                             else if ((rv.Financiero == "2") | (rv.Financiero == "3"))
                             {
                                 AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("pagado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             }

                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Total), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                         }
                         else if (rv.Tipo == "2")
                         {
                             AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(rv.Oficio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(rv.Comisionado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Viaticos_Otorgados), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Viaticos_Comprobados), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Combustible_Otorgado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Combustible_Comprobado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Peaje_Otorgado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Peaje_Comprobado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Pasaje_Otorgado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Pasaje_Comprobado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Reintegro), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Reintegro_Efectuado), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

                             totalOtorgado = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double ( rv.Viaticos_Otorgados) +clsFuncionesGral .Convert_Double ( rv.Combustible_Otorgado) +clsFuncionesGral .Convert_Double ( rv.Peaje_Otorgado) +clsFuncionesGral .Convert_Double ( rv.Pasaje_Otorgado));

                             if (totalOtorgado >= clsFuncionesGral.Convert_Double(rv.Total))
                             {
                                 AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Deudor"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             }
                             else if (totalOtorgado == clsFuncionesGral.Convert_Double(rv.Total))
                             {
                                 AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("pagado-comprobado"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             }
                             else if (totalOtorgado <= clsFuncionesGral.Convert_Double(rv.Total))
                             {
                                 AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("A favor"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                             }
                             AgregaDatosTabla(table, clsFuncionesGral.Convert_Decimales(rv.Total), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                         }


                     }
                 *   
                     
            
            document.Add(table);*/
            }
            document.Close();
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\" + Year + "\\" + lsHoy + "\\";
            DonwloadFile(raiz, lsArchivo);

        }
        //Metodo que genera oficio de comision jllmexti
        public static void Genera_Oficio_Comision(Comision poComision, Ubicacion poUbicacion, Proyecto objProyecto, string psNombreAut, string psCargo, bool pBandera = false, string psOfDGAIPP="")
        {
            Year = poComision.Periodo;
            string config = Dictionary.CADENA_NULA;
            string estado, ciudad;
            Entidad oDireccionTipo = MngDatosDependencia.Obtiene_Tipo_Region(poComision.Ubicacion_Comisionado);

            if (pBandera) MngNegocioComision.Update_Oficio_Comision(poComision, oDireccionTipo); //Crea y actualiza num de oficio de comison que va a ser usado en archivo pdf y fisico

            PdfPTable subtabla;
            PdfPTable subtabla1;
            string[] datosVehiculo = new string[9];
            CLave_Oficio = Dictionary.CADENA_NULA;
            string path = Dictionary.CADENA_NULA;

            CLave_Oficio = clsFuncionesGral.ConvertMayus("rjl-" + clsFuncionesGral.ConvertMayus(poUbicacion.Organismo) + "-");

            /*inicia modificacion de minutario para direcciones adjuntas*/
            string rol_comisionado = MngDatosUsuarios.Obtiene_Rol_Usuario(poComision.Comisionado);

            string clave_pdf = "";

            /* if (poComision.Territorio == "3")
             {
                // CLave_Oficio = "";
                 CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("42") + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
             }
             else if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "43"))
             {
                 CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("43") + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
             }
             else if (((rol_comisionado == Dictionary.INVESTIGADOR) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (poComision.Ubicacion_Comisionado == "43"))
             {
                 CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("43") + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
             }
             else if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "44"))
             {
                 CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("44") + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
             }
             else if (((rol_comisionado == Dictionary.DIRECTOR_JURIDICO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION)) && (oDireccionTipo.Codigo == "42"))
             {
                 CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("42") + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
             }
             else
             {
                 CLave_Oficio += poUbicacion.Siglas + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
             }
             */

            if (poComision.Territorio == Dictionary.Internacional)
            {
                CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(Dictionary.DG) + "-" + poComision.Oficio + "-" + poComision.Periodo;
            }
            else if ((poComision.Ubicacion_Autoriza == Dictionary.DGAA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAJ) | (poComision.Ubicacion_Autoriza == Dictionary.DG) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP))
            {
                CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(poComision.Ubicacion_Autoriza) + "-" + poComision.Oficio + "-" + poComision.Periodo;
            }
            else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
            {
                CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(oDireccionTipo.Codigo) + "-" + poComision.Oficio + "-" + poComision.Periodo;
            }
            else
            {
                CLave_Oficio += poUbicacion.Siglas + "-" + poComision.Oficio + "-" + poComision.Periodo;
            }
            /*termina modificacion de minutario para direcciones adjuntas*/

            string archivo = ""; // 

            if (pBandera)
            {
                archivo = CLave_Oficio + lsExtencion;
                poComision.Archivo = archivo;
            }
            else archivo = CLave_Oficio + "-autografa" + lsExtencion;
            //Ruta donde se va a guardar el archivo
            /*
                        if (poComision.Territorio == "3")
                        {
                            Valida_Carpeta(MngNegocioDependencia.Centro_Descrip("42", true), poComision.Archivo.Replace(".pdf", ""));
                        }
                        else if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "43"))
                        {
                            Valida_Carpeta(MngNegocioDependencia.Centro_Descrip("43", true), poComision.Archivo.Replace(".pdf", ""));
                        }
                        else if (((rol_comisionado == Dictionary.INVESTIGADOR) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (poComision.Ubicacion_Comisionado == "43"))
                        {
                            Valida_Carpeta(MngNegocioDependencia.Centro_Descrip("43", true), poComision.Archivo.Replace(".pdf", ""));
                        }
                        else if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "44"))
                        {
                            Valida_Carpeta(MngNegocioDependencia.Centro_Descrip("44", true), poComision.Archivo.Replace(".pdf", ""));
                        }
                        else if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "42"))
                        {
                            Valida_Carpeta(MngNegocioDependencia.Centro_Descrip("42", true), poComision.Archivo.Replace(".pdf", ""));
                        }
                        else
                        {
                            Valida_Carpeta(poUbicacion.Descripcion, poComision.Archivo.Replace(".pdf", ""));
                        }
                        */


            if (poComision.Territorio == Dictionary.Internacional)
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(Dictionary.DG, true), poComision.Archivo.Replace(".pdf", ""));
            }
            else if ((poComision.Ubicacion_Autoriza == Dictionary.DGAA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAJ) | (poComision.Ubicacion_Autoriza == Dictionary.DG) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP))
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Autoriza, true), poComision.Archivo.Replace(".pdf", ""));
            }
            else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(oDireccionTipo.Codigo, true), poComision.Archivo.Replace(".pdf", ""));

            }
            else
            {
                Valida_Carpeta(poUbicacion.Descripcion, poComision.Archivo.Replace(".pdf", ""));
                //  CLave_Oficio += poUbicacion.Siglas + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
            }

            path = Ruta + "\\" + archivo;

            if (pBandera) MngNegocioComision.Update_ruta_Comision(lsUbicacion, archivo, poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Oficio, poComision);

            poComision.Ruta = lsUbicacion;
            /*
            //ACTUALIZAR EL PEDO EXISTENCIAL DE PEDRO SIERRA INSERT
            if (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP )
            {
                bool x = MngDatosDgaipp.Inserta_Oficio_Comision(poComision.Oficio,poComision.Archivo , "OC","0", poComision.Objetivo,"", poComision.Ubicacion_Comisionado , poComision.Comisionado, poComision.Vobo,"1",Year );
            }

            //  ComisionDetalle oDetalle = MngNegocioComisionDetalle.Obtiene_detalle(poComision.Archivo, poComision.Comisionado, poComision.Ubicacion_Comisionado, Year);
            */
            //Creamos el dodumento con sus caracteristicas
            document = Crea_Pdf();
            //  Fuente_Normal();

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            //Apertura del documento
            document.Open();

            //Cabeceras de documento aling rigth
            //Imagen_Fondo(document, "mexico", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            //Imagen_Fondo(document, "cenefa_baja_zapata", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            //Imagen_Fondo(document, "cenefa_baja_leona", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            //Imagen_Fondo(document, "cenefa_baja_independencia", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            Imagen_Fondo(document, "cenefa_baja_2022", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            PdfPTable table = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

            float[] medidaCeldas = new float[1];//{ 1.65f, 3.35f };
            medidaCeldas[0] = 7f;

            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_LEFT, medidaCeldas);
            /*if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2018-12-01"))
            {
                Agrega_Imagen_Celda(subtabla, "SAGARPA_old", 160, 100, 0);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-08-01"))
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.SAGARPA, 160, 100, 0);
            }
            else
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULTURA, 160, 100, 0);
            }*/
            //Agrega_Imagen_Celda(subtabla, Dictionary.SAGARPA, 160, 100, 0);
            //Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULTURA, 160, 100, 0);
            //Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULINAPESCAOLD, 400, 100, 0);
            Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULINAPESCA, 400, 100, 0);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            subtabla = null;
            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_RIGHT, medidaCeldas);
            if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-06-05"))
            {
                Agrega_Imagen_Celda(subtabla, "firma-inapesca_old", 160, 100, 0);
            }else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-08-15"))
            {
                
                Agrega_Imagen_Celda(subtabla, "firma-inapesca", 160, 100, 0);
            }
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            config = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA1");
            if (poComision.Territorio == "3")
            {
                clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "            -");
                clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else if (CLave_Oficio.Substring(13, 3) == "DG-")//else if (((psRol == Dictionary.DIRECTOR_ADJUNTO) | (psRol == Dictionary.DIRECTOR_ADMINISTRACION) | (psRol == Dictionary.DIRECTOR_JURIDICO)) && (poDireccionTipo.Codigo == "42"))
            {
                clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "                        -");
                clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control de viaticos: ") + poComision.Oficio, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);

            }
            else if (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP)//(CLave_Oficio.Substring(13, 3) == "DG-")
            {
                //clave_pdf = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-DGAIPP-" + poComision.Oficio_DGAIPP + "-" + Year); Secomento el 28-08-2021 - por la modificación en el estatuto del INAPESCA
                clave_pdf = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-DIPP-" + poComision.Oficio_DGAIPP + "-" + Year);// opcion 1 dejarlo como este renglon en caso de que la abreviatura de la DGAIPP cambie a DIPP o bien comentar todo el if y que se ponga el consecutivo de smaf como las demas direcciones.
                //clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control de viaticos: ") + poComision.Oficio, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);

            }
            else if (poComision.Ubicacion_Autoriza == config)
            {
                if ((psOfDGAIPP == "0") || (psOfDGAIPP == null) || (psOfDGAIPP == Dictionary.CADENA_NULA))
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control DGAIPP: ") + psOfDGAIPP, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);
                }

            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            document.Add(table);

            /*  table = null;
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_RIGHT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //PRUEBA EXUISTENCIAL DE DECRETO
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" 2017, AÑO DEL CENTENARIO DE LA PROMULGACIÓN DE LA CONSTITUCIÓN POLÍTICA DE LOS ESTADOS UNIDOS MEXICANOS"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
            document.Add(table);
            */

            table = null;
            table = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("datos del comisionado"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Autoriza);


            if (oUbicacion.Ciudad == "DF")
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), oUbicacion.Clvestado.Trim());

                if (((Convert.ToDateTime(poComision.Fecha_Inicio)) < (Convert.ToDateTime(Dictionary.FECHA_ACTUAL))))
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
                    
                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(Dictionary.FECHA_ACTUAL), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
                }

            }
            else
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), poUbicacion.Clvestado.Trim());
                if (((Convert.ToDateTime(poComision.Fecha_Inicio)) < (Convert.ToDateTime(Dictionary.FECHA_ACTUAL))))
                {

                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad) + "," + clsFuncionesGral.ConvertMinus(estado)) + ",  a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad) + "," + clsFuncionesGral.ConvertMinus(estado)) + ",  a " + clsFuncionesGral.Convert_Mes_Letra(Dictionary.FECHA_ACTUAL), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                }
            }
            oUbicacion = null;

            Usuario objUsuario = MngNegocioUsuarios.Datos_Comisionado(poComision.Comisionado, poComision.Ubicacion_Comisionado);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.NOMBRE + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Nombre + " " + objUsuario.ApPat + " " + objUsuario.ApMat), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.CARGO + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Cargo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.RFC + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.RFC), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.CLAVE_PUESTO + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Nivel + " " + objUsuario.Plaza), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.NOMBRE_PUESTO + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Puesto), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.UBICACION_ADSCRIPCION + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poUbicacion.Dependencia == Dictionary.DG)
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("AV. MÉXICO 190 COL. DEL CARMEN ALCALDÍA-COYOACÁN."), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else if (poUbicacion.Ciudad == "DF")
            {
                estado = MngNegocioEstado.Estado(poUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(poUbicacion.Ciudad.Trim(), poUbicacion.Clvestado.Trim());
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poUbicacion.Descripcion + " , " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poUbicacion.Descripcion + "," + MngNegocioEstado.Estado(poUbicacion.Clvestado) + "."), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Telefono :  " + poUbicacion.Telefono), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            document.Add(table);

            //Tabla Datos Comisión
            table = null;
            medidaCeldas = new float[2];//{ 1.65f, 3.35f };
            medidaCeldas[0] = 1.65f;
            medidaCeldas[1] = 3.35f;
            //            { 2.55f, 2.25f };//0.55f
            table = Genera_Tabla_TamañoPersonalizado(2, 100, "LEFT", medidaCeldas);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.DESTINO + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Lugar), 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.PERIODO_COMISION + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //Validacion de periodo de comison
            if (poComision.Fecha_Inicio == poComision.Fecha_Final)
            {
                AgregaDatosTabla(table, clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio), 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Del ") + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio)) + clsFuncionesGral.ConvertMayus("  al  ") + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Final)) + ".", 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.MEDIO_TRANSPORTE + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //tipos de transporte autorizado


            switch (poComision.Tipo_Aut)
            {
                /***************vehiculos nulos o ninguno****************/
                case "NUL": //vehiculo oficial

                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    // AgregaDatosTabla(Subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    // AgregaDatosTabla_Colspan(Subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;


                /*********vehiculos validos************/
                case "VO": //vehiculo oficial

                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AB"://autobus
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AP"://auto personal
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "AC": //Embarcacion
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AE": //avion
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "AA"://AVION EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AV": //AVION - VEHICULO OFICIAL
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VB": //vehiculo oficial - autobus
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VP": //vehiculo oficial - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VA"://vehiculo oficial - embarcacion
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VAA": //VEHICULO OFICIAL - AVION - ACUATICO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);



                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VAP": //VEHICULO OFICIAL - AUTOBUS - AUTO PERSONAL
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "BA"://Autobus Aereo
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "ABP": //AUTOBUS  - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "BEM": //AUTOBUS  - EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acutico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AAA"://AUTOBUS - AVION - ACUATICO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AVE"://AUTOBUS - AVION - VEHICULO OFICIAL - EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "PP": //AUTO PROPIO - AVION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "APE": //AUTO PROPIO   - EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "PAA"://AUTO PROPIO - AVION -ACUATICO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "VAB": //VEHICULO OFICIAL - AVION - AUTOBUS 
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "OAP": //VEHICULO OFICIAL - AVION - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AAP": //AUTOBUS - AVION - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;
                /*****************/
                case "VEA"://VEHICULO OFICIAL-EMBARCACION-AUTOBUS
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    datosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (datosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < datosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (datosVehiculo[j] != null)) | ((j == 5) & (datosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += datosVehiculo[j] + separador;
                        }
                    }
                    break;
                /*******************/

            }

            AgregaDatosTabla(table, "DESCRIPCION DEL VEHICULO", 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, lsVehiculo, 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            //Hora inicio y fin comision

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Horario de salida :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Inicio_Comision), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Horario de regreso :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Fin_Comision), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Detalles de la comisión :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Nombre del programa / POA : "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Descripcion), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Número del programa :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Clv_Proy + " - " + MngNegocioDependencia.Centro_Descrip(objProyecto.Dependencia)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Responsable del programa / POA :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(objProyecto.Responsable)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objetivo del programa / POA :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Metas institucionales :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Producto == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                ListaProductos = poComision.Producto.Split(new Char[] { '|' });
                ListaNewProdductos = new string[3];

                for (int i = 0; i < ListaProductos.Length; i++)
                {
                    ListaNewProdductos[i] = MngNegocioProductos.Obtiene_Descripcion_Producto(ListaProductos[i], Year);
                }
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                for (int x = 0; x < ListaNewProdductos.Length; x++)
                {
                    if ((ListaNewProdductos[x] != null) | (ListaNewProdductos[x] != ""))
                    {
                        AgregaDatosTabla(subtabla, ListaNewProdductos[x], 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                }
                //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioProductos.Obtiene_Descripcion_Producto(poComision.Producto)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                Agrega_TablainCelda_Drawing(table, 0, subtabla);
            }


            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Recurso pesquero :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Pesqueria == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioEspecies.Obtiene_Descripcion_Pesqueria(objProyecto.Programa, poComision.Pesqueria, objProyecto.Direccion)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Actividad especifica :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Actividad == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioActividades.Obtiene_Descripcion_Actividades(poComision.Actividad)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objetivo de la comisión :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            //observaciones de Comision
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Observaciones :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Observaciones_Vobo.Length < 52)
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Observaciones_Vobo) + "\n\n\n", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            else
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Observaciones_Vobo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            //Pago de viaticos
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Pago de viaticos :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            // Subtabla1 = null;
            subtabla1 = Genera_Tabla_SinEncabezado(2, 100, "LEFT");

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("ANTICIPADOS"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Forma_Pago_Viaticos == "2") Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
            else Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
            Agrega_TablainCelda_Drawing(subtabla1, 0, subtabla);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("devengados"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Forma_Pago_Viaticos == "1") Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
            else Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
            Agrega_TablainCelda_Drawing(subtabla1, 0, subtabla);

            Agrega_TablainCelda_Drawing(table, 0, subtabla1);

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");

            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Viaticos : $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            if (objProyecto.Dependencia == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objProyecto.Dependencia, "COMBUSTIBLES"))
            {
                if ((poComision.Pago_Combustible == "1"))
                {
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
                else
                {
                    if (poComision.Pago_Combustible == "2")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible Vales : $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (poComision.Pago_Combustible == "3")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible efectivo : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible en vales : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Total Vales Efectivos:") + clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales) / 100), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    }
                    else
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                }
            }
            else if (objUsuario.Ubicacion == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objUsuario.Ubicacion, "COMBUSTIBLES"))
            {
                if ((poComision.Pago_Combustible == "1"))
                {
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
                else
                {
                    if (poComision.Pago_Combustible == "2")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible Vales : $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (poComision.Pago_Combustible == "3")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible efectivo : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible en vales : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Total Vales Efectivos:") + clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales) / 100), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    }
                    else
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                }
            }
            else
            {
                AgregaDatosTabla(subtabla, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible en vales : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Total Vales Efectivos:") + clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales) / 100), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            }

            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            if ((poComision.Vale_Comb_I == Dictionary.NUMERO_CERO) | (poComision.Vale_Comb_I == null) | (poComision.Vale_Comb_I == Dictionary.CADENA_NULA))
            {
                ;
            }
            else
            {
                AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                subtabla = null;
                subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible Folio Vale Inicio : " + poComision.Vale_Comb_I), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible folio vales Final:  ") + poComision.Vale_Comb_F, 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                Agrega_TablainCelda_Drawing(table, 0, subtabla);
            }


            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("peaje : $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("pasajes : $ ") + clsFuncionesGral.Convert_Decimales(poComision.Pasaje), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);


            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "\n", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            //nuevo
            table = null;//ACA
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);

            oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Comisionado);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if ((poComision.Territorio == Dictionary.Internacional))
            {
                if ((objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_GRAL) | (objUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                {
                    AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    //  AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }

            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }


            //puesto del que autoriza o su ptm
            config = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA1");

            Usuario usu = MngNegocioUsuarios.Datos_Administrador(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo, true);
            if (poComision.Ubicacion_Autoriza == config)
            {

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(usu.Cargo), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza, poComision.Ubicacion_Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            }


            string psUsuario = "";
           /*if (poComision.Territorio == Dictionary.Internacional)
            {
                if ((objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_GRAL) | (objUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                {
                    AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    //  AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }
                else
                {
                    if ((oUbicacion.Tipo == Dictionary.DIRECCION_JEFE))// | (oUbicacion.Tipo == Dictionary.DIRECCION_JURIDICA) | (oUbicacion.Tipo == Dictionary.DIRECCION_ADMON) | (oUbicacion.Tipo == Dictionary.DIRECCIONES_ADJUNTAS))
                    {
                        AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else if (oUbicacion.Tipo == Dictionary.DIRECCION_JURIDICA)
                    {
                        psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_JURIDICO, oUbicacion.Dependencia);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oUbicacion.Dependencia)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else if (oUbicacion.Tipo == Dictionary.DIRECCION_ADMON)
                    {
                        psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, oUbicacion.Dependencia);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oUbicacion.Dependencia)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else if (oUbicacion.Tipo == Dictionary.DIRECCIONES_ADJUNTAS)
                    {
                        psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oUbicacion.Dependencia);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oUbicacion.Dependencia)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else
                    {
                        psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oDireccionTipo.Codigo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }

                }
            }
            else
            {
            */
                AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //}

            if (poComision.Ubicacion_Autoriza == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA"))
            {
                Agrega_Imagen_Celda(table, poComision.Autoriza, 100, 100, 0);
            }
            else
            {
                Agrega_Imagen_Celda(table, "fondo", 10, 100, 0);
            }

            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_LEFT);
            //aki nuevamente
            if (poComision.Ubicacion_Autoriza == config)
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(usu.Abreviatura + " " + usu.Nombre + " " + usu.ApPat + " " + usu.ApMat)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            //usu = null;
            //aki morro
            /*if (poComision.Territorio == Dictionary.Internacional)
            {
                if ((objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_GRAL) | (objUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                {
                    AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    //  AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }
                else
                {
                    if ((oUbicacion.Tipo == Dictionary.DIRECCION_JEFE))// | (oUbicacion.Tipo == Dictionary.DIRECCION_JURIDICA) | (oUbicacion.Tipo == Dictionary.DIRECCION_ADMON) | (oUbicacion.Tipo == Dictionary.DIRECCIONES_ADJUNTAS))
                    {
                        AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(psUsuario)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                    }
                }

            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
             */

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);
            usu = null;

            //con copia y viaticos y combustible
            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);

            Usuario objAdministrador = new Usuario();

            objAdministrador = MngNegocioUsuarios.Datos_Administrador(MngNegocioUsuarios.Obtiene_Rol(poComision.Vobo), poComision.Vobo);


            AgregaDatosTabla(table, "C.c.p.   " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(objAdministrador.Abreviatura + " " + objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + ", " + objAdministrador.Cargo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);

            if (objProyecto.Dependencia != poComision.Ubicacion_Comisionado)
            {

                objAdministrador = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), true);
                if (poComision.Vobo != objAdministrador.Usser)
                {
                    AgregaDatosTabla(table, "C.c.p.   " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(objAdministrador.Abreviatura + " " + objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + ", " + objAdministrador.Cargo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);
                }

            }

            objAdministrador = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "", Dictionary.PUESTO_RECURSOS_HUMANOS));
            AgregaDatosTabla(table, "C.c.p.   " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(objAdministrador.Abreviatura + " " + objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + ", " + objAdministrador.Cargo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);

            AgregaDatosTabla(table, "Archivo.", 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);
            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, 6);

            document.Add(table);

            //document.Add(Agrega_Parrafo(clsFuncionesGral.ConvertMinus("www.inapesca.gob.mx"), Dictionary.FUENTE_NORMAL, Dictionary.GRAY, Dictionary.ALIGN_CENTER, 6));
            document.Close();

            if (pBandera)
            {
                clsMail.Mail_Oficio_Comision(poComision, objProyecto, path);
            }

            //Process.Start(path);

            document.Close();

            pdfw.Close();

            // if (!pBandera)
            //{
            //   string raiz = HttpContext.Current.Server.MapPath("..")+"\\" + poComision .Ruta + "\\";
            //  DonwloadFile(raiz, poComision .Archivo );

            //            }
        }
       
        public void Destructor()
        {


        }

        /// <summary>
        /// Metodo que reimpirme genera oficio de comision segun paerametros
        /// </summary>
        /// <param name="poComision"></param>
        /// <param name="poUbicacion"></param>
        /// <param name="objProyecto"></param>
        /// <param name="psNombreAut"></param>
        /// <param name="psCargo"></param>
        /// <param name="pBandera"></param>
        public static void Reimprime_Oficio_Comision(Comision poComision, Ubicacion poUbicacion, Proyecto objProyecto, string psRol, Entidad poDireccionTipo, string psUbicacionAutoriza)
        {
            string estado, ciudad;
            string config = Dictionary.CADENA_NULA;
            //Valida_Carpeta(poUbicacion.Descripcion, poComision.Archivo.Replace(".pdf", ""));

            //   string rol_comisionado = MngDatosUsuarios.Obtiene_Rol_Usuario(poComision.Comisionado);
            //  Entidad oDireccionTipo = MngDatosDependencia.Obtiene_Tipo_Region(poComision.Ubicacion_Comisionado);

            // if (poComision.Territorio == "3")
            // {
            //Valida_Carpeta_Reimpresion(MngNegocioDependencia.Centro_Descrip(Dictionary.DG, true), poComision.Archivo.Replace(".pdf", ""));
            //}
            //else
            //{
            Valida_Carpeta_Reimpresion(poComision.Ruta, poComision.Archivo.Replace(".pdf", ""));
            //}

            //!Directory.Exists(raiz + Year

            if (File.Exists(Ruta + "/" + poComision.Archivo))
            {
                File.Delete(Ruta + "/" + poComision.Archivo);
            }

            document = Crea_Pdf();

            string[] DatosVehiculo = new string[9];
            CLave_Oficio = Dictionary.CADENA_NULA;
            string path = Dictionary.CADENA_NULA;

            CLave_Oficio = poComision.Archivo.Replace(".pdf", "");

            string archivo = ""; // 

            archivo = CLave_Oficio + lsExtencion;

            path = HttpContext.Current.Server.MapPath("..") + "\\" + poComision.Ruta + "\\" + poComision.Archivo;

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            document.Open();

            PdfPTable subtabla;
            PdfPTable Subtabla1;
            // string[] organismo = MngNegocioDependencia.ObtieneDatosOrganismo(poUbicacion.Secretaria, poUbicacion.Organismo, "1").Split(new Char[] { '|' });
            //string[] header = new string[5];

            //   header = MngNegocioDependencia.ObtieneDatosHeader(poUbicacion.Organismo, poUbicacion.Dependencia);

            if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-01-01"))
            {
                Imagen_Fondo(document, "aguila", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-08-01"))
            {
                Imagen_Fondo(document, "mexico", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2020-01-01"))
            {
                Imagen_Fondo(document, "cenefa_baja_zapata", 500, 500, Dictionary.FUENTE_SUBRAYADO);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-01-01"))
            {
                Imagen_Fondo(document, "cenefa_baja_leona", 530, 500, Dictionary.FUENTE_SUBRAYADO);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2022-01-01"))
            {
                Imagen_Fondo(document, "cenefa_baja_independencia", 530, 500, Dictionary.FUENTE_SUBRAYADO);
            }
            else 
            {
                Imagen_Fondo(document, "cenefa_baja_2022", 530, 500, Dictionary.FUENTE_SUBRAYADO);
            }
            

            PdfPTable table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_JUSTIFIED);

            //subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);

            // Agrega_TablainCelda_Drawing(table, 0, subtabla);

            float[] medidaCeldas = new float[1];//{ 1.65f, 3.35f };
            medidaCeldas[0] = 7f;
            
            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_LEFT, medidaCeldas);
            if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2018-12-01"))
            {
                Agrega_Imagen_Celda(subtabla, "SAGARPA_old", 160, 100, 0);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-07-26"))
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.SAGARPA, 160, 100, 0);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-08-01"))
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULTURA, 160, 100, 0);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-01-01"))
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULINAPESCAOLD, 400, 100, 0);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2022-04-08"))
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULINAPESCANORMAL2022, 400, 100, 0);
            }
            else 
            {
                Agrega_Imagen_Celda(subtabla, Dictionary.AGRICULINAPESCA, 400, 100, 0);
            }

            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            subtabla = null;
            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_RIGHT, medidaCeldas);
            if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-07-26"))
            {
                Agrega_Imagen_Celda(subtabla, "firma-inapesca_old", 160, 100, 0);
            }
            else if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2019-08-01"))
            {
                Agrega_Imagen_Celda(subtabla, "firma-inapesca", 160, 100, 0);
            }
            
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            string clave_pdf = "";
            config = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA1");
            if (poComision.Territorio == "3")
            {
                if (poComision.Oficio_DG == Dictionary.CADENA_NULA)
                {
                    clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "            -");
                    clave_pdf = clave_pdf.Replace(".pdf", "");
                }
                else
                {
                    clave_pdf = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-DG-" + poComision.Oficio_DG + "-" + poComision.Periodo);

                }

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control de viaticos: ") + poComision.Oficio, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);

            }
            else if (CLave_Oficio.Substring(13, 3) == "DG-")//else if (((psRol == Dictionary.DIRECTOR_ADJUNTO) | (psRol == Dictionary.DIRECTOR_ADMINISTRACION) | (psRol == Dictionary.DIRECTOR_JURIDICO)) && (poDireccionTipo.Codigo == "42"))
            {
                if (poComision.Oficio_DG == Dictionary.CADENA_NULA)
                {
                    clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "                        -");
                    clave_pdf = clave_pdf.Replace(".pdf", "");
                }
                else
                {
                    clave_pdf = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-DG-" + poComision.Oficio_DG + "-" + poComision.Periodo);

                }

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control de viaticos: ") + poComision.Oficio, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);

            }
            else if (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP)//(CLave_Oficio.Substring(13, 3) == "DG-")
            {

                if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-08-28"))
                {
                    clave_pdf = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-DGAIPP-" + poComision.Oficio_DGAIPP + "-" + poComision.Periodo);
                }
                else
                {
                    clave_pdf = clsFuncionesGral.ConvertMayus("RJL-INAPESCA-DIPP-" + poComision.Oficio_DGAIPP + "-" + poComision.Periodo); //si cambia abreviatura en no. oficio así quedará, si no hay que hacer modificación o dejarlo como estaba antes del 28-08-2021
                }
                
                
                //clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control de viaticos: ") + poComision.Oficio, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);

            }
            else if (poComision.Ubicacion_Autoriza == config) 
            {
                if (poComision.Oficio_DGAIPP == "0")
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    
                }
                else 
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("consecutivo control DGAIPP: ") + poComision.Oficio_DGAIPP, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tMedia);
                }
                
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            document.Add(table);
            /*    table = null;
                table = Genera_Tabla_SinEncabezado(2, 100,Dictionary.ALIGN_RIGHT );
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                //PRUEBA EXUISTENCIAL DE DECRETO
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(  " 2017, AÑO DEL CENTENARIO DE LA PROMULGACIÓN DE LA CONSTITUCIÓN POLÍTICA DE LOS ESTADOS UNIDOS MEXICANOS"), 0, Dictionary.FUENTE_ITALICA , Dictionary.BLACK, tPequeño);
                document.Add(table);
                */
            table = null;
            table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_JUSTIFIED);

            //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No." + poComision.Archivo.Replace(".pdf", "")), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("datos del comisionado"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Autoriza);


            if (oUbicacion.Ciudad == "DF")
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), oUbicacion.Clvestado.Trim());
                if (((Convert.ToDateTime(poComision.Fecha_Inicio)) < (Convert.ToDateTime(poComision.Fecha_Vobo))))
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Vobo), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
                }
                //AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Vobo), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            }
            else
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), poUbicacion.Clvestado.Trim());
                if (((Convert.ToDateTime(poComision.Fecha_Inicio)) < (Convert.ToDateTime(poComision.Fecha_Vobo))))
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad) + "," + clsFuncionesGral.ConvertMinus(estado)) + ",  a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);

                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad) + "," + clsFuncionesGral.ConvertMinus(estado)) + ",  a " + clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Vobo), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
                }
                
            }
            oUbicacion = null;

            //aqui
            
             Usuario objUsuario = MngNegocioUsuarios.Datos_Comisionado(poComision.Comisionado, poComision.Ubicacion_Comisionado);
            
            
            
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.NOMBRE + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Nombre + " " + objUsuario.ApPat + " " + objUsuario.ApMat), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.CARGO + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if ((objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO))
            {
                if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-08-28"))
                {

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo_job_periodo(poComision.Comisionado, poComision.Ubicacion_Comisionado, poComision.Periodo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
                else 
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Cargo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
            }
            else 
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Cargo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
           
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.RFC + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.RFC), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.CLAVE_PUESTO + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Nivel + " " + objUsuario.Plaza), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.NOMBRE_PUESTO + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if ((objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO))
            {
                if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-08-28"))
                {

                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_puesto_job_periodo(poComision.Comisionado, poComision.Ubicacion_Comisionado, poComision.Periodo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Cargo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objUsuario.Puesto), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.UBICACION_ADSCRIPCION + ": "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if(poUbicacion.Dependencia==Dictionary.DG)
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("AV. MÉXICO 190 COL. DEL CARMEN ALCALDÍA-COYOACÁN."), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poUbicacion.Descripcion + "," + MngNegocioEstado.Estado(poUbicacion.Clvestado) + "."), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Telefono :  " + poUbicacion.Telefono), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, "  ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);

            document.Add(table);

            //Tabla Datos Comisión
            table = null;
            medidaCeldas = new float[2];//{ 1.65f, 3.35f };
            medidaCeldas[0] = 1.65f;
            medidaCeldas[1] = 3.35f;
            //            { 2.55f, 2.25f };//0.55f
            table = Genera_Tabla_TamañoPersonalizado(2, 100, Dictionary.ALIGN_JUSTIFIED, medidaCeldas);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.DESTINO + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Lugar), 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.PERIODO_COMISION + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //Validacion de periodo de comison
            if (poComision.Fecha_Inicio == poComision.Fecha_Final)
            {
                AgregaDatosTabla(table, clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio), 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Del ") + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Inicio)) + clsFuncionesGral.ConvertMayus("  al  ") + clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convert_Mes_Letra(poComision.Fecha_Final)) + ".", 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(Dictionary.MEDIO_TRANSPORTE + ": "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //tipos de transporte autorizado

            //tipos de transporte autorizado


            switch (poComision.Tipo_Aut)
            {
                /***************vehiculos nulos o ninguno****************/
                case "NUL": //vehiculo oficial

                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    // AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    // AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;


                /*********vehiculos validos************/
                case "AA"://AVION EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VO": //vehiculo oficial

                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AB"://autobus
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AP"://auto personal
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "AC": //Embarcacion
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AE": //avion
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AV": //AVION - VEHICULO OFICIAL
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VB": //vehiculo oficial - autobus
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VP": //vehiculo oficial - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VA"://vehiculo oficial - embarcacion
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VAA": //VEHICULO OFICIAL - AVION - ACUATICO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);



                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "VAP": //VEHICULO OFICIAL - AUTOBUS - AUTO PERSONAL
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "BA"://Autobus Aereo
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "ABP": //AUTOBUS  - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "BEM": //AUTOBUS  - EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acutico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AAA"://AUTOBUS - AVION - ACUATICO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AVE"://AUTOBUS - AVION - VEHICULO OFICIAL - EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "PP": //AUTO PROPIO - AVION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "APE": //AUTO PROPIO   - EMBARCACION
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "PAA"://AUTO PROPIO - AVION -ACUATICO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "VAB": //VEHICULO OFICIAL - AVION - AUTOBUS 
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "OAP": //VEHICULO OFICIAL - AVION - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;

                case "AAP": //AUTOBUS - AVION - AUTO PROPIO
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;
                case "VEA"://VEHICULO OFICIAL-EMBARCACION-AUTOBUS
                    medidaCeldas = new float[6];//{ 1.65f, 3.35f };
                    medidaCeldas[0] = 0.50f;
                    medidaCeldas[1] = 0.11f;
                    medidaCeldas[2] = 0.50f;
                    medidaCeldas[3] = 0.11f;
                    medidaCeldas[4] = 0.50f;
                    medidaCeldas[5] = 0.11f;

                    subtabla = null;
                    subtabla = Genera_Tabla_TamañoPersonalizado(6, 100, "LEFT", medidaCeldas);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("vehiculo oficial"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("autobus"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("auto propio"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Terrestre :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Terrestre), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("avion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido aereo :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Aereo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("embarcacion "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Recorrido Acuatico :"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
                    AgregaDatosTabla_Colspan(subtabla, clsFuncionesGral.ConvertMayus(poComision.Origen_Dest_Acu), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño, 5);


                    Agrega_TablainCelda_Drawing(table, 1, subtabla);

                    //  oUbicacionTrans = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Trans_Aut);
                    DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(poComision.Clase_Aut, poComision.Tipo_Aut, poComision.Ubicacion_Trans_Aut, poComision.Vehiculo_Autorizado);

                    separador = string.Empty;
                    lsVehiculo = "";
                    if (DatosVehiculo[0] == null)
                    { lsVehiculo = Dictionary.CADENA_NULA; }
                    else
                    {
                        for (int j = 0; j < DatosVehiculo.Length; j++)
                        {
                            if (((j == 2) & (DatosVehiculo[j] != null)) | ((j == 5) & (DatosVehiculo[j] != null))) separador = "\r";
                            else separador = " ";
                            lsVehiculo += DatosVehiculo[j] + separador;
                        }
                    }
                    break;
                /*******************/


            }

            AgregaDatosTabla(table, "DESCRIPCION DEL VEHICULO", 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, lsVehiculo, 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            //Hora inicio y fin comision

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Horario de salida :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Inicio_Comision.Trim()), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Horario de regreso :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Fin_Comision.Trim()), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Detalles de la comisión :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño);
            
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Nombre del programa / POA : "), 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Descripcion), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            
            if ((poComision.Ubicacion_Autoriza == Dictionary.DGAA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPA))
            {
                if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-08-28"))
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Número del programa :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Clv_Proy + " - " + MngNegocioDependencia.Descrip_Centro_PERIODO(objProyecto.Dependencia, poComision.Periodo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Número del programa :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Clv_Proy + " - " + MngNegocioDependencia.Centro_Descrip(objProyecto.Dependencia)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                }
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Número del programa :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Clv_Proy + " - " + MngNegocioDependencia.Centro_Descrip(objProyecto.Dependencia)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Responsable del programa / POA :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(objProyecto.Responsable)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objetivo del programa / POA :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(objProyecto.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Metas institucionales :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Producto == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                ListaProductos = poComision.Producto.Split(new Char[] { '|' });
                ListaNewProdductos = new string[3];

                for (int i = 0; i < ListaProductos.Length; i++)
                {
                    ListaNewProdductos[i] = MngNegocioProductos.Obtiene_Descripcion_Producto(ListaProductos[i], Year, poComision.Fecha_Vobo);
                }
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);

                for (int x = 0; x < ListaNewProdductos.Length; x++)
                {
                    if ((ListaNewProdductos[x] != null) | (ListaNewProdductos[x] != ""))
                    {
                        AgregaDatosTabla(subtabla, ListaNewProdductos[x], 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                }
                //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioProductos.Obtiene_Descripcion_Producto(poComision.Producto)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                Agrega_TablainCelda_Drawing(table, 0, subtabla);
            }

            //            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioProductos.Obtiene_Descripcion_Producto(poComision.Producto)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Recurso pesquero :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Pesqueria == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioEspecies.Obtiene_Descripcion_Pesqueria(objProyecto.Programa, poComision.Pesqueria, objProyecto.Direccion)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Actividad especifica :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Actividad == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioActividades.Obtiene_Descripcion_Actividades(poComision.Actividad)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objetivo de la comisión :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            //observaciones de Comision
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Observaciones :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Observaciones_Vobo.Length < 52)
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Observaciones_Vobo) + "\n\n\n", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            else
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Observaciones_Vobo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            //Pago de viaticos
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Pago de viaticos :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            // Subtabla1 = null;
            Subtabla1 = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_JUSTIFIED);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_JUSTIFIED);
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("ANTICIPADOS"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Forma_Pago_Viaticos == "2") Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
            else Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
            Agrega_TablainCelda_Drawing(Subtabla1, 0, subtabla);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("devengados"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            if (poComision.Forma_Pago_Viaticos == "1") Agrega_Imagen_Celda(subtabla, "seleccionado", 10, 10, 0);
            else Agrega_Imagen_Celda(subtabla, "deseleccionado", 10, 10, 0);
            Agrega_TablainCelda_Drawing(Subtabla1, 0, subtabla);

            Agrega_TablainCelda_Drawing(table, 0, Subtabla1);

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);


            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Viaticos : $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            //nuevo
            if (objProyecto.Dependencia == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objProyecto.Dependencia, "COMBUSTIBLES"))
            {
                if ((poComision.Pago_Combustible == "1"))
                {
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
                else
                {
                    if (poComision.Pago_Combustible == "2")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible Vales : $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (poComision.Pago_Combustible == "3")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible efectivo : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible en vales : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Total Vales Efectivos:") + clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales) / 100), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    }
                    else
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                }
            }
            else if (objUsuario.Ubicacion == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(objUsuario.Ubicacion, "COMBUSTIBLES"))
            {
                if ((poComision.Pago_Combustible == "1"))
                {
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Efectivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                }
                else
                {
                    if (poComision.Pago_Combustible == "2")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible Vales : $ ") + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Vales), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                    else if (poComision.Pago_Combustible == "3")
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible efectivo : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible en vales : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Total Vales Efectivos:") + clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales) / 100), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    }
                    else
                    {
                        AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible EFECTIVO: $ ") + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                    }
                }
            }
            else
            {
                AgregaDatosTabla(subtabla, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible en vales : $ ") + clsFuncionesGral.Convert_Decimales(clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales))), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Total Vales Efectivos:") + clsFuncionesGral.ConvertString(clsFuncionesGral.Convert_Double(poComision.Combustible_Vales) / 100), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            }


            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            if ((poComision.Vale_Comb_I == Dictionary.NUMERO_CERO) | (poComision.Vale_Comb_I == null) | (poComision.Vale_Comb_I == Dictionary.CADENA_NULA))
            {
                ;
            }
            else
            {
                AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                subtabla = null;
                subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible Folio Vale Inicio : " + poComision.Vale_Comb_I), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Combustible folio vales Final:  ") + poComision.Vale_Comb_F, 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                Agrega_TablainCelda_Drawing(table, 0, subtabla);
            }

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("peaje : $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("pasajes : $ ") + clsFuncionesGral.Convert_Decimales(poComision.Pasaje), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");


            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "\n", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            table = null;//ACA
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);

            oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Comisionado);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            /*if ((poComision.Territorio == Dictionary.Internacional))
            {
                if ((objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_GRAL) | (objUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                {
                    AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    //  AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }
                else
                {
                    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }

            }
            else
            {*/
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            /*}*/

            //puesto del que autoriza o su ptm
            //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza, poComision.Ubicacion_Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            
            config = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA1");
            Entidad oDireccionTipo = MngDatosDependencia.Obtiene_Tipo_Region(poComision.Ubicacion_Comisionado);

            Usuario usu = MngNegocioUsuarios.Datos_Administrador(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo, true);
            if (poComision.Ubicacion_Autoriza == config)
            {

                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(usu.Cargo), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            }
            else
            {
            
                if((poComision.Ubicacion_Autoriza== Dictionary.DGAA)| (poComision.Ubicacion_Autoriza== Dictionary.DGAIA)|(poComision.Ubicacion_Autoriza== Dictionary.DGAIPP)|(poComision.Ubicacion_Autoriza== Dictionary.DGAIPA))
                {
                    if (Convert.ToDateTime(poComision.Fecha_Inicio) < Convert.ToDateTime("2021-08-28"))
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo_job_periodo(poComision.Autoriza, poComision.Ubicacion_Autoriza, poComision.Periodo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                    }
                    else 
                    {
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza, poComision.Ubicacion_Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                
                }else
                {
                     AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza, poComision.Ubicacion_Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                }
                
                
            }


            string psUsuario = "";
            /*  if (poComision.Territorio == Dictionary.Internacional)
              {
                  if ((objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_GRAL) | (objUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                  {
                      AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                      //  AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                  }
                  else
                  {
                      if ((oUbicacion.Tipo == Dictionary.DIRECCION_JEFE))// | (oUbicacion.Tipo == Dictionary.DIRECCION_JURIDICA) | (oUbicacion.Tipo == Dictionary.DIRECCION_ADMON) | (oUbicacion.Tipo == Dictionary.DIRECCIONES_ADJUNTAS))
                      {
                          AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                      }
                      else if (oUbicacion.Tipo == Dictionary.DIRECCION_JURIDICA)
                      {
                          psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_JURIDICO, oUbicacion.Dependencia);
                          AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oUbicacion.Dependencia)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                      }
                      else if (oUbicacion.Tipo == Dictionary.DIRECCION_ADMON)
                      {
                          psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, oUbicacion.Dependencia);
                          AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oUbicacion.Dependencia)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                      }
                  else if (oUbicacion.Tipo == Dictionary.DIRECCIONES_ADJUNTAS)
                      {
                          psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oUbicacion.Dependencia);
                          AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, oUbicacion.Dependencia)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                      }
                      else
                      {
                          psUsuario = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, poDireccionTipo.Codigo);
                          AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(psUsuario, poDireccionTipo.Codigo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                      }

                  }
              }
              else
              {*/
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //  }

            if (poComision.Ubicacion_Autoriza == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA"))
            {
                Agrega_Imagen_Celda(table, poComision.Autoriza, 100, 100, 0);
            }
            else
            {
                Agrega_Imagen_Celda(table, "fondo", 10, 100, 0);

                //AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                //AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                //AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_LEFT);

            //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Ubicacion_Autoriza == config)
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(usu.Abreviatura + " " + usu.Nombre + " " + usu.ApPat + " " + usu.ApMat), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            usu = null;
            oDireccionTipo = null;
            //aki morro
            /*    if (poComision.Territorio == Dictionary.Internacional)
                {
                    if ((objUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (objUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (objUsuario.Rol == Dictionary.DIRECTOR_GRAL) | (objUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                    {
                        AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                        //  AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    }
                    else
                    {
                        if ((oUbicacion.Tipo == Dictionary.DIRECCION_JEFE))// | (oUbicacion.Tipo == Dictionary.DIRECCION_JURIDICA) | (oUbicacion.Tipo == Dictionary.DIRECCION_ADMON) | (oUbicacion.Tipo == Dictionary.DIRECCIONES_ADJUNTAS))
                        {
                            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                        }
                        else
                        {
                            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(psUsuario)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                        }
                    }

                }
                else
                {*/
            //}

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            //con copia y viativos y combustible
            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

            Usuario objAdministrador = new Usuario();

            objAdministrador = MngNegocioUsuarios.Datos_Administrador(MngNegocioUsuarios.Obtiene_Rol(poComision.Vobo), poComision.Vobo);


            AgregaDatosTabla(table, "C.c.p.   " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(objAdministrador.Abreviatura + " " + objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + ", " + objAdministrador.Cargo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);


            if ((objProyecto.Dependencia != poComision.Ubicacion_Comisionado) & (poComision.Vobo != MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "", Dictionary.PUESTO_FINANCIERO)))
            {
                objAdministrador = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), true);

                AgregaDatosTabla(table, "C.c.p.   " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(objAdministrador.Abreviatura + " " + objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + ", " + objAdministrador.Cargo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);
            }

            objAdministrador = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "", Dictionary.PUESTO_RECURSOS_HUMANOS));
            AgregaDatosTabla(table, "C.c.p.   " + clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(objAdministrador.Abreviatura + " " + objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + ", " + objAdministrador.Cargo)), 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);

            AgregaDatosTabla(table, "Archivo.", 0, Dictionary.FUENTE_NORMAL, Dictionary.GRAY, 6);
            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, 6);

            document.Add(table);

            //  document.Add(Agrega_Parrafo(clsFuncionesGral.ConvertMinus("www.inapesca.gob.mx"), Dictionary.FUENTE_NORMAL, Dictionary.GRAY, Dictionary.ALIGN_CENTER, 6));
            document.Close();
            pdfw.Close();


            //  Process.Start(path);
            string raiz = "";

            /*if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "42"))
            {
                raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\" + Year + "\\" + MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Autoriza , true) + "\\" + Dictionary.COMISIONES + "\\" + poComision.Archivo.Replace(".pdf", "");
            }
            else
            {
                raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\" + Year + "\\" + MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Comisionado, true) + "\\" + Dictionary.COMISIONES + "\\" + poComision.Archivo.Replace(".pdf", "");
            }*/

            raiz = HttpContext.Current.Server.MapPath("..") + "\\" + poComision.Ruta;

            DonwloadFile(raiz, archivo);
        }


        public static void Genera_ReferenciaPago(string sFolioRef, string sRefBan, string sImporte, string sFecha, string sArchivo, List<Entidad> ListConceptos)
        {

            //Comentar el using System.Drawing;  o referenciar la libreria del itextSharp


            iTextSharp.text.Font Letra12Negrita = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            iTextSharp.text.Font Letra12Normal = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL));
            iTextSharp.text.Font Letra10Negrita = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
            iTextSharp.text.Font Letra10Normal = new Font(FontFactory.GetFont("Arial", 10, Font.NORMAL));
            iTextSharp.text.Font Letra09Negrita = new Font(FontFactory.GetFont("Arial", 9, Font.BOLD));
            iTextSharp.text.Font Letra09Normal = new Font(FontFactory.GetFont("Arial", 9, Font.NORMAL));
            iTextSharp.text.Font Letra08Negrita = new Font(FontFactory.GetFont("Arial", 8, Font.BOLD));
            iTextSharp.text.Font Letra08Normal = new Font(FontFactory.GetFont("Arial", 8, Font.NORMAL));

            Document document = new Document(PageSize.LETTER);//, 23, 23, 36, 23);
            //Agregamos Propiedades al documento
            document.AddTitle(clsFuncionesGral.ConvertMayus("PDF COMISION SMAF"));
            document.AddCreator(clsFuncionesGral.ConvertMayus("Ing. Juan Antonio López López"));

            //Valida_Carpeta();

            string archivo = sRefBan + ".pdf";

            string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\REFERENCIAS\\";

            if (!Directory.Exists(raiz)) Directory.CreateDirectory(raiz);

            string path = raiz + "/" + archivo;

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            PdfWriter pdfWrite = PdfWriter.GetInstance(document, file);

            document.Open();


            // PDF REFERENCIA //

            float[] celdamedidas = new float[3];
            celdamedidas[0] = 1.00f;
            celdamedidas[1] = 2.00f;
            celdamedidas[2] = 1.00f;


            PdfPTable table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);
            //Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCAOLD, 250, 100, 0);
            Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 250, 100, 0);
            document.Add(table);
            table = null;

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            //Agrega_Imagen_Celda(table, Dictionary.INAPESCA, 140, 80, 0);

            PdfPTable subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("\n INSTITUTO NACIONAL DE PESCA Y ACUACULTURA"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tGrande);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("solicitud de bienes y/o servicios"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);
            //Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 140, 80, 0);
            //Agrega_Imagen_Celda(table, Dictionary.AGRICULTURA, 140, 80, 0);

            document.Add(table);
            table = null;
            subtabla = null;


            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_RIGHT);
            PdfPCell celda2 = new PdfPCell(Agrega_Parrafo("SOLICITUD DE REINTEGRO", Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano));
            celda2.Border = 0;
            celda2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(celda2);
            document.Add(table);
            table = null;
            celda2 = null;

            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            celda2 = new PdfPCell(Agrega_Parrafo("DATOS", Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tGrande));
            celda2.HorizontalAlignment = 1;
            celda2.BackgroundColor = BaseColor.GRAY;
            table.AddCell(celda2);
            document.Add(table);

            table = null;
            celda2 = null;


            Phrase phrase = new Phrase();
            //Folio REFERENCIA
            phrase.Add(new Chunk("\n", Letra10Negrita));

            phrase.Add(new Chunk("Folio de Referencia: ", Letra10Negrita));
            phrase.Add(new Chunk(sFolioRef + "\n", Letra10Normal));
            // REFERENCIA BANCARIA
            phrase.Add(new Chunk("Referencia Bancaria: ", Letra10Negrita));
            phrase.Add(new Chunk(sRefBan + "\n", Letra10Normal));

            // CONVENIO
            phrase.Add(new Chunk("Convenio CIE: ", Letra10Negrita));
            //phrase.Add(new Chunk("1422731" + "\n", Letra10Normal));
            phrase.Add(new Chunk("1546546" + "\n", Letra10Normal));

            // IMPORTE
            double importeconvert = Convert.ToDouble(sImporte);
            phrase.Add(new Chunk("Importe: ", Letra10Negrita));
            phrase.Add(new Chunk(importeconvert.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")) + "\n", Letra10Normal));

            // FECHA VENCIMIENTO
            phrase.Add(new Chunk("Fecha de Vencimiento: ", Letra10Negrita));
            phrase.Add(new Chunk(Convert.ToDateTime(sFecha).ToString("dd/MM/yyyy") + "\n", Letra10Normal));

            // FECHA GENERACION
            phrase.Add(new Chunk("Fecha de Generación: ", Letra10Negrita));
            phrase.Add(new Chunk(Convert.ToDateTime(lsHoy).ToString("dd/MM/yyyy") + "\n", Letra10Normal)); //AGREGAR REFERENCIA GLOBALITA

            // MOTIVO
            phrase.Add(new Chunk("Motivo: ", Letra10Negrita));
            phrase.Add(new Chunk("REINTEGRO DE LA COMISION " + sArchivo + "\n \n", Letra10Normal));

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_RIGHT);
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            table.AddCell(celda2);

            document.Add(table);
            table = null;
            celda2 = null;



            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************


            celdamedidas = new float[3];
            celdamedidas[0] = 1f;
            celdamedidas[1] = 2f;
            celdamedidas[2] = 3f;



            table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, celdamedidas);

            phrase = new Phrase();
            phrase.Add(new Chunk("DETALLES", Letra10Negrita));

            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.Colspan = 3;
            celda2.Padding = 7;
            celda2.BackgroundColor = BaseColor.GRAY;
            table.AddCell(celda2);
            celda2 = null;
            //********************************

            phrase = new Phrase();
            phrase.Add(new Chunk("Núm", Letra09Negrita));
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(celda2);
            celda2 = null;
            //********************************
            phrase = new Phrase();
            phrase.Add(new Chunk("CONCEPTO", Letra09Negrita));
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(celda2);
            celda2 = null;
            //********************************
            phrase = new Phrase();
            phrase.Add(new Chunk("IMPORTE", Letra09Negrita));
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(celda2);
            celda2 = null;



            PdfPTable Subtabla = new PdfPTable(Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, celdamedidas));


            //List<Entidad> ListConceptos = new List<Entidad>(); //AQUI HAY QUE CARGAR LA LISTA DE CONCEPTOS E IMPORTE

            ////borrar esta parte
            //Entidad objEnt = new Entidad();

            //objEnt.Codigo = "viaticos";
            //objEnt.Descripcion = "$11.5";
            //ListConceptos.Add(objEnt);

            //objEnt = new Entidad();

            //objEnt.Codigo = "partida peaje";
            //objEnt.Descripcion = "$1200.00";
            //ListConceptos.Add(objEnt);

            ////borrar esta parte

            int Contador = 0;

            foreach (Entidad X in ListConceptos)
            {
                Contador++;
                phrase = new Phrase();
                phrase.Add(new Chunk(Contador.ToString(), Letra10Normal));
                celda2 = new PdfPCell(phrase);
                celda2.HorizontalAlignment = 1;
                celda2.VerticalAlignment = Element.ALIGN_CENTER;

                Subtabla.AddCell(celda2);
                celda2 = null;
                //********************************
                phrase = new Phrase();
                phrase.Add(new Chunk(X.Codigo, Letra08Normal));
                celda2 = new PdfPCell(phrase);
                celda2.HorizontalAlignment = 1;
                celda2.VerticalAlignment = Element.ALIGN_CENTER;
                Subtabla.AddCell(celda2);
                celda2 = null;
                //********************************

                double xDescripcion = Convert.ToDouble(X.Descripcion);
                phrase = new Phrase();
                phrase.Add(new Chunk(xDescripcion.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Normal));
                celda2 = new PdfPCell(phrase);
                celda2.HorizontalAlignment = 1;
                celda2.VerticalAlignment = Element.ALIGN_CENTER;
                Subtabla.AddCell(celda2);
                celda2 = null;
                //********************************

            }



            celda2 = new PdfPCell(Subtabla);
            celda2.HorizontalAlignment = 1;
            celda2.Colspan = 3;
            //celda2.MinimumHeight = 100f;
            celda2.FixedHeight = 250f;
            table.AddCell(celda2);
            celda2 = null;
            //********************************


            document.Add(table);
            table = null;



            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null;  //SALTO DE LINEA
            //******************************
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************



            celdamedidas = new float[1];
            celdamedidas[0] = 2.00f;


            subtabla = null;

            table = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_LEFT, celdamedidas);

            string imageFilePath2 = "";

            imageFilePath2 = HttpContext.Current.Server.MapPath("..") + "\\Resources\\logoban.png";

            iTextSharp.text.Image objImageFondo2 = iTextSharp.text.Image.GetInstance(imageFilePath2);
            objImageFondo2.ScaleToFit(220, 100);
            objImageFondo2.Alignment = iTextSharp.text.Image.UNDERLYING;



            PdfPCell CeldaPdf = new PdfPCell(objImageFondo2);
            CeldaPdf.Border = 0;
            CeldaPdf.VerticalAlignment = Element.ALIGN_MIDDLE;
            // CeldaPdf.BorderColor = BaseColor.BLACK;
            table.AddCell(CeldaPdf);



            //Agrega_Imagen_Celda(table, "logoban", 280, 160, 0);

            document.Add(table);
            table = null;



            //PIE DE PAGINA
            PdfPTable tab = new PdfPTable(1);
            phrase = new Phrase();
            phrase.Add(new Chunk("Av. México N° 190, Col. Del Carmen, Delegación Coyoacan,  C.P. 04100, Ciudad de México \n Teléfono (55) 38719553 \n www.inapesca.gob.mx", new Font(FontFactory.GetFont("Arial", 8, Font.UNDEFINED))));
            PdfPCell cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tab.TotalWidth = 300F;
            tab.AddCell(cell);

            tab.WriteSelectedRows(0, -1, 150, 60, pdfWrite.DirectContent);

            tab = null;
            cell = null;

            document.Close();

            pdfWrite.Close();

            //DESCARGAR ARCHIVO 

            //Process.Start(path);
            archivo = "";
            archivo = sRefBan + ".pdf";
            raiz = "";
            raiz = HttpContext.Current.Server.MapPath("..") + "/PDF/" + archivo;



            //DonwloadFile(raiz, archivo);      
        }

        public static void Genera_ReferenciaPago1(string sFolioRef, string sRefBan, string sImporte, string sFecha, string sArchivo, List<Entidad> ListConceptos)
        {

            //Comentar el using System.Drawing;  o referenciar la libreria del itextSharp


            iTextSharp.text.Font Letra12Negrita = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            iTextSharp.text.Font Letra12Normal = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL));
            iTextSharp.text.Font Letra10Negrita = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
            iTextSharp.text.Font Letra10Normal = new Font(FontFactory.GetFont("Arial", 10, Font.NORMAL));
            iTextSharp.text.Font Letra09Negrita = new Font(FontFactory.GetFont("Arial", 9, Font.BOLD));
            iTextSharp.text.Font Letra09Normal = new Font(FontFactory.GetFont("Arial", 9, Font.NORMAL));
            iTextSharp.text.Font Letra08Negrita = new Font(FontFactory.GetFont("Arial", 8, Font.BOLD));
            iTextSharp.text.Font Letra08Normal = new Font(FontFactory.GetFont("Arial", 8, Font.NORMAL));

            Document document = new Document(PageSize.LETTER);//, 23, 23, 36, 23);
            //Agregamos Propiedades al documento
            document.AddTitle(clsFuncionesGral.ConvertMayus("PDF COMISION SMAF"));
            document.AddCreator(clsFuncionesGral.ConvertMayus("Ing. Juan Antonio López López"));

            //Valida_Carpeta();

            string archivo = sRefBan + ".pdf";

            string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\REFERENCIAS\\";

            if (!Directory.Exists(raiz)) Directory.CreateDirectory(raiz);

            string path = raiz + "/" + archivo;

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            PdfWriter pdfWrite = PdfWriter.GetInstance(document, file);

            document.Open();


            // PDF REFERENCIA //

            float[] celdamedidas = new float[3];
            celdamedidas[0] = 1.00f;
            celdamedidas[1] = 2.00f;
            celdamedidas[2] = 1.00f;


            PdfPTable table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);
            //Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCAOLD, 250, 100, 0);
            Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 250, 100, 0);
            document.Add(table);
            table = null;

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            //Agrega_Imagen_Celda(table, Dictionary.INAPESCA, 140, 80, 0);

            PdfPTable subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("\n INSTITUTO NACIONAL DE PESCA Y ACUACULTURA"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tGrande);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("solicitud de bienes y/o servicios"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);
            //Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 140, 80, 0);
            //Agrega_Imagen_Celda(table, Dictionary.AGRICULTURA, 140, 80, 0);

            document.Add(table);
            table = null;
            subtabla = null;


            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_RIGHT);
            PdfPCell celda2 = new PdfPCell(Agrega_Parrafo("SOLICITUD DE REINTEGRO", Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano));
            celda2.Border = 0;
            celda2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(celda2);
            document.Add(table);
            table = null;
            celda2 = null;

            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            celda2 = new PdfPCell(Agrega_Parrafo("DATOS", Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tGrande));
            celda2.HorizontalAlignment = 1;
            celda2.BackgroundColor = BaseColor.GRAY;
            table.AddCell(celda2);
            document.Add(table);

            table = null;
            celda2 = null;


            Phrase phrase = new Phrase();
            //Folio REFERENCIA
            phrase.Add(new Chunk("\n", Letra10Negrita));

            phrase.Add(new Chunk("Folio de Referencia: ", Letra10Negrita));
            phrase.Add(new Chunk(sFolioRef + "\n", Letra10Normal));
            // REFERENCIA BANCARIA
            phrase.Add(new Chunk("Referencia Bancaria: ", Letra10Negrita));
            phrase.Add(new Chunk(sRefBan + "\n", Letra10Normal));

            // CONVENIO
            phrase.Add(new Chunk("Convenio CIE: ", Letra10Negrita));
            //phrase.Add(new Chunk("1422731" + "\n", Letra10Normal));
            phrase.Add(new Chunk("1546546" + "\n", Letra10Normal));

            // IMPORTE
            double importeconvert = Convert.ToDouble(sImporte);
            phrase.Add(new Chunk("Importe: ", Letra10Negrita));
            phrase.Add(new Chunk(importeconvert.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")) + "\n", Letra10Normal));

            // FECHA VENCIMIENTO
            phrase.Add(new Chunk("Fecha de Vencimiento: ", Letra10Negrita));
            phrase.Add(new Chunk(Convert.ToDateTime(sFecha).ToString("dd/MM/yyyy") + "\n", Letra10Normal));

            // FECHA GENERACION
            phrase.Add(new Chunk("Fecha de Generación: ", Letra10Negrita));
            phrase.Add(new Chunk(Convert.ToDateTime(lsHoy).ToString("dd/MM/yyyy") + "\n", Letra10Normal)); //AGREGAR REFERENCIA GLOBALITA

            // MOTIVO
            phrase.Add(new Chunk("Motivo: ", Letra10Negrita));
            phrase.Add(new Chunk("REINTEGRO DE LA COMISION " + sArchivo + "\n \n", Letra10Normal));

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_RIGHT);
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            table.AddCell(celda2);

            document.Add(table);
            table = null;
            celda2 = null;



            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************


            celdamedidas = new float[3];
            celdamedidas[0] = 1f;
            celdamedidas[1] = 2f;
            celdamedidas[2] = 3f;



            table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, celdamedidas);

            phrase = new Phrase();
            phrase.Add(new Chunk("DETALLES", Letra10Negrita));

            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.Colspan = 3;
            celda2.Padding = 7;
            celda2.BackgroundColor = BaseColor.GRAY;
            table.AddCell(celda2);
            celda2 = null;
            //********************************

            phrase = new Phrase();
            phrase.Add(new Chunk("Núm", Letra09Negrita));
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(celda2);
            celda2 = null;
            //********************************
            phrase = new Phrase();
            phrase.Add(new Chunk("CONCEPTO", Letra09Negrita));
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(celda2);
            celda2 = null;
            //********************************
            phrase = new Phrase();
            phrase.Add(new Chunk("IMPORTE", Letra09Negrita));
            celda2 = new PdfPCell(phrase);
            celda2.HorizontalAlignment = 1;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(celda2);
            celda2 = null;



            PdfPTable Subtabla = new PdfPTable(Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, celdamedidas));


            //List<Entidad> ListConceptos = new List<Entidad>(); //AQUI HAY QUE CARGAR LA LISTA DE CONCEPTOS E IMPORTE

            ////borrar esta parte
            //Entidad objEnt = new Entidad();

            //objEnt.Codigo = "viaticos";
            //objEnt.Descripcion = "$11.5";
            //ListConceptos.Add(objEnt);

            //objEnt = new Entidad();

            //objEnt.Codigo = "partida peaje";
            //objEnt.Descripcion = "$1200.00";
            //ListConceptos.Add(objEnt);

            ////borrar esta parte

            int Contador = 0;

            foreach (Entidad X in ListConceptos)
            {
                Contador++;
                phrase = new Phrase();
                phrase.Add(new Chunk(Contador.ToString(), Letra10Normal));
                celda2 = new PdfPCell(phrase);
                celda2.HorizontalAlignment = 1;
                celda2.VerticalAlignment = Element.ALIGN_CENTER;

                Subtabla.AddCell(celda2);
                celda2 = null;
                //********************************
                phrase = new Phrase();
                phrase.Add(new Chunk(X.Codigo, Letra08Normal));
                celda2 = new PdfPCell(phrase);
                celda2.HorizontalAlignment = 1;
                celda2.VerticalAlignment = Element.ALIGN_CENTER;
                Subtabla.AddCell(celda2);
                celda2 = null;
                //********************************

                double xDescripcion = Convert.ToDouble(X.Descripcion);
                phrase = new Phrase();
                phrase.Add(new Chunk(xDescripcion.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Normal));
                celda2 = new PdfPCell(phrase);
                celda2.HorizontalAlignment = 1;
                celda2.VerticalAlignment = Element.ALIGN_CENTER;
                Subtabla.AddCell(celda2);
                celda2 = null;
                //********************************

            }



            celda2 = new PdfPCell(Subtabla);
            celda2.HorizontalAlignment = 1;
            celda2.Colspan = 3;
            //celda2.MinimumHeight = 100f;
            celda2.FixedHeight = 250f;
            table.AddCell(celda2);
            celda2 = null;
            //********************************


            document.Add(table);
            table = null;



            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null;  //SALTO DE LINEA
            //******************************
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************



            celdamedidas = new float[1];
            celdamedidas[0] = 2.00f;


            subtabla = null;

            table = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_LEFT, celdamedidas);

            string imageFilePath2 = "";

            imageFilePath2 = HttpContext.Current.Server.MapPath("..") + "\\Resources\\logoban.png";

            iTextSharp.text.Image objImageFondo2 = iTextSharp.text.Image.GetInstance(imageFilePath2);
            objImageFondo2.ScaleToFit(220, 100);
            objImageFondo2.Alignment = iTextSharp.text.Image.UNDERLYING;



            PdfPCell CeldaPdf = new PdfPCell(objImageFondo2);
            CeldaPdf.Border = 0;
            CeldaPdf.VerticalAlignment = Element.ALIGN_MIDDLE;
            // CeldaPdf.BorderColor = BaseColor.BLACK;
            table.AddCell(CeldaPdf);



            //Agrega_Imagen_Celda(table, "logoban", 280, 160, 0);

            document.Add(table);
            table = null;



            //PIE DE PAGINA
            PdfPTable tab = new PdfPTable(1);
            phrase = new Phrase();
            phrase.Add(new Chunk("Av. México N° 190, Col. Del Carmen, Delegación Coyoacan,  C.P. 04100, Ciudad de México \n Teléfono (55) 38719553 \n www.inapesca.gob.mx", new Font(FontFactory.GetFont("Arial", 8, Font.UNDEFINED))));
            PdfPCell cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tab.TotalWidth = 300F;
            tab.AddCell(cell);

            tab.WriteSelectedRows(0, -1, 150, 60, pdfWrite.DirectContent);

            tab = null;
            cell = null;

            document.Close();

            pdfWrite.Close();

            //DESCARGAR ARCHIVO 

            //Process.Start(path);
            archivo = "";
            archivo = sRefBan + ".pdf";
            raiz = "";
            raiz = HttpContext.Current.Server.MapPath("..") + "/PDF/" + archivo;



            //DonwloadFile(raiz, archivo);      
        }

        //public static void SaltoDeLinea()
        //{
        //    PdfPTable table2 = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
        //    AgregaDatosTabla(table2, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
        //    document.Add(table2);
        //    table2 = null;
        //    //salt
        //}

        public static void Genera_Reporte_Comparativo()
        {

        }


        public static void Genera_MinistracionSmaf(Comision poComision, string psFolio, double totalDev, double totalAnt)
        {
            iTextSharp.text.Font Letra12Negrita = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            iTextSharp.text.Font Letra12Normal = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL));
            iTextSharp.text.Font Letra10Negrita = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
            iTextSharp.text.Font Letra10Normal = new Font(FontFactory.GetFont("Arial", 10, Font.NORMAL));
            iTextSharp.text.Font Letra09Negrita = new Font(FontFactory.GetFont("Arial", 9, Font.BOLD));
            iTextSharp.text.Font Letra09Normal = new Font(FontFactory.GetFont("Arial", 9, Font.NORMAL));
            iTextSharp.text.Font Letra08Negrita = new Font(FontFactory.GetFont("Arial", 8, Font.BOLD));
            iTextSharp.text.Font Letra08Normal = new Font(FontFactory.GetFont("Arial", 8, Font.NORMAL));
            iTextSharp.text.Font Letra07Negrita = new Font(FontFactory.GetFont("Arial", 7, Font.BOLD));
            iTextSharp.text.Font Letra07Normal = new Font(FontFactory.GetFont("Arial", 7, Font.NORMAL));
            iTextSharp.text.Font Letra06Negrita = new Font(FontFactory.GetFont("Arial", 6, Font.BOLD));
            iTextSharp.text.Font Letra06Normal = new Font(FontFactory.GetFont("Arial", 6, Font.NORMAL));
            iTextSharp.text.Font Letra05Negrita = new Font(FontFactory.GetFont("Arial", 5, Font.BOLD));
            iTextSharp.text.Font Letra05Normal = new Font(FontFactory.GetFont("Arial", 5, Font.NORMAL));

            double TotalCombAmp = 0;
            double TotalPeajeAmp = 0;
            double TotalPasajeAmp = 0;
            double TotalViaticos = 0;
            double TotalCombustible = 0;
            double TotalPasaje = 0;
            double TotalPeaje = 0;
            double TotalTotal = 0;
            double TotalViaticosRurales = 0;
            double TotalViaticosNavegados = 0;

            float[] medidaCeldas;

            Year = poComision.Periodo;

            Document document = new Document(PageSize.LETTER);//, 23, 23, 36, 23);
            //Agregamos Propiedades al documento
            document.AddTitle(clsFuncionesGral.ConvertMayus("PDF COMISION SMAF"));
            document.AddCreator(clsFuncionesGral.ConvertMayus("Ing. Juan Antonio López López"));

            //string archivo = "prueba.pdf";

            //string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\";

            //string path = raiz + archivo;
            string raiz = HttpContext.Current.Server.MapPath("~") + "\\PDF\\";

          /*Se comenta el 11/09/22 para nueva forma de validar carpeta
           * 
           * Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Comisionado, true), poComision.Archivo.Replace(".pdf", ""));
           * 
           */

            //Inicia nueva forma de validar carpeta para ministración//
            Entidad oDireccionTipo = MngDatosDependencia.Obtiene_Tipo_Region(poComision.Ubicacion_Comisionado);

            if (poComision.Territorio == Dictionary.Internacional)
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(Dictionary.DG, true), poComision.Archivo.Replace(".pdf", ""));
            }
            else if ((poComision.Ubicacion_Autoriza == Dictionary.DGAA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAJ) | (poComision.Ubicacion_Autoriza == Dictionary.DG) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIA) | (poComision.Ubicacion_Autoriza == Dictionary.DGAIPP))
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Autoriza, true), poComision.Archivo.Replace(".pdf", ""));
            }
            else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(oDireccionTipo.Codigo, true), poComision.Archivo.Replace(".pdf", ""));

            }
            else
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Comisionado, true), poComision.Archivo.Replace(".pdf", ""));
            }

            oDireccionTipo = null;
            
            //termina nueva forma de validar carpeta para ministración  //
          
           // string Ruta = raiz + Year + "/" + MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Comisionado, true) + "/" + Dictionary.COMISIONES + "/" + poComision.Archivo.Replace(".pdf", "");
           
            string path = Dictionary.CADENA_NULA;
            string archivo = "Ministracion - " + poComision.Archivo;
            path = Ruta + "\\" + archivo;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Busca Lista AMPLIACIONES
            List<Entidad> ListArchivosAmp = new List<Entidad>();
            if (poComision.Archivo_Ampliacion != null)
            {
                string[] psCadena;
                psCadena = poComision.Archivo_Ampliacion.Split(new Char[] { '|' });
                string[] psCadena2;
                psCadena2 = poComision.Oficio_Ampliacion.Split(new Char[] { '|' });


                if (poComision.Archivo_Ampliacion != "")
                {
                    for (int i = 0; i < psCadena.Length; i++)
                    {
                        Entidad oEntidad = new Entidad();
                        oEntidad.Codigo = psCadena2[i];
                        oEntidad.Descripcion = psCadena[i];
                        ListArchivosAmp.Add(oEntidad);
                    }
                }
            }
            // Termina lista AMpliaciones


            double total = clsFuncionesGral.Convert_Double(Dictionary.NUMERO_CERO);
            int consecutivo = 1;

            //Creamos el documento con sus caracteristicas

            //  Fuente_Normal();

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            //Apertura del documento
            document.Open();
            //CREA TABLA ENCABEZADO
            PdfPTable table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

            //DATOS ENCABEZADO
            //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
            //Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 160, 100, 0);
            //Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCAOLD, 400, 100, 0);
            Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 400, 100, 0);
            document.Add(table);
            table = null;

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            PdfPTable subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura y desarrollo rural"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca y acuacultura"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("relación de gastos de la operación "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Detalle de la comisión "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            Agrega_TablainCelda_Drawing(table, 0, subtabla);
            //Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
            subtabla = null;
            document.Add(table);

            Phrase phrase = new Phrase();
            PdfPCell celda = new PdfPCell();
            int ContObj = 0;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);

            medidaCeldas = new float[6];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 1.5f;
            medidaCeldas[2] = 1f;
            medidaCeldas[3] = 1f;
            medidaCeldas[4] = 0.5f;
            medidaCeldas[5] = 1f;
            PdfPTable tablaAmp = Genera_Tabla_TamañoPersonalizado(6, 100, Dictionary.ALIGN_CENTER, medidaCeldas);
            string sObjetivo = "";
            string sArchivos = "";
            string sNoOficios = "";

            if (ListArchivosAmp.Count != 0)
            {
                foreach (Entidad r in ListArchivosAmp)
                {
                    Comision DetalleComisionAmp = new Comision();

                    DetalleComisionAmp = MngNegocioComision.Obten_Detalle(r.Descripcion, poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Estatus);

                    if (DetalleComisionAmp.Archivo != null)
                    {
                        sArchivos += " | " + DetalleComisionAmp.Archivo.Replace(".pdf", "").Replace(("-DGAIPP-" + DetalleComisionAmp.Oficio), ("-DGAIPP-" + DetalleComisionAmp.Oficio_DGAIPP));
                        sNoOficios += " | " + DetalleComisionAmp.Oficio;
                        if ((DetalleComisionAmp.Zona_Comercial == "0") | (DetalleComisionAmp.Zona_Comercial == "2") | (DetalleComisionAmp.Zona_Comercial == "4") | (DetalleComisionAmp.Zona_Comercial == "6") | (DetalleComisionAmp.Zona_Comercial == "8") | (DetalleComisionAmp.Zona_Comercial == "10") | (DetalleComisionAmp.Zona_Comercial == "12") | (DetalleComisionAmp.Zona_Comercial == "14") | (DetalleComisionAmp.Zona_Comercial == "15") | (DetalleComisionAmp.Zona_Comercial == "19"))
                        {
                            consecutivo += 1;
                            sObjetivo += consecutivo.ToString() + ". " + DetalleComisionAmp.Objetivo + " | AMPLIACIÓN DE LA COMISION CON NUMERO " + poComision.Archivo.Replace(".pdf", "") + "\n";

                            DateTime FechaI = Convert.ToDateTime(DetalleComisionAmp.Fecha_Inicio);
                            DateTime FechaF = Convert.ToDateTime(DetalleComisionAmp.Fecha_Final);
                            string FechaComision = FechaI.ToString("dd-MM-yyyy") + " al " + FechaF.ToString("dd-MM-yyyy");
                            double TViaticos = Convert.ToDouble(DetalleComisionAmp.Total_Viaticos);
                            double TarifaDias = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa(DetalleComisionAmp.Zona_Comercial));

                            TotalViaticos = TotalViaticos + Convert.ToDouble(DetalleComisionAmp.Total_Viaticos) + Convert.ToDouble(DetalleComisionAmp.Singladuras);
                            TotalCombAmp = TotalCombAmp + Convert.ToDouble(DetalleComisionAmp.Combustible_Efectivo);
                            TotalPasajeAmp = TotalPasajeAmp + Convert.ToDouble(DetalleComisionAmp.Pasaje);
                            TotalPeajeAmp = TotalPeajeAmp + Convert.ToDouble(DetalleComisionAmp.Peaje);
                            
                            tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                            tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, DetalleComisionAmp.Lugar, true, false, true));
                            tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                            tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaDias.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                            tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, DetalleComisionAmp.Dias_Reales, true, false, true));
                            tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, TViaticos.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));

                        }

                        if ((DetalleComisionAmp.Zona_Comercial == "16") | (DetalleComisionAmp.Zona_Comercial == "17") | (DetalleComisionAmp.Zona_Comercial == "18") | (DetalleComisionAmp.Zona_Comercial == "20") | (DetalleComisionAmp.Zona_Comercial == "21") | (DetalleComisionAmp.Zona_Comercial == "22") | (DetalleComisionAmp.Zona_Comercial == "23"))
                        {
                            double TarifaComercial = 0;
                            double TarifaRural = 0;
                            double TarifaNavegable = 0;
                            double Tarifa50km = 0;

                            double tViatComercial = 0;
                            double tViatRural = 0;
                            double tViatNavegados = 0;
                            double tViat50km = 0;

                            switch (DetalleComisionAmp.Zona_Comercial)
                            {

                                case "16":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                                    break;
                                case "17":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                                    break;
                                case "18":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    TarifaNavegable = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("15"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatNavegados = Convert.ToDouble(DetalleComisionAmp.Dias_Navegados) * TarifaNavegable;
                                    TotalViaticosNavegados = TotalViaticosNavegados + tViatNavegados;
                                    break;
                                case "20":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    break;
                                case "21":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    break;
                                case "22":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                                    break;
                                case "23":
                                    TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                                    TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                                    Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                                    tViatComercial = Convert.ToDouble(DetalleComisionAmp.Dias_Comercial) * TarifaComercial;
                                    tViatRural = Convert.ToDouble(DetalleComisionAmp.Dias_Rural) * TarifaRural;
                                    tViat50km = Convert.ToDouble(DetalleComisionAmp.Dias_50) * Tarifa50km;
                                    TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                                    break;
                            }

                            DateTime FechaI = Convert.ToDateTime(DetalleComisionAmp.Fecha_Inicio);
                            DateTime FechaF = Convert.ToDateTime(DetalleComisionAmp.Fecha_Final);
                            string FechaComision = FechaI.ToString("dd-MM-yyyy") + " al " + FechaF.ToString("dd-MM-yyyy");

                            if (tViatComercial != 0)
                            {
                                consecutivo += 1;
                                sObjetivo += consecutivo.ToString() + ". " + DetalleComisionAmp.Objetivo + " | AMPLIACIÓN DE LA COMISION CON NUMERO " + poComision.Archivo.Replace(".pdf", "") + "\n";
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, DetalleComisionAmp.Lugar, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaComercial.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, DetalleComisionAmp.Dias_Comercial, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViatComercial.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));

                            }
                            if (tViatRural != 0)
                            {
                                consecutivo += 1;
                                sObjetivo += consecutivo.ToString() + ". " + DetalleComisionAmp.Objetivo + " | AMPLIACIÓN DE LA COMISION CON NUMERO " + poComision.Archivo.Replace(".pdf", "") + "\n";
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, DetalleComisionAmp.Lugar, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaRural.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, DetalleComisionAmp.Dias_Rural, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViatRural.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                            }
                            if (tViatNavegados != 0)
                            {
                                consecutivo += 1;
                                sObjetivo += consecutivo.ToString() + ". " + DetalleComisionAmp.Objetivo + " | AMPLIACIÓN DE LA COMISION CON NUMERO " + poComision.Archivo.Replace(".pdf", "") + "\n";
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, DetalleComisionAmp.Lugar, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaNavegable.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, DetalleComisionAmp.Dias_Rural, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViatNavegados.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                            }
                            if (tViat50km != 0)
                            {
                                consecutivo += 1;
                                sObjetivo += consecutivo.ToString() + ". " + DetalleComisionAmp.Objetivo + " | AMPLIACIÓN DE LA COMISION CON NUMERO " + poComision.Archivo.Replace(".pdf", "") + "\n";
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, DetalleComisionAmp.Lugar, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, Tarifa50km.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, DetalleComisionAmp.Dias_50, true, false, true));
                                tablaAmp.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViat50km.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                            }
                            TotalViaticos = tViatComercial + tViatRural + tViatNavegados + tViat50km;

                            TotalCombAmp = TotalCombAmp + Convert.ToDouble(DetalleComisionAmp.Combustible_Efectivo);
                            TotalPasajeAmp = TotalPasajeAmp + Convert.ToDouble(DetalleComisionAmp.Pasaje);
                            TotalPeajeAmp = TotalPeajeAmp + Convert.ToDouble(DetalleComisionAmp.Peaje);
                        }

                    }
                }

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Nombre del servidor publico comisionado:"), Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado) + "\n"), Letra08Negrita));

                // objetivo

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Objeto de la comisión:\n"), Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("1." + poComision.Objetivo + "\n" + sObjetivo), Letra08Normal));//AGREGA AMPLIACIONES
                // Actividades

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Principales actividades desarrolladas:\n"), Letra08Negrita));
                phrase.Add(new Chunk("Se anexa informe de Comisión\n", Letra08Normal));

                //EVALUACION
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Evaluación\n"), Letra08Negrita));

                phrase.Add(new Chunk("Comprobación de Comision Oficio Número :" + poComision.Archivo.Replace(".pdf", "").Replace(("-DGAIPP-" + poComision.Oficio), ("-DGAIPP-" + poComision.Oficio_DGAIPP)) + sArchivos + "\n", Letra08Normal));//AGREGA AMPLIACIONES

                if (poComision.Oficio_DGAIPP != "0")
                {
                    phrase.Add(new Chunk("Comprobación de consecutivo de viaticos Número: " + poComision.Oficio + sNoOficios + "\n", Letra08Negrita));//AGREGA AMPLIACIONES
                }

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Folio Comprobacion SMAF.WEB:" + poComision.Archivo.Replace(".pdf", "") + "/" + psFolio + "\n"), Letra08Normal));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Fecha cierre comprobacion SMaf.web :") + MngNegocioComprobacion.Obtiene_Fecha_Comprobacion(psFolio, poComision.Archivo), Letra08Normal));

                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                celda.Border = 0;
                //celda.FixedHeight=250f; // verificar despues codigo
                table.AddCell(celda);


            }

            else
            {

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Nombre del servidor publico comisionado:"), Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado) + "\n"), Letra08Negrita));

                // objetivo

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Objeto de la comisión:\n"), Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(poComision.Objetivo + "\n"), Letra08Normal));
                // Actividades

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Principales actividades desarrolladas:\n"), Letra08Negrita));
                phrase.Add(new Chunk("Se anexa informe de Comisión\n", Letra08Normal));

                //EVALUACION
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Evaluación\n"), Letra08Negrita));

                phrase.Add(new Chunk("Comprobación de Comision Oficio Número :" + poComision.Archivo.Replace(".pdf", "").Replace(("-DGAIPP-" + poComision.Oficio), ("-DGAIPP-" + poComision.Oficio_DGAIPP)) + "\n", Letra08Normal));

                if (poComision.Oficio_DGAIPP != "0")
                {
                    phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Comprobación de consecutivo de viaticos Número: " + poComision.Oficio + "\n"), Letra08Negrita));
                }

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Folio Comprobacion SMAF.WEB:" + poComision.Archivo.Replace(".pdf", "") + "/" + psFolio + "\n"), Letra08Normal));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("Fecha cierre comprobacion SMaf.web :") + MngNegocioComprobacion.Obtiene_Fecha_Comprobacion(psFolio, poComision.Archivo), Letra08Normal));

                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                celda.Border = 0;
                //celda.FixedHeight=250f; // verificar despues codigo
                table.AddCell(celda);



            }

            document.Add(table);
            table = null;
            celda = null;

            medidaCeldas = new float[3];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 1f;
            medidaCeldas[2] = 1f;

            table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

            table.AddCell(AgregaDatoCeldasNew(Letra08Negrita, "DOCUMENTOS DE COMPROBACIÓN", false, true));

            table.AddCell(AgregaDatoCeldasNew(Letra06Normal, " ( ) Programa de Trabajo \n ( ) Acta Circunstanciada ", false, true));

            table.AddCell(AgregaDatoCeldasNew(Letra06Normal, " ( ) Diploma o Constancia de Participación \n (X) Otros ", false, true));

            document.Add(table);
            table = null;
            celda = null;

            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************

            // ENCABEZADOS TABLA DETALLES//
            medidaCeldas = new float[6];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 1.5f;
            medidaCeldas[2] = 1f;
            medidaCeldas[3] = 1f;
            medidaCeldas[4] = 0.5f;
            medidaCeldas[5] = 1f;

            table = Genera_Tabla_TamañoPersonalizado(6, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NUM", true, false, false, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "LUGAR DE LA COMISIÓN", true, false, false, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "PERIODO DE LA COMISIÓN", true, false, false, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CUOTA DIARIA", true, false, false, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "DIAS", true, false, false, true));

            phrase = new Phrase();


            if (totalDev != 0 && totalAnt != 0)
            {
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CALCULADO/OTORGADO", true, false, false, true));
            }
            else
            {
                if (poComision.Forma_Pago_Viaticos == Dictionary.VIATICOS_DEVENGADOS)
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE CALCULADO", true, false, false, true));
                }
                else
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE OTORGADO", true, false, false, true));
                }
            }


            ////////////////TERMINA ENCABEZADOS ///////////////////////////

            // FILA 1 //

            //internacional Dolares y euros
            if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
            {

                DateTime FechaI = Convert.ToDateTime(poComision.Fecha_Inicio);
                DateTime FechaF = Convert.ToDateTime(poComision.Fecha_Final);
                string FechaComision = FechaI.ToString("dd-MM-yyyy") + " al " + FechaF.ToString("dd-MM-yyyy");

                TipoCambio tc = new TipoCambio();
                tc = MngNegocioComision.TipoCambio(poComision.Comisionado, poComision.Fecha_Vobo);
                string TarifaDias = "";
                if (tc.Denominacion == "USD")
                {
                    TarifaDias = tc.Denominacion + " $ " + clsFuncionesGral.Convert_Decimales(tc.Tarifa);
                }
                else
                {
                    TarifaDias = tc.Denominacion + " € " + clsFuncionesGral.Convert_Decimales(tc.Tarifa);
                }

                double ImportVia = Convert.ToDouble(tc.Tipo_Cambio);

                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "1", true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Lugar, true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaDias, true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Dias_Reales, true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, ImportVia.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                // TERMINA FILA 1 //
            }
            //tERMINA internacional Dolares y euros
            //zonas simples
            if ((poComision.Zona_Comercial == "0") | (poComision.Zona_Comercial == "2") | (poComision.Zona_Comercial == "4") | (poComision.Zona_Comercial == "6") | (poComision.Zona_Comercial == "8") | (poComision.Zona_Comercial == "10") | (poComision.Zona_Comercial == "12") | (poComision.Zona_Comercial == "14") | (poComision.Zona_Comercial == "15") | (poComision.Zona_Comercial == "19") | (poComision.Zona_Comercial == "7"))
            {
                double TarifaDias = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa(poComision.Zona_Comercial));
                double ImportVia = Convert.ToDouble(poComision.Total_Viaticos) + Convert.ToDouble(poComision.Singladuras);

                DateTime FechaI = Convert.ToDateTime(poComision.Fecha_Inicio);
                DateTime FechaF = Convert.ToDateTime(poComision.Fecha_Final);
                string FechaComision = FechaI.ToString("dd-MM-yyyy") + " al " + FechaF.ToString("dd-MM-yyyy");
                if ((poComision.Zona_Comercial != "7"))
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "1", true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, poComision.Lugar, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaDias.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Dias_Reales, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, ImportVia.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                }
                

                TotalViaticos = TotalViaticos + Convert.ToDouble(poComision.Total_Viaticos) + Convert.ToDouble(poComision.Singladuras);

            }
            //TERMINA ZONAS SIMPLES

            //zonas dobles y triples
            if ((poComision.Zona_Comercial == "16") | (poComision.Zona_Comercial == "17") | (poComision.Zona_Comercial == "18") | (poComision.Zona_Comercial == "20") | (poComision.Zona_Comercial == "21") | (poComision.Zona_Comercial == "22") | (poComision.Zona_Comercial == "23"))
            {
                double TarifaComercial = 0;
                double TarifaRural = 0;
                double TarifaNavegable = 0;
                double Tarifa50km = 0;

                double tViatComercial = 0;
                double tViatRural = 0;
                double tViatNavegados = 0;
                double tViat50km = 0;

                switch (poComision.Zona_Comercial)
                {

                    case "16":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                        break;
                    case "17":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                        break;
                    case "18":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        TarifaNavegable = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("15"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatNavegados = Convert.ToDouble(poComision.Dias_Navegados) * TarifaNavegable;
                        TotalViaticosNavegados = TotalViaticosNavegados + tViatNavegados;
                        break;
                    case "20":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        break;
                    case "21":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        break;
                    case "22":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("2"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                        break;
                    case "23":
                        TarifaComercial = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("4"));
                        TarifaRural = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("14"));
                        Tarifa50km = Convert.ToDouble(MngNegocioComision.Obtiene_Tarifa("19"));
                        tViatComercial = Convert.ToDouble(poComision.Dias_Comercial) * TarifaComercial;
                        tViatRural = Convert.ToDouble(poComision.Dias_Rural) * TarifaRural;
                        tViat50km = Convert.ToDouble(poComision.Dias_50) * Tarifa50km;
                        TotalViaticosRurales = TotalViaticosRurales + tViatRural;
                        break;
                }

                DateTime FechaI = Convert.ToDateTime(poComision.Fecha_Inicio);
                DateTime FechaF = Convert.ToDateTime(poComision.Fecha_Final);
                string FechaComision = FechaI.ToString("dd-MM-yyyy") + " al " + FechaF.ToString("dd-MM-yyyy");

                if (tViatComercial != 0)
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, poComision.Lugar, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaComercial.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Dias_Comercial, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViatComercial.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    consecutivo += 1;
                }
                if (tViatRural != 0)
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, poComision.Lugar, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaRural.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Dias_Rural, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViatRural.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    consecutivo += 1;
                }
                if (tViatNavegados != 0)
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, poComision.Lugar, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TarifaNavegable.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Dias_Rural, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViatNavegados.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    consecutivo += 1;

                }
                if (tViat50km != 0)
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, consecutivo.ToString(), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, poComision.Lugar, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, Tarifa50km.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, poComision.Dias_50, true, false, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, tViat50km.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                    consecutivo += 1;
                }
                TotalViaticos = tViatComercial + tViatRural + tViatNavegados + tViat50km;

            }
            //TERMINA zonas dobles y triples

            //////////////////////////////
            if (ListArchivosAmp.Count != 0)
            {
                celda = new PdfPCell(tablaAmp);
                celda.Colspan = 6;
                celda.Border = 0;
                table.AddCell(celda);
                celda = null;
            }
            //////////////////



            if ((poComision.Combustible_Efectivo != Dictionary.NUMERO_CERO) | (TotalCombAmp != 0))
            {
                phrase = new Phrase(new Chunk(clsFuncionesGral.ConvertMayus("COMBUSTIBLE efectivo"), Letra08Normal));
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.Colspan = 5;
                //celda.Border = 1;
                table.AddCell(celda);
                TotalCombustible = Convert.ToDouble(poComision.Combustible_Efectivo) + TotalCombAmp;
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TotalCombustible.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));
            }

            if ((poComision.Peaje != Dictionary.NUMERO_CERO) | (TotalPeajeAmp != 0))
            {
                phrase = new Phrase(new Chunk(clsFuncionesGral.ConvertMayus("peaje"), Letra08Normal));
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.Colspan = 5;
                //celda.Border = 1;
                table.AddCell(celda);
                TotalPeaje = Convert.ToDouble(poComision.Peaje) + TotalPeajeAmp;
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TotalPeaje.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));

            }

            if ((poComision.Pasaje != Dictionary.NUMERO_CERO) | (TotalPasajeAmp != 0))
            {
                phrase = new Phrase(new Chunk(clsFuncionesGral.ConvertMayus("pasaje"), Letra08Normal));
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.Colspan = 5;
                //celda.Border = 1;
                table.AddCell(celda);
                TotalPasaje = Convert.ToDouble(poComision.Pasaje) + TotalPasajeAmp;
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TotalPasaje.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));

            }

            //total = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje);

            TotalTotal = TotalViaticos + TotalCombustible + TotalPasaje + TotalPeaje;


            // FILA 3 // ModAHora
            if (totalDev != 0 && totalAnt != 0)
            {
                phrase = new Phrase();

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("efectivo total calculado: "), Letra08Normal));
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.Colspan = 5;
                table.AddCell(celda);

                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, totalDev.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));

                phrase = new Phrase();

                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("efectivo total otorgado: "), Letra08Normal));
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.Colspan = 5;
                table.AddCell(celda);

                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, totalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));

                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, " RECIBI EN EFECTIVO LA CANTIDAD DE: ", true, false, false, false, 6));

                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, totalAnt.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, clsFuncionesGral.Convertir_Num_Letra(totalAnt.ToString(), true), false, false, false, false, 5));

            }
            else
            {
                phrase = new Phrase();
                if (poComision.Forma_Pago_Viaticos == "1")
                {
                    if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
                    {
                        phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("efectivo total calculado en moneda nacional"), Letra08Normal));
                    }
                    else
                    {
                        phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("efectivo total calculado: "), Letra08Normal));
                    }
                }
                else
                {
                    if ((poComision.Zona_Comercial == "7") | (poComision.Zona_Comercial == "8"))
                    {
                        phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("efectivo total otorgado en moneda nacional"), Letra08Normal));
                    }
                    else
                    {
                        phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus("efectivo total otorgado: "), Letra08Normal));
                    }
                }

                // EFECTIVO TOTAL OTORGADO TEXTO
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.Colspan = 5;
                table.AddCell(celda);

                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TotalTotal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));

                // TERMINA FILA 3 //
                // FILA 4 //
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, " RECIBI EN EFECTIVO LA CANTIDAD DE: ", true, false, false, false, 6));

                // TERMINA FILA 4 //
                // FILA 5 //

                if (poComision.Forma_Pago_Viaticos == "1")
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "$" + clsFuncionesGral.Convert_Decimales(Dictionary.NUMERO_CERO)));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "    D   E   V   E   N   G   A   D   O ", false, false, false, false, 5));
                }
                else
                {
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, TotalTotal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, clsFuncionesGral.Convertir_Num_Letra(TotalTotal.ToString(), true), false, false, false, false, 5));
                }

            }

            // TERMINA FILA 5 //

            document.Add(table);
            table = null;
            celda = null;

            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; ; //SALTO DE LINEA
            //******************************

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            table.AddCell(AgregaDatoCeldasNew(Letra08Negrita, " RELACIÓN DE GASTOS \n DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACIÓN CON REQUISITOS FISCALES ", true, true));
            document.Add(table);
            table = null;


            // FECHA CONCEPTO IMPORTE OBSERVACIONES //

            // ENCABEZADOS TABLA 2 //
            medidaCeldas = new float[4];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 1f;
            medidaCeldas[2] = 1f;
            medidaCeldas[3] = 1.5f;


            table = Genera_Tabla_TamañoPersonalizado(4, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "FECHA", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CONCEPTO", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "OBSERVACIONES", true, false, true, true));


            medidaCeldas = new float[4];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 1f;
            medidaCeldas[2] = 1f;
            medidaCeldas[3] = 1.5f;


            PdfPTable table2 = Genera_Tabla_TamañoPersonalizado(4, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

            // TERMINAN ENCABEZADOS //

            double TotalcompFis = 0;
            double Totalcomp = 0;
            //declarar 

            
            List<Entidad> oConceptos = MngNegocioComprobacion.Obtiene_Lista_Conceptos();
            foreach (Entidad oent in oConceptos)
            {
                Phrase phFecha = new Phrase(), phConcepto = new Phrase(), phImporte = new Phrase(), phObservacions = new Phrase();

                Entidad oDatos = new Entidad();
                oDatos = MngNegocioComprobacion.Obtiene_FechaMax_Importe(poComision.Archivo, poComision.Comisionado, oent.Codigo);
                double ImporteComp = Convert.ToDouble(oDatos.Descripcion);
                if (ImporteComp > 0)
                {
                    TotalcompFis = TotalcompFis + ImporteComp;
                    //guardar datos
                    phFecha.Add(new Chunk(oDatos.Codigo, Letra08Normal));//FECHA
                    phConcepto.Add(new Chunk(oent.Descripcion, Letra08Normal));//CONCEPTO
                    phImporte.Add(new Chunk(ImporteComp.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Normal));//IMPORTE


                    List<Entidad> LlFacturas = MngNegocioComprobacion.ObtieneListaImportes(poComision.Ubicacion_Comisionado, oent.Codigo, poComision.Oficio, poComision.Archivo, poComision.Ubicacion, "2");//verificar que trae

                    if (LlFacturas.Count > 1)
                    {
                        phObservacions.Add(new Chunk(clsFuncionesGral.ConvertMayus("Se anexa relacion de facturas"), Letra06Normal));//OBSERVACIONES
                    }
                    else
                    {
                        if (LlFacturas.Count == 1)
                        {
                            foreach (Entidad x in LlFacturas)
                            {
                                phObservacions.Add(new Chunk(clsFuncionesGral.ConvertMayus(x.Descripcion), Letra06Normal));//OBSERVACIONES
                            }
                        }
                        else if (LlFacturas.Count == 0)
                        {
                            phObservacions.Add(new Chunk("  ", Letra08Normal));//OBSERVACIONES
                        }
                    }

                    celda = new PdfPCell(phFecha);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(celda);

                    celda = new PdfPCell(phConcepto);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(celda);

                    celda = new PdfPCell(phImporte);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(celda);

                    celda = new PdfPCell(phObservacions);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(celda);

                }
            }



            // FILA 1 //

            if (TotalcompFis > 0)
            {
                celda = new PdfPCell(table2);
                celda.Colspan = 4;
                celda.Border = 0;
                table.AddCell(celda);
            }
            else
            {
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "00-00-0000", true));//FECHA
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NA", true));// CONCEPTO
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "$0.00", true));// IMPORTE
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NA", true));//OBSERVACIONES
            }
            Totalcomp = TotalcompFis;
            // TERMINA FILA 1 //
            // FILA 2 //

            phrase = new Phrase();
            phrase.Add(new Chunk(" TOTAL ", Letra08Negrita));
            celda = new PdfPCell(phrase);
            celda.Colspan = 2;
            celda.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk(TotalcompFis.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Negrita)); // MONTO $$$$
            celda = new PdfPCell(phrase);
            celda.Colspan = 2;
            celda.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(celda);
            document.Add(table);
            table = null;
            celda = null;

            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            table.AddCell(AgregaDatoCeldasNew(Letra08Negrita, " DESGLOSE DE EROGACIONES COMPROBADAS CON DOCUMENTACIÓN SIN REQUISITOS FISCALES ", true, true));
            document.Add(table);
            table = null;
            celda = null;

            // ENCABEZADOS TABLA 3 //
            medidaCeldas = new float[4];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 2f;
            medidaCeldas[2] = 1.2f;
            medidaCeldas[3] = 2.3f;


            table = Genera_Tabla_TamañoPersonalizado(4, 100, Dictionary.ALIGN_CENTER, medidaCeldas);


            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "FECHA", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CONCEPTO", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "OBSERVACIONES", true, false, true, true));

            // TERMINAN ENCABEZADOS //

            // FILA 1 //

            List<Comisio_Comprobacion> ListaComprobNofiscal = new List<Comisio_Comprobacion>();
            ListaComprobNofiscal = MngNegocioComprobacion.Regresa_ListComprobacion(poComision.Comisionado, poComision.Comisionado, poComision.Archivo, "3", true);


            double impNoFiscal = 0;
            if (ListaComprobNofiscal.Count == 1)
            {
                foreach (Comisio_Comprobacion oComp in ListaComprobNofiscal)
                {

                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, oComp.FECHA_COMPROBACION, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, oComp.CONCEPTO_COMP, true));
                    impNoFiscal = impNoFiscal + Convert.ToDouble(oComp.IMPORTE.ToString().Replace("$", ""));
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, oComp.IMPORTE, true));
                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, clsFuncionesGral.ConvertMayus(oComp.OBSERVACIONES), true));
                    
                }
            }
            else if (ListaComprobNofiscal.Count > 1)
            {
                DateTime dtFecha = new DateTime();
                string sConceptosNoFiscal = "";
                foreach (Comisio_Comprobacion oComp in ListaComprobNofiscal)
                {
                    dtFecha = Convert.ToDateTime(oComp.FECHA_COMPROBACION);
                    impNoFiscal = impNoFiscal + Convert.ToDouble(oComp.IMPORTE.ToString().Replace("$", ""));
                    sConceptosNoFiscal += oComp.CONCEPTO_COMP + " | ";
                }
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, dtFecha.ToString("dd-MM-yyyy"), true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra07Normal, "NO FISCALES", true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, impNoFiscal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true, false, true));
                table.AddCell(AgregaDatoCeldasNew(Letra07Normal, clsFuncionesGral.ConvertMayus(sConceptosNoFiscal), true, false, true));
            }
            if (impNoFiscal <= 0)
            {
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "00-00-0000", true));//FECHA
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NA", true));// CONCEPTO
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "$0.00", true));// IMPORTE
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NA", true));//OBSERVACIONES
            }
            // TERMINA FILA 1 //

            // FILA 2 //

            phrase = new Phrase();
            phrase.Add(new Chunk(" TOTAL ", Letra08Negrita));
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_RIGHT;
            celda.Colspan = 2;
            //celda.Border = 1;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk(impNoFiscal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Negrita)); // MONTO $$$$
            celda = new PdfPCell(phrase);
            celda.Colspan = 2;
            //celda.Border = 1;
            table.AddCell(celda);
            // TERMINA FILA 2 //
            document.Add(table);
            table = null;
            celda = null;
            Totalcomp = Totalcomp + impNoFiscal;
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null;  //SALTO DE LINEA
            //******************************

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            phrase = new Phrase();
            phrase.Add(new Chunk(" REINTEGRO EFECTUADO ", Letra08Negrita));
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.Border = 0;
            table.AddCell(celda);
            document.Add(table);
            table = null;
            celda = null;

            // ENCABEZADOS TABLA 4//
            medidaCeldas = new float[4];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 2f;
            medidaCeldas[2] = 1.2f;
            medidaCeldas[3] = 2.3f;


            table = Genera_Tabla_TamañoPersonalizado(4, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "FECHA", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CONCEPTO", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "OBSERVACIONES", true, false, true, true));
            // TERMINAN ENCABEZADOS //
            double totalReinte = 0;
            List<Entidad> oNofacturable = MngNegocioComprobacion.Obtiene_No_Facturables(poComision.Comisionado, poComision.Archivo, "3", true);

            if (oNofacturable.Count > 0)
            {
                foreach (Entidad oEntidad in oNofacturable)
                {
                    string[] ads = new string[2];
                    ads = oEntidad.Codigo.Split(new Char[] { '|' });
                    string[] ads1 = new string[2];
                    ads1 = oEntidad.Descripcion.Split(new Char[] { '|' });


                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, clsFuncionesGral.FormatFecha(ads[0]), true));//FECHA

                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, ads1[0], true));// CONCEPTO

                    double ImporteReintegro = Convert.ToDouble(ads[1]);
                    totalReinte = totalReinte + ImporteReintegro;
                    table.AddCell(AgregaDatoCeldasNew(Letra08Normal, ImporteReintegro.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));// IMPORTE

                    table.AddCell(AgregaDatoCeldasNew(Letra07Normal, clsFuncionesGral.ConvertMayus(ads1[1]), true));//OBSERVACIONES
                }
            }
            else
            {
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "00-00-0000", true));//FECHA
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NA", true));// CONCEPTO
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "$0.00", true));// IMPORTE
                table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "NA", true));//OBSERVACIONES
            }

            // FILA 1 //



            // TERMINA FILA 1 //

            // FILA 2 //

            phrase = new Phrase();
            phrase.Add(new Chunk(" TOTAL ", Letra08Negrita));
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_RIGHT;

            celda.Colspan = 2;
            //celda.Border = 1;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk(totalReinte.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Negrita)); // MONTO $$$$
            celda = new PdfPCell(phrase);
            celda.Colspan = 2;
            //celda.Border = 1;
            table.AddCell(celda);
            // TERMINA FILA 2 //
            document.Add(table);
            table = null;
            celda = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null;  //SALTO DE LINEA
            //****************************** 

            // ENCABEZADOS TABLA 5//

            medidaCeldas = new float[4];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 2f;
            medidaCeldas[2] = 1.2f;
            medidaCeldas[3] = 2.3f;
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            phrase = new Phrase();
            phrase.Add(new Chunk(" SUMA TOTAL DE COMPROBACIÓN ", Letra08Negrita));
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.Border = 0;
            table.AddCell(celda);
            document.Add(table);
            table = null;
            celda = null;

            
            Totalcomp = Totalcomp + totalReinte;

            table = Genera_Tabla_TamañoPersonalizado(4, 100, Dictionary.ALIGN_CENTER, medidaCeldas);


            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "FECHA", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CONCEPTO", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE", true, false, true, true));
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "OBSERVACIONES", true, false, true, true));
            string fechaCierreComp= MngNegocioComprobacion.Obtiene_Fecha_Comprobacion(psFolio, poComision.Archivo);
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, fechaCierreComp, true));//FECHA
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "TOTAL COMPROBADO", true));// CONCEPTO
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, Totalcomp.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));// IMPORTE
            table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "", true));//OBSERVACIONES
            
            document.Add(table);
            table = null;
            celda = null;
            //******************************
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            document.Add(table);
            table = null; //SALTO DE LINEA
            //******************************

            medidaCeldas = new float[3];
            medidaCeldas[0] = 1f;
            medidaCeldas[1] = 1f;
            medidaCeldas[2] = 1f;

            table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldas);


            phrase = new Phrase();
            phrase.Add(new Chunk("CERTIFICADO DE TRANSITO", Letra08Normal));
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.Colspan = 3;
            celda.Border = 0;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk(" CERTIFICO QUE EL C.\n", Letra05Normal));
            phrase.Add(new Chunk(" HA PERMANECIDO EN ESTA CIUDAD DEL:\n ", Letra05Normal));
            phrase.Add(new Chunk(" OBSERVACIONES:\n ", Letra05Normal));
            phrase.Add(new Chunk(" CIUDAD O LOCALIDAD:\n", Letra05Normal));
            phrase.Add(new Chunk(" ", Letra05Normal));

            celda = new PdfPCell(phrase);
            celda.Border = 0;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk("\n", Letra05Normal));
            phrase.Add(new Chunk(" AL           \n", Letra05Normal));
            phrase.Add(new Chunk("\n", Letra05Normal));
            phrase.Add(new Chunk("\n", Letra05Normal));
            phrase.Add(new Chunk(" SELLO", Letra05Normal));

            celda = new PdfPCell(phrase);
            celda.Border = 0;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", Letra05Normal));
            phrase.Add(new Chunk(" \n", Letra05Normal));
            phrase.Add(new Chunk(" \n", Letra05Normal));
            phrase.Add(new Chunk(" \n", Letra05Normal));
            phrase.Add(new Chunk(" CERTIFICA NOMBRE, FIRMA Y PUESTO ", Letra05Normal));

            celda = new PdfPCell(phrase);
            celda.Border = 0;
            table.AddCell(celda);


            document.Add(table);
            table = null;
            celda = null;


            /////////////////////////////////////////////////////////////////////////modificar ultimo

            //PIE DE PAGINA

            medidaCeldas = new float[1];
            medidaCeldas[0] = 1f;

            table = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

            phrase = new Phrase();
            phrase.Add(new Chunk(" DECLARO BAJO PROTESTA DE DECIR LA VERDAD, QUE LOS DATOS CONTENIDOS EN ESTE DOCUMENTO SON VERIDICOS Y MANIFIESTO TENER CONOCIMIENTO DE LAS SANCIONES QUE SE APLICAN EN CASO CONTRARIO. \n", Letra06Normal));
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.Border = 0;
            celda.Colspan = 1;
            table.AddCell(celda);

            phrase = new Phrase();
            phrase.Add(new Chunk("INFORMA Y ENTREGA \n", Letra08Negrita));
            phrase.Add(new Chunk("                  \n\n\n", Letra08Negrita));
            phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado) + "\n"), Letra08Negrita));
            phrase.Add(new Chunk("COMISIONADO       ", Letra08Negrita)); // NOMBRE DEL COMISIONADO
            celda = new PdfPCell(phrase);
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            //celda.Border = 1;
            table.AddCell(celda);

            /*
            Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(poComision.Dep_Proy, poComision.Proyecto, Year);
            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(objProyecto.Dependencia);

            if (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION)
            {
                phrase = new Phrase();
                phrase.Add(new Chunk(" AUTORIZA \n", Letra08Negrita));
                phrase.Add(new Chunk("                  \n\n\n", Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza) + "\n"), Letra08Negrita));
                phrase.Add(new Chunk(" TITULAR DE LA UNIDAD RESPONSABLE ", Letra08Negrita)); // TITUTAR DE LA UNIDAD RESPONSABLE
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                //celda.Border = 1;
                table.AddCell(celda);

                phrase = new Phrase();
                phrase.Add(new Chunk(" RECIBE Y ACEPTA \n", Letra08Negrita));
                phrase.Add(new Chunk("                  \n\n\n", Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Vobo) + "\n"), Letra08Negrita));
                phrase.Add(new Chunk(" TITULAR DEL AREA ADMINISTRATIVA ", Letra08Negrita)); // TITUTAR DEL AREA ADMINISTRATIVA
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(celda);
            }
            else
            {

                if (poComision.Autoriza == MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION))
                {
                    Usuario objAdministrador = MngNegocioUsuarios.Datos_Administrador(Dictionary.SUBDIRECTOR_ADJUNTO, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_FINANCIERO, Dictionary.SUBDIRECTOR_ADJUNTO), true);


                    phrase = new Phrase();
                    phrase.Add(new Chunk(" AUTORIZA \n\n\n\n", Letra08Negrita));
                    phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat + "\n"), Letra08Negrita));
                    phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(objAdministrador.Cargo), Letra08Negrita)); // TITUTAR DE LA UNIDAD RESPONSABLE
                    celda = new PdfPCell(phrase);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    //celda.Border = 1;
                    table.AddCell(celda);



                }
                else
                {
                    phrase = new Phrase();
                    phrase.Add(new Chunk(" AUTORIZA \n\n\n\n", Letra08Negrita));
                    phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza) + "\n"), Letra08Negrita));
                    phrase.Add(new Chunk(" TITULAR DE LA UNIDAD RESPONSABLE ", Letra08Negrita)); // TITUTAR DE LA UNIDAD RESPONSABLE
                    celda = new PdfPCell(phrase);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    //celda.Border = 1;
                    table.AddCell(celda);


                }

                phrase = new Phrase();
                phrase.Add(new Chunk(" RECIBE Y ACEPTA \n\n\n\n", Letra08Negrita));
                phrase.Add(new Chunk(clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_DIRECTOR_ADMON, Dictionary.DIRECTOR_ADMINISTRACION)))), Letra08Negrita));
                phrase.Add(new Chunk("\n TITULAR DEL AREA ADMINISTRATIVA ", Letra08Negrita)); // TITUTAR DEL AREA ADMINISTRATIVA
                celda = new PdfPCell(phrase);
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(celda);

            }

            */
            //table.AddCell(celda);
            table.TotalWidth = 535F;
            table.WriteSelectedRows(0, -1, 37, 110, pdfw.DirectContent);
            
            table = null;
            celda = null;
            medidaCeldas = null;



            if ((poComision.Zona_Comercial != "15") && (poComision.Zona_Comercial != "18"))
            {
                if (ListaComprobNofiscal.Count > 0)
                {

                    DateTime FechaI = Convert.ToDateTime(poComision.Fecha_Inicio);
                    DateTime FechaF = Convert.ToDateTime(poComision.Fecha_Final);
                    string FechaComision = FechaI.ToString("dd-MM-yyyy") + " AL " + FechaF.ToString("dd-MM-yyyy");

                    float[] medidaCeldasDatos = new float[3];
                    medidaCeldasDatos[0] = 1f;
                    medidaCeldasDatos[1] = 1f;
                    medidaCeldasDatos[2] = 1f;

                    document.NewPage();
                    table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

                    //DATOS ENCABEZADO
                    //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                    Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 400, 100, 0);
                    
                    document.Add(table);
                    table = null;

                    table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                    subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("cOMPRoBANTE DE GASTOS NO FISCALES"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                    Agrega_TablainCelda_Drawing(table, 0, subtabla);
                    //Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
                    subtabla = null;
                    document.Add(table);
                    table = null;

                    PdfPTable tablaNofiscal = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldasDatos);
                    PdfPTable tablaPeajeNoFac = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldasDatos);
                    impNoFiscal = 0;
                    double ImportPeajeNoFiscal = 0;
                    bool bNoFiscal = false, bPeajeNoFisc = false;

                    foreach (Comisio_Comprobacion oComp in ListaComprobNofiscal)
                    {
                        if (oComp.NUM_CONCEP == "12")
                        {
                            bNoFiscal = true;
                            tablaNofiscal.AddCell(AgregaDatoCeldasNew(Letra07Normal, oComp.CONCEPTO_COMP, true));
                            impNoFiscal = impNoFiscal + Convert.ToDouble(oComp.IMPORTE.ToString().Replace("$", ""));
                            tablaNofiscal.AddCell(AgregaDatoCeldasNew(Letra08Normal, oComp.IMPORTE, true));
                            tablaNofiscal.AddCell(AgregaDatoCeldasNew(Letra07Normal, clsFuncionesGral.ConvertMayus(oComp.OBSERVACIONES), true));
                        }
                        if (oComp.NUM_CONCEP == "16")
                        {
                            bPeajeNoFisc = true;
                            tablaPeajeNoFac.AddCell(AgregaDatoCeldasNew(Letra07Normal, oComp.CONCEPTO_COMP, true));
                            ImportPeajeNoFiscal = ImportPeajeNoFiscal + Convert.ToDouble(oComp.IMPORTE.ToString().Replace("$", ""));
                            tablaPeajeNoFac.AddCell(AgregaDatoCeldasNew(Letra08Normal, oComp.IMPORTE, true));
                            tablaPeajeNoFac.AddCell(AgregaDatoCeldasNew(Letra07Normal, clsFuncionesGral.ConvertMayus(oComp.OBSERVACIONES), true));
                        }
                    }
                    if (bNoFiscal)
                    {
                        medidaCeldas = new float[2];
                        medidaCeldas[0] = 5f;
                        medidaCeldas[1] = 1f;
                        table = Genera_Tabla_TamañoPersonalizado(2, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

                        phrase = new Phrase();
                        phrase.Add(new Chunk("BUENO POR: " + impNoFiscal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Normal)); // MONTO $$$$
                        phrase.Add(new Chunk("\n" + clsFuncionesGral.Convertir_Num_Letra(impNoFiscal.ToString(), true), Letra08Normal));
                        celda = new PdfPCell(phrase);
                        celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                        celda.Border = 0;
                        table.AddCell(celda);
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "   ", true, true));
                        document.Add(table);
                        table = null;
                        celda = null;

                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                        table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldasDatos);
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CONCEPTO", true, false, true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE", true, false, true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "OBSERVACIONES", true, false, true, true));

                        celda = new PdfPCell(tablaNofiscal);
                        celda.Colspan = 3;
                        celda.Border = 0;
                        table.AddCell(celda);
                        celda = null;

                        phrase = new Phrase(new Chunk("TOTAL: ", Letra08Normal));
                        celda = new PdfPCell(phrase);
                        celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(celda);//TOTAL
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, impNoFiscal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));//IMPORTE TOTAL
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, " ", true));//OBSERVACIONES
                        document.Add(table);
                        celda = null;
                        table = null;

                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************

                        table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "PERIODO DE LA COMISION", true, false, false, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, " ", true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, "LUGAR DE LA COMISIÓN: " + poComision.Lugar, true, true));
                        document.Add(table);
                        table = null;

                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************

                        table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);
                        table.AddCell(AgregaDatoCeldasNew(Letra07Negrita, " ", true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Negrita, " ", true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra08Negrita, "NOMBRE Y FIRMA DEL COMISIONADO \n\n\n\n\n" + clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado)), true));
                        document.Add(table);
                        table = null;
                    }

                    if (bNoFiscal == true && bPeajeNoFisc == true)
                    {
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null;//SALTO DE LINEA
                        //******************************
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                        table.AddCell(AgregaDatoCeldasNew(Letra07Negrita, "___________________________________________________________________________________", true, true));
                        document.Add(table);
                        table = null;
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                    }
                    if (bPeajeNoFisc)
                    {
                        medidaCeldas = new float[2];
                        medidaCeldas[0] = 5f;
                        medidaCeldas[1] = 1f;
                        table = Genera_Tabla_TamañoPersonalizado(2, 100, Dictionary.ALIGN_CENTER, medidaCeldas);

                        phrase = new Phrase();
                        phrase.Add(new Chunk("BUENO POR: " + ImportPeajeNoFiscal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), Letra08Normal)); // MONTO $$$$
                        phrase.Add(new Chunk("\n" + clsFuncionesGral.Convertir_Num_Letra(ImportPeajeNoFiscal.ToString(), true), Letra08Normal));
                        celda = new PdfPCell(phrase);
                        celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                        celda.Border = 0;
                        table.AddCell(celda);
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "   ", true, true));
                        document.Add(table);
                        table = null;
                        celda = null;

                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************
                        table = Genera_Tabla_TamañoPersonalizado(3, 100, Dictionary.ALIGN_CENTER, medidaCeldasDatos);
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "CONCEPTO", true, false, true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "IMPORTE", true, false, true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "OBSERVACIONES", true, false, true, true));

                        celda = new PdfPCell(tablaPeajeNoFac);
                        celda.Colspan = 3;
                        celda.Border = 0;
                        table.AddCell(celda);
                        celda = null;

                        phrase = new Phrase(new Chunk("TOTAL: ", Letra08Normal));
                        celda = new PdfPCell(phrase);
                        celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(celda);//TOTAL
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, ImportPeajeNoFiscal.ToString("C", CultureInfo.CreateSpecificCulture("es-MX")), true));//IMPORTE TOTAL
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, " ", true));//OBSERVACIONES
                        document.Add(table);
                        celda = null;
                        table = null;

                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************

                        table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);
                        table.AddCell(AgregaDatoCeldasNew(Letra08Normal, "PERIODO DE LA COMISION", true, false, false, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, " ", true, true));

                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, FechaComision, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Normal, "LUGAR DE LA COMISIÓN: " + poComision.Lugar, true, true));
                        document.Add(table);
                        table = null;

                        //******************************
                        table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);
                        AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
                        document.Add(table);
                        table = null; //SALTO DE LINEA
                        //******************************

                        table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);
                        table.AddCell(AgregaDatoCeldasNew(Letra07Negrita, " ", true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra07Negrita, " ", true, true));
                        table.AddCell(AgregaDatoCeldasNew(Letra08Negrita, "NOMBRE Y FIRMA DEL COMISIONADO \n\n\n\n\n" + clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado)), true));
                        document.Add(table);
                        table = null;
                    }
                    tablaPeajeNoFac = null;
                    tablaNofiscal = null;
                    medidaCeldas = null;

                }

            }
            List<GridView> listaComp = MngNegocioComprobacion.ListaComprobantes(poComision.Oficio, poComision.Comisionado, poComision.Archivo, poComision.Ubicacion_Comisionado);

            if (listaComp.Count > 0)
            {
                document.NewPage();

                table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_JUSTIFIED);

                //DATOS ENCABEZADO
                //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                Agrega_Imagen_Celda(table, Dictionary.AGRICULINAPESCA, 400, 100, 0);
                document.Add(table);

                table = null;
                table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("relacion de comprobantes"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                Agrega_TablainCelda_Drawing(table, 0, subtabla);
                //Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
                subtabla = null;
                document.Add(table);

                table = null;


                table = Genera_Tabla_SinEncabezado(6, 100, Dictionary.ALIGN_CENTER);

                PdfPCell titulo1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo1);

                PdfPCell titulo2 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("PDF"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo2);

                PdfPCell titulo3 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("XML"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo3.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo3);

                PdfPCell titulo4 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("TICKET"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo4.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo4);

                PdfPCell titulo5 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("IMPORTE"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo5.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo5);

                PdfPCell titulo6 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("FOLIO FISCAL"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo6.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo6);

                foreach (GridView obj in listaComp)
                {
                    PdfPCell cConcep1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Adscripcion), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cConcep1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cConcep1);

                    PdfPCell cPDF = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Comisionado), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cPDF.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cPDF);

                    PdfPCell cXML = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Lugar), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cXML.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cXML);

                    PdfPCell cTICKET = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Rol), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cTICKET.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cTICKET);

                    PdfPCell cImport = new PdfPCell(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(obj.RFC), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cImport.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cImport);

                    PdfPCell cFolioF = new PdfPCell(Agrega_Parrafo(obj.Ubicacion, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cFolioF.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cFolioF);
                }
                document.Add(table);
                table = null;
            }

            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("  "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("INAPESCA " + Year), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus("generado via web por Smaf.inapesca.gob.mx/index.aspx"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

            document.Add(table);

            table = null;
            oNofacturable = null;
            oConceptos = null;

            document.Close();

            pdfw.Close();

            //DESCARGAR ARCHIVO 

            //Process.Start(path);
            //archivo = "";
            //archivo = archivo + ".pdf";
            //raiz = "";
            //raiz = HttpContext.Current.Server.MapPath("..") + "/PDF/" + archivo;


            //DonwloadFile(raiz, archivo);      
        }

        public static PdfPCell AgregaDatoCeldasNew(iTextSharp.text.Font Tletra, string Dato, bool AlingHorizCenter = false, bool NoBorder = false, bool AlingVertCenter = false, bool FondoGris = false, int Colsplan = 0)
        {
            Phrase phrase = new Phrase(new Chunk(Dato, Tletra));
            PdfPCell celda = new PdfPCell(phrase);
            if (AlingHorizCenter)
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
            if (AlingVertCenter)
                celda.VerticalAlignment = Element.ALIGN_MIDDLE;
            if (NoBorder)
                celda.Border = 0;
            if (FondoGris)
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
            if (Colsplan > 0)
                celda.Colspan = Colsplan;
            return celda;
        }

    }


}