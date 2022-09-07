using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public  class comision_informe
    {
       private string _lsFolio;
       private string _lsComisionado;
       private string _lsDep_Comisionado;
       private string _lsProyecto;
       private string _lsDep_Proyec;
       private string _lsActividades;
       private string _lsEvidencia1;
       private string _lsEvidencia2;
       private string _lsEvidencia3;
       private string _lsFechaI;
       private string _lsFechaF;
       private string _lsFechaAut;
       private string _lsSecuencia;
       private string _lsDepSolicitud;

       public  string UBICACION_SOLICITUD
       {
           get { return _lsDepSolicitud; }
           set { _lsDepSolicitud = value; }
       }

       public string SECUENCIA
       {
           get { return _lsSecuencia; }
           set { _lsSecuencia = value; }
       }


       public string FOLIO
       {
           get { return _lsFolio;}
           set { _lsFolio = value; }
       }

       public string COMISIONADO
       {
           get { return _lsComisionado; }
           set { _lsComisionado = value; }
       }

       public string UBICACION_COMISIONADO
       {
           get { return _lsDep_Comisionado; }
           set { _lsDep_Comisionado = value; }
       }

       public string PROYECTO
       {
           get { return _lsProyecto; }
           set { _lsProyecto = value; }
       }

       public string UBICACION_PROYECTO
       {
           get { return _lsDep_Proyec; }
           set { _lsDep_Proyec = value; }
       }

       public string ACTIVIDADES
       {
           get { return _lsActividades; }
           set { _lsActividades = value; }
       }

       public string EVIDENCIA_1
       {
           get { return _lsEvidencia1; }
           set { _lsEvidencia1 = value; }
       }

       public string EVIDENCIA_2
       {
           get { return _lsEvidencia2; }
           set { _lsEvidencia2 = value; }
       }

       public string EVIDENCIA_3
       {
           get { return _lsEvidencia3; }
           set { _lsEvidencia3 = value; }
       }

       public string FECHA_INICIO
       {
           get { return _lsFechaI; }
           set { _lsFechaI = value; }
       }

       public string FECHA_FINAL
       {
           get { return _lsFechaF; }
           set { _lsFechaF = value; }
       }

       public string FECHA_AUT
       {
           get { return _lsFechaAut; }
           set { _lsFechaAut = value; }
       }
    }
}
