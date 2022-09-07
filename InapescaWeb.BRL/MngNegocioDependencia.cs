using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
    public class MngNegocioDependencia
    {
        public static List<Entidad> ListaSiglas(string psPeriodo)
        {
            return MngDatosDependencia.ListaSiglas(psPeriodo);
        }

        public static string Obtiene_Pais(string psPais)
        {
            return MngDatosDependencia.Obtiene_Pais(psPais);
        }

        public static Entidad Obtiene_Dependencia_porTipo(string psTipo, string psPeriodo = "")
        {
            return MngDatosDependencia.Obtiene_Dependencia_porTipo(psTipo, psPeriodo);
        }

        public static string Obtiene_Siglas(string psDep)
        {
            return MngDatosDependencia.Obtiene_Siglas(psDep);
        }

        public static string Obtiene_DIrector_Financieros(string psPuestoFinanciero, string psRol)
        {
            return MngDatosDependencia.Obtiene_DIrector_Financieros(psPuestoFinanciero, psRol);
        }

        public static Entidad Obtiene_Tipo_Region(string psDep)
        {
            return MngDatosDependencia.Obtiene_Tipo_Region(psDep);
        }

        public static List<Entidad> ObtieneDep() 
        {
            return InapescaWeb.DAL.MngDatosDependencia.ObtieneSecretaria();
        }

        public static List<Entidad> ObtieneOrg(string psDep)
        {
            return InapescaWeb.DAL.MngDatosDependencia.ObtieneOrganismos(psDep);
        }

        public static List<Entidad> ObtieneCentro(string psSec, string psOrg, string psCentro)
        {
            return InapescaWeb.DAL.MngDatosDependencia.ObtieneCentro(psSec, psOrg, psCentro);
        }

        public static List<Entidad> ObtieneCentro1(string psSec, string psOrg, string psCentro)
        {

            return InapescaWeb.DAL.MngDatosDependencia.ObtieneCentro1(psSec, psOrg, psCentro);
        }

        public static string ObtieneDatosOrganismo(string pssecretaria, string  psOrganismo, string psClave = "")
        {
            return InapescaWeb.DAL.MngDatosDependencia.Obtiene_Organismo(pssecretaria ,psOrganismo , psClave );
        }

        public static string[] ObtieneDatosHeader(string psOrganismo, string psUbicacion)
        {
            return InapescaWeb.DAL.MngDatosDependencia.Header_Mail(psOrganismo, psUbicacion);
        }

        public static string Centro_Descrip(string psUbicacion,bool pbBandera = false )
        {
            return InapescaWeb.DAL.MngDatosDependencia.Descrip_Centro(psUbicacion,pbBandera );
        }
        public static string Descrip_Centro_PERIODO(string psUbicacion, string psPeriodo, bool pbBandera = false)
        {
            return InapescaWeb.DAL.MngDatosDependencia.Descrip_Centro_PERIODO(psUbicacion, psPeriodo, pbBandera);
        }
        public static string Descrip_DepProy(string psUbicacion, string psYear)
        {
            return InapescaWeb.DAL.MngDatosDependencia.Descrip_DepProy(psUbicacion, psYear);
        }

        public static string Obtiene_Direccion(string psDep)
        {
            return MngDatosDependencia.Obtiene_Direccion(psDep);
        }

        public static string Obtiene_Descripcion_Estado(string psEstado)
        {
            return MngDatosDependencia.Obtiene_Descripcion_Estado(psEstado);
        }

        public static string Obtiene_Descripcion_Cuidad(string psCiudad, string psEstado)
        {
            return MngDatosDependencia.Obtiene_Descripcion_Ciudad(psCiudad, psEstado);
        }

        public static string Obtiene_ciudad(string psCiudad)
        {
            return MngDatosDependencia.Obtiene_Ciudad(psCiudad);
        }

        public static List<Entidad> Obtiene_Estados(string psPais)
        {
            return MngDatosDependencia.Obtiene_Estados(psPais);
        }

        public static List<Entidad> Obtiene_ciudades(string psEdo)
        {
            return MngDatosDependencia.Obtiene_ciudades(psEdo);
        }

        public static List<Entidad> ListaSiglasDependencias(string psPeriodo)
        {
            return MngDatosDependencia.ListaSiglasDependencias(psPeriodo);
        }
    }
}
