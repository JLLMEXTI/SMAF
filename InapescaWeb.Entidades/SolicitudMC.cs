/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Sustativo
	FileName:	ActualizaModuloConsulta.aspx
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Of. Centrales
	Date:		Marzo 2021
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

namespace InapescaWeb.Entidades
{
    public class SolicitudMC
    {
        private string lsFolio;
        private string lsFechaSol;
        private string lsOfSolicitud;
        private string lsSolicitante;
        private string lsCorreo;
        private string lsAsunto;
        private string lsRecurso;
        private string lsDescripcion;
        private string lsOfRespuesta;
        private string lsFechaResp;
        private string lsTipoResp;
        private string lsEstatus;
        private string lsPeriodo;
        private string lsObservaciones;
        private string lsDocumento;
        private string lsExpediente;


        public string Folio
        {
            get { return lsFolio; }
            set { lsFolio = value; }
        }
        public string FechaSol
        {
            get { return lsFechaSol; }
            set { lsFechaSol = value; }
        }
        public string OfSolicitud
        {
            get { return lsOfSolicitud; }
            set { lsOfSolicitud = value; }
        }
        public string Solicitante
        {
            get { return lsSolicitante; }
            set { lsSolicitante = value; }
        }
        public string Correo
        {
            get { return lsCorreo; }
            set { lsCorreo = value; }
        }
        public string Asunto
        {
            get { return lsAsunto; }
            set { lsAsunto = value; }
        }
        public string Recurso
        {
            get { return lsRecurso; }
            set { lsRecurso = value; }
        }
        public string Descripcion
        {
            get { return lsDescripcion; }
            set { lsDescripcion = value; }
        }
        public string OfRespuesta
        {
            get { return lsOfRespuesta; }
            set { lsOfRespuesta = value; }
        }
        public string FechaResp
        {
            get { return lsFechaResp; }
            set { lsFechaResp = value; }
        }
        public string TipoResp
        {
            get { return lsTipoResp; }
            set { lsTipoResp = value; }
        }
        public string Estatus
        {
            get { return lsEstatus; }
            set { lsEstatus = value; }
        }
        public string Periodo
        {
            get { return lsPeriodo; }
            set { lsPeriodo = value; }
        }
        public string Observaciones
        {
            get { return lsObservaciones; }
            set { lsObservaciones = value; }
        }
        public string Documento
        {
            get { return lsDocumento; }
            set { lsDocumento = value; }
        }
        public string Expediente
        {
            get { return lsExpediente; }
            set { lsExpediente = value; }
        }
    }
}
