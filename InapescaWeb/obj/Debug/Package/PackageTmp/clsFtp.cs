using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InapescaWeb.DAL;
namespace InapescaWeb
{
    public class clsFtp
    {
        public static bool Valida_Folder_ftp(string psFolder = "")
        {
            return MngFtp.Valida_Conexion_ftp(psFolder );
        }

        public static string Crear_Directorio_ftp(string psFolder)
        {
            return MngFtp.creaDirectorio_ftp(psFolder);
        }

    }
}