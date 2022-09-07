using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public class ComisionComparativo
    {
       
       private string lsOficio;
       private string lsArchivo;
       private string lsComisionado;
       private string lsLugar;
       private string lsFechas;
       private string lsPasaje;
       private string lsPeaje;
       private string lsCombustible;
       private string lsViaticos;
       private string lsSingladuras;

       private string lsTotal;
       private string lsReintegro;
       private string lsEjercidoReal;


       private string lsInicio;
       private string lsFinal;
       private string lsFechaPagoV;
       private string lsFechaPagoS;
       private string lsFechaPagoC;
       private string lsFechaPagoPe;
       private string lsFechaPagoPa;


       public string Ejercido
       {
           get { return lsEjercidoReal; }
           set { lsEjercidoReal = value; }
       }

       public string Reintegro
       {
           get { return lsReintegro; }
           set { lsReintegro = value; }
       }

       public string Total_Otorgado
       {
           get { return lsTotal; }
           set { lsTotal = value; }

       }

       public string Fechas
       {
           get { return lsFechas; }
           set { lsFechas = value; }
       }

       public string Archivo
       {
           get { return lsArchivo; }
           set { lsArchivo = value; }
       }

       public string Fecha_Pasaje
       {
           get { return lsFechaPagoPa; }
           set { lsFechaPagoPa = value; }
       }

       public string Pasaje
       {
           get { return lsPasaje; }
           set { lsPasaje = value; }
       }

       public string Fecha_Peaje
       {
           get { return lsFechaPagoPe; }
           set { lsFechaPagoPe = value; }
       }

       public string Peaje
       {
           get { return lsPeaje; }
           set { lsPeaje = value; }
       }

       public string Fecha_Combustible
       {
           get { return lsFechaPagoC; }
           set { lsFechaPagoC = value; }
       }

       public string Combustible
       {
           get { return lsCombustible; }
           set { lsCombustible = value; }
       }

       public string Fecha_Singladuras
       {
           get { return lsFechaPagoS; }
           set { lsFechaPagoS = value; }
       }

       public string Singladuras
       {
           get { return lsSingladuras; }
           set { lsSingladuras = value; }
       }

       public string Fecha_Viaticos
       {
           get { return lsFechaPagoV; }
           set { lsFechaPagoV = value; }
       }


       public string Viaticos
       {
           get { return lsViaticos; }
           set { lsViaticos = value; }
       }

       public string Final
       {
           get { return lsFinal; }
           set { lsFinal = value; }
       }

       public string Inicio
       {
           get { return lsInicio; }
           set { lsInicio = value; }
       }

       public string Lugar
       {
           get { return lsLugar; }
           set { lsLugar = value; }
       }

       public string Oficio
       {
           get { return lsOficio; }
           set { lsOficio = value; }
       }

       public string Comisionado
       {
           get { return lsComisionado ; }
           set { lsComisionado = value; }
       }
    }
}
