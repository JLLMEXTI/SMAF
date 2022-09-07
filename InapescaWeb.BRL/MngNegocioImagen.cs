using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;


namespace InapescaWeb.BRL
{
  public   class MngNegocioImagen
    {
      public static bool Carga_Imagenes(string psClave, string psDescripcion, string psNombreCorto, string psFecha, Byte[] byteImage)
      {
         return MngDatosImagenes.CargarImagen (psClave , psDescripcion ,psNombreCorto, psFecha ,byteImage    );
          
      }
    }
}
