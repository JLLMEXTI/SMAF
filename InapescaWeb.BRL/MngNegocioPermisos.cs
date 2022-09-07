using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
public     class MngNegocioPermisos
    {
    public static string ObtienePermisos(string psUsuario,string psPermiso,string psProyecto = "", string psDep= "")
    {
        return InapescaWeb.DAL.MngDatosPermisos.obtienePermisos(psUsuario,psPermiso , psProyecto,psDep );
    }
    public static int obtieneCountPermisos(string psUsuario, string psPermiso)
    {
        return InapescaWeb.DAL.MngDatosPermisos.obtieneCountPermisos(psUsuario, psPermiso);
    }
    public static int obtieneCantPermisos(string psUsuario, string psPermiso)
    {
        return InapescaWeb.DAL.MngDatosPermisos.obtieneCantPermisos(psUsuario, psPermiso);
    }
    public static int obtieneCountSolicitudes(string psUsuario)
    {
        return InapescaWeb.DAL.MngDatosPermisos.obtieneCountSolicitudes(psUsuario);
    }
    public static List<Entidad> ObtieneListPermisos()
    {
        return InapescaWeb.DAL.MngDatosPermisos.ObtieneListPermisos();
    }
    public static Boolean Insert_NuevoPermiso(string psUsuario,string psPermiso,string psAutoriza, string psFecha, string psObservaciones, string psCantPermisos)
    {
        return MngDatosPermisos.Insert_NuevoPermiso(psUsuario, psPermiso, psAutoriza, psFecha, psObservaciones, psCantPermisos);
    }

    }
}
