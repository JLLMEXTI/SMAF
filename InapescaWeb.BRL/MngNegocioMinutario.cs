using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using System.Data;


namespace InapescaWeb.BRL
{
    public class MngNegocioMinutario
    {
        public static Boolean Update_Estatus_Reservado(string psReservado, string psPeriodo, string psEstatus, string psOficio,bool pBandera = false )
        {
            return MngDatosMinutario.Update_Estatus_Reservado(psReservado, psPeriodo, psEstatus, psOficio, pBandera );
        }
        
        public static DataSet ReturnDataSet(string psPadre)
        {
            return MngDatosMinutario.ReturnDataSet(psPadre);
        }


        public static List<Entidad> Lista_Oficios(string psPeriodo,bool pBandera = false )
        {
            return MngDatosMinutario.Lista_Oficios(psPeriodo, pBandera );
        }

        public static Boolean Update_Oficio(Minutario poMinutario)
        {
            return MngDatosMinutario.Update_Oficio(poMinutario);
        }

        public static Minutario oMinutarioOficio(string psReservado, string psPeriodo = "")
        {
            return MngDatosMinutario.oMinutarioOficio(psReservado, psPeriodo);
        }
        public static string Obtiene_Max_Oficio(string psPeriodo)
        {
            return MngDatosMinutario.Obtiene_Max_Oficio(psPeriodo);
        }

        public static string Obtiene_Max_Secuencia(string Periodo, string psOficio, string psComplemento = "")
        {
            return MngDatosMinutario.Obtiene_Max_Secuencia(Periodo, psOficio, psComplemento);
        }

        public static Boolean Update_Reservado(string psReservado, string Periodo, string psOficio, string psSec, string psComplemento = "", bool pbBandera = false)
        {
            return MngDatosMinutario.Update_Reservado(psReservado, Periodo, psOficio, psSec, psComplemento, pbBandera);
        }

        public static string Obtiene_Max_Reservado(string Periodo)
        {
            return MngDatosMinutario.Obtiene_Max_Reservado(Periodo);
        }

        public static string Obtiene_Max_Reservado(string Periodo, string psOficio, string psComplemento = "")
        {
            return MngDatosMinutario.Obtiene_Max_Reservado(Periodo, psOficio, psComplemento);
        }

        public static Minutario oMinutario(string Periodo, string psOficio, string psComplemento = "")
        {
            return MngDatosMinutario.oMinutario(Periodo, psOficio, psComplemento);
        }
        public static List<Entidad> ListaReservados1(string Periodo, string psOficio, string psReservado = "0", string psComplemento = "", bool bandera = false)
        {
            return MngDatosMinutario.ListaReservados1(Periodo, psOficio,psReservado , psComplemento,bandera);
        }
        public static List<Entidad> ListaReservados(string Periodo, string psOficio, string psComplemento = "",bool pBandera= false )
        {
            return MngDatosMinutario.ListaReservados(Periodo, psOficio, psComplemento, pBandera );
        }

        public static string ObtieneExpediente(string Periodo, string psOficio, string psComplemento = "")
        {
            return MngDatosMinutario.ObtieneExpediente(Periodo, psOficio, psComplemento);
        }

        public static List<Entidad> Lista_Tipos_Oficios(string psRol)
        {
            return MngDatosMinutario.Lista_Tipos_Oficios(psRol);
        }

        public static List<Entidad> Lista_Oficios_Sin_Reservado(string psPeriodo)
        {
            return MngDatosMinutario.Lista_Oficios_Sin_Reservado(psPeriodo);
        }
    }
}
