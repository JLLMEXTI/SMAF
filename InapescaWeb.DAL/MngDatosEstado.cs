using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
   public  class MngDatosEstado
    {

        static clsDictionary Dictionary = new clsDictionary();
        static string lsYear = DateTime.Now.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

       public static string Estado(string psEstado)
       {
           string cargo = "";

         string   lsQuery = "";
           lsQuery = " SELECT DESCR FROM crip_estado ";
           lsQuery += " WHERE CLV_ESTADO = '" + psEstado +"'";

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
           cmd.Connection.Open();

           MySqlDataReader Reader = cmd.ExecuteReader();

           if (Reader.Read())
           {
               cargo = Convert.ToString(Reader["DESCR"]);
           }
           else
           {
               cargo = "";
           }
           MngConexion.disposeConexionSMAF(ConexionMysql );

           return cargo;
       }


       public static List<Entidad> Estados()
       {
           string Query = "";
           Query += "SELECT  CLV_ESTADO AS CODIGO , DESCR AS DESCRIPCION ";
           Query += " FROM crip_estado ";
           Query += " WHERE 1=1 ";

           MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
           MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
           cmd.Connection.Open();
           MySqlDataReader Reader = cmd.ExecuteReader();

           List<Entidad> ListaEntidad = new List<Entidad>();

           Entidad objetoEntidad1 = new Entidad();
           objetoEntidad1.Codigo = Dictionary.CADENA_NULA;
           objetoEntidad1.Descripcion = " = S E L E C C I O N E = ";

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

    }
}
