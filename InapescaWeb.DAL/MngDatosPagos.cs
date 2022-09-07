using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;

namespace InapescaWeb.DAL
{
    public class MngDatosPagos
    {
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());


        public static List<Entidad> ListaComisionesPagar(string psUbicacion,string psUbicacionProyecto="", string psPeriodo = "", bool psPagador = false )
        {
            string Query = "";

            Query += "SELECT ARCHIVO AS CODIGO, COMISIONADO AS COMISIONADO ";
            Query += "    FROM crip_comision_detalle ";
            Query += " WHERE 1=1 ";

            if (!psPagador)
            {
                  Query += " AND DEP_PROYECTO = '"+ psUbicacionProyecto  +"'";
                  Query += " AND CLV_DEP_COM = '" + psUbicacion +"'";
            }
            else
            {
                Query += "  AND  CLV_DEP_COM = '" + psUbicacion + "'";
            }

            Query += "  and ESTATUS  IN ('5','7','9') ";
            Query += "  AND FINANCIEROS NOT IN ( '2','0') ";

            if (psPeriodo != "")
            {
                Query += "  AND PERIODO ='" + Year  + "'";
            }
            else
            {
                Query += "  AND PERIODO ='" + psPeriodo + "'";
            }
            
            Query += "  ORDER BY NO_OFICIO ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]).Replace(".pdf", "") + "|" + Convert.ToString(Reader["COMISIONADO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["CODIGO"]).Replace(".pdf", "") + "|" + MngDatosUsuarios.Obtiene_Nombre_Completo(Convert.ToString(Reader["COMISIONADO"]));

                ListaEntidad.Add(objetoEntidad);

                objetoEntidad = null;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
         //   ConexionMysql = null;
            return ListaEntidad;

        }


        public static List<Pagos> Comisiones_Pagar(string psUbicacion, string psPeriodo = "")
        {
            string Query = "SELECT ARCHIVO AS FOLIO,";
            Query += "  USUARIO AS COMISIONADO, ";

            Query += "  LUGAR AS LUGAR , ";
            Query += "  CONCAT( FECHA_I,' AL ',FECHA_F) AS PERIODO ";
            Query += "  FROM crip_comision ";
            Query += "  WHERE CLV_DEP_PROY = '" + psUbicacion + "' ";
            Query += "  and ESTATUS NOT IN ('0','1') ";
            Query += "  AND ZONA_COMERCIAL NOT IN('15','18')";

            if (psPeriodo != "")
            {
                Query += "AND PERIODO ='" + psPeriodo + "'";
            }
            else
            {
                Query += "AND PERIODO ='" + Year + "'";
            }

            Query += " ORDER BY NO_OFICIO ASC";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);

            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Pagos> objPagos = new List<Pagos>();

            while (Reader.Read())
            {
                Pagos oPagos = new Pagos();

                oPagos.Folio = Convert.ToString(Reader["FOLIO"]).Replace(".pdf", "");
                oPagos.Usuario = Convert.ToString(Reader["COMISIONADO"]); ;
                oPagos.Comisionado = MngDatosUsuarios.Obtiene_Nombre_Completo(Convert.ToString(Reader["COMISIONADO"]));
                oPagos.Lugar = Convert.ToString(Reader["LUGAR"]);
                oPagos.Periodo = Convert.ToString(Reader["PERIODO"]);

                objPagos.Add(oPagos);
                oPagos = null;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return objPagos;
        }

    }
}
