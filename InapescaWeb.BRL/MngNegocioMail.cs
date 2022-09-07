using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;


namespace InapescaWeb.BRL
{
   public class MngNegocioMail
    {

       public static string Mail_Enviar(string psSecretaria, string psOrganismo, string psUbicacion , string psROl, string psUsuario = "")
       {
           return MngDatosMail.Mail_Destino (psSecretaria ,psOrganismo,psUbicacion,psROl ,psUsuario );
       }

       public static string Obtiene_Mail(string psUsuario)
       {
           return MngDatosMail.Obtiene_Mail(psUsuario);
       }

       public static List<Entidad> Obtiene_Mail(string psTipo ,string psFolio, string psDep)
       {
           return MngDatosMail.Obtiene_Mail(psTipo , psFolio,psDep );
       }
    }
}
