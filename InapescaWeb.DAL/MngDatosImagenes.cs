/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosImagenes.cs
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
using System.Net;
using System.Net.Mime;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.IO;
using InapescaWeb;

namespace InapescaWeb.DAL
{
    public class MngDatosImagenes
    {
        static clsDictionary Dictionary = new clsDictionary();

        public static bool CargarImagen(string psClave, string psDescripcion, string psNombreCorto, string psFecha, Byte[] byteImage)
        {
            Boolean pbResult;


            string Query = Dictionary.CADENA_NULA;
            string Year = DateTime.Today.Year.ToString();

            //FileStream streamFicheroMemoria = new System.IO.FileStream(rutaFicheroInsertarBD, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //BinaryReader readerBinario = new BinaryReader(streamFicheroMemoria);
            //FileInfo fichero = new FileInfo(rutaFicheroInsertarBD);
            //ficheroBLOB = readerBinario.ReadBytes((int)(fichero.Length));

            Query = "INSERT INTO crip_secretaria ( ";
            Query += " CLV_SECRETARIA , ";
            Query += " DESCR ,";
            Query += " NOM_CORTO , ";
            Query += " FECHA ,";
            Query += " FECH_EFF , ";
            Query += " SECEFF , ";
            Query += " ESTATUS, ";
            Query += " ANIO ,";
            Query += " IMAGE ";
            Query += " ) ";
            Query += "VALUES ";
            Query += " ( ";
            Query += "'" + psClave + "',";
            Query += "'" + psDescripcion + "',";
            Query += "'" + psNombreCorto + "',";
            Query += "'" + psFecha + "',";
            Query += "'" + psFecha + "',";
            Query += "'1',";
            Query += "'" + Year + "',";
            Query += "@IMAGE";
            Query += ")";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Parameters.Add("@IMAGE", MySqlDbType.LongBlob);
            cmd.Parameters["@IMAGE"].Value = byteImage;
            
            cmd.Connection.Open();

            //MySqlParameter parametroLongBlob = new MySqlParameter("?IMAGE", MySqlDbType.LongBlob);
            //parametroLongBlob.Size = ficheroBLOB.Length;
            //parametroLongBlob.Value = ficheroBLOB;
            //cmd.Parameters.Add(parametroLongBlob);

            if (cmd.ExecuteNonQuery() > 0)
            {
                pbResult = true;
            }
            else
            {
                pbResult = false;
            }

            //readerBinario.Close();
            //readerBinario.Dispose();
            //streamFicheroMemoria.Close();
            //streamFicheroMemoria.Dispose();
            //streamFicheroMemoria = null;
            //readerBinario = null;
            MngConexion.disposeConexionSMAF (ConexionMysql );

            return pbResult;

        }

    }
}
