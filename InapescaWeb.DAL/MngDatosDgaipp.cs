/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.DAL
	FileName:	MngDatosDgaipp.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Mayo 2016
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
    public class MngDatosDgaipp
    {
        static string max;
        static int liNumero;
        // static string Dictionary.FECHA_NULA = "1900-01-01";
        static clsDictionary Dictionary = new clsDictionary();
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());


        public static Boolean Inserta_Oficio_Comision(string psOficio,string psArchivo, string psTipoOficio,string psReservado, string psObjetivo,string psDoc, string psDep,string psComisionado,string psUsuario,string psSec,string psPeriodo)
        {
            Boolean lbResultado;
            string Query = " INSERT INTO dgaipp_minutario  ";
            Query += " ( ";
            Query += " NO_OFICIO , ";
            Query += " ARCHIVO, ";
            Query += " TIPO_OFICIO,";
            Query += " FOLIO_RESERVADO,";
            Query += " EXPEDIENTE,";
            Query += " DOC_REF, ";
            Query += " CLV_DEP, ";
            Query += " DESCRIPCION,";
            Query += " USUARIO , ";
            Query += " REGISTRO, ";
            Query += " FECHA, ";
            Query += " ESTATUS, ";
            Query += " SECUENCIA,";
            Query += " PERIODO "; 
            Query += " )";
            Query += " VALUES";
            Query += " ( ";
            Query += " '" + psOficio + "',";
            Query += " '" + psArchivo +"',";
            Query += " '" + psTipoOficio + "',";
            Query += " '" + psReservado +"',";
            Query += " '"+ psObjetivo +"',";
            Query += " '" + psDoc +"',";
            Query += " '" + psDep +"',";
            Query += " '" + MngDatosDependencia.Descrip_Centro (psDep )+"',";
            Query += " '"+ MngDatosUsuarios.Obtiene_Nombre_Completo ( psComisionado) +"',";
            Query += " '"+ MngDatosUsuarios.Obtiene_Nombre_Completo ( psUsuario) +"',";
            Query += " '" + lsHoy +"',";
            Query += " '1',";
            Query += " '" + psSec +"',";
            Query += " '"+ psPeriodo +"'";
            Query += " ) ";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);

            return lbResultado;
        }

        public static string Obtiene_folio_Max(string psPeriodo)
        {
            string Query = "";
            Query = "SELECT MAX(NO_OFICIO) AS MAX FROM dgaipp_minutario ";
            Query += " WHERE PERIODO =  '"+ psPeriodo +"' ";
            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }

                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp );

            return Convert.ToString(liNumero);
        }

        


    }
}
