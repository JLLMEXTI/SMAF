using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
    public class MngActualizaDatos
    {
        static string Year = DateTime.Today.Year.ToString();


        static clsDictionary Dictionary = new clsDictionary();

        public static Usuario Obten_DatosUser(string psUsuario, string psdep,string psYear)
        {
            string lsQuery = "";

            lsQuery = " SELECT DISTINCT A.NOMBRE,A.AP_PAT,A.AP_MAT,A.FECH_NAC,A.CALLE,A.NUM_EXT AS NUMEXT,A.NUM_INT,A.COLONIA,A.DELEGACION,A.RFC,A.CURP,A.EMAIL,CD,CLV_ESTADO,A.SEC_EFF ";
            lsQuery += "FROM crip_usuarios A,crip_job B";
            lsQuery += " WHERE A.USUARIO='" + psUsuario + "'";
            lsQuery += "     AND A.ESTATUS= '1'";
            lsQuery += " AND A.USUARIO= B.USUARIO";
            lsQuery += " AND B.CLV_DEP= '" + psdep + "'";
            
            if (psYear  == "")
            {
                lsQuery += " AND B.PERIODO = '" + Year  + "'";
            }
            else
            {
                lsQuery += " AND B.PERIODO = '" + psYear + "'";
            }
            
            Usuario usu = new Usuario();
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
           
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                usu.Nombre = Convert.ToString(Reader["NOMBRE"]);
                usu.ApPat = Convert.ToString(Reader["AP_PAT"]);
                usu.ApMat = Convert.ToString(Reader["AP_MAT"]);
                usu.fech_nac = Convert.ToString(Reader["fech_nac"]);
                usu.calle = Convert.ToString(Reader["calle"]);
                usu.numext = Convert.ToString(Reader["NUMEXT"]);
                usu.num_int = Convert.ToString(Reader["num_int"]);
                usu.colonia = Convert.ToString(Reader["colonia"]);
                usu.delegacion = Convert.ToString(Reader["delegacion"]);
                usu.CURP = Convert.ToString(Reader["CURP"]);
                usu.RFC = Convert.ToString(Reader["RFC"]);
                usu.Email = Convert.ToString(Reader["Email"]);
                usu.CD = Convert.ToString(Reader["CD"]);
                usu.Estado = Convert.ToString(Reader["CLV_ESTADO"]);
           
                usu.SEC_EFF = Convert.ToString(Reader["SEC_EFF"]);

            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return usu;

        }

        public static Boolean Update_ActualizaDatosUser(Usuario poUsuario,string psUsuario)
        {
            Boolean Resultado = false;

            string lsQuery;
            lsQuery =  " UPDATE crip_usuarios SET NOMBRE = '" + poUsuario .Nombre +"' ,";// ,FECHEFF= '" + clsFunciones.FormatFecha(Convert.ToString(DateTime.Today)) + "'";
            lsQuery += " AP_PAT = '" + poUsuario .ApPat +"',";
            lsQuery += " AP_MAT = '" + poUsuario.ApMat  + "',";
            lsQuery += " FECH_NAC ='" + clsFunciones.FormatFecha ( poUsuario .fech_nac )+"',";
            lsQuery += " CALLE = '" + poUsuario .calle +"',";
            lsQuery += " NUM_EXT = '" + poUsuario.numext + "',";
            lsQuery += " NUM_INT = '" + poUsuario .num_int +"',";
            lsQuery += " COLONIA = '" + poUsuario .colonia +"',";
            lsQuery += " DELEGACION = '" + poUsuario .delegacion +"',";
            lsQuery += " CD = '" + poUsuario.CD + "',";
            lsQuery += " CLV_ESTADO = '" +poUsuario .Estado  +"',";
            lsQuery += " RFC = '" + poUsuario .RFC +"',";
            lsQuery += " CURP = '"+ poUsuario .CURP +"',";
            lsQuery += " EMAIL = '" + poUsuario.Email + "',";
            lsQuery += " FECHEFF = '" + clsFunciones.FormatFecha(Convert.ToString(DateTime.Today)) + "'";
            lsQuery += " WHERE USUARIO = '" + psUsuario + "' ";
            lsQuery += " AND ESTATUS = '1'";
           // lsQuery += "AND SEC_EFF = '" + sec_eff + "'";


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

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }
        public static Boolean Update_ActualizaCuentaBancaria(string psUsuario, string psCuentaBancaria)
        {
            Boolean Resultado = false;

            string lsQuery;
            lsQuery = " UPDATE crip_job SET CUENTA_CONTABLE = '" + psCuentaBancaria + "' ";
            lsQuery += "    WHERE USUARIO = '" + psUsuario + "' ";
            lsQuery += "    AND ESTATUS = '1'";
            lsQuery += "    AND PERIODO = '"+ Year +"'";


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

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }

        public static Boolean Update_ActualizaDatos(string psclv_nombre, string psclv_ap_pat, string psclv_ap_mat, string psclv_fecha_nac, string psclv_calle, string psclv_numext, string psclv_num_int, string psclv_colonia, string psclv_delegacion, string psclv_rfc, string psclv_curp, string psclv_email, string psCD, string psCLV_ESTADO)
        {

            Boolean Resultado = false;

            string lsQuery;
            lsQuery = " UPDATE INTO crip_usuarios ( ";
            lsQuery = lsQuery + " CLV_NOMBRE, ";
            lsQuery = lsQuery + " CLV_AP_PAT, ";
            lsQuery = lsQuery + " CLV_AP_MAT, ";
            lsQuery = lsQuery + " CLV_FECHA_NAC, ";
            lsQuery = lsQuery + " CLV_CALLE, ";
            lsQuery = lsQuery + " CLV_NUMEXT, ";
            lsQuery = lsQuery + " CLV_NUM_INT, ";
            lsQuery = lsQuery + " CLV_COLONIA, ";
            lsQuery = lsQuery + " CLV_DELEGACION, ";
            lsQuery = lsQuery + " CLV_RFC, ";
            lsQuery = lsQuery + " CLV_CURP, ";
            lsQuery = lsQuery + " CLV_EMAIL, ";
            lsQuery = lsQuery + " CD, ";
            lsQuery = lsQuery + " CLV_ESTADO, ";
            lsQuery = lsQuery + " ) ";
            lsQuery = lsQuery + " VALUES ";
            lsQuery = lsQuery + " (";
            lsQuery = lsQuery + " '" + psclv_nombre + "', ";
            lsQuery = lsQuery + " '" + psclv_ap_pat + "', ";
            lsQuery = lsQuery + " '" + psclv_ap_mat + "', ";
            lsQuery = lsQuery + " '" + psclv_fecha_nac + "', ";
            lsQuery = lsQuery + " '" + psclv_calle + "', ";
            lsQuery = lsQuery + " '" + psclv_numext + "', ";
            lsQuery = lsQuery + " '" + psclv_num_int + "', ";
            lsQuery = lsQuery + " '" + psclv_colonia + "', ";
            lsQuery = lsQuery + " '" + psclv_delegacion + "', ";
            lsQuery = lsQuery + " '" + psclv_rfc + "', ";
            lsQuery = lsQuery + " '" + psclv_curp + "', ";
            lsQuery = lsQuery + " '" + psclv_email + "', ";
            lsQuery = lsQuery + " '" + psCD + "', ";
            lsQuery = lsQuery + " '" + psCLV_ESTADO + "', ";
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

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;

        }

        public static bool Insert_Datos(string psUSUARIO, string psNOMBRE, string psAP_PAT, string PSAP_MAT, string psABREVIATURA, string PSGRADO_ACADEMICO, string PSTITULO, string PSFECH_NAC, string PSCALLE, string PSNUMEXT, string PSNUM_INT, string PSCOLONIA, string PSDELEGACION, string psCD, string PSCLV_ESTADO, string PSCLV_PAIS, string PSRFC, string PSCURP, string PSEMAIL)
        {
            Boolean pbResult;
            string lsQuery;
            lsQuery = " INSERT INTO crip_usuarios ( ";
            lsQuery = lsQuery + " USUARIO, ";
            lsQuery = lsQuery + " NOMBRE, ";
            lsQuery = lsQuery + " AP_PAT, ";
            lsQuery = lsQuery + " AP_MAT, ";
            lsQuery = lsQuery + " ABREVIATURA, ";
            lsQuery = lsQuery + " GRADO_ACADEMICO, ";
            lsQuery = lsQuery + " TITULO, ";
            lsQuery = lsQuery + " FECH_NAC, ";
            lsQuery = lsQuery + " CALLE, ";
            lsQuery = lsQuery + " `NUM-EXT`, ";
            lsQuery = lsQuery + " NUM_INT, ";
            lsQuery = lsQuery + " COLONIA, ";
            lsQuery = lsQuery + " DELEGACION, ";
            lsQuery = lsQuery + " CD, ";
            lsQuery = lsQuery + " CLV_ESTADO, ";
            lsQuery = lsQuery + " CLV_PAIS, ";
            lsQuery = lsQuery + " RFC, ";
            lsQuery = lsQuery + " CURP, ";
            lsQuery = lsQuery + " EMAIL, ";
            lsQuery = lsQuery + " FECHA, ";
            lsQuery = lsQuery + " FECHEFF, ";
            lsQuery = lsQuery + " ESTATUS, ";
            lsQuery = lsQuery + " SEC_EFF ";
            lsQuery = lsQuery + " ) ";
            lsQuery = lsQuery + " VALUES ";
            lsQuery = lsQuery + " ( ";
            lsQuery = lsQuery + "'" + psUSUARIO + "',";
            lsQuery = lsQuery + "'" + psNOMBRE + "',";
            lsQuery = lsQuery + "'" + psAP_PAT + "',";
            lsQuery = lsQuery + "'" + PSAP_MAT + "',";
            lsQuery = lsQuery + "'" + psABREVIATURA + "',";
            lsQuery = lsQuery + "'" + PSGRADO_ACADEMICO + "',";
            lsQuery = lsQuery + "'" + PSTITULO + "',";
            lsQuery = lsQuery + "'" + PSFECH_NAC + "',";
            lsQuery = lsQuery + "'" + PSCALLE + "',";
            lsQuery = lsQuery + "'" + PSNUMEXT + "',";
            lsQuery = lsQuery + "'" + PSNUM_INT + "',";
            lsQuery = lsQuery + "'" + PSCOLONIA + "',";
            lsQuery = lsQuery + "'" + PSDELEGACION + "',";
            lsQuery = lsQuery + "'"+ psCD +"',";
            lsQuery = lsQuery + "'"+PSCLV_ESTADO +"',";
            lsQuery = lsQuery + "'" + PSCLV_PAIS+"',";
            lsQuery = lsQuery + "'" + PSRFC + "',";
            lsQuery = lsQuery + "'" + PSCURP + "',";
            lsQuery = lsQuery + "'" + PSEMAIL + "',";
            lsQuery = lsQuery + "'" + clsFunciones.FormatFecha(Convert.ToString(DateTime.Today))+ "',";
            lsQuery = lsQuery + "'" + clsFunciones.FormatFecha(Convert.ToString(DateTime.Today))+ "',";
            lsQuery = lsQuery + "'1',";
            lsQuery = lsQuery + "(";
            lsQuery = lsQuery + "SELECT MAX";
            lsQuery = lsQuery + "(";
            lsQuery = lsQuery + "A.SEC_EFF";
            lsQuery = lsQuery + ")";
            lsQuery = lsQuery + " +1";
            lsQuery = lsQuery + " FROM crip_usuarios A";
            lsQuery = lsQuery + " WHERE A.USUARIO=";
            lsQuery = lsQuery + "'"+ psUSUARIO +"'";
            lsQuery = lsQuery + ")";
            lsQuery = lsQuery + ")";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
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
    }
}
