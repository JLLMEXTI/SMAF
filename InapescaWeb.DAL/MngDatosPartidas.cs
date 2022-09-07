using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosPartidas
    {
        static string year = DateTime.Today.Year.ToString();

        private static DataSet datasetArbol;

        public static DataSet ObtienePartidas(string psPadre)
        {
            string lsQuery;

            datasetArbol = new DataSet("DataSetArbol");

            lsQuery = "SELECT ID AS MODULO,DESCRIPCION,PADRE FROM crip_partidas WHERE PADRE = '" + psPadre + "'  and ESTATUS= '1'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);

            MySqlDataAdapter adapter = new MySqlDataAdapter(lsQuery, ConexionMysql);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");

            MngConexion.disposeConexionSMAF(ConexionMysql);
           // ConexionMysql = null;
            return datasetArbol;

        }


        public static List<Entidad> ListaPartidas(string psPadre)
        {
            string Query;
            Query = "SELECT ID AS CODIGO , CONCAT(ID,' - ', DESCRIPCION) AS DESCRIPCION  FROM crip_partidas WHERE PADRE = '" + psPadre + "'  and ESTATUS= '1'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = "";
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
           // ConexionMysql = null;

            return ListaEntidad;
        }

        public static List<Entidad> Obtiene_partidas_pasajes()
        {
            string Query;
            Query = "SELECT ID AS CODIGO,CONCAT(ID, ' - ', DESCRIPCION) AS DESCRIPCION FROM crip_partidas ";
            Query += " WHERE ESTATUS='1'";
            Query += "AND ID IN('37101','37104','37106','37201','37204''37206')";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = "";
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
         //  ConexionMysql = null;
            return ListaEntidad;
        }

        public static string ObtieneDescripcionPartida(string psPartida)
        {
            string lsQuery;
            string resultado = "";
           
            lsQuery = "SELECT DESCRIPCION FROM crip_partidas WHERE ID = '" + psPartida + "'  and ESTATUS= '1'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DESCRIPCION"]);                
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            // ConexionMysql = null;
            return resultado;

        }

    }
}
