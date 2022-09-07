using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
namespace InapescaWeb.DAL
{

   public  class MngDatosConfiguraciones
    {
       static string Year = DateTime.Today.Year.ToString();

       public static string Obtiene_Dep_Configuracion(string psDep,string psConfiguracion,string psPeriodo = "")
       {
           string Query = "";
           string resultado = "";

           Query = "SELECT DISTINCT CLV_DEP AS DEPENDENCIA FROM crip_configuraciones ";
           Query += " WHERE CONFIGURACION  = '" + psConfiguracion +"' AND ESTATUS = '1'";
           if (psPeriodo == "")
           {
               Query += " AND PERIODO = '" + Year + "'";
           }
           else
           {
               Query += " AND PERIODO = '" + psPeriodo  + "'";
           }
           
           Query += " AND CLV_DEP = '" + psDep +"' ";

           

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

           cmd.Connection.Open();

           MySqlDataReader Reader = cmd.ExecuteReader();

           if (Reader.Read())
           {
               resultado = Convert.ToString(Reader["DEPENDENCIA"]);
           }

           MngConexion.disposeConexionSMAF(ConexionMysql);

           return resultado;
       }

       public static List<Entidad> Lista_Configuraciones(string psDep, string psConfiguracion, string psPeriodo = "")
       {
           string Query = "SELECT DISTINCT SECUENCIA  AS CODIGO , DESCRIPCION AS DESCRIPCION ";
               Query += " FROM crip_configuraciones ";
               Query += " WHERE CONFIGURACION  = '"+ psConfiguracion +"' AND ESTATUS = '1'";
               if (psPeriodo == "")
               {
                   Query += " AND PERIODO = '"+ Year +"'";
               }
               else
               { 
               Query += " AND PERIODO =  '"  + psPeriodo +"'";
               }
           
               Query  += " AND CLV_DEP = '" + psDep +"'";

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
