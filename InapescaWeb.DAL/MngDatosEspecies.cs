/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngDatosespecies.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Noviembre 2015
    Description: Clase que contiene metodos que extraen datos de proyectos
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
    public class MngDatosEspecies
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsYear = DateTime.Now.Year.ToString();

        public static string Obtiene_Descripcion_Pesqueria(string psPrograma, string psPesqueria,string psDireccion)
        {
            string lsResultado = "";
            string Query = "";
            Query = "SELECT DESCRIPCION AS DESCRIPCION FROM crip_especies ";
            Query += "    WHERE 1 = 1";
            Query += "     AND ESTATUS = '1'";
            Query += "     AND ANIO = '" + lsYear + "'";
            Query += " AND CLV_PROGRAMA  = '" + psPrograma + "'";
            Query += " AND CLV_PESQUERIA  = '" + psPesqueria + "'";
            Query += "AND DIRECCION = '" + psDireccion + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                lsResultado = Convert.ToString(Reader["DESCRIPCION"]);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lsResultado;
        }


        public static List<Entidad> Obtiene_Especies(string psPrograma,string psDireccion)
        {
            string Query = "SELECT CLV_PESQUERIA AS CODIGO ,DESCRIPCION  AS DESCRIPCION ";
            Query += " FROM crip_especies ";
            Query += " WHERE CLV_PROGRAMA = '" + psPrograma + "'";
            Query += " AND ANIO = '" + lsYear + "'";
            Query += " AND ESTATUS ='1'";
            Query += "AND DIRECCION = '" + psDireccion + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();


            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return ListaEntidad;

        }
    }
}
