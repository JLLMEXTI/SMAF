using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
    public class MngDatosProgramas
    {
        static string Year = DateTime.Today.Year.ToString();

        public static Boolean Insert_Programas(string psclv_componente,string psPrograma, string psdescripcion, string psdesc_corta, string psobjetivo, string pscoordinador, string psclv_direccion, string pspresupuesto)
        {
            Boolean Resultado;

            string lsQuery;
            lsQuery = " INSERT INTO crip_programas ( ";
            lsQuery = lsQuery + " CLV_COMPONENTE, ";
            lsQuery = lsQuery + " CLV_PROGRAMA, ";
            lsQuery = lsQuery + " DESCRIPCION, ";
            lsQuery = lsQuery + " DESC_CORTA, ";
            lsQuery = lsQuery + " OBJETIVO, ";
            lsQuery = lsQuery + " ESTATUS, ";
            lsQuery = lsQuery + " PERIODO, ";
            lsQuery = lsQuery + " SECEFF, ";
            lsQuery = lsQuery + " FECHA_EFF, ";
            lsQuery = lsQuery + " FECHA, ";
            lsQuery = lsQuery + " COORDINADOR, ";
            lsQuery = lsQuery + " CLV_DIRECCION, ";
            lsQuery = lsQuery + " AUTORIZADO ";
            lsQuery = lsQuery + " ) ";
            lsQuery = lsQuery + " VALUES ";
            lsQuery = lsQuery + " (";
            lsQuery = lsQuery + " '" + psclv_componente + "', ";
            lsQuery = lsQuery + " '" + psPrograma + "', ";
            lsQuery = lsQuery + " '" + psdescripcion + "', ";
            lsQuery = lsQuery + " '" + psdesc_corta + "', ";
            lsQuery = lsQuery + " '" + psobjetivo + "', ";
            lsQuery = lsQuery + " '1', ";
            lsQuery = lsQuery + " '" + Year + "', ";
            lsQuery = lsQuery + " '1', ";
            lsQuery = lsQuery + " '" + clsFunciones.FormatFecha(DateTime.Today.ToString()) + "', ";
            lsQuery = lsQuery + " '" + clsFunciones.FormatFecha(DateTime.Today.ToString()) + "', ";
            lsQuery = lsQuery + " '" + pscoordinador + "', ";
            lsQuery = lsQuery + " '" + psclv_direccion + "', ";
            lsQuery = lsQuery + " '" + pspresupuesto + "' ";
            lsQuery = lsQuery + " ) ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() == 1)
            {
                Resultado = true;
            }
            else
            {
                Resultado = false;
            }


            MngConexion.disposeConexionSMAF(ConexionMysql );


            return Resultado;


        }

        public static List<Entidad> ListaProgramas(string psDirecciones, string psYear)
        {
            List<Entidad> ListaEntidad = new List<Entidad>();
            string Query = "SELECT  CLV_PROGRAMA AS CODIGO , DESCRIPCION  AS DESCRIPCION ,CLV_DIRECCION AS DIRECCION ";
              Query += "  FROM crip_programas  ";
               Query += " WHERE 1 = 1";
               Query += " and ESTATUS = '1'";
               Query += " AND PERIODO = '" + psYear+"' ";
               Query += " AND CLV_DIRECCION IN(" + psDirecciones +")";
               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

               MySqlCommand cmd = new MySqlCommand(Query , ConexionMysql);
               cmd.Connection.Open();

               MySqlDataReader Reader = cmd.ExecuteReader();

               while (Reader.Read())
               {
                   Entidad oEntidad = new Entidad();
                   oEntidad.Codigo  = Convert.ToString(Reader["CODIGO"]);
                   oEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]) + " - " + MngDatosDependencia.Obtiene_Siglas (Convert.ToString(Reader["DIRECCION"]));
                   
                   ListaEntidad.Add(oEntidad );
               }
               return ListaEntidad;
        }

        public static List<Programa> Obtiene_Programas(string psDirecciones,string psYear = "",bool proyecto = false )
        {
            List<Programa> ListaEntidad = new List<Programa>();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            if (!proyecto)
            {
                string lsQuery = "SELECT  CLV_PROGRAMA AS CODIGO , DESCRIPCION  AS DESCRIPCION , DESC_CORTA AS DESC_CORTA , AUTORIZADO  AS PRESUPUESTO , CLV_DIRECCION FROM crip_programas";
                lsQuery += " WHERE 1 = 1";

                if (psYear != "")
                {
                    lsQuery += " AND PERIODO = '" + psYear + "'";

                }
                else
                {
                    lsQuery += " AND PERIODO = '" + Year + "'";
                    lsQuery += " AND ESTATUS = '1'";
                }

                lsQuery += " AND CLV_DIRECCION IN (" + psDirecciones + ")";
                lsQuery += "ORDER BY  CLV_DIRECCION ASC";

               
                MySqlCommand cmd = new MySqlCommand(lsQuery , ConexionMysql);
                cmd.Connection.Open();

                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Programa objetoEntidad = new Programa();
                    objetoEntidad.Id_Programa = Convert.ToString(Reader["CODIGO"]);
                    objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                    objetoEntidad.Descripcion_Corta = Convert.ToString(Reader["DESC_CORTA"]);
                    objetoEntidad.Presupuesto = Convert.ToString(Reader["PRESUPUESTO"]);
                    objetoEntidad.Direccion = Convert.ToString(Reader["CLV_DIRECCION"]);
                    ListaEntidad.Add(objetoEntidad);
                }
            }
            else
            {
                string lsQuery = "SELECT CLV_PROY AS PROYECTO ,DESCRIPCION AS DESCRIPCION,NOMBRE_LARGO,RECURSO AS PRESUPUESTO  FROM crip_proyecto ";
                lsQuery += " WHERE 1 = 1";

                if (psYear != "")
                {
                    lsQuery += " AND PERIODO = '" + psYear + "'";

                }
                else
                {
                    lsQuery += " AND PERIODO = '" + Year + "'";
                   // lsQuery += " AND ESTATUS = '1'";
                }

                lsQuery += " AND CLV_DEP = '" + psDirecciones + "'";

               
                MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
                cmd.Connection.Open();

                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Programa objetoEntidad = new Programa();
                    objetoEntidad.Id_Programa = Convert.ToString(Reader["PROYECTO"]);
                    objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                    objetoEntidad.Descripcion_Corta = Convert.ToString(Reader["NOMBRE_LARGO"]);
                    objetoEntidad.Presupuesto = Convert.ToString(Reader["PRESUPUESTO"]);

                    ListaEntidad.Add(objetoEntidad);
                }
              
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        
        }

        public static Boolean IsDireccion(string psDireccion)
        { 
            string query = "";
            query += "    select * from crip_direcciones ";
            query += "    where anio = '"+Year +"'";
            query += "    and ESTATUS = '1'";
            query += "   and CLV_DIR = '"+ psDireccion +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query , ConexionMysql);
            
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            bool isDireccion = false;

            if (Reader.Read())
            {
                isDireccion = true;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return isDireccion;
        }

        public static List<Entidad> Programas(string psDireccion,string psAnio= "")
        {
            string lsQuery = "select CLV_PROGRAMA AS CODIGO, DESCRIPCION AS DESCRIPCION ";
                   lsQuery += " from crip_programas";
                   lsQuery += " where ESTATUS = '1'";
            if( psAnio != "")
            {
             lsQuery += "  AND  PERIODO = '"+ psAnio +"'";
            }
            else
            {
             lsQuery += "  AND  PERIODO = '"+ Year  +"'";
            }
            
             lsQuery +=   " AND CLV_DIRECCION = '" + psDireccion +"'";
             lsQuery += " ORDER BY CLV_PROGRAMA ASC";

             MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
             MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();
            
            Entidad OBJ1 = new Entidad();
            OBJ1.Codigo = string.Empty;
            OBJ1.Descripcion = " =  S E L E C C I O N E = ";

            ListaEntidad.Add(OBJ1);

            OBJ1 = null;

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);
                objetoEntidad = null;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );
            return ListaEntidad;
        }

        public static List<Entidad> Obtiene_Programas_Direccion(string psUbicacion)
        {
            string lsQuery = "SELECT CLV_PROGRAMA AS CODIGO, DESCRIPCION  AS DESCRIPCION FROM crip_programas ";
            lsQuery += " WHERE CLV_DIRECCION = '" + MngDatosDependencia.Obtiene_Direccion(psUbicacion) + "'";
            lsQuery += "     AND ESTATUS = '1'";
            lsQuery += " AND PERIODO = '" + Year + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad OBJ1 = new Entidad();
            OBJ1.Codigo = string.Empty;
            OBJ1.Descripcion = " =  S E L E C C I O N E = ";

            ListaEntidad.Add(OBJ1);

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

        public static Programa Obtiene_Datos_Programa(string psPrograma, string psDireccion)
        {
            string query = "SELECT CLV_COMPONENTE AS COMPONENTE ,";
            query += "    CLV_PROGRAMA AS PROGRAMA,";
            query += " DESCRIPCION AS DESCRIPCION,";
            query += " DESC_CORTA AS DESCRIPCION_CORTA,";
            query += " OBJETIVO AS OBJETIVO,";
            query += " COORDINADOR AS COORDINADOR,";
            query += " CLV_DIRECCION AS DIRECCION,";
            query += " TIPO AS TIPO,";
            query += " AUTORIZADO AS PRESUPUESTO";
            query += " FROM crip_programas";
            query += " WHERE CLV_PROGRAMA ='" + psPrograma + "'";
            query += "    AND CLV_DIRECCION = '" + psDireccion + "'";
            query += " AND PERIODO = '" + Year + "'";
            query += " AND ESTATUS = '1' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            Programa obj = new Programa();
            while (Reader.Read())
            {

                obj.Componente = Convert.ToString(Reader["COMPONENTE"]);
                obj.Id_Programa = Convert.ToString(Reader["PROGRAMA"]);
                obj.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                obj.Descripcion_Corta = Convert.ToString(Reader["DESCRIPCION_CORTA"]);
                obj.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
                obj.Coordinador = Convert.ToString(Reader["COORDINADOR"]);
                obj.Direccion = Convert.ToString(Reader["DIRECCION"]);
                obj.Presupuesto = Convert.ToString(Reader["PRESUPUESTO"]);
                obj.Tipo = Convert.ToString(Reader["TIPO"]);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return obj;
        }

        public static string Obtiene_Max_Programa(string psDireccion)
        {
            string Query = "";
            Query = "SELECT MAX(CLV_PROGRAMA) AS MAX ";
            Query += "    FROM crip_programas";
            Query += " WHERE 1 = 1 ";
             Query  += "   AND CLV_DIRECCION = '"+ psDireccion +"'";
            Query += " AND PERIODO = '"+Year +"'";

            string max = "";
            int liNumero;
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
            MngConexion.disposeConexionSMAF(ConexionMysql );

            return Convert.ToString(liNumero);
        }

        public static List<Programa> ObtieneProgramas1(string psYear)
        {
            string lsQuery = "SELECT DISTINCT CLV_COMPONENTE, CLV_PROGRAMA,DESCRIPCION,OBJETIVO, CLV_DIRECCION, TIPO ";
               lsQuery += " FROM crip_programas ";
               lsQuery += " WHERE 1=1 ";
               lsQuery += " AND PERIODO = '" + psYear  +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql); 
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Programa > ListPrograma = new List<Programa>();

            while (Reader.Read())
            {
                Programa  objetoEntidad = new Programa ();
                objetoEntidad.Componente = Convert.ToString(Reader["CLV_COMPONENTE"]);
                objetoEntidad.Id_Programa = Convert.ToString(Reader["CLV_PROGRAMA"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objetoEntidad.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
                objetoEntidad.Direccion = Convert.ToString(Reader ["CLV_DIRECCION"]);
                objetoEntidad .Tipo = Convert .ToString (Reader ["TIPO"]);

                ListPrograma.Add(objetoEntidad);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            
            return ListPrograma;
        }


    }
}
