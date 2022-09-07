using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using System.Data;


namespace InapescaWeb.BRL
{
   public  class MngNegocioCuentas
    {
       public static List<Entidad> ListaBancos(string psDep, bool pbBandera = false)
       {
           return MngDatosCuentasBancarias.ListaBancos(psDep,pbBandera );
       }

       public static CuentasBancarias ListaCuentas(string psDep,string psBanco)
       {
           return MngDatosCuentasBancarias.ListaCuentas(psDep,psBanco );
       }

    }
}
