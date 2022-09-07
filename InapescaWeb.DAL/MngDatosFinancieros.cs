using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
  public   class MngDatosFinancieros
    {
      public static string Obtiene_Trimestre(string psFecha)
      {
          string Query = "", resultado="";
          Query += "SELECT QUARTER('" + psFecha +"') AS TRIMESTRE_ACTUAL";

          MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
          MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
          cmd.Connection.Open();
          MySqlDataReader Reader = cmd.ExecuteReader();

          if (Reader.Read())
          {
              resultado = Convert.ToString(Reader["TRIMESTRE_ACTUAL"]);
          }

          MngConexion.disposeConexionSMAF(ConexionMysql);

          return resultado;

      }

    }
}
