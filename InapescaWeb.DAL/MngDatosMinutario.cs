using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using InapescaWeb;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace InapescaWeb.DAL
{
    public class MngDatosMinutario
    {
        static CultureInfo culture = new CultureInfo("es-MX");
        static clsDictionary Dictionary = new clsDictionary();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static double medio = 0.5;
        static string separador;
        static string year = DateTime.Today.Year.ToString();
        private static DataSet datasetArbol;

        public static Boolean Update_Estatus_Reservado(string psReservado, string psPeriodo, string psEstatus, string psOficio,bool pBandera = false )
        {
            Boolean lbResultado;
            string Query = "";
            Query += "  UPDATE dgaipp_minutario SET ";
            if(pBandera )
            {
                Query += "ESTATUS = '" + psEstatus + "' , FECHA_EFF = '"+ lsHoy +"'";
            }else
            {
                Query += "ESTATUS_RESERVADO = '" + psEstatus + "' , FECHA_EFF = '"+ lsHoy + "' ";
            }
            
            Query += "    WHERE 1=1  ";
            if (psReservado != Dictionary.NUMERO_CERO)
            {
                Query += " AND FOLIO_RESERVADO = '" + psReservado + "' ";
            }
            Query += "   AND PERIODO = '" + psPeriodo + "'";
            Query += " AND NO_OFICIO= '" + psOficio + "'";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);

            return lbResultado;
        }

        public static Boolean Inserta_Oficio_Comision(string psOficio, string psArchivo, string psTipoOficio, string psReservado, string psObjetivo, string psDoc, string psDep, string psDescripcion, string psComisionado, string psUsuario, string psSec, string psPeriodo)
        {
            Boolean lbResultado;
            string Query = " INSERT INTO dgaipp_minutario  ";
            Query += " ( ";
            Query += " NO_OFICIO , ";
            Query += " ARCHIVO, ";
            Query += " TIPO_OFICIO,";
            Query += " FOLIO_RESERVADO,";
            Query += " EXPEDIENTE,";
            Query += " DOC_REF, ";
            Query += " CLV_DEP, ";
            Query += " DESCRIPCION,";
            Query += " USUARIO , ";
            Query += " REGISTRO, ";
            Query += " FECHA, ";
            Query += " ESTATUS, ";
            Query += " SECUENCIA,";
            Query += " PERIODO ";
            Query += " )";
            Query += " VALUES";
            Query += " ( ";
            Query += " '" + psOficio + "',";
            Query += " '" + psArchivo + "',";
            Query += " '" + psTipoOficio + "',";
            Query += " '" + psReservado + "',";
            Query += " '" + psObjetivo + "',";
            Query += " '" + psDoc + "',";
            Query += " '" + psDep + "',";
            Query += " '" + psDescripcion + "',";
            Query += " '" + psComisionado + "',";
            Query += " '" + psUsuario + "',";
            Query += " '" + lsHoy + "',";
            Query += " '1',";
            Query += " '" + psSec + "',";
            Query += " '" + psPeriodo + "'";
            Query += " ) ";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);

            return lbResultado;
        }

        public static string Obtiene_Max_Secuencia(string Periodo, string psOficio, string psComplemento = "")
        {
            string max = "";
            int liNumero = 0;
            string Query = "SELECT MAX(SECUENCIA) AS MAX  ";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1  ";
            Query += " AND ESTATUS = '1'";
            Query += " AND PERIODO = '" + Periodo + "' ";
            Query += " and NO_OFICIO = '" + psOficio + "'";

            if (psComplemento != "")
            {
                Query += " and COMPLEMENTO = '" + psComplemento + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }

            }

            liNumero = Convert.ToInt32(max) + 1;

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(liNumero);
        }

        public static string Obtiene_Max_Oficio(string psPeriodo)
        {
            string max = "";
            int liNumero;
            string Query = " SELECT MAX(NO_OFICIO) AS MAX   ";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1  ";
            Query += " AND PERIODO = '" + psPeriodo + "' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }

            }

            liNumero = Convert.ToInt32(max) + 1;

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Convert.ToString(liNumero);
        }

        public static string Obtiene_Max_Reservado(string Periodo)
        {
            string max = "";
            int liNumero;
            string Query = " SELECT MAX(FOLIO_RESERVADO) AS MAX  ";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1  ";
            Query += " AND PERIODO = '" + Periodo + "' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }

            }



            liNumero = Convert.ToInt32(max) + 1;

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Convert.ToString(liNumero);
        }

        public static string Obtiene_Max_Reservado(string Periodo, string psOficio, string psComplemento = "")
        {
            string max = "";
            int liNumero;
            string Query = " SELECT MAX(FOLIO_RESERVADO) AS MAX  ";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1  ";
            Query += " AND ESTATUS = '1'";
            Query += " AND PERIODO = '" + Periodo + "' ";
            //  Query += " and NO_OFICIO = '" + psOficio + "'";

            if (psComplemento != "")
            {
                Query += " and COMPLEMENTO = '" + psComplemento + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }

            }

            liNumero = Convert.ToInt32(max) + 1;

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Convert.ToString(liNumero);
        }

        public static Minutario oMinutarioOficio(string psReservado, string psPeriodo = "")
        {
            string Query = " SELECT * ";
            Query += " FROM dgaipp_minutario ";
            Query += "     WHERE 1=1 ";
            Query += " AND FOLIO_RESERVADO = '" + psReservado + "'";

            if (psPeriodo != "")
            {
                Query += "and PERIODO = '" + psPeriodo + "'";
            }
            else
            {
                Query += "and PERIODO = '" + year + "'";
            }


            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            Minutario oMinutario = new Minutario();

            while (Reader.Read())
            {
                oMinutario.Oficio = Convert.ToString(Reader["NO_OFICIO"]);
                oMinutario.Complemento = Convert.ToString(Reader["COMPLEMENTO"]);
                oMinutario.Archivo = Convert.ToString(Reader["ARCHIVO"]);
                oMinutario.Tipo = Convert.ToString(Reader["TIPO_OFICIO"]);
                oMinutario.Reservado = Convert.ToString(Reader["FOLIO_RESERVADO"]);
                oMinutario.Expediente = Convert.ToString(Reader["EXPEDIENTE"]);
                oMinutario.Docuemnto_Referencia = Convert.ToString(Reader["DOC_REF"]);
                oMinutario.Ubicacion_Destino = Convert.ToString(Reader["CLV_DEP"]);
                oMinutario.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                oMinutario.Usuario_destino = Convert.ToString(Reader["USUARIO"]);
                oMinutario.Usuario_Captura = Convert.ToString(Reader["REGISTRO"]);
                oMinutario.Fecha_captura = Convert.ToString(Reader["FECHA"]);
                oMinutario.Estatus = Convert.ToString(Reader["ESTATUS"]);
                oMinutario.Secuencia = Convert.ToString(Reader["SECUENCIA"]);
                oMinutario.Periodo = Convert.ToString(Reader["PERIODO"]);
                oMinutario.Estatus_Reservado = Convert.ToString(Reader["ESTATUS_RESERVADO"]);
            }
            return oMinutario;

        }

        public static Minutario oMinutario(string Periodo, string psOficio, string psComplemento = "")
        {
            string Query = " SELECT * FROM dgaipp_minutario  ";
            Query += " WHERE 1=1  ";
            /* Query += " AND ESTATUS = '1'"*/
            Query += " AND PERIODO = '" + Periodo + "' ";
            Query += " and NO_OFICIO = '" + psOficio + "'";

            if (psComplemento != "")
            {
                Query += " and COMPLEMENTO = '" + psComplemento + "'";
            }

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            Minutario oMinutario = new Minutario();
            while (Reader.Read())
            {
                oMinutario.Oficio = Convert.ToString(Reader["NO_OFICIO"]);
                oMinutario.Complemento = Convert.ToString(Reader["COMPLEMENTO"]);
                oMinutario.Archivo = Convert.ToString(Reader["ARCHIVO"]);
                oMinutario.Tipo = Convert.ToString(Reader["TIPO_OFICIO"]);
                oMinutario.Reservado = Convert.ToString(Reader["FOLIO_RESERVADO"]);
                oMinutario.Expediente = Convert.ToString(Reader["EXPEDIENTE"]);
                oMinutario.Docuemnto_Referencia = Convert.ToString(Reader["DOC_REF"]);
                oMinutario.Ubicacion_Destino = Convert.ToString(Reader["CLV_DEP"]);
                oMinutario.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                oMinutario.Usuario_destino = Convert.ToString(Reader["USUARIO"]);
                oMinutario.Usuario_Captura = Convert.ToString(Reader["REGISTRO"]);
                oMinutario.Fecha_captura = Convert.ToString(Reader["FECHA"]);
                oMinutario.Estatus = Convert.ToString(Reader["ESTATUS"]);
                oMinutario.Secuencia = Convert.ToString(Reader["SECUENCIA"]);
                oMinutario.Periodo = Convert.ToString(Reader["PERIODO"]);
                oMinutario.Estatus = Convert.ToString(Reader["ESTATUS"]);
            }
            return oMinutario;
        }

        public static string ObtieneExpediente(string Periodo, string psOficio, string psComplemento = "")
        {
            string Resultado = "";
            string Query = "SELECT EXPEDIENTE AS DESCRIPCION    ";
            Query += "  FROM dgaipp_minutario  ";
            Query += " WHERE ESTATUS = '1'  ";
            Query += " AND PERIODO = '" + Periodo + "' ";
            Query += " and NO_OFICIO = '" + psOficio + "'";

            if (psComplemento != "")
            {
                Query += " and COMPLEMENTO = '" + psComplemento + "'";
            }


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["DESCRIPCION"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "";
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysql);

            return Resultado;

        }

        public static List<Entidad> ListaReservados1(string Periodo, string psOficio, string psReservado = "0", string psComplemento = "", bool bandera = false)
        {
            string Query = "";

            Query += "SELECT FOLIO_RESERVADO ,DOC_REF AS DESCRIPCION,ESTATUS_RESERVADO ";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1 ";

            if (psOficio != Dictionary.NUMERO_CERO)
            {
                Query += " AND  NO_OFICIO = '" + psOficio + "'";
            }

            if (psReservado != Dictionary.NUMERO_CERO)
            {
                Query += " AND FOLIO_RESERVADO = '" + psReservado + "'";
            }

            Query += " AND PERIODO = '" + Periodo + "'";

            if (psComplemento != "")
            {
                Query += " and COMPLEMENTO = '" + psComplemento + "'";
            }

            if (bandera)
            {
                Query += " AND ESTATUS_RESERVADO = '1'";
                Query += " AND FOLIO_RESERVADO != 0";
            }

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> listaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["FOLIO_RESERVADO"]) + "|" + Convert.ToString(Reader["ESTATUS_RESERVADO"]);
                objetoEntidad.Descripcion = " Reservado Num : " + Convert.ToString(Reader["FOLIO_RESERVADO"]) + " Expediente : " + Convert.ToString(Reader["DESCRIPCION"]);

                listaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
            return listaEntidad;


        }
       
        public static List<Entidad> ListaReservados(string Periodo, string psOficio, string psComplemento = "",bool pbbandera =false )
        {
            string Query = "";

            Query += "SELECT FOLIO_RESERVADO ,DOC_REF AS DESCRIPCION,ESTATUS_RESERVADO";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1 ";

            if (pbbandera)
            {
                Query += " AND FOLIO_RESERVADO != '0'";
            }
            else
            {
                Query += " AND  NO_OFICIO = '" + psOficio + "'";
            }
            
            Query += " AND PERIODO = '" + Periodo + "'";

            if (psComplemento != "")
            {
                Query += " and COMPLEMENTO = '" + psComplemento + "'";
            }

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> listaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                if (pbbandera)
                {
                    objetoEntidad.Codigo = Convert.ToString(Reader["FOLIO_RESERVADO"]) + "|" + Convert.ToString(Reader["ESTATUS_RESERVADO"]);
                }
                else
                {
                    objetoEntidad.Codigo = Convert.ToString(Reader["FOLIO_RESERVADO"]);
                }
                objetoEntidad.Descripcion = " Reservado Num : " + Convert.ToString(Reader["FOLIO_RESERVADO"]) + " Expediente : " + Convert.ToString(Reader["DESCRIPCION"]);

                listaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
            return listaEntidad;

        }

        public static List<Entidad> Lista_Tipos_Oficios(string psRol)
        {
            string Query = "";

            Query = "SELECT TIPO AS CODIGO , DESCRIPCION AS DESCRIPCION ";
            Query += "  FROM dgaipp_tipo ";
            Query += " WHERE ESTATUS = '1' ";
            Query += "AND ID_ROL = '" + psRol + "'";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> listaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                listaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
            return listaEntidad;

        }

        public static Boolean Update_Reservado(string psReservado, string Periodo, string psOficio, string psSec, string psComplemento = "", bool pbBandera = false)
        {
            bool bandera = false;
            string query = " UPDATE dgaipp_minutario ";
            //  query += "  SET FOLIO_RESERVADO = '" + psReservado + "'";

            if (pbBandera)
            {
                query += "SET ESTATUS = '0'";
            }
            else
            {
                query += "  SET FOLIO_RESERVADO = '" + psReservado + "'";
            }


            query += "  WHERE 1=1  ";
            query += "  AND ESTATUS = '1'";
            query += "  AND PERIODO = '" + Periodo + "'";
            query += "  AND NO_OFICIO = '" + psOficio + "'";

            if (psComplemento != "")
            {
                query += " and COMPLEMENTO = '" + psComplemento + "'";
            }

            if (pbBandera)
            {
                query += " AND SECUENCIA = '" + psSec + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            //   MySqlDataReader Reader = cmd.ExecuteReader();

            if (cmd.ExecuteNonQuery() == 1) bandera = true;
            else bandera = false;

            MngConexion.disposeConexion_dgaipp(ConexionMysql);

            return bandera;
        }

        public static Boolean Update_Oficio(Minutario poMinutario)
        {
            bool bandera = false;
            string query = "UPDATE dgaipp_minutario ";
            query += " SET NO_OFICIO = '" + poMinutario.Oficio + "',";
            query += " COMPLEMENTO = '" + poMinutario.Complemento + "',";
            query += " ARCHIVO = '" + poMinutario.Archivo + "',";
            query += " DOC_REF = '" + poMinutario.Docuemnto_Referencia + "',";
            query += " REGISTRO = '" + poMinutario.Usuario_Captura + "',";
            query += " FECH_EFF =  '" + lsHoy + "'";
            query += " WHERE 1=1 ";
            query += " AND FOLIO_RESERVADO = '" + poMinutario.Reservado + "'";
            query += " and PERIODO = '" + poMinutario.Periodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            //   MySqlDataReader Reader = cmd.ExecuteReader();

            if (cmd.ExecuteNonQuery() == 1) bandera = true;
            else bandera = false;

            MngConexion.disposeConexion_dgaipp(ConexionMysql);

            return bandera;
        }

        public static DataSet ReturnDataSet(string psPadre)
        {

            string lsQuery;

            datasetArbol = new DataSet("DataSetArbol");

            lsQuery = "SELECT DISTINCT NO_OFICIO AS MODULO,DOC_REF as DESCRIPCION , NO_OFICIO AS PADRE ";
            lsQuery += " FROM dgaipp_minutario ";
            lsQuery += " WHERE PERIODO = '" + psPadre + "' ";
            lsQuery += " AND NO_OFICIO != '0'";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysqlDgaipp);

            MySqlDataAdapter adapter = new MySqlDataAdapter(lsQuery, ConexionMysqlDgaipp);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");

            MngConexion.disposeConexionSMAF(ConexionMysqlDgaipp);
            // ConexionMysql = null;

            return datasetArbol;
        }

        public static List<Entidad> Lista_Oficios(string psperiodo,bool pBandera = false )
        {
            string lsQuery = " SELECT DISTINCT NO_OFICIO AS CODIGO, DOC_REF AS DESCRIPCION,ESTATUS ";
            lsQuery += "  FROM  dgaipp_minutario ";
            lsQuery += "  WHERE PERIODO= '" + psperiodo + "'";
            if (pBandera)
            {
                ;
            }
            else
            {
                lsQuery += " AND ESTATUS = '1'";
            }

            lsQuery += " AND NO_OFICIO != '0' ";
            lsQuery += " ORDER BY NO_OFICIO ASC";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> listaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                if (pBandera)
                {
                    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]) + "|" + Convert.ToString(Reader["ESTATUS"]);
                }
                else
                {
                    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                }

                objetoEntidad.Descripcion = " Oficio : " + Convert.ToString(Reader["CODIGO"]) + " - Expediente " + Convert.ToString(Reader["DESCRIPCION"]);

                listaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
            return listaEntidad;

        }

        public static List<Entidad> Lista_Oficios_Sin_Reservado(string psPeriodo,bool pBandera = false )
        {
            string Query = "";
            Query += "SELECT DISTINCT NO_OFICIO AS NO_OFICIO ";
            Query += " FROM dgaipp_minutario ";
            Query += " WHERE 1=1 ";
            Query += " AND NO_OFICIO != '0'";
            Query += "   AND PERIODO= '" + psPeriodo + "'";
            Query += " AND FOLIO_RESERVADO  = '0'";

            MySqlConnection ConexionMysqlDgaipp = MngConexion.getConexionMysql_dgaipp();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysqlDgaipp);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> listaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["NO_OFICIO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["NO_OFICIO"]);

                listaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexion_dgaipp(ConexionMysqlDgaipp);
            return listaEntidad;

        }


    }
}
