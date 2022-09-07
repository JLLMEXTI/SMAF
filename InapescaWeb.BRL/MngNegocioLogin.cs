using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;


namespace InapescaWeb.BRL
{
    public class MngNegocioLogin
    {
        //nuevo metodo de inicio de session
        public static Usuario Acceso_Smaf(string psUbicacion, string psUsuario, string psPassword)
        {
            return MngDatosLogin.Acceso_Smaf(psUbicacion, psUsuario, psPassword);
        }

        public static usuariosDgaipp Acceso_Dgaipp(string psUsuario, string psPassword)
        {
            return MngDatosLogin.Acceso_Dgaipp(psUsuario, psPassword);
        }

        /*
        public static Boolean Session(string psUbicacion, string psUsuario, string psPassword)
        {
            return MngDatosLogin.MngDatosSession(psUbicacion ,psUsuario ,psPassword );
         }
         
        public static Usuario DatosUsuario()
        {
            return MngDatosLogin.MngDatosUsuario();
        }

        public static string Error()
        {
            return MngDatosLogin.ReturnError();
        }

        public static Entidad MngNegocioInfo(string psNivel,string psPuesto, string psPlaza)
        {
            return MngDatosLogin.MngInfo(psNivel, psPuesto, psPlaza);
        }
        */
    }
}
