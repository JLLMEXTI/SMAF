using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace InapescaWeb.DAL
{
    public class MngDatosSolicitud
    {

        static string max;
        static int liNumero;
        // static string Dictionary.FECHA_NULA = "1900-01-01";
        static readonly clsDictionary dictionary = new clsDictionary();
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());


        public static SolicitudMC Detalle_SolicitudMC(string psFolio, string psPeriodo="")
        {
            string query = "    SELECT DISTINCT FOLIO,";
            query += "          SOLICITUD, ";
            query += "          FECHA AS FECHA_RECEPCION, ";
            query += "          SOLICITANTE , ";
            query += "          CORREO_ELECT_SOLICITANTE AS CORREO_SOLICITANTE, ";
            query += "          ASUNTO ,";
            query += "          RECURSO,";
            query += "          DESCRIPCION,";
            query += "          OF_RESPUESTA AS OFICIO_SEGUIMIENTO,";
            query += "          FECH_RESPUESTA AS FECHA_OFICIO_SEGUIMIENTO,";
            query += "          TIPO_RESPUESTA AS TIPO_DOCUMENTO,";
            query += "          ESTATUS,";
            query += "          PERIODO,";
            query += "          OBSERVACIONES,";
            query += "          DOCUMENTO,";
            query += "          EXPEDIENTE";
            query += "          FROM solicitudes WHERE FOLIO = '" + psFolio + "'";
            query += "          AND SEC_EFF = '" + Obtiene_SecEff(psFolio) + "'";

           
           MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
           MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
           cmd.Connection.Open();

           MySqlDataReader reader = cmd.ExecuteReader();

           SolicitudMC objetoSolicitud = new SolicitudMC();
           if (reader.Read())
           {

               objetoSolicitud.Folio = Convert.ToString(reader["FOLIO"]);
               objetoSolicitud.OfSolicitud = Convert.ToString(reader["SOLICITUD"]);

               objetoSolicitud.FechaSol = Convert.ToString(reader["FECHA_RECEPCION"]);
               objetoSolicitud.Solicitante = Convert.ToString(reader["SOLICITANTE"]);

               objetoSolicitud.Correo = Convert.ToString(reader["CORREO_SOLICITANTE"]);

               objetoSolicitud.Asunto = Convert.ToString(reader["ASUNTO"]);
               objetoSolicitud.Recurso = Convert.ToString(reader["RECURSO"]);
               objetoSolicitud.Descripcion = Convert.ToString(reader["DESCRIPCION"]);
               objetoSolicitud.OfRespuesta = Convert.ToString(reader["OFICIO_SEGUIMIENTO"]);
               objetoSolicitud.FechaResp = Convert.ToString(reader["FECHA_OFICIO_SEGUIMIENTO"]);
               objetoSolicitud.TipoResp = Convert.ToString(reader["TIPO_DOCUMENTO"]);
               objetoSolicitud.Estatus = Convert.ToString(reader["ESTATUS"]);
               objetoSolicitud.Periodo = Convert.ToString(reader["PERIODO"]);
               objetoSolicitud.Observaciones = Convert.ToString(reader["OBSERVACIONES"]);
               objetoSolicitud.Documento = Convert.ToString(reader["DOCUMENTO"]);
               objetoSolicitud.Expediente = Convert.ToString(reader["EXPEDIENTE"]);

               
           }
           reader.Close();
           MngConexion.disposeConexionMC(ConexionMysql);

           return objetoSolicitud;
        }

        public static List<Entidad> ObtieneListaEstatus(Boolean psFlag = false)
        {
            string Query = "";
            Query = " SELECT CLV_ESTATUS AS CODIGO, DESCRIPCION AS DESCRIPCION ";
            Query += "FROM estatus";
            if (psFlag == true)
            {
                Query += " WHERE CLV_ESTATUS NOT IN ('0','1')";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo="00";
            objetoEntidad1.Descripcion = " = S E L E C C I O N E = ";
            List<Entidad> ListaEntidad = new List<Entidad>();
            ListaEntidad.Add(objetoEntidad1);

            while (reader.Read())
            {

                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(reader["CODIGO"]);

                objetoEntidad.Descripcion = Convert.ToString(reader["DESCRIPCION"]);
                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionMC(ConexionMysql);

            return ListaEntidad;

        }

        public static string ObtieneDGA(string psUbicacion)
        {
            string Adscripcion = "";
            string Query = "SELECT CLV_DEP AS CODIGO,DESCR_CORTO AS DESCRIPCION FROM crip_dependencia WHERE CLV_ORG = 'INAPESCA' AND CLV_SECRE = 'SADER' AND ESTATUS = '1'";
            Query += " AND CLV_DEP='" + psUbicacion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();


            while (Reader.Read())
            {
                
                Adscripcion= Convert.ToString(Reader["DESCRIPCION"]);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();

            return Adscripcion;

        }
        public static string ObtieneDocumento(string psFolio)
        {
            string Documento = "";
            string Query = "SELECT DOCUMENTO AS CODIGO FROM solicitudes WHERE ESTATUS != '0'";
            Query += " AND FOLIO'" + psFolio + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();


            while (Reader.Read())
            {

                Documento = Convert.ToString(Reader["CODIGO"]);

            }

            MngConexion.disposeConexionMC(ConexionMysql);
            Reader.Close();

            return Documento;
        
        }

        //public static Boolean UpdateEstatusSolicitud(string psFolio, string psSecEff)
        //{
        //    Boolean Flag = false;
        //    string query = "UPDATE crip_comision SET ESTATUS = '0'";

        //}

        public static Boolean Inserta_Documento(string psFolio, string psNameDoc, string psFechaActual)
        {
            Boolean lbResultado;
            string lsQuery = "   INSERT INTO Solicitudes";
            lsQuery += "(DOCUMENTO)";
            lsQuery += "VALUES ( '" + psNameDoc + "')";
            lsQuery += "WHERE FOLIO='" + psFolio + "' AND ESTATUS!='0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() >= 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionMC(ConexionMysql);

            return lbResultado;

        }

        public static Boolean InsertNuevoRegSolicitud(SolicitudMC psDetSolicitud, string psEstatus, string psFecha, string psObs, string psUsuario, string psOfResp)
        {
            Boolean Resultado = false;
            string lsQuery;

            lsQuery = " INSERT INTO solicitudes ";
            lsQuery += " ( ";
            lsQuery += " FOLIO, ";
            lsQuery += " SOLICITUD, ";
            lsQuery += " FECHA, ";
            lsQuery += " SOLICITANTE, ";
            lsQuery += " CORREO_ELECT_SOLICITANTE, ";
            lsQuery += " ASUNTO, ";
            lsQuery += " RECURSO, ";
            lsQuery += " DESCRIPCION, ";
            lsQuery += " OF_RESPUESTA, ";
            lsQuery += " FECH_RESPUESTA, ";
            lsQuery += " TIPO_RESPUESTA, ";
            lsQuery += " SEC_EFF, ";
            lsQuery += " FECH_EFF, ";
            lsQuery += " ESTATUS, ";
            lsQuery += " ESTATUS_INT, ";
            lsQuery += " PERIODO, ";
            lsQuery += " USUARIO, ";
            lsQuery += " OBSERVACIONES, ";
            lsQuery += " DOCUMENTO, ";
            lsQuery += " EXPEDIENTE ";
            lsQuery += " ) ";
            lsQuery += " VALUES ";
            lsQuery += " ( ";
            lsQuery += " '" + psDetSolicitud.Folio + "', ";
            lsQuery += " '" + psDetSolicitud.OfSolicitud + "', ";
            lsQuery += " '" + clsFunciones.FormatFecha(Convert.ToString(psDetSolicitud.FechaSol)) + "', ";
            lsQuery += " '" + psDetSolicitud.Solicitante + "', ";
            lsQuery += " '" + psDetSolicitud.Correo + "', ";
            lsQuery += " '" + psDetSolicitud.Asunto + "', ";
            lsQuery += " '" + psDetSolicitud.Recurso + "', ";
            lsQuery += " '" + psDetSolicitud.Descripcion + "', ";
            lsQuery += " '" + clsFunciones.ConvertMayus(psOfResp) + "', ";
            lsQuery += " '" + clsFunciones.FormatFecha(Convert.ToString(psFecha)) + "', ";
            lsQuery += " '" + clsFunciones.ConvertMayus(psObs) + "', ";
            lsQuery += " '"+ Obtiene_Max(psDetSolicitud.Folio)+"', ";
            lsQuery += " '" + lsHoy + "', ";
            lsQuery += " '"+ psEstatus +"', ";
            lsQuery += " '1', ";
            lsQuery += " '" + Convert.ToInt32(psDetSolicitud.Periodo) + "', ";
            lsQuery += " '" + psUsuario+ "', ";
            lsQuery += " '" + clsFunciones.ConvertMayus(psObs) + "', ";
            lsQuery += " '" + psDetSolicitud.Documento + "', ";
            lsQuery += " '" + psDetSolicitud.Expediente + "' ";
            lsQuery += " ) ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();


            if (cmd.ExecuteNonQuery() == 1)
            {
                Resultado = true;
            }
            else
            {
                Resultado = false;
            }

            MngConexion.disposeConexionMC(ConexionMysql);

            return Resultado;
        }

        public static string Obtiene_Max(string psFolio)
        {
            string max = "0";
            string query = "SELECT MAX(SEC_EFF) AS MAX FROM solicitudes WHERE  FOLIO = '" + psFolio + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);
            }

            if (max == "")
            {
                max = "0";
            }

            MngConexion.disposeConexionMC(ConexionMysql);

            max = clsFunciones.ConvertString(clsFunciones.ConvertInteger(max) + 1);

            return max;
        }

        public static string Obtiene_SecEff(string psFolio)
        {
            string max = "0";
            string query = "SELECT MAX(SEC_EFF) AS MAX FROM solicitudes WHERE  FOLIO = '" + psFolio + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_ModuloConsulta();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);
            }

            if (max == "")
            {
                max = "1";
            }

            MngConexion.disposeConexionMC(ConexionMysql);


            return max;
        }
    }
}
