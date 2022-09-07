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
using System.Drawing;
using System.Diagnostics;
using System.Web.SessionState;
using InapescaWeb.DAL;
using System.Data;

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
                    imageFilePath = HttpContext.Current.Server.MapPath("..") + "\\Resources\\mexico.PNG";

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

                    objImageFondo.SetAbsolutePosition(55, 155);
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
            Ruta = raiz + psDescripcion  ;
            lsUbicacion = psDescripcion ;
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

            if (File.Exists(path ))
            {
                File.Delete(path );
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
            PdfPTable table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

            //DATOS ENCABEZADO
            //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
            Agrega_Imagen_Celda(table , Dictionary.SAGARPA, 160, 100, 0);
            PdfPTable subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe de la comision"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano );

            Agrega_TablainCelda_Drawing(table, 0, subtabla);
            Agrega_Imagen_Celda(table, "firma-inapesca" , 160, 100, 0);
            subtabla = null;
            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("nOMBRE DEL SERVIDOR PUBLICO COMISONADO : " + clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado))), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Objeto de la comisión "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Objetivo), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Principales actividades desarrolladas"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "Se anexa informe de Comisión", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Evaluación"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, "Comprobación de Comision Oficio Número : " + poComision.Archivo.Replace(".pdf", ""), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Folio Comprobacion SMAF.WEB :" + archivo.Replace(".pdf", "") + " / " + psFolio ), 0, Dictionary.FUENTE_NORMAL , Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus( "Fecha cierre comprobacion SMaf.web :" + lsHoy), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
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

                if ((poComision.Forma_Pago_Viaticos == "1") | (poComision.Forma_Pago_Viaticos == Dictionary.NUMERO_CERO ))
                {
                    table.AddCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(" $ "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));

                    PdfPCell recibiLetter = new PdfPCell(new Phrase(Agrega_Parrafo(clsFuncionesGral.ConvertMayus ("  D  e  v  e  n  g  a  d  o  s  "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
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
                    AgregaDatosTabla(subtabla, clsFuncionesGral.ConvertMayus("Se anexa relacion de facturas"), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño );
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

            string totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo , "2");

            double totalOtorgado = clsFuncionesGral.Convert_Double(poComision.Total_Viaticos) + clsFuncionesGral.Convert_Double(poComision.Combustible_Efectivo) + clsFuncionesGral.Convert_Double(poComision.Peaje) + clsFuncionesGral.Convert_Double(poComision.Pasaje) ;

           
            if (clsFuncionesGral.Convert_Double ( totalComprobad)  > totalOtorgado )
            {
                totalComprobad = totalOtorgado.ToString(); 
            }

            PdfPCell cellTotalNum = new PdfPCell(new Phrase(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(totalComprobad ), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
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

            List<Entidad> oNofacturable = MngNegocioComprobacion.Obtiene_No_Facturables(poComision.Comisionado, poComision.Archivo, "3",false );

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
            totalComprobad = MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12");
            PdfPCell cellNOTotalNum = new PdfPCell(new Phrase(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(totalComprobad ), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
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

                    PdfPCell cObser = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(ads1[1]), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tPequeño ));
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

            PdfPCell cellRETotalIm = new PdfPCell(new Phrase(Agrega_Parrafo("$ " + clsFuncionesGral.Convert_Decimales(totalComprobad ), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano)));
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

            table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

            subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informa y entrega"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("comisionado"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            PdfPCell cInforma = new PdfPCell(subtabla);
            cInforma.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cInforma);

            Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(poComision.Dep_Proy, poComision.Proyecto);
            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(objProyecto.Dependencia );

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
  
                    AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus( objAdministrador.Nombre + " " + objAdministrador.ApPat + "  " + objAdministrador.ApMat ), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
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
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(MngNegocioUsuarios.Obten_Usuario( Dictionary.DIRECTOR_ADMINISTRACION, MngNegocioDependencia.Obtiene_DIrector_Financieros(Dictionary.PUESTO_DIRECTOR_ADMON , Dictionary.DIRECTOR_ADMINISTRACION )))), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("titular del area administrativa"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

                PdfPCell cacepta = new PdfPCell(subtabla);
                cacepta.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cacepta);
            }

            document.Add(table);
            
            subtabla = null;
            table = null;

            oNofacturable = MngNegocioComprobacion.Obtiene_No_Facturables(poComision.Comisionado, poComision.Archivo, "3", false);

            if (oNofacturable.Count > 0)
            {
                document.NewPage();
                table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

                //DATOS ENCABEZADO
                //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 160, 100, 0);
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("cOMPRoBANTE DE GASTOS NO FISCALES"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                Agrega_TablainCelda_Drawing(table, 0, subtabla);
                Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
                subtabla = null;
                document.Add(table);
                table = null;

                table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Bueno por : $ " + clsFuncionesGral.Convert_Decimales ( MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12"))), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                document.Add(table);

                table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_CENTER);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("         "), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table,clsFuncionesGral.ConvertMayus(clsFuncionesGral.Convertir_Num_Letra(clsFuncionesGral.ConvertString(MngNegocioComprobacion.Totales(poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Archivo, "3", "12")),true )) , 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tmediano);

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

                PdfPCell No3= new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Observaciones"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
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
                PdfPCell Periodo = new PdfPCell(Agrega_Parrafo(clsFuncionesGral .ConvertMayus ("Periodo de la comision "), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                Periodo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(Periodo);

                AgregaDatosTabla_Center(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

                PdfPCell cPeriodo11 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(poComision.Fecha_Inicio + " al " + poComision.Fecha_Final), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                cPeriodo11.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(cPeriodo11);
                
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" Lugar de la Comision :" +  clsFuncionesGral.ConvertMayus ( poComision .Lugar) ), 0, Dictionary.FUENTE_NORMAL , Dictionary.BLACK, tmediano);

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

            
            List<GridView> listaComp = MngNegocioComprobacion.ListaComprobantes(poComision.Oficio, poComision.Comisionado, poComision.Archivo, poComision.Ubicacion_Comisionado);

            if (listaComp.Count  > 0)
            {
                document.NewPage();

                table = Genera_Tabla_SinEncabezado(3, 100, Dictionary.ALIGN_CENTER);

                //DATOS ENCABEZADO
                //Agrega_Imagen_CeldaCentrada(subtabla, "fondo", 8, 30, 0);
                Agrega_Imagen_Celda(table, Dictionary.SAGARPA, 160, 100, 0);
                subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_CENTER);

                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("Secretaria de agricultura, ganaderia ,desarrollo rural, pesca y alimentacion "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("instituto nacional de pesca "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("informe y relacion de gastos de la operacion"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla_Center(subtabla, clsFuncionesGral.ConvertMayus("relacion de comprobantes"), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

                Agrega_TablainCelda_Drawing(table, 0, subtabla);
                Agrega_Imagen_Celda(table, "firma-inapesca", 160, 100, 0);
                subtabla = null;
                document.Add(table);

                table = null;
             

                table = Genera_Tabla_SinEncabezado(5, 100, Dictionary.ALIGN_CENTER);

                PdfPCell titulo1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("Concepto"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo1);

                PdfPCell titulo2 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("PDF"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo2);

                PdfPCell titulo3 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("XML"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo3.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo3);

                PdfPCell titulo4 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("IMPORTE"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo4.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo4);

                PdfPCell titulo5 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus("FOLIO FISCAL"), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                titulo5.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                table.AddCell(titulo5);

                foreach (GridView  obj in listaComp )
                {
                    PdfPCell cConcep1 = new PdfPCell(Agrega_Parrafo(clsFuncionesGral .ConvertMayus (obj.Adscripcion) , Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cConcep1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cConcep1);

                    PdfPCell cPDF = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Comisionado), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cPDF.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cPDF);

                    PdfPCell cXML = new PdfPCell(Agrega_Parrafo(clsFuncionesGral.ConvertMayus(obj.Lugar), Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano));
                    cXML.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
                    table.AddCell(cXML);


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
            AgregaDatosTabla_Center(table , clsFuncionesGral.ConvertMayus("INAPESCA " + Year ), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño );
            AgregaDatosTabla_Center(table , clsFuncionesGral.ConvertMayus("generado via web por Smaf.sytes.net/index.aspx"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            
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
        public static void Genera_Oficio_Comision(Comision poComision, Ubicacion poUbicacion, Proyecto objProyecto, string psNombreAut, string psCargo, bool pBandera = false)
        {
            Year = poComision.Periodo;

            string estado, ciudad;
            Entidad oDireccionTipo = MngDatosDependencia.Obtiene_Tipo_Region(poComision.Ubicacion_Comisionado);

            if (pBandera) MngNegocioComision.Update_Oficio_Comision(poComision,oDireccionTipo); //Crea y actualiza num de oficio de comison que va a ser usado en archivo pdf y fisico

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

            if (poComision.Territorio == "3")
            {
                CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("42") + "-" + poComision.Oficio + "-" + poComision .Periodo ;
            }
            else if ((poComision.Ubicacion_Autoriza == "24") | (poComision.Ubicacion_Autoriza == "25") | (poComision.Ubicacion_Autoriza == "42") | (poComision.Ubicacion_Autoriza == "44") | (poComision.Ubicacion_Autoriza == "45") | (poComision.Ubicacion_Autoriza == "43"))
            {
                CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(poComision.Ubicacion_Autoriza) + "-" + poComision.Oficio + "-" + poComision.Periodo;
            }
            else if (oDireccionTipo.Descripcion == Dictionary .SUBDIRECCIONES_GENERALES)
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

            if (poComision.Territorio == "3")
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip("42", true), poComision.Archivo.Replace(".pdf", ""));
            }
            else if ((poComision.Ubicacion_Autoriza == "24") | (poComision.Ubicacion_Autoriza == "25") | (poComision.Ubicacion_Autoriza == "42") | (poComision.Ubicacion_Autoriza == "44") | (poComision.Ubicacion_Autoriza == "45") | (poComision.Ubicacion_Autoriza == "43"))
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(poComision.Ubicacion_Autoriza , true), poComision.Archivo.Replace(".pdf", ""));
            }
            else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
            {
                Valida_Carpeta(MngNegocioDependencia.Centro_Descrip(oDireccionTipo.Codigo , true), poComision.Archivo.Replace(".pdf", ""));
                
            }
            else
            {
                Valida_Carpeta(poUbicacion.Descripcion, poComision.Archivo.Replace(".pdf", ""));
              //  CLave_Oficio += poUbicacion.Siglas + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
            }


            path = Ruta + "\\" + archivo;

            if (pBandera) MngNegocioComision.Update_ruta_Comision(lsUbicacion, archivo, poComision.Ubicacion_Comisionado, poComision.Comisionado, poComision.Oficio,poComision );

            poComision.Ruta = lsUbicacion;

            //ACTUALIZAR EL PEDO EXISTENCIAL DE PEDRO SIERRA INSERT
            if (poComision.Ubicacion_Autoriza == "43")
            {
                bool x = MngDatosDgaipp.Inserta_Oficio_Comision(poComision.Oficio,poComision.Archivo , "OC","0", poComision.Objetivo,"", poComision.Ubicacion_Comisionado , poComision.Comisionado, poComision.Vobo,"1",Year );
            }
            //  ComisionDetalle oDetalle = MngNegocioComisionDetalle.Obtiene_detalle(poComision.Archivo, poComision.Comisionado, poComision.Ubicacion_Comisionado, Year);

            //Creamos el dodumento con sus caracteristicas
            document = Crea_Pdf();
            //  Fuente_Normal();

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            //Apertura del documento
            document.Open();

            //Cabeceras de documento aling rigth
            Imagen_Fondo(document, "mexico", 500, 500, Dictionary.FUENTE_SUBRAYADO);

            PdfPTable table = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

            float[] medidaCeldas = new float[1];//{ 1.65f, 3.35f };
            medidaCeldas[0] = 7f;

            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_LEFT, medidaCeldas);
            Agrega_Imagen_Celda(subtabla, Dictionary.SAGARPA, 160, 100, 0);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            subtabla = null;
            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_RIGHT, medidaCeldas);
            Agrega_Imagen_Celda(subtabla, "firma-inapesca", 160, 100, 0);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Territorio == "3")
            {
                clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "            -");
                clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else if ( (CLave_Oficio.Substring (13,3)== "DG-"))//((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO)) && (oDireccionTipo.Codigo == "42"))
            {
                clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "            -");
                clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
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

            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Autoriza );


            if (oUbicacion.Ciudad == "DF")
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), oUbicacion.Clvestado.Trim());

                AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(Dictionary.FECHA_ACTUAL), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            }
            else
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), poUbicacion.Clvestado.Trim());

                AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad) + "," + clsFuncionesGral.ConvertMinus(estado)) + ",  a " + clsFuncionesGral.Convert_Mes_Letra(Dictionary.FECHA_ACTUAL), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
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
            if (poUbicacion.Ciudad == "DF")
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

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("producto institucional"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
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
                    ListaNewProdductos[i] = MngNegocioProductos.Obtiene_Descripcion_Producto(ListaProductos[i],Year );
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


            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Recurso pesquero"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Pesqueria == "00")
            {
                AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioEspecies.Obtiene_Descripcion_Pesqueria(objProyecto.Programa, poComision.Pesqueria, objProyecto.Direccion)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Actividad especifica"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
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

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Territorio == "3")
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else //if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO)) && (oDireccionTipo.Codigo == "42"))
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }/*
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" E L  C O M I S I O N A D O "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }*/

            //pablo AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" E L  C O M I S I O N A D O "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

           // if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Descripcion == "1"))
            //{
              //  AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza, oDireccionTipo.Codigo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //}
            //else
           // {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
          //  }


            //  AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Territorio == "3")
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo), oDireccionTipo.Codigo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
           // else if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO)) && (oDireccionTipo.Codigo == "42"))
           // {
             //   AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           // }
            else
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
             //   AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            if (poComision.Ubicacion_Autoriza == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA"))
            {
                Agrega_Imagen_Celda(table, poComision.Autoriza, 100, 100, 0);
            }
            else
            {
                Agrega_Imagen_Celda(table, "fondo", 20, 100, 0);
            }

            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_LEFT);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           
            if (poComision.Territorio == "3")
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo))), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           
            }
            else //if (((rol_comisionado == Dictionary.DIRECTOR_ADJUNTO) | (rol_comisionado == Dictionary.DIRECTOR_ADMINISTRACION) | (rol_comisionado == Dictionary.DIRECTOR_JURIDICO)) && (oDireccionTipo.Codigo == "42"))
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            //else
           // {
            //    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado )), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           // }

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            //con copia y viativos y combustible
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

            document.Add(Agrega_Parrafo(clsFuncionesGral.ConvertMinus("www.inapesca.gob.mx"), Dictionary.FUENTE_NORMAL, Dictionary.GRAY, Dictionary.ALIGN_CENTER, 6));
            document.Close();

            if (pBandera)
            {
                clsMail.Mail_Oficio_Comision(poComision, objProyecto, path);
            }

            Process.Start(path);
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
        public static void Reimprime_Oficio_Comision(Comision poComision, Ubicacion poUbicacion, Proyecto objProyecto, string psRol, Entidad poDireccionTipo,string psUbicacionAutoriza)
        {
            string estado, ciudad;

            //Valida_Carpeta(poUbicacion.Descripcion, poComision.Archivo.Replace(".pdf", ""));

         //   string rol_comisionado = MngDatosUsuarios.Obtiene_Rol_Usuario(poComision.Comisionado);
          //  Entidad oDireccionTipo = MngDatosDependencia.Obtiene_Tipo_Region(poComision.Ubicacion_Comisionado);

            if (poComision.Territorio == "3")
            {
                Valida_Carpeta_Reimpresion(MngNegocioDependencia.Centro_Descrip("42", true), poComision.Archivo.Replace(".pdf", ""));
            }
           /* else  if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Codigo == "43"))
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
            else
            {
                Valida_Carpeta_Reimpresion(poComision.Ruta, poComision.Archivo.Replace(".pdf", ""));
            }

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

            FileStream file = new FileStream(path, FileMode.OpenOrCreate   , FileAccess.ReadWrite, FileShare.ReadWrite);
           
            PdfWriter pdfw = PdfWriter.GetInstance(document, file);

            document.Open();

            PdfPTable subtabla;
            PdfPTable Subtabla1;
            // string[] organismo = MngNegocioDependencia.ObtieneDatosOrganismo(poUbicacion.Secretaria, poUbicacion.Organismo, "1").Split(new Char[] { '|' });
            //string[] header = new string[5];

            //   header = MngNegocioDependencia.ObtieneDatosHeader(poUbicacion.Organismo, poUbicacion.Dependencia);

            Imagen_Fondo(document, "mexico", 500, 500, Dictionary.FUENTE_SUBRAYADO);

            PdfPTable table = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

            //subtabla = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);

            // Agrega_TablainCelda_Drawing(table, 0, subtabla);

            float[] medidaCeldas = new float[1];//{ 1.65f, 3.35f };
            medidaCeldas[0] = 7f;

            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_LEFT, medidaCeldas);
            Agrega_Imagen_Celda(subtabla, Dictionary.SAGARPA, 160, 100, 0);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            subtabla = null;
            subtabla = Genera_Tabla_TamañoPersonalizado(1, 100, Dictionary.ALIGN_RIGHT, medidaCeldas);
            Agrega_Imagen_Celda(subtabla, "firma-inapesca", 160, 100, 0);
            Agrega_TablainCelda_Drawing(table, 0, subtabla);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            string clave_pdf = "";

            if (poComision.Territorio == "3")
            {
                clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "            -");
                clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else if (CLave_Oficio.Substring (13,3)== "DG-")//else if (((psRol == Dictionary.DIRECTOR_ADJUNTO) | (psRol == Dictionary.DIRECTOR_ADMINISTRACION) | (psRol == Dictionary.DIRECTOR_JURIDICO)) && (poDireccionTipo.Codigo == "42"))
            {
                clave_pdf = CLave_Oficio.Replace((poComision.Oficio + "-"), "            -");
                clave_pdf = clave_pdf.Replace(".pdf", "");
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No. ") + clave_pdf, 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
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
            table = Genera_Tabla_SinEncabezado(3, 100, "LEFT");

            //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Oficio de Notificación de Comisión No." + poComision.Archivo.Replace(".pdf", "")), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("datos del comisionado"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);

            Ubicacion oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(poComision.Ubicacion_Autoriza);


            if (oUbicacion.Ciudad == "DF")
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), oUbicacion.Clvestado.Trim());

                AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad)) + " ,a " + clsFuncionesGral.Convert_Mes_Letra(Dictionary.FECHA_ACTUAL), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tPequeño);
            }
            else
            {
                estado = MngNegocioEstado.Estado(oUbicacion.Clvestado.Trim());
                ciudad = MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad.Trim(), poUbicacion.Clvestado.Trim());

                AgregaDatosTabla(table, clsFuncionesGral.Convert_First_Letter(clsFuncionesGral.ConvertMinus(ciudad) + "," + clsFuncionesGral.ConvertMinus(estado)) + ",  a " + clsFuncionesGral.Convert_Mes_Letra(Dictionary.FECHA_ACTUAL), 0, Dictionary.FUENTE_ITALICA, Dictionary.BLACK, tPequeño);
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
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poUbicacion.Descripcion + "," + MngNegocioEstado.Estado(poUbicacion.Clvestado) + "."), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
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

            }

            AgregaDatosTabla(table, "DESCRIPCION DEL VEHICULO", 1, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, lsVehiculo, 1, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            //Hora inicio y fin comision

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Horario de salida :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Inicio_Comision.Trim()), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Horario de regreso :"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(poComision.Fin_Comision.Trim()), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);
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

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("producto institucional"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

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
                    ListaNewProdductos[i] = MngNegocioProductos.Obtiene_Descripcion_Producto(ListaProductos[i],Year  );
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

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Recurso pesquero"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioEspecies.Obtiene_Descripcion_Pesqueria(objProyecto.Programa, poComision.Pesqueria, objProyecto.Direccion)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Actividad especifica"), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioActividades.Obtiene_Descripcion_Actividades(poComision.Actividad)), 0, Dictionary.FUENTE_NORMAL, Dictionary.BLACK, tmediano);

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
            Subtabla1 = Genera_Tabla_SinEncabezado(2, 100, "LEFT");

            subtabla = null;
            subtabla = Genera_Tabla_SinEncabezado(2, 100, "LEFT");
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

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Territorio == "3")
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" A T E N T A M E N T E "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
           // else if (((psRol == Dictionary.DIRECTOR_ADJUNTO) | (psRol == Dictionary.DIRECTOR_ADMINISTRACION) | (psRol == Dictionary.DIRECTOR_JURIDICO)) && (poDireccionTipo.Codigo == "42"))
            //{
             //   AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           // }
            else
            {
               // AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus("Acepto instruccion de comisión "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            //pablo AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" E L  C O M I S I O N A D O "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            
            //if (((rol_comisionado == Dictionary.JEFE_CENTRO) | (rol_comisionado == Dictionary.JEFE_DEPARTAMENTO) | (rol_comisionado == Dictionary.SUBDIRECTOR_ADJUNTO)) && (oDireccionTipo.Descripcion == "1"))
            //{
              //  AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza, oDireccionTipo.Codigo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //}
            //else
           // {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //}
            
            
            //  AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            if (poComision.Territorio == "3")
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, poDireccionTipo.Codigo), poDireccionTipo.Codigo)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else if (((psRol == Dictionary.DIRECTOR_ADJUNTO) | (psRol == Dictionary.DIRECTOR_ADMINISTRACION) | (psRol == Dictionary.DIRECTOR_JURIDICO)) && (poDireccionTipo.Codigo == "42"))
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }
            else
            {
               AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(" "), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
               // AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Cargo(poComision.Comisionado)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            if (poComision.Ubicacion_Autoriza == MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(poComision.Ubicacion_Autoriza, "FIRMA"))
            {
                Agrega_Imagen_Celda(table, poComision.Autoriza, 100, 100, 0);
            }
            else
            {
                Agrega_Imagen_Celda(table, "fondo", 20, 100, 0);
            }

            AgregaDatosTabla(table, "", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            table = null;
            table = Genera_Tabla_SinEncabezado(2, 100, Dictionary.ALIGN_LEFT);

            AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Autoriza)), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           
            if (poComision.Territorio == "3")
            {
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, poDireccionTipo.Codigo))), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
           
            }
           // else if (((psRol == Dictionary.DIRECTOR_ADJUNTO) | (psRol == Dictionary.DIRECTOR_ADMINISTRACION) | (psRol == Dictionary.DIRECTOR_JURIDICO)) && (poDireccionTipo.Codigo == "42"))
           // {
            //    AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            //}
            else
            {
                //AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado )), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
                AgregaDatosTabla(table, clsFuncionesGral.ConvertMayus(""), 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            }

            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);
            AgregaDatosTabla(table, " ", 0, Dictionary.FUENTE_NEGRITA, Dictionary.BLACK, tmediano);

            document.Add(table);

            //con copia y viativos y combustible
            table = null;
            table = Genera_Tabla_SinEncabezado(1, 100, Dictionary.ALIGN_LEFT);

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

            document.Add(Agrega_Parrafo(clsFuncionesGral.ConvertMinus("www.inapesca.gob.mx"), Dictionary.FUENTE_NORMAL, Dictionary.GRAY, Dictionary.ALIGN_CENTER, 6));
            document.Close();

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

            raiz = HttpContext.Current.Server.MapPath("..") + "\\" + poComision.Ruta ;

            DonwloadFile(raiz, archivo);
        }

        public static void Genera_Reporte_Comparativo()
        { 
        
        }

    }


}