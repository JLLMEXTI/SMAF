using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public class GridView
    {
        private string lsComisionado;
        private string lsRFC;
        private string lsAdscripcion;
        private string lsLugar;
        private string lsClv_Ubicacion;
        private string lsClv_Rol;
        private string lsClv_Zona;

        public string Usuario
        {
            get;
            set;
        }

        public string Puesto
        {
            get;
            set;
        }

        public string Comisionado
        {
            get { return lsComisionado; }
            set { lsComisionado = value; }
        }

        public string RFC
        {
            get { return lsRFC; }
            set { lsRFC = value; }
        }

        public string Adscripcion
        {
            get { return lsAdscripcion; }
            set { lsAdscripcion = value; }
        }

        public string Lugar
        {
            get { return lsLugar; }
            set { lsLugar = value; }
        }

        public string Ubicacion
        {
            get { return lsClv_Ubicacion; }
            set { lsClv_Ubicacion = value; }
        }
        public string Rol
        {
            get { return lsClv_Rol; }
            set { lsClv_Rol = value; }
        }
        public string Zona
        {
            get { return lsClv_Zona; }
            set { lsClv_Zona = value; }
        }
     
    }
}
