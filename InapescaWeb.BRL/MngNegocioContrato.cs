using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;
using System.Data;

namespace InapescaWeb.BRL
{
    public class MngNegocioContrato
    {
        public static string Obtiene_Nombre_Completo_AdmCont(string psRol, string psUbicacion)
        {
            return MngDatosContrato.Obtiene_Nombre_Completo_AdmCont(psRol, psUbicacion);
        }

        public static Boolean InsertDetalleCont(SolicitudPSP oPSP, string psUsuario)
        {
            return MngDatosContrato.InsertDetalleCont(oPSP, psUsuario);
        }

        public static DataSet ObtieneDetalleSolicitudesPSP(string psAnio, string psInicio, string psFinal, string psAdscripcion)
        {
            return MngDatosContrato.ObtieneDetalleSolicitudesPSP(psAnio, psInicio, psFinal, psAdscripcion);
        }
    }
}
