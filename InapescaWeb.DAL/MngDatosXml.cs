/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.DAL
	FileName:	MngDatosXml.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Diciembre 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosXml
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static string Obtiene_Max(string psUUID, string psRFC)
        {
            string  max = "0";
            string query = "SELECT MAX(SEC_EFF) AS MAX FROM crip_xml_detalle WHERE UUID = '" + psUUID + "' AND RFC = '" + psRFC + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            
            cmd.Connection.Open();
            
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);
            }
           
            if (max == "")
            {
                max = "0";
            }
            
            MngConexion.disposeConexionSMAF( ConexionMysql );

            max  = clsFunciones .ConvertString ( clsFunciones.ConvertInteger (max) + 1);

            return max ;
        }

        public static Boolean Inserta_DetalleXML(string psUsuario, string psUbicacion,string psDocumento, string psXml,string psTimbreFiscal,string psProvedor,string psConcepto,string psPartida,string psFechaTimbrado,string psImporteUnid,string psIva,string psTua,string psisr,string psieps, string pssfp, string psIsh, string psIvaRet,string psTuaRet,string psIsrRet,string psIepsRet,string psSfpRet,string psIshRet,string psSubtotal, string pstotal, string psImpuestosRetenidos,string psimpuestostrasladados)
        {
            Boolean lbResultado;

            string Query = "INSERT INTO crip_xml_detalle ";
                   Query += " (";
                   Query += " USUARIO,";
                   Query += " UBICACION_USUARIO,";
                   Query += " DOCUMENTO,";
                   Query += " `XML`,";
                   Query += " UUID,";
                   Query += "  RFC,";
                   Query += " SEC_EFF,";
                   Query += " ESTATUS,";
                   Query += " CONCEPTO,";
                   Query += " PARTIDA,";
                   Query += " FECHA,";
                   Query += " FECH_EFF,";
                   Query += " IMPORTE,";
                   Query += " IVA,";
                   Query += " TUA,";
                   Query += " ISR,";
                   Query += " IEPS,";
                   Query += " SFP,";
            
                   Query += " ISH,";
                   Query += " IVA_RETENIDO,";
                   Query += " TUA_RETENIDO,";
                   Query += " ISR_RETENIDO,";
                   Query += " IEPS_RETENIDO,";
                   Query += " SFP_RETENIDO,";
                   Query += " ISH_RETENIDO,";
                   
                    Query += " SUBTOTAL,";
                   Query += " TOTAL,";
            
                   Query += " IMPUESTOS_RETENIDOS,";

                   Query += "IMPUESTOS_TRASLADADOS";
                   Query += " )";
                   Query += " VALUES";
                   Query += " (";
                   Query += " '"+psUsuario +"',";
                   Query += " '"  + psUbicacion + "',";
                   Query += " '" + psDocumento + "',";
                   Query += " '" + psXml + "',";
                   Query += " '" + psTimbreFiscal  + "',";
                   Query += " '" + psProvedor + "',";
                   Query += " '" + MngDatosXml.Obtiene_Max (psTimbreFiscal,psProvedor ) +"',";
                   Query += " '1',";
                   Query += " '" + psConcepto  + "',";
                   Query += " '" + psPartida + "',";
                   Query += " '" + psFechaTimbrado + "',";
                   Query += " '" + lsHoy +"',";
                   Query += " '" + psImporteUnid + "',";
                   Query += " '"+ psIva +"',";
                   Query += " '" + psTua+ "',";
                   Query += " '"+ psisr +"',";
                   Query += " '" + psieps+"',";
                   Query +=  "'"+ pssfp+"',";

                   Query += "'" + psIsh  + "',";
                   Query += "'" + psIvaRet + "',";
                   Query += "'" + psTuaRet  + "',";
                   Query += "'" + psIsrRet + "',";
                   Query += "'" + psIepsRet  + "',";
                   Query += "'" + psSfpRet  + "',";
                   Query += "'" + psIshRet + "',";
            
                   Query += " '" + psSubtotal  + "',";

                   Query += " '"+ pstotal +"',";

                   Query += "'" + psImpuestosRetenidos  + "',";
                   Query += " '"+ psimpuestostrasladados +"'";
                   Query  += "  )";

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
    }
}
