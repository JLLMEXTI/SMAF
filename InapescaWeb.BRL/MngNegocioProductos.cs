﻿/// <
/// 
/// summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngNegocioProductos.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Noviembre 2015
    Description: Clase quefunciona de puente con la clase de datos productos en el DAL
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
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
     public class MngNegocioProductos
    {

         public static string Obtiene_Descripcion_Producto(string psProducto,string psYear, string psFecha_VoBo="")
         {
             return MngDatosProductos.Obtiene_Producto_Descripcion(psProducto,psYear, psFecha_VoBo);
         }

         public static List<Entidad> Obtiene_Productos(string psYear, string psDireccion, string psComponente = "")
         {
             return MngDatosProductos.Obtiene_Productos(psYear , psDireccion, psComponente );
         }
    }
}
