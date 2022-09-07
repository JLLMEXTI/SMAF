/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosComisionDetalles.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		febrero 2016
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
 * */
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
namespace InapescaWeb.DAL
{
    public class MngDatosComisionDetalle
    {
        static clsDictionary Dictionary = new clsDictionary();

        static string Year = DateTime.Today.Year.ToString();

        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static string Otorgado_por_Proyecto(string psProyecto, string psUbicacion, string psInicio, string psFin)
        {
            string resultado = "";
            string Query = "  SELECT SUM(TOTAL_VIATICOS + COMBUSTIBLE + PEAJE + PASAJE ) AS TOTAL ";
            Query += " FROM vw_comisiones ";
            Query += " WHERE 1 = 1 ";
            Query += " AND PROYECTO = '" + psProyecto + "'";
            Query += " AND UBICACION_PROYECTO = '" + psUbicacion + "'";
            Query += " AND ((INICIO BETWEEN '" + psInicio + "' AND '" + psFin + "') OR ( FINAL BETWEEN '" + psInicio + "' AND '" + psFin + "' ))";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["TOTAL"]);
            }

            if (resultado == "")
            {
                resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return resultado;
        }

        public static ComisionDetalle Obtiene_detalle(string psarchivo, string psUsuario, string psDep, string psPeriodo)
        {
            string Query = "select * from crip_comision_detalle ";
            Query += "  WHERE ARCHIVO = '" + psarchivo + "'";
            Query += " AND COMISIONADO = '" + psUsuario + "'";
            Query += " AND CLV_DEP_COM ='" + psDep + "'";
            Query += "     and periodo = '" + psPeriodo + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();


            ComisionDetalle oComision = new ComisionDetalle();
            if (Reader.Read())
            {
                oComision.Oficio = Convert.ToString(Reader["NO_OFICIO"]);
                oComision.Archivo = Convert.ToString(Reader["ARCHIVO"]);
                oComision.Tipo = Convert.ToString(Reader["CLV_TIPO_COM"]);
                oComision.Inicio = Convert.ToString(Reader["FECHA_INICIO"]);
                oComision.Final = Convert.ToString(Reader["FECHA_FINAL"]);
                oComision.Proyecto = Convert.ToString(Reader["PROYECTO"]);
                oComision.Ubicacion_Proyecto = Convert.ToString(Reader["DEP_PROYECTO"]);
                oComision.Forma_Pago = Convert.ToString(Reader["FORMA_PAGO_VIATICOS"]);
                oComision.Viaticos = Convert.ToString(Reader["VIATICOS_PAGADOS"]);
                oComision.Partida_Viaticos = Convert.ToString(Reader["PARTIDA"]);
                oComision.Combustible = Convert.ToString(Reader["COMBUSTIBLE_PAGADO"]);
                oComision.Partida_Combustible = Convert.ToString(Reader["PARTIDA_COMB"]);
                oComision.Peaje = Convert.ToString(Reader["PEAJE_PAGADO"]);
                oComision.Partida_Peaje = Convert.ToString(Reader["PARTIDA_PEAJE"]);
                oComision.Pasaje = Convert.ToString(Reader["PASAJE_PAGADO"]);
                oComision.Partida_Pasaje = Convert.ToString(Reader["PARTIDA_PASAJE"]);
                oComision.EstatuS = Convert.ToString(Reader["ESTATUS"]);
                oComision.Financieros = Convert.ToString(Reader["FINANCIEROS"]);
                oComision.Observaciones = Convert.ToString(Reader["OBSERVACIONES"]);
                oComision.Anio = Convert.ToString(Reader["PERIODO"]);
                oComision.Singladuras = Convert.ToString(Reader["SINGLADURAS_PAGADAS"]);
                oComision.Partida_Singladuras = Convert.ToString(Reader["PARTIDA_SINGLADURAS"]);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );

            return oComision;
        }

        public static List<ComisionDetalle> ListaFiltros(string psTipo, string psEstatus, string psFinancieros, string psAdscripcion, string psUsuario, string psInicio, string psFin)
        {
            string Query = "";

            Query += "  SELECT * FROM vw_comisiones ";
            Query += "  WHERE 1 = 1 ";

            if (psTipo != "")
            {
                Query += "  AND FORMA_PAGO_VIATICOS = '" + psTipo + "' ";
            }

            if (psEstatus != "")
            {
                Query += "  AND ESTATUS = '" + psEstatus + "' ";
            }

            if (psFinancieros != "")
            {
                Query += " AND FINANCIEROS = '" + psFinancieros + "' ";
            }

            if (psAdscripcion != "")
            {
                Query += " AND UBICACION_COMISIONADO = '" + psAdscripcion + "' ";
            }

            if (psUsuario != "")
            {
                Query += " AND COMISIONADO = '" + psUsuario + "'";
            }

            Query += " AND ((INICIO BETWEEN '" + psInicio + "' AND '" + psFin + "') OR ( FINAL BETWEEN '" + psInicio + "' AND '" + psFin + "' ))";

            Query += " AND ESTATUS != '0'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<ComisionDetalle> ListaEntidad = new List<ComisionDetalle>();

            while (Reader.Read())
            {
                ComisionDetalle objGrid = new ComisionDetalle();

                objGrid.Dias_Reales = Convert.ToString(Reader["DIAS"]);
                objGrid.Inicio = Convert.ToString(Reader["INICIO"]);
                objGrid.Final = Convert.ToString(Reader["FINAL"]);
                objGrid.Viaticos = Convert.ToString(Reader["TOTAL_VIATICOS"]);
                objGrid.Combustible = Convert.ToString(Reader["COMBUSTIBLE"]);
                objGrid.Peaje = Convert.ToString(Reader["PEAJE"]);
                objGrid.Pasaje = Convert.ToString(Reader["PASAJE"]);
                objGrid.EstatuS = Convert.ToString(Reader["ESTATUS"]);

                objGrid.Actividad = Convert.ToString(Reader["ACTIVIDAD"]);
                objGrid.Especie = Convert.ToString(Reader["ESPECIE"]);
                objGrid.Producto = Convert.ToString(Reader["PRODUCTO"]);
                objGrid.Lugar = Convert.ToString(Reader["LUGAR"]);
                objGrid.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
                objGrid.Comisionado = Convert.ToString(Reader["COMISIONADO"]);
                objGrid.Ubicacion_Comisionado = Convert.ToString(Reader["UBICACION_COMISIONADO"]);
                objGrid.Financieros = Convert.ToString(Reader["FINANCIEROS"]);
                objGrid.Tipo = Convert.ToString(Reader["FORMA_PAGO_VIATICOS"]);
                objGrid.Archivo = Convert.ToString(Reader["ARCHIVO"]);
                ListaEntidad.Add(objGrid);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);



            return ListaEntidad;
        }

        public static List<ComisionComparativo> ListaFiltros(string psPeriodo, string psAdscripcion, string psMes = "",string psUbicacionPrograma= "", string psPrograma = "", string psUsuario = "", string psTipoViaticos = "", string psEstatus = "", string psFinancieros = "", bool pbIsDireccion = false)
        {
            string query = "";

            query = "    select `a`.`CLV_TIPO_COM` AS `TIPO_COMISION`, ";
            query += "   `a`.`NO_OFICIO` AS `OFICIO`, ";
            query += "   `a`.`ARCHIVO` AS `ARCHIVO`, ";

            query += "   `a`.`CLV_PROY` AS `PROYECTO`, ";
            query += "   `a`.`CLV_DEP_PROY` AS `UBICACION_PROYECTO`, ";

            query += "   `b`.`COMISIONADO` AS `COMISIONADO`,";
            query += "   `b`.`CLV_DEP_COM` AS `UBICACION_COMISIONADO`, ";
            query += "   `a`.`LUGAR` AS `LUGAR`,";
            query += "   `a`.`FECHA_I` AS `INICIO`,";
            query += "   `a`.`FECHA_F` AS `FINAL`, ";
            query += "   `a`.`TOTAL_VIATICOS` AS `TOTAL_VIATICOS`,";
            query += "   `b`.`VIATICOS_PAGADOS` AS `VIATICOS_PAGADOS`, ";
            query += "   `b`.`FECHA_PAGO_VIATICOS` AS `FECHA_PAGO_VIATICOS`,";
            query += "   `a`.`SINGLADURAS` AS `SINGLADURAS`,";
            query += "   `b`.`SINGLADURAS_PAGADAS` AS `SINGLADURAS_PAGADAS`,";
            query += "   `b`.`FECHA_PAGO_SINGLADURAS` AS `FECHA_PAGO_SINGLADURAS`,";
            query += "   `a`.`COMBUST_EFECTIVO` AS `COMBUSTIBLE`,";
            query += "   `b`.`COMBUSTIBLE_PAGADO` AS `COMBUSTIBLE_PAGADO`,";
            query += "   `b`.`FECHA_PAGO_COMB` AS `FECHA_PAGO_COMBUSTIBLE`,";
            query += "   `a`.`PEAJE` AS `PEAJE`,";
            query += "   `b`.`PEAJE_PAGADO` AS `PEAJE_PAGADO`,";
            query += "   `b`.`FECHA_PAGO_PEAJE` AS `FECHA_PAGO_PEAJE`,";
            query += "   `a`.`PASAJE` AS `PASAJE`,";
            query += "   `b`.`PASAJE_PAGADO` AS `PASAJE_PAGADO`,";
            query += "   `b`.`FECHA_PAGO_PASAJE` AS `FECHA_PAGO_PASAJE`";
            query += "     from (`crip_comision` `a` join `crip_comision_detalle` `b`) ";
            query += "    where (";
            query += "    (1 = 1) ";
            query += " and (`a`.`ESTATUS` <> '0') ";
            query += " and (`a`.`ARCHIVO` <> '') ";
            query += " and (`a`.`NO_OFICIO` <> '0')  ";
            query += " and (`a`.`NO_OFICIO` = `b`.`NO_OFICIO`) ";
            query += " and (`a`.`USUARIO` = `b`.`COMISIONADO`) ";
            query += " and (`a`.`CLV_DEP_COM` = `b`.`CLV_DEP_COM`) ";

            query += " and b.PERIODO = '" + psPeriodo + "'";

            if (psMes != "")
            {
                double mes = clsFunciones.Convert_Double(psMes) - 1;
                switch (mes.ToString())
                {
                    case "1":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-01-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-01-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-01-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-01-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-01-31')";
                        query += " )";
                        break;

                    case "2":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-02-01' and '" + psPeriodo + "-02-29') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-02-01' and '" + psPeriodo + "-02-29') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-02-01' and '" + psPeriodo + "-02-29')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-02-01' and '" + psPeriodo + "-02-29')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-02-01' and '" + psPeriodo + "-02-29')";
                        query += " )";
                        break;

                    case "3":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-03-01' and '" + psPeriodo + "-03-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-03-01' and '" + psPeriodo + "-03-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-03-01' and '" + psPeriodo + "-03-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-03-01' and '" + psPeriodo + "-03-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-03-01' and '" + psPeriodo + "-03-31')";
                        query += " )";
                        break;


                    case "4":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-04-01' and '" + psPeriodo + "-04-30') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-04-01' and '" + psPeriodo + "-04-30') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-04-01' and '" + psPeriodo + "-04-30')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-04-01' and '" + psPeriodo + "-04-30')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-04-01' and '" + psPeriodo + "-04-30')";
                        query += " )";
                        break;


                    case "5":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-05-01' and '" + psPeriodo + "-05-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-05-01' and '" + psPeriodo + "-05-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-05-01' and '" + psPeriodo + "-05-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-05-01' and '" + psPeriodo + "-05-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-05-01' and '" + psPeriodo + "-05-31')";
                        query += " )";
                        break;


                    case "6":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "06-01' and '" + psPeriodo + "-06-30') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-06-01' and '" + psPeriodo + "-06-30') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-06-01' and '" + psPeriodo + "-06-30')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-06-01' and '" + psPeriodo + "-06-30')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-06-01' and '" + psPeriodo + "-06-30')";
                        query += " )";
                        break;


                    case "7":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-07-01' and '" + psPeriodo + "-07-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-07-01' and '" + psPeriodo + "-07-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-07-01' and '" + psPeriodo + "-07-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-07-01' and '" + psPeriodo + "-07-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-07-01' and '" + psPeriodo + "-07-31')";
                        query += " )";
                        break;

                    case "8":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-08-01' and '" + psPeriodo + "-08-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-08-01' and '" + psPeriodo + "-08-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-08-01' and '" + psPeriodo + "-08-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-08-01' and '" + psPeriodo + "-08-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-08-01' and '" + psPeriodo + "-08-31')";
                        query += " )";
                        break;

                    case "9":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "09-01' and '" + psPeriodo + "-09-30') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-09-01' and '" + psPeriodo + "-09-30') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-09-01' and '" + psPeriodo + "-09-30')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-09-01' and '" + psPeriodo + "-09-30')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-09-01' and '" + psPeriodo + "-09-30')";
                        query += " )";
                        break;

                    case "10":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-10-01' and '" + psPeriodo + "-10-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-10-01' and '" + psPeriodo + "-10-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-10-01' and '" + psPeriodo + "-10-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-10-01' and '" + psPeriodo + "-10-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-10-01' and '" + psPeriodo + "-10-31')";
                        query += " )";
                        break;

                    case "11":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "11-01' and '" + psPeriodo + "-11-30') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-11-01' and '" + psPeriodo + "-11-30') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-11-01' and '" + psPeriodo + "-11-30')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-11-01' and '" + psPeriodo + "-11-30')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-11-01' and '" + psPeriodo + "-11-30')";
                        query += " )";
                        break;

                    case "12":
                        query += "   and ( ";
                        query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-12-01' and '" + psPeriodo + "-12-31') ";
                        query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-12-01' and '" + psPeriodo + "-12-31') ";
                        query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-12-01' and '" + psPeriodo + "-12-31')";
                        query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-12-01' and '" + psPeriodo + "-12-31')";
                        query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-12-01' and '" + psPeriodo + "-12-31')";
                        query += " )";
                        break;

                }

            }
            else
            {
           /*     query += "   and ( ";
                query += " ( b.FECHA_PAGO_VIATICOS BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-12-31') ";
                query += "     or ( b.FECHA_PAGO_SINGLADURAS BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-12-31') ";
                query += " or ( b.FECHA_PAGO_PEAJE BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-12-31')";
                query += "   or ( b.FECHA_PAGO_PASAJE BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-12-31')";
                query += "  or ( b.FECHA_PAGO_COMB BETWEEN '" + psPeriodo + "-01-01' and '" + psPeriodo + "-12-31')";
                query += " )";
            * */
            }

            if (psAdscripcion != "")
            {
                query += " and a.CLV_DEP_COM = '" + psAdscripcion + "'";
            }


            if (psUbicacionPrograma != "")
            {
                query += " and a.CLV_DEP_PROY  = '" + psUbicacionPrograma + "' ";
            }

            if (psPrograma != "")
            {
                query += "  and b.PROYECTO  = '" + psPrograma + "'";
            }



            if (psUsuario != "")
            {
                query += "  and b.COMISIONADO = '" + psUsuario + "'";
            }

            if (psTipoViaticos != "")
            {
                query += "           and b.FORMA_PAGO_VIATICOS = '" + psTipoViaticos + "'";
            }

            if (psEstatus != "")
            {
                query += " and a.ESTATUS =  '" + psEstatus + "' ";
            }

            if (psFinancieros != "")
            {
                query += " and b.FINANCIEROS = '" + psFinancieros + "'";
            }

            query += " ) ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<ComisionComparativo> ListComparativo = new List<ComisionComparativo>();

            while (Reader.Read())
            {
                ComisionComparativo obj = new ComisionComparativo();

                obj.Comisionado = Convert.ToString(Reader["COMISIONADO"]);
                obj.Oficio = Convert.ToString(Reader["OFICIO"]);
                obj.Lugar = Convert.ToString(Reader["LUGAR"]);
                obj.Inicio = Convert.ToString(Reader["INICIO"]);
                obj.Final = Convert.ToString(Reader["FINAL"]);
                obj.Viaticos = Convert.ToString(Reader["VIATICOS_PAGADOS"]);
                obj.Fecha_Viaticos = Convert.ToString(Reader["FECHA_PAGO_VIATICOS"]);
                obj.Singladuras = Convert.ToString(Reader["SINGLADURAS_PAGADAS"]);
                obj.Fecha_Singladuras = Convert.ToString(Reader["FECHA_PAGO_SINGLADURAS"]);
                obj.Combustible = Convert.ToString(Reader["COMBUSTIBLE_PAGADO"]);
                obj.Fecha_Combustible = Convert.ToString(Reader["FECHA_PAGO_COMBUSTIBLE"]);
                obj.Peaje = Convert.ToString(Reader["PEAJE_PAGADO"]);
                obj.Pasaje = Convert.ToString(Reader["PASAJE_PAGADO"]);
                obj.Fecha_Pasaje = Convert.ToString(Reader["FECHA_PAGO_PASAJE"]);
                obj.Archivo = Convert.ToString(Reader["ARCHIVO"]);


                ListComparativo.Add(obj);
                obj = null;

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return ListComparativo;

        }
        /*
         * checar
        public static List<ComisionDetalle> Lista_Montos_Comisiones(string psProyecto, string psUbicacion, string psFinancieros = "", string psInicio = "", string psFin = "")
        { 
        
        }
        */
        public static List<ComisionDetalle> ListaComisiones(string psProyecto, string psUbicacion, string psFinancieros = "", string psInicio = "", string psFin = "")
        {
            string Query = "";

            Query += "  SELECT  * ";//DIAS , INICIO, FINAL, TOTAL_VIATICOS,COMBUSTIBLE,PEAJE,PASAJE,ESTATUS  ";
            Query += "  FROM vw_comisiones  ";
            Query += "  WHERE PROYECTO = '" + psProyecto + "'";
            Query += "  AND UBICACION_PROYECTO = '" + psUbicacion + "'";

            if (psFinancieros != "")
            {
                Query += "  AND FINANCIEROS = '" + psFinancieros + "'";
            }

            Query += "  AND ((INICIO BETWEEN '" + psInicio + "' AND '" + psFin + "') OR ( FINAL BETWEEN '" + psInicio + "' AND '" + psFin + "' ))";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<ComisionDetalle> ListaEntidad = new List<ComisionDetalle>();

            while (Reader.Read())
            {
                ComisionDetalle objGrid = new ComisionDetalle();

                objGrid.Dias_Reales = Convert.ToString(Reader["DIAS"]);
                objGrid.Inicio = Convert.ToString(Reader["INICIO"]);
                objGrid.Final = Convert.ToString(Reader["FINAL"]);
                objGrid.Viaticos = Convert.ToString(Reader["TOTAL_VIATICOS"]);
                objGrid.Combustible = Convert.ToString(Reader["COMBUSTIBLE"]);
                objGrid.Peaje = Convert.ToString(Reader["PEAJE"]);
                objGrid.Pasaje = Convert.ToString(Reader["PASAJE"]);
                objGrid.EstatuS = Convert.ToString(Reader["ESTATUS"]);

                objGrid.Actividad = Convert.ToString(Reader["ACTIVIDAD"]);
                objGrid.Especie = Convert.ToString(Reader["ESPECIE"]);
                objGrid.Producto = Convert.ToString(Reader["PRODUCTO"]);
                objGrid.Lugar = Convert.ToString(Reader["LUGAR"]);
                objGrid.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
                objGrid.Comisionado = Convert.ToString(Reader["COMISIONADO"]);
                objGrid.Ubicacion_Comisionado = Convert.ToString(Reader["UBICACION_COMISIONADO"]);
                objGrid.Financieros = Convert.ToString(Reader["FINANCIEROS"]);
                objGrid.Tipo = Convert.ToString(Reader["FORMA_PAGO_VIATICOS"]);
                objGrid.Archivo = Convert.ToString(Reader["ARCHIVO"]);

                ListaEntidad.Add(objGrid);

                objGrid = null;

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );



            return ListaEntidad;
        }

        public static bool UPdate_Monto_Pagado(string psMontoPagado, string psFechaPago, string psPartida, string psOpcion, string psArchivo, string psComisionado)
        {
            bool resultado = false;
            string Query = "";
            Query += " UPDATE crip_comision_detalle   ";

            switch (psOpcion)
            {
                case "17":
                    Query += " SET VIATICOS_PAGADOS = '" + psMontoPagado + "',FECHA_PAGO_VIATICOS = '" + psFechaPago + "',PARTIDA = '" + psPartida + "' ";
                    break;

                case "6":
                    Query += " SET COMBUSTIBLE_PAGADO = '" + psMontoPagado + "',FECHA_PAGO_COMB = '" + psFechaPago + "', PARTIDA_COMB = '" + psPartida + "'";
                    break;

                case "7":
                    Query += " SET PEAJE_PAGADO = '" + psMontoPagado + "',FECHA_PAGO_PEAJE = '" + psFechaPago + "',PARTIDA_PEAJE = '" + psPartida + "'";
                    break;

                case "8":
                    Query += " SET PASAJE_PAGADO = '" + psMontoPagado + "',FECHA_PAGO_PASAJE = '" + psFechaPago + "',PARTIDA_PASAJE = '" + psPartida + "'";
                    break;

                case "18":
                    Query += " SET SINGLADURAS_PAGADAS = '" + psMontoPagado + "',FECHA_PAGO_SINGLADURAS = '" + psFechaPago + "',PARTIDA_SINGLADURAS = '" + psPartida + "'";
                    break;
            }

            Query += "WHERE  ARCHIVO = '" + psArchivo + "'";
            Query += " AND COMISIONADO = '" + psComisionado + "'";
            Query += "  AND ESTATUS != '0'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1) resultado = true;
            else resultado = false;

            MngConexion.disposeConexionSMAF(ConexionMysql );


            return resultado;
        }

        public static bool UpdateEstatusFinancieros(string psEstatusFinancieros, string psArchivo, string psUsuario, string psPeriodo)
        {
            bool resultado = false;
            string Query = "";
            Query += " UPDATE crip_comision_detalle  ";
            Query += "      SET FINANCIEROS = '" + psEstatusFinancieros + "'";
            Query += "   WHERE 1=1";
            Query += " AND ARCHIVO= '" + psArchivo + "'";
            Query += " AND COMISIONADO = '" + psUsuario + "'";
            Query += " AND PERIODO = '" + psPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1) resultado = true;
            else resultado = false;

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return resultado;

        }
        public static Boolean Insertar_Detalle(string psNoOficio, string psArchivo, string psTipCom, string psFechIni, string psFechFin, string psProyecto, string psDepProy, string psComisionado, string psClvDepCom, string psFormaPagoViat, string psPeriodo, string psTerritorio, string psNameArchivoOf, string psViaticos = "", string psSingladuras = "", string psCombustible = "", string psPeaje = "", string psPasaje = "")
        {
            Boolean lbResultado;
            string Query = "";

            Query = "INSERT INTO crip_comision_detalle";
            Query += " ( ";
            Query += " NO_OFICIO, ";
            Query += " ARCHIVO, ";
            Query += " CLV_TIPO_COM, ";
            Query += " FECHA_MINISTRACION, ";
            Query += " FECHA_INICIO, ";
            Query += " FECHA_FINAL, ";
            Query += " FECHA_PAGO, ";
            Query += " PROYECTO, ";
            Query += " DEP_PROYECTO, ";
            Query += " COMISIONADO, ";
            Query += " CLV_DEP_COM, ";
            Query += " FORMA_PAGO_VIATICOS, ";
            Query += " VIATICOS, ";
            Query += " PARTIDA, ";
            Query += " SINGLADURAS, ";
            Query += " PARTIDA_SINGLADURAS, ";
            Query += " COMBUSTIBLE, ";
            Query += " PARTIDA_COMB, ";
            Query += " PEAJE, ";
            Query += " PARTIDA_PEAJE, ";
            Query += " PASAJE, ";
            Query += " PARTIDA_PASAJE, ";
            Query += " ESTATUS, ";
            Query += " SEC_EFF, ";
            Query += " PERIODO, ";
            Query += " TERRITORIO, ";
            Query += " CLC_SEC, ";
            Query += " ARCHIVO_OFICIO ";
            Query += " ) ";
            Query += " VALUES ";
            Query += " ( ";
            Query += " '" + psNoOficio + "', ";
            Query += " '" + psArchivo + "', ";
            Query += " '" + psTipCom + "', ";
            Query += " '1900-01-01', ";
            Query += " '" + psFechIni + "', ";
            Query += " '" + psFechFin + "', ";
            Query += " '1900-01-01', ";
            Query += " '" + psProyecto + "', ";
            Query += " '" + psDepProy + "', ";
            Query += " '" + psComisionado + "', ";
            Query += " '" + psClvDepCom + "', ";
            Query += " '" + psFormaPagoViat + "', ";
            Query +=  Convert.ToDouble(psViaticos) + " , " ;
            Query += " '" + Dictionary.Partida_Viat + "', ";
            Query +=  Convert.ToDouble(psSingladuras) + " , ";
            Query += " '" + Dictionary.Partida_Sing + "', ";
            Query +=  Convert.ToDouble(psCombustible) + ", ";
            Query += " '" + Dictionary.Partida_Comb + "', ";
            Query +=  Convert.ToDouble(psPeaje) + ", ";
            Query += " '" + Dictionary.Partida_Peaje + "',";
            Query +=  Convert.ToDouble(psPasaje) + ", ";
            Query += " '" + Dictionary.Partida_Pasaje + "', ";
            Query += " '1', ";
            Query += " '1', ";
            Query += " '" + psPeriodo + "', ";
            Query += " '" + psTerritorio + "', ";
            Query += " '0', ";
            Query += " '" + psNameArchivoOf + "' ";
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
        public static comision_informe Obtiene_OficioFirmado(string psComisionado, string psUbiaccionCom, string psFolio, string psDepSolicitud, string psPeriodo)
        {

            string query = "SELECT DISTINCT NO_OFICIO AS OFICIO , ";
            query += " CLV_DEP_COM AS UBICACION_COMISIONADO, ";
            query += " COMISIONADO AS COMISIONADO, ";
            query += " PROYECTO  AS PROYECTO, ";
            query += " DEP_PROYECTO  AS UBICACION_PROYECTO, ";
            query += " FECHA_INICIO AS FECHA_INICIO, ";
            query += " FECHA_FINAL AS FECHA_FIN, ";
            query += " SEC_EFF AS SECUENCIA, ";
            query += " ARCHIVO_OFICIO AS ARCHIVO_OFICIO, ";
            query += " FECHA_MINISTRACION AS FECHA_MINISTRACION ";
            query += " FROM crip_comision_detalle";
            query += " WHERE NO_OFICIO = '" + psFolio + "' ";
            query += " AND CLV_DEP_COM = '" + psUbiaccionCom + "' ";
            query += " AND COMISIONADO = '" + psComisionado + "' ";
            query += " AND ESTATUS = '1' ";
            query += " AND  DEP_PROYECTO =  '" + psDepSolicitud + "'";
            query += " AND FECHA_MINISTRACION = '1900-01-01'";
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
                obj.FECHA_INICIO = Convert.ToString(Reader["FECHA_INICIO"]);
                obj.FECHA_FINAL = Convert.ToString(Reader["FECHA_FIN"]);
                obj.SECUENCIA = Convert.ToString(Reader["SECUENCIA"]);
                obj.UBICACION_SOLICITUD = Convert.ToString(Reader["ARCHIVO_OFICIO"]);
                obj.FECHA_AUT = Convert.ToString(Reader["FECHA_MINISTRACION"]);

            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return obj;
        }


    }
}
