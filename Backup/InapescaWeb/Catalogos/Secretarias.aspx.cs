using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;

namespace InapescaWeb.Catalogos
{
    public partial class Secretarias : System.Web.UI.Page
    {
        #region declaraciones
        static string lsUsuario;
        static string lsNivel;
        static string lsPlaza;
        static string lsPuesto;
        static string lsSecretaria;
        static string lsOrganismo;
        static string lsUbicacion;
        static string lsArea;
        static string lsNombre;
        static string lsApPat;
        static string lsApMat;
        static string lsRFC;
        static string lsCargo;
        static string lsEmail;
        static string lsRol;
        static clsDictionary Dictionary = new clsDictionary();

        string lsClave;
        string lsDescripcion;
        string lsNombreCorto;
        string lsImagen;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Version"] != null)
                {
                    Carga_Session();
                    Carga_Valores();
                    clsFuncionesGral.LlenarTreeView(Dictionary.NUMERO_CERO, tvMenu, false, lsRol);
                }
            }
        }

        public void Carga_Valores()
        {
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = clsFuncionesGral.ConvertMayus(lsNombre + " " + lsApPat + " " + lsApMat);
            Label1.Text = clsFuncionesGral.ConvertMayus("Catalogo de Secretarias .");
            Label2.Text = clsFuncionesGral.ConvertMayus("Clave : ");
            Label3.Text = clsFuncionesGral.ConvertMayus("Descripción:");
            Label4.Text = clsFuncionesGral.ConvertMayus("Nombre Corto :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Logo :");

        }
        public void Carga_Session()
        {
            lsUsuario = Session["Crip_Usuario"].ToString();
            lsNivel = Session["Crip_Nivel"].ToString();
            lsPlaza = Session["Crip_Plaza"].ToString();
            lsPuesto = Session["Crip_Puesto"].ToString();
            lsSecretaria = Session["Crip_Secretaria"].ToString();
            lsOrganismo = Session["Crip_Organismo"].ToString();
            lsUbicacion = Session["Crip_Ubicacion"].ToString();
            lsArea = Session["Crip_Area"].ToString();
            lsNombre = Session["Crip_Nombre"].ToString();
            lsApPat = Session["Crip_ApPat"].ToString();
            lsApMat = Session["Crip_ApMat"].ToString();
            lsRFC = Session["Crip_RFC"].ToString();
            lsCargo = Session["Crip_Cargo"].ToString();
            lsEmail = Session["Crip_Email"].ToString();
            lsRol = Session["Crip_Rol"].ToString();

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string lsModulo;
            string lsRol = Session["Crip_Rol"].ToString();

            if (tvMenu.SelectedNode != null)
            {
                lsModulo = Convert.ToString(tvMenu.SelectedNode.Value);

                WebPage objWebPage = MngNegocioMenu.MngNegocioURL(lsModulo, lsRol);

                if (objWebPage != null)
                {
                    if (objWebPage.URL != Dictionary.CADENA_NULA)
                    {
                        if (objWebPage.Padre != Dictionary.NUMERO_CERO)
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                        if (objWebPage.Modulo == Dictionary.CERRAR_SESSION)
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                    }
                    else
                    {
                        //message de error por hacer
                    }

                }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Valida_Campos();
        }

        public void Valida_Campos()
        {
            if (txtClave.Text == Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Clave Obligatoria');", true);
                return;
            }
            else
            { lsClave = txtClave.Text; }

            if (txtDescripion.Text == Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Descripcion Obligatoria');", true);
                return;
            }
            else
            {
                lsDescripcion = txtDescripion.Text;
            }

            if (txtNomCorto.Text == Dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Descripcion Obligatoria');", true);
                return;
            }
            else
            {
                lsNombreCorto = txtNomCorto.Text;
            }
            if ((ImagenFile.PostedFile != null) && (ImagenFile.PostedFile.ContentLength > 0))
            {
                //obtener archivos subidos
                HttpPostedFile ImgFile = ImagenFile.PostedFile;
                // crear el array
                // Almacenamos la imagen en una variable para insertarla en la bd.//buscar la longitud y convertir en longitud byte
                Byte[] byteImage = new Byte[ImagenFile.PostedFile.ContentLength];
                //cargado en una matriz de bytes
               ImgFile.InputStream.Read(byteImage, 0, ImagenFile.PostedFile.ContentLength);

            string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
            bool x = MngNegocioImagen.Carga_Imagenes(lsClave, lsDescripcion, lsNombreCorto, lsHoy, byteImage );

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Imagen obligatoria');", true);
                return;
            }
        }
    }
}