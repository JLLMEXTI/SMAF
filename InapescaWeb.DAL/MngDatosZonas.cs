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
   public  class MngDatosZonas
    {
        static readonly clsDictionary dictionary = new clsDictionary();
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());


       public static string obtienDescripcion(string psZona)
       {
           string resultado = "";

           string Query = "";
           Query += " SELECT DESCRIPCION ";
           Query += " FROM crip_zonas  ";
           Query += " WHERE CLV_ZONA= '" + psZona +"'";
           Query += " AND ESTATUS ='1'";

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
           cmd.Connection.Open();

           MySqlDataReader Reader = cmd.ExecuteReader();

           if (Reader.Read())
           {
               resultado = Convert.ToString(Reader["DESCRIPCION"]);
           }

           if ((resultado == null) | (resultado == "")) resultado = dictionary.CADENA_NULA;

           Reader.Close();
           MngConexion.disposeConexionSMAF(ConexionMysql);

           return resultado;
       }
       public static double obtieneTarifa(int psZona)
       {
           double resultado = 0.00;

           string Query = "";
           Query += " SELECT TARIFA ";
           Query += " FROM crip_zonas  ";
           Query += " WHERE CLV_ZONA= " + psZona + "";
           Query += " AND ESTATUS ='1'";

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
           cmd.Connection.Open();

           MySqlDataReader Reader = cmd.ExecuteReader();

           if (Reader.Read())
           {
               resultado = Convert.ToDouble(Reader["TARIFA"]);
           }

           if ((resultado == null) | (resultado == 0.00)) resultado = 0.00;

           Reader.Close();
           MngConexion.disposeConexionSMAF(ConexionMysql);

           return resultado;
       }

    }
}
