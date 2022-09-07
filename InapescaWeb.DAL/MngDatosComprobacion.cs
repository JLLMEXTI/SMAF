using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using InapescaWeb;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosComprobacion
    {

        static clsDictionary Dictionary = new clsDictionary();
        private static DataSet datasetArbol;
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static string NOm_Reintegro(string psOficio, string psArchivo)
        {
            string Resultado = "";
            string Query = "";
            Query += "    SELECT DOCUMENTO_COMPROBACION AS REINTEGRO  ";
            Query += "    FROM crip_comision_comprobacion ";
            Query += " WHERE 1=1 ";
            Query += " AND NO_OFICIO = '" + psOficio + "' ";
            Query += " AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND CONCEPTO = '13'";
            Query += " AND ESTATUS != '0'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["REINTEGRO"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Resultado;
        }

        public static bool Update_MetodoPagoUsuario(string psUsuario, string psTimbreFiscal, string psArchivo, string psticket)
        {
            bool bandera = false;
            string query = "UPDATE crip_comision_comprobacion SET METODO_PAGO_USUARIO='28'";
            if (psticket != "")
            {
                query += ", TICKET='" + psticket + "' ";
            }
            query += " WHERE CLV_DOC='" + psTimbreFiscal + "' AND CLAVE_OFICIO='" + psArchivo + "' AND COMISIONADO='" + psUsuario + "' AND ESTATUS='1' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() > 0)
            {
                bandera = true;
            }
            else
            {
                bandera = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return bandera;

        }

        public static List<GridView> ListaComprobantes(string psOficio, string psComisionado, string psArchivo, string psDep)
        {
            string Query = "";
            Query += " SELECT DESCRIPCION AS CONCEPTO,COMPROBANTE AS PDF, XML AS XML, TICKET AS TICKET,  IMPORTE AS IMPORTE, CLV_DOC AS FOLIO_FISCAL";
            Query += "    FROM crip_comision_comprobacion ";
            Query += "    WHERE NO_OFICIO = '" + psOficio + "'";
            Query += "    AND COMISIONADO = '" + psComisionado + "'";
            Query += "    AND CLAVE_OFICIO = '" + psArchivo + "'";
            //Query += "    AND DEP_COMSIONADO = '" + psDep + "'";
            Query += "    AND ESTATUS != 0";
            Query += "    ORDER BY SEC_EFF  ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<GridView> Listacomprobacion = new List<GridView>();

            while (Reader.Read())
            {
                GridView obj = new GridView();
                obj.Adscripcion = Convert.ToString(Reader["CONCEPTO"]);
                obj.Comisionado = Convert.ToString(Reader["PDF"]);
                obj.Lugar = Convert.ToString(Reader["XML"]);
                obj.Rol = Convert.ToString(Reader["TICKET"]);
                obj.RFC = Convert.ToString(Reader["IMPORTE"]);
                obj.Ubicacion = Convert.ToString(Reader["FOLIO_FISCAL"]);

                Listacomprobacion.Add(obj);
                obj = null;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Listacomprobacion;
        }

        public static Entidad Obtiene_Comprobaciones(string psPeriodo, string psUbicacion, string psOficio, string psComisionado, string psConcepto, bool bandera = false)
        {
            string query = "";
            query += "select sum(IMPORTE) AS CODIGO ,max(FECHA_COMPROBACION) AS DESCRIPCION ";
            query += "   from crip_comision_comprobacion ";
            query += "  where 1=1 ";
            query += " AND ANIO = '" + psPeriodo + "'";
            query += "     and DEP_COMSIONADO = '" + psUbicacion + "'";
            query += " AND NO_OFICIO = '" + psOficio + "'";
            query += "    AND COMISIONADO = '" + psComisionado + "'";
            query += " and CONCEPTO in (" + psConcepto + ")";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objComp = new Entidad();

            if (Reader.Read())
            {
                if ((Convert.ToString(Reader["CODIGO"]) == "") | (Convert.ToString(Reader["CODIGO"]) == null))
                {
                    objComp.Codigo = Dictionary.NUMERO_CERO;
                    objComp.Descripcion = Dictionary.FECHA_NULA;
                }
                else
                {
                    objComp.Codigo = Convert.ToString(Reader["CODIGO"]);//clsFunciones.FormatFecha ( Convert.ToString(Reader["FECHA"]));
                    objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                }
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return objComp;
        }

        public static string Total_No_Fiscal(string psUsuario, string psDep, string psComprobante, string psPeriodo)
        {
            string Resultado = "";
            string Query = "SELECT DISTINCT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE COMISIONADO ='" + psUsuario + "' AND DEP_COMSIONADO = '" + psDep + "'";
            Query += " AND CONCEPTO IN (" + psComprobante + ")";
            Query += " AND ESTATUS != '0'";
            Query += " AND ANIO = " + psPeriodo + "";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }


        public static string Total(string psUsuario, string psDep, string psArchivo, string psComprobante)
        {
            string Resultado = "";
            string Query = "SELECT DISTINCT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE COMISIONADO ='" + psUsuario + "' AND DEP_COMSIONADO = '" + psDep + "'";
            Query += " AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND CONCEPTO IN (" + psComprobante + ")";
            Query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }

        public static string Exist_UUUID_Ministracion(string psUUID)
        {
            string resultado = "";
            string Query = " SELECT DOCUMENTO AS DOCUMENTO ";
            Query += "FROM crip_ministracion";
            Query += " WHERE DOCUMENTO = '" + psUUID + "'";
            Query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DOCUMENTO"]);
            }


            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;
        }

        public static string Exist_UUUID(string psUUID)
        {
            string resultado = "";
            string Query = "   SELECT CLV_DOC FROM crip_comision_comprobacion ";
            Query += " WHERE CLV_DOC= '" + psUUID + "' AND ESTATUS IN ('1','5','7')";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["CLV_DOC"]);
            }


            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;
        }

        public static string Sum_Totales(string psConcepto, string psPeriodo, string psDep)
        {
            string resultado = Dictionary.NUMERO_CERO;
            string Query = "";
            Query += "SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " from crip_comision_comprobacion ";
            Query += " where 1=1 ";
            Query += " AND CONCEPTO in ( " + psConcepto + ") ";
            Query += " AND ANIO = '" + psPeriodo + "' ";
            Query += " AND CLV_DEP_PROY = '" + psDep + "'";
            Query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if (resultado == "")
            {
                resultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;

        }

        public static string Sum_TotalesPory(string psConcepto, string psPeriodo, string psProy, string psDep)
        {
            string resultado = Dictionary.NUMERO_CERO;
            string Query = "";
            Query += "SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " from crip_comision_comprobacion ";
            Query += " where 1=1 ";
            Query += " AND CONCEPTO in ( " + psConcepto + ") ";
            Query += " AND ANIO = '" + psPeriodo + "' ";
            Query += " AND CLV_PROY = '" + psProy + "'";
            Query += " AND CLV_DEP_PROY = '" + psDep + "'";
            Query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if (resultado == "")
            {
                resultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;

        }

        //METODO PARA AUDITORÍA Y REPORTES DE VIATICOS
        public static string Sum_Importe1(string psUsuario, string psUbicacion, string psConcepto, string psOficio, string psArchivo)
        {
            string resultado = Dictionary.NUMERO_CERO;

            string Query = "SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " where comisionado = '" + psUsuario + "' ";

            if (psConcepto == "0")
            {
                Query += "AND CONCEPTO IN ('5','9','11','14','15','18','20')";
            }
            else
            {
                Query += "    AND CONCEPTO  IN ( " + psConcepto + ") ";
            }

            Query += "    AND NO_OFICIO = '" + psOficio + "'";
            Query += "    AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND ESTATUS != '0'";
            Query += " and DEP_COMSIONADO = '" + psUbicacion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if (resultado == "")
            {
                resultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        //TERMINA METODO PARA AUDITORÍA Y REPORTES DE VIATICOS



        public static string Sum_Importe(string psUsuario, string psUbicacion, string psConcepto, string psOficio, string psArchivo)
        {
            string resultado = Dictionary.NUMERO_CERO;

            string Query = "SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " where comisionado = '" + psUsuario + "' ";

            if (psConcepto == "0")
            {
                Query += "AND CONCEPTO IN ('5','9','11','14','15')";
            }
            else
            {
                Query += "    AND CONCEPTO = '" + psConcepto + "' ";
            }

            Query += "    AND NO_OFICIO = '" + psOficio + "'";
            Query += "    AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND ESTATUS != '0'";
            Query += " and DEP_COMSIONADO = '" + psUbicacion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if (resultado == "")
            {
                resultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        public static string Fecha_Max_Factura(string psUsuario, string psUbicacion, string psConcepto, string psOficio, string psArchivo, string psOpcion)
        {
            string resultado = "";

            string Query = " SELECT MAX(FECHA_FACTURA) AS FECHA ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " where comisionado = '" + psUsuario + "'";
            Query += " AND CONCEPTO = '" + psConcepto + "'";
            Query += " AND NO_OFICIO = '" + psOficio + "'";
            Query += " AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND ESTATUS != '0'";
            Query += " and DEP_COMSIONADO = '" + psUbicacion + "'";
            Query += " AND TIPO_COMPROBANTE = '" + psOpcion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["FECHA"]);
            }

            if (resultado == "")
            {
                resultado = "1900-01-01";
            }

            resultado = clsFunciones.FormatFecha(resultado);
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        public static string Totales(string psUbicacion, string psUsuario, string psfolio, string psOpcion, string psConcepto = "")
        {
            string resultado = "";
            string Query = "   SELECT SUM(IMPORTE) AS IMPORTE FROM crip_comision_comprobacion ";
            Query += "  WHERE DEP_COMSIONADO = '" + psUbicacion + "'";
            Query += " AND COMISIONADO = '" + psUsuario + "'";
            Query += " AND CLAVE_OFICIO = '" + psfolio + "'";
            Query += " AND ESTATUS !='0'";

            //if (psOpcion != "0")
            //{
            //    Query += " AND TIPO_COMPROBANTE = '" + psOpcion + "'";
            //}


            if (psConcepto != "")
            {
                if ((psConcepto == "5") | (psConcepto == "9") | (psConcepto == "11") | (psConcepto == "14") | (psConcepto == "15") || (psConcepto == "18") || (psConcepto == "20"))
                {
                    Query += "AND CONCEPTO IN ('5','9','11','14','15','18','20')";
                }
                else
                {
                    Query += " AND CONCEPTO  ='" + psConcepto + "'";
                }


            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if (resultado == "")
            {
                resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;
        }

        public static DataSet ListaComprobacion(string psUbicacion, string psUsuario, string psfolio, string psOpcion)
        {
            datasetArbol = new DataSet();

            string Query = "";
            Query += " SELECT DISTINCT FECHA_FACTURA AS FECHA ,DESCRIPCION  AS CONCEPTO ,IMPORTE AS IMPORTE , OBSERVACIONES AS OBSERVACIONES  ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE DEP_COMSIONADO = '" + psUbicacion + "'";
            Query += " AND COMISIONADO = '" + psUsuario + "'";
            Query += " AND CLAVE_OFICIO = '" + psfolio + "'";
            //Query += " AND NO_OFICIO = '" + psfolio + "'";
            /* Query += " AND ANIO = '"+ Year  +"'";*/

            if (psOpcion == "2")
            {
                Query += " AND TIPO_COMPROBANTE = '2'";
            }
            else if (psOpcion == "3")
            {
                Query += " AND TIPO_COMPROBANTE = '3'";
            }
            else if (psOpcion == Dictionary.NUMERO_CERO)
            {
                ;
            }
            Query += "AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            /*   cmd.Connection.Open();

             MySqlDataReader Reader = cmd.ExecuteReader();

             List<comprobacion> Listacomprobacion = new List<comprobacion >();
          //   Listacomprobacion.Clear();
             int contador = 0;
          
             while (Reader.Read())
             {
                 comprobacion objComp = new comprobacion();
                     objComp.Id =  Convert.ToString ( contador + 1);
                     objComp.Fecha = Convert.ToString(Reader["FECHA"]) ;//clsFunciones.FormatFecha ( Convert.ToString(Reader["FECHA"]));
                     objComp.Concepto = Convert.ToString(Reader["CONCEPTO"]);
                     objComp.Importe = Convert.ToString(Reader["IMPORTE"]);//" $ " + clsFunciones.Convert_Decimales( Convert.ToString(Reader["IMPORTE"]));
                     objComp.Observaciones = Convert.ToString(Reader["OBSERVACIONES"]);

                     Listacomprobacion.Add(objComp);
                    
                    contador++;
             }

             MngConexion.disposeConexion();

             return Listacomprobacion;*/
            MySqlDataAdapter adapter = new MySqlDataAdapter(Query, MngConexion.getConexionMysql());
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return datasetArbol;
        }

        public static List<Entidad> ObtieneListaImportes(string PSUsuario, string psConcepto, string psOficio, string psArchivo, string psUbicacion, string psTipo)
        {
            string Query = "";
            Query += "SELECT  FECHA_FACTURA AS CODIGO ,  CLV_DOC AS DESCRIPCION";
            Query += " FROM crip_comision_comprobacion ";
            Query += " where comisionado = '" + PSUsuario + "'";
            Query += " AND CONCEPTO = '" + psConcepto + "'";
            Query += " AND NO_OFICIO = '" + psOficio + "'";
            Query += "    AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND ESTATUS != '0'";
            Query += " and DEP_COMSIONADO = '" + psUbicacion + "'";
            Query += " AND TIPO_COMPROBANTE  = '" + psTipo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> Listacomprobacion = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objComp = new Entidad();

                objComp.Codigo = Convert.ToString(Reader["CODIGO"]);//clsFunciones.FormatFecha ( Convert.ToString(Reader["FECHA"]));
                objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                Listacomprobacion.Add(objComp);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Listacomprobacion;
        }

        public static List<Entidad> Obtiene_No_Facturables(string psComisionado, string psOficio, string psTipo, bool pBandera = false)
        {
            string Query = "SELECT FECHA_FACTURA ,IMPORTE, CONCAT( DESCRIPCION ,'|',OBSERVACIONES)  AS DESCRIPCION FROM crip_comision_comprobacion ";
            Query += "  where comisionado = '" + psComisionado + "' ";
            Query += " AND CLAVE_OFICIO = '" + psOficio + "' ";
            Query += " AND ESTATUS != '0'";
            Query += " AND TIPO_COMPROBANTE  = '" + psTipo + "'";

            if (pBandera)
            {
                Query += "AND CONCEPTO = '13'";
            }
            else
            {
                Query += "AND CONCEPTO != '13'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> Listacomprobacion = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objComp = new Entidad();

                objComp.Codigo = Convert.ToString(Reader["FECHA_FACTURA"]) + "|" + Convert.ToString(Reader["IMPORTE"]);
                objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                Listacomprobacion.Add(objComp);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Listacomprobacion;

        }

        public static List<Entidad> Obtiene_Lista_Conceptos()
        {
            string Query = "";
            Query += " SELECT CLV_TIPO AS CODIGO , DESCRIPCION AS DESCRIPCION ";
            Query += " FROM crip_tipocomprobaciones ";
            Query += " WHERE 1 = 1 ";
            Query += " AND CLV_TIPO NOT IN ('0','1','2','3','4','10','12','13','19','18','16')";
            Query += "     AND ESTATUS = '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> Listacomprobacion = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objComp = new Entidad();

                objComp.Codigo = Convert.ToString(Reader["CODIGO"]);//clsFunciones.FormatFecha ( Convert.ToString(Reader["FECHA"]));
                objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                Listacomprobacion.Add(objComp);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Listacomprobacion;
        }

        public static Boolean Update_Comprobacion(string psUbicacion, string psUsuario, string psfolio, string psArchivo, string psEstatus, string psOpcion = "")
        {
            bool bandera = false;
            string Query = "UPDATE crip_comision_comprobacion SET ESTATUS = '" + psEstatus + "'";

            if (psOpcion != "")
            {
                if (psOpcion == Dictionary.VOBO)
                {
                    Query += ", FECHA_VOBO = '" + lsHoy + "'";
                }
                else
                {
                    Query += ", FECHA_AUTORIZA = '" + lsHoy + "'";
                }
            }
            Query += " WHERE DEP_COMSIONADO = '" + psUbicacion + "' AND COMISIONADO  ='" + psUsuario + "'";
            Query += " AND NO_OFICIO = '" + psfolio + "'";
            Query += " AND CLAVE_OFICIO =  '" + psArchivo + "'";
            Query += " AND ESTATUS  != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() > 0)
            {
                bandera = true;
            }
            else
            {
                bandera = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return bandera;

        }


        //*********************  PATRICIA QUINO MORENO  *************************////
        public static Entidad Obtiene_TotalPagado(string psPeriodo, string psclaveproy, string psUbicacionProy, string psEstatus)
        {
            string query = "";
            query += " SELECT SUM(TOTAL_PAGADO) AS CODIGO ";
            query += "   FROM crip_ministracion ";
            query += "  WHERE PERIODO= '" + psPeriodo + "'";
            query += " AND CLV_PROY= '" + psclaveproy + "'";
            query += " AND UBICACION_PROY= '" + psUbicacionProy + "'";
            query += " AND ESTATUS= '" + psEstatus + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objComp = new Entidad();

            if (Reader.Read())
            {
                if ((Convert.ToString(Reader["CODIGO"]) == "") | (Convert.ToString(Reader["CODIGO"]) == null))
                {
                    objComp.Codigo = Dictionary.NUMERO_CERO;
                }
                else
                {
                    objComp.Codigo = Convert.ToString(Reader["CODIGO"]);

                }
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return objComp;
        }

        public static string Total_Comprobado(string psPeriodo, string psClvProyecto, string psTipo)
        {
            string Resultado = "";
            string Query = "  ";
            Query += " SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE ANIO= '" + psPeriodo + "'";
            Query += "AND CLV_PROY= '" + psClvProyecto + "'";

            if (psTipo == "VIATICOS")
            {
                Query += "AND CONCEPTO IN ('5', '9', '11', '14', '15', '17', '20')";

            }

            if (psTipo == "COMBUSTIBLE")
            {
                Query += "AND CONCEPTO = '6'";

            }

            if (psTipo == "PEAJE")
            {
                Query += "AND CONCEPTO = '7'";
            }

            if (psTipo == "PASAJE")
            {
                Query += "AND CONCEPTO = '8'";

            }

            if (psTipo == "SINGLADURAS")
            {
                Query += "AND CONCEPTO = '19'";

            }


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }
        //************************PATRICIA QUINO MORENO*******************************//
        //********* PATRICIA QUINO MORENO 28-06-17******//
        public static List<Comisio_Comprobacion> Regresa_ListComprobacion(string psUbicacion, string psUsuario, string psArchivo, string psOpcion, bool pBandera = false)
        {

            string Query = "";
            Query += " SELECT DISTINCT FECHA_FACTURA AS FECHA ,DESCRIPCION  AS CONCEPTO ,IMPORTE AS IMPORTE , OBSERVACIONES AS OBSERVACIONES, METODO_PAGO_USUARIO AS METODOPAGO,CONCEPTO AS NUM_CONCEPTO  ";
            Query += " FROM crip_comision_comprobacion ";
            //Query += " WHERE DEP_COMSIONADO = '" + psUbicacion + "'";
            Query += " WHERE COMISIONADO = '" + psUsuario + "'";
            Query += " AND CLAVE_OFICIO = '" + psArchivo + "'";
            //Query += " AND NO_OFICIO = '" + psfolio + "'";
            /* Query += " AND ANIO = '"+ Year  +"'";*/

            if (psOpcion == "2")
            {
                Query += " AND TIPO_COMPROBANTE = '2' ";
            }
            else if (psOpcion == "3")
            {
                Query += " AND TIPO_COMPROBANTE = '3' ";
            }
            else if (psOpcion == Dictionary.NUMERO_CERO)
            {
                ;
            }
            if (pBandera)
            {
                Query += "AND CONCEPTO != '13' ";
            }
            Query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();


            List<Comisio_Comprobacion> Listacomprobacion = new List<Comisio_Comprobacion>();

            int contador = 0;

            while (Reader.Read())
            {
                Comisio_Comprobacion objComp = new Comisio_Comprobacion();

                DateTime fecha = Convert.ToDateTime(Reader["FECHA"]);
                objComp.FECHA_COMPROBACION = fecha.ToString("yyyy-MM-dd");
                objComp.CONCEPTO_COMP = Convert.ToString(Reader["CONCEPTO"]);
                objComp.IMPORTE = "$" + Convert.ToString(Reader["IMPORTE"]);
                objComp.OBSERVACIONES = Convert.ToString(Reader["OBSERVACIONES"]);
                objComp.METODO_PAGO_USUARIO = Convert.ToString(Reader["METODOPAGO"]);
                objComp.NUM_CONCEP = Convert.ToString(Reader["NUM_CONCEPTO"]);


                Listacomprobacion.Add(objComp);

                contador++;
            }

            //MngConexion.disposeConexion();

            //return Listacomprobacion;
            //MySqlDataAdapter adapter = new MySqlDataAdapter(Query, MngConexion.getConexionMysql());
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Listacomprobacion;
        }
        //********* PATRICIA QUINO MORENO 28-06-17 TERMINA ******//
        //********* PATRICIA QUINO MORENO 12/07/2017  ******//
        public static string Total_AnualNoFiscal(string pComisionado)
        {
            string Resultado = "";
            string Query = "  ";
            Query += " SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE ANIO = '" + Year + "'";
            Query += "AND COMISIONADO= '" + pComisionado + "'";
            Query += "AND CONCEPTO = '12'";
            Query += "AND ESTATUS != '0'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if (Resultado == "")
            {
                Resultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Resultado;
        }

        public static string Metodo_PagoComprobacion(string ID)
        {
            string Resultado = "";
            string Query = "  ";
            Query += "SELECT DISTINCT CONCAT( ID,' - ', DESCRIPCION) AS METODO_PAGO  ";
            Query += " FROM crip_metodo_pago ";
            Query += " WHERE ESTATUS != '0'";
            Query += " AND PERIODO = '" + Year + "'";
            Query += " AND ID='" + ID + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["METODO_PAGO"]);
            }

            if (Resultado == "")
            {
                Resultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Resultado;
        }

        //********* PATRICIA QUINO MORENO 12/07/2017  ******//

        //MODIFICACION PEDRO 01-09-2017
        public static string Obtiene_Fecha_Comprobacion(string psFolio, string psArchivo)
        {
            string resultado = "";

            string Query = "";
            Query += " SELECT FECHA AS FECHA_COMPROBACION FROM CRIP_COMISION_COMPROBANTE ";
            Query += " WHERE FOLIO = '" + psFolio + "' AND ARCHIVO = '" + psArchivo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                if (Reader["FECHA_COMPROBACION"] != null)
                {
                    DateTime dtFecha = Convert.ToDateTime(Reader["FECHA_COMPROBACION"]);
                    resultado = dtFecha.ToString("dd-MM-yyyy");
                }
            }

            else
            {
                DateTime dtFecha = Convert.ToDateTime(lsHoy);
                resultado = dtFecha.ToString("dd-MM-yyyy");
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        public static Entidad Obtiene_FechaMax_Importe(string psArchivo, string psComisionado, string psConcepto)
        {
            string Query = "SELECT MAX(FECHA_FACTURA) as FECHAMAX, SUM(IMPORTE) AS SUMIMPORT FROM crip_comision_comprobacion";
            Query += " WHERE CLAVE_OFICIO= '" + psArchivo + "' ";
            Query += " AND CONCEPTO = '" + psConcepto + "' ";
            Query += " AND COMISIONADO='" + psComisionado + "' ";
            Query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            Entidad objComp = new Entidad();

            if (Reader.Read())
            {


                if (Convert.ToString(Reader["FECHAMAX"]) != "")
                {
                    DateTime fecha = Convert.ToDateTime(Convert.ToString(Reader["FECHAMAX"]));
                    objComp.Codigo = fecha.ToString("dd-MM-yyyy");
                }
                else
                {
                    objComp.Codigo = "01-01-1990";
                }

                objComp.Descripcion = Convert.ToString(Reader["SUMIMPORT"]);// es = null entonces =0
                if (objComp.Descripcion == "")
                {
                    objComp.Descripcion = Dictionary.NUMERO_CERO;
                }

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return objComp;

        }
    }
}
