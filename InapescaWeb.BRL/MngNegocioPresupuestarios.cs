using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
namespace InapescaWeb.BRL
{
   public  class MngNegocioPresupuestarios
    {
       public static List<Entidad> ObtienePresupuestarios()
       {
           return MngDatosPresupuestarios.Presupuestarios();
       }

    }
}
