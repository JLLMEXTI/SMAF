using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;


namespace InapescaWeb.BRL
{
    public class MngNegocioMenu
    {

        public static DataSet MngDatosMenudgaipp(string psRol, string psPadre)
        {
            return InapescaWeb.DAL.MngDatosMenu.ReturnDataSet_DGAIPP (psRol, psPadre);
        }

        public static DataSet MngDatosMenu(string psRol,string psPadre)
        {
            return InapescaWeb.DAL.MngDatosMenu.ReturnDataSet(psRol ,psPadre );
        }

        public static WebPage MngNegocioURLDGAIPP(string psModulo, string psRol)
        {
            return InapescaWeb.DAL.MngDatosMenu.MngdatsUrlsDGAIPP(psModulo, psRol); 
        }

        public static WebPage MngNegocioURL(string psModulo, string psRol)
        {

            return InapescaWeb.DAL.MngDatosMenu.MngDatsUrls(psModulo, psRol); 
        }

    }
}
