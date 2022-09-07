/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Entidades
	FileName:	Entidad.cs
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
   public  class Entidad
    {
       private string lsCodigo;
       private string lsDescripcion;

       public Entidad()
       {
           lsCodigo = "";
           lsDescripcion = "";
       }

       public string Codigo
       {
           get { return lsCodigo; }
           set { lsCodigo = value; }
       }

       public string Descripcion
       {
           get { return lsDescripcion; }
           set { lsDescripcion = value; }
       }

    }

}
