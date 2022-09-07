/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngFtp.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		julio 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Net;


namespace InapescaWeb.DAL
{
  public   class MngFtp
    {
      static string serverFtpEncriptado = ConfigurationManager.AppSettings["serveFtp"];
      static string usserFtpEncriptado = ConfigurationManager.AppSettings["usserFtp"];
      static string passFtpEncriptado = ConfigurationManager.AppSettings["passFtp"];

      public static bool Valida_Conexion_ftp(string psFolder = "")
      {
          bool bExiste = true;

          try
          {
              string ruta = MngEncriptacion.decripString(serverFtpEncriptado );// +psFolder;
              if (psFolder != "")
              {
                  ruta += "/" + psFolder;
              }

              FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(ruta );//( );
              ftp.Credentials = new NetworkCredential(MngEncriptacion.decripString (usserFtpEncriptado ),MngEncriptacion.decripString (passFtpEncriptado ));
         ftp.Method = WebRequestMethods.Ftp.ListDirectory;
              ftp.KeepAlive = false;
              ftp.Proxy = null;

              FtpWebResponse respuesta = (FtpWebResponse)ftp.GetResponse();
              respuesta.Close();
              ftp = null;
          }
          catch (WebException ex)
          {
              if (ex.Response != null)
              {
                  FtpWebResponse respuesta = (FtpWebResponse)ex.Response;
                  if (respuesta.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                  {
                      respuesta.Close();
                      bExiste = false;
                  }
              }
          }
          
          return bExiste;
      }

      //Crea Directorio ftp que se le pasa por parametro
      public static string creaDirectorio_ftp(string psDir)
      {
          FtpWebRequest peticionFTP;
          string ruta = MngEncriptacion.decripString(serverFtpEncriptado);// +psFolder;
        
          if (psDir  != "")
          {
              ruta += "/" + psDir ;
          }
          
          // Creamos una peticion FTP con la dirección del directorio que queremos crear 
          peticionFTP = (FtpWebRequest)WebRequest.Create(new Uri(ruta ));
          peticionFTP.KeepAlive = true;

          // Fijamos el usuario y la contraseña de la petición 
          peticionFTP.Credentials = new NetworkCredential(MngEncriptacion.decripString(usserFtpEncriptado), MngEncriptacion.decripString(passFtpEncriptado));

          // Seleccionamos el comando que vamos a utilizar: Crear un directorio 
          peticionFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
          //(FtpWebResponse) request.GetResponse(); 

          try
          {
              FtpWebResponse respuesta;
              respuesta = (FtpWebResponse)peticionFTP.GetResponse();
              respuesta.Close();
              // Si todo ha ido bien, se devolverá String.Empty 
              peticionFTP = null;

              return string.Empty;

          }
          catch (Exception ex)
          {
              // Si se produce algún fallo, se devolverá el mensaje del error 
              return ex.Message;
          }

      } 
    }
}
