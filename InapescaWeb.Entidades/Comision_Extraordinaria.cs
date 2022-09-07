/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb.Entidades
	FileName:	Comision_Extraordinaria.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Diciembre 2015
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
   public  class Comision_Extraordinaria
    {
        private string lsOficio;
       private string lsfechaInicio;
       private string lsFechaFinal;
       private string lsLugar;
       private string lsProyecto;
       private string lsProyDep;
       private string lsObjetivo;

       public string Objetivo
       {
           get { return lsObjetivo; }
           set { lsObjetivo = value; }
       }

       public string Oficio
       {
           get { return lsOficio; }
           set { lsOficio = value; }
       }

       public string Fecha_Inicio
       {
           get { return lsfechaInicio; }
           set { lsfechaInicio = value; }
       }

       public string Fecha_Final
       {
           get { return lsFechaFinal; }
           set { lsFechaFinal = value; }
       }

       public string Lugar
       {
           get { return lsLugar; }
           set { lsLugar = value; }
       }

       public string Proyecto
       {
           get { return lsProyecto; }
           set { lsProyecto = value; }
       }

       public string Ubicacion_Proyecto
       {
           get { return lsProyDep; }
           set { lsProyDep = value; }
       }

    }
}
