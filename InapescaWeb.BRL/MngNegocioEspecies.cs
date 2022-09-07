/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngNegocioEspecies.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Noviembre 2015
    Description: Clase quefunciona de puente con la clase de datos especies em el DAL
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
    public  class MngNegocioEspecies
    {
        public static string Obtiene_Descripcion_Pesqueria(string psPrograma, string psPesqueria,string psDireccion)
        {
            return MngDatosEspecies.Obtiene_Descripcion_Pesqueria(psPrograma, psPesqueria,psDireccion );
        }

        public static List<Entidad> Obtiene_Especies(string psPrograma,string psDireccion)
        {
            return MngDatosEspecies.Obtiene_Especies(psPrograma,psDireccion );
        }

    }
}
