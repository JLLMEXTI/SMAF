using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public  class ComisionDetalle
    {
       string _noOficio;
       string _archivo;
       string _tipoCom;
       string _FechaInicio;
       string _FechaFinal;
       string _Proyecto;
       string _DepProy;
       string _FormaPago;
       string _Viaticos;
       string _PartidaV;
       string _Combustible;
       string _PartidaC;
       string _Peaje;
       string _PartidaP;
       string _Pasaje;
       string _PartidaPas;
       string _Estatus;
       string _Financieros;
       string _Observaciones;
       string _Periodo;
       string _Dias;
       
       string _Comisionado;
       string _lugar;
       string _uComisionado;
       string _especie;
       string _producto;
       string _actividad;
       string _viaticosaldia;
       string _Objetivo;

       string _Singladuras;
       string _PartidaSingladuras;

       public string Partida_Singladuras
       {
           get { return _PartidaSingladuras; }
           set { _PartidaSingladuras = value; }
       }


       public string Singladuras
       {
           get { return _Singladuras; }
           set { _Singladuras = value; }
       }

       public string Objetivo
       {
           get { return _Objetivo; }
           set { _Objetivo = value; }
       }

       public string Viaticos_Dia
       {
           get { return  _viaticosaldia; }
           set { _viaticosaldia = value; }
       }

       public string Actividad
       {
           get { return _actividad; }
           set { _actividad = value; }
       }

       public string Producto
       {
           get { return _producto; }
           set { _producto = value; }
       }

       public string Especie
       {
           get { return _especie; }
           set { _especie = value; }
       }
        
       public string Ubicacion_Comisionado
       {
           get { return _uComisionado; }
           set { _uComisionado = value; }
       }

       public string Lugar
       {
           get { return _lugar; }
           set { _lugar = value; }
       }

      public  string Comisionado
       {
           get { return _Comisionado; }
           set { _Comisionado = value; }
       }

       public string Dias_Reales
       {
           get { return _Dias; }
           set { _Dias = value; }
       }

       public string Anio
       {
           get { return _Periodo; }
           set { _Periodo = value; }
       }

       public string Observaciones
       {
           get { return _Observaciones; }
           set { _Observaciones = value; }
       }

       public string Financieros
       {
           get { return _Financieros; }
           set { _Financieros = value; }
       }

       public string EstatuS
       {
           get { return _Estatus; }
           set { _Estatus = value; }
       }

       public string Partida_Pasaje
       {
           get { return _PartidaPas; }
           set { _PartidaPas = value; }
       }

       public string Pasaje
       {
           get { return _Pasaje; }
           set { _Pasaje = value; }
       }

       public string Partida_Peaje
       {
           get { return _PartidaP; }
           set { _PartidaP = value; }
       }

       public string Peaje
       {
           get { return _Peaje; }
           set { _Peaje = value; }
       }

       public string Partida_Combustible
       {
           get { return _PartidaC; }
           set { _PartidaC = value; }
       }

       public string Combustible
       {
           get
           {
               return _Combustible;

           }
           set { _Combustible = value; }
       }

       public string Partida_Viaticos
       {
           get { return _PartidaV; }
           set { _PartidaV = value; }
       }

       public string Viaticos
       {
           get { return _Viaticos; }
           set { _Viaticos = value; }
       }

       public string Forma_Pago
       {
           get { return _FormaPago; }
           set { _FormaPago = value; }
       }

       public string Ubicacion_Proyecto
       {
           get { return _DepProy; }
           set { _DepProy = value; }
       }

       public string Proyecto
       {
           get { return _Proyecto; }
           set { _Proyecto = value; }
       }

       public string Final
       {
           get { return _FechaFinal;}
           set { _FechaFinal = value; }
       }

       public string Inicio
       {
           get { return _FechaInicio; }
           set { _FechaInicio = value; }
       }

       public string Tipo
       {
           get { return _tipoCom; }
           set { _tipoCom = value; }
       }

       public string Archivo
       {
           get { return _archivo; }
           set { _archivo = value; }
       }


       public string Oficio
       {
           get { return _noOficio;}
           set { _noOficio = value; }
       }


    }
}
