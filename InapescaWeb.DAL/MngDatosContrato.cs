using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using InapescaWeb;
using System.Globalization;

namespace InapescaWeb.DAL
{
    public class MngDatosContrato
    {
        static string lsQuery;
        static clsDictionary Dictionary = new clsDictionary();
        static string lsYear = DateTime.Now.Year.ToString();
        static string lsHoy =clsFunciones.FormatFecha ( DateTime.Today.ToString());
        static string max;
        static int liNumero;

        public static string Obtiene_Nombre_Completo_AdmCont(string psRol, string psUbicacion)
        {
            string Nombre;
            string lsQuery = "";
            lsQuery = "   SELECT  DISTINCT (CONCAT(ABREVIATURA,' ',NOMBRE,' ',AP_PAT,' ', AP_MAT)) AS NOMBRE ";
            lsQuery += "  FROM crip_job";
            lsQuery += "WHERE ROL  = '" + psRol+ "'";
            lsQuery += "AND PERIODO  = '" + Convert.ToString(DateTime.Today.Year.ToString());
            lsQuery += "AND CLV_DEP  = '" + psUbicacion + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Nombre = Convert.ToString(Reader["NOMBRE"]);
            }
            else
            {
                Nombre = "";
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Nombre;
        }

        public static Boolean InsertDetalleCont(SolicitudPSP oPSP, string psUsuario)
        {
            Boolean pbResult;
            string lsQuery = "";
            string Cadena = oPSP.Telefono.Replace(".", "");
            Cadena = Cadena.Replace("/", "");
            Cadena = Cadena.Replace("-", "");
            Cadena = Cadena.Replace("(", "");
            Cadena = Cadena.Replace(")", "");
            Cadena = Cadena.Replace(" ", "");
            lsQuery = "   INSERT INTO ct_peticiones(  ";
            lsQuery += "  ID_SOLICITUD, ";
            lsQuery += "  NOMBRE, ";
            lsQuery += "  APELLIDO_PAT, ";
            lsQuery += "  APELLIDO_MAT, ";
            lsQuery += "  EMAIL, ";
            lsQuery += "  TIPOID, ";
            lsQuery += "  N_ID, ";
            lsQuery += "  RFC, ";
            lsQuery += "  ACTIVIDAD, ";
            lsQuery += "  CLABEINT,  ";
            lsQuery += "  TELEFONO, ";
            lsQuery += "  CALLE, ";
            lsQuery += "  NUM_EXT, ";
            lsQuery += "  NUM_INT, ";
            lsQuery += "  COLONIA, ";
            lsQuery += "  CIUDAD, ";
            lsQuery += "  ESTADO, ";
            lsQuery += "  COD_POSTAL, ";
            lsQuery += "  OBJETO, ";
            lsQuery += "  FECHA_I, ";
            lsQuery += "  FECHA_F, ";
            lsQuery += "  MENSUAL_SIVA, ";
            lsQuery += " IVA_MENSUAL, ";
            lsQuery += "  MENSUAL_BRUTO, ";
            lsQuery += "  MONTO_CONTRATO, ";
            lsQuery += "  NOM2, ";
            lsQuery += "  AP_PAT2, ";
            lsQuery += "  AP_MAT2, ";
            lsQuery += "  MONTO2, ";
            lsQuery += "  NOM3, ";
            lsQuery += "  AP_PAT3, ";
            lsQuery += "  AP_MAT3, ";
            lsQuery += "  MONTO3, ";
            lsQuery += "  DIAS_NAT, ";
            lsQuery += "  EXHIBICIONES, ";
            lsQuery += "  LUGAR_EJECUCION, ";
            lsQuery += "  DIRECCION_LUGAR, ";
            lsQuery += "  DIRECCION, ";
            lsQuery += "  OFICINA, ";
            lsQuery += "  PISO, ";
            lsQuery += "  ADMIN_CONTRATO, ";
            lsQuery += "  FECHA, ";
            lsQuery += "  FECHA_EFF, ";
            lsQuery += "  SEC_EFF, ";
            lsQuery += "  SOLICITA, ";
            lsQuery += "  ESTATUS, ";
            lsQuery += "  PERIODO ";
            lsQuery += "  ) ";
            lsQuery += "  VALUES ";
            lsQuery += "  ( ";
            lsQuery += " '" + IDSolicitud() + "', ";
            lsQuery += " '" + oPSP.NombrePSP + "', ";
            lsQuery += " '" + oPSP.ApPatPSP + "', ";
            lsQuery += " '" + oPSP.ApMatPSP + "', ";
            lsQuery += " '" + oPSP.EMAIL + "', " ;
            lsQuery += " '" + oPSP.TipoID + "', ";
            lsQuery += " '" + oPSP.NoID + "', ";
            lsQuery += " '" + oPSP.RFC + "', ";
            lsQuery += " '" + oPSP.ACT + "',";
            lsQuery += " '" + oPSP.Clabe + "', ";
            lsQuery += " '" + Cadena + "', ";
            lsQuery += " '" + oPSP.Calle + "', ";
            lsQuery += " '" + oPSP.NoExt + "', ";
            lsQuery += " '" + oPSP.NoInt + "', ";
            lsQuery += " '" + oPSP.Colonia + "', ";
            lsQuery += " '" + oPSP.Municipio + "', ";
            lsQuery += " '" + oPSP.Estado + "', ";
            lsQuery += " '" + oPSP.CP + "', ";
            lsQuery += " '" + oPSP.ObjContrato + "', ";
            lsQuery += " '" + oPSP.FechInc + "', ";
            lsQuery += " '" + oPSP.FechFin + "', ";
            lsQuery += " '" + Convert.ToDouble(oPSP.MontoMensualSinIVA) + "', ";
            lsQuery += " '" + Convert.ToDouble(oPSP.IVA) + "', ";
            lsQuery += " '" + Convert.ToDouble(oPSP.MontoMensualConIVA) + "', ";
            lsQuery += " '" + Convert.ToDouble(oPSP.MontoTotalCont) + "', ";
            lsQuery += " '" + oPSP.NombrePart2 + "', ";
            lsQuery += " '" + oPSP.ApPatPart2 + "', ";
            lsQuery += " '" + oPSP.ApMatPart2 + "', ";
            lsQuery += " '" + Convert.ToDouble(oPSP.MontoTotalContPart2) + "', ";
            lsQuery += " '" + oPSP.NombrePart3 + "', ";
            lsQuery += " '" + oPSP.ApPatPart3 + "', ";
            lsQuery += " '" + oPSP.ApMatPart3 + "',";
            lsQuery += " '" + Convert.ToDouble(oPSP.MontoTotalContPart3) + "', ";
            lsQuery += " '" + oPSP.DiasNat + "', ";
            lsQuery += " '" + oPSP.Exhibiciones + "', ";
            lsQuery += " '" + oPSP.Ubicacion + "', ";
            lsQuery += " '" + oPSP.DireccionUbica + "', ";
            lsQuery += " '" + oPSP.DGA + "', ";
            lsQuery += " '" + oPSP.Oficina + "', ";
            lsQuery += " '" + oPSP.Piso + "', ";
            lsQuery += " '" + oPSP.AdminContr + "', ";
            lsQuery += " '" + lsHoy + "', ";
            lsQuery += " '" + lsHoy + "', ";
            lsQuery += " '1', ";
            lsQuery += " '" + psUsuario + "', ";
            lsQuery += " '1', ";
            lsQuery += " '" + lsYear + "') ";
             
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_Contratos();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
         //  MySqlDataReader Reader = cmd.ExecuteReader();

            if (cmd.ExecuteNonQuery() == 1)
            {
                pbResult = true;
            }
            else
            {
                pbResult = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return pbResult;
        }

        public static int IDSolicitud()
        {
            
            string lsQuery = " SELECT MAX(ID_SOLICITUD) AS MAX  ";
            lsQuery += "  FROM ct_peticiones   ";
            lsQuery += "  WHERE PERIODO = '" + lsYear + "'  ";
            lsQuery += "  AND ESTATUS = '1'  ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_Contratos();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
           
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if ((max == "") || (max == null ))
                {
                    max = "0";
                }

                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }

            Reader.Close();
        
            MngConexion.disposeConexionSMAF(ConexionMysql);


            return liNumero;
        }

        public static DataSet ObtieneDetalleSolicitudesPSP(string psAnio, string psInicio, string psFinal, string psAdscripcion)
        {
            DataSet datasetArbol = new DataSet("DataSetArbol");
            string query = "";

            query += "  SELECT ID_SOLICITUD, ";
            query += "  NOMBRE,  ";
            query += "  APELLIDO_PAT,  ";
            query += "  APELLIDO_MAT, ";
            query += "  EMAIL, ";
            query += "  TIPOID, ";
            query += "  N_ID, ";
            query += "  RFC, ";
            query += "  ACTIVIDAD, ";
            query += "  CLABEINT,  ";
            query += "  TELEFONO, ";
            query += "  CALLE, ";
            query += "  NUM_EXT,   ";
            query += "  NUM_INT, ";
            query += "  COLONIA, ";
            query += "  CIUDAD, ";
            query += "  ESTADO,  ";
            query += "  COD_POSTAL,  ";
            query += "  OBJETO, ";
            query += "  FECHA_I, ";
            query += "  FECHA_F, ";
            query += "  MENSUAL_SIVA, ";
            query += "  IVA_MENSUAL, ";
            query += "  MENSUAL_BRUTO, ";
            query += "  MONTO_CONTRATO,  ";
            query += "  NOM2, ";
            query += "  AP_PAT2, ";
            query += "  AP_MAT2,   ";
            query += "  MONTO2, ";
            query += "  NOM3, ";
            query += "  AP_PAT3, ";
            query += "  AP_MAT3, ";
            query += "  MONTO3, ";
            query += "  DIAS_NAT, ";
            query += "  EXHIBICIONES, ";
            query += "  LUGAR_EJECUCION, ";
            query += "  DIRECCION_LUGAR, ";
            query += "  DIRECCION, ";
            query += "  ADMIN_CONTRATO, ";
            query += "  PERIODO ";
            query += "  FROM ct_peticiones ";
            query += "  WHERE 1 = 1     ";
            query += "   AND PERIODO = '" + psAnio + "' ";
            query += "  AND FECHA_EFF BETWEEN '" + psInicio + "' AND '" + psFinal + "'";

            if (psAdscripcion != "Todas")
            {
                query += " AND LUGAR_EJECUCION = '" + psAdscripcion + "'";
            }
            
            
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_Contratos();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConexionMysql);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return datasetArbol;
        }
    }
}
