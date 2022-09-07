using System;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
  public  class MngDatosProovedores
    {
       static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

      public static bool Update_Proveedor(Proovedor poProovedor)
      {
          bool bandera = false;
          string query = "UPDATE crip_proovedores ";
            query += "    SET RFC = '" + poProovedor.RFC  +"',";
           query += " RAZON_SOCIAL= '"+ poProovedor.Razon_Social  + "',";
           query += " CONTACTO= '" + poProovedor.Contacto  +"',";
       query += " TELEFONO= '" + poProovedor.Telefono  +"', ";
       query += " EMAIL ='" + poProovedor.Email  +"', ";
           query += " CALLE= '" + poProovedor.Calle  +"',";
            query += "    NUM_EXT='" + poProovedor.Num_Ext +"',";
          query += "  NUM_INT = '" + poProovedor.Num_int+"',";
               query += " COLONIA='" + poProovedor.Colonia  +"',";
           query += " MUNICIPIO= '"+ poProovedor .Municipio  +"',";
           query += " CIUDAD= '" + poProovedor.Ciudad +"',";
        query += " ESTADO= '" + poProovedor .Estado +"',";
          query += "  PAIS= '" + poProovedor .Pais +"',";
          query += "  CP= '" + poProovedor .CP +"',";
          query += "  TELEFONO_EMPRESA1= '" + poProovedor .Telefono1 +"',";
           query += " TELEFONO_EMPRESA2= '" + poProovedor.Telefono2+"',";
           query += " REGIMEN_FISCAL = '" + poProovedor .Regimen_Fiscal +"',";
           query += " SERVICIO= '" +  poProovedor.Servicio +"',";
           query += " FECHA= '" + lsHoy +"',";
         /*  query += " CURP= '" + poProovedor.Curp +"',";
          query += "  NUMERO_IDENT= '" + poProovedor .Num_Ident +"',";
           query += " TIPO_IDENTIFICACION = '" + poProovedor.Tipo_ident +"',";
          query += " IDENT_EXPEDIDA = '"+ poProovedor.Expedicion + "',";
          */  query += " BANCO= '" + poProovedor .Banco +"',";
           query += " CLABE_INTER= '" + poProovedor.Clabe +"',";
           query += " CUENTA= '" + poProovedor.Cuenta+"'";
               query += " WHERE RFC = '" + poProovedor.RFC +"'";
               query += " AND ESTATUS = '1'";


               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
               cmd.Connection.Open();
               //   MySqlDataReader Reader = cmd.ExecuteReader();

               if (cmd.ExecuteNonQuery() == 1) bandera = true;
               else bandera = false;

               MngConexion.disposeConexionSMAF(ConexionMysql);

          return bandera;
      }


      public static Proovedor Proveedor( string psRFC)
      {
          string query = " SELECT * ";
               query += " FROM crip_proovedores ";
               query += " WHERE ESTATUS = '1'";
               query += " AND RFC= '" + psRFC +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            Proovedor oProv = new Proovedor();
            while (Reader.Read())
            {
              //  obj.FOLIO = Convert.ToString(Reader["OFICIO"]);
                oProv.RFC = Convert.ToString(Reader["RFC"]);
                oProv.Razon_Social = Convert.ToString(Reader["RAZON_SOCIAL"]);
                oProv.Contacto = Convert.ToString(Reader["CONTACTO"]);
                oProv.Telefono = Convert.ToString(Reader["TELEFONO"]);
                oProv.Email = Convert.ToString(Reader["EMAIL"]);
                oProv.Calle = Convert.ToString(Reader["CALLE"]);
                oProv.Num_Ext = Convert.ToString(Reader["NUM_EXT"]);
                oProv.Num_int = Convert.ToString(Reader["NUM_INT"]);
                oProv.Colonia = Convert.ToString(Reader["COLONIA"]);
                oProv.Municipio = Convert.ToString(Reader["MUNICIPIO"]);
                oProv.Ciudad = Convert.ToString(Reader["CIUDAD"]);
                oProv.Estado = Convert.ToString(Reader["ESTADO"]);
                oProv.Pais = Convert.ToString(Reader["PAIS"]);
                oProv.CP = Convert.ToString(Reader["CP"]);
                oProv.Telefono1 = Convert.ToString(Reader["TELEFONO_EMPRESA1"]);
                oProv.Telefono2 = Convert.ToString(Reader["TELEFONO_EMPRESA2"]);
                oProv.Regimen_Fiscal = Convert.ToString(Reader["REGIMEN_FISCAL"]);
                oProv.Servicio = Convert.ToString(Reader["SERVICIO"]);
                oProv.Estatus = Convert.ToString(Reader["ESTATUS"]);
                oProv.Fecha = Convert.ToString(Reader["FECHA"]);
                oProv.Cuenta = Convert.ToString(Reader["CUENTA"]);
                oProv.Banco = Convert.ToString(Reader["BANCO"]);
                oProv.Clabe = Convert.ToString(Reader["CLABE_INTER"]);
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return oProv;
      }
    }
}
