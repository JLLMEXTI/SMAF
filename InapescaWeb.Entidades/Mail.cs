using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
  public   class Mail
    {
       private string lsNotificacion;
       private string lsMenssage;
       private string lsLink;

     
       public string Notificacion
       {
           get { return lsNotificacion ; }
           set { lsNotificacion  = value; }
       }

       public string Message
       {
           get { return lsMenssage; }
           set { lsMenssage  = value; }
       }

       public string Link
       {
           get { return lsLink; }
           set { lsLink = value; }
       }
    }
}
