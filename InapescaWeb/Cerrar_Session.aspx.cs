using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InapescaWeb
{
    public partial class Cerrar_Session : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cerrar_session();
        }

        public void Cerrar_session()
        {
            Session.Abandon();
           // FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("Index.aspx", true); /* tu pagina de logueo*/
        }
    }
}