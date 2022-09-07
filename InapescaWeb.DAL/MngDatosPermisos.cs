using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;


namespace InapescaWeb.DAL
{

    public class MngDatosPermisos
    {


        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static string obtienePermisos(string psUsuario, string psPermiso, string psProyecto = "", string psDep = "")
        {

            string lsPermisos = "";
            string lsQuery = "SELECT PERMISO FROM crip_permisos ";
            lsQuery += " WHERE 1 = 1 ";
            lsQuery += " AND  CLV_USUARIO = '" + psUsuario + "' ";
            lsQuery += " AND PERMISO = '" + psPermiso + "' ";
            lsQuery += " AND ESTATUS = '1' ";
            if ((psPermiso == "VIAT") || (psPermiso == "OFMAY") || (psPermiso == "EXT") || (psPermiso == "EXTPROY")) lsQuery += "  AND FECHA = '" + lsHoy + "' ";
            if (psProyecto != "") lsQuery += "  AND CLV_PROY = '" + psProyecto + "' ";
            if (psDep != "") lsQuery += "AND CLV_DEP_PROY = '" + psDep + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {

                lsPermisos = Convert.ToString(Reader["PERMISO"]);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return lsPermisos;

        }
        public static int obtieneCountPermisos(string psUsuario, string psPermiso)
        {

            int lsPermisos = 0;
            string lsQuery = "SELECT COUNT(*) AS COUNT FROM crip_permisos ";
            lsQuery += " WHERE 1 = 1 ";
            lsQuery += " AND  CLV_USUARIO = '" + psUsuario + "' ";
            lsQuery += " AND FECHA= '" + lsHoy + "' ";
            lsQuery += " AND ESTATUS = '1' ";
            lsQuery += " AND PERMISO= '" + psPermiso + "' ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {

                lsPermisos = Convert.ToInt16(Reader["COUNT"]);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return lsPermisos;

        }
        public static int obtieneCantPermisos(string psUsuario, string psPermiso)
        {

            int lsPermisos = 0;
            string lsQuery = "SELECT CANTIDAD_PERMISOS FROM crip_permisos ";
            lsQuery += " WHERE 1 = 1 ";
            lsQuery += " AND  CLV_USUARIO = '" + psUsuario + "' ";
            lsQuery += " AND FECHA= '" + lsHoy + "' ";
            lsQuery += " AND ESTATUS = '1' ";
            lsQuery += " AND PERMISO= '" + psPermiso + "' ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {

                lsPermisos = Convert.ToInt16(Reader["CANTIDAD_PERMISOS"]);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return lsPermisos;

        }
        public static int obtieneCountSolicitudes(string psUsuario)
        {

            int lsSolicitudes = 0;
            string lsQuery = "SELECT COUNT(*) AS COUNT FROM crip_comision ";
            lsQuery += " WHERE 1 = 1 ";
            lsQuery += " AND  USUARIO = '" + psUsuario + "' ";
            lsQuery += " AND FECHA_SOL= '" + lsHoy + "' ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {

                lsSolicitudes = Convert.ToInt16(Reader["COUNT"]);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return lsSolicitudes;

        }
        public static List<Entidad> ObtieneListPermisos()
        {
            string lsQuery="";
            lsQuery = "SELECT DISTINCT CLV_PERMISO AS CODIGO ,  CLV_PERMISO AS DESCRIPCION FROM crip_catalogo_permisos WHERE ESTATUS= '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  S E L E C C I O N E  = ";

            ListaEntidad.Add(obj);

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
        public static Boolean Insert_NuevoPermiso(string psUsuario, string psPermiso, string psAutoriza, string psFecha, string psObservaciones = "", string psCantPermisos="")
        {
            Boolean Resultado = false;

            string lsQuery;
            lsQuery = " INSERT INTO crip_permisos " ;
            lsQuery += " ( ";
            lsQuery += " CLV_USUARIO, ";
            lsQuery += " PERMISO, ";
            lsQuery += " AUT_EXTERNO, ";
            lsQuery += " CLV_PROY, ";
            lsQuery += " CLV_DEP_PROY, ";
            lsQuery += " ESTATUS, ";
            lsQuery += " FECHA, ";
            lsQuery += " OBSERVACIONES, ";
            lsQuery += " CANTIDAD_PERMISOS ";
            lsQuery += " ) ";
            lsQuery += " VALUES ";
            lsQuery += " ( ";
            lsQuery += " '" + psUsuario + "', ";
            lsQuery += " '" + psPermiso + "', ";
            lsQuery += " '" + psAutoriza + "', ";
            lsQuery += " '1', ";
            lsQuery += " '1', ";
            lsQuery += " '1', ";
            lsQuery += " '" + clsFunciones.FormatFecha(Convert.ToString(psFecha)) + "', ";
            lsQuery += " '" + psObservaciones + "', ";
            lsQuery += " '" + psCantPermisos + "' ";
            lsQuery += " ) ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() == 1)
            {
                Resultado = true;
            }
            else
            {
                Resultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }
    }
}
