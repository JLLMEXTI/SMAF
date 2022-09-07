
/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Entidades
	FileName:	Comision.cs
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Of. Centrales
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
    public class SolicitudPSP
    {
        private string lsNombrePSP;
        private string lsApPatPSP;
        private string lsApMatPSP;
        private string lsCorreoPSP;
        private string lsTipIDPSP;
        private string lsNoIDPSP;
        private string lsRFCPSP;
        private string lsActPSP;
        private string lsClabePSP;
        private string lsTelefonoPSP;
        private string lsCalle;
        private string lsNoExt;
        private string lsNoInt;
        private string lsColonia;
        private string lsMunicipio;
        private string lsCP;
        private string lsObjContrato;
        private string lsFechIncio;
        private string lsFechFin;
        private string lsMontoMensinIVA;
        private string lsIVA;
        private string lsMontoMenconIVA;
        private string lsMontoMenconIVALet;
        private string lsMontoTotalCont;
        private string lsMontoTotalConLet;
        private string lsNombrePart2;
        private string lsApPatPart2;
        private string lsApMatPart2;
        private string lsNombrePart3;
        private string lsApPatPart3;
        private string lsApMatPart3;
        private string lsMontoTotalContPart2;
        private string lsMontoTotalContPart3;
        private string lsMontoTotalContLetPart2;
        private string lsMontoTotalContLetPart3;
        private string lsDiasNat;
        private string lsExhibiciones;
        private string lsUbicacion;
        private string lsDireccionUbica;
        private string lsDGA;
        private string lsAdminCont;
        private string lsAdscripcion;
        private string lsOficina;
        private string lsPiso;
        private string lsEstado;
        

        public SolicitudPSP()
        {
            lsOficina = "";
            lsPiso = "";
            lsNombrePSP="";
            lsApPatPSP = "";
            lsApMatPSP = "";
            lsCorreoPSP = "";
            lsTipIDPSP = "";
            lsNoIDPSP = "";
            lsRFCPSP = "";
            lsActPSP = "";
            lsClabePSP = "";
            lsTelefonoPSP = "";
            lsCalle = "";
            lsNoExt = "";
            lsNoInt = "";
            lsColonia = "";
            lsMunicipio = "";
            lsCP = "";
            lsObjContrato = "";
            lsFechIncio = "";
            lsFechFin = "";
            lsMontoMensinIVA = "";
            lsIVA = "";
            lsMontoMenconIVA = "";
            lsMontoMenconIVALet = "";
            lsMontoTotalCont = "";
            lsMontoTotalConLet = "";
            lsNombrePart2 = "";
            lsApPatPart2 = "";
            lsApMatPart2 = "";
            lsNombrePart3 = "";
            lsApPatPart3 = "";
            lsApMatPart3 = "";
            lsMontoTotalContPart2 = "";
            lsMontoTotalContPart3 = "";
            lsMontoTotalContLetPart2 = "";
            lsMontoTotalContLetPart3 = "";
            lsDiasNat = "";
            lsExhibiciones = "";
            lsUbicacion = "";
            lsDGA = "";
            lsAdminCont = "";
            lsAdscripcion = "";
            lsEstado = "";
            lsDireccionUbica="";
        }


        public string DireccionUbica
        {
            get { return lsDireccionUbica; }
            set { lsDireccionUbica = value; }
        }

        public string Estado
        {
            get { return lsEstado; }
            set { lsEstado = value; }
        }

        public string Oficina
        {
            get { return lsOficina; }
            set { lsOficina = value; }
        }
        public string Piso
        {
            get { return lsPiso; }
            set { lsPiso = value; }
        }
        public string NombrePSP
        {
            get { return lsNombrePSP; }
            set { lsNombrePSP = value; }
        }
        public string ApPatPSP
        {
            get { return lsApPatPSP; }
            set { lsApPatPSP = value; }
        }
        public string ApMatPSP
        {
            get { return lsApMatPSP; }
            set { lsApMatPSP = value; }
        }
        public string EMAIL
        {
            get { return lsCorreoPSP; }
            set { lsCorreoPSP = value; }
        }
        public string TipoID
        {
            get { return lsTipIDPSP; }
            set { lsTipIDPSP = value; }
        }
        public string NoID
        {
            get { return lsNoIDPSP; }
            set { lsNoIDPSP = value; }

        }
        public string RFC
        {
            get { return lsRFCPSP; }
            set { lsRFCPSP = value; }
        }
        public string ACT
        {
            get { return lsActPSP; }
            set { lsActPSP = value; }
        }
        public string Clabe
        {
            get { return lsClabePSP; }
            set { lsClabePSP = value; }
        }
        public string Telefono
        {
            get { return lsTelefonoPSP; }
            set { lsTelefonoPSP = value; }
        }
        public string Calle
        {
            get { return lsCalle; }
            set { lsCalle = value; }
        }
        public string NoExt
        {
            get { return lsNoExt; }
            set { lsNoExt = value; }
        }
        public string NoInt
        {
            get { return lsNoInt; }
            set { lsNoInt = value; }
        }
        public string Colonia
        {
            get { return lsColonia; }
            set { lsColonia = value; }
        }
        public string Municipio
        {
            get { return lsMunicipio; }
            set { lsMunicipio = value; }
        }
        public string CP
        {
            get { return lsCP; }
            set { lsCP = value; }
        }
        public string ObjContrato
        {
            get { return lsObjContrato; }
            set { lsObjContrato = value; }
        }
        public string FechInc
        {
            get { return lsFechIncio; }
            set { lsFechIncio = value; }
        }
        public string FechFin
        {
            get { return lsFechFin; }
            set { lsFechFin = value; }
        }
        public string MontoMensualSinIVA
        {
            get { return lsMontoMensinIVA; }
            set { lsMontoMensinIVA = value; }
        }
        public string IVA
        {
            get { return lsIVA; }
            set { lsIVA = value; }
        }
        public string MontoMensualConIVA
        {
            get { return lsMontoMenconIVA; }
            set { lsMontoMenconIVA = value; }
        }
        public string MontoTotalCont
        {
            get { return lsMontoTotalCont; }
            set { lsMontoTotalCont = value; }
        }
        public string MontoMensualConIVALetra
        {
            get { return lsMontoMenconIVALet; }
            set { lsMontoMenconIVALet = value; }
        }
        public string MontoTotalContLetra
        {
            get { return lsMontoTotalConLet; }
            set { lsMontoTotalConLet = value; }
        }
        public string NombrePart2
        {
            get { return lsNombrePart2; }
            set { lsNombrePart2 = value; }
        }
        public string ApPatPart2
        {
            get { return lsApPatPart2; }
            set { lsApPatPart2 = value; }
        }
        public string ApMatPart2
        {
            get { return lsApMatPart2; }
            set { lsApMatPart2 = value; }
        }
        public string MontoTotalContPart2
        {
            get { return lsMontoTotalContPart2; }
            set { lsMontoTotalContPart2 = value; }
        }
        public string MontoTotalContLetraPart2
        {
            get { return lsMontoTotalContLetPart2; }
            set { lsMontoTotalContLetPart2 = value; }
        }
        public string NombrePart3
        {
            get { return lsNombrePart3; }
            set { lsNombrePart3 = value; }
        }
        public string ApPatPart3
        {
            get { return lsApPatPart3; }
            set { lsApPatPart3 = value; }
        }
        public string ApMatPart3
        {
            get { return lsApMatPart3; }
            set { lsApMatPart3 = value; }
        }
        public string MontoTotalContPart3
        {
            get { return lsMontoTotalContPart3; }
            set { lsMontoTotalContPart3 = value; }
        }
        public string MontoTotalContLetraPart3
        {
            get { return lsMontoTotalContLetPart3; }
            set { lsMontoTotalContLetPart3 = value; }
        }
        public string DiasNat
        {
            get { return lsDiasNat; }
            set { lsDiasNat = value; }
        }
        public string Exhibiciones
        {
            get { return lsExhibiciones; }
            set { lsExhibiciones = value; }
        }
        public string Ubicacion
        {
            get { return lsUbicacion; }
            set { lsUbicacion = value; }
        }
        public string DGA
        {
            get { return lsDGA; }
            set { lsDGA = value; }
        }
        public string AdminContr
        {
            get { return lsAdminCont; }
            set { lsAdminCont = value; }
        }
        public string Adscripcion
        {
            get { return lsAdscripcion; }
            set { lsAdscripcion = value; }
        }
    }
}
