using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosGenerarRef
    {


        public static string Obtiene_Max_Referencia()
        {
            string max;
            int IdNumero;
            string Query = "";
            Query = " SELECT MAX(ID) AS MAX ";
            Query += " FROM crip_referencia ";


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

                IdNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                IdNumero = 1;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(IdNumero);
        }

        public static Boolean Inserta_Referencia_Detalles(GenerarRef oRefencia)
        {
            Boolean lbResultado;

            string Query = "INSERT INTO crip_referenciaDetalles ";
            Query += "    ( ";
            Query += " ID,";
            Query += " REFERENCIA,";
            Query += " ARCHIVO,";
            Query += " COMISIONADO,";
            Query += " ESTATUS,";
            Query += " SEC_EFF,";
            Query += " CONCEPTO,";
            Query += " IMPORTE,";
            Query += " FECHA";
            Query += " ) ";
            Query += " VALUES ";
            Query += " ( ";
            Query += " '" + oRefencia.ID + "', ";
            Query += " '" + oRefencia.Referencia + "', ";
            Query += " '" + oRefencia.Archivo + "', ";
            Query += " '" + oRefencia.Comisionado + "', ";
            Query += " '" + oRefencia.Estatus + "', ";
            Query += " '" + oRefencia.SecEff + "', ";
            Query += " '" + oRefencia.Concepto + "', ";
            Query += " '" + oRefencia.Importe + "', ";
            Query += " '" + oRefencia.Fecha + "' ";
            Query += " )";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }

        public static Boolean Inserta_Referencia(GenerarRef oRefencia)
        {
            Boolean lbResultado;

            string Query = "INSERT INTO crip_referencia ";
            Query += "    ( ";
            Query += " ID,";
            Query += " REFERENCIA,";
            Query += " ARCHIVO,";
            Query += " COMISIONADO,";
            Query += " ESTATUS,";
            Query += " FECHA,";
            Query += " FECHA_VENC,";
            Query += " IMPORTE,";
            Query += " SEC_EFF,";
            Query += " IMPORTE_PAGADO,";
            Query += " FECHA_PAGO";
            Query += " ) ";
            Query += " VALUES ";
            Query += " ( ";
            Query += " '" + oRefencia.ID + "', ";
            Query += " '" + oRefencia.Referencia + "', ";
            Query += " '" + oRefencia.Archivo + "', ";
            Query += " '" + oRefencia.Comisionado + "', ";
            Query += " '" + oRefencia.Estatus + "', ";
            Query += " '" + oRefencia.Fecha + "' ,";
            Query += " '" + oRefencia.FechaVencimiento + "' ,";
            Query += " '" + oRefencia.Importe + "', ";
            Query += " '" + oRefencia.SecEff + "', ";
            Query += " '" + oRefencia.ImportePagado + "', ";
            Query += " '" + oRefencia.FechaPago + "' ";


            Query += " )";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lbResultado;
        }

        public static Comision Obten_Informacion_Comision(string psArchivo)
        {
            string query = "SELECT DISTINCT ARCHIVO, LUGAR AS LUGAR, FECHA_I AS FECHAINICIO, USUARIO , FECHA_F AS FECHAFINAL, OBJETIVO AS OBJETIVO, ESTATUS, CLV_DEP_COM";
            query += "  FROM crip_comision WHERE ARCHIVO='" + psArchivo + "' ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {

                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);

                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHAINICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHAFINAL"]);
                objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["CLV_DEP_COM"]);

            }
            else
            {
                objetoEntidad.Comisionado = "0";
            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }


        public static List<GenerarRef> Obten_Informacion_ReferenciaDetalles(string psArchivo, string psReferencia, bool boolID = false)
        {
            string query = "SELECT ID, REFERENCIA, ARCHIVO, COMISIONADO, ESTATUS, SEC_EFF, CONCEPTO, IMPORTE,FECHA";
            query += " FROM crip_referenciadetalles";

            if (!boolID)
            {
                query += " WHERE ARCHIVO = '" + psArchivo + "' AND REFERENCIA = '" + psReferencia + "'";
            }
            else
            {
                query += " WHERE ID = '" + psArchivo + "' AND REFERENCIA = '" + psReferencia + "'";
            }

            query += " AND ESTATUS != '0'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            List<GenerarRef> ListRefDetalles = new List<GenerarRef>();

            while (reader.Read())
            {
                GenerarRef objRef = new GenerarRef();
                objRef.Numero = Convert.ToString(reader["SEC_EFF"]);
                objRef.ID = Convert.ToString(reader["ID"]);
                objRef.Referencia = Convert.ToString(reader["REFERENCIA"]);
                objRef.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objRef.Comisionado = Convert.ToString(reader["COMISIONADO"]);
                objRef.Estatus = Convert.ToString(reader["ESTATUS"]);
                objRef.SecEff = Convert.ToString(reader["SEC_EFF"]);
                objRef.Concepto = Convert.ToString(reader["CONCEPTO"]);
                objRef.Importe = Convert.ToString(reader["IMPORTE"]);
                objRef.Fecha = Convert.ToString(reader["FECHA"]);

                ListRefDetalles.Add(objRef);
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListRefDetalles;
        }

        //public static List<Entidad> Obten_Informacion_Referencias(string psArchivo, string psReferencia)
        //{
        //    string query = "SELECT ID, REFERENCIA, ARCHIVO, COMISIONADO, ESTATUS, FECHA, FECHA_VENC, IMPORTE, SEC_EFF, IMPORTE_PAGADO, FECHA_PAGO";
        //    query += "FROM crip_referencia";
        //    query += "AND ARCHIVO = '" + psArchivo + "'";
        //    query += "AND REFERENCIA= '" + psReferencia + "'";

        //    MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
        //    MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
        //    cmd.Connection.Open();

        //    MySqlDataReader Reader = cmd.ExecuteReader();
        //    List<Entidad> ListaDetalles = new List<Entidad>();

        //    while (Reader.Read())
        //    {
        //        Entidad objDet = new Entidad();

        //        objDet.Codigo = Convert.ToString(Reader["CODIGO"]);
        //        objDet.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

        //        ListaDetalles.Add(objDet);

        //    }

        //    MngConexion.disposeConexionSMAF(ConexionMysql);
        //    return ListaDetalles;
        //}

        public static List<GenerarRef> Obten_Informacion_Referencia(string psArchivo)
        {
            string query = "SELECT ID, REFERENCIA, ARCHIVO, COMISIONADO, ESTATUS, FECHA, FECHA_VENC, IMPORTE, SEC_EFF, IMPORTE_PAGADO, FECHA_PAGO";
            query += " FROM crip_referencia";
            query += " WHERE ARCHIVO = '" + psArchivo + "' and estatus!='0'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            List<GenerarRef> ListRefDetalles = new List<GenerarRef>();


            while (reader.Read())
            {
                GenerarRef objetDet = new GenerarRef();

                objetDet.ID = Convert.ToString(reader["ID"]);
                objetDet.Referencia = Convert.ToString(reader["REFERENCIA"]);
                objetDet.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetDet.Comisionado = Convert.ToString(reader["COMISIONADO"]);
                if (reader["ESTATUS"].ToString() == "1")
                {
                    objetDet.Estatus = "ACTIVO";
                }
                if (reader["ESTATUS"].ToString() == "2")
                {
                    objetDet.Estatus = "PAGADO";
                }

                objetDet.Fecha = Convert.ToString(reader["FECHA"]);

                DateTime fecha = Convert.ToDateTime(reader["FECHA_VENC"]);


                objetDet.FechaVencimiento = fecha.ToString("yyyy-MM-dd");
                objetDet.Importe = Convert.ToString(reader["IMPORTE"]);
                objetDet.SecEff = Convert.ToString(reader["SEC_EFF"]);
                objetDet.ImportePagado = Convert.ToString(reader["IMPORTE_PAGADO"]);
                objetDet.FechaPago = Convert.ToString(reader["FECHA_PAGO"]);
                ListRefDetalles.Add(objetDet);
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListRefDetalles;
        }

        public static Boolean Update_UpdateReferencia(string sIdExistente, string sReferenciaExiste, string ESTATUS)
        {
            Boolean Resultado = false;

            string lsQueryRef;
            lsQueryRef = " UPDATE crip_referencia SET ESTATUS='" + ESTATUS + "'";
            lsQueryRef += " WHERE ID='" + sIdExistente + "'";
            lsQueryRef += " AND REFERENCIA='" + sReferenciaExiste + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(lsQueryRef, ConexionMysql);
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

        public static Boolean Update_UpdateTablasReferencias(string sIdExistente, string sReferenciaExiste, string ESTATUS)
        {
            Boolean Resultado = false;

            string lsQuery;
            lsQuery = "UPDATE crip_referencia,crip_referenciadetalles SET crip_referenciadetalles.ESTATUS='" + ESTATUS + "', crip_referencia.ESTATUS='" + ESTATUS + "'";
            lsQuery += " WHERE crip_referenciadetalles.ID='" + sIdExistente + "'";
            lsQuery += " AND crip_referenciadetalles.REFERENCIA='" + sReferenciaExiste + "'";
            lsQuery += " AND crip_referencia.ID='" + sIdExistente + "'";
            lsQuery += " AND crip_referencia.REFERENCIA='" + sReferenciaExiste + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() != 0)
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


        //************************
        public static Boolean Update_UpdateReferenciaGenerada(string sReferencia, string sImportePagado, string sFechaPago)
        {
            Boolean Resultado = false;

            string lsQueryRefGen;
            lsQueryRefGen = " UPDATE crip_referencia SET ESTATUS= '2', IMPORTE_PAGADO='" + sImportePagado + "', FECHA_PAGO='" + sFechaPago + "'";
            lsQueryRefGen += " WHERE REFERENCIA='" + sReferencia + "' AND ESTATUS!='0'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(lsQueryRefGen, ConexionMysql);
            cmd.Connection.Open();
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

        //*************************************************************************************************
        public static Comision Detalle_Comision_Archivo(string psArchivo, string psComisionado = "")
        {
            string query = "    SELECT DISTINCT FOLIO  AS FOLIO ,";
            query += "          NO_OFICIO AS OFICIO , ";
            query += "          RUTA AS RUTA , ";
            query += "          ARCHIVO AS ARCHIVO , ";
            query += "          FECHA_SOL AS FECHA_SOLICITUD, ";
            query += "          FECHA_RESP AS FECHA_RESP_PROYECT,";
            query += "          USSER AS USUARIO_SOLICITA,";
            query += "          CLV_DEP AS UBICACION,";
            query += "          CLV_AREA AS AREA,";
            query += "          CLV_PROY AS PROYECTO,";
            query += "          CLV_DEP_PROY AS DEP_PROYECTO,";
            query += "          CLV_TIPO_COM AS TIPO_COMISION,";
            query += "          ESPECIE AS PESQUERIA,";
            query += "          PRODUCTO AS PRODUCTO,";
            query += "          ACTIVIDAD_ESPECIFICA AS ACTIVIDAD_ESPECIFICA,";
            query += "          LUGAR  AS LUGAR,";
            query += "          CAPITULO AS CAPITULO,";
            query += "          PROCESO AS PROCESO,";
            query += "          INDICADOR AS INDICADOR,";
            query += "          PART_PRESUPUESTAL AS PARTIDA,";
            query += "          FECHA_I AS FECHA_INICIO,";
            query += "          FECHA_F AS FECHA_FINAL,";
            query += "          DIAS_TOTAL AS DIAS_TOTAL,";
            query += "          DIAS_REALES AS DIAS_REALES,";
            query += "          OBJETIVO AS OBJETIVO,";
            query += "          CLV_CLASE AS CLASE_TRANS,";
            query += "          CLV_TIPO_TRANS AS TIPO_TRANS,";
            query += "          `CLV_TRANS-SOL` AS VEHICULO_SOL,";
            query += "          CLV_CLASE_AUT AS CLASE_V_AUT,";
            query += "          CLV_TIPO_AUT AS TIPO_V_AUT,";
            query += "          CLV_TRANS_AUT AS VEHICULO_AUTORIZADO,";
            query += "          CLV_DEP_TRANS_AUT AS DEP_TRANS_AUT,";
            query += "          DESC_AUTO AS DESCRIP_VEHICULO,";
            query += "          DEP_TRANS AS UBICACION_TRANS,";
            query += "          ORIGEN_DESTINO_TER AS ORIGEN_DESTINO_TER,";
            query += "          AEROLINEA_PARTIDA AS AEROLINEA_PARTIDA,  ";
            query += "          VUELO_PARTIDA_NUM AS VUELO_PARTIDA_NUM,";
            query += "          FECHA_VUELO_IDA AS FECHA_VUELO_IDA,";
            query += "          HORA_PART AS HORA_PART,";
            query += "          AEROLINEA_REGRESO AS AEROLINEA_REGRESO, ";
            query += "          VUELO_RETURN_NUM AS VUELO_RETURN_NUM,";
            query += "          FECHA_VUELO_RETURN AS FECHA_VUELO_RETURN,";
            query += "          HORA_RET AS HORA_RET,";
            query += "          ORIGEN_DESTINO_AER AS ORIGEN_DESTINO_AER,";
            query += "          ORIGEN_DESTINO_ACU AS ORIGEN_DESTINO_ACUATICO,";
            query += "          COMBUSTIBLE_SOL AS COMBUSTIBLE_SOLICITADO,";
            query += "          COMBUSTIBLE_AUT AS COMBUSTIBLE_AUTORIZADO, ";
            query += "          PARTIDA_COMBUSTIBLE AS PARTIDA_COMBUSTIBLE,";
            query += "          VALE_COMBUST_I AS VALE_COMBUST_I ,";
            query += "          VALE_COMBUST_F AS VALE_COMBUST_F ,";
            query += "          TIPO_PAGO_COMB AS PAGO_COMBUSTIBLES ,";
            query += "          PEAJE AS PEAJE ,";
            query += "          PART_PEAJE AS PART_PEAJE ,";
            query += "          PASAJE AS PASAJE ,";
            query += "          PART_PASAJE AS PART_PASAJE ,";
            query += "          ZONA_COMERCIAL AS ZONA_COMERCIAL,";
            query += "          FORMA_PAGO_VIATICOS AS FORMA_PAGO_VIATICOS,";
            query += "          TIPO_VIATICOS AS TIPO_VIATICOS, ";
            query += "          TIPO_PAGO_VIATICO AS TIPO_PAGO_VIATICOS,";
            query += "          TOTAL_VIATICOS AS TOTAL_VIATICOS,";
            query += "          EQUIPO AS EQUIPO,";
            query += "          OBSERVACIONES_SOL AS OBSERVACIONES_SOL,";
            query += "          RESP_PROY AS RESP_PROY,";
            query += "          OBSERVACIONES_VoBo AS OBSERVACIONES_VoBo,";
            query += "          OBSERVACIONES_AUT AS OBSERVACIONES_AUT,";
            query += "          HORA_INICIAL AS HORA_INICIAL, ";
            query += "          HORA_FINAL AS HORA_FINAL, ";
            query += "          SECEFF AS SECUENCIA, ";
            query += "          ESTATUS AS ESTATUS ,";
            query += "          TERRITORIO AS TERRITORIO ,";
            query += "          DIAS_PAGAR AS  DIAS_PAGAR,";
            query += "          DIAS_RURAL AS  DIAS_RURAL,";
            query += "          DIAS_COMERCIAL AS  DIAS_COMERCIAL,";
            query += "          DIAS_NAVEGADOS AS  DIAS_NAVEGADOS,";
            query += "          COMBUST_EFECTIVO AS  COMBUSTIBLE_EFECTIVO,";
            query += "          COMBUST_VALES AS  COMBUSTIBLE_VALES,";
            query += "          TERRITORIO AS  TERRITORIO,";
            query += "          SINGLADURAS AS  SINGLADURAS,";
            query += "          PERIODO AS PERIODO,  ";

            query += "  CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA";
            query += " ,USUARIO AS USUARIO ,";
            query += "  AUTORIZA AS AUTORIZA, ";
            query += "  VoBo AS VOBO, ";
            query += "  CLV_DEP_COM AS UBICACION_COMISIONADO";
            //    query += "  CLV_DEP_AUTORIZA AS UBICACION_AUTORIZA";


            query += "          FROM crip_comision WHERE ARCHIVO = '" + psArchivo + "' AND ESTATUS != '0'";

            if (psComisionado != "")
            {
                //Query += "          AND ESTATUS = '9'";
                query += "          AND USUARIO = '" + psComisionado + "'";

            }


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();


            Comision objetoEntidad = new Comision();
            if (reader.Read())
            {

                objetoEntidad.Folio = Convert.ToString(reader["FOLIO"]);
                objetoEntidad.Oficio = Convert.ToString(reader["OFICIO"]);

                objetoEntidad.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetoEntidad.Ruta = Convert.ToString(reader["RUTA"]);

                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);

                objetoEntidad.Fecha_Solicitud = Convert.ToString(reader["FECHA_SOLICITUD"]);
                objetoEntidad.Fecha_Responsable = Convert.ToString(reader["FECHA_RESP_PROYECT"]);
                objetoEntidad.Usuario_Solicita = Convert.ToString(reader["USUARIO_SOLICITA"]);
                objetoEntidad.Ubicacion = Convert.ToString(reader["UBICACION"]);
                objetoEntidad.Area = Convert.ToString(reader["AREA"]);
                objetoEntidad.Proyecto = Convert.ToString(reader["PROYECTO"]);
                objetoEntidad.Dep_Proy = Convert.ToString(reader["DEP_PROYECTO"]);

                objetoEntidad.Pesqueria = Convert.ToString(reader["PESQUERIA"]);
                objetoEntidad.Producto = Convert.ToString(reader["PRODUCTO"]);
                objetoEntidad.Actividad = Convert.ToString(reader["ACTIVIDAD_ESPECIFICA"]);
                objetoEntidad.Tipo_Comision = Convert.ToString(reader["TIPO_COMISION"]);

                objetoEntidad.Lugar = Convert.ToString(reader["LUGAR"]);
                objetoEntidad.Capitulo = Convert.ToString(reader["CAPITULO"]);
                objetoEntidad.Proceso = Convert.ToString(reader["PROCESO"]);
                objetoEntidad.Indicador = Convert.ToString(reader["INDICADOR"]);
                objetoEntidad.Partida_Presupuestal = Convert.ToString(reader["PARTIDA"]);
                objetoEntidad.Fecha_Inicio = Convert.ToString(reader["FECHA_INICIO"]);
                objetoEntidad.Fecha_Final = Convert.ToString(reader["FECHA_FINAL"]);
                objetoEntidad.Dias_Total = Convert.ToString(reader["DIAS_TOTAL"]);
                objetoEntidad.Dias_Reales = Convert.ToString(reader["DIAS_REALES"]);
                objetoEntidad.Objetivo = Convert.ToString(reader["OBJETIVO"]);
                objetoEntidad.Clase_Trans = Convert.ToString(reader["CLASE_TRANS"]);
                objetoEntidad.Tipo_Trans = Convert.ToString(reader["TIPO_TRANS"]);
                objetoEntidad.Vehiculo_Solicitado = Convert.ToString(reader["VEHICULO_SOL"]);
                objetoEntidad.Clase_Aut = Convert.ToString(reader["CLASE_V_AUT"]);
                objetoEntidad.Tipo_Aut = Convert.ToString(reader["TIPO_V_AUT"]);//
                objetoEntidad.Vehiculo_Autorizado = Convert.ToString(reader["VEHICULO_AUTORIZADO"]);
                objetoEntidad.Ubicacion_Trans_Aut = Convert.ToString(reader["DEP_TRANS_AUT"]);
                objetoEntidad.Descrip_vehiculo = Convert.ToString(reader["DESCRIP_VEHICULO"]);
                objetoEntidad.Ubicacion_Transporte = Convert.ToString(reader["UBICACION_TRANS"]);
                objetoEntidad.Origen_Terrestre = Convert.ToString(reader["ORIGEN_DESTINO_TER"]);
                objetoEntidad.Aerolinea_Part = Convert.ToString(reader["AEROLINEA_PARTIDA"]);
                objetoEntidad.Vuelo_Part_Num = Convert.ToString(reader["VUELO_PARTIDA_NUM"]);
                objetoEntidad.Fecha_Vuelo_part = Convert.ToString(reader["FECHA_VUELO_IDA"]);
                objetoEntidad.Hora_Vuelo_Part = Convert.ToString(reader["HORA_PART"]);
                objetoEntidad.Aerolinea_Ret = Convert.ToString(reader["AEROLINEA_REGRESO"]);
                objetoEntidad.Vuelo_Ret_Num = Convert.ToString(reader["VUELO_RETURN_NUM"]);
                objetoEntidad.Fecha_Vuelo_Ret = Convert.ToString(reader["FECHA_VUELO_RETURN"]);
                objetoEntidad.Hora_Vuelo_ret = Convert.ToString(reader["HORA_RET"]);
                objetoEntidad.Origen_Aereo = Convert.ToString(reader["ORIGEN_DESTINO_AER"]);
                objetoEntidad.Origen_Dest_Acu = Convert.ToString(reader["ORIGEN_DESTINO_ACUATICO"]);
                objetoEntidad.Combustible_Solicitado = Convert.ToString(reader["COMBUSTIBLE_SOLICITADO"]);
                objetoEntidad.Combustible_Autorizado = Convert.ToString(reader["COMBUSTIBLE_AUTORIZADO"]);
                objetoEntidad.Partida_Combustible = Convert.ToString(reader["PARTIDA_COMBUSTIBLE"]);
                objetoEntidad.Vale_Comb_I = Convert.ToString(reader["VALE_COMBUST_I"]);
                objetoEntidad.Vale_Comb_F = Convert.ToString(reader["VALE_COMBUST_F"]);
                objetoEntidad.Pago_Combustible = Convert.ToString(reader["PAGO_COMBUSTIBLES"]);

                objetoEntidad.Peaje = Convert.ToString(reader["PEAJE"]);
                objetoEntidad.Partida_Peaje = Convert.ToString(reader["PART_PEAJE"]);
                objetoEntidad.Pasaje = Convert.ToString(reader["PASAJE"]);
                objetoEntidad.Partida_Pasaje = Convert.ToString(reader["PART_PASAJE"]);


                objetoEntidad.Zona_Comercial = Convert.ToString(reader["ZONA_COMERCIAL"]);
                objetoEntidad.Forma_Pago_Viaticos = Convert.ToString(reader["FORMA_PAGO_VIATICOS"]);
                objetoEntidad.Tipo_Viaticos = Convert.ToString(reader["TIPO_VIATICOS"]);
                objetoEntidad.Tipo_Pago_Viatico = Convert.ToString(reader["TIPO_PAGO_VIATICOS"]);
                objetoEntidad.Total_Viaticos = Convert.ToString(reader["TOTAL_VIATICOS"]);

                objetoEntidad.Equipo = Convert.ToString(reader["EQUIPO"]);
                objetoEntidad.Observaciones_Solicitud = Convert.ToString(reader["OBSERVACIONES_SOL"]);
                objetoEntidad.Observaciones_Vobo = Convert.ToString(reader["OBSERVACIONES_VoBo"]);
                objetoEntidad.Observaciones_Autoriza = Convert.ToString(reader["OBSERVACIONES_AUT"]);
                objetoEntidad.Responsable_proyecto = Convert.ToString(reader["RESP_PROY"]);
                objetoEntidad.Inicio_Comision = Convert.ToString(reader["HORA_INICIAL"]);
                objetoEntidad.Fin_Comision = Convert.ToString(reader["HORA_FINAL"]);
                objetoEntidad.Secuencia = Convert.ToString(reader["SECUENCIA"]);
                objetoEntidad.Estatus = Convert.ToString(reader["ESTATUS"]);
                objetoEntidad.Territorio = Convert.ToString(reader["TERRITORIO"]);

                objetoEntidad.Dias_Pagar = Convert.ToString(reader["DIAS_PAGAR"]);
                objetoEntidad.Dias_Rural = Convert.ToString(reader["DIAS_RURAL"]);
                objetoEntidad.Dias_Comercial = Convert.ToString(reader["DIAS_COMERCIAL"]);
                objetoEntidad.Dias_Navegados = Convert.ToString(reader["DIAS_NAVEGADOS"]);

                objetoEntidad.Combustible_Efectivo = Convert.ToString(reader["COMBUSTIBLE_EFECTIVO"]);
                objetoEntidad.Combustible_Vales = Convert.ToString(reader["COMBUSTIBLE_VALES"]);
                objetoEntidad.Singladuras = Convert.ToString(reader["SINGLADURAS"]);
                objetoEntidad.Periodo = Convert.ToString(reader["PERIODO"]);

                objetoEntidad.Ubicacion_Autoriza = Convert.ToString(reader["UBICACION_AUTORIZA"]);

                
                    objetoEntidad.Comisionado = Convert.ToString(reader["USUARIO"]);
                    objetoEntidad.Autoriza = Convert.ToString(reader["AUTORIZA"]);
                    objetoEntidad.Vobo = Convert.ToString(reader["VOBO"]);
                    objetoEntidad.Ubicacion_Comisionado = Convert.ToString(reader["UBICACION_COMISIONADO"]);

               
            }
            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetoEntidad;
        }

        //*************************************************************************************************************
        public static GenerarRef Obten_Detalles_Referencia(string psReferencia)
        {
            string query = "SELECT DISTINCT ID, REFERENCIA, ARCHIVO, COMISIONADO, ESTATUS, FECHA, FECHA_VENC, IMPORTE, SEC_EFF, IMPORTE_PAGADO, FECHA_PAGO";
            query += " FROM crip_referencia";
            query += " WHERE REFERENCIA = '" + psReferencia + "' and estatus!='0'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            GenerarRef objetDet = new GenerarRef();

            if (reader.Read())
            {

                objetDet.ID = Convert.ToString(reader["ID"]);
                objetDet.Referencia = Convert.ToString(reader["REFERENCIA"]);
                objetDet.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objetDet.Comisionado = Convert.ToString(reader["COMISIONADO"]);
                if (reader["ESTATUS"].ToString() == "1")
                {
                    objetDet.Estatus = "ACTIVO";
                }
                if (reader["ESTATUS"].ToString() == "2")
                {
                    objetDet.Estatus = "PAGADO";
                }

                objetDet.Fecha = Convert.ToString(reader["FECHA"]);

                DateTime fecha = Convert.ToDateTime(reader["FECHA_VENC"]);


                objetDet.FechaVencimiento = fecha.ToString("yyyy-MM-dd");
                objetDet.Importe = Convert.ToString(reader["IMPORTE"]);
                objetDet.SecEff = Convert.ToString(reader["SEC_EFF"]);
                objetDet.ImportePagado = Convert.ToString(reader["IMPORTE_PAGADO"]);
                objetDet.FechaPago = Convert.ToString(reader["FECHA_PAGO"]);
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objetDet;
        }

        //***********************PATRICIA QUINO MORENO 24/08/2017**************************************
        public static List<GenerarRef> Obtiene_FFinal_FInicio(string psInicio, string psFinal, string sEstatus)
        {
            string Query = "SELECT ID, REFERENCIA, ARCHIVO, COMISIONADO, ESTATUS, IMPORTE, FECHA_PAGO,FECHA ";
            Query += " FROM crip_referencia ";
            Query += " WHERE FECHA BETWEEN '" + psInicio + "' AND '" + psFinal + "'";

            if (sEstatus != "OTRAS")
                Query += "AND ESTATUS='" + sEstatus + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<GenerarRef> ListObtieneF = new List<GenerarRef>();

            while (reader.Read())
            {
                GenerarRef objRef = new GenerarRef();
                objRef.ID = Convert.ToString(reader["ID"]);
                objRef.Referencia = Convert.ToString(reader["REFERENCIA"]);
                objRef.Archivo = Convert.ToString(reader["ARCHIVO"]);
                objRef.Comisionado = Convert.ToString(reader["COMISIONADO"]);
                objRef.Estatus = Convert.ToString(reader["ESTATUS"]);
                objRef.Importe = Convert.ToString(reader["IMPORTE"]);
                objRef.FechaPago = Convert.ToString(reader["FECHA_PAGO"]);
                objRef.Fecha = Convert.ToString(reader["FECHA"]);

                ListObtieneF.Add(objRef);
            }

            reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListObtieneF;
        }
    }
}
