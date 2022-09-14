using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using InapescaWeb.BRL;
using InapescaWeb.Entidades;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Configuration;

namespace InapescaWeb
{
    public class clsFuncionesGral
    {
        static DataSet datasetArbol;
        static DataTable tblTabla;
        static string Year = DateTime.Today.Year.ToString();
        
        private DataSet dataArbol;

        static clsDictionary Dictionary = new clsDictionary();
        private static String[] UNIDADES = { "", "un ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
        private static String[] DECENAS = {"diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ",
        "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ",
        "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa "};
        private static String[] CENTENAS = {"", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ",
        "setecientos ", "ochocientos ", "novecientos "};

        static private Regex r;
        static String[] Num;

        public static bool IsSessionTimedOut()
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx == null)
                throw new Exception("Este método sólo se puede usar en una aplicación Web");

            //Comprobamos que haya sesión en primer lugar 
            //(por ejemplo si por ejemplo EnableSessionState=false)
            if (ctx.Session == null)
                return true;   //Si no hay sesión, no puede caducar pero hay que iniciar sesión nuevamente
            //Se comprueba si se ha generado una nueva sesión en esta petición
            if (!ctx.Session.IsNewSession)
                return false;   //Si no es una nueva sesión es que no ha caducado

            HttpCookie objCookie = ctx.Request.Cookies["ASP.NET_SessionId"];
            //Esto en teoría es imposible que pase porque si hay una 
            //nueva sesión debería existir la cookie, pero lo compruebo porque
            //IsNewSession puede dar True sin ser cierto (más en el post)
            if (objCookie == null)
                return true;
                //return false; esto se modificó para probar 

            //Si hay un valor en la cookie es que hay un valor de sesión previo, pero como la sesión 
            //es nueva no debería estar, por lo que deducimos que la sesión anterior ha caducado
            if (!string.IsNullOrEmpty(objCookie.Value))
                return true;
            else
                return false;
        }
        public static void Lee_XMl(string psArchivo, List<Entidad> plEntidad, Entidades.Xml poXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));

            XmlTextReader reader = new XmlTextReader(psArchivo);

            Comprobante factura = (Comprobante)serializer.Deserialize(reader);

            //comparar versiones
            double dVersion = Convert.ToDouble(factura.Version);


            poXml.SERIE = factura.Serie;
            poXml.FOLIO = factura.Folio;
            poXml.VERSION = factura.Version;
            if (dVersion < 3.3)
            {
                poXml.FORMA_PAGO = factura.formaDePago.ToString();
                poXml.METODO_PAGO = factura.metodoDePago.ToString();
                poXml.CUENTA_PAGO = factura.NumCtaPago;
                poXml.TIPO_COMPROBANTE = factura.TipoDeComprobante.ToString();

            }
            else
            {
                
                poXml.FORMA_PAGO = factura.FormaPago.ToString();
                //poXml.METODO_PAGO = factura.MetodoPago.ToString();
                switch (factura.MetodoPago.ToString())
                {
                    case "PUE":
                        poXml.METODO_PAGO = "Pago en una sola exhibición";
                        break;
                    case "PIP":
                        poXml.METODO_PAGO = "Pago inicial y parcialidades";
                        break;
                    case "PPD":
                        poXml.METODO_PAGO = "Pago en parcialidades o diferido";
                        break;
                }
                poXml.CUENTA_PAGO = "NA";
                //poXml.TIPO_COMPROBANTE = factura.TipoDeComprobante.ToString();
                switch (factura.TipoDeComprobante.ToString())
                {
                    case "I":
                        poXml.TIPO_COMPROBANTE = "Ingreso";
                        break;
                    case "E":
                        poXml.TIPO_COMPROBANTE = "Egreso";
                        break;
                    case "T":
                        poXml.TIPO_COMPROBANTE = "Traslado";
                        break;
                    case "N":
                        poXml.TIPO_COMPROBANTE = "Nómina";
                        break;
                    case "P":
                        poXml.TIPO_COMPROBANTE = "Pago";
                        break;
                }
            }

            ////////////// datos emisor

            poXml.CALLE_EMISOR = "NA";
            poXml.NO_EXTERIOR_EMMISOR = "0";
            poXml.NO_INTERIOR_EMISOR = "";
            poXml.LOCALIDAD_EMISOR = "NA";
            poXml.COLONIA_EMISOR = "NA";
            poXml.MUNICIPIO_EMISOR = "NA";
            poXml.ESTADO_EMISOR = "NA";
            poXml.PAIS_EMISOR = "NA";
            poXml.CP_EMISOR = factura.LugarExpedicion;

            ///////////////// datos receptor
            /*
            poXml.CALLE_RECEPTOR = "PITAGORAS";
            poXml.NOEXTERIOR_RECEPTOR = "1320";
            poXml.NOINTERIOR_RECEPTOR = "SN";
            poXml.LOCALIDAD_RECEPTOR = "CDMX";
            poXml.COLONIA_RECEPTOR = "SANTA CRUZ ATOYAC";
            poXml.MUNICIPIO_RECEPTOR = "";
            poXml.ESTADO_RECEPTOR = "CIUDAD DE MEXICO";
            poXml.PAIS_RECEPTOR = "MEXICO";
            poXml.CP_RECEPTOR = "03310";
            */


            poXml.CALLE_RECEPTOR = "AV. MÉXICO";
            poXml.NOEXTERIOR_RECEPTOR = "190";
            poXml.NOINTERIOR_RECEPTOR = "SN";
            poXml.LOCALIDAD_RECEPTOR = "CDMX";
            poXml.COLONIA_RECEPTOR = "DEL CARMEN";
            poXml.MUNICIPIO_RECEPTOR = "COYOACAN";
            poXml.ESTADO_RECEPTOR = "CIUDAD DE MEXICO";
            poXml.PAIS_RECEPTOR = "MEXICO";
            poXml.CP_RECEPTOR = "04100";

            ////////
            poXml.AÑOEXP =factura.fecha.Year.ToString();
            poXml.FECHA_EXPEDICION = FormatFecha(factura.Fecha.ToString());
            poXml.SUBTOTAL = Convert_Decimales(factura.SubTotal.ToString());
            poXml.DESCUENTO = Convert_Decimales(factura.Descuento.ToString());
            poXml.TOTAL = Convert_Decimales(factura.Total.ToString());
          

            poXml.LUGAR_EXPEDICION = factura.LugarExpedicion.ToString();
            poXml.NO_CERTIFICADO = factura.NoCertificado;

            poXml.RFC_EMISOR = factura.Emisor.Rfc;
            poXml.NOMBRE_EMISOR = factura.Emisor.Nombre;


            poXml.RFC_RECEPTOR = factura.Receptor.Rfc;
            poXml.NOMBRE_RECEPTOR = factura.Receptor.Nombre;



            foreach (ComprobanteConcepto con in factura.Conceptos)
            {
                Entidad obj = new Entidad();
                obj.Codigo = Convert_Decimales(con.Importe.ToString());
                obj.Descripcion = con.Descripcion;
                plEntidad.Add(obj);
                obj = null;

            }

            if (factura.Impuestos != null)
            {
                poXml.TOTAL_IMPUESTOS_TRASLADADOS = clsFuncionesGral.Convert_Decimales(factura.Impuestos.TotalImpuestosTrasladados.ToString());
                poXml.TOTAL_IMPUESTOS_RETENIDOS = clsFuncionesGral.Convert_Decimales(factura.Impuestos.TotalImpuestosRetenidos.ToString());
            }
            else
            {
                poXml.TOTAL_IMPUESTOS_TRASLADADOS = clsFuncionesGral.Convert_Decimales("0.00");
                poXml.TOTAL_IMPUESTOS_RETENIDOS = clsFuncionesGral.Convert_Decimales("0.00");
            }

            if (factura.Emisor.RegimenFiscal != null)
            {
                poXml.REGIMENFISCAL_EMISOR = MngNegocioXml.Obtiene_RegimenFiscal(factura.Emisor.RegimenFiscal.ToString());
            }
            else
            {
                poXml.REGIMENFISCAL_EMISOR = "NA";
            }

            if (factura.Impuestos != null)
            {
                if (factura.Impuestos.Traslados != null)
                {
                    foreach (ComprobanteImpuestosTraslado im in factura.Impuestos.Traslados)
                    {

                        switch (im.Impuesto.ToString())
                        {
                            case "IVA":
                                poXml.IVA = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "002":
                                poXml.IVA = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "ISR":
                                poXml.ISR = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "001":
                                poXml.ISR = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "ISH":
                                poXml.ISH = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "TUA":
                                poXml.TUA = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "IEPS":
                                poXml.IEPS = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "003":
                                poXml.IEPS = Convert_Decimales(im.Importe.ToString());
                                break;
                            case "SFP":
                                poXml.SFP = Convert_Decimales(im.Importe.ToString());
                                break;
                        }

                    }
                }

                if (factura.Impuestos.Retenciones != null)
                {
                    foreach (ComprobanteImpuestosRetencion ir in factura.Impuestos.Retenciones)
                    {
                        switch (ir.Impuesto.ToString())
                        {
                            case "IVA":
                                poXml.IVA_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "002":
                                poXml.IVA_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "ISR":
                                poXml.ISR_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "001":
                                poXml.ISR_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "ISH":
                                poXml.ISH_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;

                            case "TUA":
                                poXml.TUA_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "IEPS":
                                poXml.IEPS_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "003":
                                poXml.IEPS_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                            case "SFP":
                                poXml.SFP_RETENIDO = Convert_Decimales(ir.Importe.ToString());
                                break;
                        }
                    }
                }
            }
            //int cont = 0; 
            foreach (ComprobanteComplemento r in factura.Complemento)
            {

                for (int y = 0; y < r.Any.Length; y++)
                {
                    int x = r.Any[y].Attributes.Count;

                    for (int contador = 0; contador < x; contador++)
                    {
                        if (r.Any[y].Attributes[contador].Name == "UUID")
                        {
                            poXml.TIMBRE_FISCAL = r.Any[y].Attributes["UUID"].Value;
                            poXml.FECHA_TIMBRADO = FormatFecha(r.Any[y].Attributes["FechaTimbrado"].Value);

                        }
                    }
                }

                //              cont++;
            }


        }

        public static void Valida_Carpeta(string psDescripcion, string psFolio, string psRuta, string psUbicacion, bool pbInforme = false, bool pbComprobacion = false)
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\PDF\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES);
            if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + psFolio)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio);

            if (pbInforme)
            {
                if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + psFolio)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio + "/" + Dictionary.INFORME);
            }

            if (pbComprobacion)
            {
                if (!Directory.Exists(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + psFolio)) Directory.CreateDirectory(raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio + "/" + Dictionary.FISCALES);
            }

            psRuta = raiz + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio;
            psUbicacion = "PDF/" + Year + "/" + psDescripcion + "/" + Dictionary.COMISIONES + "/" + psFolio;
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

        public static String Convertir_Num_Letra(String numero, bool mayusculas)
        {

            String literal = "";
            String parte_decimal;
            //si el numero utiliza (.) en lugar de (,) -> se reemplaza
            numero = numero.Replace(".", ",");

            //si el numero no tiene parte decimal, se le agrega ,00
            if (numero.IndexOf(",") == -1)
            {
                numero = numero + ",00";
            }
            //se valida formato de entrada -> 0,00 y 999 999 999,00
            r = new Regex(@"\d{1,9},\d{1,2}");
            MatchCollection mc = r.Matches(numero);
            if (mc.Count > 0)
            {
                //se divide el numero 0000000,00 -> entero y decimal
                Num = numero.Split(',');

                //de da formato al numero decimal
                parte_decimal = Num[1] + "/100 M.N.";
                //se convierte el numero a literal
                if (int.Parse(Num[0]) == 0)
                {//si el valor es cero                
                    literal = "cero ";
                }
                else if (int.Parse(Num[0]) > 999999)
                {//si es millon
                    literal = getMillones(Num[0]);
                }
                else if (int.Parse(Num[0]) > 999)
                {//si es miles
                    literal = getMiles(Num[0]);
                }
                else if (int.Parse(Num[0]) > 99)
                {//si es centena
                    literal = getCentenas(Num[0]);
                }
                else if (int.Parse(Num[0]) > 9)
                {//si es decena
                    literal = getDecenas(Num[0]);
                }
                else
                {//sino unidades -> 9
                    literal = getUnidades(Num[0]);
                }
                //devuelve el resultado en mayusculas o minusculas
                if (mayusculas)
                {
                    return (literal + parte_decimal).ToUpper();
                }
                else
                {
                    return (literal + parte_decimal);
                }
            }
            else
            {//error, no se puede convertir
                return literal = null;
            }
        }

        /* funciones para convertir los numeros a literales */

        private static String getUnidades(String numero)
        {   // 1 - 9            
            //si tuviera algun 0 antes se lo quita -> 09 = 9 o 009=9
            String num = numero.Substring(numero.Length - 1);
            return UNIDADES[int.Parse(num)];
        }

        private static String getDecenas(String num)
        {// 99                        
            int n = int.Parse(num);
            if (n < 10)
            {//para casos como -> 01 - 09
                return getUnidades(num);
            }
            else if (n > 19)
            {//para 20...99
                String u = getUnidades(num);
                if (u.Equals(""))
                { //para 20,30,40,50,60,70,80,90
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8];
                }
                else
                {
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8] + "y " + u;
                }
            }
            else
            {//numeros entre 11 y 19
                return DECENAS[n - 10];
            }
        }

        private static String getCentenas(String num)
        {// 999 o 099
            if (int.Parse(num) > 99)
            {//es centena
                if (int.Parse(num) == 100)
                {//caso especial
                    return " cien ";
                }
                else
                {
                    return CENTENAS[int.Parse(num.Substring(0, 1))] + getDecenas(num.Substring(1));
                }
            }
            else
            {//por Ej. 099 
                //se quita el 0 antes de convertir a decenas
                return getDecenas(int.Parse(num) + "");
            }
        }

        private static String getMiles(String numero)
        {// 999 999
            //obtiene las centenas
            String c = numero.Substring(numero.Length - 3);
            //obtiene los miles
            String m = numero.Substring(0, numero.Length - 3);
            String n = "";
            //se comprueba que miles tenga valor entero
            if (int.Parse(m) > 0)
            {
                n = getCentenas(m);
                return n + "mil " + getCentenas(c);
            }
            else
            {
                return "" + getCentenas(c);
            }

        }

        private static String getMillones(String numero)
        { //000 000 000        
            //se obtiene los miles
            String miles = numero.Substring(numero.Length - 6);
            //se obtiene los millones
            String millon = numero.Substring(0, numero.Length - 6);
            String n = "";
            if (millon.Length > 1)
            {
                n = getCentenas(millon) + "millones ";
            }
            else
            {
                n = getUnidades(millon) + "millon ";
            }
            return n + getMiles(miles);
        }

        public static void Mostrar_Mensage(string psMessage, Page psPage)
        {
            string script = "alert('" + psMessage + "')";
            ScriptManager.RegisterStartupScript(psPage, psPage.GetType(), "msgBox", script, true);

        }

        public static string Convert_First_Letter(string texto)
        {
            string str = "";
            str = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(texto);

            return str;
        }

        public static string ConvertMayus(string psCadena)
        {
            string cadena;
            cadena = psCadena.ToUpper();
            return cadena;
        }

        public static string ConvertMinus(string psCadena)
        {
            string cadena;
            cadena = psCadena.ToLower();
            return cadena;
        }

        public static string ConvertString(Object pParametro)
        {
            string lsCadenaNueva;
            //lsCadenaNueva = Convert.ToString (pParametro );

            //lsCadenaNueva =  LTrim(RTrim(CStr(pParametro)))
            lsCadenaNueva = Convert.ToString(pParametro);


            return lsCadenaNueva;

        }

        public static string FormatFecha(string pdate)
        {
            DateTime psFecha;
            string lsFecha;
            psFecha = Convert.ToDateTime(pdate);
            string day = psFecha.Day.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            }

            string Month = psFecha.Month.ToString();

            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }

            string Year = psFecha.Year.ToString();

            lsFecha = Year + "-" + Month + "-" + day;


            return lsFecha;
        }

        public static string Convert_Mes_Letra(string pdate)
        {
            DateTime psFecha;
            string lsFecha;
            psFecha = Convert.ToDateTime(pdate);
            string day = psFecha.Day.ToString();

            if (day.Length == 1)
            {
                day = "0" + day;
            }

            string Month = psFecha.Month.ToString();

            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            switch (Month)
            {
                case "01":
                    Month = "Enero";
                    break;

                case "02":
                    Month = "Febrero";
                    break;

                case "03":
                    Month = "Marzo";
                    break;

                case "04":
                    Month = "Abril";
                    break;

                case "05":
                    Month = "Mayo";
                    break;

                case "06":
                    Month = "Junio";
                    break;

                case "07":
                    Month = "Julio";
                    break;

                case "08":
                    Month = "Agosto";
                    break;

                case "09":
                    Month = "Septiembre";
                    break;

                case "10":
                    Month = "Octubre";
                    break;

                case "11":
                    Month = "Noviembre";
                    break;

                case "12":
                    Month = "Diciembre";
                    break;
            }

            string Year = psFecha.Year.ToString();

            lsFecha = day + " de " + Month + " de " + Year;


            return lsFecha;
        }
        
        public static double Convert_Double(object pObjeto)
        {
            double ldNumero;

            ldNumero = Convert.ToDouble(pObjeto);

            return ldNumero;
        }

        public static int ConvertInteger(Object pObjeto)
        {
            int liNumero;
            liNumero = Convert.ToInt32(pObjeto);

            return liNumero;
        }

        public static string Convert_Decimales(string psCantidad)
        {
            if ((psCantidad == null) | (psCantidad == "")) psCantidad = "0";

            string cantidad_decimal = "";

            string[] Decimales = psCantidad.Split(new Char[] { '.' });


            if (Decimales.Length == 1)
            {
                cantidad_decimal = psCantidad + ".00";
            }
            else
            {
                if (Decimales[1].Length > 2)
                {
                    cantidad_decimal = Decimales[0] + "." + Decimales[1].Substring(0, 2);
                }
                else
                {
                    cantidad_decimal = psCantidad;
                }

            }

            return cantidad_decimal;

        }

        public static bool valida_session(string session)
        {
            bool lbBandera;

            if (session == null)
            {
                lbBandera = true;

            }
            else
            {
                lbBandera = false;
            }
            return lbBandera;
        }

        public static bool IsNumeric(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        public static void LlenarTreeViews(string psPadre, TreeView ptvObject, Boolean psBandera, string psTipo, string psAplicativo, string psRol = "", string psUbicacion = "")
        {
            string lsPadre;
            string lsModulo;
            string lsDescripcion;
            Boolean lbBandera;
      

            int i = 0;
            switch (psAplicativo)
            {
                case "SMAF":
                    switch (psTipo)
                    {
                        case "Menu":

                            datasetArbol = MngNegocioMenu.MngDatosMenu(psRol, psPadre);
                            tblTabla = new DataTable();
                            tblTabla = datasetArbol.Tables["DataSetArbol"];

                            break;

                        case "Partidas":

                            datasetArbol.Clear();

                            datasetArbol = MngNegocioPartidas.MngDatosPartidas(psPadre);
                            tblTabla = new DataTable();
                            tblTabla = datasetArbol.Tables["DataSetArbol"];
                            break;

                    }

                    break;
                case "DGAIPP":
                    /*
                        datasetArbol = MngNegocioMenu.MngDatosMenudgaipp(psRol, psPadre);
                        tblTabla = new DataTable();
                        tblTabla = datasetArbol.Tables["DataSetArbol"];
                        break;*/
                    switch (psTipo)
                    {
                        case "Seguimiento":
                            datasetArbol = MngNegocioMinutario.ReturnDataSet(psPadre);
                            tblTabla = new DataTable();
                            tblTabla = datasetArbol.Tables["DataSetArbol"];
                            break;
                    }

                    break;
            }

            clsTreeview objTreeview = new clsTreeview();

            foreach (DataRow lRegistro in tblTabla.Rows)
            {

                lsPadre = Convert.ToString(lRegistro["PADRE"]);
                lsModulo = Convert.ToString(lRegistro["MODULO"]);
                lsDescripcion = Convert.ToString(lRegistro["DESCRIPCION"]);

                if (i == 0)
                {
                    lbBandera = true;
                    objTreeview.ContruirMenu(lsModulo, lsDescripcion, ptvObject, lbBandera, psRol, psTipo, psAplicativo);
                    i++;
                }
                else
                {
                    lbBandera = false;
                    objTreeview.ContruirMenu(lsModulo, lsDescripcion, ptvObject, lbBandera, psRol, psTipo, psAplicativo);
                    i++;
                }
            }

        }

        public static void LlenarTreeView_DGAIPP(string psPadre, TreeView ptvObject, Boolean psBandera, string psRol = "1")
        {
            string lsPadre;
            string lsModulo;
            string lsDescripcion;
            Boolean lbBandera;
            int i = 0;
            datasetArbol = MngNegocioMenu.MngDatosMenudgaipp(psRol, psPadre);
            tblTabla = new DataTable();
            tblTabla = datasetArbol.Tables["DataSetArbol"];

            clsPartidas objpartidas = new clsPartidas();
            clsMenu objMenu = new clsMenu();

            foreach (DataRow lRegistro in tblTabla.Rows)
            {

                lsPadre = Convert.ToString(lRegistro["PADRE"]);
                lsModulo = Convert.ToString(lRegistro["MODULO"]);
                lsDescripcion = Convert.ToString(lRegistro["DESCRIPCION"]);

                if (i == 0)
                {
                    lbBandera = true;
                    if (!psBandera)
                    {

                        objMenu.ContruirMenuDGAIPP(lsModulo, lsDescripcion, ptvObject, lbBandera, psRol);
                    }
                    else
                    {
                        objpartidas.ContruirPartidas(lsModulo, lsDescripcion, ptvObject, lbBandera);
                    }

                    i++;
                }
                else
                {
                    lbBandera = false;
                    if (!psBandera)
                    {
                        objMenu.ContruirMenuDGAIPP(lsModulo, lsDescripcion, ptvObject, lbBandera, psRol);
                    }
                    else
                    {
                        objpartidas.ContruirPartidas(lsModulo, lsDescripcion, ptvObject, lbBandera);
                    }

                    i++;
                }
            }

        }

        public static void LlenarTreeView(string psPadre, TreeView ptvObject, Boolean psBandera, string psRol = "INVEST")
        {
            string lsPadre;
            string lsModulo;
            string lsDescripcion;
            Boolean lbBandera;
            int i = 0;

            if (!psBandera)
            {
                datasetArbol = MngNegocioMenu.MngDatosMenu(psRol, psPadre);
                tblTabla = new DataTable();
                tblTabla = datasetArbol.Tables["DataSetArbol"];
            }
            else
            {
                datasetArbol.Clear();

                datasetArbol = MngNegocioPartidas.MngDatosPartidas(psPadre);
                tblTabla = new DataTable();
                tblTabla = datasetArbol.Tables["DataSetArbol"];
            }

            clsPartidas objpartidas = new clsPartidas();
            clsMenu objMenu = new clsMenu();

            foreach (DataRow lRegistro in tblTabla.Rows)
            {

                lsPadre = Convert.ToString(lRegistro["PADRE"]);
                lsModulo = Convert.ToString(lRegistro["MODULO"]);
                lsDescripcion = Convert.ToString(lRegistro["DESCRIPCION"]);

                if (i == 0)
                {
                    lbBandera = true;
                    if (!psBandera)
                    {

                        objMenu.ContruirMenu(lsModulo, lsDescripcion, ptvObject, lbBandera, psRol);
                    }
                    else
                    {
                        objpartidas.ContruirPartidas(lsModulo, lsDescripcion, ptvObject, lbBandera);
                    }

                    i++;
                }
                else
                {
                    lbBandera = false;
                    if (!psBandera)
                    {
                        objMenu.ContruirMenu(lsModulo, lsDescripcion, ptvObject, lbBandera, psRol);
                    }
                    else
                    {
                        objpartidas.ContruirPartidas(lsModulo, lsDescripcion, ptvObject, lbBandera);
                    }

                    i++;
                }
            }


        }

        public static void ConstruyeMenu(string lsRol, TreeView tvObject)
        {
            string lsPadre;
            string lsModulo;
            string lsDescripcion;
            Boolean lbBandera;
            int i = 0;

            datasetArbol = MngNegocioMenu.MngDatosMenu(lsRol, "0");
            tblTabla = new DataTable();
            tblTabla = datasetArbol.Tables["DataSetArbol"];

            clsMenu objMenu = new clsMenu();

            foreach (DataRow lRegistro in tblTabla.Rows)
            {

                lsPadre = Convert.ToString(lRegistro["PADRE"]);
                lsModulo = Convert.ToString(lRegistro["MODULO"]);
                lsDescripcion = Convert.ToString(lRegistro["DESCRIPCION"]);

                if (i == 0)
                {
                    lbBandera = true;
                    objMenu.ContruirMenu(lsModulo, lsDescripcion, tvObject, lbBandera, lsRol);
                    i++;
                }
                else
                {
                    lbBandera = false;
                    objMenu.ContruirMenu(lsModulo, lsDescripcion, tvObject, lbBandera, lsRol);
                    i++;
                }
            }
        }

        //METODO QUE OTIENE JERARQUICO
        public static string[] Obtiene_Jerarquico(Usuario poUsuario)
        {
            string Ubicacion_Jerarquico;
            string[] Headers = new string[2];

            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(poUsuario.Ubicacion);

            if (oDireccionTipo.Descripcion == Dictionary.CENTROS_INVESTIGACION)
            {
                string lsusuarioVoBo="";
                lsusuarioVoBo= MngNegocioUsuarios.Obten_Usuario(Dictionary.ADMINISTRADOR, poUsuario.Ubicacion);
                string lsusuarioAut = "";
                lsusuarioAut = MngNegocioUsuarios.Obten_Usuario(Dictionary.JEFE_CENTRO, poUsuario.Ubicacion);
                if (lsusuarioVoBo != "")
                {
                    Headers[0] = lsusuarioVoBo;
                }
                else if(lsusuarioAut!="")
                {
                    Headers[0] = lsusuarioAut;

                }

                if ((poUsuario.Rol == Dictionary.ADMINISTRADOR) | (poUsuario.Rol == Dictionary.INVESTIGADOR) | (poUsuario.Rol == Dictionary.ENLACE))
                {
                    Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.JEFE_CENTRO, poUsuario.Ubicacion);
                }
                else if ((poUsuario.Rol == Dictionary.JEFE_CENTRO) | (poUsuario.Rol == Dictionary.SUBDIRECTOR_ADJUNTO) | (poUsuario.Rol == Dictionary.JEFE_DEPARTAMENTO))
                {
                    Ubicacion_Jerarquico = MngNegocioDependencia.Obtiene_Direccion(poUsuario.Ubicacion);
                    Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, Ubicacion_Jerarquico);
                }

            }
            else if ((oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES))
            {
                //Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "5200", Dictionary.PUESTO_FINANCIERO);
                Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, Dictionary.SRF);
                if ((oDireccionTipo.Codigo == "4000"))
                {
                    Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Descripcion);
                }
                else if ((oDireccionTipo.Codigo == "3000") | (oDireccionTipo.Codigo == "2000"))
                {
                    if (poUsuario.Ubicacion != Dictionary.SUBACUA)
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo);
                    }
                    else
                    {
                        if (poUsuario.Rol == Dictionary.SUBDIRECTOR_ADJUNTO)
                        {
                            Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, oDireccionTipo.Codigo);
                        }
                        else 
                        {
                            Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, poUsuario.Ubicacion);
                    
                        }

                        
                    }
                    
                }
                else if ((oDireccionTipo.Codigo == "5000"))
                {
                    if ((poUsuario.Rol == Dictionary.SUBDIRECTOR_ADJUNTO))
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, oDireccionTipo.Codigo);
                    }
                    else if ((poUsuario.Rol == Dictionary.INVESTIGADOR) | (poUsuario.Rol == Dictionary.JEFE_DEPARTAMENTO) | (poUsuario.Rol == Dictionary.ENLACE))
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, poUsuario.Ubicacion);
                    }
                }
                else if ((oDireccionTipo.Codigo == "1000"))
                {
                    if (poUsuario.Rol == Dictionary.SUBDIRECTOR_ADJUNTO)
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_GRAL, oDireccionTipo.Codigo);
                    }
                    else
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, poUsuario.Ubicacion);
                    }
                }
                else if (oDireccionTipo.Codigo == "6000")
                {
                    Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_JURIDICO, oDireccionTipo.Codigo);
                }
            }
            else if ((oDireccionTipo.Descripcion == Dictionary.DIRECCIONES_ADJUNTAS) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_ADMON) | (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JURIDICA))
            {
                if ((poUsuario.Rol == Dictionary.DIRECTOR_ADJUNTO) | (poUsuario.Rol == Dictionary.DIRECTOR_ADMINISTRACION) | (poUsuario.Rol == Dictionary.DIRECTOR_JURIDICO))
                {
                    //Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "5200", Dictionary.PUESTO_FINANCIERO);
                    Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, Dictionary.SRF);
                    Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_GRAL, "1000");
                }
                else
                {
                    //Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "5200", Dictionary.PUESTO_FINANCIERO);
                    Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, Dictionary.SRF);

                    if (oDireccionTipo.Descripcion == Dictionary.DIRECCIONES_ADJUNTAS)
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, poUsuario.Ubicacion);
                    }
                    else if (oDireccionTipo.Descripcion == Dictionary.DIRECCION_ADMON)
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION);
                    }
                    else if (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JURIDICA)
                    {
                        Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_JURIDICO, poUsuario.Ubicacion);
                    }
                    else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
                    {
                        //    Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_JURIDICO,oDireccionTipo.Codigo );
                    }

                }

            }
            else if (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JEFE)
            {
                //Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "5200", Dictionary.PUESTO_FINANCIERO);
                Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, Dictionary.SRF);
                Ubicacion_Jerarquico = MngNegocioDependencia.Obtiene_Direccion(oDireccionTipo.Codigo);
                Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_GRAL, poUsuario.Ubicacion);
            }
            else
            {
                //Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, "5200", Dictionary.PUESTO_FINANCIERO);
                Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.SUBDIRECTOR_ADJUNTO, Dictionary.SRF);
                Headers[1] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, "5000");
            }

            if (((Headers[0] == null) | (Headers[0] == Dictionary.CADENA_NULA)) && ((Headers[1] != null) | (Headers[1] != Dictionary.CADENA_NULA)))
            {

                if ((oDireccionTipo.Codigo == Dictionary.DG) | (oDireccionTipo.Codigo == Dictionary.SPDG) | (oDireccionTipo.Codigo == Dictionary.SIDG) | (oDireccionTipo.Codigo == Dictionary.DGAIA) | (oDireccionTipo.Codigo == Dictionary.SUBACUA) | (oDireccionTipo.Codigo == Dictionary.DGAIPA) | (oDireccionTipo.Codigo == Dictionary.DGAIPP) | (oDireccionTipo.Codigo == Dictionary.DGAA) | (oDireccionTipo.Codigo == Dictionary.SINF) | (oDireccionTipo.Codigo == Dictionary.SRF) | (oDireccionTipo.Codigo == Dictionary.SRM) | (oDireccionTipo.Codigo == Dictionary.SRH) | (oDireccionTipo.Codigo == Dictionary.DGAJ) | (oDireccionTipo.Codigo == Dictionary.SUBDJ))
                {

                    Headers[0] = MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, Dictionary.DGAA);
                }
                else
                {
                    Headers[0] = Headers[1];
                }
                
            }
            else if (((Headers[1] == null) | (Headers[1] == Dictionary.CADENA_NULA)) && ((Headers[0] != null) | Headers[0] != Dictionary.CADENA_NULA))
            {
                Headers[1] = Headers[0];
            }

            return Headers;
        }

        public static string[] Genera_Descripcion_Vehiculo(string psClaseT, string psTipoT, string psDepVehiculo, string psVehiculo)
        {
            string[] DatosVehiculoMixto1 = new string[3];
            string[] DatosVehiculoMixto = new string[3];
            string[] DatosVehiculo = new string[3];

            string[] Descrip_Vehiculo = new string[9];

            string claseT, tipoT;

            switch (psTipoT)
            {
                case "NUL": //vehiculo oficial
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "VO": //vehiculo oficial
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "AB"://autobus
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "AP"://auto personal
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "AC": //Embarcacion
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "AE": //avion
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "AV": //AVION - VEHICULO OFICIAL
                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);

                    break;

                case "VB": //vehiculo oficial - autobus
                    claseT = "TER";
                    tipoT = "AB";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);

                    break;

                case "VP": //vehiculo oficial - AUTO PROPIO
                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);

                    break;

                case "VA"://vehiculo oficial - embarcacion
                    claseT = "ACU";
                    tipoT = "AC";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;


                case "VAA": //VEHICULO OFICIAL - AVION - ACUATICO

                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "ACU";
                    tipoT = "AC";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);

                    break;

                case "VAP": //VEHICULO OFICIAL - AUTOBUS - AUTO PERSONAL
                    claseT = "TER";
                    tipoT = "AB";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
                    break;

                case "BA"://Autobus Aereo
                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "AB";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "ABP": //AUTOBUS  - AUTO PROPIO
                    claseT = "TER";
                    tipoT = "AB";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "AP";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "BEM": //AUTOBUS  - EMBARCACION
                    claseT = "TER";
                    tipoT = "AB";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "ACU";
                    psTipoT = "AC";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "AAA"://AUTOBUS - AVION - ACUATICO
                    claseT = "TER";
                    tipoT = "AB";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "ACU";
                    psTipoT = "AC";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "PP": //AUTO PROPIO - AVION
                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "AER";
                    psTipoT = "AE";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "APE": //AUTO PROPIO   - EMBARCACION
                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "ACU";
                    psTipoT = "AC";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "PAA"://AUTO PROPIO - AVION -ACUATICO
                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "ACU";
                    psTipoT = "AC";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "VAB": //VEHICULO OFICIAL - AVION - AUTOBUS 
                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "TER";
                    tipoT = "AB";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;


                case "OAP": //VEHICULO OFICIAL - AVION - AUTO PROPIO
                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "VO";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

                case "AAP": //AUTOBUS - AVION - AUTO PROPIO
                    claseT = "AER";
                    tipoT = "AE";
                    DatosVehiculoMixto1 = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    claseT = "TER";
                    tipoT = "AP";
                    DatosVehiculoMixto = MngNegocioTransporte.Descrip_Trans(claseT, tipoT, psDepVehiculo, "");

                    psClaseT = "TER";
                    psTipoT = "AB";
                    DatosVehiculo = MngNegocioTransporte.Descrip_Trans(psClaseT, psTipoT, psDepVehiculo, "");
                    break;

            }

            int sec = 0;

            if (DatosVehiculoMixto1[0] != null)
            {
                for (int i = 0; i < DatosVehiculoMixto1.Length; i++)
                {
                    Descrip_Vehiculo[i] = DatosVehiculoMixto1[i];
                    sec = i;
                }
            }

            if ((DatosVehiculoMixto[0] != null))
            {
                if ((Descrip_Vehiculo[sec] == null))// | (Descrip_Vehiculo[sec] != " "))
                {
                    for (int i = 0; i < DatosVehiculoMixto.Length; i++)
                    {
                        Descrip_Vehiculo[i] = DatosVehiculoMixto[i];
                        sec = i;
                    }

                }
                else
                {
                    for (int i = 0; i < DatosVehiculoMixto.Length; i++)
                    {
                        sec += 1;
                        Descrip_Vehiculo[sec] = DatosVehiculoMixto[i];
                    }
                }

            }


            if (DatosVehiculo[0] != null)
            {
                if ((Descrip_Vehiculo[sec] == null))//| (Descrip_Vehiculo[sec] != Dictionary.CADENA_NULA) | (Descrip_Vehiculo[sec] != " "))
                {
                    for (int i = 0; i < DatosVehiculo.Length; i++)
                    {

                        Descrip_Vehiculo[sec] = DatosVehiculo[i];
                        sec = i;
                    }

                }
                else
                {
                    for (int i = 0; i < DatosVehiculo.Length; i++)
                    {
                        sec += 1;
                        Descrip_Vehiculo[sec] = DatosVehiculo[i];
                    }
                }
            }

            return Descrip_Vehiculo;

        }

        public static void Activa_Paneles(Panel poPanel, bool pbVisible = true, bool pbEnabled = true)
        {
            poPanel.Visible = pbVisible;
            poPanel.Enabled = pbEnabled;
        }

        public static void Llena_Lista(DropDownList pdplList, string psLista)
        {
            string lsTipo;
            string lsDescripcion;
            string[] lsCadena;
            lsCadena = psLista.Split(new Char[] { '|' });

            List<Entidad> TipoUsuario = new List<Entidad>();

            for (int i = 0; i < lsCadena.Length; i++)
            {
                Entidad objetoEntidad = new Entidad();
                lsTipo = ConvertString(i + 1);
                lsDescripcion = lsCadena[i];

                objetoEntidad.Codigo = lsTipo;
                objetoEntidad.Descripcion = lsDescripcion;
                TipoUsuario.Add(objetoEntidad);
            }

            pdplList.DataSource = TipoUsuario;
            pdplList.DataTextField = Dictionary.DESCRIPCION;
            pdplList.DataValueField = Dictionary.CODIGO;
            pdplList.DataBind();

        }

        /// <summary>
        /// Carga capitulos disponibles para inapesca
        /// </summary>
        /// <param name="pdplList"></param>
        public static void Llena_Capitulo(DropDownList pdplList)
        {
            string lsTipo = "";
            string lsDescripcion = "";
            List<Entidad> Capitulos = new List<Entidad>();

            for (int i = 0; i < 6; i++)
            {
                Entidad objetoEntidad = new Entidad();
                if (i == 0)
                {
                    lsTipo = "";
                    lsDescripcion = "= S E L E C C I O N E = ";
                }
                else if (i == 1)
                {
                    lsTipo = "2000";
                    lsDescripcion = "Capitulo 2000";
                }
                else if (i == 2)
                {
                    lsTipo = "3000";
                    lsDescripcion = "Capitulo 3000";
                }
                else if (i == 3)
                {
                    lsTipo = "4000";
                    lsDescripcion = "Capitulo 4000";
                }
                else if (i == 4)
                {
                    lsTipo = "5000";
                    lsDescripcion = "Capitulo 5000";
                }
                else if (i == 4)
                {
                    lsTipo = "6000";
                    lsDescripcion = "Capitulo 6000";
                }
                objetoEntidad.Codigo = lsTipo;
                objetoEntidad.Descripcion = lsDescripcion;
                Capitulos.Add(objetoEntidad);
            }

            pdplList.DataSource = Capitulos;
            pdplList.DataTextField = "Descripcion";
            pdplList.DataValueField = "Codigo";
            pdplList.DataBind();

        }

        public static string Crea_ReferenciaBancaria(string psArchivo, string psImporte, string psFecha)
        {
            string RefBancaria = "";
            string FechaCondensada = "";
            string ImporteCondensado = "";
            string DigVerifGlobal = "";
            string RefBancariaCompleta = "";

            // Empieza Referencia
            string Referencia = Convert.ToString(psArchivo);
            Referencia = Referencia.Replace(".pdf", "");
            Referencia = Referencia.Replace(".PDF", "");
            Referencia = Referencia.Replace("-", "");
            // Termina Referencia

            // Empieza Fecha Condensada
            DateTime FechaVencimiento = Convert.ToDateTime(psFecha);

            int sDia = 0;
            int sMes = 0;
            int sAnio = 0;
            sDia = Convert.ToInt16(FechaVencimiento.Day);
            sMes = Convert.ToInt16(FechaVencimiento.Month);
            sAnio = Convert.ToInt16(FechaVencimiento.Year);

            sAnio = ((sAnio - 2013) * 372);
            sMes = ((sMes - 1) * 31);
            sDia = sDia - 1;

            FechaCondensada = Convert.ToString(sAnio + sMes + sDia);
            // termina Fecha Condensada

            // Empieza ImporteCondensado
            string Importe = Convert.ToString(psImporte);

            char[] MyChar = { '$', ' ', ',' };

            double ConvertImporte = Convert.ToDouble(Importe.TrimStart(MyChar));
            ConvertImporte = Math.Round(ConvertImporte, 2);


            string cadena = string.Format("{0:0.00}", ConvertImporte);
            cadena = cadena.Replace(".", "");



            char[] charArray = cadena.ToCharArray();
            Array.Reverse(charArray);
            new string(charArray);

            string cadena2 = Convert.ToString(new string(charArray));

            int contador = 0;
            int SumaProducto = 0;

            for (int i = 0; i < cadena2.Length; i++)
            {

                contador++;

                int Numero = Convert.ToInt16(cadena2.Substring(i, 1));

                if (contador == 1)
                {
                    Numero = Numero * 7;
                    SumaProducto = SumaProducto + Numero;
                }
                if (contador == 2)
                {
                    Numero = Numero * 3;
                    SumaProducto = SumaProducto + Numero;
                }
                if (contador == 3)
                {
                    Numero = Numero * 1;
                    SumaProducto = SumaProducto + Numero;
                    contador = 0;
                }

            }

            ImporteCondensado = Convert.ToString(SumaProducto % 10);
            // Termina ImporteCondensado
            RefBancaria = Referencia + FechaCondensada + ImporteCondensado + "2";


            // Empieza DigVerifGlobal
            RefBancariaCompleta = RefBancaria;

            RefBancaria = RefBancaria.Replace("A", "1");
            RefBancaria = RefBancaria.Replace("B", "2");
            RefBancaria = RefBancaria.Replace("C", "3");
            RefBancaria = RefBancaria.Replace("D", "4");
            RefBancaria = RefBancaria.Replace("E", "5");
            RefBancaria = RefBancaria.Replace("F", "6");
            RefBancaria = RefBancaria.Replace("G", "7");
            RefBancaria = RefBancaria.Replace("H", "8");
            RefBancaria = RefBancaria.Replace("I", "9");
            RefBancaria = RefBancaria.Replace("J", "1");
            RefBancaria = RefBancaria.Replace("K", "2");
            RefBancaria = RefBancaria.Replace("L", "3");
            RefBancaria = RefBancaria.Replace("M", "4");
            RefBancaria = RefBancaria.Replace("N", "5");
            RefBancaria = RefBancaria.Replace("O", "6");
            RefBancaria = RefBancaria.Replace("P", "7");
            RefBancaria = RefBancaria.Replace("Q", "8");
            RefBancaria = RefBancaria.Replace("R", "9");
            RefBancaria = RefBancaria.Replace("S", "2");
            RefBancaria = RefBancaria.Replace("T", "3");
            RefBancaria = RefBancaria.Replace("U", "4");
            RefBancaria = RefBancaria.Replace("V", "5");
            RefBancaria = RefBancaria.Replace("W", "6");
            RefBancaria = RefBancaria.Replace("X", "7");
            RefBancaria = RefBancaria.Replace("Y", "8");
            RefBancaria = RefBancaria.Replace("Z", "9");



            char[] charArray2 = RefBancaria.ToCharArray();
            Array.Reverse(charArray2);
            new string(charArray2);

            string RefBancaria2 = Convert.ToString(new string(charArray2));

            int contador2 = 0;
            int SumaMultiplicacion = 0;

            for (int i = 0; i < RefBancaria2.Length; i++)
            {
                contador2++;

                int Numero = Convert.ToInt16(RefBancaria2.Substring(i, 1));

                if (contador2 == 1)
                {
                    Numero = Numero * 11;
                    SumaMultiplicacion = SumaMultiplicacion + Numero;
                }
                if (contador2 == 2)
                {
                    Numero = Numero * 13;
                    SumaMultiplicacion = SumaMultiplicacion + Numero;
                }
                if (contador2 == 3)
                {
                    Numero = Numero * 17;
                    SumaMultiplicacion = SumaMultiplicacion + Numero;
                }
                if (contador2 == 4)
                {
                    Numero = Numero * 19;
                    SumaMultiplicacion = SumaMultiplicacion + Numero;

                }
                if (contador2 == 5)
                {
                    Numero = Numero * 23;
                    SumaMultiplicacion = SumaMultiplicacion + Numero;
                    contador2 = 0;
                }

            }

            int iDVG = (SumaMultiplicacion % 97) + 1;

            if (iDVG < 10)
            {
                DigVerifGlobal = "0" + Convert.ToString(iDVG);
            }
            else
            {
                DigVerifGlobal = Convert.ToString(iDVG);
            }

            // termina DigVerifGlobal

            RefBancariaCompleta = RefBancariaCompleta + DigVerifGlobal;   // imprime referencia Completa

            return RefBancariaCompleta;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";
            for (int v = 0; v < sinsignos.Length; v++)
            {
                string i = consignos.Substring(v, 1);
                string j = sinsignos.Substring(v, 1);
                str = str.Replace(i, j);
            }
            return str.Replace("–", "").Replace(":", "").Replace(";", "").Replace("_", "").Replace("–", "").Replace("<", "").Replace(">", "").Replace("“", "").Replace("=", "").Replace("ª", "").Replace("º", "").Replace("´", "").Replace("¨", "").Replace("`", "");
            //return Regex.Replace(str, @"[^\w\.@-]", "",RegexOptions.); 
        }
        public static Boolean Exp_Regular(string Cadena)
        {
            Boolean Existe=false;
            string patron = "@|\\*|\\|//|--|'|/|\"| drop | select | union | delete | update | alter | insert | from | where | database | table | instance | DROP | SELECT | UNION | DELETE | UPDATE | ALTER | INSERT | FROM | WHERE | DATABASE | TABLE | INSTANCE ";
            //string patron = "@|\\*|\\|//|--|'|/|\"";
            if (Regex.IsMatch(Cadena, patron))
            {
                Existe = true;

            }
            return Existe;
        }
        public static Boolean Exp_Regular2(string Cadena)
        {
            Boolean Existe = false;
            string patron = "\\*|\\|//|--|'|/|\"| drop | select | union | delete | update | alter | insert | from | where | database | table | instance | DROP | SELECT | UNION | DELETE | UPDATE | ALTER | INSERT | FROM | WHERE | DATABASE | TABLE | INSTANCE ";
            if (Regex.IsMatch(Cadena, patron))
            {
                Existe = true;

            }
            return Existe;
        }
        //public static int Obtener_NumeroDias(string FechaI,string FechaF)
        //{
        //    DateTime dFechaI = Convert.ToDateTime(FechaI);
        //    DateTime dFechaF = Convert.ToDateTime(FechaF);
        //    TimeSpan ts = dFechaI - dFechaF;

        //    int difDias = ts.Days;
        //    return difDias;
        //}

    }

}