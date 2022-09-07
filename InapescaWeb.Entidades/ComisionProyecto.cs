using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class ComisionProyecto
    {

        string lsCombustible;
        string lsPeaje;
        string lsPasaje;
        string lsSingladuras;
        string lsTotalviaticos;
        string lsGranTotal;

        public string Combustible
        {
            get { return lsCombustible; }
            set { lsCombustible = value; }
        }

        public string Peaje
        {
            get { return lsPeaje; }
            set { lsPeaje = value; }
        }

        public string Pasaje
        {
            get { return lsPasaje; }
            set { lsPasaje = value; }
        }

        public string Singladuras
        {
            get { return lsSingladuras; }
            set { lsSingladuras = value; }
        }

        public string Totalviaticos
        {
            get { return lsTotalviaticos; }
            set { lsTotalviaticos = value; }
        }

        public string GranTotal
        {
            get { return lsGranTotal; }
            set { lsGranTotal = value; }
        }

    }

}
