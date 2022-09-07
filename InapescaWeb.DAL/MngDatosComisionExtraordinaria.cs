/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.DAL
	FileName:	MngDatosComisionExtraordinaria.cs
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

namespace InapescaWeb.DAL
{
    public class MngDatosComisionExtraordinaria
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string Year = DateTime.Today.Year.ToString();
        
        public static List<Comision_Extraordinaria> Obtiene_ComisionExtraordinaria(string psUsuario ,string psFechaInicio,string psFechaFin,string psProyecto = "")
        {
            string Query = "";

            Query = "SELECT DISTINCT NO_OFICIO AS OFICIO,FECHA_I AS INICIO , FECHA_F AS FINAL,LUGAR AS LUGAR,CLV_PROY AS PROYECTO, CLV_DEP_PROY AS UBICACION_PROYECTO,OBJETIVO AS OBJETIVO ";
               Query += " FROM crip_comision ";
               Query += " WHERE USUARIO ='"+ psUsuario +"'";
               Query += " and ESTATUS = '6' AND FECHA_I BETWEEN '"+ psFechaInicio +"' AND '"+psFechaFin +"'";
               if (psProyecto != "")
               { 
               Query += " AND CLV_PROY = '"+psProyecto +"'";
               }
               Query += " UNION ";
               Query += " SELECT DISTINCT NO_OFICIO AS OFICIO,FECHA_I AS INICIO , FECHA_F AS FINAL,LUGAR AS LUGAR,CLV_PROY AS PROYECTO, CLV_DEP_PROY AS UBICACION_PROYECTO,OBJETIVO AS OBJETIVO";
               Query += " FROM crip_comision";
               Query += " WHERE USUARIO ='"+psUsuario +"'";
               Query += " and ESTATUS = '6' AND FECHA_F BETWEEN '"+psFechaInicio +"' AND '"+psFechaFin +"'";
               if (psProyecto != "")
               {
                   Query += " AND CLV_PROY = '" + psProyecto + "'";
               }

               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Comision_Extraordinaria > ListGrid = new List<Comision_Extraordinaria >();
            
            while (Reader.Read())
            {
                //Entidad objetoEntidad = new Entidad();
                Comision_Extraordinaria objComision = new Comision_Extraordinaria();
                objComision.Oficio = Convert.ToString(Reader["OFICIO"]);
                objComision.Fecha_Inicio = Convert.ToString(Reader["INICIO"]);
                objComision.Fecha_Final = Convert.ToString(Reader["FINAL"]);
                objComision.Lugar = Convert.ToString(Reader["LUGAR"]);
                objComision.Proyecto = Convert.ToString(Reader["PROYECTO"]);
                objComision.Ubicacion_Proyecto = Convert.ToString(Reader["UBICACION_PROYECTO"]);
                objComision.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
             
                ListGrid.Add(objComision );
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return ListGrid;
        }

    }
}
