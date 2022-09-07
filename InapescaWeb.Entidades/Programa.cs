/// <summary>
/// 
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Entidades
	FileName:	Programa.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2016
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
   public  class Programa
    {
       private string lsComponente;
       private string lsPrograma;
       private string lsDescripcion;
       private string lsDescCorto;
       private string lsObjetivo;
       private string lsCoordinador;
       private string lsDireccion;
       private string lspresupuesto;
       
       public string Tipo
       { get; set; }

       public string Direccion
       {
           get { return lsDireccion; }
           set { lsDireccion = value; }
       }

       public string Componente
       {
           get { return lsComponente; }
           set { lsComponente = value; }
       }

       public string Id_Programa
       {
           get { return lsPrograma; }
           set { lsPrograma = value; }
       }

       public string Descripcion
       {
           get { return lsDescripcion; }
           set { lsDescripcion = value; }
       }

       public string Descripcion_Corta
       {
           get { return lsDescCorto; }
           set { lsDescCorto = value; }
       }

       public string Objetivo
       {
           get { return lsObjetivo; }
           set { lsObjetivo = value; }
       }

       public string Coordinador
       {
           get { return lsCoordinador; }
           set { lsCoordinador = value; }
       }

       public string Presupuesto
       {
           get { return lspresupuesto; }
           set { lspresupuesto = value; }
       }

    }
}
