/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.DAL
	FileName:	MngDatosComision.cs
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Oficinas Centrales
	Date:		Abril 2020
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
using InapescaWeb;
using System.Globalization;

namespace InapescaWeb.DAL
{
    public class MngDatosProyeccionViaticos
    {
        static string max;
        static int liNumero;
        // static string Dictionary.FECHA_NULA = "1900-01-01";
        static readonly clsDictionary dictionary = new clsDictionary();
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        public static string Dias_Acumulados(string psComisionado, string psPeriodo)
        {
            string dias = "";

            string Query = "SELECT SUM(CANT_DIAS_VIAT) AS DIAS ";
            Query += " FROM crip_proyeccion_viat ";
            Query += " WHERE USUARIO = '" + psComisionado + "' ";
            Query += " AND ESTATUS = 1";
            Query += " AND PERIODO= '" + psPeriodo + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                dias = Convert.ToString(Reader["DIAS"]);
            }

            if (dias == "")
            {
                dias = dictionary.NUMERO_CERO;
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return dias;
        }
        public static string Dias_AcumuladosMes(string psComisionado, string psPeriodo, string psMes)
        {
            string dias = "";

            string Query = "SELECT SUM(CANT_DIAS_VIAT) AS DIAS ";
            Query += " FROM crip_proyeccion_viat ";
            Query += " WHERE USUARIO = '" + psComisionado + "' ";
            Query += " AND ESTATUS = 1";
            Query += " AND PERIODO= '" + psPeriodo + "'";
            Query += " AND CLV_MES= '" + psMes + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                dias = Convert.ToString(Reader["DIAS"]);
            }

            if (dias == "")
            {
                dias = dictionary.NUMERO_CERO;
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return dias;
        }
        public static Boolean Insert_Proyecion_Viaticos(string psComisionado, string psProyecto, string psDepProy, string psUbicacion, double psCantDiasViat, string psRol, int psZona, double psTotalViat, string psUsserRegistro, string psMes) 
        {
            Boolean lbResultado;

            string Query = "INSERT INTO `crip_proyeccion_viat` ";
            Query += "    ( ";
            Query += " ID,";
            Query += " USUARIO,";
            Query += " PROYECTO,";
            Query += " DEP_PROY,";
            Query += " UBICACION,";
            Query += " CANT_DIAS_VIAT,";
            Query += " ROL,";
            Query += " CLV_MES,";
            Query += " CLV_ZONA,";
            Query += " TOTAL_VIATICOS,";
            Query += " FECHA_EFF,";
            Query += " ESTATUS,";
            Query += " SEC_EFF,";
            Query += " PERIODO,";
            Query += " USSER";
            Query += " ) ";
            Query += " VALUES ";
            Query += " ( ";
            Query += " '" + Obtener_Max_ID() +"', ";
            Query += " '" + psComisionado + "', ";
            Query += " '" + psProyecto + "', ";
            Query += " '" + psDepProy + "', ";
            Query += " '" + psUbicacion + "', ";
            Query += " " + psCantDiasViat + ", ";
            Query += " '" + psRol + "', ";
            Query += " '" + psMes + "', ";
            Query += " " + psZona + ", ";
            Query += " " + psTotalViat + ", ";
            Query += " '" + lsHoy + "', ";
            Query += " '" + 1 + "', ";
            Query += " '" + 1 + "',";
            Query += " '" + year + "',";
            Query += " '" + psUsserRegistro + "'";
            Query += " )";
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
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }
        public static string Obtener_Max_ID() 
        {
            string Query = "SELECT  MAX(ID)  AS MAX FROM crip_proyeccion_viat";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);
            }
            //   MngConexion.disposeConexion();
            if (max == dictionary.CADENA_NULA)
            {
                liNumero = 1;
            }
            else 
            {
                liNumero = Convert.ToInt32(max) + 1;
            }
            

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Convert.ToString(liNumero);
            
        }
        public static string Obtener_Existen_Registro(string psComisionado, string psProyecto, string psDepProy, string psDepUbic, string psMes, string psZona)
        {
            string COUNT = "";
            int Zona=0;
            string Query = "SELECT COUNT(*) AS COUNT ";
            Query += " FROM crip_proyeccion_viat ";
            Query += " WHERE USUARIO = '" + psComisionado + "' ";
            Query += " AND ESTATUS = 1";
            Query += " AND PERIODO= '" + year + "'";
            Query += " AND PROYECTO= '" + psProyecto + "'";
            Query += " AND DEP_PROY= '" + psDepProy + "'";
            Query += " AND UBICACION= '" + psDepUbic + "'";
            Query += " AND CLV_MES= '" + psMes + "'";
            if(psZona =="1")
            {
                string Rol= MngDatosUsuarios.Obtiene_Rol_Usuario(psComisionado);
                if (Rol != dictionary.INVESTIGADOR)
                {
                    Zona = 4;
                }
                else 
                {
                    Zona = 2;
                }

            }
            else if (psZona == "2")
            {
                Zona = 15;
            }
            Query += " AND CLV_ZONA= " + Zona + "";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                COUNT = Convert.ToString(Reader["COUNT"]);
            }

            if (COUNT == "")
            {
                COUNT = dictionary.NUMERO_CERO;
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return COUNT;
        }
        public static List<Entidad> Obtener_Lista_Meses(string psMesActual)
        {
            string lsQuery = "";
            lsQuery = "SELECT DISTINCT ID AS CODIGO , DESCRIPCION AS DESCRIPCION FROM crip_mes ";
            lsQuery+= " WHERE 1=1 ";
            lsQuery += " AND ID >= '" + psMesActual+"' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  S E L E C C I O N E  = ";

            ListaEntidad.Add(obj);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListaEntidad;
        }
        public static DataSet ObtieneProyeccionViat(string psPeriodo, string psAdscripcion, string psUsuario)
        {
            DataSet datasetArbol = new DataSet("DataSetArbol");
            string query = "";

            query += "  SELECT USUARIO AS COMISIONADO, ";
            query += "  PROYECTO AS PROYECTO,  ";
            query += "  DEP_PROY AS UBICACION_PROYECTO,  ";
            query += "  UBICACION AS ADSCRIPCION_COMISIONADO, ";
            query += "  CANT_DIAS_VIAT AS TOTAL_DIAS, ";
            query += "  CLV_MES AS MES_PROYECCION, ";
            query += "  CLV_ZONA AS ZONA, ";
            query += "  TOTAL_VIATICOS AS IMPORTE_VIATICOS, ";
            query += "  FECHA_EFF AS FECHA_PROYECCION, ";
            query += "  PERIODO AS AÑO_PROYECCION, ";
            query += "  USSER AS USUARIO_REGISTRO ";
            query += "  FROM crip_proyeccion_viat ";
            query += "  WHERE 1 = 1     ";
            query += "  AND ESTATUS NOT IN ('0')";
            query += "   AND PERIODO = '" + psPeriodo + "' ";

            if (psAdscripcion != string.Empty)
            {
                query += " AND UBICACION= '" + psAdscripcion + "'";
            }
            if ((psUsuario != "") | (psUsuario != string.Empty))
            {
                query += " AND USUARIO = '" + psUsuario + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConexionMysql);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return datasetArbol;
        }
        public static string ObtieneDescrMes(string psMes)
        {
            string MES = "";

            string Query = "SELECT DESCRIPCION AS MES ";
            Query += " FROM crip_mes ";
            Query += " WHERE ID = '" + psMes + "' ";
            Query += " AND ESTATUS = 1";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                MES = Convert.ToString(Reader["MES"]);
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return MES;
        }

    }
}
