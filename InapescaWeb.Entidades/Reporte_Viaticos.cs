/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb
	FileName:	Reporte_Viaticos.cs
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
  public   class Reporte_Viaticos
    {
      string _Archivo;
      string _Comisionado;
      string _Viaticos_Otorgados;
      string _Viaticos_Comprobados;
      string _Combustible_Otorgado;
      string _Combustible_Comprobado;
      string _Peaje_Otorgado;
      string _Peaje_Comprobado;
      string _Pasaje_Otorgado;
      string _Pasaje_Comprobado;
      string _Reintegro;
      string _Reintegro_Efectuado;
      string _Finaneciero;
      string _Total;
      string _Tipo;

      public string Tipo
      {
          get { return _Tipo; }
          set { _Tipo = value; }
      }

      public string Total
      {
          get { return _Total; }
          set { _Total = value; }
      }

      public string Oficio
      {
          get { return _Archivo; }
          set { _Archivo = value; }
      }

      public string Comisionado
      {
          get { return _Comisionado; }
          set { _Comisionado = value; }
      }

      public string Viaticos_Otorgados
      {
          get { return _Viaticos_Otorgados; }
          set { _Viaticos_Otorgados = value; }
      }

      public string Viaticos_Comprobados
      {
          get { return _Viaticos_Comprobados; }
          set { _Viaticos_Comprobados = value; }
      }

      public string Combustible_Otorgado
      {
          get { return _Combustible_Otorgado; }
          set { _Combustible_Otorgado = value; }
      }

      public string Combustible_Comprobado
      {
          get { return _Combustible_Comprobado; }
          set { _Combustible_Comprobado = value ;}
      }

      public string Peaje_Otorgado
      {
      get {return _Peaje_Otorgado ;}
          set { _Peaje_Otorgado = value; }
      }

      public string Peaje_Comprobado
      {
      get {return _Peaje_Comprobado ;}
          set {_Peaje_Comprobado = value ;}
      }

      public string Pasaje_Otorgado
      {
      get {return _Pasaje_Otorgado ;}
          set {_Pasaje_Otorgado = value ;}
      }

        public string Pasaje_Comprobado
        {
            get {return _Pasaje_Comprobado ;}
            set { _Pasaje_Comprobado = value ; }
        }

      public string Reintegro
      {
      get {return _Reintegro ;}
          set {_Reintegro = value ;}
      }

      public string Reintegro_Efectuado
      {
      get {return _Reintegro_Efectuado ;}
          set {_Reintegro_Efectuado = value ;}
      }

      public string Financiero
      {
      get {return _Finaneciero ;}
          set {_Finaneciero = value ;}
      }

    }
}
