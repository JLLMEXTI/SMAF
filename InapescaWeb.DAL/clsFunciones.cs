using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

using InapescaWeb.Entidades;
using System.Globalization;
using System.Text.RegularExpressions;


namespace InapescaWeb.DAL
{
    public class clsFunciones
    {
       // static DataSet datasetArbol;
       // static DataTable tblTabla;
        static clsDictionary Dictionary = new clsDictionary();
        private static String[] UNIDADES = { "", "un ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
        private static String[] DECENAS = {"diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ",
        "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ",
        "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa "};
        private static String[] CENTENAS = {"", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ",
        "setecientos ", "ochocientos ", "novecientos "};

        static private Regex r;
        static String[] Num;
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
            string cantidad_decimal = "";
            string[] Decimales = psCantidad.Split(new Char[] { '.' });


            if (Decimales.Length == 1)
            {
                cantidad_decimal = psCantidad + ".00";
            }
            else
            {
                cantidad_decimal = psCantidad;
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

       
    }
}