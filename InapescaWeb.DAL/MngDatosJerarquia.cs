using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosJerarquia
    {
        

        public static List<Entidad> Obtiene_Rol_Jerarquico(string psRol)
        {

            string Query = "SELECT ID_PADRE,TIPO_MAIL FROM crip_jerarquia ";
            Query += " WHERE ESTATUS = '1'";
            Query += " AND ID_HIJO = '" + psRol + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["ID_PADRE"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["TIPO_MAIL"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }

        public static string Rol_Jerarquico(string psRol)
        {
            string resultado = "";
            string Query = "SELECT ID_PADRE AS ROL FROM crip_jerarquia ";
            Query += " WHERE ESTATUS = '1' ";
            Query += " AND TIPO_MAIL = 'VOBO'  ";
            Query += "  AND ID_HIJO = '"+psRol +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            
            if (Reader.Read())
            {
                resultado  = Convert.ToString(Reader["ROL"]);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return resultado;
        
        }


    }
}
