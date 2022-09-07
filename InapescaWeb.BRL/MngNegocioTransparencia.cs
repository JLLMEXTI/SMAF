using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;


namespace InapescaWeb.BRL
{
    public class MngNegocioTransparencia
    {
        public static List<Entidad> Transparencia_Ampliaciones(string sPeriodo)
        {
            return MngDatosTransparencia.Transparencia_Ampliaciones(sPeriodo);
        }
        public static List<Comision> Obten_Lista_Comision(string psPeriodo)
        {
            return MngDatosTransparencia.Obten_Lista_Comision(psPeriodo);
        }

        public static Usuario Obten_DatosExtras(string psUsuario)
        {
            return MngDatosTransparencia.Obten_DatosExtras(psUsuario);
        }
        public static Entidad Obtiene_Clv_CiudadEstado(string psDependencia)
        {
            return MngDatosTransparencia.Obtiene_Clv_CiudadEstado(psDependencia);
        }
        public static string SumaImporteComprobado(string psArchivo, bool pbBandera = false)
        {
            return MngDatosTransparencia.SumaImporteComprobado(psArchivo, pbBandera);
        }
        public static Comision ImportePorConceptos(string psArchivo)
        {
            return MngDatosTransparencia.ImportePorConceptos(psArchivo);
        }
    }
}
