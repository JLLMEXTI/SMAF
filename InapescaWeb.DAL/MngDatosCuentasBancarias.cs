using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;


namespace InapescaWeb.DAL
{
    public class MngDatosCuentasBancarias
    {

        public static List<Entidad> ListaBancos(string psDep,bool pbBandera = false )
        { 
        string Query = " SELECT CLV_BANCO AS CODIGO,CLV_BANCO AS DESCRIPCION ,ADSCRIPCION";
               Query += " FROM crip_cuentas ";
               Query += " WHERE ESTATUS = '1' ";

               if (!pbBandera)
               {
                   Query += " AND CLV_DEP in ('0','" + psDep + "')";
               }
                Query += " ORDER BY CLV_DEP ASC";

               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
               cmd.Connection.Open();
               MySqlDataReader Reader = cmd.ExecuteReader();
               List<Entidad> lista = new List<Entidad>();

               while (Reader.Read())
               {
                   Entidad ent = new Entidad();
                   ent.Codigo = Convert.ToString(Reader["CODIGO"]);
                   ent.Descripcion = Convert.ToString(Reader["DESCRIPCION"])+  " - " + Convert.ToString(Reader["ADSCRIPCION"]);

                   lista.Add(ent );
                   ent  = null;
               }

               MngConexion.disposeConexionSMAF(ConexionMysql );
               Reader.Close();

               return lista;
        }

        public static CuentasBancarias ListaCuentas(string psDep,string  psBanco)
        {
            string Query = " SELECT * FROM crip_cuentas ";
            Query += "  WHERE ESTATUS= '1'";
            Query += " AND CLV_DEP in ('0','" + psDep + "')";
            Query += "  AND CLV_BANCO = '" + psBanco +"'";
            Query += " ORDER BY CLV_DEP ASC ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

          CuentasBancarias oCuentas = new CuentasBancarias();
            while (Reader.Read())
            {
               
                oCuentas.Cuenta = Convert.ToString(Reader["CLV_CUENTA"]);
                oCuentas.Clabe = Convert.ToString(Reader["CLABE_INTER"]);
                oCuentas.Banco = Convert.ToString(Reader["CLV_BANCO"]);
                oCuentas.Dependencia = Convert.ToString(Reader["CLV_DEP"]);
              
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();

            return oCuentas;
        }

    }
}
