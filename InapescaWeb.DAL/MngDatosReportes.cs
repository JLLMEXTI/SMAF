/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosReportes.cs
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
    public class MngDatosReportes
    {
        static readonly clsDictionary Dictionary = new clsDictionary();
   
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static List<Reporte_Viaticos > ListaComprobacion(string psPeriodo = "", string psInicio = "", string psFinal = "", string psFormaPago = "", string psEstatus = "", string psFinancieros = "", string psDep = "", string psUsuario = "")
        {
            string forma;
            string query = "";
            query += " SELECT   * ";/*DISTINCT ARCHIVO AS ARCHIVO, COMISIONADO AS COMISIONADO,";
            query += " CLV_DEP_COM AS UBICACION, ";
            query += " SUM(VIATICOS) AS VIATICOS_OTORGADOS, ";
            query += " SUM(COMBUSTIBLE) AS COMBUSTIBLE_OTORGADO, ";
            query += " SUM(PEAJE) AS PEAJE_OTORGADO, ";
            query += " SUM(PASAJE) AS PASAJE_OTORGADO, ";
            query += " FORMA_PAGO_VIATICOS as TIPO_COMISION,";
            query += " FINANCIEROS as FINANCIEROS";*/
            query += " FROM crip_comision_detalle";
            query += " WHERE 1 = 1";
        
            if( psPeriodo != "") query += " AND PERIODO = '" + psPeriodo +"'";
        
            if ((psInicio !="") & (psFinal != "") )
            {
                query += "   AND FECHA_INICIO BETWEEN '" + psInicio +"' AND '" + psFinal +"'";
                query += " AND FECHA_FINAL BETWEEN '" + psInicio +"' AND '" + psFinal +"'";
            }
            else  if ((psInicio !="") & (psFinal == "") )
            {
             query += "   AND FECHA_INICIO ='" + psInicio +"'";
             query += " AND FECHA_FINAL = '" + lsHoy  +"'";
            }
            else if ((psInicio =="") & (psFinal != "") )
            {
             query += "   AND FECHA_INICIO ='(SELECT INICIO FROM CRIP_ANIO_FISCAL WHERE ESTATUS = '1')'";
             query += " AND FECHA_FINAL = '" + lsHoy  +"'";
            }

            if (psFormaPago != "") query += " AND FORMA_PAGO_VIATICOS ='" + psFormaPago +"'";
           
            if(psEstatus != "") query  += " AND ESTATUS in ( " + psEstatus +")";
               
            if( psDep != "") query  += " AND CLV_DEP_COM = '" + psDep +"'";

            if (psUsuario !="") query += " AND COMISIONADO = '" + psUsuario +"'";
            
            if(psFinancieros !="") query += "  AND FINANCIEROS = '" + psFinancieros +"'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            List<Reporte_Viaticos > listaEntidad = new List<Reporte_Viaticos>();

            while (reader.Read())
            {
                Reporte_Viaticos obj = new Reporte_Viaticos();
                
                obj.Oficio = Convert.ToString(reader["ARCHIVO"]);
                obj.Comisionado = MngDatosUsuarios .Obtiene_Nombre_Completo (Convert.ToString(reader["COMISIONADO"]));
                obj.Viaticos_Otorgados = Convert.ToString(reader["VIATICOS"]);
                obj.Viaticos_Comprobados = MngDatosComprobacion.Total(Convert.ToString(reader["COMISIONADO"]), Convert.ToString(reader["CLV_DEP_COM"]), obj.Oficio, "'5','9','12'");
                obj.Combustible_Otorgado = Convert.ToString(reader["COMBUSTIBLE"]);
                obj.Combustible_Comprobado = MngDatosComprobacion.Total(Convert.ToString(reader["COMISIONADO"]), Convert.ToString(reader["CLV_DEP_COM"]), obj.Oficio, "'6'");
                obj.Peaje_Otorgado = Convert.ToString(reader["PEAJE"]);
                obj.Peaje_Comprobado = MngDatosComprobacion.Total(Convert.ToString(reader["COMISIONADO"]), Convert.ToString(reader["CLV_DEP_COM"]), obj.Oficio, "'7'");
                obj.Pasaje_Otorgado = Convert.ToString(reader["PASAJE"]);
                obj.Pasaje_Comprobado = MngDatosComprobacion.Total(Convert.ToString(reader["COMISIONADO"]), Convert.ToString(reader["CLV_DEP_COM"]), obj.Oficio, "'8'");
                
                forma = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);

                if (forma == "1")
                {
                    obj.Reintegro = "0";
                    obj.Reintegro_Efectuado = "0";
                }
                else
                {
                    obj.Reintegro = clsFunciones.ConvertString((clsFunciones.Convert_Double(obj.Viaticos_Otorgados) + clsFunciones.Convert_Double(obj.Combustible_Otorgado) + clsFunciones.Convert_Double(obj.Peaje_Otorgado) + clsFunciones.Convert_Double(obj.Pasaje_Otorgado)) - (clsFunciones.Convert_Double(obj.Viaticos_Comprobados) + clsFunciones.Convert_Double(obj.Combustible_Comprobado) + clsFunciones.Convert_Double(obj.Peaje_Comprobado) + clsFunciones.Convert_Double(obj.Pasaje_Comprobado)));
                    obj.Reintegro_Efectuado = MngDatosComprobacion.Total(Convert.ToString(reader["COMISIONADO"]), Convert.ToString(reader["CLV_DEP_COM"]), obj.Oficio, "'13'");
                }
                
                
                //obj.Deudor = obj.Reintegro;
                obj.Total = clsFunciones.Convert_Decimales(clsFunciones.ConvertString(clsFunciones.Convert_Double(clsFunciones.Convert_Double(obj.Viaticos_Comprobados) + clsFunciones.Convert_Double(obj.Combustible_Comprobado) + clsFunciones.Convert_Double(obj.Peaje_Comprobado) + clsFunciones.Convert_Double(obj.Pasaje_Comprobado) + clsFunciones.Convert_Double (obj.Reintegro_Efectuado ))));
                obj.Tipo = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                obj.Financiero = Convert.ToString(reader["FINANCIEROS"]);

                listaEntidad.Add(obj);

            }

         MngConexion.disposeConexionSMAF(ConexionMysql );


            return listaEntidad;
        }

        public static List<Entidad> TraeReporteViatOtorPorUsser(string psPeriodo, string psAdscripcion = "")
        {
            string query = "SELECT DISTINCT USUARIO, SUM(TOTAL_VIATICOS) AS SUMA_VIATICOS FROM crip_comision    ";
            query += " WHERE PERIODO='" + psPeriodo + "'";
            query += " AND ESTATUS IN ('9','7')";
            if(psAdscripcion!="")
            {
                query += " AND CLV_DEP_COM='" + psAdscripcion + "'";
            }
            
            query += " GROUP BY NOMBRE_CANDIDATO";
            query += " ORDER BY VOTOS DESC LIMIT 5";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> ListDGA = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objDGA = new Entidad();

                objDGA.Codigo = Convert.ToString(Reader["USUARIO"]);
                objDGA.Descripcion = Convert.ToString(Reader["NSUMA_VIATICOS"]);

                ListDGA.Add(objDGA);

            }


            MngConexion.disposeConexion();
            return ListDGA;

        }
    }
}



