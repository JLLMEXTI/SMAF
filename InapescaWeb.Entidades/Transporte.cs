using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public  class Transporte
    {
       string _lsclv;
       string _lsClase;
       string _lsDescCl;
       string _lsTipo;
       string _lsDescT;
       string _lsNumEco;
       string _lsDescripcion;
       string _lsMarca;
       string _lsModelo;
       string _lsPlacas;
       string _lsEstatus;
       string _lsObservaciones;

       public string Clv_Trans
       {
           get { return _lsclv ; }
           set { _lsclv  = value; }
       }

       public string Clase
       {
           get { return _lsClase; }
           set { _lsClase = value; }
       }

       public string Desc_Clase
       {
           get { return _lsDescCl; }
           set { _lsDescCl = value; }
       }

       public string Tipo
       {
           get { return _lsTipo; }
           set { _lsTipo = value; }
       }

       public string Desc_Tipo
       {
           get { return _lsDescT; }
           set { _lsDescT = value; }
       }

       public string Economico
       {
           get { return _lsNumEco; }
           set { _lsNumEco = value; }
       }
       public string Descripcion
       {
           get { return _lsDescripcion; }
           set { _lsDescripcion = value; }
       }

       public string Marca
       {
           get { return _lsMarca; }
           set { _lsMarca = value; }
       }

       public string Modelo
       {
           get { return _lsModelo;}
           set { _lsModelo = value; }
       }
       public string Placas
       {
           get { return _lsPlacas; }
           set { _lsPlacas = value; }
       }

       public string Estatus
       {
           get { return _lsEstatus; }
           set { _lsEstatus = value;}
       }

       public string Observaciones
       {
           get { return _lsObservaciones; }
           set { _lsObservaciones = value; }
       }
    }
}
