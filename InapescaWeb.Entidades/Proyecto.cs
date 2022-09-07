/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
	FileName:	Proyecto.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
    Description: Clase entidad que contiene propiedades de objeto
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
   public  class Proyecto
    {
       string lsClvProy;
       string lsDescripcion;
       string lsResponsable;
       string lsObjetivo;
       string lsPeriodo;
       string lsSecEff;
       string lsFecha;
       string lsFechaEff;
       string lsDep;
       string lsArea;
       string lsTotal;
       string lsTipo;
       string lsPrograma;
       string lsComponente;
       string lsDireccion;

       public string Direccion
       {
           get { return lsDireccion; }
           set { lsDireccion = value; }
       }

       public string Programa
       {
           get { return lsPrograma; }
           set { lsPrograma = value; }
       }

       public string Componente
       {
           get { return lsComponente; }
           set { lsComponente = value; }
       }

       public string Tipo_Dep
       {
           get { return lsTipo; }
           set { lsTipo = value; }
       }
       public string Clv_Proy
       {
           get { return lsClvProy; }
           set { lsClvProy = value; }
       }

       public string Descripcion
       {
           get { return lsDescripcion; }
           set { lsDescripcion = value; }
       }

       public string Responsable
       {
           get { return lsResponsable; }
           set { lsResponsable = value; }
       }

       public string Objetivo
       {
           get { return lsObjetivo; }
           set { lsObjetivo = value; }
       }

       public string Periodo
       {
           get { return lsPeriodo; }
           set { lsPeriodo = value; }
       }

       public string SecEff
       {
           get { return lsSecEff; }
           set { lsSecEff = value; }
       }

       public string Fecha
       {
           get { return lsFecha; }
           set { lsFecha = value; }
       }

       public string FechEff
       {
           get { return lsFechaEff; }
           set { lsFechaEff = value; }
       }

       public string Dependencia
       {
           get { return lsDep; }
           set { lsDep = value; }
       }

       public string Area
       {
           get { return lsArea; }
           set { lsArea = value; }
       }

       public string Total
       {
           get { return lsTotal; }
           set { lsTotal = value; }
       }

       //public string Restante
       //{
       //    get { return lsRestante; }
       //    set { lsRestante = value; }
       //}
    }
}
