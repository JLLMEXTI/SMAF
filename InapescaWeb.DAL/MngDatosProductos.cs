/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngDatosProductos.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Noviembre 2015
    Description: Clase que contiene metodos que extraen datos de productos
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
/// using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace InapescaWeb.DAL
{
        public  class MngDatosProductos
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsYear = DateTime.Now.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());


        public static string Obtiene_Producto_Descripcion(string psProducto, string psYear, string psFecha_VoBo = "")
        {
            string resultado = "";
            string Query = "";
             Query = "SELECT DESCRIPCION AS DESCRIPCION FROM crip_productos ";
                   Query += " where CLV_PRODUCTOS =  '"+psProducto +"'";
                   if (psFecha_VoBo != Dictionary.CADENA_NULA)
                   {
                       if (psYear == lsYear && (Convert.ToDateTime(psFecha_VoBo) > Convert.ToDateTime("2020-02-25")))
                       {
                           Query += " AND ESTATUS = '1'";
                       }
                       else
                       {
                           Query += " AND ESTATUS = '0'";
                       }


                   }
                   else 
                   {
                       Query += " AND ESTATUS = '1'";
                   }
                   
                   
                   Query += " AND PERIODO = '"+ psYear +"'";

                   MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
                   MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DESCRIPCION"]);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }
            
            
            public static List<Entidad> Obtiene_Productos(string year,string psDireccion, string psComponente="")
            {
                string Query = "SELECT CLV_PRODUCTOS AS CODIGO , DESCRIPCION AS DESCRIPCION ";
                Query += "FROM crip_productos WHERE ESTATUS = '1'";
                Query += " AND PERIODO = '" + year + "'";
                if ((psDireccion == "3000") | ((psDireccion == "4000")))
                {
                    Query += " AND AREA IN ('1','3')";
                    Query += " ORDER BY CLV_PRODUCTOS ASC";
                }
                else if (psDireccion == "2000")
                {
                    if (psComponente == "4KS")
                    {
                        Query += " AND AREA IN ('2','3','4')";
                        Query += " ORDER BY CLV_PRODUCTOS ASC";
                    }
                    else
                    {
                        Query += " AND AREA IN ('2','3')";
                        Query += " ORDER BY CLV_PRODUCTOS ASC";

                    }


                }
                else 
                {
                    Query += " AND AREA IN ('2','3')";
                    Query += " ORDER BY CLV_PRODUCTOS ASC";
                }

                MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
                MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
                cmd.Connection.Open();
                MySqlDataReader Reader = cmd.ExecuteReader();

                List<Entidad> ListaEntidad = new List<Entidad>();

                while (Reader.Read())
                {
                    Entidad objetoEntidad = new Entidad();
                    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                    objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                    ListaEntidad.Add(objetoEntidad);
                }
                MngConexion.disposeConexionSMAF(ConexionMysql );
                return ListaEntidad;
            }
    }
}
