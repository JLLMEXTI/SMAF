using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;

namespace InapescaWeb.BRL
{
    public class MngNegocioPartidas
    {

        public static DataSet MngDatosPartidas(string psPadre)
        {
            return InapescaWeb.DAL.MngDatosPartidas.ObtienePartidas(psPadre);
        }

        public static List<Entidad> ListaPartidas(string psPadre)
        {
            return InapescaWeb.DAL.MngDatosPartidas.ListaPartidas(psPadre);
        }

        public static List<Entidad> Obtiene_Partidas_Pasajes()
        {
            return InapescaWeb.DAL.MngDatosPartidas.Obtiene_partidas_pasajes();
        }

        public static string ObtieneDescripcionPartida(string psPartida)
        {
            return InapescaWeb.DAL.MngDatosPartidas.ObtieneDescripcionPartida(psPartida);
        }
    }
}
