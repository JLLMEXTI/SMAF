using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
  public  class MngDatosAnio
    {
      static string Year = DateTime.Today.Year.ToString();

      public static List<Entidad> ObtieneAnios()
      {
          string Query = "select PERIODO AS CODIGO,PERIODO AS DESCRIPCION from crip_anio_fiscal";
          MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

          MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
          cmd.Connection.Open();
          
          MySqlDataReader Reader = cmd.ExecuteReader();

          Entidad objetoEntidad1 = new Entidad();
          objetoEntidad1.Codigo = string.Empty;
          objetoEntidad1.Descripcion = "= S E L E C C I O N E=";

          List<Entidad> ListaEntidad = new List<Entidad>();

          ListaEntidad.Add(objetoEntidad1);

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

      public static string Obtiene_Periodo(string psInicio, string psFinal)
      {
          string resultado = "";
          string Query ="SELECT PERIODO AS PERIODO ";
               Query += " FROM crip_anio_fiscal ";
               Query += " WHERE ";
               Query += " ( " ;
		       Query += "     ('" + psInicio +"' BETWEEN INICIO AND FINAL) ";
               Query += " AND ('"+ psFinal + "' BETWEEN INICIO AND FINAL) ";
               Query += " ) ";
               
                MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

               MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
               cmd.Connection.Open();
               MySqlDataReader Reader = cmd.ExecuteReader();

               if (Reader.Read())
               {
                   resultado = Convert.ToString(Reader["PERIODO"]);
               }

               MngConexion.disposeConexionSMAF(ConexionMysql);

               return resultado;

      }


      public static Entidad Inicio_Final(string psPeridodo)
      {
          string Query = "  SELECT INICIO , FINAL ";
           Query += " FROM CRIP_ANIO_FISCAL ";
           Query += " WHERE  PERIODO = '"+ psPeridodo +"'";

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
           cmd.Connection.Open();
           MySqlDataReader Reader = cmd.ExecuteReader();
           Entidad objEntidad = new Entidad();
           while (Reader.Read())
           {
               objEntidad.Codigo = Convert.ToString(Reader["INICIO"]);
               objEntidad.Descripcion = Convert.ToString(Reader["FINAL"]);

           }
           Reader.Close();
           MngConexion.disposeConexionSMAF(ConexionMysql);
           return objEntidad;
      }

      public static string Anio(string psPeriodo)
      {
          string resultado = "";
          string Query = "  SELECT INICIO FROM CRIP_ANIO_FISCAL WHERE  PERIODO = '" + psPeriodo +"'";
          
          MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

          MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
          cmd.Connection.Open();
          MySqlDataReader Reader = cmd.ExecuteReader();

          if (Reader.Read())
          {
              resultado = Convert.ToString(Reader["INICIO"]);
          }
          MngConexion.disposeConexionSMAF(ConexionMysql);
          return resultado;
      }

      public static string Fin_Anio(string psPeriodo)
      {
          string resultado = "";
          string Query = "  SELECT FINAL FROM CRIP_ANIO_FISCAL WHERE  PERIODO = '" + psPeriodo + "'";

          MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

          MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
          cmd.Connection.Open();
          MySqlDataReader Reader = cmd.ExecuteReader();

          if (Reader.Read())
          {
              resultado = Convert.ToString(Reader["INICIO"]);
          }
          MngConexion.disposeConexionSMAF(ConexionMysql);
          return resultado;
      }
    }
}
