/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.DAL
	FileName:	MngDatosComision.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Diciembre 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/
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
    public class MngDatosComision
    {
        static string max;
        static int liNumero;
        // static string Dictionary.FECHA_NULA = "1900-01-01";
        static readonly clsDictionary dictionary = new clsDictionary();
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        /******************PATRICIA QUINO MORENO****************************/
        public static ComisionProyecto RegresaDatos(string pPeriodo, string pClave, string pDependencia)
        {
            string Query = "";
            Query += "SELECT SUM(COMBUST_EFECTIVO) AS COMBUSTIBLE, SUM(PEAJE) AS PEAJE ,SUM(PASAJE) AS PASAJE, SUM(SINGLADURAS) AS SINGLADURAS, SUM(TOTAL_VIATICOS) AS TOTAL_VIATICOS,  ";
            Query += " SUM(COMBUST_EFECTIVO) + SUM(PEAJE) + SUM(PASAJE)+SUM(SINGLADURAS) + SUM(TOTAL_VIATICOS) AS GRANTOTAL ";
            Query += " FROM  crip_comision ";
            Query += " WHERE PERIODO='" + pPeriodo + "' AND CLV_PROY='" + pClave + "' AND CLV_DEP_PROY='" + pDependencia + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            ComisionProyecto oComision = new ComisionProyecto();
            while (Reader.Read())
            {

                oComision.Combustible = Convert.ToString(Reader["COMBUSTIBLE"]);
                oComision.Peaje = Convert.ToString(Reader["PEAJE"]);
                oComision.Pasaje = Convert.ToString(Reader["PASAJE"]);
                oComision.Singladuras = Convert.ToString(Reader["SINGLADURAS"]);
                oComision.Totalviaticos = Convert.ToString(Reader["TOTAL_VIATICOS"]);
                oComision.GranTotal = Convert.ToString(Reader["GRANTOTAL"]);


                if (oComision.Combustible == "")
                {
                    oComision.Combustible = "0";
                }

                if (oComision.Peaje == "")
                {
                    oComision.Peaje = "0";
                }

                if (oComision.Pasaje == "")
                {
                    oComision.Pasaje = "";
                }

                if (oComision.Singladuras == "")
                {
                    oComision.Singladuras = "";
                }

                if (oComision.Totalviaticos == "")
                {
                    oComision.Totalviaticos = "";
                }

                if (oComision.GranTotal == "")
                {
                    oComision.GranTotal = "";
                }

            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return oComision;

        }

        /*******************PATRICIA QUINO MORENO***********************/


        public static List<Comision> Lista_ComisionesTransparencia(string psYear)
        {
            string Query = "";
            /*  Query += "select `a`.`CLV_TIPO_COM` AS `TIPO_COMISION`, ";
              Query += "    `a`.`NO_OFICIO` AS `OFICIO`,";
              Query += " `a`.`ARCHIVO` AS `ARCHIVO`,";
              Query += "    `a`.`USUARIO` AS `COMISIONADO`,";
              Query += "  `a`.`CLV_DEP_COM` AS `UBICACION_COMISIONADO`,";
              Query += " `a`.`ESPECIE` AS `ESPECIE`,";
              Query += " `a`.`PRODUCTO` AS `PRODUCTO`,";
              Query += "    `a`.`ACTIVIDAD_ESPECIFICA` AS `ACTIVIDAD`,";
              Query += " `a`.`LUGAR` AS `LUGAR`,";
              Query += "  `a`.`DIAS_REALES` AS `DIAS`,";
              Query += "    `a`.`FECHA_I` AS `INICIO`,";
              Query += " `a`.`FECHA_F` AS `FINAL`,";
              Query += " `a`.`OBJETIVO` AS `OBJETIVO`,";
              Query += "     `a`.`CLV_CLASE_AUT` AS `CLASE`,";
              Query += " `a`.`CLV_TIPO_AUT` AS `TIPO`,";
              Query += " `a`.`CLV_TRANS_AUT` AS `TRANSPORTE`,";
              Query += " `a`.`CLV_DEP_TRANS_AUT` AS `UBICACION_TRANSPORTE`,";
              Query += " `a`.`ORIGEN_DESTINO_TER` AS `RECORRIDO_TER`,";
              Query += " `a`.`ORIGEN_DESTINO_AER` AS `RECORRIDO_AER`,";
              Query += " `a`.`ORIGEN_DESTINO_ACU` AS `RECORRIDO_ACU`,";
              Query += " `a`.`PASAJE` AS `PASAJE`,";
              Query += " `a`.`PART_PASAJE` AS `PART_PASAJE`,";
              Query += " `a`.`PEAJE` AS `peaje`,";
              Query += " `a`.`PART_PEAJE` AS `PART_PEAJE`,";
              Query += " `a`.`COMBUST_EFECTIVO` AS `COMBUSTIBLE`,";
              Query += " `a`.`PARTIDA_COMBUSTIBLE` AS `PARTIDA_COMBUSTIBLE`,";
              Query += " `a`.`TOTAL_VIATICOS` AS `TOTAL_VIATICOS`,";
              Query += " `a`.`PART_PRESUPUESTAL` AS `PART_PRESUPUESTAL`,";
              Query += " `a`.`SINGLADURAS` AS `SINGLADURAS`,";
              Query += " `a`.`ZONA_COMERCIAL` AS `TARIFA`,";
              Query += " `a`.`FORMA_PAGO_VIATICOS` AS `FORMA_PAGO_VIATICOS`,";
              Query += " `a`.`TIPO_VIATICOS` AS `TIPO_VIATICOS`,";
              Query += " `a`.`TIPO_PAGO_VIATICO` AS `TIPO_PAGO_VIATICO`,";
              Query += " `a`.`PERIODO` AS `PERIODO`,";
              Query += " `a`.`TERRITORIO` AS `TERRITORIO`,";
              Query += " `a`.`ESTATUS` AS `ESTATUS` ";
              Query += " from `crip_comision` `a` ";
              Query += " where (";
              Query += " (1 = 1) ";
              Query += " and (`a`.`ESTATUS` in('7','5'))";
              Query += " AND (A.PERIODO ='" + psYear + "')";
              Query += " ) ";
              Query += " order by `a`.`CLV_DEP_COM`";
  */

            Query += "SELECT ARCHIVO, ";
            Query += " PERIODO, ";
            Query += " 'Empleado', ";
            Query += " CLV_DEP_COM,";
            Query += " USUARIO, ";
            Query += " OBJETIVO,";
            Query += " TERRITORIO, ";
            Query += " '0', ";
            Query += " '0', ";
            Query += " 'México', ";
            Query += " LUGAR, ";
            Query += " FECHA_I, ";
            Query += " FECHA_F ";
            Query += " FROM crip_comision ";
            Query += " WHERE 1=1 ";
            Query += " AND PERIODO= '2017' ";
            Query += " AND ESTATUS IN ('5','7') ";
            Query += " AND ( COMBUST_EFECTIVO + PEAJE + PASAJE+ TOTAL_VIATICOS + SINGLADURAS) > 0 ";
            Query += " AND ";
            Query += " ( ";
            Query += "	NO_OFICIO_AMPLIACION = 0 ";
            Query += " OR NO_OFICIO_AMPLIACION = NULL ";
            Query += "  )";



            List<Comision> ListaComisiones = new List<Comision>();
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Comision oComision = new Comision();
                oComision.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);
                oComision.Oficio = Convert.ToString(reader["OFICIO"]);
                oComision.Archivo = Convert.ToString(reader["ARCHIVO"]);
                oComision.Comisionado = Convert.ToString(reader["COMISIONADO"]);
                oComision.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                oComision.Especie = Convert.ToString(reader["ESPECIE"]);
                oComision.Producto = Convert.ToString(reader["PRODUCTO"]);
                oComision.Actividad = Convert.ToString(reader["ACTIVIDAD"]);
                oComision.Lugar = Convert.ToString(reader["LUGAR"]);
                oComision.Dias_Reales = Convert.ToString(reader["DIAS"]);
                oComision.Fecha_Inicio = Convert.ToString(reader["INICIO"]);
                oComision.Fecha_Final = Convert.ToString(reader["FINAL"]);
                oComision.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                oComision.Clase_Aut = Convert.ToString(reader["CLASE"]);
                oComision.Tipo_Aut = Convert.ToString(reader["TIPO"]);
                oComision.Vehiculo_Autorizado = Convert.ToString(reader["TRANSPORTE"]);
                oComision.Ubicacion_Trans_Aut = Convert.ToString(reader["UBICACION_TRANSPORTE"]);
                oComision.Origen_Terrestre = Convert.ToString(reader["RECORRIDO_TER"]);
                ListaComisiones.Add(oComision);
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListaComisiones;
        }

        public static string Importe_Debito(string psUsuario, string psPeriodo, string psArchivo)
        {
            string Query = "", resultado = "";
            Query += " SELECT SUM(IMPORTE) AS IMPORTE ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE 1=1 ";
            Query += " AND COMISIONADO = '" + psUsuario + "'";
            Query += " AND ANIO= '" + psPeriodo + "' ";
            Query += " AND CLAVE_OFICIO = '" + psArchivo + "'";
            Query += " AND CONCEPTO IN('5','9','11','14','15','17','20')";
            Query += " AND METODO_PAGO_USUARIO = '28'";
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
                resultado = dictionary.NUMERO_CERO;
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;
        }

        public static TipoCambio TipoCambio(string psUsuario, string psFecha)
        {
            string query = "";
            query += " SELECT *  ";
            query += "  FROM crip_tipo_cambio ";
            query += " WHERE USUARIO = '" + psUsuario + "'";
            query += " AND FECHA_CAMBIO = '" + psFecha + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            TipoCambio tc = new TipoCambio();
            while (Reader.Read())
            {
                //  objEntidad.Codigo = Convert.ToString(Reader["VoBo"]);
                tc.Denominacion = Convert.ToString(Reader["DENOMINACION"]);
                tc.Tipo_Cambio = Convert.ToString(Reader["TIPO_CAMBIO"]);
                tc.Tarifa = Convert.ToString(Reader["TARIFA_AUT"]);
                tc.Fecha = Convert.ToString(Reader["FECHA_CAMBIO"]);
                tc.Usuario = Convert.ToString(Reader["USUARIO"]);
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return tc;

        }

        public static List<Comision> Comisiones_Efectivas(string psUbicacion, string psAnio)
        {
            string query = "";
            query = "SELECT NO_OFICIO as FOLIO, USUARIO as COMISIONADO,USUARIO AS USUARIO_SOLICITA ,ESTATUS AS ESTATUS ";
            query += " FROM crip_comision ";
            query += " WHERE CLV_DEP_COM = '" + psUbicacion + "'";
            query += " AND ESTATUS != 1";
            query += " AND NO_OFICIO != 0 ";
            query += " AND PERIODO = '" + psAnio + "' ";
            query += " ORDER BY NO_OFICIO ASC ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);

            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Comision> listGrid = new List<Comision>();
            string status;
            while (reader.Read())
            {
                Comision objetoEntidad = new Comision();
                objetoEntidad.Oficio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Usuario_Solicita =  Convert.ToString(reader["USUARIO_SOLICITA"]);

                objetoEntidad.Comisionado = MngDatosUsuarios.Obtiene_Nombre_Completo(Convert.ToString(reader["COMISIONADO"]));
                status = Convert.ToString(reader["ESTATUS"]);
                if (status == "0")
                {
                    objetoEntidad.Estatus = "Cancelada";
                }
                else if (status == "9")
                {
                    objetoEntidad.Estatus = "Ministrada";
                }
                else if (status == "7")
                {
                    objetoEntidad.Estatus = "Comprobada sin validar";
                }
                else if (status == "5")
                {
                    objetoEntidad.Estatus = "Comprobada Validada";
                }

                listGrid.Add(objetoEntidad);
            }

            reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return listGrid;

        }

        /// <summary>
        /// Metodo que devuelve lista de tipos de comision 
        /// </summary>
        /// <returns></returns>
        public static List<Entidad> Obtiene_Tipo_Comision()
        {
            string query = "";

            query = "SELECT CLV_TIPO_COMISION AS CODIGO , DESCRIPCION AS DESCRIPCION FROM crip_comision_tipo ";
            query += " WHERE ESTATUS = '1' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);

            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Entidad> ListGrid = new List<Entidad>();
            while (reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(reader["DESCRIPCION"]);
                ListGrid.Add(objetoEntidad);
            }
            reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListGrid;

        }

        /// <summary>
        /// Metodo que obtiene detalle de comision dependiendo usuario y ubicacion
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <param name="psUbiaccion"></param>
        /// <param name="psFolio"></param>
        /// <returns></returns>
        public static comision_informe Obtiene_Informe(string psUsuario, string psUbiaccion, string psFolio, string psDepSolicitud, string psPeriodo, string psProy = "")
        {

            string query = "SELECT DISTINCT NO_OFICIO AS OFICIO , ";
            query += " CLV_DEP_COMISIONADO AS UBICACION_COMISIONADO, ";
            query += " COMISIONADO AS COMISIONADO, ";
            query += " CLV_PROY  AS PROYECTO, ";
            query += " CLV_DEP_PROY  AS UBICACION_PROYECTO, ";
            query += " ACTIVIDADES AS ACTIVIDADES, ";
            query += " EVIDENCIA1 AS EVIDENCIA_1, ";
            query += " EVIDENCIA2 AS EVIDENCIA_2, ";
            query += " EVIDENCIA3 AS EVIDENCIA_3, ";
            query += " FECHA_INFORME_I AS FECHA_INICIO, ";
            query += " FECHA_INFORME_F AS FECHA_FIN, ";
            query += " FECHA_AUTORIZACION AS FECHA_AUT, ";
            query += " SECUENCIA AS SECUENCIA, ";
            query += " DEP_SOLICITUD AS DEP_SOLICITUD";
            query += " FROM CRIP_COMISION_INFORME ";
            query += " WHERE NO_OFICIO = '" + psFolio + "' ";
            query += " AND CLV_DEP_COMISIONADO = '" + psUbiaccion + "' ";
            query += " AND COMISIONADO = '" + psUsuario + "' ";
            query += " AND ESTATUS = '1' ";
            query += " AND  CLV_DEP_PROY =  '" + psDepSolicitud + "'";
            query += " AND FECHA_AUTORIZACION = '1900-01-01'";
            query += " AND PERIODO  = '" + psPeriodo + "'";

            if (psProy!="")
            {
                query += " AND CLV_PROY  = '" + psProy + "'";
            }

            //    query += "AND FECHA_INFORME_F != '1900-01-01'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            comision_informe obj = new comision_informe();
            while (Reader.Read())
            {
                //  objEntidad.Codigo = Convert.ToString(Reader["VoBo"]);
                obj.FOLIO = Convert.ToString(Reader["OFICIO"]);
                obj.UBICACION_COMISIONADO = Convert.ToString(Reader["UBICACION_COMISIONADO"]);
                obj.COMISIONADO = Convert.ToString(Reader["COMISIONADO"]);
                obj.PROYECTO = Convert.ToString(Reader["PROYECTO"]);
                obj.UBICACION_PROYECTO = Convert.ToString(Reader["UBICACION_PROYECTO"]);
                obj.ACTIVIDADES = Convert.ToString(Reader["ACTIVIDADES"]);
                obj.EVIDENCIA_1 = Convert.ToString(Reader["EVIDENCIA_1"]);
                obj.EVIDENCIA_2 = Convert.ToString(Reader["EVIDENCIA_2"]);
                obj.EVIDENCIA_3 = Convert.ToString(Reader["EVIDENCIA_3"]);
                obj.FECHA_INICIO = Convert.ToString(Reader["FECHA_INICIO"]);
                obj.FECHA_FINAL = Convert.ToString(Reader["FECHA_FIN"]);
                obj.FECHA_AUT = Convert.ToString(Reader["FECHA_AUT"]);
                obj.SECUENCIA = Convert.ToString(Reader["SECUENCIA"]);
                obj.UBICACION_SOLICITUD = Convert.ToString(Reader["DEP_SOLICITUD"]);

            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return obj;
        }
        
        public static comision_informe Obtiene_Oficio(string psUsuario, string psUbiaccion, string psFolio, string psDepSolicitud, string psPeriodo)
        {

            string query = "SELECT DISTINCT NO_OFICIO AS OFICIO , ";
            query += " CLV_DEP_COM AS UBICACION_COMISIONADO, ";
            query += " COMISIONADO AS COMISIONADO, ";
            query += " PROYECTO  AS PROYECTO, ";
            query += " DEP_PROYECTO  AS UBICACION_PROYECTO, ";
            query += " ARCHIVO_OFICIO_FIRMADO AS OFICIO_COMISION, ";
            query += " SECUENCIA AS SECUENCIA, ";
            query += " FROM CRIP_COMISION_DETALLE ";
            query += " WHERE NO_OFICIO = '" + psFolio + "' ";
            query += " AND CLV_DEP_COM = '" + psUbiaccion + "' ";
            query += " AND COMISIONADO = '" + psUsuario + "' ";
            query += " AND ESTATUS = '1' ";
            query += " AND  DEP_PROYECTO =  '" + psDepSolicitud + "'";
            query += " AND PERIODO  = '" + psPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            comision_informe obj = new comision_informe();
            while (Reader.Read())
            {
                //  objEntidad.Codigo = Convert.ToString(Reader["VoBo"]);
                obj.FOLIO = Convert.ToString(Reader["OFICIO"]);
                obj.UBICACION_COMISIONADO = Convert.ToString(Reader["UBICACION_COMISIONADO"]);
                obj.COMISIONADO = Convert.ToString(Reader["COMISIONADO"]);
                obj.PROYECTO = Convert.ToString(Reader["PROYECTO"]);
                obj.UBICACION_PROYECTO = Convert.ToString(Reader["UBICACION_PROYECTO"]);
                obj.ACTIVIDADES = Convert.ToString(Reader["OFICIO_COMISION"]);
                obj.SECUENCIA = Convert.ToString(Reader["SECUENCIA"]);
                obj.UBICACION_SOLICITUD = Convert.ToString(Reader["DEP_SOLICITUD"]);

            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return obj;
        }
        public static string Dias_Acumulados(string psComisionado, string psPeriodo)
        {
            string dias = "";
            /*string Query = " SELECT SUM(DIAS) AS DIAS FROM vw_comisiones ";
            Query += " WHERE COMISIONADO = '" + psComisionado + "'";
            Query += "AND ESTATUS != '0'";
            Query += "and PERIODO = '" + psPeriodo + "'";
            */

            string Query = "SELECT SUM(A.DIAS_COMERCIAL)+ SUM(A.DIAS_RURAL)+ SUM(A.DIAS_50KM) AS DIAS ";
            Query += " FROM crip_comision A ";
            Query += " WHERE A.USUARIO = '" + psComisionado + "' ";
            Query += " AND A.ESTATUS NOT IN ('0','1') ";
            Query += " AND A.ZONA_COMERCIAL NOT IN ('0','15','18')";
            Query += " AND A.PERIODO= '" + psPeriodo + "'";
            Query += " AND TERRITORIO!='3'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                dias = Convert.ToString(Reader["DIAS"]);
            }

            if (dias == "")
            {
                dias = dictionary.NUMERO_CERO;
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return dias;
        }
        public static string Obtiene_Lugar(string psFolio, string psUsuario, string psAccion)
        {
            string Lugar = "";
            string Query = "   SELECT CLV_DEP FROM crip_comision ";
            Query += " WHERE FOLIO = '" + psFolio + "' ";

            if (psAccion == "AUTORIZA")
            {
                Query += "  AND AUTORIZA = '" + psUsuario + "'";
                Query += " AND FECHA_AUTORIZA = '1900-01-01' ";
            }
            else if (psAccion == "VOBO")
            {
                Query += "  AND VoBo = '" + psUsuario + "'";
                Query += " AND FECHA_VoBo = '1900-01-01' ";
            }

            Query += "AND ESTATUS = '1'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Lugar = Convert.ToString(Reader["CLV_DEP"]);
            }
            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Lugar;
        }

        public static Entidad vobo_aut(string psfolio, string psVobo = "", string psAutoriza = "")
        {
            string Query = "SELECT VoBo , AUTORIZA FROM crip_comision ";
            Query += "WHERE FOLIO = '" + psfolio + "' ";

            if (psVobo != dictionary.CADENA_NULA)
            {
                Query += " AND VoBo = '" + psVobo + "'";
            }

            if (psAutoriza != dictionary.CADENA_NULA)
            {
                Query += " AND AUTORIZA = '" + psAutoriza + "'";
            }
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            Entidad objEntidad = new Entidad();
            while (Reader.Read())
            {
                objEntidad.Codigo = Convert.ToString(Reader["VoBo"]);
                objEntidad.Descripcion = Convert.ToString(Reader["AUTORIZA"]);

            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return objEntidad;
        }

        public static string Otiene_SecEff_Comision(string psFolio)
        {
            string Query = "SELECT  MAX(SECEFF)  AS MAX FROM crip_comision WHERE FOLIO = '" + psFolio + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);
            }
            //   MngConexion.disposeConexion();

            liNumero = Convert.ToInt32(max) + 1;

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Convert.ToString(liNumero);
        }

        public static Boolean Update_ruta_Comision(string psRuta, string psArchivo, string psDep, string psComisionado, string psOficio, Comision poComision)
        {
            //ACAMESMO
            bool bandera = false;
            string query = "UPDATE crip_comision SET RUTA = '" + psRuta + "' , ARCHIVO = '" + psArchivo + "'";
            query += " WHERE CLV_DEP_COM = '" + psDep + "' AND USUARIO = '" + psComisionado + "'";

            if ((poComision.Zona_Comercial == "0") & (((poComision.Combustible_Efectivo == dictionary.NUMERO_CERO) | (poComision.Combustible_Efectivo == null) | (poComision.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((poComision.Peaje == dictionary.NUMERO_CERO) | (poComision.Peaje == null) | (poComision.Peaje == dictionary.CADENA_NULA)) & ((poComision.Pasaje == dictionary.NUMERO_CERO) | (poComision.Pasaje == dictionary.CADENA_NULA) | (poComision.Pasaje == null))))
            {
                query += " and ESTATUS = '5'";
            }
            else if ((poComision.Zona_Comercial == "15") & (((poComision.Combustible_Efectivo == dictionary.NUMERO_CERO) | (poComision.Combustible_Efectivo == null) | (poComision.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((poComision.Peaje == dictionary.NUMERO_CERO) | (poComision.Peaje == null) | (poComision.Peaje == dictionary.CADENA_NULA)) & ((poComision.Pasaje == dictionary.NUMERO_CERO) | (poComision.Pasaje == dictionary.CADENA_NULA) | (poComision.Pasaje == null))))
            {
                query += " and ESTATUS = '5'";
            }
            else
            {
                query += " and ESTATUS = '9'";
            }

            query += " AND NO_OFICIO = '" + psOficio + "'";
            query += " AND PERIODO = '" + poComision.Periodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            //   MySqlDataReader Reader = cmd.ExecuteReader();

            if (cmd.ExecuteNonQuery() == 1) bandera = true;
            else bandera = false;

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return bandera;

        }

        public static Boolean Update_Oficio_Comision(Comision poComision, Entidad pObjEntidad)
        {
            bool bandera = false;
            string Query = "";
            string oficio = "";
            /************ Inicia conexion a bd del maestro sierra******************/
            //Obtenemos datos del usuario  comisionado
            string rol_comisionado = MngDatosUsuarios.Obtiene_Rol_Usuario(poComision.Comisionado);

            if (poComision.Territorio == "3")
            {
                oficio = Obtiene_Max_Comision("1000", poComision.Periodo, true);
            }
            else if ((poComision.Ubicacion_Autoriza == "5000") | (poComision.Ubicacion_Autoriza == "6000") | (poComision.Ubicacion_Autoriza == "3000") | (poComision.Ubicacion_Autoriza == "2000") | (poComision.Ubicacion_Autoriza == "4000"))
            {
                //  oficio = Obtiene_Max_Comision(poComision.Ubicacion_Autoriza, poComision.Periodo, true);
                //oficio = Obtiene_Max_Comision_Like(poComision.Periodo, MngDatosDependencia.Obtiene_Siglas(poComision.Ubicacion_Autoriza)); se comento el 27-08-2021 - Por el cambio de denominación de las unidades administrativas en el Estatuto del INAPESCA
                oficio = Obtiene_Max_Comision(poComision.Ubicacion_Autoriza, poComision.Periodo, true);//se agrego Por el cambio de denominación de las unidades administrativas en el Estatuto del INAPESCA

            }
            else if ((poComision.Ubicacion_Autoriza == "1000") | (poComision.Ubicacion_Autoriza == "1100") | (poComision.Ubicacion_Autoriza == "1200"))
            {
                //oficio = Obtiene_Max_Comision_Like(poComision.Periodo, MngDatosDependencia.Obtiene_Siglas(poComision.Ubicacion_Autoriza) + "-"); se comento el 27-08-2021 - Por el cambio de denominación de las unidades administrativas en el Estatuto del INAPESCA
                oficio = Obtiene_Max_Comision(poComision.Ubicacion_Autoriza, poComision.Periodo, true); //se agrego Por el cambio de denominación de las unidades administrativas en el Estatuto del INAPESCA
            }
            /*else if (poComision.Ubicacion_Autoriza == "4000")
            {
                oficio = MngDatosDgaipp.Obtiene_folio_Max(poComision.Periodo);
                //    bool x = MngDatosDgaipp.Inserta_Oficio_Comision(oficio, "OC", poComision.Objetivo, "43", poComision.Comisionado, poComision.Vobo);
            }*/
            else if (pObjEntidad.Descripcion == dictionary.SUBDIRECCIONES_GENERALES)
            {
                //oficio = Obtiene_Max_Comision(pObjEntidad.Codigo, true);
                oficio = Obtiene_Max_Comision_Like(poComision.Periodo, MngDatosDependencia.Obtiene_Siglas(pObjEntidad.Codigo));
            }
            else
            {
                oficio = Obtiene_Max_Comision(poComision.Ubicacion_Comisionado, poComision.Periodo, true);
            }
            Query = "   UPDATE crip_comision SET NO_OFICIO =  '" + oficio + "' ";
            Query += "where CLV_DEP_COM = '" + poComision.Ubicacion_Comisionado + "' AND USUARIO = '" + poComision.Comisionado + "'";
            Query += " and FOLIO = '" + poComision.Folio + "'";
            if ((poComision.Zona_Comercial == "0") & (((poComision.Combustible_Efectivo == dictionary.NUMERO_CERO) | (poComision.Combustible_Efectivo == null) | (poComision.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((poComision.Peaje == dictionary.NUMERO_CERO) | (poComision.Peaje == null) | (poComision.Peaje == dictionary.CADENA_NULA)) & ((poComision.Pasaje == dictionary.NUMERO_CERO) | (poComision.Pasaje == dictionary.CADENA_NULA) | (poComision.Pasaje == null))))
            {
                Query += " and ESTATUS = '5'";
            }
            else if ((poComision.Zona_Comercial == "15") & (((poComision.Combustible_Efectivo == dictionary.NUMERO_CERO) | (poComision.Combustible_Efectivo == null) | (poComision.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((poComision.Peaje == dictionary.NUMERO_CERO) | (poComision.Peaje == null) | (poComision.Peaje == dictionary.CADENA_NULA)) & ((poComision.Pasaje == dictionary.NUMERO_CERO) | (poComision.Pasaje == dictionary.CADENA_NULA) | (poComision.Pasaje == null))))
            {
                Query += " and ESTATUS = '5'";
            }
            else
            {
                Query += " and ESTATUS = '9'";
            }
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            //   MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() > 0) bandera = true;
            else bandera = false;
            poComision.Oficio = oficio;
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return bandera;
        }
        public static Boolean Update_Oficio_Comision_DGAIPP(Comision poComision, int psNoMinDGAIPP)
        {
            bool bandera = false;
            string Query = "";

            Query = "   UPDATE crip_comision SET NO_OFICIO_DGAIPP =  '" + psNoMinDGAIPP + "' ";
            Query += "where CLV_DEP_COM = '" + poComision.Ubicacion_Comisionado + "' AND USUARIO = '" + poComision.Comisionado + "'";
            Query += " and FOLIO = '" + poComision.Folio + "'";
            Query += " and NO_OFICIO = '" + poComision.Oficio + "'";
            
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            //   MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() > 0) bandera = true;
            else bandera = false;
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return bandera;
        }
        /// <summary>
        /// MEtodo que extrae maxima secuencia de tabla crip_comision_comprobacion
        /// </summary>
        /// <param name="psOficio"></param>
        /// <param name="psClvOficio"></param>
        /// <param name="psComisionado"></param>
        /// <param name="psUbicacionComisionado"></param>
        /// <param name="psProyecto"></param>
        /// <param name="psubicacionProy"></param>
        /// <returns></returns>
        public static string Obtiene_Max_Comprobacion(string psOficio, string psClvOficio, string psComisionado, string psUbicacionComisionado, string psProyecto, string psubicacionProy)
        {
            string Query = "SELECT MAX(SEC_EFF) AS MAX FROM crip_comision_comprobacion ";
            Query += " WHERE NO_OFICIO='" + psOficio + "'";
            Query += " AND COMISIONADO = '" + psComisionado + "'";
            Query += "  AND DEP_COMSIONADO = '" + psUbicacionComisionado + "'";
            Query += " AND CLV_PROY = '" + psProyecto + "'";
            Query += " AND CLV_DEP_PROY = '" + psubicacionProy + "' ";
            Query += " AND CLAVE_OFICIO = '" + psClvOficio + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
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

                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(liNumero);

        }

        public static string Obtiene_Max_Comision_Like(string psPeriodo, string psAbreviatura)
        {
            string Query = "";
            Query = " SELECT MAX(NO_OFICIO) AS MAX ";
            Query += " FROM crip_comision          ";
            Query += " WHERE ARCHIVO LIKE '%" + psAbreviatura + "%' ";
            Query += " AND PERIODO = '" + psPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
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

                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(liNumero);
        }

        public static string Obtiene_Max_Comision(string psUbicacion, string psPeriodo, bool psBandera = false)
        {
            string Query = "";
            if (!psBandera) Query = "SELECT MAX(FOLIO) AS MAX FROM crip_comision WHERE CLV_DEP = '" + psUbicacion + "'";
            else
            {


                if (psUbicacion == dictionary.DGAIA)
                {
                    Query = "SELECT MAX(NO_OFICIO) AS MAX FROM crip_comision WHERE CLV_DEP_AUTORIZA IN ('" + psUbicacion + "', '"+ dictionary.SUBACUA + "')";
                    Query += " AND PERIODO = '" + psPeriodo + "' ";
                }
                else if (psUbicacion == dictionary.DA)
                {
                    Query = "SELECT MAX(NO_OFICIO) AS MAX FROM crip_comision WHERE CLV_DEP_AUTORIZA IN ('" + psUbicacion + "', '" + dictionary.SUBINF + "', '" + dictionary.SUBFIN + "', '" + dictionary.SUBMAT + "', '" + dictionary.SUBHUM + "')";
                    Query += " AND PERIODO = '" + psPeriodo + "' ";
                }
                else if ((psUbicacion == dictionary.DG) | (psUbicacion == dictionary.SPDG) | (psUbicacion == dictionary.SIDG)) 
                {
                    Query = "SELECT MAX(NO_OFICIO) AS MAX FROM crip_comision WHERE CLV_DEP_AUTORIZA IN ('"+ psUbicacion +"', '" + dictionary.SPDG + "', '"+ dictionary.SIDG+"')";
                    Query += " AND PERIODO = '" + psPeriodo + "' ";
                }
                else
                {
                    Query = "SELECT MAX(NO_OFICIO) AS MAX FROM crip_comision WHERE CLV_DEP_AUTORIZA = '" + psUbicacion + "'";
                    Query += " AND PERIODO = '" + psPeriodo + "' ";

                }

                /*
                 Query = "SELECT MAX(NO_OFICIO) AS MAX FROM crip_comision WHERE CLV_DEP_AUTORIZA = '" + psUbicacion + "'";
                 Query += " AND PERIODO = '" + psPeriodo + "' ";
                 se comento el 27-08-2021 - Por el cambio de denominación de las unidades administrativas en el Estatuto del INAPESCA
                 */

            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
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

                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(liNumero);
        }

        public static Boolean Inserta_Comprobacion_Comision(string psOficio, string psClvOficio, string psComisionado, string psUbicacionComisionado, string psFechaFactura, string psProyecto, string psUbicacionProyecto, string psTipoComprobacion, string psClvConcepto, string psDescripcionConcepto, string psPdf, string psImporte, string psXml, string psMetPago, string psMetPagoUsser, string psObservaciones, string psDocumento, string psTicket, string psUUID, string psPeriodo, string psVersion="")
        {
            Boolean lbResultado;

            string Query = "INSERT INTO `crip_comision_comprobacion` ";
            Query += "    ( ";
            Query += " NO_OFICIO,";
            Query += " CLAVE_OFICIO,";
            Query += " COMISIONADO,";
            Query += " DEP_COMSIONADO,";
            Query += " FECHA_AUTORIZA,";
            Query += " FECHA_COMPROBACION,";
            Query += " FECHA_VOBO,";
            Query += " FECHA_FACTURA,";
            Query += " SEC_EFF,";
            Query += " CLV_PROY,";
            Query += " CLV_DEP_PROY,";
            Query += " TIPO_COMPROBANTE,";
            Query += " CONCEPTO,";
            Query += " DESCRIPCION,";
            Query += " COMPROBANTE,";
            Query += " IMPORTE,";
            Query += " `XML`,";
            Query += " METODO_PAGO_FACTURA,";
            Query += " METODO_PAGO_USUARIO,";
            Query += " OBSERVACIONES,";
            Query += " CLV_DOC,";
            Query += " DOCUMENTO_COMPROBACION,";
            Query += " TICKET,";
            Query += " ESTATUS,";
            Query += " ANIO, ";
            Query += " VERSION_CFDI";
            Query += " ) ";
            Query += " VALUES ";
            Query += " ( ";
            Query += " '" + psOficio + "', ";
            Query += " '" + psClvOficio + "', ";
            Query += " '" + psComisionado + "', ";
            Query += " '" + psUbicacionComisionado + "', ";
            Query += " '" + dictionary.FECHA_NULA + "', ";
            Query += " '" + lsHoy + "', ";
            Query += " '" + dictionary.FECHA_NULA + "', ";
            Query += " '" + psFechaFactura + "', ";
            Query += " '" + MngDatosComision.Obtiene_Max_Comprobacion(psOficio, psClvOficio, psComisionado, psUbicacionComisionado, psProyecto, psUbicacionProyecto) + "', ";
            Query += " '" + psProyecto + "', ";
            Query += " '" + psUbicacionProyecto + "', ";
            Query += " '" + psTipoComprobacion + "', ";
            Query += " '" + psClvConcepto + "',";
            Query += " '" + psDescripcionConcepto + "',";
            Query += " '" + psPdf + "',";
            Query += " '" + psImporte + "',";
            Query += " '" + psXml + "',";
            Query += " '" + psMetPago + "',";
            Query += " '" + psMetPagoUsser + "',";
            Query += " '" + psObservaciones + "',";
            Query += " '" + psUUID + "',";
            Query += " '" + psDocumento + "',";
            Query += " '" + psTicket + "', ";
            Query += " '1', ";
            Query += " '" + psPeriodo + "', ";
            Query += " '" + psVersion + "'";
            Query += " )";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }

        public static Boolean Inserta_Alterno(string psFolio, string psUbicacion, string psComisionado)
        {
            Boolean lbResultado;

            string Query = "INSERT INTO crip_alt ";
            Query += " (NO_OFICIO, CLV_DEP,COMISIONADO,FECHA) ";
            Query += " VALUES ";
            Query += " ('" + psFolio + "','" + psUbicacion + "','" + psComisionado + "','" + lsHoy + "')";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }

        /// <summary>
        /// Metodo que afecta ala bD en la tabala crip_comision_Infome insercion segun parametros
        /// </summary>
        /// <param name="psOficio"></param>
        /// <param name="psUbicacionSol"></param>
        /// <param name="pscComisionado"></param>
        /// <param name="psUbicacionComisionado"></param>
        /// <param name="psProyecto"></param>
        /// <param name="psUbicacionProyect"></param>
        /// <param name="psSecuencia"></param>
        /// <param name="psInforme"></param>
        /// <param name="psEvidencia1"></param>
        /// <param name="psEvidencia2"></param>
        /// <param name="psEvidencia3"></param>
        /// <param name="psDateI"></param>
        /// <param name="psDateF"></param>
        /// <returns></returns>
        public static Boolean Inserta_Informe_Comision(string psOficio, string psUbicacionSol, string pscComisionado, string psUbicacionComisionado, string psProyecto, string psUbicacionProyect, string psSecuencia, string psInforme, string psEvidencia1, string psEvidencia2, string psEvidencia3, string psDateI, string psDateF, string psPeriodo, string psDepAut="")
        {
            Boolean lbResultado;

            string Query = "INSERT INTO crip_comision_informe ";
            Query += "     ( ";
            Query += "        NO_OFICIO , ";
            Query += "        DEP_SOLICITUD,";
            Query += "        COMISIONADO, ";
            Query += "        CLV_DEP_COMISIONADO,";
            Query += "        CLV_PROY, ";
            Query += "        CLV_DEP_PROY, ";
            Query += "        ESTATUS ,";
            Query += "        SECUENCIA,";
            Query += "        ACTIVIDADES,";
            Query += "        EVIDENCIA1,";
            Query += "        EVIDENCIA2,";
            Query += "        EVIDENCIA3,";
            Query += "        FECHA_INFORME_I,";
            Query += "        FECHA_INFORME_F,";
            Query += "        FECHA_AUTORIZACION,";
            Query += "        PERIODO ";
            Query += "       )";
            Query += " VALUES ";
            Query += "      (";
            Query += "  '" + psOficio + "',";
            Query += "  '" + psUbicacionSol + "',";
            Query += "  '" + pscComisionado + "',";
            Query += "  '" + psUbicacionComisionado + "',";
            Query += "  '" + psProyecto + "',";
            Query += "  '" + psUbicacionProyect + "',";
            Query += "  '1',";
            Query += "  '" + psSecuencia + "',";
            Query += "  '" + psInforme + "',";
            Query += "  '" + psEvidencia1 + "',";
            Query += "  '" + psEvidencia2 + "',";
            Query += "  '" + psEvidencia3 + "',";
            Query += "  '" + psDateI + "',";
            Query += "  '" + psDateF + "',";
            Query += "  '" + dictionary.FECHA_NULA + "',";
            Query += "  '" + psPeriodo + "'";
            Query += "     )";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }


        public static Boolean Inserta_Oficio_Comision(Comision poComision, string psOficio)
        {
            Boolean lbResultado;
            string lsQuery = "   INSERT INTO crip_comision_detalle ( ";
            lsQuery += " NO_OFICIO,";
            lsQuery += " ARCHIVO,";
            lsQuery += " ARCHIVO_OFICIO_FIRMADO,";
            lsQuery += " CLV_TIPO_COM,";
            lsQuery += " FECHA_MINISTRACION,";
            lsQuery += " FECHA_INICIO,";
            lsQuery += " FECHA_FINAL,";
            lsQuery += " FECHA_PAGO,";
            lsQuery += " PROYECTO,";
            lsQuery += " DEP_PROYECTO,";
            lsQuery += " COMISIONADO,";
            lsQuery += " CLV_DEP_COM,";
            lsQuery += " FORMA_PAGO_VIATICOS,";
            lsQuery += " VIATICOS,";
            lsQuery += " PARTIDA,";
            lsQuery += " COMBUSTIBLE,";
            lsQuery += " PARTIDA_COMB,";
            lsQuery += " PEAJE,";
            lsQuery += " PARTIDA_PEAJE,";
            lsQuery += " PASAJE,";
            lsQuery += " PARTIDA_PASAJE,";
            lsQuery += " ESTATUS,";
            lsQuery += " SEC_EFF,";
            lsQuery += " FINANCIEROS,";
            lsQuery += " OBSERVACIONES,";
            lsQuery += " PERIODO, ";
            lsQuery += " TERRITORIO ";
            lsQuery += " )";
            lsQuery += " VALUES  ";
            lsQuery += " (";
            lsQuery += " '" + poComision.Oficio + "',";
            lsQuery += " '" + poComision.Archivo + "',";
            lsQuery += " '" + psOficio + "',";
            lsQuery += " '" + poComision.Tipo_Comision + "',";

            if ((poComision.Fecha_Vobo == "") | (poComision.Fecha_Vobo == null))
            {
                lsQuery += " '" + lsHoy + "',";
            }
            else
            {
                lsQuery += " '" + poComision.Fecha_Vobo + "',";
            }

            lsQuery += " '" + poComision.Fecha_Inicio + "',";
            lsQuery += " '" + poComision.Fecha_Final + "',";
            lsQuery += " '" + dictionary.FECHA_NULA + "',";
            lsQuery += " '" + poComision.Proyecto + "',";
            lsQuery += " '" + poComision.Dep_Proy + "',";
            lsQuery += " '" + poComision.Comisionado + "',";
            lsQuery += " '" + poComision.Ubicacion_Comisionado + "',";
            lsQuery += " '" + poComision.Forma_Pago_Viaticos + "',";
            lsQuery += " '" + poComision.Total_Viaticos + "',";
            lsQuery += " '" + poComision.Partida_Presupuestal + "',";
            lsQuery += " '" + poComision.Combustible_Efectivo + "',";
            lsQuery += " '" + poComision.Partida_Combustible + "',";
            lsQuery += " '" + poComision.Peaje + "',";
            lsQuery += " '" + poComision.Partida_Peaje + "',";
            lsQuery += " '" + poComision.Pasaje + "',";
            lsQuery += " '" + poComision.Partida_Pasaje + "',";
            lsQuery += " '" + poComision.Estatus + "',";
            lsQuery += " '1',";
            lsQuery += " '9',";

            /*if (poComision.Forma_Pago_Viaticos == "1") lsQuery += " '9',";
            else if (poComision.Forma_Pago_Viaticos == "2") lsQuery += " '2',";
            else lsQuery += " '0',";
            */
            lsQuery += " '" + poComision.Observaciones_Vobo + "',";

            if ((poComision.Periodo == null) | (poComision.Periodo == ""))
            {
                lsQuery += " '" + year + "',";
            }
            else
            {
                lsQuery += " '" + poComision.Periodo + "',";
            }

            lsQuery += " '" + poComision.Territorio + "'";
            lsQuery += ")";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }

        public static List<Entidad> Lista_ComprobantesFiscales(string psOficio, string psComisionado, string psUbicacionComisionado, string psProyecto, string psUbicacionproyecto, string psarchivo)
        {
            string Query = "SELECT `XML` AS CODIGO,COMPROBANTE AS DESCRIPCION";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE NO_OFICIO='" + psOficio + "'";
            Query += " AND COMISIONADO = '" + psComisionado + "'";
            Query += " AND DEP_COMSIONADO = '" + psUbicacionComisionado + "'";
            Query += " AND CLV_PROY = '" + psProyecto + "'";
            Query += "    AND CLV_DEP_PROY = '" + psUbicacionproyecto + "'";
            Query += " AND CLAVE_OFICIO = '" + psarchivo + "'";
            Query += " and ESTATUS = '1'";
            Query += " AND TIPO_COMPROBANTE = '2'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListGrid = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                ListGrid.Add(objetoEntidad);
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListGrid;
        }

        /// <summary>
        /// Metodo que obtiene nombre de informes de comision segun parametros
        /// </summary>
        /// <param name="psOficio"></param>
        /// <param name="psUbicacionSol"></param>
        /// <param name="psComisionado"></param>
        /// <param name="psUbicacionComisionado"></param>
        /// <param name="psProyect"></param>
        /// <param name="psUbicacionProy"></param>
        /// <returns></returns>
        public static List<Entidad> ObtieneNombresInformes(string psOficio, string psUbicacionSol, string psComisionado, string psUbicacionComisionado, string psProyect, string psUbicacionProy)
        {

            string Query = "SELECT SECUENCIA AS CODIGO,ACTIVIDADES DESCRIPCION";
            Query += "  FROM crip_comision_informe ";
            Query += "  WHERE NO_OFICIO = '" + psOficio + "' AND DEP_SOLICITUD = '" + psUbicacionSol + "'";
            Query += "   AND COMISIONADO = '" + psComisionado + "' AND CLV_DEP_COMISIONADO = '" + psUbicacionComisionado + "'";
            Query += "   AND CLV_PROY = '" + psProyect + "' AND CLV_DEP_PROY = '" + psUbicacionProy + "'";
            Query += "   ORDER BY SECUENCIA ASC";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListGrid = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                ListGrid.Add(objetoEntidad);
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListGrid;

        }

        public static Boolean Update_Estatus_Informe(string psOficio, string psUbicacionSol, string psComisionado, string psUbicacionComisionado, string psProyect, string psUbicacionProy, string psSecuencia, int piopcion)
        {
            Boolean lbResultado;

            string Query = "UPDATE crip_comision_informe SET";

            switch (piopcion)
            {
                case 1:
                    Query += " ESTATUS = '6',FECHA_INFORME_F = '" + lsHoy + "' , FECHA_AUTORIZACION = '" + lsHoy + "'";
                    break;
                case 2:
                    Query += " ESTATUS = '0',FECHA_INFORME_F = '" + lsHoy + "'";
                    break;

                case 3:
                    Query += " FECHA_INFORME_F = '" + lsHoy + "'";
                    break;
            }
            Query += "  WHERE NO_OFICIO = '" + psOficio + "' AND DEP_SOLICITUD = '" + psUbicacionSol + "'";
            Query += "  AND COMISIONADO = '" + psComisionado + "' AND CLV_DEP_COMISIONADO = '" + psUbicacionComisionado + "'";
            Query += "  AND CLV_PROY = '" + psProyect + "' AND CLV_DEP_PROY = '" + psUbicacionProy + "'";
            Query += "  AND SECUENCIA = '" + psSecuencia + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();


            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return lbResultado;
        }

        /// <summary>
        /// Metodo que inserta a ala tabla detalle comision
        /// </summary>
        /// <param name="psClv_Oficio"></param>
        /// <param name="psTipoComision"></param>
        /// <param name="psFolioSolicitud"></param>
        /// <param name="psUbicacion"></param>
        /// <param name="psFechaSol"></param>
        /// <param name="psFechaadjunto"></param>
        /// <param name="psAdjunto"></param>
        /// <param name="psFechaAut"></param>
        /// <param name="psAutoriza"></param>
        /// <param name="psComisionado"></param>
        /// <param name="psDepComisionado"></param>
        /// <returns></returns>
        public static Boolean Insert_Detalle_Comision(Comision poComision)
        {
            Boolean lbResultado;
            string lsQuery = "   INSERT INTO crip_comision_detalle ( ";
            lsQuery += " NO_OFICIO,";
            lsQuery += " ARCHIVO,";
            lsQuery += " CLV_TIPO_COM,";
            lsQuery += " FECHA_MINISTRACION,";
            lsQuery += " FECHA_INICIO,";
            lsQuery += " FECHA_FINAL,";
            lsQuery += " FECHA_PAGO,";
            lsQuery += " PROYECTO,";
            lsQuery += " DEP_PROYECTO,";
            lsQuery += " COMISIONADO,";
            lsQuery += " CLV_DEP_COM,";
            lsQuery += " FORMA_PAGO_VIATICOS,";
            lsQuery += " VIATICOS,";
            lsQuery += " PARTIDA,";
            lsQuery += " COMBUSTIBLE,";
            lsQuery += " PARTIDA_COMB,";
            lsQuery += " PEAJE,";
            lsQuery += " PARTIDA_PEAJE,";
            lsQuery += " PASAJE,";
            lsQuery += " PARTIDA_PASAJE,";
            lsQuery += " ESTATUS,";
            lsQuery += " SEC_EFF,";
            lsQuery += " FINANCIEROS,";
            lsQuery += " OBSERVACIONES,";
            lsQuery += " PERIODO, ";
            lsQuery += " TERRITORIO ";
            lsQuery += " )";
            lsQuery += " VALUES  ";
            lsQuery += " (";
            lsQuery += " '" + poComision.Oficio + "',";
            lsQuery += " '" + poComision.Archivo + "',";
            lsQuery += " '" + poComision.Tipo_Comision + "',";

            if ((poComision.Fecha_Vobo == "") | (poComision.Fecha_Vobo == null))
            {
                lsQuery += " '" + lsHoy + "',";
            }
            else
            {
                lsQuery += " '" + poComision.Fecha_Vobo + "',";
            }

            lsQuery += " '" + poComision.Fecha_Inicio + "',";
            lsQuery += " '" + poComision.Fecha_Final + "',";
            lsQuery += " '" + dictionary.FECHA_NULA + "',";
            lsQuery += " '" + poComision.Proyecto + "',";
            lsQuery += " '" + poComision.Dep_Proy + "',";
            lsQuery += " '" + poComision.Comisionado + "',";
            lsQuery += " '" + poComision.Ubicacion_Comisionado + "',";
            lsQuery += " '" + poComision.Forma_Pago_Viaticos + "',";
            lsQuery += " '" + poComision.Total_Viaticos + "',";
            lsQuery += " '" + poComision.Partida_Presupuestal + "',";
            lsQuery += " '" + poComision.Combustible_Efectivo + "',";
            lsQuery += " '" + poComision.Partida_Combustible + "',";
            lsQuery += " '" + poComision.Peaje + "',";
            lsQuery += " '" + poComision.Partida_Peaje + "',";
            lsQuery += " '" + poComision.Pasaje + "',";
            lsQuery += " '" + poComision.Partida_Pasaje + "',";
            lsQuery += " '" + poComision.Estatus + "',";
            lsQuery += " '1',";
            lsQuery += " '9',";

            /*if (poComision.Forma_Pago_Viaticos == "1") lsQuery += " '9',";
            else if (poComision.Forma_Pago_Viaticos == "2") lsQuery += " '2',";
            else lsQuery += " '0',";
            */
            lsQuery += " '" + poComision.Observaciones_Vobo + "',";

            if ((poComision.Periodo == null) | (poComision.Periodo == ""))
            {
                lsQuery += " '" + year + "',";
            }
            else
            {
                lsQuery += " '" + poComision.Periodo + "',";
            }

            lsQuery += " '" + poComision.Territorio + "'";
            lsQuery += ")";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() >= 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return lbResultado;
        }

        /// <summary>
        /// Metodo qu inserta a la tabla crip_comision  datos de solicitud para cada comisionado existente en la solicitud
        /// </summary>
        /// <param name="psClv_Oficio"></param>
        /// <param name="psTipoComision"></param>
        /// <param name="psFolio"></param>
        /// <param name="psNo_Oficio"></param>
        /// <param name="psFecha_Sol"></param>
        /// <param name="psFecharesp"></param>
        /// <param name="psFechaVoBo"></param>
        /// <param name="psFechaAut"></param>
        /// <param name="psUsuarioSol"></param>
        /// <param name="psDep"></param>
        /// <param name="psArea"></param>
        /// <param name="psProy"></param>
        /// <param name="psDep_Proy"></param>
        /// <param name="psLugar"></param>
        /// <param name="psTelefono"></param>
        /// <param name="psCapitulo"></param>
        /// <param name="psProceso"></param>
        /// <param name="psIndicador"></param>
        /// <param name="psPart_Presupuestal"></param>
        /// <param name="psFechaI"></param>
        /// <param name="psFechaF"></param>
        /// <param name="psDiasTotal"></param>
        /// <param name="psDiasReales"></param>
        /// <param name="psObjetivo"></param>
        /// <param name="psClaseT"></param>
        /// <param name="psTipoT"></param>
        /// <param name="psTrans_Sol"></param>
        /// <param name="psTransAut"></param>
        /// <param name="psDescAuto"></param>
        /// <param name="psDepTrans"></param>
        /// <param name="psRecorridoTerrestre"></param>
        /// <param name="psVueloPartNum"></param>
        /// <param name="psFechVueloPart"></param>
        /// <param name="psHorVueloPart"></param>
        /// <param name="psVueloRetNum"></param>
        /// <param name="psFechVueloRet"></param>
        /// <param name="psHorVueloRet"></param>
        /// <param name="psRecorridoAereo"></param>
        /// <param name="psComSol"></param>
        /// <param name="psComAut"></param>
        /// <param name="psPartCom"></param>
        /// <param name="psPeaje"></param>
        /// <param name="psPartPeaje"></param>
        /// <param name="psPasaje"></param>
        /// <param name="psPartPasaje"></param>
        /// <param name="psRecorridoAcuetico"></param>
        /// <param name="psSecEff"></param>
        /// <param name="psEquipo"></param>
        /// <param name="psObservSol"></param>
        /// <param name="psRespProy"></param>
        /// <param name="psVoBo"></param>
        /// <param name="psAut"></param>
        /// <param name="psComisionado"></param>
        /// <param name="psDep_Com"></param>
        /// <param name="psEstatus"></param>
        /// <param name="psObservVoBo"></param>
        /// <param name="psObservAut"></param>
        /// <param name="psEspecies"></param>
        /// <param name="psProductos"></param>
        /// <param name="psActividades"></param>
        /// <returns></returns>
        public static Boolean Insert_Comision(string psClv_Oficio, string psTipoComision, string psFolio, string psNo_Oficio, string psFecha_Sol, string psFecharesp, string psFechaVoBo, string psFechaAut, string psUsuarioSol, string psDep, string psArea, string psProy, string psDep_Proy, string psLugar, string psTelefono, string psCapitulo, string psProceso, string psIndicador, string psPart_Presupuestal, string psFechaI, string psFechaF, string psDiasTotal, string psDiasReales, string psObjetivo, string psClaseT, string psTipoT, string psTrans_Sol, string psTransAut, string psDescAuto, string psDepTrans, string psRecorridoTerrestre, string psVueloPartNum, string psFechVueloPart, string psHorVueloPart, string psVueloRetNum, string psFechVueloRet, string psHorVueloRet, string psRecorridoAereo, string psComSol, string psComAut, string psPartCom, string psPeaje, string psPartPeaje, string psPasaje, string psPartPasaje, string psRecorridoAcuetico, string psSecEff, string psEquipo, string psObservSol, string psRespProy, string psVoBo, string psAut, string psComisionado, string psDep_Com, string psEstatus, string psObservVoBo, string psObservAut, string psEspecies, string psProductos, string psActividades, string psTerritorio, string psUbicacion_Autoriza, string psPeriodo,string psPais, string psEstado)
        {
            Boolean pbResult;
            string lsQuery;
            lsQuery = " INSERT INTO crip_comision ( ";
            lsQuery = lsQuery + " CVL_OFICIO, ";
            lsQuery = lsQuery + " CLV_TIPO_COM, ";
            lsQuery = lsQuery + " FOLIO, ";
            lsQuery = lsQuery + " NO_OFICIO, ";
            lsQuery = lsQuery + " FECHA_SOL, ";
            lsQuery = lsQuery + " FECHA_RESP, ";
            lsQuery = lsQuery + " FECHA_VoBo, ";
            lsQuery = lsQuery + " FECHA_AUTORIZA, ";
            lsQuery = lsQuery + " USSER, ";
            lsQuery = lsQuery + " CLV_DEP, ";
            lsQuery = lsQuery + " CLV_AREA, ";
            lsQuery = lsQuery + " CLV_PROY, ";
            lsQuery = lsQuery + " CLV_DEP_PROY, ";
            lsQuery = lsQuery + " LUGAR, ";
            lsQuery = lsQuery + " TELEFONO, ";
            lsQuery = lsQuery + " CAPITULO, ";
            lsQuery = lsQuery + " PROCESO, ";
            lsQuery = lsQuery + " INDICADOR, ";
            lsQuery = lsQuery + " PART_PRESUPUESTAL ,";
            lsQuery = lsQuery + " FECHA_I, ";
            lsQuery = lsQuery + " FECHA_F, ";
            lsQuery = lsQuery + " DIAS_TOTAL, ";
            lsQuery = lsQuery + "DIAS_REALES, ";
            lsQuery = lsQuery + " OBJETIVO, ";
            lsQuery = lsQuery + " CLV_CLASE, ";
            lsQuery = lsQuery + "CLV_TIPO_TRANS, ";
            lsQuery = lsQuery + " `CLV_TRANS-SOL`, ";
            lsQuery = lsQuery + " CLV_TRANS_AUT, ";
            lsQuery = lsQuery + " DESC_AUTO, ";
            lsQuery = lsQuery + " DEP_TRANS, ";
            lsQuery = lsQuery + " ORIGEN_DESTINO_TER, ";
            lsQuery = lsQuery + " VUELO_PARTIDA_NUM, ";
            lsQuery = lsQuery + " FECHA_VUELO_IDA, ";
            lsQuery = lsQuery + " HORA_PART, ";
            lsQuery = lsQuery + " VUELO_RETURN_NUM, ";
            lsQuery = lsQuery + " FECHA_VUELO_RETURN, ";
            lsQuery = lsQuery + " HORA_RET, ";
            lsQuery = lsQuery + " ORIGEN_DESTINO_AER, ";
            lsQuery = lsQuery + " ORIGEN_DESTINO_ACU, ";
            lsQuery = lsQuery + " COMBUSTIBLE_SOL, ";
            lsQuery = lsQuery + " COMBUSTIBLE_AUT, ";
            lsQuery = lsQuery + " PARTIDA_COMBUSTIBLE, ";
            /*      lsQuery = lsQuery + " MET_VIATICOS, ";*/

            lsQuery = lsQuery + " PEAJE, ";
            lsQuery = lsQuery + " PART_PEAJE, ";
            lsQuery = lsQuery + " PASAJE, ";
            lsQuery = lsQuery + " PART_PASAJE, ";

            lsQuery = lsQuery + " SECEFF, ";
            lsQuery = lsQuery + " EQUIPO, ";
            lsQuery = lsQuery + " OBSERVACIONES_SOL, ";
            lsQuery = lsQuery + " RESP_PROY, ";
            lsQuery = lsQuery + " VoBo, ";
            lsQuery = lsQuery + "AUTORIZA, ";
            lsQuery = lsQuery + "USUARIO, ";
            lsQuery = lsQuery + "CLV_DEP_COM, ";
            lsQuery = lsQuery + " ESTATUS, ";
            lsQuery = lsQuery + "OBSERVACIONES_VoBo,";
            lsQuery = lsQuery + "OBSERVACIONES_AUT,";

            lsQuery = lsQuery + "ESPECIE,";
            lsQuery = lsQuery + "PRODUCTO,";
            lsQuery = lsQuery + "ACTIVIDAD_ESPECIFICA,";
            lsQuery = lsQuery + "TERRITORIO,";
            lsQuery = lsQuery + "CLV_DEP_AUTORIZA,";
            lsQuery = lsQuery + "PERIODO,";
            lsQuery = lsQuery + "PAIS,";
            lsQuery = lsQuery + "CLV_ESTADO";
            lsQuery = lsQuery + " ) ";

            lsQuery = lsQuery + " VALUES ";
            lsQuery = lsQuery + " ( ";
            lsQuery = lsQuery + "'" + psClv_Oficio + "',";
            lsQuery = lsQuery + "'" + psTipoComision + "',";
            lsQuery = lsQuery + "'" + psFolio + "',";
            lsQuery = lsQuery + "'" + psNo_Oficio + "',";
            lsQuery = lsQuery + "'" + psFecha_Sol + "',";
            lsQuery = lsQuery + "'" + psFecharesp + "',";
            lsQuery = lsQuery + "'" + psFechaVoBo + "',";
            lsQuery = lsQuery + "'" + psFechaAut + "',";
            lsQuery = lsQuery + "'" + psUsuarioSol + "',";
            lsQuery = lsQuery + "'" + psDep + "',";
            lsQuery = lsQuery + "'" + psArea + "',";
            lsQuery = lsQuery + "'" + psProy + "',";
            lsQuery = lsQuery + "'" + psDep_Proy + "',";
            lsQuery = lsQuery + "'" + psLugar + "',";
            lsQuery = lsQuery + "'" + psTelefono + "',";
            lsQuery = lsQuery + "'" + psCapitulo + "',";
            lsQuery = lsQuery + "'" + psProceso + "',";
            lsQuery = lsQuery + "'" + psIndicador + "',";
            lsQuery = lsQuery + "'" + psPart_Presupuestal + "',";
            lsQuery = lsQuery + "'" + psFechaI + "',";
            lsQuery = lsQuery + "'" + psFechaF + "',";
            lsQuery = lsQuery + "'" + psDiasTotal + "',";
            lsQuery = lsQuery + "'" + psDiasReales + "',";
            lsQuery = lsQuery + "'" + psObjetivo + "',";
            lsQuery = lsQuery + "'" + psClaseT + "',";
            lsQuery = lsQuery + "'" + psTipoT + "',";
            lsQuery = lsQuery + "'" + psTrans_Sol + "',";
            lsQuery = lsQuery + "'" + psTransAut + "',";
            lsQuery = lsQuery + "'" + psDescAuto + "',";
            lsQuery = lsQuery + "'" + psDepTrans + "',";
            lsQuery = lsQuery + "'" + psRecorridoTerrestre + "',";
            lsQuery = lsQuery + "'" + psVueloPartNum + "',";
            lsQuery = lsQuery + "'" + psFechVueloPart + "',";
            lsQuery = lsQuery + "'" + psHorVueloPart + "',";
            lsQuery = lsQuery + "'" + psVueloRetNum + "',";
            lsQuery = lsQuery + "'" + psFechVueloRet + "',";
            lsQuery = lsQuery + "'" + psHorVueloRet + "',";
            lsQuery = lsQuery + "'" + psRecorridoAereo + "',";
            lsQuery = lsQuery + "'" + psRecorridoAcuetico + "',";
            lsQuery = lsQuery + "'" + psComSol + "',";
            lsQuery = lsQuery + "'" + psComAut + "',";
            lsQuery = lsQuery + "'" + psPartCom + "',";
            /* lsQuery = lsQuery + "'" + psMetViaticos + "',";*/

            lsQuery = lsQuery + "'" + psPeaje + "',";
            lsQuery = lsQuery + "'" + psPartPeaje + "',";
            lsQuery = lsQuery + "'" + psPasaje + "',";
            lsQuery = lsQuery + "'" + psPartPasaje + "',";

            lsQuery = lsQuery + "'" + psSecEff + "',";
            lsQuery = lsQuery + "'" + psEquipo + "',";
            lsQuery = lsQuery + "'" + psObservSol + "',";
            lsQuery = lsQuery + "'" + psRespProy + "',";
            lsQuery = lsQuery + "'" + psVoBo + "',";
            lsQuery = lsQuery + "'" + psAut + "',";
            lsQuery = lsQuery + "'" + psComisionado + "',";
            lsQuery = lsQuery + "'" + psDep_Com + "',";
            lsQuery = lsQuery + "'" + psEstatus + "',";
            lsQuery = lsQuery + "'" + psObservVoBo + "',";
            lsQuery = lsQuery + "'" + psObservAut + "',";

            lsQuery = lsQuery + "'" + psEspecies + "',";
            lsQuery = lsQuery + "'" + psProductos + "',";
            lsQuery = lsQuery + "'" + psActividades + "',";
            lsQuery = lsQuery + "'" + psTerritorio + "',";
            lsQuery = lsQuery + "'" + psUbicacion_Autoriza + "',";
            lsQuery = lsQuery + "'" + psPeriodo + "',";
            lsQuery = lsQuery + "'" + psPais + "',";
            lsQuery = lsQuery + "'" + psEstado + "'";
            lsQuery = lsQuery + " ) ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            
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

        public static string Obtiene_Tarifas(string psZona)
        {
            string valor = "0";
            string Query = "SELECT TARIFA FROM crip_zonas WHERE CLV_ZONA = '" + psZona + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                valor = Convert.ToString(Reader["TARIFA"]);
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return valor;
        }

        public static List<Entidad> ObtieneTipoComprobacion(bool pbBander = false)
        {
            string query = "";

            query = " SELECT CLV_TIPO AS CODIGO, DESCRIPCION AS DESCRIPCION FROM crip_tipocomprobaciones ";
            query += " WHERE ESTATUS = '1'";

            if (!pbBander)
            {
                query += "  AND CLV_TIPO IN ( '0','1', '2','3','4')";
            }
            else
            {
                query += "  AND CLV_TIPO NOT IN ( '1', '2','3','4','10','12','13','17','18')";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListGrid = new List<Entidad>();
            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                ListGrid.Add(objetoEntidad);
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListGrid;
        }

        public static List<Entidad> Obtiene_Comision(string psAction, string psUsuario, string psEstatus)
        {
            string USUARIO = "";

            string Query = "";

            switch (psAction)
            {
                case "AUTORIZA":

                    Query = " SELECT  DISTINCT FOLIO AS CODIGO ,";
                    Query += " USSER  AS DESCRIPCION,  ";
                    Query += " CLV_DEP   AS UBICACION,";
                    Query += " PERIODO AS PERIODO";
                    Query += " FROM crip_comision  WHERE ESTATUS = '" + psEstatus + "'";
                    Query += " AND AUTORIZA =  '" + psUsuario + "'";
                    Query += "   AND FECHA_AUTORIZA = '" + dictionary.FECHA_NULA + "'";

                    break;
                case "VOBO":

                    Query = " SELECT  DISTINCT FOLIO AS CODIGO ,";
                    Query += " USSER  AS DESCRIPCION,  ";
                    Query += " CLV_DEP   AS UBICACION,";
                    Query += " PERIODO AS PERIODO";
                    Query += " FROM crip_comision  WHERE ESTATUS = '" + psEstatus + "'";
                    Query += " AND VoBo =  '" + psUsuario + "'";
                    Query += " AND FECHA_VoBo = '" + dictionary.FECHA_NULA + "'";

                    if (psEstatus != "1")
                    {
                        Query += "   AND FECHA_AUTORIZA != '" + dictionary.FECHA_NULA + "'";
                    }


                    break;
                case "AUTDIAS":

                    Query = " SELECT  DISTINCT FOLIO AS CODIGO ,";
                    Query += " USSER  AS DESCRIPCION,  ";
                    Query += " CLV_DEP   AS UBICACION,";
                    Query += " PERIODO AS PERIODO";
                    Query += " FROM crip_comision  WHERE ESTATUS = '" + psEstatus + "'";
                    Query += " AND VoBo =  '" + psUsuario + "'";

                    break;
            }

            /*
            Query += " AND FECHA_SOL BETWEEN (SELECT INICIO FROM CRIP_ANIO_FISCAL WHERE ESTATUS = '1') AND (SELECT FINAL FROM CRIP_ANIO_FISCAL WHERE ESTATUS =  '1') ";
          */

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListGrid = new List<Entidad>();
            //string lsComisionT = Dictionary.CADENA_NULA;
            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]) + "|" + Convert.ToString(Reader["UBICACION"]);
                USUARIO = MngDatosUsuarios.Obtiene_Nombre_Completo(Convert.ToString(Reader["DESCRIPCION"]));
                //lsComisionT = Convert.ToString(Reader["TIPO_COMISION"]);
                ///  if (lsComisionT == "01") objetoEntidad.Descripcion = " Comision Ordinaria - "  + USUARIO + " - " + Convert.ToString(Reader["CODIGO"]) ;
                objetoEntidad.Descripcion = USUARIO + " - " + Convert.ToString(Reader["CODIGO"]) + " - " + Convert.ToString(Reader["PERIODO"]);

                ListGrid.Add(objetoEntidad);
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListGrid;
        }

        public static List<Entidades.GridView> ComisionadosS(string psFolio, string psOpcion, string psUsuario, string psEstatus, string psTipoCom = "", string psDep = "")
        {
            string Query = "SELECT USUARIO ,CLV_DEP_COM FROM crip_comision WHERE FOLIO = '" + psFolio + "'";

            if (psDep != "") Query += " AND CLV_DEP = '" + psDep + "'";

            if (psTipoCom != "") Query += "AND CLV_TIPO_COM = '" + psTipoCom + "'";

            if (psOpcion == dictionary.AUTORIZA)
            {
                Query += "  AND AUTORIZA= '" + psUsuario + "'";
            }
            else
            {
                Query += "  AND VoBo= '" + psUsuario + "'";
            }

            Query += " AND ESTATUS = '" + psEstatus + "'  ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidades.GridView> ListaEntidad = new List<GridView>();

            while (Reader.Read())
            {
                Entidades.GridView objetoEntidad = new GridView();
                Usuario objUsuario = MngDatosUsuarios.DatosComisionado(Convert.ToString(Reader["USUARIO"]), Convert.ToString(Reader["CLV_DEP_COM"]));

                objetoEntidad.Usuario = objUsuario.Usser;
                objetoEntidad.Comisionado = objUsuario.Nombre + " " + objUsuario.ApPat + " " + objUsuario.ApMat;
                objetoEntidad.Puesto = objUsuario.Cargo;
                ListaEntidad.Add(objetoEntidad);
                objetoEntidad = null;
                objUsuario = null;
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);


            return ListaEntidad;
        }
        /// <summary>
        /// Extrae lista de comisionados de solicitud ordinaria 
        /// </summary>
        /// <param name="psFolio"></param>
        /// <param name="psDep"></param>
        /// <param name="psOpcion"></param>
        /// <param name="psUsuario"></param>
        /// <returns></returns>
        public static List<Entidad> Comisionados(string psFolio, string psOpcion, string psUsuario, string psEstatus, string psTipoCom = "", string psDep = "")
        {
            string Query = "SELECT USUARIO FROM crip_comision WHERE FOLIO = '" + psFolio + "'";

            if (psDep != "") Query += " AND CLV_DEP = '" + psDep + "'";

            if (psTipoCom != "") Query += "AND CLV_TIPO_COM = '" + psTipoCom + "'";

            if (psOpcion == dictionary.AUTORIZA)
            {
                Query += "  AND AUTORIZA= '" + psUsuario + "'";
            }
            else
            {
                Query += "  AND VoBo= '" + psUsuario + "'";
            }

            Query += " AND ESTATUS = '" + psEstatus + "'  ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["USUARIO"]);
                objetoEntidad.Descripcion = MngDatosUsuarios.Obtiene_Nombre_Completo(Convert.ToString(Reader["USUARIO"]));

                ListaEntidad.Add(objetoEntidad);
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }

        /// <summary>
        ///Meotodo que extraes todas las comisiones comprobadas y no comprobadas
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <param name="psDep"></param>
        /// <returns></returns>
        public static List<comprobacion> Comision_Comp(string psUsuario, string psDep)
        {
            string Query = " SELECT DISTINCT SUM(IMPORTE) AS IMPORTE, ";
            Query += " NO_OFICIO AS OFICIO, ";
            Query += " FECHA_AUTORIZA AS FECHA_AUTORIZA, ";
            Query += " FECHA_VOBO AS FECHA_VOBO";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE COMISIONADO ='" + psUsuario + "'";
            Query += " AND DEP_COMSIONADO = '" + psDep + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<comprobacion> ListaEntidad = new List<comprobacion>();

            while (Reader.Read())
            {
                comprobacion objGrid = new comprobacion();
                objGrid.Importe = Convert.ToString(Reader["IMPORTE"]);
                objGrid.Id = Convert.ToString(Reader["OFICIO"]);
                objGrid.Fecha = Convert.ToString(Reader["FECHA_AUTORIZA"]);
                objGrid.Concepto = Convert.ToString(Reader["FECHA_VOBO"]);

                ListaEntidad.Add(objGrid);
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }

        public static Boolean Insert_Folio_Comprobante(string psOficio, string psArchivo, string psComisionado)
        {
            Boolean lbResultado;
            string lsQuery = "   INSERT INTO CRIP_COMISION_COMPROBANTE";
            lsQuery += "(FOLIO,NO_OFICIO,ARCHIVO,COMISIONADO,ESTATUS,FECHA,FECH_EFF)";
            lsQuery += "VALUES ( '" + MngDatosComision.Obtiene_Max_Comprobante() + "','" + psOficio + "','" + psArchivo + "','" + psComisionado + "','1','" + lsHoy + "','" + lsHoy + "')";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
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

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }

        public static string Obtiene_Max_Comision_Comprobar(string psComisionado)
        {
            int YearAnt = Convert.ToInt16(year) - 1;
            string query = "";
            string fechaMax;
            query = "SELECT MIN(FECHA_F) AS MINIMO  ";
            query += "FROM crip_comision ";
            query += " WHERE USUARIO = '" + psComisionado + "'";
            query += " AND FORMA_PAGO_VIATICOS = '2' ";
            query += " AND PERIODO IN ('" + YearAnt + "', '"+ year +"')";
            query += " AND ESTATUS = 9 ";
            query += " AND FECHA_F < '" + lsHoy + "'";
            


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                fechaMax = Convert.ToString(Reader["MINIMO"]);

            }
            else
            {
                fechaMax = "";
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return fechaMax;
        }

        public static string Obtiene_Max_Comprobante()
        {
            string query = "";

            query = "select MAX(Folio) as MAX from crip_comision_comprobante";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                max = Convert.ToString(reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }

                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(liNumero);
        }

        public static string Obtiene_Dias_Continuos(string psFechas, string psComisionado, string psTerritorio)
        {
            string resultado = "";

            string Query = "";
            Query += " SELECT SUM(DIAS) AS DIAS ";
            Query += "    FROM vw_comisiones ";
            Query += "    where 1=1 ";
            Query += "   AND FINAL in(" + psFechas + ")";
            Query += "   AND COMISIONADO = '" + psComisionado + "'";
            Query += " AND TERRITORIO = '" + psTerritorio + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DIAS"]);
            }

            if ((resultado == null) | (resultado == "")) resultado = dictionary.NUMERO_CERO;

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;


        }

        public static string Obtiene_Folio_Comprobacion(string Oficio, string Archivo, string Comisionado)
        {
            string resultado = "";

            string Query = "";
            Query += " SELECT FOLIO FROM CRIP_COMISION_COMPROBANTE";
            Query += " WHERE NO_OFICIO = '" + Oficio + "' AND ARCHIVO = '" + Archivo + "'";
            Query += "AND COMISIONADO = '" + Comisionado + "'  ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["FOLIO"]);
            }

            if ((resultado == null) | (resultado == "")) resultado = dictionary.NUMERO_CERO;
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        public static string Obtiene_Importe_total(string psUsuario, string psDep, string psFolio)
        {
            string resultado = "";

            string Query = "";
            Query += " SELECT DISTINCT SUM(IMPORTE) AS IMPORTE";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE COMISIONADO ='" + psUsuario + "' AND DEP_COMSIONADO = '" + psDep + "' ";
            Query += " AND ESTATUS != '0'";
            /*  Query += " AND ANIO = '" + Year + "'";*/
            Query += " AND NO_OFICIO = '" + psFolio + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["IMPORTE"]);
            }

            if ((resultado == null) | (resultado == "")) resultado = dictionary.NUMERO_CERO;

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;

        }

        public static List<Entidad> Comision_Comprobada(string psUsuario, string psDep, int tipo)
        {
            string Query = "";

            Query += " SELECT DISTINCT SUM(IMPORTE) AS IMPORTE, NO_OFICIO AS OFICIO ";
            Query += " FROM crip_comision_comprobacion ";
            Query += " WHERE COMISIONADO ='" + psUsuario + "' AND DEP_COMSIONADO = '" + psDep + "' ";


            if (tipo == 1)
            {
                Query += " AND ESTATUS != '0'";
                //Query += " AND FECHA_AUTORIZA = '1900-01-01'";
            }
            else if (tipo == 2)
            {
                Query += " AND ESTATUS = '7'";
                Query += " AND FECHA_VOBO = '1900-01-01'";

            }
            else if (tipo == 3)
            {
                Query += " AND ESTATUS = '5'";
                Query += " AND FECHA_VOBO != '1900-01-01'";
                Query += " AND FECHA_AUTORIZA = '1900-01-01'";
            }

            // Query += " AND ANIO = '" + Year + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objGrid = new Entidad();
                objGrid.Codigo = Convert.ToString(Reader["IMPORTE"]);
                objGrid.Descripcion = Convert.ToString(Reader["OFICIO"]);

                if ((objGrid.Codigo == "") | (objGrid.Codigo == null))
                {
                    ;
                }
                else
                {
                    ListaEntidad.Add(objGrid);
                }

            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListaEntidad;
        }

        public static DataSet Filtros_Reintegros(string psPeriodo, string psInicio, string psFinal, string psEstatus, string psAdscripcion, string psUsuario)
        {
            DataSet datasetArbol = new DataSet("DataSetArbol");
            string query = "";
            query += "SELECT A.NO_OFICIO,  ";
            query += " A.ARCHIVO, ";
            query += " A.USUARIO,";
            query += " A.LUGAR, ";
            query += " CONCAT('DEL ',A.FECHA_I,' AL ',A.FECHA_F) AS FECHAS,";
            query += " (A.PASAJE + A.PEAJE +A.COMBUST_EFECTIVO +A.TOTAL_VIATICOS ) AS TOTAL,";
            query += " A.ESTATUS,";
            query += " A.CLV_DEP_COM , ";
            query += " A.RUTA ";
            query += " FROM crip_comision A ";
            query += " WHERE 1 = 1 ";
            query += " AND ESTATUS NOT IN ('0','1','8')";
            query += " AND PERIODO = '" + psPeriodo + "'";
            query += " AND FECHA_I BETWEEN '" + psInicio + "' AND '" + psFinal + "' ";

            if (psEstatus != "")
            {
                query += " AND ESTATUS IN (" + psEstatus + ")";
            }

            if (psAdscripcion != string.Empty)
            {
                query += "     AND CLV_DEP_COM = '" + psAdscripcion + "'";
            }
            if ((psUsuario != "") | (psUsuario != string.Empty))
            {
                query += "  AND USUARIO = '" + psUsuario + "'";
            }

            query += "  AND (A.PASAJE + A.PEAJE +A.COMBUST_EFECTIVO +A.TOTAL_VIATICOS ) > 0 ";
            query += " ORDER BY NO_OFICIO ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConexionMysql);

            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return datasetArbol;
        }


        public static List<Entidad> Lista_Centros_Comisiones(string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
  { 
//        
//FROM crip_comision   
//WHERE 1 = 1       
//AND ESTATUS NOT IN ('0','1','8')  
//AND PERIODO = '2017'   
//AND FECHA_I BETWEEN '2017-01-01' AND '2017-12-31' 
//ORDER BY CLV_DEP_PROY ASC

      string query = "";
      query += "SELECT DISTINCT CLV_DEP_PROY  ";
      
            query += "  FROM crip_comision ";
      query += "  WHERE 1 = 1     ";
      query += "  AND ESTATUS NOT IN ('0','1','8')";
      query += "   AND PERIODO = '" + psPeriodo + "' ";
      query += "  AND FECHA_I BETWEEN '" + psInicio + "' AND '" + psFinal + "'";

      if (psFormaPago != "")
      {
          query += " AND FORMA_PAGO_VIATICOS = '" + psFormaPago + "'";
      }
      if (psEstatus != "")
      {
          query += " AND ESTATUS IN (" + psEstatus + ")";
      }
      if (psAdscripcion != string.Empty)
      {
          query += " AND CLV_DEP_COM = '" + psAdscripcion + "'";
      }
      if ((psUsuario != "") | (psUsuario != string.Empty))
      {
          query += " AND USUARIO = '" + psUsuario + "'";
      }
      query += "ORDER BY CLV_DEP_PROY ASC";

     // List<Entidad> ListaNueva = new List<Entidad>();
     
      MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
      MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
      cmd.Connection.Open();

      MySqlDataReader Reader = cmd.ExecuteReader();

      List<Entidad> ListaEntidad = new List<Entidad>();

      while (Reader.Read())
      {
          Entidad objGrid = new Entidad();
          {
              objGrid.Codigo = Convert.ToString(Reader["CLV_DEP_PROY"]);
              objGrid.Descripcion = MngDatosDependencia.Descrip_Centro(objGrid.Codigo,true );
              
          }
          ListaEntidad.Add(objGrid);
      }

      Reader.Close();

      MngConexion.disposeConexionSMAF(ConexionMysql);

      return ListaEntidad;

        }

        public static List<Entidad> Lista_Proyectos_Comisiones(string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
        {
            //        
            //FROM crip_comision   
            //WHERE 1 = 1       
            //AND ESTATUS NOT IN ('0','1','8')  
            //AND PERIODO = '2017'   
            //AND FECHA_I BETWEEN '2017-01-01' AND '2017-12-31' 
            //ORDER BY CLV_DEP_PROY ASC

            string query = "";
            query += "SELECT DISTINCT CLV_PROY, CLV_DEP_PROY  ";

            query += "  FROM crip_comision ";
            query += "  WHERE 1 = 1     ";
            query += "  AND ESTATUS NOT IN ('0','1','8')";
            query += "   AND PERIODO = '" + psPeriodo + "' ";
            query += "  AND FECHA_I BETWEEN '" + psInicio + "' AND '" + psFinal + "'";

            if (psFormaPago != "")
            {
                query += " AND FORMA_PAGO_VIATICOS = '" + psFormaPago + "'";
            }
            if (psEstatus != "")
            {
                query += " AND ESTATUS IN (" + psEstatus + ")";
            }
            if (psAdscripcion != string.Empty)
            {
                query += " AND CLV_DEP_COM = '" + psAdscripcion + "'";
            }
            if ((psUsuario != "") | (psUsuario != string.Empty))
            {
                query += " AND USUARIO = '" + psUsuario + "'";
            }
            query += "ORDER BY CLV_DEP_PROY ASC";

            // List<Entidad> ListaNueva = new List<Entidad>();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();
            string depproy = "";
            while (Reader.Read())
            {
                Entidad objGrid = new Entidad();
                {
                    objGrid.Codigo = Convert.ToString(Reader["CLV_PROY"]);
                    objGrid.Descripcion = Convert.ToString(Reader["CLV_DEP_PROY"]); 
                    depproy = "";
                }
                ListaEntidad.Add(objGrid);
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }

       //para auditoría

        public static DataSet Filtros1(string psTipoCom, string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
        {
            DataSet datasetArbol = new DataSet("DataSetArbol");
            string query = "";
    
            query += "  SELECT NO_OFICIO AS OFICIO, ";
            query += "  ARCHIVO AS ARCHIVO,  ";
            query += "  ARCHIVO_AMPLIACION AS AMPLIACION,  ";
            query += "  USUARIO AS USUARIO, ";
            query += "  LUGAR AS LUGAR, ";
            query += "  CONCAT('DEL ',FECHA_I,' AL ',FECHA_F) AS FECHAS, ";
            query += "  PASAJE AS PASAJE, ";
            query += "  PEAJE AS PEAJE, ";
            query += "  COMBUST_EFECTIVO AS COMBUST_EFECTIVO, ";
            query += "  TOTAL_VIATICOS AS VIATICOS,  ";
            query += "  SINGLADURAS AS SINGLADURAS, ";
            query += "  ZONA_COMERCIAL AS ZONA, ";
            query += "  CLV_DEP_COM AS UBICACION,   ";
            query += "  ESTATUS as ESTATUS,";
            query += "  TERRITORIO AS TERRITORIO ,";
            query += "  FORMA_PAGO_VIATICOS FORMA, ";
            query += "  CLV_DEP_PROY AS UBICACION_PROYECTO, ";
            query += "  CLV_PROY AS PROYECTO ";
            query += "  FROM crip_comision ";
            query += "  WHERE 1 = 1     ";
            query += "  AND ESTATUS NOT IN ('0','1','8')";
            query += "  AND TERRITORIO IN (" + psTipoCom + ")";
            query += "   AND PERIODO = '" + psPeriodo + "' ";
            query += "  AND FECHA_I BETWEEN '" + psInicio + "' AND '" + psFinal + "'";

            if (psFormaPago != "")
            {
                query += " AND FORMA_PAGO_VIATICOS = '" + psFormaPago + "'";
            }
            if (psEstatus != "")
            {
                query += " AND ESTATUS IN (" + psEstatus + ")";
            }
            if (psAdscripcion != string.Empty)
            {
                query += " AND CLV_DEP_COM = '" + psAdscripcion + "'";
            }
            if ((psUsuario != "") | (psUsuario != string.Empty))
            {
                query += " AND USUARIO = '" + psUsuario + "'";
            }
            query += " ORDER BY NO_OFICIO ASC";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConexionMysql);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return datasetArbol;
        }
     

        //termina para auditoría
        public static DataSet Filtros(string psPeriodo, string psInicio, string psFinal, string psFormaPago, string psEstatus, string psAdscripcion, string psUsuario)
        {
            DataSet datasetArbol = new DataSet("DataSetArbol");
            string query = "";
            query += "SELECT NO_OFICIO AS OFICIO, ";
            query += " ARCHIVO AS ARCHIVO, ";
            query += " USUARIO AS USUARIO, ";
            query += " LUGAR AS LUGAR, ";
            query += " CONCAT('DEL ',FECHA_I,' AL ',FECHA_F) AS FECHAS, ";
            query += " PASAJE AS PASAJE,";
            query += " PEAJE AS PEAJE, ";
            query += " COMBUST_EFECTIVO AS COMBUST_EFECTIVO, ";
            query += " TOTAL_VIATICOS AS VIATICOS,";
            query += "  SINGLADURAS AS SINGLADURAS,";
            query += " CLV_DEP_COM AS UBICACION,";
            query += " ESTATUS as ESTATUS";
            query += "  FROM crip_comision ";
            query += "  WHERE 1 = 1     ";
            query += "  AND ESTATUS NOT IN ('0','1','8')";
            query += "   AND PERIODO = '" + psPeriodo + "' ";
            query += "  AND FECHA_I BETWEEN '" + psInicio + "' AND '" + psFinal + "'";
           
            if (psFormaPago != "")
            {
                query += " AND FORMA_PAGO_VIATICOS = '" + psFormaPago + "'";
            }
            if (psEstatus != "")
            {
                query += " AND ESTATUS IN (" + psEstatus + ")";
            }
            if (psAdscripcion != string.Empty)
            {
                query += " AND CLV_DEP_COM = '" + psAdscripcion + "'";
            }
            if ((psUsuario != "") | (psUsuario != string.Empty))
            {
                query += " AND USUARIO = '" + psUsuario + "'";
            }
            query += " ORDER BY NO_OFICIO ASC";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConexionMysql);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return datasetArbol;
        }
        //cambiarts

        public static DataSet Comisiones(string psUsuario, string psDep, string psPeriodo, string psEstatus = "")
        {
            DataSet datasetArbol = new DataSet("DataSetArbol");
            string query = "";
            //query = " SELECT  DISTINCT A.NO_OFICIO AS OFICIO ,  ";
            query = "  SELECT  DISTINCT A.ARCHIVO AS OFICIO, ";
            query += " A.LUGAR AS DESTINO, ";
            query += " CONCAT(A.FECHA_I,' AL ',A.FECHA_F) AS PERIODO,";
            query += " (A.TOTAL_VIATICOS + A.PEAJE + A.COMBUST_EFECTIVO + A.PASAJE ) as TOTAL_VIATICOS ,";
            query += " A.ESTATUS AS ESTATUS,";
            query += " A.FORMA_PAGO_VIATICOS ";
            query += " FROM crip_comision A ";
            query += " WHERE USUARIO = '" + psUsuario + "' ";
            /* query += " and A.CLV_DEP_COM = '" + psDep + "'";*/
            query += "AND PERIODO = '" + psPeriodo + "'";
            query += " AND NO_OFICIO != 0 ";

            if (psEstatus != "") query += " AND ESTATUS = '" + psEstatus + "' ";

            query += " ORDER BY A.NO_OFICIO ASC";
            /* MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql );
             cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

             List<menu_comprobacion > listamenu = new List<menu_comprobacion>();

             while (Reader.Read())
             {
                 menu_comprobacion objmenu = new menu_comprobacion();
                 {
                     objmenu.Folio = Convert.ToString(Reader["OFICIO"]);
                     objmenu.Lugar = Convert.ToString(Reader["DESTINO"]);
                     objmenu.Periodo = Convert.ToString(Reader["PERIODO"]);
                     objmenu.Total = Convert.ToString(Reader["TOTAL_VIATICOS"]);
                     objmenu.Estatus = Convert.ToString(Reader["ESTATUS"]);
                     objmenu.Tipo = Convert.ToString(Reader["FORMA_PAGO_VIATICOS"]);   
                 }
                 listamenu.Add(objmenu);
              objmenu = null;
             }

             MngConexion.disposeConexion();

             return listamenu;*/

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConexionMysql);
            datasetArbol.Tables.Clear();
            adapter.Fill(datasetArbol, "DataSetArbol");

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return datasetArbol;
        }
        /// <summary>
        /// Metodo que extrae lista de comisiones con total de recurso autorizado
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <returns></returns>
        public static List<GridView> Comisiones_Recurso(string psUsuario, string psDep, string psestatus = "")
        {
            string query = "SELECT DISTINCT	A.NO_OFICIO AS OFICIO , ";
            query += " A.LUGAR AS DESTINO,";
            // query += "A.TOTAL_VIATICOS + A.PEAJE + A.COMBUSTIBLE_AUT + A.PASAJE ) as TOTAL_VIATICOS";
            query += "(A.TOTAL_VIATICOS + A.PEAJE + A.COMBUSTIBLE_AUT + A.PASAJE ) as TOTAL_VIATICOS  ";
            query += " FROM crip_comision A ";
            query += " WHERE  A.USUARIO = '" + psUsuario + "'";

            if (psestatus != "")
            {
                query += " and A.ESTATUS in (" + psestatus + ")";
            }
            else
            {
                query += " and A.ESTATUS = '9'";
            }

            query += " and A.CLV_DEP_COM = '" + psDep + "'";

            query += "  AND ZONA_COMERCIAL NOT IN ('15','18')";
            query += "  AND FORMA_PAGO_VIATICOS NOT IN ('0', '1') ";
            query += "  AND COMBUST_EFECTIVO = '0' ";
            query += "  AND PEAJE = '0' ";
            query += "  AND PASAJE = '0' ";
            query += "AND FECHA_F < '" + lsHoy + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<GridView> ListaEntidad = new List<GridView>();

            while (Reader.Read())
            {
                GridView objGrid = new GridView();
                {
                    objGrid.Adscripcion = Convert.ToString(Reader["OFICIO"]);
                    objGrid.Lugar = Convert.ToString(Reader["DESTINO"]);
                    objGrid.Comisionado = Convert.ToString(Reader["TOTAL_VIATICOS"]);
                }
                ListaEntidad.Add(objGrid);
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }

        public static List<Entidad> Action(string lsRol)
        {


            string Query = "        SELECT DISTINCT TIPO_MAIL FROM  crip_jerarquia ";
            Query += "   WHERE ID_PADRE = '" + lsRol + "'  ";
            Query += "   AND ESTATUS='1' ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["TIPO_MAIL"]);
                objetoEntidad.Descripcion = string.Empty;

                ListaEntidad.Add(objetoEntidad);

            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        }

        /// <summary>
        /// Metodo que valida si existe cruze de comision para cierto inbvestigador
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <param name="psFechaInicio"></param>
        /// <param name="psFechaFinal"></param>
        /// <returns></returns>
        public static bool Comision_Extraordinaria(string psUsuario, string psFechaInicio, string psFechaFinal, string psOpcion)
        {
            bool bandera = false;
            string Query = "SELECT * FROM crip_comision ";
            Query += " where  1= 1 ";

            if (psOpcion == "1") Query += "AND  FECHA_I ";
            else if (psOpcion == "2") Query += "AND  FECHA_F ";

            Query += "BETWEEN  '" + psFechaInicio + "' and '" + psFechaFinal + "'";
            Query += "and USUARIO = '" + psUsuario + "'";
            Query += " and ESTATUS != '0'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                bandera = true;
            }
            else
            {
                bandera = false;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return bandera;
        }

        /// <summary>
        /// actualiza la tabla comisiones
        /// </summary>
        /// <param name="pObjeto"></param>
        /// <param name="psOpcion"></param>
        /// <param name="psFolio"></param>
        /// <param name="psUsuario"></param>
        /// <returns></returns>
        public static bool Autorizaciones(Comision pObjeto, string psOpcion, string psFolio, string psUsuario, string psPermisos, Boolean update = false)
        {
            bool bandera = false;
            string Query = "UPDATE crip_comision SET  ";

            if (psOpcion == dictionary.AUTORIZA)
            {
                //Query += " NO_OFICIO_DGAIPP = '" + pObjeto.Oficio_DGAIPP + "' , ";
                Query += " FECHA_AUTORIZA = '" + lsHoy + "' , ";
                Query += " OBSERVACIONES_AUT =  '" + pObjeto.Observaciones_Autoriza + "', ";

                if (psPermisos == dictionary.PERMISO_ADMINISTRADOR_LOCAL)
                {
                    Query += " OBSERVACIONES_VoBo = '" + pObjeto.Observaciones_Vobo + "', ";
                    Query += " FECHA_VoBo = '" + lsHoy + "' , ";
                    //  Query += " ESTATUS = '9', ";

                    Query += " HORA_INICIAL = '" + pObjeto.Inicio_Comision + "' , ";
                    Query += " HORA_FINAL = ' " + pObjeto.Fin_Comision + "' ,";
                    Query += " PART_PRESUPUESTAL = '" + pObjeto.Partida_Presupuestal + "' , ";
                    Query += " FECHA_I = '" + pObjeto.Fecha_Inicio + "' , ";
                    Query += " FECHA_F = '" + pObjeto.Fecha_Final + "' , ";
                    Query += " DIAS_REALES = '" + pObjeto.Dias_Reales + "' , ";

                    Query += " CLV_CLASE_AUT = '" + pObjeto.Clase_Aut + "' , ";
                    Query += " CLV_TIPO_AUT = '" + pObjeto.Tipo_Aut + "', ";
                    Query += " CLV_TRANS_AUT = '" + pObjeto.Vehiculo_Autorizado + "', ";
                    Query += " CLV_DEP_TRANS_AUT = '" + pObjeto.Ubicacion_Trans_Aut + "' , ";

                    Query += " AEROLINEA_PARTIDA = '" + pObjeto.Aerolinea_Part + "', ";
                    Query += " VUELO_PARTIDA_NUM = '" + pObjeto.Vuelo_Part_Num + "', ";
                    Query += " FECHA_VUELO_IDA = '" + pObjeto.Fecha_Vuelo_part + "', ";
                    Query += " HORA_PART = '" + pObjeto.Hora_Vuelo_Part + "', ";
                    Query += " AEROLINEA_REGRESO = '" + pObjeto.Aerolinea_Ret + "', ";
                    Query += " VUELO_RETURN_NUM = '" + pObjeto.Vuelo_Ret_Num + "',";
                    Query += " FECHA_VUELO_RETURN = '" + pObjeto.Fecha_Vuelo_Ret + "', ";
                    Query += " HORA_RET = '" + pObjeto.Hora_Vuelo_ret + "',";

                    Query += " ORIGEN_DESTINO_TER = '" + pObjeto.Origen_Terrestre + "', ";
                    Query += " ORIGEN_DESTINO_AER = '" + pObjeto.Origen_Aereo + "', ";
                    Query += " ORIGEN_DESTINO_ACU = '" + pObjeto.Origen_Dest_Acu + "',";
                    Query += " COMBUSTIBLE_AUT = '" + pObjeto.Combustible_Autorizado + "',";


                    Query += " PARTIDA_COMBUSTIBLE = '" + pObjeto.Partida_Combustible + "', ";
                    Query += " TIPO_PAGO_COMB = '" + pObjeto.Pago_Combustible + "', ";
                    Query += " VALE_COMBUST_I = '" + pObjeto.Vale_Comb_I + "', ";
                    Query += " VALE_COMBUST_F = '" + pObjeto.Vale_Comb_F + "', ";

                    Query += " PEAJE = '" + pObjeto.Peaje + "',  ";
                    Query += " PART_PEAJE = '" + pObjeto.Partida_Peaje + "',  ";
                    Query += " PASAJE = '" + pObjeto.Pasaje + "',  ";
                    Query += " PART_PASAJE = '" + pObjeto.Partida_Pasaje + "',  ";

                    Query += " ZONA_COMERCIAL = '" + pObjeto.Zona_Comercial + "',  ";
                    Query += " FORMA_PAGO_VIATICOS = '" + pObjeto.Forma_Pago_Viaticos + "', ";
                    Query += " TIPO_VIATICOS= '" + pObjeto.Tipo_Viaticos + "',";
                    Query += " TIPO_PAGO_VIATICO= '" + pObjeto.Tipo_Pago_Viatico + "', ";

                    Query += " TOTAL_VIATICOS='" + pObjeto.Total_Viaticos + "',";
                    Query += " TERRITORIO='" + pObjeto.Territorio + "',";

                    Query += " DIAS_RURAL='" + pObjeto.Dias_Rural + "',";
                    Query += " DIAS_COMERCIAL='" + pObjeto.Dias_Comercial + "',";
                    Query += " DIAS_NAVEGADOS='" + pObjeto.Dias_Navegados + "',";
                    Query += " DIAS_PAGAR='" + pObjeto.Dias_Pagar + "',";
                    Query += " DIAS_50KM='" + pObjeto.Dias_50 + "',";

                    Query += " COMBUST_EFECTIVO='" + pObjeto.Combustible_Efectivo + "',";
                    Query += " COMBUST_VALES='" + pObjeto.Combustible_Vales + "',";
                    Query += " SINGLADURAS='" + pObjeto.Singladuras + "'";

                    if ((pObjeto.Zona_Comercial == "0") & (((pObjeto.Combustible_Efectivo == dictionary.NUMERO_CERO) | (pObjeto.Combustible_Efectivo == null) | (pObjeto.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((pObjeto.Peaje == dictionary.NUMERO_CERO) | (pObjeto.Peaje == null) | (pObjeto.Peaje == dictionary.CADENA_NULA)) & ((pObjeto.Pasaje == dictionary.NUMERO_CERO) | (pObjeto.Pasaje == dictionary.CADENA_NULA) | (pObjeto.Pasaje == null))))
                    {
                        Query += " ,ESTATUS = '5' ";
                    }
                    else if ((pObjeto.Zona_Comercial == "15") & (((pObjeto.Combustible_Efectivo == dictionary.NUMERO_CERO) | (pObjeto.Combustible_Efectivo == null) | (pObjeto.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((pObjeto.Peaje == dictionary.NUMERO_CERO) | (pObjeto.Peaje == null) | (pObjeto.Peaje == dictionary.CADENA_NULA)) & ((pObjeto.Pasaje == dictionary.NUMERO_CERO) | (pObjeto.Pasaje == dictionary.CADENA_NULA) | (pObjeto.Pasaje == null))))
                    {
                        Query += " ,ESTATUS = '5' ";
                    }
                    else
                    {
                        Query += " ,ESTATUS = '9' ";
                    }

                }
                else
                {
                    Query += " ESTATUS = '8' ";

                }


            }
            else
            {
                //if (pObjeto.Oficio_DGAIPP != null)
                //{
                //    Query += " NO_OFICIO_DGAIPP = '" + pObjeto.Oficio_DGAIPP + "' , ";
                //}
                Query += " FECHA_VoBo = '" + lsHoy + "' , ";
                Query += " OBSERVACIONES_VoBo = '" + pObjeto.Observaciones_Vobo + "', ";
                //  Query += " ESTATUS = '9', ";

                Query += " HORA_INICIAL = '" + pObjeto.Inicio_Comision + "' , ";
                Query += " HORA_FINAL = ' " + pObjeto.Fin_Comision + "' ,";
                Query += " PART_PRESUPUESTAL = '" + pObjeto.Partida_Presupuestal + "' , ";
                Query += " FECHA_I = '" + pObjeto.Fecha_Inicio + "' , ";
                Query += " FECHA_F = '" + pObjeto.Fecha_Final + "' , ";
                Query += " DIAS_REALES = '" + pObjeto.Dias_Reales + "' , ";

                Query += " CLV_CLASE_AUT = '" + pObjeto.Clase_Aut + "' , ";
                Query += " CLV_TIPO_AUT = '" + pObjeto.Tipo_Aut + "', ";
                Query += " CLV_TRANS_AUT = '" + pObjeto.Vehiculo_Autorizado + "', ";
                Query += " CLV_DEP_TRANS_AUT = '" + pObjeto.Ubicacion_Trans_Aut + "' , ";

                Query += " AEROLINEA_PARTIDA = '" + pObjeto.Aerolinea_Part + "', ";
                Query += " VUELO_PARTIDA_NUM = '" + pObjeto.Vuelo_Part_Num + "', ";
                Query += " FECHA_VUELO_IDA = '" + pObjeto.Fecha_Vuelo_part + "', ";
                Query += " HORA_PART = '" + pObjeto.Hora_Vuelo_Part + "', ";
                Query += " AEROLINEA_REGRESO = '" + pObjeto.Aerolinea_Ret + "', ";
                Query += " VUELO_RETURN_NUM = '" + pObjeto.Vuelo_Ret_Num + "',";
                Query += " FECHA_VUELO_RETURN = '" + pObjeto.Fecha_Vuelo_Ret + "', ";
                Query += " HORA_RET = '" + pObjeto.Hora_Vuelo_ret + "',";

                Query += " ORIGEN_DESTINO_TER = '" + pObjeto.Origen_Terrestre + "', ";
                Query += " ORIGEN_DESTINO_AER = '" + pObjeto.Origen_Aereo + "', ";
                Query += " ORIGEN_DESTINO_ACU = '" + pObjeto.Origen_Dest_Acu + "',";
                Query += " COMBUSTIBLE_AUT = '" + pObjeto.Combustible_Autorizado + "',";


                Query += " PARTIDA_COMBUSTIBLE = '" + pObjeto.Partida_Combustible + "', ";
                Query += " TIPO_PAGO_COMB = '" + pObjeto.Pago_Combustible + "', ";
                Query += " VALE_COMBUST_I = '" + pObjeto.Vale_Comb_I + "', ";
                Query += " VALE_COMBUST_F = '" + pObjeto.Vale_Comb_F + "', ";

                Query += " PEAJE = '" + pObjeto.Peaje + "',  ";
                Query += " PART_PEAJE = '" + pObjeto.Partida_Peaje + "',  ";
                Query += " PASAJE = '" + pObjeto.Pasaje + "',  ";
                Query += " PART_PASAJE = '" + pObjeto.Partida_Pasaje + "',  ";

                Query += " ZONA_COMERCIAL = '" + pObjeto.Zona_Comercial + "',  ";
                Query += " FORMA_PAGO_VIATICOS = '" + pObjeto.Forma_Pago_Viaticos + "', ";
                Query += " TIPO_VIATICOS= '" + pObjeto.Tipo_Viaticos + "',";
                Query += " TIPO_PAGO_VIATICO= '" + pObjeto.Tipo_Pago_Viatico + "', ";

                Query += " TOTAL_VIATICOS='" + pObjeto.Total_Viaticos + "',";
                Query += " TERRITORIO='" + pObjeto.Territorio + "',";

                Query += " DIAS_RURAL='" + pObjeto.Dias_Rural + "',";
                Query += " DIAS_COMERCIAL='" + pObjeto.Dias_Comercial + "',";
                Query += " DIAS_NAVEGADOS='" + pObjeto.Dias_Navegados + "',";
                Query += " DIAS_PAGAR='" + pObjeto.Dias_Pagar + "',";
                Query += " DIAS_50KM='" + pObjeto.Dias_50 + "',";

                Query += " COMBUST_EFECTIVO='" + pObjeto.Combustible_Efectivo + "',";
                Query += " COMBUST_VALES='" + pObjeto.Combustible_Vales + "',";
                Query += " SINGLADURAS='" + pObjeto.Singladuras + "'";

                if ((pObjeto.Zona_Comercial == "0") & (((pObjeto.Combustible_Efectivo == dictionary.NUMERO_CERO)) & (pObjeto.Peaje == dictionary.NUMERO_CERO) & (pObjeto.Pasaje == dictionary.NUMERO_CERO)))
                {
                    Query += " ,ESTATUS = '5' ";
                }
                else if ((pObjeto.Zona_Comercial == "15") & (((pObjeto.Combustible_Efectivo == dictionary.NUMERO_CERO) | (pObjeto.Combustible_Efectivo == null) | (pObjeto.Combustible_Efectivo == dictionary.CADENA_NULA)) & ((pObjeto.Peaje == dictionary.NUMERO_CERO) | (pObjeto.Peaje == null) | (pObjeto.Peaje == dictionary.CADENA_NULA)) & ((pObjeto.Pasaje == dictionary.NUMERO_CERO) | (pObjeto.Pasaje == dictionary.CADENA_NULA) | (pObjeto.Pasaje == null))))
                {
                    Query += " ,ESTATUS = '5' ";
                }
                else
                {
                    Query += ", ESTATUS = '9' ";
                }


            }
            /*
                        Query += " HORA_INICIAL = '" + pObjeto.Inicio_Comision + "' , ";
                        Query += " HORA_FINAL = ' " + pObjeto.Fin_Comision + "' ,";
                        Query += " PART_PRESUPUESTAL = '" + pObjeto.Partida_Presupuestal + "' , ";
                        Query += " FECHA_I = '" + pObjeto.Fecha_Inicio + "' , ";
                        Query += " FECHA_F = '" + pObjeto.Fecha_Final + "' , ";
                        Query += " DIAS_REALES = '" + pObjeto.Dias_Reales + "' , ";

                        Query += " CLV_CLASE_AUT = '" + pObjeto.Clase_Aut + "' , ";
                        Query += " CLV_TIPO_AUT = '" + pObjeto.Tipo_Aut + "', ";
                        Query += " CLV_TRANS_AUT = '" + pObjeto.Vehiculo_Autorizado + "', ";
                        Query += " CLV_DEP_TRANS_AUT = '" + pObjeto.Ubicacion_Trans_Aut + "' , ";

                        Query += " AEROLINEA_PARTIDA = '" + pObjeto.Aerolinea_Part + "', ";
                        Query += " VUELO_PARTIDA_NUM = '" + pObjeto.Vuelo_Part_Num + "', ";
                        Query += " FECHA_VUELO_IDA = '" + pObjeto.Fecha_Vuelo_part + "', ";
                        Query += " HORA_PART = '" + pObjeto.Hora_Vuelo_Part + "', ";
                        Query += " AEROLINEA_REGRESO = '" + pObjeto.Aerolinea_Ret + "', ";
                        Query += " VUELO_RETURN_NUM = '" + pObjeto.Vuelo_Ret_Num + "',";
                        Query += " FECHA_VUELO_RETURN = '" + pObjeto.Fecha_Vuelo_Ret + "', ";
                        Query += " HORA_RET = '" + pObjeto.Hora_Vuelo_ret + "',";

                        Query += " ORIGEN_DESTINO_TER = '" + pObjeto.Origen_Terrestre + "', ";
                        Query += " ORIGEN_DESTINO_AER = '" + pObjeto.Origen_Aereo + "', ";
                        Query += " ORIGEN_DESTINO_ACU = '" + pObjeto.Origen_Dest_Acu + "',";
                        Query += " COMBUSTIBLE_AUT = '" + pObjeto.Combustible_Autorizado + "',";


                        Query += " PARTIDA_COMBUSTIBLE = '" + pObjeto.Partida_Combustible + "', ";
                        Query += " TIPO_PAGO_COMB = '" + pObjeto.Pago_Combustible + "', ";
                        Query += " VALE_COMBUST_I = '" + pObjeto.Vale_Comb_I + "', ";
                        Query += " VALE_COMBUST_F = '" + pObjeto.Vale_Comb_F + "', ";

                        Query += " PEAJE = '" + pObjeto.Peaje + "',  ";
                        Query += " PART_PEAJE = '" + pObjeto.Partida_Peaje + "',  ";
                        Query += " PASAJE = '" + pObjeto.Pasaje + "',  ";
                        Query += " PART_PASAJE = '" + pObjeto.Partida_Pasaje + "',  ";

                        Query += " ZONA_COMERCIAL = '" + pObjeto.Zona_Comercial + "',  ";
                        Query += " FORMA_PAGO_VIATICOS = '" + pObjeto.Forma_Pago_Viaticos + "', ";
                        Query += " TIPO_VIATICOS= '" + pObjeto.Tipo_Viaticos + "',";
                        Query += " TIPO_PAGO_VIATICO= '" + pObjeto.Tipo_Pago_Viatico + "', ";

                        Query += " TOTAL_VIATICOS='" + pObjeto.Total_Viaticos + "',";
                        Query += " TERRITORIO='" + pObjeto.Territorio  + "',";

                        Query += " DIAS_RURAL='" + pObjeto.Dias_Rural + "',";
                        Query += " DIAS_COMERCIAL='" + pObjeto.Dias_Comercial + "',";
                        Query += " DIAS_NAVEGADOS='" + pObjeto.Dias_Navegados  + "',";
                        Query += " DIAS_PAGAR='" + pObjeto.Dias_Pagar  + "',";

                        Query += " COMBUST_EFECTIVO='" + pObjeto.Combustible_Efectivo  + "',";
                        Query += " COMBUST_VALES='" + pObjeto.Combustible_Vales + "'";
                        */
            Query += " WHERE FOLIO = '" + psFolio + "' AND CLV_DEP = '" + pObjeto.Ubicacion + "' ";


            if (psOpcion == dictionary.AUTORIZA)
            {
                Query += "AND AUTORIZA = '" + psUsuario + "'";
                //   if (!update)
                //  {
                Query += " AND ESTATUS = '1' ";
                // }
                // else
                //{
                //   Query += " AND ESTATUS = '9' ";
                //}

            }
            else
            {
                Query += "AND VoBo = '" + psUsuario + "'";
                // if (psPermisos == Dictionary.PERMISO_ADMINISTRADOR_LOCAL) Query += " AND ESTATUS = '1' ";
                //else {
                //  if (!update)
                //{
                Query += " AND ESTATUS = '8' ";
                // }
                //else
                //{
                // Query += " AND ESTATUS = '9' ";
                //}

                //  } 

            }

            if (update)
            {
                Query += " AND USUARIO = '" + pObjeto.Comisionado + "' ";
            }
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

        public static bool Update_Status_Comision_Detalle(string psFolio, string psUbicacion, string psComisionado = "", string psOpcion = "", string psComentarios = "", string psNoOficio = "", string psAutoriza = "")
        {
            bool bandera = false;

            string query = "update crip_comision_detalle set";

            if (psOpcion != "")
            {
                if (psOpcion == dictionary.VOBO)
                {
                    query += " ESTATUS = '9', ";
                    query += " FECHA_AUTORIZA =  '" + lsHoy + "',";
                    query += "NO_OFICIO = '" + psNoOficio + "' ";
                }
            }
            else
            {
                query += " ESTATUS = '0' ";
            }

            if (psComentarios != "")
            {
                query += ",OBSERVACIONES = '" + psComentarios + "' ";
            }

            query += "    WHERE FOLIO = '" + psFolio + "'  ";
            query += "    AND UBICACION_SOLICITUD = '" + psUbicacion + "' ";

            if (psComisionado != "")
            {
                query += "    AND COMISIONADO = '" + psComisionado + "'";
            }

            if (psAutoriza != "")
            {
                query += "    AND AUTORIZA = '" + psAutoriza + "'";
            }
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

        public static bool Update_Status_ComisionDetalle(string psEstatus, string psFolio, string psArchivo, string psComisionado, string psUbicacion)
        {
            bool bandera = false;
            string query = "UPDATE crip_comision_detalle SET ESTATUS  = '" + psEstatus + "'";
            query += " WHERE NO_OFICIO = '" + psFolio + "'";
            query += " AND ARCHIVO = '" + psArchivo + "'";
            query += " AND COMISIONADO  = '" + psComisionado + "'";
            query += " AND CLV_DEP_COM = '" + psUbicacion + "'";
            query += " AND ESTATUS <> 0 ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() == 1)
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

        public static bool Update_estatus_Comision(string psEstatus, string psUsuario = "", string psFolio = "", string psDep = "", string psArchivo = "", string psObservaciones = "", bool x = false)
        {
            bool bandera = false;
            string query = "UPDATE crip_comision SET ESTATUS = '" + psEstatus + "'";
            if (psObservaciones != "")
            {
                query += ",OBSERVACIONES_VoBo = '" + psObservaciones + "',OBSERVACIONES_AUT = '" + psObservaciones + "'";
            }
            query += "  WHERE 1 = 1 ";
            if (psUsuario != "")
            {
                query += " AND USUARIO = '" + psUsuario + "'";
            }
            if (psFolio != "")
            {
                if (!x)
                {
                    query += " AND NO_OFICIO = '" + psFolio + "'";
                }
                else
                {
                    query += " AND FOLIO = '" + psFolio + "'";
                }
            }

            if (psDep != "")
            {
                query += " AND CLV_DEP_COM = '" + psDep + "'";
            }

            if (psArchivo != "")
            {
                query += "  AND ARCHIVO = '" + psArchivo + "'";
            }

            query += " AND ESTATUS <> 0 ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() == 1)
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

        public static bool Update_Estatus_Comprobacion(string psOficio, string psComisionado, string psUbicacion, string psFecha, string psImporte, string psDocumento, string psOpcion)
        {
            bool bandera = false;

            string Query = "UPDATE crip_comision_comprobacion ";
            Query += "    SET ESTATUS = '0'";
            Query += "    WHERE NO_OFICIO = '" + psOficio + "'";
            Query += " AND COMISIONADO = '" + psComisionado + "'";
            Query += " AND DEP_COMSIONADO = '" + psUbicacion + "'";
            Query += " AND FECHA_FACTURA = '" + psFecha + "'";
            Query += " AND IMPORTE =  '" + psImporte + "'";

            if (psOpcion == "2")
            {
                Query += "    AND DOCUMENTO_COMPROBACION = '" + psDocumento + "'";
                Query += "    AND TIPO_COMPROBANTE  = '" + psOpcion + "'";
            }
            else
            {
                Query += "    AND OBSERVACIONES = '" + psDocumento + "'";
                Query += "    AND TIPO_COMPROBANTE  = '" + psOpcion + "'";
            }
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

        public static bool Update_Sec_Eff(string psFolio, string psUbicacion, string psComisionado, string psSecEff)
        {
            bool bandera = false;

            string Query = "UPDATE crip_comision SET ";

            if (clsFunciones.ConvertInteger(psSecEff) > 1)
            {
                Query += "COMBUSTIBLE_AUT = '0' , VALE_COMBUST_I = '0',VALE_COMBUST_F = '0'";
            }
            else
            {
                Query += " SECEFF = '" + psSecEff + "' ";
            }

            Query += " WHERE FOLIO = '" + psFolio + "'  AND CLV_DEP= '" + psUbicacion + "'";

            Query += "AND USUARIO = '" + psComisionado + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() == 1)
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

        public static bool Update_Status(string psFolio, string psUbicacion, string psComisionado = "", string psOpcion = "", string psComentarios = "", string psFecha = "")
        {
            bool bandera = false;

            string query = "UPDATE crip_comision SET ESTATUS = '0' ";
            if (psOpcion != "")
            {
                if (psOpcion == "AUTORIZA")
                {
                    query += " , FECHA_AUTORIZA = '" + psFecha + "' ";
                    query += " , OBSERVACIONES_AUT = '" + psComentarios + "'";
                }
                else
                {
                    query += " , FECHA_VoBo = '" + psFecha + "'";
                    query += " , OBSERVACIONES_VoBo =   '" + psComentarios + "'";
                }
            }
            query += " WHERE FOLIO = '" + psFolio + "'  AND CLV_DEP= '" + psUbicacion + "'";
            if (psComisionado != "")
            {
                query += "AND USUARIO = '" + psComisionado + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() == 1)
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

        public static List<Comision> ListaComisionesPagar(string psArchivo, string psPeriodo)
        {

            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          RUTA AS RUTA , ";
            query += "          ARCHIVO AS ARCHIVO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          FECHA_VoBo AS FECHA_VoBo,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          CLV_TIPO_COM AS TIPO_COMISION,";
            query += "          ESPECIE AS PESQUERIA,";
            query += "          PRODUCTO AS PRODUCTO,";
            query += "          ACTIVIDAD_ESPECIFICA AS ACTIVIDAD_ESPECIFICA,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";

            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          SECEFF AS SECUENCIA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES  AS COMBUSTIBLE_VALES, ";
            query += "          TERRITORIO AS TERRITORIO,  ";
            query += "          SINGLADURAS  AS SINGLADURAS ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO,";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          PERIODO AS PERIODO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA ";
            query += "          FROM crip_comision";
            query += "          WHERE 1=1 ";

            query += " AND  ARCHIVO LIKE '%" + psArchivo + "%'";


            query += " AND PERIODO = '" + psPeriodo + "'";
            query += "      ORDER BY NO_OFICIO ASC";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Comision> ListaComisiones = new List<Comision>();

            while (reader.Read())
            {
                Comision objetoEntidad = new Comision();

                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_VoBo"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Pesqueria = Convert.ToString(reader["PESQUERIA"]);
                objetoEntidad.Producto = Convert.ToString(reader["PRODUCTO"]);
                objetoEntidad.Actividad = Convert.ToString(reader["ACTIVIDAD_ESPECIFICA"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);
                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);
                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);
                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);

                ListaComisiones.Add(objetoEntidad);
                objetoEntidad = null;

            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaComisiones;

        }

        public static Comision PagoComision(string psArchivo, string psPeriodo, bool psBandera = false)
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          RUTA AS RUTA , ";
            query += "          ARCHIVO AS ARCHIVO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          FECHA_VoBo AS FECHA_VoBo,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          CLV_TIPO_COM AS TIPO_COMISION,";
            query += "          ESPECIE AS PESQUERIA,";
            query += "          PRODUCTO AS PRODUCTO,";
            query += "          ACTIVIDAD_ESPECIFICA AS ACTIVIDAD_ESPECIFICA,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";

            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          SECEFF AS SECUENCIA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES  AS COMBUSTIBLE_VALES, ";
            query += "          TERRITORIO AS TERRITORIO,  ";
            query += "          SINGLADURAS  AS SINGLADURAS ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO,";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          PERIODO AS PERIODO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA ";
            query += "          FROM crip_comision";

            query += "          WHERE 1=1 ";
            if (!psBandera)
            {
                query += "           AND ARCHIVO = '" + psArchivo + "'";
            }
            else
            {
                query += " AND  ARCHIVO LIKE '%" + psArchivo + "%'";
            }

            query += " AND PERIODO = '" + psPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {
                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_VoBo"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Pesqueria = Convert.ToString(reader["PESQUERIA"]);
                objetoEntidad.Producto = Convert.ToString(reader["PRODUCTO"]);
                objetoEntidad.Actividad = Convert.ToString(reader["ACTIVIDAD_ESPECIFICA"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);
                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);
                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);
                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;

        }

        /// <summary>
        /// Metodo que extrae objeto comision para la ministracion y autorizacion
        /// </summary>
        /// <param name="psFolio"></param>
        /// <param name="psDep"></param>
        /// <param name="psComisionado"></param>
        /// <returns></returns>
        public static Comision Detalle_Comision_Reimpresion(string psFolio, string psDep, string psPeriodo, string psComisionado = "", string piEstatus = "")
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          RUTA AS RUTA , ";
            query += "          ARCHIVO AS ARCHIVO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          FECHA_VoBo AS FECHA_VoBo,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          CLV_TIPO_COM AS TIPO_COMISION,";
            query += "          ESPECIE AS PESQUERIA,";
            query += "          PRODUCTO AS PRODUCTO,";
            query += "          ACTIVIDAD_ESPECIFICA AS ACTIVIDAD_ESPECIFICA,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          SECEFF AS SECUENCIA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES  AS COMBUSTIBLE_VALES, ";
            query += "          TERRITORIO AS TERRITORIO,  ";
            query += "          SINGLADURAS  AS SINGLADURAS ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO,";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          PERIODO AS PERIODO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA ,";
            query += "          NO_OFICIO_DGAIPP ,";
            query += "          NO_OFICIO_DG,";
            query += "          NO_OFICIO_AMPLIACION ,";
            query += "          ARCHIVO_AMPLIACION";

            query += "          FROM crip_comision WHERE NO_OFICIO = '" + psFolio + "' ";

            //if (psComisionado == "")
            // {
            //    query += "      AND CLV_DEP_AUTORIZA = '" + psDep + "' ";
            // }
            // else
            // {
            query += "      AND CLV_DEP_COM = '" + psDep + "' ";
            //}
            query += "  AND PERIODO = '" + psPeriodo + "'";

            if (psComisionado != "")
            {
                //Query += "          AND ESTATUS = '9'";
                query += "      AND USUARIO = '" + psComisionado + "'";

            }

            if (piEstatus != "")
            {

                query += "       AND ESTATUS = '" + piEstatus + "'";


            }
            /*
            if (piEstatus != "")
            {      
             * query += "          AND ESTATUS = '" + piEstatus + "'";
            }
            else
            {
                query += "          AND ESTATUS = '1'";
            }*/
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {
                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_VoBo"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Pesqueria = Convert.ToString(reader["PESQUERIA"]);
                objetoEntidad.Producto = Convert.ToString(reader["PRODUCTO"]);
                objetoEntidad.Actividad = Convert.ToString(reader["ACTIVIDAD_ESPECIFICA"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);
                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);
                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);
                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
                objetoEntidad.Oficio_DGAIPP = Convert.ToString(reader["NO_OFICIO_DGAIPP"]);

                objetoEntidad.Oficio_DG = Convert.ToString(reader["NO_OFICIO_DG"]);
                objetoEntidad.Oficio_Ampliacion = Convert.ToString(reader["NO_OFICIO_AMPLIACION"]);
                objetoEntidad.Archivo_Ampliacion = Convert.ToString(reader["ARCHIVO_AMPLIACION"]);

            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }


        public static Comision Obten_DetalleComision(string psFolio, string psDep, string psComisionado, string pssEtatus)
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          ARCHIVO AS ARCHIVO, ";
            query += "          RUTA AS RUTA, ";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          SECEFF AS SECUENCIA ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES AS COMBUSTIBLE_VALES, ";
            query += "          SINGLADURAS AS SINGLADURAS, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO, ";
            query += "          PERIODO AS PERIODO,    ";
            query += "          FECHA_VOBO AS FECHA_VOBO ,   ";
            query += "          TERRITORIO AS TERRITORIO,";
            query += "          NO_OFICIO_AMPLIACION ,";
            query += "          ARCHIVO_AMPLIACION,";
            query += "          NO_OFICIO_DGAIPP";
            query += "          FROM crip_comision WHERE ";
            query += "          ARCHIVO = '" + psFolio + "'";
            query += "          AND ESTATUS != '0' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {

                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);
                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);
                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);
                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_VOBO"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                objetoEntidad.Oficio_Ampliacion = Convert.ToString(reader["NO_OFICIO_AMPLIACION"]);
                objetoEntidad.Archivo_Ampliacion = Convert.ToString(reader["ARCHIVO_AMPLIACION"]);
                objetoEntidad.Oficio_DGAIPP = Convert.ToString(reader["NO_OFICIO_DGAIPP"]);

            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }
        /// <summary>
        /// Metodo que extrae detalle de la comision especifica segun parametros para combrobaciones
        /// </summary>
        /// <param name="psFolio"></param>
        /// <param name="psDep"></param>
        /// <param name="psComisionado"></param>
        /// <returns></returns>
        public static Comision Obten_Detalle_Comision(string psFolio, string psDep, string psComisionado, string pssEtatus)
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          ARCHIVO AS ARCHIVO, ";
            query += "          RUTA AS RUTA, ";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          SECEFF AS SECUENCIA ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES AS COMBUSTIBLE_VALES, ";
            query += "          SINGLADURAS AS SINGLADURAS, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO, ";
            query += "          PERIODO AS PERIODO,    ";
            query += "          FECHA_VOBO AS FECHA_VOBO ,   ";
            query += "          TERRITORIO AS TERRITORIO, ";
            query += "          NO_OFICIO_DGAIPP, ";
            query += "          NO_OFICIO_DG, ";
            query += "          NO_OFICIO_AMPLIACION, ";
            query += "          CLV_TIPO_COM, ";
            query += "          NO_OFICIO_DGAIPP,";
            query += "          ARCHIVO_AMPLIACION ";
            query += "          FROM crip_comision WHERE ";
            query += "         ARCHIVO = '" + psFolio + "'"; // AND CLV_DEP_COM = '" + psDep + "' ";
            //  NO_OFICIO = '" + psFolio + "' AND CLV_DEP_COM = '" + psDep + "' ";

            if (pssEtatus != dictionary.NUMERO_CERO)
            {
                query += "          AND ESTATUS = '" + pssEtatus + "'";
            }

            query += "          AND USUARIO = '" + psComisionado + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {

                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Oficio_DGAIPP = Convert.ToString(reader["NO_OFICIO_DGAIPP"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);

                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);


                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);

                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);

                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);


                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_VOBO"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Oficio_DGAIPP = Convert.ToString(reader["NO_OFICIO_DGAIPP"]);
                objetoEntidad.Oficio_DG = Convert.ToString(reader["NO_OFICIO_DG"]);
                objetoEntidad.Oficio_Ampliacion = Convert.ToString(reader["NO_OFICIO_AMPLIACION"]);
                objetoEntidad.Archivo_Ampliacion = Convert.ToString(reader["ARCHIVO_AMPLIACION"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["CLV_TIPO_COM"]);

                if (psComisionado != "")
                {
                    objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                    objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                    objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                    objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                    objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);

                }
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }

        public static Comision Detalle_Comision(string psFolio, string psDep, string psComisionado = "", string piEstatus = "")
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          NO_OFICIO_DGAIPP , ";
            query += "          RUTA AS RUTA , ";
            query += "          ARCHIVO AS ARCHIVO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          CLV_TIPO_COM AS TIPO_COMISION,";
            query += "          ESPECIE AS PESQUERIA,";
            query += "          PRODUCTO AS PRODUCTO,";
            query += "          ACTIVIDAD_ESPECIFICA AS ACTIVIDAD_ESPECIFICA,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          SECEFF AS SECUENCIA, ";
            query += "          ESTATUS AS ESTATUS ,";
            query += "          TERRITORIO AS TERRITORIO ,";
            query += "          DIAS_PAGAR AS  DIAS_PAGAR,";
            query += "          DIAS_RURAL AS  DIAS_RURAL,";
            query += "          DIAS_COMERCIAL AS  DIAS_COMERCIAL,";
            query += "          DIAS_NAVEGADOS AS  DIAS_NAVEGADOS,";
            query += "          COMBUST_EFECTIVO AS  COMBUSTIBLE_EFECTIVO,";
            query += "          COMBUST_VALES AS  COMBUSTIBLE_VALES,";
            query += "          TERRITORIO AS  TERRITORIO,";
            query += "          SINGLADURAS AS  SINGLADURAS,";
            query += "          PERIODO AS PERIODO,  ";
            query += "          PAIS AS PAIS,  ";
            query += "          CLV_ESTADO AS ESTADO,  ";

            query += "  CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA";

            if (psComisionado != "")
            {
                query += " ,USUARIO AS USUARIO ,";
                query += "  AUTORIZA AS AUTORIZA, ";
                query += "  VoBo AS VOBO, ";
                query += "  CLV_DEP_COM AS UBICACION_COMISIONADO";
                //    query += "  CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA";
            }

            query += "          FROM crip_comision WHERE FOLIO = '" + psFolio + "' AND CLV_DEP = '" + psDep + "' ";

            if (psComisionado != "")
            {
                //Query += "          AND ESTATUS = '9'";
                query += "          AND USUARIO = '" + psComisionado + "'";

            }

            if (piEstatus != "")
            {
                if (piEstatus == "0")
                {
                }
                else
                {
                    query += "          AND ESTATUS = '" + piEstatus + "'";
                }

            }
            else
            {
                query += "          AND ESTATUS = '1'";
            }
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();


            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {

                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Oficio_DGAIPP= Convert.ToString(reader["NO_OFICIO_DGAIPP"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);

                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);

                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);

                objetoEntidad.Pesqueria = Convert.ToString(reader["PESQUERIA"]);
                objetoEntidad.Producto = Convert.ToString(reader["PRODUCTO"]);
                objetoEntidad.Actividad = Convert.ToString(reader["ACTIVIDAD_ESPECIFICA"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);

                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);

                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);


                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);

                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);

                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);

                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
                
                objetoEntidad.Pais = Convert.ToString(reader["PAIS"]);
                objetoEntidad.Estado  = Convert.ToString(reader["ESTADO"]);

                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);

                if (psComisionado != "")
                {
                    objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                    objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                    objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                    objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);

                }
            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }

        public static Comision DetalleComision_Pagos(string psFolio, string psDep, string psComisionado = "", bool psUsuarioPagador = false)
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          ARCHIVO AS ARCHIVO, ";
            query += "          RUTA AS RUTA, ";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          SECEFF AS SECUENCIA ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES AS COMBUSTIBLE_VALES, ";
            query += "          SINGLADURAS AS SINGLADURAS, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO, ";
            query += "          PERIODO AS PERIODO,";
            query += "          CLV_TIPO_COM  AS TIPO_COMISION,";
            query += "          FECHA_VoBo  AS FECHA_MINISTRACION,";
            query += "          TERRITORIO  AS TERRITORIO";

            query += "          FROM crip_comision WHERE ";
            query += "         ARCHIVO = '" + psFolio + "' ";

            if (psUsuarioPagador)
            {
                query += "         AND CLV_DEP_COM = '" + psDep + "' ";
            }
            else
            {
                query += "AND CLV_DEP_PROY = '" + psDep + "' ";
            }
            //  NO_OFICIO = '" + psFolio + "' AND CLV_DEP_COM = '" + psDep + "' ";
            query += "          AND ESTATUS != '0'";
            query += "          AND USUARIO = '" + psComisionado + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {

                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);

                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);


                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);

                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);

                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);

                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);

                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);

                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_MINISTRACION"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            reader.Close();
            return objetoEntidad;
        }

        public static List<Entidad> Obtiene_Detalles_ComisionUsuarioSolicitado(string pArchivo, string pPeriodo, string pUsuario)
        {
            string query = "";
            query = "SELECT FOLIO, COMBUST_EFECTIVO, PEAJE, PASAJE, SINGLADURAS, TOTAL_VIATICOS   FROM crip_comision ";
            query += " WHERE ARCHIVO='" + pArchivo + "' AND PERIODO='" + pPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);

            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Entidad> ListGrid = new List<Entidad>();


            double ImporteViaticos = 0;
            double ImportePeaje = 0;
            double ImporteCombustible = 0;
            double ImportePasaje = 0;
            double ImporteReintegros = 0;
            double Importesingladura = 0;
            double ImporteNoFiscales = 0;
            double TotalImportes = 0;

            while (reader.Read())
            {

                ImporteViaticos = ImporteViaticos + Convert.ToDouble(reader["TOTAL_VIATICOS"]);
                ImportePeaje = ImportePeaje + Convert.ToDouble(reader["PEAJE"]);
                ImporteCombustible = ImporteCombustible + Convert.ToDouble(reader["COMBUST_EFECTIVO"]);
                ImportePasaje = ImportePasaje + Convert.ToDouble(reader["PASAJE"]);
                Importesingladura = Importesingladura + Convert.ToDouble(reader["SINGLADURAS"]);

            }
            reader.Close();

            Entidad oEntidad = new Entidad();
            if (ImporteViaticos > 0)
            {
                oEntidad.Codigo = "VIATICOS";
                oEntidad.Descripcion = ImporteViaticos.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                ListGrid.Add(oEntidad);
            }
            if (ImportePeaje > 0)
            {
                oEntidad = new Entidad();
                oEntidad.Codigo = "PEAJE";
                oEntidad.Descripcion = ImportePeaje.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                ListGrid.Add(oEntidad);
            }
            if (ImporteCombustible > 0)
            {
                oEntidad = new Entidad();
                oEntidad.Codigo = "COMBUSTIBLE";
                oEntidad.Descripcion = ImporteCombustible.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                ListGrid.Add(oEntidad);
            }
            if (ImportePasaje > 0)
            {
                oEntidad = new Entidad();
                oEntidad.Codigo = "PASAJE";
                oEntidad.Descripcion = ImportePasaje.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                ListGrid.Add(oEntidad);
            }

            if (Importesingladura > 0)
            {
                oEntidad = new Entidad();
                oEntidad.Codigo = "SINGLADURA";
                oEntidad.Descripcion = Importesingladura.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                ListGrid.Add(oEntidad);
            }

            TotalImportes = ImporteViaticos + ImportePeaje + ImporteCombustible + ImportePasaje + ImporteReintegros + Importesingladura + ImporteNoFiscales;

            if (TotalImportes > 0)
            {
                oEntidad = new Entidad();
                oEntidad.Codigo = "TOTAL";
                oEntidad.Descripcion = TotalImportes.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
                ListGrid.Add(oEntidad);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListGrid;
        }

        //Modificacion pedro
        public static List<Comision> Regresa_ListComision(string psUsuario, string psPeriodo, string psEstatus = "")
        {

            string Query = "";
            Query += " SELECT DISTINCT NO_OFICIO, ARCHIVO AS OFICIO,  LUGAR AS DESTINO, FECHA_I AS FECHAINICIO, ";
            Query += " TOTAL_VIATICOS , PEAJE , COMBUST_EFECTIVO , PASAJE,SINGLADURAS, ";
            Query += " ESTATUS AS ESTATUS, FECHA_F AS FECHAFINAL, ARCHIVO_AMPLIACION AS ARCHIAMP, FORMA_PAGO_VIATICOS ";
            Query += " FROM crip_comision WHERE USUARIO = '" + psUsuario + "' AND PERIODO = '" + psPeriodo + "' AND NO_OFICIO != 0 ";


            if (psEstatus != "")
            {
                Query += " AND ESTATUS = '" + psEstatus + "'";
            }
            Query += " ORDER BY NO_OFICIO ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();


            List<Comision> ListaComision = new List<Comision>();

            while (Reader.Read())
            {
                double TOTAL = 0;
                Comision objComision = new Comision();

                TOTAL = Convert.ToDouble(Reader["TOTAL_VIATICOS"]) + Convert.ToDouble(Reader["COMBUST_EFECTIVO"]) + Convert.ToDouble(Reader["PEAJE"]) + Convert.ToDouble(Reader["PASAJE"]) + Convert.ToDouble(Reader["SINGLADURAS"]);
                DateTime fechaI = Convert.ToDateTime(Reader["FECHAINICIO"]);
                objComision.Fecha_Inicio = fechaI.ToString("dd-MM-yyyy");
                DateTime fechaF = Convert.ToDateTime(Reader["FECHAFINAL"]);
                objComision.Fecha_Final = fechaF.ToString("dd-MM-yyyy");
                objComision.Archivo = Convert.ToString(Reader["OFICIO"]);
                objComision.Total_Viaticos = TOTAL.ToString();
                if (Convert.ToString(Reader["ARCHIAMP"]) == "")
                {
                    objComision.Archivo_Ampliacion = "0";
                }
                else
                {
                    objComision.Archivo_Ampliacion = Convert.ToString(Reader["ARCHIAMP"]);
                }
                objComision.Estatus = Convert.ToString(Reader["ESTATUS"]);
                objComision.Lugar = Convert.ToString(Reader["DESTINO"]);
                objComision.Tipo_Pago_Viatico = Convert.ToString(Reader["FORMA_PAGO_VIATICOS"]);

                ListaComision.Add(objComision);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListaComision;
        }

        public static Entidad Obten_DetalleArchivo(string psUsuario, string psPeriodo, string psArchivo)
        {
            string Query = "";
            Query += " SELECT DISTINCT FECHA_F AS FECHAFINAL, ";
            Query += " TOTAL_VIATICOS , PEAJE , COMBUST_EFECTIVO , PASAJE,SINGLADURAS ";
            Query += " FROM crip_comision WHERE USUARIO = '" + psUsuario + "' AND PERIODO = '" + psPeriodo + "' AND archivo = '" + psArchivo + "' ";
            Query += " AND ESTATUS != '0'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad = new Entidad();
            if (Reader.Read())
            {
                double TOTAL = 0;
                TOTAL = Convert.ToDouble(Reader["TOTAL_VIATICOS"]) + Convert.ToDouble(Reader["COMBUST_EFECTIVO"]) + Convert.ToDouble(Reader["PEAJE"]) + Convert.ToDouble(Reader["PASAJE"]) + Convert.ToDouble(Reader["SINGLADURAS"]);
                objetoEntidad.Codigo = TOTAL.ToString();
                DateTime fechaF = Convert.ToDateTime(Reader["FECHAFINAL"]);
                objetoEntidad.Descripcion = fechaF.ToString("dd-MM-yyyy");

            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }

        public static string Comisiones_SinComprobar(string psUsuario, string psPeriodo)
        {
            int YearAnt = Convert.ToInt16(psPeriodo) - 1;
            string Resultado = "";
            string Query = " SELECT COUNT(ARCHIVO) AS CONTADOR ";
            Query += " FROM crip_comision ";
            Query += " WHERE USUARIO ='" + psUsuario + "' AND ESTATUS= '9'";
            Query += " AND PERIODO IN ( '" + psPeriodo + "', '"+ YearAnt + "')";
            Query += " AND FORMA_PAGO_VIATICOS = '2'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["CONTADOR"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }

        public static string Solicitudes_Registradas(string psUsuario, string psFecha)
        {
            string Resultado = "";
            string Query = " SELECT COUNT(*) AS CONTADOR ";
            Query += " FROM crip_comision ";
            Query += " WHERE USUARIO ='" + psUsuario + "'";
            Query += " AND FECHA_SOL= '" + psFecha + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["CONTADOR"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }
        public static string Obtiene_PeriodoComision(string psArchivo)
        {
            string Resultado = "";
            string Query = " SELECT DISTINCT PERIODO FROM crip_comision WHERE ARCHIVO='" + psArchivo + "' ";



            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["PERIODO"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }

        //metodo que trae solo de la que autoriza
        public static Comision Detalle_Comision_Reimpresion1(string psFolio, string psDep, string psPeriodo)
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          RUTA AS RUTA , ";
            query += "          ARCHIVO AS ARCHIVO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          FECHA_VoBo AS FECHA_VoBo,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          CLV_TIPO_COM AS TIPO_COMISION,";
            query += "          ESPECIE AS PESQUERIA,";
            query += "          PRODUCTO AS PRODUCTO,";
            query += "          ACTIVIDAD_ESPECIFICA AS ACTIVIDAD_ESPECIFICA,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          SECEFF AS SECUENCIA, ";
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES  AS COMBUSTIBLE_VALES, ";
            query += "          TERRITORIO AS TERRITORIO,  ";
            query += "          SINGLADURAS  AS SINGLADURAS ";
            query += "          ,USUARIO AS USUARIO ,";
            query += "          AUTORIZA AS AUTORIZA, ";
            query += "          VoBo AS VOBO, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO,";
            query += "          ESTATUS AS ESTATUS, ";
            query += "          PERIODO AS PERIODO , ";
            query += "          CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA ,";
            query += "          NO_OFICIO_DGAIPP ,";
            query += "          NO_OFICIO_DG,";
            query += "          NO_OFICIO_AMPLIACION ,";
            query += "          ARCHIVO_AMPLIACION";
            query += "          FROM crip_comision WHERE NO_OFICIO = '" + psFolio + "' ";
            query += "      AND CLV_DEP_AUTORIZA = '" + psDep + "' ";
            //}
            query += "  AND PERIODO = '" + psPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {
                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);
                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Fecha_Vobo = Convert.ToString(reader["FECHA_VoBo"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);
                objetoEntidad.Pesqueria = Convert.ToString(reader["PESQUERIA"]);
                objetoEntidad.Producto = Convert.ToString(reader["PRODUCTO"]);
                objetoEntidad.Actividad = Convert.ToString(reader["ACTIVIDAD_ESPECIFICA"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);
                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);
                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);
                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);
                objetoEntidad.Oficio_DGAIPP = Convert.ToString(reader["NO_OFICIO_DGAIPP"]);

                objetoEntidad.Oficio_DG = Convert.ToString(reader["NO_OFICIO_DG"]);
                objetoEntidad.Oficio_Ampliacion = Convert.ToString(reader["NO_OFICIO_AMPLIACION"]);
                objetoEntidad.Archivo_Ampliacion = Convert.ToString(reader["ARCHIVO_AMPLIACION"]);

            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }
        
    }
}
