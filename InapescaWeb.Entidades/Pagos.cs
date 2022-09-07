using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public class Pagos
    {
        private string lsComisionado;
        private string lsFolio;
        private string lsLugar;
        private string lsPeriodo;
        private string lsUsuario;

        public string Usuario
        {
            get { return lsUsuario; }
            set { lsUsuario = value; }
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
            set { lsFolio  = value; }
        }


       public string Comisionado
        {
            get { return lsComisionado; }
            set { lsComisionado = value; }
        }


    }
}
