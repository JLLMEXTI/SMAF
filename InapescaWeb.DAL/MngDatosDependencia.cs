using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
    public class MngDatosDependencia
    {
        static string Year = DateTime.Today.Year.ToString();


        /// <summary>
        /// metodo que obtiene la direccion y el tipo de dependencia recibiendo como para metro la dep del proyectp
        /// </summary> 
        /// <param name="psDep"></param>
        /// <returns></returns>
        public static Entidad Obtiene_Tipo_Region(string psDep)
        {
            string Query = "SELECT CLV_DIR AS DIRECCION , TIPO AS TIPO ";
            Query += " FROM crip_dependencia ";
            Query += " WHERE CLV_DEP = '" + psDep + "'";
            Query += "     AND ESTATUS = '1'";
            Query += " AND ANIO ='" + Year  + "'";


            Entidad objetoEntidad = new Entidad();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            while (Reader.Read())
            {
                objetoEntidad.Codigo = Convert.ToString(Reader["DIRECCION"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["TIPO"]);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return objetoEntidad;
        }

        public static List<Entidad> Obtiene_ciudades(string psEdo)
        {
            string Query = "SELECT CLV_CIUDAD AS CODIGO , DESCR AS DESCRIPCION FROM crip_ciudad ";
            Query += "  WHERE CLV_ESTADO = '" +psEdo + "' ORDER BY DESCR ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad objeto = new Entidad();
            objeto.Codigo = "00";
            objeto.Descripcion = "= S E L E C C I O N E =";

            ListaEntidad.Add(objeto);

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

          public static List<Entidad > Obtiene_Estados(string psPais)
        {
            string Query = "SELECT CLV_ESTADO AS CODIGO, DESCR AS DESCRIPCION FROM crip_estado";
                   Query += " WHERE CLV_PAIS = '"+ psPais +"' ORDER BY CLV_ESTADO ASC";

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

        public static List<Entidad> ObtieneSecretaria()
        {
            string Query = "SELECT CLV_SECRETARIA AS CODIGO ,DESCR AS DESCRIPCION FROM crip_secretaria where estatus = '1'";

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

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        }

        public static List<Entidad> ObtieneOrganismos(string psSec)
        {
            string Query = "SELECT CLV_ORG AS CODIGO,DESCRIP AS DESCRIPCION FROM crip_organismos WHERE CLV_SECRETARIA = '" + psSec + "' AND ESTATUS = '1'";

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

        public static List<Entidad> ListaSiglas(string psPeriodo)
        {
            string Query = "SELECT  SIGLAS AS ABREVIATURA from crip_dependencia ";
               Query += " WHERE ANIO='" + Year +"'";
           Query += " AND ESTATUS = '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["ABREVIATURA"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["ABREVIATURA"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            //  ConexionMysql = null;
            return ListaEntidad;
        }

        public static List<Entidad> ListaSiglasDependencias(string psPeriodo)
        {
            string Query = "SELECT  DESCR_CORTO, SIGLAS from crip_dependencia WHERE ANIO='" + Year + "' ";
            Query += " AND ESTATUS = '1' AND SIGLAS!='NULL' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["SIGLAS"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["SIGLAS"].ToString() + "-" + Reader["DESCR_CORTO"].ToString()); 
                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            //  ConexionMysql = null;
            return ListaEntidad;
        }

        public static List<Entidad> ObtieneCentro(string psSec, string psOrg, string psCentro)
        {
            string Query = "SELECT DISTINCT CLV_DEP AS CODIGO,  DESCR_CORTO AS DESCRIPCION FROM crip_dependencia WHERE CLV_ORG = '" + psOrg + "' AND CLV_SECRE = '" + psSec + "'  AND ESTATUS= '1'  AND CLV_DEP != '" + psCentro + "'";
           
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

            MngConexion.disposeConexionSMAF(ConexionMysql);
          //  ConexionMysql = null;
            return ListaEntidad;
        }

        public static List<Entidad> ObtieneCentro1(string psSec, string psOrg, string psCentro)
        {
            string Query = "SELECT DISTINCT CLV_DEP AS CODIGO,  DESCR_CORTO AS DESCRIPCION FROM crip_dependencia WHERE CLV_ORG = '" + psOrg + "' AND CLV_SECRE = '" + psSec + "'  AND ESTATUS= '1'  AND CLV_DEP = '" + psCentro + "'";

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

            MngConexion.disposeConexionSMAF(ConexionMysql);
            //  ConexionMysql = null;
            return ListaEntidad;
        }

        public static string Obtiene_Pais(string psPais)
        {
            string lsOrganismos = "";
            string Query = "";
            Query += " SELECT NOM_CORTO AS DESCRIPCION";
            Query += " FROM crip_pais ";
            Query += " where CLV_PAIS = '" + psPais +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                lsOrganismos = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lsOrganismos;
        }

        public static string Obtiene_Organismo(string psSecretaria , string  psOrganismo, string psClave = "")
        {
            string lsOrganismos =  "";
           string Query ="";

            if (psClave != "") Query = "SELECT CONCAT(DESCRIP,'|',CLAVE) AS DESCRIPCION FROM crip_organismos ";
            else   Query = "SELECT DESCRIP AS DESCRIPCION FROM crip_organismos  " ;
           
            Query += "    WHERE CLV_ORG = '" + psOrganismo  + "' AND CLV_SECRETARIA= '" + psSecretaria  + "' AND ESTATUS = '1'";
            Query += "  AND ANIO = '" + Year + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
          
            while (Reader.Read())
            {
                lsOrganismos = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lsOrganismos;
        }

        public static string[] Header_Mail(string psOrganismo, string psUbicacion)
        {

            string[] Headers = new string [5];


        string Query = "  SELECT DISTINCT  A.CLV_DEP AS CLV_DEP, A.DESCRIPCION AS DEPENDENCIA ,";
			   Query += " C.DESCR AS ESTADO, B.CLV_DIR AS CLV_DIR, B.DESCRIPCION AS DIRECCION  ";
               Query += " FROM crip_dependencia A , crip_direcciones B, crip_estado C ";
               Query += " WHERE A.ESTATUS = B.ESTATUS ";
               Query += " AND A.CLV_ORG = (SELECT CLV_ORG FROM crip_organismos B ";
			   Query += " WHERE B.ESTATUS = '1' AND B.CLV_ORG= '" + psOrganismo + "') ";
               Query += " AND A.CLV_DIR = B.CLV_DIR ";
               Query += " AND A.CLV_DEP = '" + psUbicacion  + "'";
               Query += " AND A.CLV_ESTADO =  C.CLV_ESTADO";
               Query += " AND A.ANIO ='" + Year + "'";

               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
               cmd.Connection.Open();

               MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader .Read ())
            {
                Headers[0] = Convert.ToString(Reader["CLV_DEP"]);
                Headers[1] = Convert.ToString(Reader["DEPENDENCIA"]);
                Headers[2] = Convert.ToString(Reader["ESTADO"]);
                Headers[3] = Convert.ToString(Reader["CLV_DIR"]);
                Headers[4] = Convert.ToString(Reader["DIRECCION"]);
            }


             MngConexion.disposeConexionSMAF(ConexionMysql );
        return Headers;
        }

        public static string Obtiene_Siglas(string psDep)
        {
            string Query = "";
            string resultado = "";

            Query = "SELECT  SIGLAS AS ABREVIATURA from crip_dependencia ";
            Query += " WHERE CLV_DEP = '"+ psDep +"' AND ANIO='" + Year +"'";
            Query += "AND ESTATUS = '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["ABREVIATURA"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );
          
            return resultado;
        }

        public static string Descrip_Centro( string psUbicacion,bool pbBandera= false )
        {
            string lsDescripcion = "";
            string Query = "";
            if (!pbBandera)
            {
                Query = "SELECT DISTINCT DESCR_CORTO AS DESCRIPCION ";
            }
            else
            {
                Query = "SELECT DESCRIPCION  AS DESCRIPCION ";
            }

             Query += "FROM crip_dependencia WHERE  ESTATUS= '1'  AND CLV_DEP = '" + psUbicacion + "' AND ANIO = '"+ Year +"'";
             MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
             MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                lsDescripcion = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
          //  ConexionMysql = null;
            return lsDescripcion;
        }
        public static string Descrip_Centro_PERIODO(string psUbicacion, string psPeriodo, bool pbBandera = false)
        {
            string lsDescripcion = "";
            string Query = "";
            if (!pbBandera)
            {
                Query = "SELECT DISTINCT DESCR_CORTO AS DESCRIPCION ";
            }
            else
            {
                Query = "SELECT DESCRIPCION  AS DESCRIPCION ";
            }

            Query += "FROM crip_dependencia WHERE CLV_DEP = '" + psUbicacion + "' AND ANIO = '" + psPeriodo + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                lsDescripcion = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            //  ConexionMysql = null;
            return lsDescripcion;
        }
        public static string Descrip_DepProy(string psUbicacion, string psYear)
        {
            string lsDescripcion = "";
            string Query = "";
           
            Query = "SELECT DESCRIPCION  AS DESCRIPCION "; 
            Query += "FROM crip_dependencia WHERE  ESTATUS= '1'  AND CLV_DEP = '" + psUbicacion + "' AND ANIO = '" + psYear + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                lsDescripcion = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            //  ConexionMysql = null;
            return lsDescripcion;
        }

        public static string Obtiene_Direccion(string psDep)
        {
            string Direccion = "";
            string Query = "  SELECT DISTINCT(A.CLV_DIR) AS DIRECCION FROM crip_dependencia AS A , crip_direcciones AS B WHERE A.CLV_DIR = B.CLV_DIR AND CLV_DEP = '" + psDep + "'";
           
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Direccion = Convert.ToString(Reader["DIRECCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return Direccion;
        }

        public static string Obtiene_Descripcion_Estado(string psClvEstado)
        {
            string estado = "";
            string Query = " SELECT DESCR AS DESCRIPCION  FROM crip_estado WHERE CLV_ESTADO = '" + psClvEstado +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                estado = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return estado;
        }

        public static string Obtiene_Descripcion_Ciudad(string psCiudad, string psClvEstado)
        {
            string ciudad = "";
            string Query = " SELECT DESCR  AS DESCRIPCION FROM crip_ciudad WHERE CLV_CIUDAD = '"+ psCiudad  +"' AND CLV_ESTADO = '"+psClvEstado+"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                ciudad = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return ciudad;
        }

        public static string Obtiene_Ciudad(string psClv_ciudad)
        {
            string descl = "";
            string Query = "SELECT DESCR FROM crip_ciudad WHERE CLV_CIUDAD = '"+psClv_ciudad +"'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                descl = Convert.ToString(Reader["DESCR"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );
            return descl;
        }

        public static string Obtiene_DIrector_Financieros(string psPuestoFinanciero,string psRol)
        {
            string resultado = "";
            string Query = " SELECT B.CLV_DEP  AS DEP ";
                    Query += " FROM  crip_dependencia A,  crip_job B ";
                   Query  += " WHERE B.CLV_PUESTO = '"+ psPuestoFinanciero +"' ";
                   Query += " AND A.CLV_DEP = B.CLV_DEP " ;
                   Query  += "     AND A.ESTATUS = '1'";
            Query += " AND A.ANIO = '"+Year +"'";
            Query += " AND B.ROL = '"+psRol +"'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DEP"]);
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
         //   ConexionMysql = null;
            return resultado;
        }

        public static Entidad Obtiene_Dependencia_porTipo(string psTipo , string psPeriodo= "" )
        {
            Entidad oEntidad = new Entidad();

            string descl = "";
            string Query = "  SELECT CLV_DEP AS CODIGO, DESCRIPCION AS DESCRIPCION FROM crip_dependencia ";
                   Query += " WHERE 1 = 1 ";
                   Query += " AND TIPO =  '" + psTipo +"'";
                   Query += " AND ESTATUS = '1'";
                   if (psPeriodo != "")
                   {
                       Query += " AND ANIO = '" + psPeriodo + "'";
                   }
                   else
                   {
                       Query += " AND ANIO = '" + Year  + "'";
                   }


                   MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
                   MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                oEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                oEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
          //  ConexionMysql = null;
            return oEntidad;
        }



    }
}
