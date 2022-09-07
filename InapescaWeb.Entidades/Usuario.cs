/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Entidades
	FileName:	Usuario.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class Usuario
    {
        private string lsUsuario;
        private string lsPassword;
        private string lsNivel;
        private string lsPlaza;
        private string lsPuesto;
        private string lsSecretaria;
        private string lsOrganismo;
        private string lsUbicacion;
        private string lsArea;
        private string lsNombre;
        private string lsApPat;
        private string lsApMat;
        private string lsRFC;
        private string lsCargo;
        private string lsEmail;
        private string lsRol;
        private string lsAbreviatura;
        private string lsfech_nac;
        private string lscalle;
        private string lsnumext;
        private string lsnum_int;
        private string lscolonia;
        private string lsdelegacion;
        private string lsCURP;
        private string lssec_eff;
        private string lsestado;
        private string lscd;
        private string lsNumEmp;
        
      
        
        public Usuario()
        {
            lsUsuario = string.Empty;
            lsPassword = string.Empty;
            lsNivel = string.Empty;
            lsPlaza = string.Empty;
            lsPuesto = string.Empty;
            lsSecretaria = string.Empty;
            lsOrganismo = string.Empty;
            lsUbicacion = string.Empty;
            lsArea = string.Empty;
            lsNombre = string.Empty;
            lsApPat = string.Empty;
            lsApMat = string.Empty;
            lsRFC = string.Empty;
            lsCargo = string.Empty;
            lsEmail = string.Empty;
            lsAbreviatura = string .Empty ;
           
            lsfech_nac = string.Empty;
            lscalle = string.Empty;
            lsnumext = string.Empty;
            lsnum_int = string.Empty;
            lscolonia = string.Empty;
            lsdelegacion = string.Empty;
            lsCURP = string.Empty;
            lssec_eff = string.Empty;
            lsestado = string.Empty;
            lscd = string.Empty;
            lsNumEmp = string.Empty;
        }

        public string NUM_EMP
        {
            get { return lsNumEmp; }
            set
            {
                lsNumEmp = value;
            }
        }
        public string CD
        {
            get { return lscd; }
            set
            {
                lscd = value;
            }
        }

        public string Estado
        {
            get { return lsestado; }
            set
            {
                lsestado = value;
            }
        }
        public string colonia
        {
            get { return lscolonia; }
            set { lscolonia = value; }
        }
        public string delegacion
        {
            get { return lsdelegacion; }
            set { lsdelegacion = value; }
        }
        public string CURP
        {
            get { return lsCURP; }
            set { lsCURP = value; }
        }
        public string SEC_EFF
        {
            get { return lssec_eff; }
            set { lssec_eff = value; }
        }

        public string num_int
        {
            get { return lsnum_int; }
            set { lsnum_int = value; }
        }

        public string numext
        {
            get { return lsnumext; }
            set { lsnumext = value; }
        }

        public string calle
        {
            get { return lscalle; }
            set { lscalle = value; }
        }

        public string fech_nac
        {
            get { return lsfech_nac; }
            set { lsfech_nac = value; }
        }

        public string Abreviatura
        {
            get { return lsAbreviatura; }
            set { lsAbreviatura = value; }
        }
        public string Usser
        {
            get { return lsUsuario; }
            set { lsUsuario = value; }
        }

        public string Password
        {
            get { return lsPassword; }
            set { lsPassword = value; }
        }

        public string Nivel
        {
            get { return lsNivel; }
            set { lsNivel = value; }
        }

        public string Plaza
        {
            get { return lsPlaza; }
            set { lsPlaza = value; }
        }

        public string Puesto
        {
            get { return lsPuesto; }
            set { lsPuesto = value; }
        }

        public string Secretaria
        {
            get { return lsSecretaria; }
            set { lsSecretaria = value; }
        }

        public string Organismo
        {
            get { return lsOrganismo; }
            set { lsOrganismo = value; }
        }

        public string Ubicacion
        {
            get { return lsUbicacion; }
            set { lsUbicacion = value; }
        }

        public string Area
        {
            get { return lsArea; }
            set { lsArea = value; }
        }

        public string Nombre
        {
            get { return lsNombre; }
            set { lsNombre = value; }
        }

        public string ApPat
        {
            get { return lsApPat; }
            set { lsApPat = value; }
        }

        public string ApMat
        {
            get { return lsApMat; }
            set { lsApMat = value; }
        }

        public string RFC
        {
            get { return lsRFC; }
            set { lsRFC = value; }
        }

        public string Cargo
        {
            get { return lsCargo; }
            set { lsCargo = value; }
        }

        public string Email
        {
            get { return lsEmail; }
            set { lsEmail = value; 
            }
        }

        public string Rol
        {
            get { return lsRol; }
            set { lsRol = value; }
        }
    }

}
