using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;


namespace InapescaWeb.BRL
{
    public class MngNegocioDirecciones
    {

        public static string NombreDireccion(string psDirec)
        {
            return MngDatosDirecciones.NombreDireccion(psDirec);
        }

        public static List<Entidad> ObtieneDireccion(string psPeriodo)
        {
            return MngDatosDirecciones .ObtieneDireccion(psPeriodo);
        }

        public static List<Entidad> ObtienenDirecciones(string psPeriodo,  bool pbAdjunta = false, bool ptodos = false)
        {
            return InapescaWeb.DAL.MngDatosDirecciones.ObtienenDirecciones(psPeriodo , pbAdjunta,ptodos  );
        }

    }
}
