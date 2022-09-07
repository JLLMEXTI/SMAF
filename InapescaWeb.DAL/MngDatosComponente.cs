using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
   public class MngDatosComponente
    {
        static string lsQuery;
        static clsDictionary Dictionary = new clsDictionary();



        public static List<Entidad> Obtienencomponente()
        {

            lsQuery = "";
            lsQuery = " SELECT CLV_COMPONENTE AS CODIGO,CONCAT(CLV_COMPONENTE ,' - ',DESCRIPCION) AS DESCRIPCION from crip_presupuestarios";
            lsQuery += " WHERE ESTATUS='1' ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery , ConexionMysql);

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
