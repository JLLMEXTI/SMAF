using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;


namespace InapescaWeb.BRL
{
  public   class MngNegocioUsuarios
    {
      
      /***********PATRICIA QUINO MORENO**************************/

      public static List<Entidad> UsuarioProyecto(string pPeriodo, string pClave, string pDepProy, string pEstatus)
      {
          return MngDatosUsuarios.UsuarioProyecto(pPeriodo, pClave, pDepProy, pEstatus);
      }
      /***********PATRICIA QUINO MORENO************************************/
      public static List<Entidad> ListaUsuarios()
      {
          return MngDatosUsuarios.ListaUsuarios();
      }

      public static string Obtiene_Cargo(string psUsuario,string psDireccion = "")
      {
          return MngDatosUsuarios.Obtiene_Cargo(psUsuario,psDireccion );
      }
      public static string Obtiene_Cargo_job_periodo(string psUsuario, string psDireccion = "", string psPeriodo = "")
      {
          return MngDatosUsuarios.Obtiene_Cargo_job_periodo(psUsuario, psDireccion, psPeriodo);
      }
      public static string Obtiene_puesto_job_periodo(string psUsuario, string psDireccion = "", string psPeriodo = "")
      {
          return MngDatosUsuarios.Obtiene_puesto_job_periodo(psUsuario, psDireccion, psPeriodo);
      }
      public static List<Entidad> ListaUsuariosDependencia(string psDep)
      {
          return MngDatosUsuarios.ListaUsuariosDependencia(psDep);
      }
      public static List<Entidad> ListaUsuariosDependencia2(string psDep)
      {
          return MngDatosUsuarios.ListaUsuariosDependencia2(psDep);
      }

      public static string Obtiene_Direccion(string psDep)
      {
          return MngDatosUsuarios.Obtiene_Direccion(psDep);
      }

      public static List<Entidad> MngBussinesUssers(string psDep,string psUsuarioLogeado,string lsRol, bool psBol = false )
      {
          return InapescaWeb.DAL.MngDatosUsuarios.MngDatosPersonal(psDep, psUsuarioLogeado,lsRol ,psBol );
      }

      public static List <Entidad > MngDatosUsuariosExt( string psSec, string psOrg, string psCentro,string psRol = "")
         {

             return InapescaWeb.DAL.MngDatosUsuarios.MngDatosPersonalExterno(psSec, psOrg, psCentro,psRol );
      }

      public static string Obtiene_Nombre(string psUsuario)
      {
          return InapescaWeb.DAL.MngDatosUsuarios.Obtiene_Nombre_Completo(psUsuario);
      }
      public static string Obtiene_Nombre_Completo_AdmCont(string psRol, string psUbicacion)
      {
          return MngDatosUsuarios.Obtiene_Nombre_Completo_AdmCont(psRol, psUbicacion);
      }
      public static string Obten_Usuario(string psRol, string psDep = "" , string Cargo = "")
      {
          return MngDatosUsuarios.Obtiene_Usuario(psRol, psDep,Cargo );
      }

      public static string Obtiene_Rol(string psUsuario)
      {
          return MngDatosUsuarios.Obtiene_Rol_Usuario(psUsuario);
      }

      public static string Obtiene_Ubicacion(string psUsuario)
      {
          return MngDatosUsuarios.Obtiene_Ubi_Usuario(psUsuario);
      }

      public static  Usuario Obten_Datos(string psUsuario , bool psBandera = false )
      {
          return MngDatosUsuarios.Obten_Datos(psUsuario,psBandera );
      }

      public static Usuario DatosComisionado1(string psComisionado, string psPeriodo)
      {
          return MngDatosUsuarios.DatosComisionado1(psComisionado, psPeriodo);
      }

      public static Usuario Datos_Comisionado(string psComisionado, string psUbicacionComisionado = "")
      {
          return MngDatosUsuarios.DatosComisionado(psComisionado, psUbicacionComisionado);
      }
  
      public static Usuario Datos_Administrador(string psRol, string psComisionado,bool DEP= false)
      {
          return MngDatosUsuarios.Datos_Administrador(psRol, psComisionado,DEP  );
      }

      public static Entidad Datos_DirAdjunto(string psUbicacion)
      {
          return MngDatosUsuarios.Datos_DirAdjunto(psUbicacion);
      }
    }
}
