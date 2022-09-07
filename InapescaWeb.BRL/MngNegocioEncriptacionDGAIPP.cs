using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;

namespace InapescaWeb.BRL
{
 public class MngNegocioEncriptacionDGAIPP
    {
        public static string Encriptacion(string psCadena)
        {
            return MngEncriptacionDgaipp.encryptString(psCadena);
        }

        public static string Decriptacion(string psCadena)
        {
            return MngEncriptacionDgaipp .decripString(psCadena);
        }

    }
}
