using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class comprobacion
    {
        static string lsFecha;
        static string lsConcepto;
        static string lsImporte;
        static string lsObservaciones;
        static string lsID;
        
        public string Fecha
        {
            get { return lsFecha; }
            set { lsFecha =value ;}
        }

        public string Concepto
        {
            get { return lsConcepto; }
            set { lsConcepto = value; }
        }

        public string Importe
        {
            get { return lsImporte; }
            set { lsImporte = value; }
        }

        public string Observaciones
        {
            get { return lsObservaciones; }
            set { lsObservaciones = value; }
        }


        public string Id
        {
            get { return lsID; }
            set { lsID = value; }
        }
    }
}
