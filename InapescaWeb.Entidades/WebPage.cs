using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InapescaWeb.Entidades
{
    public class WebPage
    {
        private string lsModulo;
        private string lsDescripcion;
        private string lsPadre;
        private string lsUrl;

        public WebPage()
        {
            lsModulo = string.Empty;
            lsDescripcion = string.Empty;
            lsPadre = string.Empty;
            lsUrl = string.Empty;
        
        }

        public string Modulo
        {
            get { return lsModulo; }
            set { lsModulo = value; }
        }

        public string Descripcion
        {
            get { return lsDescripcion; }
            set { lsDescripcion = value; }
        }

        public string Padre
        {
            get { return lsPadre; }
            set { lsPadre = value; }
        }

        public string URL
        {
            get { return lsUrl; }
            set { lsUrl = value; }
        }

    }
}
