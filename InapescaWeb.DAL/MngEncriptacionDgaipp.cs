using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace InapescaWeb.DAL
{
 public    class MngEncriptacionDgaipp
    {
         private static byte[] _key = Encoding.ASCII.GetBytes("DgaippWeb");
        private static byte[] _iv = Encoding.ASCII.GetBytes("PacificoWeb.Mysql");

        public static string encryptString(string cadena)
        {
            string cadenaEncriptada = "";

            try
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(cadena);
                byte[] encripted;

                RijndaelManaged cripto = new RijndaelManaged();
                using (MemoryStream ms = new MemoryStream(inputBytes.Length))
                {
                    using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                    {
                        objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                        objCryptoStream.FlushFinalBlock();
                        objCryptoStream.Close();
                    }
                    encripted = ms.ToArray();
                }

                cadenaEncriptada = Convert.ToBase64String(encripted);

                return cadenaEncriptada;
            }
            catch (Exception ex)
            {
                return cadenaEncriptada;
            }
        }


        public static string decripString(string cadena)
        {
            string cadenaDesencriptada = "";

            try
            {
                byte[] inputBytes = Convert.FromBase64String(cadena);
                byte[] resultBytes = new byte[inputBytes.Length];
                string textoLimpio = String.Empty;
                RijndaelManaged cripto = new RijndaelManaged();
                using (MemoryStream ms = new MemoryStream(inputBytes))
                {
                    using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(_key, _iv), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(objCryptoStream, true))
                        {
                            textoLimpio = sr.ReadToEnd();
                        }
                    }
                }
                cadenaDesencriptada = textoLimpio;

                return cadenaDesencriptada;
            }
            catch (Exception ex)
            {
                return cadenaDesencriptada;
            }
        }

    }
    
}
