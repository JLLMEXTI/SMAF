using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
   public class Reporte
    {
       public string Programa
       {
           get;
           set;
       }

       public string Objetivo
       {
           get;
           set;
       }
       public string Direccion
       {
           get;
           set;
       }
       public string Total_Solicitado
       {
           get;
           set;
       }

       public string Total_Comprobado
       {
           get;
           set;
       }
    }
}
