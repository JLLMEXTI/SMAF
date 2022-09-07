/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngConexion.cs
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
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
namespace InapescaWeb.DAL
{
    public class MngConexion
    {
        /*Cadenas Conexion server localhost smaf*/
        public static MySqlConnection ConexionMysql;

        public static MySqlConnection getConexionMysql()
        {
            string CadenaConexionEncriptada = ConfigurationManager.AppSettings["localhost"];
            //string CadenaConexionEncriptada = ConfigurationManager.AppSettings["Inapesca.Info"];
            string CadenaConexion = MngEncriptacion.decripString(CadenaConexionEncriptada);
            ConexionMysql = new MySqlConnection(CadenaConexion);
            return ConexionMysql;
        }
        public static MySqlConnection getConexionMysql_ModuloConsulta()
        {
            string CadenaConexionEncriptada = ConfigurationManager.AppSettings["RemoteModuloConsulta"];
            //string CadenaConexionEncriptada = ConfigurationManager.AppSettings["Inapesca.Info"];
            string CadenaConexion = MngEncriptacion.decripString(CadenaConexionEncriptada);
            ConexionMysql = new MySqlConnection(CadenaConexion);
            return ConexionMysql;
        }

        public static void disposeConexion()
        {/*
            if (ConexionMysql.State == ConnectionState.Open)
            {*/
                ConexionMysql.Close();
                ConexionMysql.Dispose();
              //    ConexionMysql = null;
           // }
        }
        public static void disposeConexionSMAF(MySqlConnection pMysqlConexionMysql)
        {/*
            if (ConexionMysql.State == ConnectionState.Open)
            {*/
            pMysqlConexionMysql.Close();
            pMysqlConexionMysql.Dispose();
            pMysqlConexionMysql = null;
           // }
        }
        public static void disposeConexionMC(MySqlConnection pMysqlConexionMysql)
        {/*
            if (ConexionMysql.State == ConnectionState.Open)
            {*/
            pMysqlConexionMysql.Close();
            pMysqlConexionMysql.Dispose();
            pMysqlConexionMysql = null;
            // }
        }
        public static MySqlConnection ConexionMysql2;
        public static MySqlConnection getConexionMysql_Contratos()
        {
            string CadenaConexionEncriptada = ConfigurationManager.AppSettings["localhostContratos"];
            //string CadenaConexionEncriptada = ConfigurationManager.AppSettings["Inapesca.Info"];
            string CadenaConexion = MngEncriptacion.decripString(CadenaConexionEncriptada);
            ConexionMysql2 = new MySqlConnection(CadenaConexion);
            return ConexionMysql2;
        }


        /*termina cadenas de conexion server localhos smaf*/

        /*Cadenas de Conexion de dgaipp*/ 
        public static MySqlConnection ConexionMysql1;
       
        public static MySqlConnection getConexionMysql_dgaipp()
        {
            string CadenaConexionEncriptada = ConfigurationManager.AppSettings["localhost_dgaipp"];
            //string CadenaConexionEncriptada = ConfigurationManager.AppSettings["Inapesca.Info"];
            string CadenaConexion = MngEncriptacion.decripString(CadenaConexionEncriptada);
            ConexionMysql1 = new MySqlConnection(CadenaConexion);
            return ConexionMysql1;
        }

        public static void disposeConexion_dgaipp(MySqlConnection pMysqlConexionMysql)
        {
            if (ConexionMysql1.State == ConnectionState.Open)
            {
                pMysqlConexionMysql.Close();
                pMysqlConexionMysql.Dispose();
                pMysqlConexionMysql = null;
                //  ConexionMysql = null;
            }
        }
        /*termina cadena de conexion de dgaipp*/

    }
}
