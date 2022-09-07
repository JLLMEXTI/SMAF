using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace InapescaWeb.DAL
{
    public class MngDatosViaticos
    {
        public static List<Entidad> Obtiene_Zonas()
        {
            string Query = "SELECT CLV_ZONA AS CODIGO,CONCAT(DESCRIPCION, ' - $ ' ,TARIFA) AS DESCRIPCION FROM crip_zonas WHERE ESTATUS= '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();
            
            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  SELECCIONE  = ";

            ListaEntidad.Add(obj);

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

        public static List<Entidad> Metodo_Viaticos(bool pbBandera = false)
        {
            string Query = "SELECT CLV_VIATICOS AS CODIGO, DESCRIPCION AS DESCRIPCION FROM crip_viaticos WHERE ESTATUS = '1'";

            if (pbBandera) Query += "AND CLV_VIATICOS  IN ( '0','1','2')";

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

        public static string Descrip_Zona(string psClvZona)
        {
            string Query = "SELECT DESCRIPCION FROM crip_zonas WHERE ESTATUS = '1' AND CLV_ZONA = '" + psClvZona + "'";
            string zona = "";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                zona = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return zona;
        }

        public static string Descrip_Metodo_Viaticos(string psClvPago)
        {
            string Query = "SELECT DESCRIPCION FROM crip_viaticos WHERE CLV_VIATICOS = '" + psClvPago + "' AND ESTATUS = '1'";
            string pago = "";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                pago = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return pago;
        }


        //*******************************PATRICIA QUINO MORENO *******************************************//

        public static List<Entidad> TraerDatosCrip(string psTipo, string psEstatus, string psPeriodo)
        {
            string Query = "";
            Query += " SELECT DISTINCT CLV_DEP AS CODIGO, DESCRIPCION AS DESCRIPCION";
            Query += " FROM crip_dependencia ";
            Query += " WHERE TIPO= '" + psTipo + "'";
            Query += " AND ESTATUS  = '" + psEstatus + "'";
            Query += " AND ANIO= '" + psPeriodo + "'";



            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> TraerDatosCrip = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objComp = new Entidad();

                objComp.Codigo = Convert.ToString(Reader["CODIGO"]);
                objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                TraerDatosCrip.Add(objComp);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return TraerDatosCrip;
        }

        public static string Total_Concepto(string psPeriodo, string psClvProyecto, string psEstatus, string psTipo)
        {
            string Resultado = "";
            string Query = "  ";
            Query += " SELECT SUM(TOTAL_PAGADO) AS SUMA ";
            Query += " FROM crip_ministracion ";
            Query += " WHERE PERIODO= '" + psPeriodo + "'";
            Query += " AND CLV_PROY= '" + psClvProyecto + "'";
            Query += " AND ESTATUS= '" + psEstatus + "'";

            if (psTipo == "VIATICOS")
            {
                Query += " AND CLV_CONCEPTO IN ('5', '9', '11', '14', '15', '17', '20', '12')";

            }

            if (psTipo == "COMBUSTIBLE")
            {
                Query += "AND CONCEPTO = '6'";

            }

            if (psTipo == "PEAJE")
            {
                Query += "AND CONCEPTO = '7'";
            }

            if (psTipo == "PASAJE")
            {
                Query += "AND CONCEPTO = '8'";

            }

            if (psTipo == "SINGLADURAS")
            {
                Query += "AND CONCEPTO = '19'";

            }


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Resultado = Convert.ToString(Reader["SUMA"]);
            }

            if ((Resultado == "") | (Resultado == null))
            {
                Resultado = "0";
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Resultado;
        }

        public static List<Entidad> Adscripcion(string psClvDep, string psOficio, string psEstatus)
        {
            string Query = "";
            Query += " SELECT DISTINCT CLV_DEP AS CODIGO, DESCRIPCION AS DESCRIPCION";
            Query += " FROM crip_comision ";
            Query += " WHERE CLV_DEP='" + psClvDep + "'";
            Query += " AND NO_OFICIO=  = '" + psOficio + "'";
            Query += " AND ESTATUS!= '" + psEstatus + "'";



            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> Adscripcion = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objComp = new Entidad();

                objComp.Codigo = Convert.ToString(Reader["CODIGO"]);
                objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                Adscripcion.Add(objComp);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Adscripcion;

        }



        public static List<Comision> TraerComision(string psUsuario, string psPeriodo)
        {
            string Query = " SELECT no_oficio AS OFICIO, ARCHIVO, PERIODO, FECHA_I, FECHA_F";
            Query += " FROM crip_comision";
            Query += " WHERE USUARIO='" + psUsuario + "'";
            Query += " AND periodo = '" + psPeriodo + "' AND ESTATUS ='7'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Comision> TraerComision = new List<Comision>();

            while (Reader.Read())
            {
                Comision objComp = new Comision();

                objComp.Oficio = Convert.ToString(Reader["OFICIO"]);
                objComp.Archivo = Convert.ToString(Reader["ARCHIVO"]);
                objComp.Fecha_Inicio = Convert.ToString(Reader["FECHA_I"]);
                objComp.Fecha_Final = Convert.ToString(Reader["FECHA_F"]);

                TraerComision.Add(objComp);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return TraerComision;

        }



       
    }

}
