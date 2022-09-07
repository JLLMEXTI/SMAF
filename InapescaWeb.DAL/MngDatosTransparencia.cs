using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace InapescaWeb.DAL
{
    public class MngDatosTransparencia
    {
        static readonly clsDictionary dictionary = new clsDictionary();
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static List<Comision> Obten_Lista_Comision(string psPeriodo)
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          LUGAR  AS LUGAR,";        
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";            
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";                    
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";                     
            query += "          ARCHIVO AS ARCHIVO, ";
            query += "          RUTA AS RUTA, ";            
            query += "          USUARIO AS USUARIO ,";           
            query += "          DIAS_RURAL AS DIAS_RURAL, ";
            query += "          DIAS_COMERCIAL AS DIAS_COMERCIAL, ";
            query += "          DIAS_NAVEGADOS AS DIAS_NAVEGADOS, ";
            query += "          DIAS_50KM AS DIAS_50KM, ";
            query += "          DIAS_PAGAR AS DIAS_PAGAR, ";
            query += "          TERRITORIO AS TERRITORIO ,";
            query += "          COMBUST_EFECTIVO AS COMBUSTIBLE_EFECTIVO, ";
            query += "          COMBUST_VALES AS COMBUSTIBLE_VALES, ";
            query += "          SINGLADURAS AS SINGLADURAS, ";
            query += "          CLV_DEP_COM AS UBICACION_COMISIONADO, ";     
            query += "          NO_OFICIO_DGAIPP, ";
            query += "          NO_OFICIO_DG, ";
            query += "          NO_OFICIO_AMPLIACION, ";
            query += "          ARCHIVO_AMPLIACION ";
            query += "          FROM crip_comision WHERE ";
            query += "          PERIODO = '" + psPeriodo + "'  AND ESTATUS='7'"; 

           

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Comision> ListComision = new List<Comision>();
            
            while (reader.Read())
            {
                Comision objetoEntidad = new Comision();
                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);               
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);                
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);                
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);                
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);              
                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);
                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);                
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);
                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);
                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Dias_50 = Convert.ToString(reader["DIAS_50KM"]);
                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);              
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);
                objetoEntidad.Oficio_DGAIPP = Convert.ToString(reader["NO_OFICIO_DGAIPP"]);
                objetoEntidad.Oficio_DG = Convert.ToString(reader["NO_OFICIO_DG"]);
                objetoEntidad.Oficio_Ampliacion = Convert.ToString(reader["NO_OFICIO_AMPLIACION"]);
                objetoEntidad.Archivo_Ampliacion = Convert.ToString(reader["ARCHIVO_AMPLIACION"]);                
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);                   
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);
                ListComision.Add(objetoEntidad);    
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListComision;
        }

  
        public static List<Entidad> Transparencia_Ampliaciones(string sPeriodo)
        {
            string Query = "";
            Query += " SELECT ARCHIVO AS CODIGO, ARCHIVO_AMPLIACION AS DESCRIPCION ";
            Query += " FROM crip_comision ";
            Query += " WHERE  NO_OFICIO_AMPLIACION!='0' ";
            Query += " AND ESTATUS='7' ";
            Query += " AND PERIODO= '" + sPeriodo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad> listAmpliaciones = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objComp = new Entidad();
                objComp.Codigo = Convert.ToString(Reader["CODIGO"]);
                objComp.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                listAmpliaciones.Add(objComp);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return listAmpliaciones;
        }

        public static Usuario Obten_DatosExtras(string psUsuario)
        {
            string lsQuery = "";

            lsQuery += " SELECT DISTINCT CLV_NIVEL, CLV_PUESTO, CARGO FROM crip_job WHERE USUARIO='" + psUsuario + "' AND ESTATUS='1'";
            

            Usuario usu = new Usuario();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                usu.Nivel = Convert.ToString(Reader["CLV_NIVEL"]);
                usu.Puesto = Convert.ToString(Reader["CLV_PUESTO"]);
                usu.Cargo = Convert.ToString(Reader["CARGO"]);                
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return usu;
        }

        public static Entidad Obtiene_Clv_CiudadEstado(string psDependencia)
        {
            Entidad oEntidad = new Entidad();

            string Query = " SELECT DISTINCT CIUDAD, CLV_ESTADO FROM crip_dependencia WHERE CLV_DEP='" + psDependencia + "' AND ESTATUS='1' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                string CIUDAD = Convert.ToString(Reader["CIUDAD"]);
                string ESTADO = Convert.ToString(Reader["CLV_ESTADO"]);

                oEntidad.Codigo = CIUDAD.Replace("\r", "").Replace("\n", "");
                oEntidad.Descripcion = ESTADO.Replace("\r", "").Replace("\n", "");
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            //  ConexionMysql = null;
            return oEntidad;
        }

        public static string SumaImporteComprobado(string psArchivo, bool pbBandera = false)
        {
            string Resultado = "";
            string Query = "SELECT SUM(IMPORTE) AS SUMA FROM crip_comision_comprobacion WHERE CLAVE_OFICIO='" + psArchivo + "' AND ESTATUS!='0'";
            if (!pbBandera)
            {
                Query += " AND CONCEPTO='13' ";
            }
            else
            {
                Query += " AND CONCEPTO!='13'";
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
            Reader.Close();
            //  ConexionMysql = null;
            return Resultado;
        }


        public static Comision ImportePorConceptos(string psArchivo)
        {
            
            string Query = "SELECT PART_PRESUPUESTAL,TOTAL_VIATICOS, SINGLADURAS, COMBUSTIBLE_AUT, PARTIDA_COMBUSTIBLE, PEAJE, PART_PEAJE, PASAJE, PART_PASAJE  FROM crip_comision WHERE ARCHIVO='" + psArchivo + "' AND ESTATUS!='0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            Comision Obj = new Comision();

            while (Reader.Read())
            {
                Obj.Partida_Presupuestal = Convert.ToString(Reader["PART_PRESUPUESTAL"]);
                Obj.Total_Viaticos = Convert.ToString(Reader["TOTAL_VIATICOS"]);
                Obj.Singladuras = Convert.ToString(Reader["SINGLADURAS"]);
                Obj.Combustible_Autorizado = Convert.ToString(Reader["COMBUSTIBLE_AUT"]);
                Obj.Partida_Combustible = Convert.ToString(Reader["PARTIDA_COMBUSTIBLE"]);
                Obj.Peaje = Convert.ToString(Reader["PEAJE"]);
                Obj.Partida_Peaje = Convert.ToString(Reader["PART_PEAJE"]);
                Obj.Pasaje = Convert.ToString(Reader["PASAJE"]);
                Obj.Partida_Pasaje = Convert.ToString(Reader["PART_PASAJE"]);

            }
            //if ((Resultado == "") | (Resultado == null))
            //{
            //    Resultado = "0";
            //}

            MngConexion.disposeConexionSMAF(ConexionMysql);
            Reader.Close();
            //  ConexionMysql = null;
            return Obj;
        }

    }
}
