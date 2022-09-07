using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class MenuComprobacion
    {

        static string lsFolio;
        static string lsLugar;
        static string lsPeriodo;
        static string lsTotal;
        static string lsEstatus;
        static string lsTipo;
        static string psFechaFinal;
        static string psArchivoAmp;
        static string psNoAmp;

       public MenuComprobacion()
       {
           lsFolio = "";
           lsLugar = "";
           lsPeriodo = "";
           lsTotal = "";
           lsEstatus = "";
           lsTipo = "";
           psFechaFinal = "";
           psArchivoAmp = "";
           psNoAmp = "";
       }

        public string Tipo
        {
            get { return lsTipo; }
            set { lsTipo = value; }
        }

        public string Estatus
        {
            get { return lsEstatus; }
            set { lsEstatus = value; }
        }

        public string Total
        {
            get { return lsTotal; }
            set { lsTotal = value; }
        }

        public string Periodo
        {
            get { return lsPeriodo; }
            set { lsPeriodo = value; }
        }

        public string Lugar
        {
            get { return lsLugar; }
            set { lsLugar = value; }
        }

        public string Folio
        {
            get { return lsFolio; }
            set { lsFolio = value; }
        }

        public string ArchivoAmp
        {
            get { return psArchivoAmp; }
            set { psArchivoAmp = value; }
        }

        public string FechaFinal
        {
            get { return psFechaFinal; }
            set { psFechaFinal = value; }
        }

        public string NoAmp
        {
            get { return psNoAmp; }
            set { psNoAmp = value; }
        }       

    }
}
