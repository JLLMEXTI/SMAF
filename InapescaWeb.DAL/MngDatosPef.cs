/// <summary>   
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosPef.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
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
using System.Data.OleDb;


namespace InapescaWeb.DAL
{
    public class MngDatosPef
    {
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        static System.Data.OleDb.OleDbDataAdapter adapter;
        static System.Data.OleDb.OleDbConnection conexionaexcel;
        static DataSet dataset;
        static string cadenaConexionArchivosexcel;

        public static Boolean Inserta_Pef_Detalle(Pef poPef,string psSec,string psUsuario,string psFile)
        {
            if ((poPef.UR == null) | (poPef.UR == ""))
            { 
            poPef.UR = "RJL";
            }

            Boolean lbResultado = false;
            string Query = "INSERT INTO crip_pef_detalle ";
            Query += " ( ";
            Query += "     UR,";
            Query += " PEF,";
            Query += "  ENERO,";
            Query += " FEBRERO,";
            Query += " MARZO,";
            Query += " ABRIL,";
            Query += " MAYO,";
            Query += " JUNIO,";
            Query += " JULIO,";
            Query += " AGOSTO,";
            Query += " SEPTIEMBRE,";
            Query += " OCTUBRE,";
            Query += " NOVIEMBRE,";
            Query += " DICIEMBRE,";
            Query += " ESTATUS,";
            Query += " SEC_EFF,";
            Query += " FECHA,";
            Query += " FEC_EFF,";
            Query += " ANIO,";
            Query += " USUARIO,";
            Query += " DOCUMENTO";
            Query += " )";
            Query += " VALUES";
            Query += "  ( ";
            Query += "'" + poPef.UR + "',	";
            Query += "  '" + poPef.PEF + "',";
            Query += " '" + poPef.ENERO + "', ";
            Query += " '" + poPef.FEBRERO + "', 	 ";
            Query += " '" + poPef.MARZO + "', 	 ";
            Query += " '" + poPef.ABRIL + "',";
            Query += "'" + poPef.MAYO + "',";
            Query += "  '" + poPef.JUNIO + "', 	 ";
            Query += " '" + poPef.JULIO + "',";
            Query += " '" + poPef.AGOSTO + "',";
            Query += "'" + poPef.SEPTIEMBRE + "', ";
            Query += " '" + poPef.OCTUBRE + "',";
            Query += " '" + poPef.NOVIEMBRE + "',";
            Query += " '" + poPef.DICIEMBRE + "',";
            Query += " '1',";
            Query += " '" + psSec + "',";
            Query += " '" + lsHoy + "',";
            Query += "'" + lsHoy + "',";
            Query += " '" + Year + "',";
            Query += " '" + psUsuario  + "',";
            Query += " '" + psFile + "'";
            Query += " )";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    lbResultado = true;
                }
                else
                {
                    lbResultado = false;
                }

            }
            catch (Exception e)
            {
                
            }
            MngConexion.disposeConexionSMAF(ConexionMysql );


            return lbResultado;
        }

        public static Boolean Inserta_Pef(Pef poPef,string psSec)
        {
            Boolean lbResultado = false;
            string Query = "INSERT INTO crip_pef ";
               Query += " ( ";
               Query += "     UR,";
               Query += " UE,";
               Query += " EDO,";
               Query += " FI,";
               Query += " FU,";
               Query += " SF,";
               Query += " RG,";
               Query += " AI,";
               Query += " PP,";
               Query += " CP,";
               Query += " PARTIDA,";
               Query += " TG,";
               Query += " FF,";
               Query += " PPI,";
               Query += " PEF,";
               Query += "  ENERO,";
               Query += " FEBRERO,";
               Query += " MARZO,";
               Query += " ABRIL,";
               Query += " MAYO,";
               Query += " JUNIO,";
               Query += " JULIO,";
               Query += " AGOSTO,";
               Query += " SEPTIEMBRE,";
               Query += " OCTUBRE,";
               Query += " NOVIEMBRE,";
               Query += " DICIEMBRE,";
               Query += " ESTATUS,";
               Query += " SEC_EFF,";
               Query += " FECHA,";
               Query += " FECHA_EFF,";
               Query += " ANIO";
               Query  += " )";
               Query += " VALUES";
               Query += "  ( ";
               Query += "'"+ poPef .UR +"',	";
               Query += " '"+ poPef .UE +"'	,";
               Query += " '"+poPef .EDO +"'	,";
               Query += " '"+ poPef.FI +"',";
               Query += " '"+ poPef.FU +"',";
               Query += " '"+ poPef .SF+"',";
               Query += " '"+poPef .RG+ "',";
               Query += " '" + poPef .AI + "',";
               Query += " '"+ poPef .PP +"',";
               Query += " '"+ poPef .CP +"',";
               Query += " '" + poPef .PARTIDA + "',";
               Query += " '" + poPef.TG +"',";
               Query += " '" + poPef .FF + "',";
               Query += " '" +poPef .PPI + "', ";
               Query += "  '"+poPef.PEF +"',";
               Query += " '"+ poPef .ENERO +"', ";
               Query += " '"+poPef.FEBRERO +"', 	 ";
               Query += " '" + poPef .MARZO+"', 	 ";
               Query += " '" + poPef .ABRIL + "',";
               Query  += "'"+poPef .MAYO +"',";
               Query += "  '" + poPef .JUNIO +"', 	 ";
               Query += " '" + poPef .JULIO + "',";
               Query += " '"+poPef .AGOSTO +"',";
               Query += "'"+ poPef.SEPTIEMBRE+"', ";
               Query += " '"+ poPef .OCTUBRE +"',";
               Query += " '" + poPef .NOVIEMBRE + "',";
               Query += " '" + poPef.DICIEMBRE + "',";
               Query += " '1',";
               Query  += " '"+psSec +"',";
               Query  += " '"+lsHoy +"',";
               Query  += "'" + lsHoy +"',";
               Query += " '" + Year +"'";
               Query  += " )";

               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
               cmd.Connection.Open();
               try
               {
                   if (cmd.ExecuteNonQuery() == 1)
                   {
                       lbResultado = true;
                   }
                   else
                   {
                       lbResultado = false;
                   }

               }
               catch (Exception e)
               { 
               
               }

               MngConexion.disposeConexionSMAF(ConexionMysql  );

               return lbResultado;
        }


        public static DataSet  Lee_Excel_Pef(string psArchivo, string psHoja, string psExtencion,string psNombreArchivo)
        {
            string consultaHojaExcel = "Select * from ["+  psHoja + "$]"  ;
          

            if (psExtencion == "xls")
            {
                cadenaConexionArchivosexcel = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + psArchivo + "';Extended Properties=\"Excel 8.0;";
            }
            else
            {
                cadenaConexionArchivosexcel = @"provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + psArchivo + "';Extended Properties=\"Excel 12.0;HDR=Yes\"";
            }
         
            conexionaexcel = new OleDbConnection(cadenaConexionArchivosexcel);
          //  conexionaexcel.Open();
           
         OleDbCommand cmd = new OleDbCommand(consultaHojaExcel, conexionaexcel);
            
            adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;

            dataset = new DataSet();
            adapter.Fill(dataset);
            adapter.Dispose();
            conexionaexcel.Close();
          
            return dataset ;
        }

        public static int Obtiene_pef()
        {
            int count ;
        string query  = "  SELECT COUNT(*)  AS NUMERO  FROM crip_pef_detalle";
               query += "     where ESTATUS = '1'";
               query += " AND ANIO = '" + Year + "'";
               MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
               MySqlCommand cmd = new MySqlCommand(query , ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                count = clsFunciones.ConvertInteger(Convert.ToString(Reader["NUMERO"]));
            }
            else { count = 0; }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return count;
        }

        public static Pef Obtiene_Pef()
        {
            string Query = "SELECT  UR,";
            Query += " UE,";
            Query += " EDO,";
            Query += " FI,";
            Query += " FU,";
            Query += " SF,";
            Query += " RG,";
            Query += "AI,";
            Query += " PP,";
            Query += " CP,";
            Query += " PARTIDA,";
            Query += " TG,";
            Query += " FF,";
            Query += " PPI,";
            Query += " PEF,";
            Query += " ENERO,";
            Query += " FEBRERO,";
            Query += " MARZO,  ";
            Query += " ABRIL,";
            Query += " MAYO,";
            Query += " JUNIO,";
            Query += " JULIO,  ";
            Query += " AGOSTO,";
            Query += " SEPTIEMBRE,";
            Query += " OCTUBRE,	";
            Query += " NOVIEMBRE,";
            Query += " DICIEMBRE,";
            Query += " SEC_EFF,";
            Query += " ANIO";
            Query += " FROM crip_pef";
            Query += " where anio = '" + Year + "'";
            Query += " and ESTATUS = '1'";

            Pef objPef = new Pef();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                //   objWebPage.Modulo = Convert.ToString(Reader["MODULO"]);
                objPef.UR = Convert.ToString(Reader["UR"]);
                objPef.UE = Convert.ToString(Reader["UE"]);
                objPef.EDO = Convert.ToString(Reader["EDO"]);
                objPef.FI = Convert.ToString(Reader["FI"]);
                objPef.FU = Convert.ToString(Reader["FU"]);
                objPef.SF = Convert.ToString(Reader["SF"]);
                objPef.RG = Convert.ToString(Reader["RG"]);
                objPef.AI = Convert.ToString(Reader["AI"]);
                objPef.PP = Convert.ToString(Reader["PP"]);
                objPef.CP = Convert.ToString(Reader["CP"]);
                objPef.PARTIDA = Convert.ToString(Reader["PARTIDA"]);
                objPef.TG = Convert.ToString(Reader["TG"]);
                objPef.FF = Convert.ToString(Reader["FF"]);
                objPef.PPI = Convert.ToString(Reader["PPI"]);
                objPef.PEF = Convert.ToString(Reader["PEF"]);
                objPef.ENERO = Convert.ToString(Reader["ENERO"]);
                objPef.FEBRERO = Convert.ToString(Reader["FEBRERO"]);
                objPef.MARZO = Convert.ToString(Reader["MARZO"]);
                objPef.ABRIL = Convert.ToString(Reader["ABRIL"]);
                objPef.MAYO = Convert.ToString(Reader["MAYO"]);
                objPef.JUNIO = Convert.ToString(Reader["JUNIO"]);
                objPef.JULIO = Convert.ToString(Reader["JULIO"]);
                objPef.AGOSTO = Convert.ToString(Reader["AGOSTO"]);
                objPef.SEPTIEMBRE = Convert.ToString(Reader["SEPTIEMBRE"]);
                objPef.OCTUBRE = Convert.ToString(Reader["OCTUBRE"]);
                objPef.NOVIEMBRE = Convert.ToString(Reader["NOVIEMBRE"]);
                objPef.DICIEMBRE = Convert.ToString(Reader["DICIEMBRE"]);
                objPef.SECUENCIA = Convert.ToString(Reader["SEC_EFF"]);
                objPef.ANIO = Convert.ToString(Reader["ANIO"]);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql );

            return objPef;

        }
    }
}
