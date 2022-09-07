using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;


namespace InapescaWeb.BRL
{
    /// <summary>
    /// Clase que contiene metodos que conectan a los metodos del layer DAL con colecciones de datos, objetos, listas etc.
    /// </summary>
   public class MngNegocioOficio
    {

       /// <summary>
       /// Metodo que retorna lista de documentos docs segun oficio,y usuario
       /// </summary>
       /// <param name="psOficio"></param>
       /// <param name="psDep"></param>
       /// <param name="psUsuario"></param>
       /// <returns></returns>
       public static List<InapescaWeb.Entidades.Entidad> Lista_DOCS(string psOficio, string psDep, string psUsuario)
       {
           return MngDatosOficio.Lista_DOCS(psOficio ,psDep ,psUsuario );
       }

       /// <summary>
       /// Metodo que devuelve coleccion de datos lista de layer DAL
       /// </summary>
       /// <returns></returns>
       public static List<InapescaWeb.Entidades.Entidad> Oficios_Comprobatorios()
       {
           return MngDatosOficio.Oficios_Comprobatorios();
       }
    }
}
