using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
    public class MngNegocioAdscripcion
    {

        public static List<Entidad> ObtieneCrips1(string psTipo = "1", string psDirecc = "")
        {
                    return MngDatosCatalogos.ObtieneCrips1(psTipo, psDirecc);
        }
    
        public static List<Entidad> ObtieneCrips(string psTipo = "1", string psDirecc = "")
        {
            return MngDatosCatalogos.ObtieneCrips(psTipo, psDirecc);
        }
        public static List<Entidad> Obtiene_Adscripcion_Ejecutante(string psUbicacion, string psPeriodo)
        {
            return MngDatosCatalogos.Obtiene_Adscripcion_Ejecutante(psUbicacion, psPeriodo);
        }

        public static string Obtiene_Ubicacion(string psDirecc = "")
        {
            return MngDatosCatalogos.Obtiene_Ubicacion(psDirecc);
        }

        public static List<Entidad> ObtieneAdscripcion(string psDirect = "")
        {
            return MngDatosCatalogos.ObtieneUbicacion(psDirect);
        }
        public static List<Entidad> ObtieneProyectosSustantivos(string psTipo1, string psDirecc, string psReg = "")
        {
            return MngDatosCatalogos.ObtieneProyectosSustantivos(psTipo1, psDirecc, psReg);
        }

        public static Ubicacion ObtieneDatosUbicacion(string psDependencia)
        {
            return MngDatosCatalogos.MngDatosUbicacion(psDependencia);
        }

        }
}
