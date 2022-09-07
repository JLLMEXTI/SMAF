/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngDatosActividades.cs
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
    public class MngDatosActividades
    {
        public static string Obtiene_Descripcion_Actividades(string psActividad)
        {
            string lsResultado = "";
            string Query = "";

            Query = "SELECT DESCRIPCION AS DESCRIPCION FROM crip_actividades";
            Query += "    WHERE 1 = 1 ";
           Query += " AND ESTATUS   = '1'";
           Query += "     AND CLV_ACTIVIDAD = '"+ psActividad +"'";

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
           cmd.Connection.Open();

           MySqlDataReader Reader = cmd.ExecuteReader();
           if (Reader.Read())
           {
               lsResultado = Convert.ToString(Reader["DESCRIPCION"]);
           }

           MngConexion.disposeConexionSMAF(ConexionMysql);

           Reader.Close();

           return lsResultado;

            
        }


        public static List<Entidad> Obtiene_Actividades(string psComponente = "", string psDireccion = "")
        {
            string Query = "SELECT CLV_ACTIVIDAD AS CODIGO , DESCRIPCION AS DESCRIPCION FROM crip_actividades WHERE ESTATUS='1' ";
            if (psComponente != "4KS")
            {
                Query += "    AND CLV_ACTIVIDAD NOT IN ('06')";
            }
            
            

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

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }
    }
}
