/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosMenu.cs
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
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosMenu
    {
       private static DataSet datasetArbol;

       public static DataSet ReturnDataSet_DGAIPP(string psRol, string psPadre)
       {
           string lsQuery;

           datasetArbol = new DataSet("DataSetArbol");

           lsQuery = "SELECT MODULO,DESCRIPCION,PADRE FROM dgaipp_formularios WHERE ROL ='" + psRol + "' AND PADRE = '" + psPadre + "' AND ESTATUS = '1'";

           MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
           MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysqlDgaipp);

           MySqlDataAdapter adapter = new MySqlDataAdapter(lsQuery, ConexionMysqlDgaipp);
           datasetArbol.Tables.Clear();
           adapter.Fill(datasetArbol, "DataSetArbol");

           MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
           // ConexionMysql = null;

           return datasetArbol;
       }

   
        public static DataSet ReturnDataSet(string psRol , string psPadre)
        {
          
            string lsQuery;

            datasetArbol = new DataSet("DataSetArbol");

            lsQuery = "SELECT MODULO , DESCRIPCION, PADRE  FROM crip_formularios WHERE ROL ='" + psRol + "' AND PADRE = '" +psPadre  + "' AND ESTATUS = '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);

            MySqlDataAdapter adapter = new MySqlDataAdapter(lsQuery, ConexionMysql);
            datasetArbol .Tables .Clear ();
            adapter.Fill(datasetArbol ,"DataSetArbol");

            MngConexion.disposeConexionSMAF(ConexionMysql);
           // ConexionMysql = null;

            return datasetArbol;
        }

        public static WebPage MngdatsUrlsDGAIPP(string psModulo, string psrol)
        {
            WebPage objWebPage = new WebPage();
            string lsQuery;
            lsQuery = "SELECT MODULO,DESCRIPCION,PADRE,URL FROM dgaipp_formularios WHERE MODULO = '" + psModulo + "' AND ROL= '" + psrol + "' AND ESTATUS = '1'";
            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysqlDgaipp);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {

                objWebPage.Modulo = Convert.ToString(Reader["MODULO"]);
                objWebPage.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objWebPage.Padre = Convert.ToString(Reader["PADRE"]);
                objWebPage.URL = Convert.ToString(Reader["URL"]);
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
            // ConexionMysql = null;
            return objWebPage;
        
        }

        public static WebPage MngDatsUrls(string psModulo, string psrol)
        {
            WebPage objWebPage = new WebPage();
            string lsQuery;
            lsQuery = "SELECT MODULO,DESCRIPCION,PADRE,URL FROM crip_formularios WHERE MODULO = '" + psModulo + "' AND ROL= '" + psrol + "' AND ESTATUS = '1'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {

                objWebPage.Modulo = Convert.ToString(Reader["MODULO"]);
                objWebPage.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objWebPage.Padre = Convert.ToString(Reader["PADRE"]);
                objWebPage.URL = Convert.ToString(Reader["URL"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
           // ConexionMysql = null;
            return objWebPage;
        
        }
    }
}
