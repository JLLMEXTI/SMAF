
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
   public  class MngDatosPassword
    {

       public static bool Cambia_Password(string psPassword, string psUsuario )
       { 
         bool bandera = false;
            string Query = "UPDATE crip_job ";
               Query += " SET `PASSWORD` = '"+ psPassword +"' ";
               Query += "     WHERE USUARIO = '"+ psUsuario +"'";
               Query += " AND ESTATUS = '1'";

               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
               cmd.Connection.Open();

               if (cmd.ExecuteNonQuery() > 0)
               {
                   bandera = true;
               }
               else
               {
                   bandera = false;
               }

               MngConexion.disposeConexionSMAF(ConexionMysql );

               return bandera;

       }
    }
}
