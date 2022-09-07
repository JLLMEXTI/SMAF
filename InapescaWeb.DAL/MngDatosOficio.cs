using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;


namespace InapescaWeb.DAL
{
    /// <summary>
    /// Clase Oficio, contiene metodos que devuelven colecciones de datos correspondientes a oficios en Bd
    /// </summary>
    public class MngDatosOficio
    {
        public static List<Entidad> Oficios_Comprobatorios()
        {
            string Query = "SELECT  CLV_TIPO_O  AS CODIGO,DESCRIPCION AS DESCRIPCION FROM crip_oficio ";
            Query += " WHERE CLV_TIPO_O IN ( 'CRIPSC00','CRIPSC07')";

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

        public static List<Entidad> Lista_DOCS( string psOficio,string psDep, string psUsuario)
        {
            string Query = "SELECT DISTINCT CLV_DOC AS CODIGO , DOCUMENTO_COMPROBACION AS DESCRIPCION FROM ";
                   Query += " crip_comision_comprobacion ";
                   Query += " WHERE  NO_OFICIO = '"+psOficio +"'";  
                   Query += " AND DEP_COMSIONADO = '"+psDep +"'  ";
                   Query += " AND COMISIONADO = '"+psUsuario +"'  ";
                   Query += " ORDER BY CLV_DOC ASC ";
                  
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

                   MngConexion.disposeConexionSMAF (ConexionMysql );

                   return ListaEntidad;
        }

    }
}
