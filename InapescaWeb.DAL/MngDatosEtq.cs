using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
namespace InapescaWeb.DAL
{
   public  class MngDatosEtq
    {
       
       public static string[] Obt_Etq(string psIdioma)
       {
           string Query = "";

           if (psIdioma == "ESP")
           {
                Query = "SELECT ETQ_ESP AS ETQ FROM crip_etq ";
           }
           else if (psIdioma == "POR")
           {
               Query = "SELECT ETQ_POR AS ETQ FROM crip_etq ";
           }
           else if (psIdioma == "ENG")
           {
               Query = "SELECT ETQ_ENG AS ETQ FROM crip_etq ";
           }

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
           cmd.Connection.Open();

           MySqlDataReader Reader = cmd.ExecuteReader();
           
          
           string[] Headers = new string[Convert.ToInt32 (cmd.ExecuteScalar ()) ];

           int i = 0;

          while   (Reader.Read())
           {
               Headers[i] = Convert.ToString(Reader["ETQ"]);
               i++;
           }

           MngConexion.disposeConexionSMAF(ConexionMysql );

           return Headers;

       }


    }
}
