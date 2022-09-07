using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
   public class MngNegocioGenerarRef

    {
       public static string Obtiene_Max_Referencia()
       {
           return MngDatosGenerarRef.Obtiene_Max_Referencia();
       }

       public static Boolean Inserta_Referencia_Detalles(GenerarRef oRefencia)
       {
           return MngDatosGenerarRef.Inserta_Referencia_Detalles(oRefencia);
       }

       public static Boolean Inserta_Referencia(GenerarRef oRefencia)
       {
           return  MngDatosGenerarRef.Inserta_Referencia(oRefencia);
       }

       public static Comision Obten_Informacion_Comision(string psArchivo)
       {
           return MngDatosGenerarRef.Obten_Informacion_Comision(psArchivo);
       }

       public static List<GenerarRef> Obten_Informacion_ReferenciaDetalles(string psID, string psReferencia, bool boolID)
       {
           return MngDatosGenerarRef.Obten_Informacion_ReferenciaDetalles(psID, psReferencia, boolID);
       }

       public static List<GenerarRef> Obten_Informacion_Referencia(string psArchivo)
       {
           return MngDatosGenerarRef.Obten_Informacion_Referencia(psArchivo);
       }

       public static Boolean Update_UpdateTablasReferencias(string sIdExistente, string sReferenciaExiste, string ESTATUS)
       {
           return MngDatosGenerarRef.Update_UpdateTablasReferencias(sIdExistente, sReferenciaExiste, ESTATUS);
       }

       public static Boolean Update_UpdateReferencia(string sIdExistente, string sReferenciaExiste, string ESTATUS)
       {
           return MngDatosGenerarRef.Update_UpdateReferencia(sIdExistente, sReferenciaExiste, ESTATUS);
       }
       public static Boolean Update_UpdateReferenciaGenerada(string sReferencia, string sImportePagado, string sFechaPago)
       {
           return MngDatosGenerarRef.Update_UpdateReferenciaGenerada(sReferencia,sImportePagado,sFechaPago);
       }
       public static Comision Detalle_Comision_Archivo(string psArchivo, string psComisionado = "")
       {
           return MngDatosGenerarRef.Detalle_Comision_Archivo(psArchivo,psComisionado);
       }
       public static GenerarRef Obten_Detalles_Referencia(string psReferencia)
       {
           return MngDatosGenerarRef.Obten_Detalles_Referencia(psReferencia);
       }
       //*****************PATRICIA QUINO MORENO 24/08/2017
       public static List<GenerarRef> Obtiene_FFinal_FInicio(string psInicio, string psFinal, string sEstatus)
       {
           return MngDatosGenerarRef.Obtiene_FFinal_FInicio(psInicio, psFinal, sEstatus);

       }

    }
}
