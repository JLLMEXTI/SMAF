using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class Partidas
    {
        private string lsID;
        private string lsDescripcion;
        private string lsPadre;

        public Partidas()
        {
            lsID = "";
            lsDescripcion = "";
            lsPadre = "";
        }

        public string ID
        {
            get { return lsID; }
            set { lsID = value; }
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
    }
}
