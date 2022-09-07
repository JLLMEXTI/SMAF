using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;


namespace InapescaWeb.DAL
{
    public class MngDatosTransporte
    {
        private static Boolean scheduller;
       
        static clsDictionary Dictionary = new clsDictionary();
        static string Year = DateTime.Today.Year.ToString();
        
        public static string Obtiene_Clave_Trans( string psClase, string psTipo)
        { 
            string transporte= Dictionary.CADENA_NULA ;
            string Query = "SELECT CLV_TRANSPORTE from crip_transporte  where CLV_CLASE = 'TER' and TIPO = 'AB'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read()) transporte = Convert.ToString(Reader["CLV_TRANSPORTE"]);

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return transporte;

        }



        public static List<Entidad> Obtiene_TransPDF(string psMixto = "")
        {
            string Query = "   SELECT DISTINCT TIPO AS CODIGO,  ";
            Query += " DESC_TIPO AS DESCRIPCION ";
            Query += " FROM crip_transporte ";
            Query += " WHERE 1=1 ";
            
            if (psMixto == "") Query += " AND CLV_CLASE != 'MX' ";

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

        public static List<Entidad> ObtieneClase(string psUbicacion,string psYear = "")
        {
            string Query = "SELECT DISTINCT CLV_CLASE AS CODIGO ,DESC_CLASE AS DESCRIPCION FROM  crip_transporte WHERE  CLV_DEP = '" + psUbicacion + "' AND ESTATUS = '1'";

            if (psYear != "")
            {
                Query += " AND ANIO  = '" + psYear + "'";
            }
            else
            {
                Query += " AND ANIO  = '" + Year  + "'";
            }

             Query += "ORDER BY DESC_CLASE";

             MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
             MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

           
            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = Dictionary .CADENA_NULA ;
            objetoEntidad1.Descripcion = "=  S E L E C C I O N E =";
            ListaEntidad.Add(objetoEntidad1);
            objetoEntidad1 = null;

            //objetoEntidad1 = new Entidad();
            //objetoEntidad1.Codigo = "NUL";
            //objetoEntidad1.Descripcion = " NINGUNO ";
            //ListaEntidad.Add(objetoEntidad1);
            //objetoEntidad1 = null;
           
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

        public static List<Entidad> ObtieneTipo(string psClase, string psUbicacion,string psYear = "")
        {

            string Query = "SELECT DISTINCT TIPO AS CODIGO , DESC_TIPO AS DESCRIPCION FROM  crip_transporte WHERE CLV_CLASE = '" + psClase + "' AND  CLV_DEP = '" + psUbicacion + "'  AND ESTATUS = '1'";
           
            if (psYear != "")
            {
                Query += " AND ANIO  = '" + psYear + "'";
            }
            else
            {
                Query += " AND ANIO  = '" + Year + "'";
            }

            Query += " ORDER BY CLV_TRANSPORTE";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();
            //List<Entidad> ListaEntidad1 = new List<Entidad>();
           
            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = Dictionary .CADENA_NULA;
            objetoEntidad1.Descripcion = "=  S E L E C C I O N E =";
            ListaEntidad.Add(objetoEntidad1);
           

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]).Trim ();
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return ListaEntidad;
        }

        public static List<Entidad> Descripcion_Transporte(string psClase, string psTipo, string psUbicacion, string psYear = "")
        {

            string Query = "SELECT CLV_TRANSPORTE AS CODIGO , MARCA AS MARCA, DESCRIPCION AS DESCRIPCION ,MODELO AS MODELO, PLACAS  AS PLACA FROM crip_transporte  WHERE CLV_CLASE = '" + psClase + "' AND TIPO = '" + psTipo + "' AND ESTATUS = 1  AND CLV_DEP = '" + psUbicacion + "'";

            if (psYear != "")
            {
                Query += " AND ANIO  = '" + psYear + "'";
            }
            else
            {
                Query += " AND ANIO  = '" + Year + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo =Dictionary .CADENA_NULA;
            objetoEntidad1.Descripcion = "=  S E L E C C I O N E =";
            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["MARCA"]) + "  " + Convert.ToString(Reader["DESCRIPCION"]) + " " + Convert.ToString(Reader["MODELO"]) + " " + Convert.ToString(Reader["PLACA"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );


            return ListaEntidad;
        }

        public static string[] Descrip_Trans(string psClase , string psTipo, string psUbicacion, string psVehiculo = "" )
        {
            string[] DatosVehiculo = new string[3];
            string Query = "";

            Query = "    SELECT DESC_CLASE AS CLASE,DESC_TIPO AS TIPO , MARCA AS MARCA, DESCRIPCION AS DESCRIPCION ,MODELO AS MODELO, PLACAS  AS PLACA FROM crip_transporte";
            Query += "   WHERE ";

         
                Query += "CLV_CLASE = '" + psClase + "'";
      
                Query += "   AND TIPO = '" + psTipo + "'";
       
            if (psVehiculo != "")
            {
                Query += "   AND CLV_TRANSPORTE = '" + psVehiculo + "' ";
            }
            
            Query += "      AND ESTATUS = '1'";
            Query += "   AND CLV_DEP = '" + psUbicacion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                DatosVehiculo[0] = Convert.ToString(Reader["CLASE"]);
                DatosVehiculo[1] = Convert.ToString(Reader["TIPO"]);
                DatosVehiculo[2] = Convert.ToString(Reader["MARCA"]) + "  " + Convert.ToString(Reader["DESCRIPCION"]) + " " + Convert.ToString(Reader["MODELO"]) + " " + Convert.ToString(Reader["PLACA"]);

            }


            MngConexion.disposeConexionSMAF(ConexionMysql );

            return DatosVehiculo;

        }

        public static Boolean MngScheduller(string psFechaI, string psFechaF, string psTransp, string psUbicacion)
        {

            string lsQuery = "SELECT * FROM vw_scheduller WHERE FECHA_I BETWEEN '" + psFechaI + "' AND  '" + psFechaF + "' AND TRANSPORTE_SOL = '" + psTransp + "' AND DEPENDENCIA = '" + psUbicacion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery , ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                scheduller = true;

            }
            else
            {
                scheduller = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return scheduller;
        }

    }
}
