using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mime;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.IO;
using InapescaWeb;
namespace InapescaWeb.DAL
{
   public  class MngDatosPresupuestarios
    {
       static clsDictionary Dictionary = new clsDictionary();

       public static List<Entidad> Presupuestarios()
       {
           string Query = "SELECT CLV_COMPONENTE AS CODIGO , DESCRIPCION AS DESCRIPCION FROM crip_presupuestarios WHERE ESTATUS = '1'";

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
