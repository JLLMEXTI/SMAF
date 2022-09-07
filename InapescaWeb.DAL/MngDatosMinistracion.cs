/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosMinistracion.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		febrero 2016
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosMinistracion
    {
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static List<Entidad> ListaMinistracionesSolicitadas(string psArchivo, string psMinistracion)
        {
            List<Entidad> lista = new List<Entidad>();
            string Query = "";
            Query = "  SELECT CLV_CONCEPTO AS CODIGO, CONCEPTO AS DESCRIPCION";
            Query += " FROM crip_ministracion ";
            Query += " where 1=1  ";
            Query += " and ( ";
            Query += " (ARCHIVO = '" + psArchivo + "') ";
            Query += " OR (FOLIO_FACTURA = '" + psArchivo + "')";
            Query += " ) ";
            Query += "  and ESTATUS not in ( '0' ,'2')";
            Query += " AND FOLIO_MINISTRACION = '" + psMinistracion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                Entidad objGrid = new Entidad();
                objGrid.Codigo = Convert.ToString(Reader["CODIGO"]);
                objGrid.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                lista.Add(objGrid);
                objGrid = null;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lista;
        }

        public static bool UpdateEstatusMinistracion(string psEstatus, string psArchivo, string psMinistracion, string psConcepto)
        {
            bool resultado = false;

            string Query = "  UPDATE crip_ministracion SET ESTATUS = '" + psEstatus + "' ";
            Query += " WHERE  1=1  ";
            Query += " and (  ";
            Query += "     (    ARCHIVO = '" + psArchivo + "') ";
            Query += "  OR ( FOLIO_FACTURA = '" + psArchivo + "')";
            Query += "     ) ";
            Query += "     and ESTATUS not in ( '0' ,'2')";
            Query += " AND FOLIO_MINISTRACION = '" + psMinistracion + "'";
            Query += " and CLV_CONCEPTO = '" + psConcepto + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() >= 1) resultado = true;
            else resultado = false;

            MngConexion.disposeConexionSMAF(ConexionMysql );
            return resultado;

        }

        public static bool Update_MInistracion(string psPartida, string psEstatus, string psPagado, string psFecha, string psUsuarioPagador, string psBancoOrigen, string psCuentaOrigen, string psClabeOrigen, string psBancoDestino, string psCuentaDestino, string psClabeDestino, string psRefBancaria, string psDocumento, string psMinistracion, string psConcepto, string psRazonSocial, bool psRFC = false)
        {
            bool resultado = false;
            string Query = "";

            Query += "UPDATE crip_ministracion ";
            Query += "    SET ESTATUS = '" + psEstatus + "',";
            Query += "    TOTAL_PAGADO = '" + psPagado + "',";
            Query += " FECHA_PAGO = '" + psFecha + "',";
            Query += "  USUARIO_PAGADOR = '" + psUsuarioPagador + "',";
            Query += "  BANCO_ORIGEN = '" + psBancoOrigen + "',";
            Query += "      CUENTA_ORIGEN = '" + psCuentaOrigen + "', ";
            Query += "  CLABE_ORIGEN = '" + psClabeOrigen + "',";
            Query += " BANCO_DESTINO = '" + psBancoDestino + "',";
            Query += "   CUENTA_DESTINO = '" + psCuentaDestino + "',";
            Query += " CLABE_DESTINO = '" + psClabeDestino + "',";
            Query += "REF_BANCARIA = '" + psRefBancaria + "',";
            Query += "PARTIDA= '" + psPartida + "'";
            //agergar lo de los impuestos y retenciones

            Query += " WHERE 1=1";
            Query += "         AND ( ";
            Query += "         (ARCHIVO = '" + psDocumento + "') ";
            Query += "     OR (FOLIO_FACTURA = '" + psDocumento + "')";
            Query += "     ) ";
            Query += "  and ESTATUS not in ( '0' ,'2')";
            Query += " AND FOLIO_MINISTRACION = '" + psMinistracion + "'";

            if (psRFC)
            {
                Query += "     AND RFC = '" + psRazonSocial + "'";
            }
            else
            {
                Query += "     AND RAZON_SOCIAL = '" + psRazonSocial + "'";
            }
            Query += " AND CLV_CONCEPTO = '" + psConcepto + "'";



            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() >= 1) resultado = true;
            else resultado = false;
            MngConexion.disposeConexionSMAF(ConexionMysql );
            return resultado;
        }

        public static Ministracion Objeto_Ministracion_Completo(string psArchivo, string psMinistracion, string psConcepto, bool pbBandera = false )
        {
            string Query = " SELECT DISTINCT TIPO_MINISTRACION AS TIPO_MINISTRACION,";
            Query += "       CLV_PERIODO  AS CLV_PERIODO ,";
            Query += "      FOLIO_MINISTRACION AS FOLIO_MINISTRACION, ";
            Query += "      FECHA AS FECHA, ";
            Query += "      PERIODO  AS PERIODO , ";
            Query += "      USUARIO AS USUARIO,";
            Query += "      FECHA_AUT AS FECHA_AUT, ";
            Query += "      AUTORIZA AS AUTORIZA,  ";
            Query += "      FECHA_PAGO, ";
            Query += "      USUARIO_PAGADOR AS USUARIO_PAGADOR, ";
            Query += "      CLV_DEP AS CLV_DEP, ";
            Query += "      CLV_PROY , ";
            Query += "      UBICACION_PROY AS UBICACION_PROY, ";
            Query += "      DOCUMENTO AS DOCUMENTO, ";
            Query += "      ARCHIVO AS ARCHIVO , ";
            Query += "      FORMA_PAGO AS FORMA_PAGO, ";
            Query += "      REF_BANCARIA AS REF_BANCARIA, ";
            Query += "      BANCO_DESTINO AS BACO_DESTINO,";
            Query += "      CUENTA_DESTINO AS CUENTA_DESTINO, ";
            Query += "      CLABE_DESTINO AS CLABE_DESTINO ,";
            Query += "      RAZON_SOCIAL AS RAZON_SOCIAL, ";
            Query += "      RFC AS RFC, ";
            Query += "      FOLIO_FACTURA AS FOLIO_FACTURA,";
            Query += "      FECH_EMISION AS  FECH_EMISION, ";
            Query += "      CLV_CONCEPTO AS CLV_CONCEPTO, ";
            Query += "      CONCEPTO  AS CONCEPTO, ";
            Query += "      SUBTOTAL AS SUBTOTAL, ";
            Query += "      IVA AS IVA, ";
            Query += "      TUA AS TUA, ";
            Query += "      SFP AS SFP, ";
            Query += "      ISR AS ISR, ";
            Query += "      TOTAL_PAGAR AS TOTAL_PAGAR, ";
            Query += "      IVA_RETENIDO AS IVA_RETENIDO, ";
            Query += "      TUA_RETENIDO, ";
            Query += "      SFP_RETENIDO AS SFP_RETENIDO, ";
            Query += "      DESCUENTOS AS DESCUENTOS, ";
            Query += "      SUM(TOTAL_PAGADO) AS TOTAL_PAGADO, ";
            Query += "      FECH_RECEPCION AS FECH_RECEPCION, ";
            Query += "      PROCESO AS PROCESO, ";
            Query += "      PARTIDA AS PARTIDA, ";
            Query += "      ESTATUS AS ESTATUS, ";
            Query += "      SEC_EFF AS SECUENCIA, ";
            Query += "      OBSERVACIONES AS OBSERVACIONES, ";
            Query += "      BANCO_ORIGEN AS BANCO_ORIGEN, ";
            Query += "      CUENTA_ORIGEN AS CUENTA_ORIGEN, ";
            Query += "      CLABE_ORIGEN AS CLABE_ORIGEN";
            Query += "      FROM crip_ministracion  ";
            Query += "      where 1=1  ";
            Query += "      and ( ";
            Query += "      (    ARCHIVO = '" + psArchivo + "') ";
            Query += "      OR ( FOLIO_FACTURA = '" + psArchivo + "')";
            Query += "      ) ";
            Query += "      and ESTATUS not in ( '0' ,'2')";
            Query += "      AND FOLIO_MINISTRACION = '" + psMinistracion + "'";

            if (!pbBandera)
            {
                Query += "      and CLV_CONCEPTO = '" + psConcepto + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            Ministracion oMinistracion = new Ministracion();

            while (Reader.Read())
            {
                oMinistracion.Tipo_Ministracion = Convert.ToString(Reader["TIPO_MINISTRACION"]);
                oMinistracion.Clave_Periodo = Convert.ToString(Reader["CLV_PERIODO"]);
                oMinistracion.Folio = Convert.ToString(Reader["FOLIO_MINISTRACION"]);
                oMinistracion.Fecha = Convert.ToString(Reader["FECHA"]);
                oMinistracion.Periodo = Convert.ToString(Reader["PERIODO"]);
                oMinistracion.Usuario_Genera = Convert.ToString(Reader["USUARIO"]);
                oMinistracion.Fecha_Autoriza = Convert.ToString(Reader["FECHA_AUT"]);
                oMinistracion.Autoriza = Convert.ToString(Reader["AUTORIZA"]);
                oMinistracion.Fecha_Pago = Convert.ToString(Reader["FECHA_PAGO"]);
                oMinistracion.Usuario_Pagador = Convert.ToString(Reader["USUARIO_PAGADOR"]);
                oMinistracion.Ubicacion_Genera = Convert.ToString(Reader["CLV_DEP"]);
                oMinistracion.Proyecto = Convert.ToString(Reader["CLV_PROY"]);
                oMinistracion.Ubicacion_Proyecto = Convert.ToString(Reader["UBICACION_PROY"]);
                oMinistracion.Documento = Convert.ToString(Reader["DOCUMENTO"]);
                oMinistracion.Archivo = Convert.ToString(Reader["ARCHIVO"]);
                oMinistracion.Forma_Pago = Convert.ToString(Reader["FORMA_PAGO"]);
                oMinistracion.Referencia_Bancaria = Convert.ToString(Reader["REF_BANCARIA"]);
                oMinistracion.Banco_Destino = Convert.ToString(Reader["BACO_DESTINO"]);
                oMinistracion.Cuenta_Destino = Convert.ToString(Reader["CUENTA_DESTINO"]);
                oMinistracion.Clabe_destino = Convert.ToString(Reader["CLABE_DESTINO"]);
                oMinistracion.Razon_Social = Convert.ToString(Reader["RAZON_SOCIAL"]);
                oMinistracion.RFC = Convert.ToString(Reader["RFC"]);
                oMinistracion.Folio_Factura = Convert.ToString(Reader["FOLIO_FACTURA"]);
                oMinistracion.Fecha_Emision = Convert.ToString(Reader["FECH_EMISION"]);
                oMinistracion.Concepto = Convert.ToString(Reader["CLV_CONCEPTO"]);
                oMinistracion.Descripcion_Concepto = Convert.ToString(Reader["CONCEPTO"]);
                oMinistracion.Subtotal = Convert.ToString(Reader["SUBTOTAL"]);
                oMinistracion.IVA = Convert.ToString(Reader["IVA"]);
                oMinistracion.TUA = Convert.ToString(Reader["TUA"]);
                oMinistracion.SFP = Convert.ToString(Reader["SFP"]);
                oMinistracion.ISR = Convert.ToString(Reader["ISR"]);
                oMinistracion.Total_Solicitado = Convert.ToString(Reader["TOTAL_PAGAR"]);
                oMinistracion.Iva_Retenido = Convert.ToString(Reader["IVA_RETENIDO"]);
                oMinistracion.Tua_Retenido = Convert.ToString(Reader["TUA_RETENIDO"]);
                oMinistracion.SFP_Retenido = Convert.ToString(Reader["SFP_RETENIDO"]);
                oMinistracion.Descuentos = Convert.ToString(Reader["DESCUENTOS"]);
                oMinistracion.Total_Pagado = Convert.ToString(Reader["TOTAL_PAGADO"]);
                oMinistracion.Fecha_Recepcion = Convert.ToString(Reader["FECH_RECEPCION"]);
                oMinistracion.Proceso = Convert.ToString(Reader["PROCESO"]);
                oMinistracion.Partida = Convert.ToString(Reader["PARTIDA"]);
                oMinistracion.Estatus = Convert.ToString(Reader["ESTATUS"]);
                oMinistracion.Secuencia = Convert.ToString(Reader["SECUENCIA"]);
                oMinistracion.Observaciones = Convert.ToString(Reader["OBSERVACIONES"]);
                oMinistracion.Banco_Origen = Convert.ToString(Reader["BANCO_ORIGEN"]);
                oMinistracion.Cuenta_Destino = Convert.ToString(Reader["CUENTA_ORIGEN"]);
                oMinistracion.Clabe_Origen = Convert.ToString(Reader["CLABE_ORIGEN"]);

            }

            MngConexion.disposeConexionSMAF (ConexionMysql );
            Reader.Close();
            return oMinistracion;

        }


        /*     public static Ministracion Objeto_Ministracion(string psArchivo, string psMinistracion, string psConcepto)
              {

                  string Query = " SELECT DISTINCT TIPO_MINISTRACION AS TIPO_MINISTRACION, ";
                  Query += " FOLIO_MINISTRACION AS FOLIO_MINISTRACION,";
                  Query += " FECHA AS FECHA,";
                  Query += " CLV_PERIODO  AS CLV_PERIODO";
                  Query += " PERIODO  AS PERIODO ,";
                  Query += " CLV_CONCEPTO AS CLV_CONCEPTO,";
                  Query += " TOTAL_PAGAR AS TOTAL_PAGAR,";
                  Query += " SUM(TOTAL_PAGADO) AS TOTAL_PAGADO,";
                  Query += " CLV_PERIODO AS CLV_PERIODO";
                  Query += " FROM crip_ministracion  ";
                  Query += " where 1=1  ";
                  Query += " and ( ";
                  Query += " (ARCHIVO = '" + psArchivo + "') ";
                  Query += " OR (FOLIO_FACTURA = '" + psArchivo + "')";
                  Query += " ) ";
                  Query += "  and ESTATUS not in ( '0' ,'2')";
                  Query += " AND FOLIO_MINISTRACION = '" + psMinistracion + "'";
                  Query += "and CLV_CONCEPTO = '" + psConcepto + "'";

                  MySqlCommand cmd = new MySqlCommand(Query, MngConexion.getConexionMysql());
                  cmd.Connection.Open();
                  MySqlDataReader Reader = cmd.ExecuteReader();

                  Ministracion oMinistracion = new Ministracion();

                  while (Reader.Read())
                  {  
                      oMinistracion.Tipo_Ministracion = Convert.ToString(Reader["TIPO_MINISTRACION"]);
                      oMinistracion.Clave_Periodo = Convert.ToString(Reader["CLV_PERIODO"]);
                      oMinistracion.Folio = Convert.ToString(Reader["FOLIO_MINISTRACION"]);
                      oMinistracion.Fecha = Convert.ToString(Reader["FECHA"]);
                      oMinistracion.Periodo = Convert.ToString(Reader["PERIODO"]);
                      oMinistracion.Concepto = Convert.ToString(Reader["CLV_CONCEPTO"]);
                      oMinistracion.Total_Solicitado = Convert.ToString(Reader["TOTAL_PAGAR"]);
                      oMinistracion.Total_Pagado = Convert.ToString(Reader["TOTAL_PAGADO"]);
                      oMinistracion.Clave_Periodo = Convert.ToString(Reader["CLV_PERIODO"]);
                  }
           
                  MngConexion.disposeConexion();
                  Reader.Close();
                  return oMinistracion;
              }
              */

        public static List<Entidad> Lista_tipo_Pagos()//string psTipo)
        {
            List<Entidad> lista = new List<Entidad>();
            string Query = "";
            Query = "SELECT ID AS CODIGO, DESCRIPCION AS DESCRIPCION ";
            Query += "        FROM crip_pagos  ";
            Query += " WHERE ESTATUS = '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                Entidad objGrid = new Entidad();
                objGrid.Codigo = Convert.ToString(Reader["CODIGO"]);
                objGrid.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                lista.Add(objGrid);
                objGrid = null;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lista;
        }



        public static List<Entidad> ListaTipoMinistracion(string psOpcion)
        {
            List<Entidad> lista = new List<Entidad>();
            string Query = "";
            if (psOpcion == "00")
            {
                Query += "  SELECT CLV_TIPOPAGO AS CODIGO, DESCRIPCION AS DESCRIPCION ";
                Query += " FROM crip_tipopago ";
                Query += "   WHERE ESTATUS = '1' ";
                Query += "  AND CLV_TIPOPAGO NOT IN ('4')";
            }
            else
            {
                Query += "  SELECT CLV_TIPOPAGO AS CODIGO, DESCRIPCION AS DESCRIPCION ";
                Query += " FROM crip_tipopago ";
                Query += "   WHERE ESTATUS = '1' ";
                Query += "  AND CLV_TIPOPAGO IN ('0','4')";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();


            while (Reader.Read())
            {
                Entidad objGrid = new Entidad();
                objGrid.Codigo = Convert.ToString(Reader["CODIGO"]);
                objGrid.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                lista.Add(objGrid);
                objGrid = null;
            }

            Reader.Close();

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lista;
        }

        public static Boolean Inserta_MInistracionDetalle(string psTipoMinistracion, string psFolio, string psFecha, string psPeriodo, string psUsuario, string psDep, string psFechaAut, string psUsuarioAut, string psFechaPago, string psUsuarioPagador,string psConcepto,string psTotalSolicitado, string psTotalPagado,string psEstatus,string psSecuencia,string psFechEff,string psClvPeriodo,string psBancoOrigen, string psCuentaOrigen,string psClabeOrigen , string psRefBan,string psBancoDestino,string psCuentaDestino, string psClabeDestino)
        {
            bool lbResultado = false;
            string Query = "";

            Query += "  INSERT INTO  crip_ministracion_detalle ";
            Query += "  ( ";
            Query += "  TIPO_MINISTRACION, ";
            Query += "  FOLIO, ";
            Query += "  FECHA, ";
            Query += "  PERIODO, ";
            Query += "  USUARIO    ,";
            Query += "  CLV_DEP,";
            Query += "  FECHA_AUT   ,";
            Query += "  USUARIO_AUT     ,";
            Query += "  FECHA_PAGO     ,";
            Query += "  USUARIO_PAGADOR   ,";
            Query += "  CONCEPTO  ,";
            Query += "  TOTAL_SOLICITADO,";
            Query += "  TOTAL_PAGADO,";
            Query += "  ESTATUS,";
            Query += "  SEC_EFF,";
            Query += "  DATE_EFF    ,";
            Query += "  CLAVE_PERIODO,";
            Query += "  BANCO_ORIGEN,";
            Query += "  CUENTA_ORIGEN,";
            Query += "  CLABE_ORIGEN,";
            Query += "  REF_BANCARIA,";
            Query += "  BANCO_DESTINO,";
            Query += "  CUENTA_DESTINO, ";
            Query += "  CLABE_DESTINO ";
            Query += "  ) ";
            Query += "  VALUES ";
            Query += "  ( ";
            Query += "  '"+ psTipoMinistracion +"',";
            Query += " '" + psFolio +"',";
            Query += " '" + psFecha  +"' ,";
            Query += " '"+ psPeriodo+"',";
            Query += " '" + psUsuario + "',";
            Query += " '"+ psDep + "'    ,";
            Query += " '" + psFechaAut +"',";
            Query += "  '" + psUsuarioAut  + "',";
            Query += " '" + psFechaPago +"'   ,";
            Query += " '" + psUsuarioPagador  + "',";
            Query += " '"+ psConcepto +"',";
            Query += " '"+ psTotalSolicitado +"'          ,";
            Query += " '" + psTotalPagado+"'         ,";
            Query += " '"+ psEstatus +"'    ,";
            Query += " '" + psSecuencia +"'    ,";
            Query += " '" + psFechEff +"',";
            Query += " '"+ psClvPeriodo+"'        ,";
            Query += " '" + psBancoOrigen + "'          ,";
            Query += " '" + psCuentaOrigen+ "'           ,";
            Query += " '" + psClabeOrigen + "'          ,";
            Query += " '" + psRefBan + "'           ,";
            Query += " '" + psBancoDestino +"'           ,";
            Query += "'" + psCuentaDestino +"'           ,";
            Query += " '" + psClabeDestino  +"'";
            Query += "     )";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() >= 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lbResultado;
        }


        public static List<Entidad> Obtiene_Ministracion(string psPeriodo, string psArchivo)
        {
            List<Entidad> lista = new List<Entidad>();
            /*   string Query = "   SELECT FOLIO AS CODIGO, FOLIO AS DESCRIPCION";
                      Query += "  FROM crip_ministracion_detalle  ";
                      Query += "  WHERE 1=1 " ; 
                      Query += "  AND PERIODO = '" +psPeriodo  +"'";
                      Query += "  AND USUARIO = '" + psUsuario + "'";
                      Query += "  AND CLV_DEP= '" +psDep +"'";
                      Query += "  AND ESTATUS  <> '0'";

                      MySqlCommand cmd = new MySqlCommand(Query, MngConexion.getConexionMysql());
                      cmd.Connection.Open();

                      MySqlDataReader Reader = cmd.ExecuteReader();

                      Entidad objGrid1 = new Entidad();
                      objGrid1.Codigo = "0";
                      objGrid1.Descripcion = "= S E L E C C I O N E = ";
                      lista.Add(objGrid1);
                      objGrid1 = null;

                      Entidad objGrid2 = new Entidad();
                      objGrid2.Codigo = "00";
                      objGrid2.Descripcion = "NUEVA MINISTRACION";
                      lista.Add(objGrid2);
                      objGrid2 = null;

                            while (Reader.Read())
                          {
                              Entidad objGrid = new Entidad();
                              objGrid.Codigo = Convert.ToString(Reader["CODIGO"]);
                              objGrid.Descripcion ="MIN-" + Convert.ToString(Reader["DESCRIPCION"]);
                              lista.Add(objGrid);
                              //objGrid = null;
                          }

                         // Reader.Close();
                    
                   MngConexion.disposeConexion();

               return lista;*/

            string Query = "";
            Query = "SELECT DISTINCT  FOLIO_MINISTRACION AS CODIGO,PERIODO";
            Query += "    FROM crip_ministracion ";
            Query += "    WHERE 1=1";
            Query += "    AND ESTATUS = '1' ";
            Query += "    AND PERIODO = '" + psPeriodo + "'";
            Query += "    AND FOLIO_FACTURA = '" + psArchivo + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                Entidad objGrid = new Entidad();
                objGrid.Codigo = Convert.ToString(Reader["CODIGO"]);
                objGrid.Descripcion = "MIN " + Convert.ToString(Reader["CODIGO"]) + " " + Convert.ToString(Reader["PERIODO"]) + "|" + psArchivo;
                lista.Add(objGrid);
                //objGrid = null;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lista;
        }

        public static Boolean Inserta_Ministracion(string psTipoMinistracion, string psClvPeriodo, string psFolio, string psFecha, string psPeriodo, string psUsuario, string psFechaAut, string psAutoriza, string psFechaPago, string psUsuarioPagador, string psUbicacion, string psProyecto, string psUbicaProy, string psDocumento, string psArchivo, string psTipoPago, string psRefBanc, string psBanco, string psCuenta, string psClabe, string psRazon, string psRFC, string psOficio, string psFechaEmision, string psClvConcepto, string psConcepto, string psSubtotal, string psIVA, string psTUA, string psSFP, string psISR, string psTotalPagar, string psIvaRet, string psIsrRet, string psTuaRet, string psSfpRet, string psDescuentos, string psTotalPagado, string psFechaRecep, string psComponente, string psPartida, string psEstatus, string psSecuencia, string psObservaciones, string psBancoOrigen, string psCuentaOrigen, string psClabeOrigen)
        {
            Boolean lbResultado;

            string Query = "      INSERT INTO crip_ministracion ";
            Query += "    ( ";
            Query += "	  TIPO_MINISTRACION,";//
            Query += "    CLV_PERIODO, ";//
            Query += "    FOLIO_MINISTRACION, ";//
            Query += "    FECHA, ";//
            Query += "    PERIODO, ";//
            Query += "    USUARIO, ";//
            Query += "    FECHA_AUT, ";//
            Query += "    AUTORIZA, ";//
            Query += "    FECHA_PAGO, ";//
            Query += "    USUARIO_PAGADOR, ";//
            Query += "    CLV_DEP, ";//
            Query += "    CLV_PROY, ";//
            Query += "    UBICACION_PROY , ";//
            Query += "    DOCUMENTO, ";//
            Query += "    ARCHIVO, ";//
            Query += "	  FORMA_PAGO, ";//
            Query += "    REF_BANCARIA, ";//
            Query += "    BANCO_DESTINO , ";//
            Query += "    CUENTA_DESTINO , ";//
            Query += "    CLABE_DESTINO, ";//
            Query += "    RAZON_SOCIAL, ";//
            Query += "    RFC, ";//
            Query += "    FOLIO_FACTURA, ";//
            Query += "    FECH_EMISION, ";//
            Query += "    CLV_CONCEPTO, ";//
            Query += "    CONCEPTO,  ";//
            Query += "    SUBTOTAL, ";//
            Query += "    IVA, ";//
            Query += "    TUA, ";//
            Query += "    SFP, ";//
            Query += "    ISR, ";//
            Query += "    TOTAL_PAGAR,  ";//
            Query += "    IVA_RETENIDO, ";//
            Query += "    ISR_RETENIDO, ";//
            Query += "    TUA_RETENIDO, ";//
            Query += "    SFP_RETENIDO, ";//
            Query += "    DESCUENTOS, ";//
            Query += "	 TOTAL_PAGADO, ";//
            Query += "    FECH_RECEPCION, ";//
            Query += "    PROCESO,  ";//
            Query += "    PARTIDA,  ";//
            Query += "    ESTATUS,  ";//
            Query += "    SEC_EFF,  ";//
            Query += "    FECH_EFF, ";//
            Query += "	  OBSERVACIONES, ";//
            Query += "	  BANCO_ORIGEN, ";
            Query += "	  CUENTA_ORIGEN, ";
            Query += "	  CLABE_ORIGEN ";
            Query += " ) ";

            Query += "  VALUES ";
            Query += " ( ";
            Query += " '" + psTipoMinistracion + "',";

            if (psClvPeriodo != "")
            {
                Query += "'" + psClvPeriodo + "',";
            }
            else
            {
                Query += " (SELECT CLV_PERIODO FROM crip_ministracion_periodos WHERE '" + lsHoy + "' BETWEEN INICIO AND FIN AND ANIO = '"+ psPeriodo +"' AND ESTATUS =  '1'),";
            }


            if (psFolio != "")
            {
                Query += "'" + psFolio + "',";
            }
            else
            {
                Query += "'" + Obtiene_Max_Ministracion() + "',";
            }

            Query += "'" + clsFunciones.FormatFecha(psFecha) + "',";

            if (psPeriodo != "")
            {
                Query += "'" + psPeriodo + "',";
            }
            else
            {
                Query += "'" + Year + "',";
            }


            Query += " '" + psUsuario + "', ";

            if (psFechaAut != "")
            {
                Query += " '" + clsFunciones.FormatFecha(psFechaAut) + "',";
            }
            else
            {
                Query += " '1900-01-01',";
            }

            Query += " '" + psAutoriza + "',";

            if (psFechaPago != "")
            {
                Query += "'" + clsFunciones.FormatFecha(psFechaPago) + "',";
            }
            else
            {
                Query += " '1900-01-01',";
            }

            Query += "'" + psUsuarioPagador + "',";

            Query += " '" + psUbicacion + "', ";
            Query += " '" + psProyecto + "', ";
            Query += " '" + psUbicaProy + "', ";

            Query += " '" + psDocumento + "',";
            Query += " '" + psArchivo + "',";

            Query += " '" + psTipoPago + "', ";
            Query += " '" + psRefBanc + "', ";
            Query += " '" + psBanco + "', ";
            Query += " '" + psCuenta + "', ";
            Query += " '" + psClabe + "', ";
            Query += " '" + psRazon + "', ";
            Query += " '" + psRFC + "', ";
            Query += " '" + psOficio + "', ";
            Query += " '" + clsFunciones.FormatFecha(psFechaEmision) + "', ";
            Query += " '" + psClvConcepto + "',";
            Query += " '" + psConcepto + "',";

            Query += " '" + psSubtotal + "',";
            if ((psIVA == "") | (psIVA == null))
            {
                Query += " '0',";
            }
            else
            {
                Query += " '" + psIVA + "',";
            }

            if ((psTUA == "") | (psTUA == null))
            {
                Query += " '0',";
            }
            else
            {
                Query += " '" + psTUA + "',";
            }

            if ((psSFP == "") | (psSFP == null))
            {
                Query += " '0',";
            }
            else
            {
                Query += " '" + psSFP + "',";

            }

            if ((psISR == null) | (psISR == ""))
            {
                Query += " '0',";
            }
            else
            {
                Query += " '" + psISR + "',";
            }


            Query += " '" + psTotalPagar + "',";

            if ((psIvaRet == null) | (psIvaRet == ""))
            {
                Query += "'0',";
            }
            else
            {
                Query += "'" + psIvaRet + "',";
            }

            if ((psIsrRet == null) | (psIsrRet == ""))
            {
                Query += " '0',";
            }
            else
            {
                Query += " '" + psIsrRet + "',";
            }

            if ((psTuaRet == "") | (psTuaRet == null))
            {
                Query += " '0',";
            }
            else
            {
                Query += " '" + psTuaRet + "',";
            }

            if ((psSfpRet == "") | (psSfpRet == null))
            {
                Query += "'0', ";
            }
            else
            {
                Query += "'" + psSfpRet + "', ";
            }

            if ((psDescuentos == null) | (psDescuentos == ""))
            {
                Query += " '0', ";
            }
            else
            {
                Query += " '" + psDescuentos + "', ";
            }


            Query += " '" + psTotalPagado + "',";

            Query += "'" + clsFunciones.FormatFecha(psFechaRecep) + "',";
            Query += " '" + psComponente + "',";
            Query += " '" + psPartida + "',";

            Query += " '" + psEstatus + "',";
            Query += " '" + psSecuencia + "',";

            Query += "'" + lsHoy + "',";
            Query += "'" + psObservaciones + "',";

            Query += "'" + psBancoOrigen + "',";
            Query += "'" + psCuentaOrigen + "',";
            Query += "'" + psClabeOrigen + "'";

            Query += ")";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() >= 1)
            {
                lbResultado = true;
            }
            else
            {
                lbResultado = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return lbResultado;
        }


        public static string Obtiene_ClvPeriodo(string psFecha, string psYear)
        {
            string periodo;  
            string Query = "SELECT CLV_PERIODO FROM crip_ministracion_periodos WHERE '" + psFecha  + "' BETWEEN INICIO AND FIN AND ANIO = '"+ psYear  +"' AND ESTATUS =  '1'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
              cmd.Connection.Open();
            
            MySqlDataReader Reader = cmd.ExecuteReader();

              if (Reader.Read())
              {
                  periodo = Convert.ToString(Reader["CLV_PERIODO"]);
              }
              else
              {
                  periodo = "1";
              }
              MngConexion.disposeConexionSMAF(ConexionMysql);
            return periodo;
        }

        public static string Obtiene_Max_Ministracion()
        {
            int liNumero;
            string Query = "", max;

            Query = "SELECT MAX(FOLIO) AS MAX  FROM crip_ministracion_detalle ";

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

        public static string ArchivoInMinistracion(string psArchivo)
        {
            string Query = "";
            string resultado = "";

            Query = " SELECT DISTINCT FOLIO_MINISTRACION AS FOLIO ";
            Query += " FROM crip_ministracion ";
            Query += " where 1=1  ";
            Query += " and ( ";
            Query += " (ARCHIVO = '" + psArchivo + "') ";
            Query += " OR (FOLIO_FACTURA = '" + psArchivo + "')";
            Query += " ) ";
            Query += " and ESTATUS != '0' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                resultado = Convert.ToString(Reader["FOLIO"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return resultado;

        }


        public static Entidad PagadoVsPagar(string psArchivo, string psMinistracion)
        {
            string Query = " select SUM(TOTAL_PAGAR) AS CODIGO, SUM(TOTAL_PAGADO) AS DESCRIPCION ";
            Query += " from crip_ministracion";
            Query += " WHERE  1=1  ";
            Query += "         and ( ";
            Query += "	        (    ARCHIVO = '" + psArchivo + "') ";
            Query += "  OR ( FOLIO_FACTURA = '" + psArchivo + "')";
            Query += "         ) ";
            Query += " and ESTATUS not in ( '0') ";
            Query += "  AND FOLIO_MINISTRACION = '" + psMinistracion + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            Entidad oMinistracion = new Entidad();

            while (Reader.Read())
            {
                oMinistracion.Codigo = Convert.ToString(Reader["CODIGO"]);
                oMinistracion.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );
            Reader.Close();
            return oMinistracion;

        }

    }
}
