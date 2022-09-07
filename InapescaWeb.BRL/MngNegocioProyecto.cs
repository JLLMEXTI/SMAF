using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
  public  class MngNegocioProyecto
    {
      public static Boolean  Proyecto_Dep(string psDireccion, string psPrograma, string psDep)
      {
          return MngDatosProyecto.Proyecto_Dep(psDireccion, psPrograma, psDep);
      }
      public static List<Entidad> ListaProyectoCrip(string psQuery)
      {
          return MngDatosProyecto.ListaProyectoCrip(psQuery);
      }

      public static string Total_Comprobado(string psProy, string psDep, string psYear)
      {
          return MngDatosProyecto.Total_Comprobado(psProy, psDep, psYear);
      }

      public static string Total_Solicitado(string psYear, string psProyecto, string psDep, string psFechIni, string psFechFin)
      {
          return MngDatosProyecto.Total_Solicitado(psYear, psProyecto, psDep, psFechIni, psFechFin);
      }

      public static List<Proyecto> ListaProyecto(string psPrograma, string psDireccion, string psYear)
      {
          return MngDatosProyecto.ListaProyecto(psPrograma , psDireccion, psYear);
      }

      public static List<Entidad> ObtieneProyectoEjecucion(string psDep, string psPeriodo)
      {
          return MngDatosProyecto.ObtieneProyectoEjecucion(psDep, psPeriodo);
      }

      public static string Nombre_Proyecto(string psClave, string psUbica, string psPeriodo = "")
      {
          return MngDatosProyecto.Nombre_Proyecto(psClave, psUbica, psPeriodo);
      }

      public static List<Entidad> ListaProyectoAdcripcion(string psDireccion, string psPrograma, string psPeriodo = "")
      {
          return MngDatosProyecto.ListaProyectoAdcripcion(psDireccion, psPrograma, psPeriodo);
      }

      public static List<Entidad> ObtieneProyectos(string psUsuario, string lsRol,string lsDep,string lsUbi="")
      {
          return MngDatosProyecto.ObtieneProyectos(psUsuario, lsRol ,lsDep,lsUbi );
      }
      public static List<Entidad> ObtieneProyectosJefeyAdminCriaps(string psUsuario, string lsRol, string lsDep)
      {
          return MngDatosProyecto.ObtieneProyectosJefeyAdminCriaps(psUsuario, lsRol, lsDep);
      }

      public static Proyecto ObtieneDatosProy(string psDep, string psProyecto, string psYear)
      {
          return InapescaWeb.DAL.MngDatosProyecto.ObtenDatosProyecto(psDep, psProyecto, psYear);
      }

      public static Boolean Inserta_Proyectoexterno(string psClvProy, string psDescrip, string psResponsable, string psObjetivo, string psN_Oficio)
      {
          return MngDatosProyecto.Inserta_ProyectoExterno(psClvProy, psDescrip, psResponsable, psObjetivo, psN_Oficio);
      }

      public static bool Update_proyectoExterno(string psClvProy, string psDescrip, string psResponsable, string psObjetivo, string psN_Oficio)
      {
          return MngDatosProyecto.update_proyectExterno(psClvProy,psDescrip ,psResponsable ,psObjetivo ,psN_Oficio);
      }

      public static Proyecto ObtieneDatosProyExt(string psOfiio)
      {
          return InapescaWeb.DAL.MngDatosProyecto.ObtenDatosProyectoExterno (psOfiio );
      }

      public static Boolean Inserta_Detalle_Presupuesto(string psProyec, string psComponente, string psCapitulo, string psSubcapitulo, string psPartida, string psDep, string psEnero, string psFebrero, string psMarzo, string psAbril, string psMayo, string psJunio, string psJulio, string psAgosto, string psSeptiembre, string psOctubre, string psNoviembre, string psDiciembre)
      {
          return MngDatosProyecto.Inserta_Detalle_Presupuesto( psProyec,  psComponente,  psCapitulo,  psSubcapitulo,  psPartida,  psDep,  psEnero,  psFebrero,  psMarzo,  psAbril,  psMayo,  psJunio,  psJulio,  psAgosto,  psSeptiembre,  psOctubre,  psNoviembre,  psDiciembre);
      }

      public static Boolean Inserta_Proyecto(string psComponente, string psPrograma,string psDireccion, string psDescripcion, string psDescripCorta, string psResponsable, string psObjetivo, string psUbicacion, string psTipo, string psArea, string psRecurso)
      {
          return MngDatosProyecto.Inserta_Proyecto(psComponente, psPrograma,psDireccion , psDescripcion, psDescripCorta, psResponsable, psObjetivo, psUbicacion, psTipo, psArea, psRecurso);
      }
  }
}
