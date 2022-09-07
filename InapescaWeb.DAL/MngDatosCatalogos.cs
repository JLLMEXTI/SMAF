using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
namespace InapescaWeb.DAL
{
    public class MngDatosCatalogos
    {
        static string Year = DateTime.Today.Year.ToString();
        static clsDictionary Dictionary = new clsDictionary();
        public static List<Entidad> Obtiene_Adscripcion_Ejecutante(string psUbicacion, string psPeriodo)
        {
            string Query = "    SELECT DISTINCT  CLV_DEP_COM  AS CODIGO ";
            Query += "   FROM crip_comision ";
            Query += " WHERE CLV_DEP_PROY = '" + psUbicacion + "' ";
            Query += " AND ESTATUS NOT IN ('0','1','8')";
            if (psPeriodo == "")
            {
                Query += " AND PERIODO = '" + Year + "'";
            }
            else
            {
                Query += " AND PERIODO = '" + psPeriodo + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = string.Empty;
            objetoEntidad1.Descripcion = "= S E L E C C I O N E=";

            List<Entidad> ListaEntidad = new List<Entidad>();

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = MngDatosDependencia.Descrip_Centro(Convert.ToString(Reader["CODIGO"]));

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            //  ConexionMysql = null;

            return ListaEntidad;

        }



        public static string Obtiene_Ubicacion(string psDirecc = "")
        {
            string ubicacion = "";
            string Query = "SELECT CLV_DEP AS CODIGO  FROM crip_dependencia WHERE CLV_ORG = 'INAPESCA' AND CLV_SECRE = 'SADER' AND ESTATUS = '1'";

            if (psDirecc != "")
            {
                Query += "AND CLV_DIR = '" + psDirecc + "'";
            }

            Query += "and ANIO = YEAR(NOW())";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                ubicacion = Convert.ToString(Reader["CODIGO"]);
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            //    ConexionMysql = null;
            return ubicacion;
        }

        public static List<Entidad> ObtieneCrips(string psTipo = "1", string psDirecc = "")
        {
            string Query = "";
            Query += "SELECT CLV_DEP AS CODIGO , DESCRIPCION  AS DESCRIPCION  ";
            Query += " FROM crip_dependencia ";
            Query += " WHERE 1=1  ";
            Query += " AND ESTATUS = '1' ";
            Query += " AND ANIO=  YEAR(NOW()) ";

            if (psDirecc != "")
            {
                Query += " AND CLV_DIR = '" + psDirecc + "'";
            }

            //if (psTipo == "1")
            //{
                Query += " AND TIPO = '" + psTipo + "'";
            //}

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = string.Empty;
            objetoEntidad1.Descripcion = "= S E L E C C I O N E=";

            List<Entidad> ListaEntidad = new List<Entidad>();

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            // ConexionMysql = null;

            return ListaEntidad;

        }

        public static List<Entidad> ListaCrips()
        {
            string Query = " ";
            Query += "SELECT CLV_DEP AS CODIGO,DESCR_CORTO AS DESCRIPCION ";
            Query += " FROM crip_dependencia ";
            Query += " WHERE 1=1 ";
            Query += " AND TIPO = '1'";
            Query += " AND CLV_ORG = 'INAPESCA' ";
            Query += " AND CLV_SECRE = 'SAGARPA' ";
            Query += " and ANIO = YEAR(NOW())";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = string.Empty;
            objetoEntidad1.Descripcion = "= S E L E C C I O N E =";

            List<Entidad> ListaEntidad = new List<Entidad>();

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            // ConexionMysql = null;

            return ListaEntidad;
        }

        public static List<Entidad> ObtieneUbicacion(string psDirecc = "")
        {
            string Query = "SELECT CLV_DEP AS CODIGO,DESCR_CORTO AS DESCRIPCION FROM crip_dependencia WHERE CLV_ORG = 'INAPESCA' AND CLV_SECRE = 'SADER' AND ESTATUS = '1'";

            if (psDirecc != "")
            {
                Query += "AND CLV_DIR = '" + psDirecc + "'";
            }
            Query += "and ANIO = YEAR(NOW())";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = string.Empty;
            objetoEntidad1.Descripcion = "= S E L E C C I O N E=";

            List<Entidad> ListaEntidad = new List<Entidad>();

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            // ConexionMysql = null;

            return ListaEntidad;
        }

        public static List<Entidad> ObtieneProyectosSustantivos(string psTipo1, string psDirecc, string psReg = "")
        {
            string Query = "SELECT CLV_DEP AS CODIGO,DESCR_CORTO AS DESCRIPCION FROM crip_dependencia WHERE CLV_ORG = 'INAPESCA' AND CLV_SECRE = 'SADER' AND ESTATUS = '1'";
            if (psDirecc == Dictionary.DGAIA)
            {
                Query += "  AND TIPO IN ('" + psTipo1 + "', '" + psReg + "') ";
                
            }
            else 
            {
                Query += "  AND (TIPO = '" + psTipo1 + "' AND CLV_DIR='"+Dictionary.DG +"')";
                Query += "  OR ID_REGION = '" + psReg + "'";
                Query += "  AND TIPO NOT IN ('7','8')";
            
            }
            
            
            Query += "and ANIO = YEAR(NOW())";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = string.Empty;
            objetoEntidad1.Descripcion = "= S E L E C C I O N E=";

            List<Entidad> ListaEntidad = new List<Entidad>();

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            // ConexionMysql = null;

            return ListaEntidad;
        }

        public static Ubicacion MngDatosUbicacion(string psUbicaion)
        {
            string lsQuery = " SELECT A.CLV_DEP AS DEPENDENCIA, ";
            lsQuery += " A.DESCRIPCION AS DESCRIPCION, ";
            lsQuery += " A.DESCR_CORTO AS DESC_CORTO, ";
            lsQuery += " A.CALLE AS CALLE, ";
            lsQuery += " A.NUM_EXT AS NUM_EXT ,";
            lsQuery += " A.NUM_INT AS NUM_INT, ";
            lsQuery += " A.COL AS COLONIA, ";
            lsQuery += " A.C_P AS CODIGO_POSTAL, ";
            lsQuery += " A.CIUDAD AS CIUDAD, ";
            lsQuery += " A.CLV_ESTADO AS CLV_ESTADO, ";
            /* lsQuery += " B.DESCR AS ESTADO, ";*/
            lsQuery += " A.CLV_PAIS AS PAIS, ";
            lsQuery += " A.TEL_1 AS TELEFONO, ";
            lsQuery += " A.EMAIL_1 AS EMAIL, ";
            lsQuery += " A.CLV_SECRE AS SECRETARIA, ";
            lsQuery += " A.CLV_ORG AS ORGANISMO ,";
            lsQuery += " A.SIGLAS as SIGLAS , ";
            lsQuery += " A.TIPO  AS TIPO ";
            lsQuery += " FROM crip_dependencia A";//, crip_estado B ";
            lsQuery += " WHERE"; // (A.CLV_ESTADO = B.CLV_ESTADO)  // AND ";
            lsQuery += " A.CLV_DEP = '" + psUbicaion + "' ";
            lsQuery += " AND A.ESTATUS = '1' ";
            lsQuery += " AND A.ANIO = '" + Year + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            Ubicacion objUbicacion = new Ubicacion();

            while (Reader.Read())
            {
                objUbicacion.Dependencia = Convert.ToString(Reader["DEPENDENCIA"]);
                objUbicacion.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objUbicacion.Desc_Corto = Convert.ToString(Reader["DESC_CORTO"]);
                objUbicacion.Calle = Convert.ToString(Reader["CALLE"]);
                objUbicacion.NumExt = Convert.ToString(Reader["NUM_EXT"]);
                objUbicacion.NumInt = Convert.ToString(Reader["NUM_INT"]);
                objUbicacion.Colonia = Convert.ToString(Reader["COLONIA"]);
                objUbicacion.Cp = Convert.ToString(Reader["CODIGO_POSTAL"]);
                objUbicacion.Ciudad = Convert.ToString(Reader["CIUDAD"]);
                objUbicacion.Clvestado = Convert.ToString(Reader["CLV_ESTADO"]).Trim();
                //objUbicacion.Estado = Convert.ToString(Reader[""]);
                objUbicacion.Pais = Convert.ToString(Reader["PAIS"]);
                objUbicacion.Telefono = Convert.ToString(Reader["TELEFONO"]);
                objUbicacion.Email = Convert.ToString(Reader["EMAIL"]);

                objUbicacion.Secretaria = Convert.ToString(Reader["SECRETARIA"]);
                objUbicacion.Organismo = Convert.ToString(Reader["ORGANISMO"]);
                objUbicacion.Siglas = Convert.ToString(Reader["SIGLAS"]).Trim();
                objUbicacion.Tipo = Convert.ToString(Reader["TIPO"]);
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            // ConexionMysql = null;

            return objUbicacion;
        }


        //modificacion pedro 11-10-17

        public static string Obtiene_RegimenFiscal(string psRegimen)
        {
            string sRegimenFiscal = "";
            string Query = "SELECT DISTINCT DESCRIPCION FROM c_regimen_fiscal WHERE REGIMEN_FISCAL='" + psRegimen + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                sRegimenFiscal = Convert.ToString(Reader["DESCRIPCION"]);
                if (sRegimenFiscal == "")
                {
                    sRegimenFiscal = "NA";
                }
            }

            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            //    ConexionMysql = null;
            return sRegimenFiscal;
        }


        //PSPS
        public static List<Entidad> ObtieneCrips1(string psTipo = "1", string psDirecc = "")
        {
            string Query = "";
            Query += "SELECT CLV_DEP AS CODIGO , DESCRIPCION  AS DESCRIPCION  ";
            Query += " FROM crip_dependencia ";
            Query += " WHERE 1=1  ";
            Query += " AND ESTATUS = '1' ";
            Query += " AND ANIO=  YEAR(NOW()) ";
            Query += " AND TIPO = '" + psTipo + "'";

            if ((psDirecc.Replace("'", "") == "4000") || (psDirecc.Replace("'", "") == "3000"))
            { 
            
            Query += " AND CLV_DIR = "+ psDirecc +"" ;
            
            }

            Query += " OR (CLV_DEP IN ("+ psDirecc +") AND ANIO= YEAR(NOW())  )";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = string.Empty;
            objetoEntidad1.Descripcion = "= S E L E C C I O N E=";

            List<Entidad> ListaEntidad = new List<Entidad>();

            ListaEntidad.Add(objetoEntidad1);
            objetoEntidad1 = null;

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);
                objetoEntidad = null;

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();

            //string Query1 = "";
            //Query1 += "SELECT CLV_DEP AS CODIGO , DESCRIPCION  AS DESCRIPCION  ";
            //Query1 += " FROM crip_dependencia ";
            //Query1 += " WHERE 1=1  ";
            //Query1 += " AND ESTATUS = '1' ";
            //Query1 += " AND ANIO=  YEAR(NOW()) ";
            

            //if (DEPS.Length < 2)
            //{
            //    Query1 += " AND CLV_DEP = '" + psDirecc +"' ";  
            //}
            //else
            //{
            //    Query1 += " AND CLV_DEP IN ("+ psDirecc.Replace ("|",",") +") ";
            //}


            //MySqlConnection ConexionMysql1 = MngConexion.getConexionMysql();

            //MySqlCommand cmd1 = new MySqlCommand(Query1, ConexionMysql1);

            //cmd1.Connection.Open();

            //MySqlDataReader Reader1 = cmd1.ExecuteReader();

            //while (Reader1.Read())
            //{
            //    Entidad objetoEntidad = new Entidad();
            //    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
            //    objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

            //    ListaEntidad.Add(objetoEntidad);

            //}

            //MngConexion.disposeConexionSMAF(ConexionMysql1);
            //Reader.Close();


            return ListaEntidad;

        }

    }
}
