using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InapescaWeb.DGAIPP
{
    public partial class Cerrar_Session_DGAIPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cerrar_sessiondgaipp();
        }

        public void Cerrar_sessiondgaipp()
        {
            Session.Abandon();
            // FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("Default.aspx", true); /* tu pagina de logueo*/
        }
    }
}