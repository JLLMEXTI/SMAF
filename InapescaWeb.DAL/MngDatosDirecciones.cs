using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
    public class MngDatosDirecciones
    {
        static string lsQuery;
        static clsDictionary Dictionary = new clsDictionary();
        static string Year = DateTime.Today.Year.ToString();

       


        public static string NombreDireccion(string psDirec)
        {
            string resultado = "";

            string lsQuery = " SELECT DESCRIPCION ";
            lsQuery += "  FROM crip_direcciones ";
            lsQuery += " WHERE 1=1 ";
            lsQuery += " AND ESTATUS = '1' ";
            lsQuery += " AND CLV_DIR  ='" + psDirec + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        //public static List<Entidad> ObtieneDireccionPsp(string psPeriodo, string psDireccion)
        //{ 
        
        //}

        public static List<Entidad> ObtieneDireccion(string psPeriodo)
        {
            lsQuery = "";
            lsQuery += "SELECT DISTINCT CLV_DIR AS CODIGO,DESCRIPCION AS DESCRIPCION from crip_direcciones ";
            lsQuery += "WHERE ESTATUS='1' ";
            lsQuery += " AND ANIO= '" + psPeriodo + "' ";
            lsQuery += " AND DESCRIPCION LIKE '%ADJUNTA%' ";
            lsQuery += " OR CLV_DIR =  '1000' ";
            lsQuery += " ORDER BY CLV_DIR ASC ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj1 = new Entidad();
            obj1.Codigo = "0";
            obj1.Descripcion = " =  S E L E C C I O N E = ";

            ListaEntidad.Add(obj1);
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

        public static List<Entidad> ObtienenDirecciones(string psPeriodo, bool pbAdjunta = false, bool ptodos = false)
        {
            lsQuery = "";
            lsQuery = " SELECT CLV_DIR AS CODIGO,DESCRIPCION AS DESCRIPCION from crip_direcciones ";
            lsQuery += " WHERE ESTATUS='1' ";
            lsQuery += " AND ANIO= '" + psPeriodo + "' ";

            if (pbAdjunta)
            {
                lsQuery += "AND DESCRIPCION LIKE '%ADJUNTA%'";
            }

            lsQuery += "ORDER BY CLV_DIR ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj1 = new Entidad();
            obj1.Codigo = "0";
            obj1.Descripcion = " =  S E L E C C I O N E = ";

            ListaEntidad.Add(obj1);


            /*if (ptodos)
            {
                Entidad obj = new Entidad();
                obj.Codigo = "10";
                obj.Descripcion = "  T O D O S  ";
                ListaEntidad.Add(obj);
            }*/


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
