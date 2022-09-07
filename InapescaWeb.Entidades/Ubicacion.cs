using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class Ubicacion
    {
        private string _lsUbicacion;
        private string _lsDescripcion;
        private string _lsCalle;
        private string _lsNumExt;
        private string _lsNumInt;
        private string _lsColonia;
        private string _lsCp;
        private string _lsCiudad;
        private string _lsEstado;
        private string _lsClavEst;
        private string _lsPais;
        private string _lsTelefono;
        private string _lsEmail;
        private string _lsSecre;
        private string _lsOrganismo;
        private string _lsSiglas;
        private string _lsTipo;
        private string _lsDescCorto;

        public Ubicacion()
        {
            _lsTipo = "";
            _lsUbicacion = "";
            _lsDescripcion = "";
            _lsCalle = "";
            _lsNumExt = "";
            _lsNumInt = "";
            _lsColonia = "";
            _lsCp = "";
            _lsCiudad = "";
            _lsEstado = "";
            _lsClavEst = "";
            _lsPais = "";
            _lsTelefono = "";
            _lsEmail = "";
            _lsSecre = "";
            _lsOrganismo = "";
            _lsSiglas = "";
            _lsDescCorto = "";

        }

        public string Desc_Corto
        {
            get { return _lsDescCorto; }
            set { _lsDescCorto = value; }
        }

        public string Tipo
        {
            get { return _lsTipo; }
            set { _lsTipo = value; }
        }
        public string Siglas
        {
            get { return _lsSiglas; }
            set { _lsSiglas = value; }
        }

        public string Organismo
        {
            get { return _lsOrganismo; }
            set { _lsOrganismo = value; }
        }

        public string Secretaria
        {
            get { return _lsSecre; }
            set { _lsSecre = value; }
        }
        public string Dependencia
        {
            get { return _lsUbicacion; }
            set { _lsUbicacion = value; }
        }

        public string Descripcion
        {
            get { return _lsDescripcion; }
            set { _lsDescripcion = value; }
        }

        public string Calle
        {
            get { return _lsCalle; }
            set { _lsCalle = value; }
        }

        public string NumExt
        {
            get { return _lsNumExt; }
            set { _lsNumExt = value; }
        }

        public string NumInt
        {
            get { return _lsNumInt; }
            set { _lsNumInt = value; }
        }

        public string Colonia
        {
            get { return _lsColonia; }
            set { _lsColonia = value; }
        }

        public string Cp
        {
            get { return _lsCp; }
            set { _lsCp = value; }
        }

        public string Ciudad
        {
            get { return _lsCiudad; }
            set { _lsCiudad = value; }
        }

        public string Estado
        {
            get { return _lsEstado; }
            set { _lsEstado = value; }
        }

        public string Clvestado
        {
            get { return _lsClavEst; }
            set { _lsClavEst = value; }
        }

        public string Pais
        {
            get { return _lsPais; }
            set { _lsPais = value; }
        }

        public string Telefono
        {
            get { return _lsTelefono; }
            set { _lsTelefono = value; }
        }

        public string Email
        {
            get { return _lsEmail; }
            set { _lsEmail = value; }
        }
    }
}
