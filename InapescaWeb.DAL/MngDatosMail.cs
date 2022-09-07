using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosMail
    {
        static string Mail_Invest;

        public static string Mail_Destino(string psSecretaria = "", string psOrganismo = "", string psUbicacion = "", string psRol = "", string psUsuario = "")
        {
            string Query = "      SELECT DISTINCT EMAIL";
            Query += "      FROM vw_usuarios ";
            Query += "      WHERE 1 = 1 ";

            if( psSecretaria != "") Query += "      AND SECRETARIA = '" + psSecretaria + "' ";
            if (psOrganismo  != "") Query += "      AND ORGANISMO = '" + psOrganismo + "' ";
            if (psUbicacion  != "") Query += "      AND DEPENDENCIA = '" + psUbicacion + "' ";
          
           
            if (psRol != "")
            {
                Query += "      AND ROL = '" + psRol + "'";
            }


            if (psUsuario != "")
            {
                Query += "      AND USSER = '" + psUsuario + "'";
            }


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Mail_Invest = Convert.ToString(Reader["EMAIL"]); ;
            }
            else
                Mail_Invest = "";

            MngConexion.disposeConexionSMAF(ConexionMysql );
            return Mail_Invest;
        }


        public static string Obtiene_Mail(string psUsuario)
        {
            string Query = "      SELECT DISTINCT EMAIL";
            Query += "      FROM vw_usuarios ";
            Query += "      WHERE USSER = '" + psUsuario + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Mail_Invest = Convert.ToString(Reader["EMAIL"]); ;
            }
            else
                Mail_Invest = "";

            MngConexion.disposeConexionSMAF(ConexionMysql );
            return Mail_Invest;
        
        }
        //public static List<Entidad> Jeraquico(string psRol)
        //{

        //    string Query = "SELECT TIPO_MAIL  AS CODIGO,ID_PADRE AS DESCRIPCION  FROM CRIP_JERARQUIA WHERE ID_HIJO = '" + psRol + "'";

        //    MySqlCommand cmd = new MySqlCommand(Query, MngConexion.getConexionMysql());
        //    cmd.Connection.Open();

        //    MySqlDataReader Reader = cmd.ExecuteReader();
        //    List<Entidad> ListaEntidad = new List<Entidad>();

        //    while (Reader.Read())
        //    {
        //        Entidad objetoEntidad = new Entidad();
        //        objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
        //        objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

        //        ListaEntidad.Add(objetoEntidad);
        //    }

        //    MngConexion.disposeConexion();

        //    return ListaEntidad;
        //}

        public static List<Entidad> Obtiene_Mail(string Tipo,string psFolio,string psDep)
        {
            string Query = "SELECT DISTINCT "+Tipo +" AS CODIGO ,"+Tipo +" DESCRIPCION FROM crip_comision ";
            Query += "WHERE FOLIO = '"+psFolio +"' AND CLV_DEP ='"+psDep + "'";

            if (Tipo != "VoBo")
            {
                Query += " AND ESTATUS = '1'";
            }
            else
            {
                Query += " AND ESTATUS = '8'";
            }
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
