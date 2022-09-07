/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.Entidades
	FileName:	Login.cs
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

namespace InapescaWeb.Entidades
{
    public class Login
    {
        private string lsUsuario;
        private string lsPassword;
        private string lsAdscripcion;


        public Login()
        {
            Usuario = "";
            Password = "";
            Crip = "";
        }

        public string Usuario 
        { 
            get{  return lsUsuario;}
            set{  lsUsuario  = value;}
        }

        public string Password
        {
            get { return lsPassword; }
            set { lsPassword = value; }
        }

        public string Crip
        {
            get { return lsAdscripcion; }
            set { lsAdscripcion = value; }
        }

        public string Ubicacion
        {
            get { return lsAdscripcion; }
            set { lsAdscripcion = value; }
        }
    }
}
