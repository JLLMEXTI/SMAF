/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosItinerario.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
 * */
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mime;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.IO;
using InapescaWeb;

namespace InapescaWeb.DAL
{
    public class MngDatosItinerario
    {
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static Boolean Update_StatusGral_Comisionado_Itinerario(string psFolio, string psUbicacion, string pscomisionado)
        {
            Boolean lbResultado;
            string Query = "";
            Query =  "          UPDATE crip_comision_itinerario SET ESTATUS = '0',FECHA_EFF = '"+lsHoy  +"'";
            Query += "          where FOLIO_SOLICITUD = '"+psFolio +"' ";
            Query += "          AND DEP_SOLICITUD = '"+ psUbicacion +"'";
            Query += "          AND COMISIONADO = '"+pscomisionado +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return lbResultado;
        }

        /// <summary>
        /// Metodo que actializa estatus de itinerario de vuelo segun filtros
        /// </summary>
        /// <param name="psFolio"></param>
        /// <param name="psUbicacion"></param>
        /// <param name="psUsuario"></param>
        /// <param name="psTipoCom"></param>
        /// <param name="psAerolinea"></param>
        /// <param name="psNumVuelo"></param>
        /// <param name="psFechaVuelo"></param>
        /// <param name="psHoraVuelo"></param>
        /// <param name="psTramo"></param>
        /// <returns></returns>
        public static Boolean Update_Estatus_Itinerario(string psFolio, string psUbicacion, string psUsuario, string psTipoCom, string psAerolinea, string psNumVuelo, string psFechaVuelo, string psHoraVuelo, string psTramo)
        {
            Boolean lbResultado;
            string Query  ="";

            /////////////////////////////////////////////////////
            Query += "update crip_comision_itinerario SET FECHA_EFF = '" + lsHoy  + "', ESTATUS = '0'";
            Query += "    WHERE FOLIO_SOLICITUD = '" + psFolio + "'";
            Query += "  AND DEP_SOLICITUD= '" + psUbicacion + "'";
            Query += " AND COMISIONADO = (";
            Query += "							SELECT DISTINCT A.USUARIO ";
            Query += "								FROM crip_usuarios A,crip_comision B";
            Query += "								WHERE A.ESTATUS = '1'";
            Query += "						        AND CONCAT(A.NOMBRE,' ',A.AP_PAT,' ',A.AP_MAT) =  '" + psUsuario + "'";
            Query += "					AND A.ESTATUS = B.ESTATUS ";
            Query += "			AND A.USUARIO = B.USUARIO ";
            Query += "								AND B.FOLIO = '" + psFolio + "'";
            Query += "								AND B.CLV_DEP = '" + psUbicacion + "'";
            Query += "                 AND B.CLV_TIPO_COM = '" + psTipoCom + "'";
            Query += "					)";
            Query += " AND ESTATUS = '1'";
            Query += "AND AEROLINEA = '" + psAerolinea + "'";
            Query += "   AND NUM_VUELO = '" + psNumVuelo + "'";
            Query += "  AND FECHA_VUELO = '" + psFechaVuelo + "'";
            Query += "  AND HORA_VUELO = '" + psHoraVuelo + "'";
            Query += "  AND TRAMO = '" + psTramo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );


            return lbResultado;

        }


        public static List<Itinerario> Obten_Lista_Itinerarios(string psFolio, string psUbicacion)
        {
            string lsQuery;
            lsQuery = "SELECT  COMISIONADO,AEROLINEA, NUM_VUELO, FECHA_VUELO, HORA_VUELO,TRAMO FROM crip_comision_itinerario";
            lsQuery += "    WHERE ESTATUS = '1'";
            lsQuery += " AND FOLIO_SOLICITUD = '" + psFolio + "'";
            lsQuery += " AND DEP_SOLICITUD = '" + psUbicacion + "'";
            lsQuery += "  ORDER BY SEC_EFF ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery , ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Itinerario> ListaEntidad = new List<Itinerario>();

            while (Reader.Read())
            {
                Itinerario objItinerario = new Itinerario();
                objItinerario.Comisionado = MngDatosUsuarios.Obtiene_Nombre_Completo(Convert.ToString(Reader["COMISIONADO"]));
                objItinerario.Aerolinea = Convert.ToString(Reader["AEROLINEA"]);
                objItinerario.Numero = Convert.ToString(Reader["NUM_VUELO"]);
                objItinerario.Fecha = Convert.ToString(Reader["FECHA_VUELO"]);
                objItinerario.Hora = Convert.ToString(Reader["HORA_VUELO"]);
                objItinerario.Tramo = Convert.ToString(Reader["TRAMO"]);
                ListaEntidad.Add(objItinerario);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return ListaEntidad;
        }

        public static Boolean Inserta_Itinerario(string psFolio, string psUbicacion, string psProovedor, string psAerolinea, string psNumVuelo, string psFechaVuelo, string psHoraVuelo, string psComisionado, string psTramo)
        {
            string secEff = "";
            int secuencia;
            Boolean lbResultado;
            string Query = "SELECT max(A.SEC_EFF)  AS SECUENCIA FROM crip_comision_itinerario A ";
            Query += "          WHERE A.FOLIO_SOLICITUD='" + psFolio + "' AND A.DEP_SOLICITUD = '" + psUbicacion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                secEff = Convert.ToString(Reader["SECUENCIA"]);
             
            }

            if ((secEff == null) | (secEff == ""))
            { 
            secuencia = 1;
            }
            else
            {
                secuencia = clsFunciones.ConvertInteger(secEff) + 1;
            }


             Query = "";
          
            ///////////////////////////////////////////////
            Query = " INSERT INTO crip_comision_itinerario (";
            Query += "		FOLIO_SOLICITUD,";
            Query += "      DEP_SOLICITUD,";
            Query += "      NO_OFICIO,";
            Query += "		PROOVEDOR,";
            Query += "		AEROLINEA,";
            Query += "		NUM_VUELO,";
            Query += "		FECHA_VUELO,";
            Query += "		HORA_VUELO, ";
            Query += "      COMISIONADO,";
            Query += "      FECHA ,";
            Query += "      SEC_EFF,";
            Query += "		FECHA_EFF,";
            Query += "	    ESTATUS,";
            Query += "	    TRAMO";
            Query += "    )";
            Query += " VALUES";
            Query += "    (";
            Query += "	    '" + psFolio + "',";
            Query += "		'" + psUbicacion + "',";
            Query += "      '0',";
            Query += "      '" + psProovedor + "',";
            Query += "		'" + psAerolinea + "',";
            Query += "		'" + psNumVuelo + "',";
            Query += "      '" + psFechaVuelo + "',";
            Query += "		'" + psHoraVuelo + "',";
            Query += "		'" + psComisionado + "',";
            Query += "		'" + lsHoy  + "',";
            Query +=            secuencia +",";
            Query += "		'" + lsHoy  + "',";
            Query += "      '1',";
            Query += "      '" + psTramo + "'";
            Query += "      )";
            
            cmd.Dispose();
             cmd = new MySqlCommand(Query,ConexionMysql );
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }


            MngConexion.disposeConexionSMAF(ConexionMysql );


            return lbResultado;
        }

    }
}
