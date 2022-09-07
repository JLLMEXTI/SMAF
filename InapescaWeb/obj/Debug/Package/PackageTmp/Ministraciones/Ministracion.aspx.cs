
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;
using Telerik.Web.UI;
using System.Data;
using System.IO;


namespace InapescaWeb.Ministraciones
{
    public partial class Ministracion : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static bool lbExisteProovedor = false;
        static string lsExisteFactura = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //  Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
            }
        }


        public void Carga_Valores()
        {
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lnkHome.Text = "INICIO";
       Label1.Text = clsFuncionesGral.ConvertMayus("Alta de Ministraciones :");
            /*     Label2.Text = clsFuncionesGral.ConvertMayus("RFC : ");
            Label4.Text = clsFuncionesGral.ConvertMayus("RFC : ");
            Label7.Text = clsFuncionesGral.ConvertMayus("Razon social :");
            Label9.Text = clsFuncionesGral.ConvertMayus("Calle : ");
            Label11.Text = clsFuncionesGral.ConvertMayus("Num. Ext. ");
            Label13.Text = clsFuncionesGral.ConvertMayus("Num. Int.");
            Label15.Text = clsFuncionesGral.ConvertMayus("Colonia : ");
            Label17.Text = clsFuncionesGral.ConvertMayus("Municipio / Delegacion");
            Label19.Text = clsFuncionesGral.ConvertMayus("Ciudad");
            Label21.Text = clsFuncionesGral.ConvertMayus("Estado : ");
            Label23.Text = clsFuncionesGral.ConvertMayus("Pais");
            Label5.Text = clsFuncionesGral.ConvertMayus("C.P.");
            Label8.Text = clsFuncionesGral.ConvertMayus("email :");
            Label10.Text = clsFuncionesGral.ConvertMayus("Contacto :");
            Label12.Text = clsFuncionesGral.ConvertMayus("Telefono Contacto :");
            Label14.Text = clsFuncionesGral.ConvertMayus("Telefono de Empresa 1 :");
            Label16.Text = clsFuncionesGral.ConvertMayus("Telefono de Empresa 2 :");
            Label20.Text = clsFuncionesGral.ConvertMayus("Regimen fiscal : ");
            Label22.Text = clsFuncionesGral.ConvertMayus("Servcios :");

            Label6.Text = clsFuncionesGral.ConvertMayus("Tipo MInistracion  : ");

            dplTipoMinistracion.DataSource = MngNegocioMinistracion.ListaTipoMinistracion("00");
            dplTipoMinistracion.DataValueField = Dictionary.CODIGO;
            dplTipoMinistracion.DataTextField = Dictionary.DESCRIPCION;
            dplTipoMinistracion.DataBind();

            Label18.Text = clsFuncionesGral.ConvertMayus("TIPO DE PAGO : ");
            CheckBox1.Text = clsFuncionesGral.ConvertMayus(" buscar proovedor leyendo CFDI ");
            Label27.Text = clsFuncionesGral.ConvertMayus("buscar proovedor por rfc");
            Label28.Text = clsFuncionesGral.ConvertMayus("buscar proovedor leyendo CFDI ");

            clsFuncionesGral.Activa_Paneles(pnlBuscaXRFC, false);
            clsFuncionesGral.Activa_Paneles(pnlProovedor, false);
            clsFuncionesGral.Activa_Paneles(pnlBuscarxXML, false);
            clsFuncionesGral.Activa_Paneles(pnlBusquedaProv, false);
            clsFuncionesGral.Activa_Paneles(pnlAgregaFac, false);

            Label24.Text = clsFuncionesGral.ConvertMayus("archivo pdf");
            Label25.Text = clsFuncionesGral.ConvertMayus("archivo xml");
            Label26.Text = clsFuncionesGral.ConvertMayus("Documentacion comprobatoria adicional en formato PDF que avalan factura ") + " (Opcional)";
            lnkBuscarProovedor.Text = "Leer Factura ";

            Label3.Text = clsFuncionesGral.ConvertMayus(" Agregar factura ");
            Label29.Text = clsFuncionesGral.ConvertMayus("archivo pdf");
            Label30.Text = clsFuncionesGral.ConvertMayus("archivo xml");
            Label31.Text = clsFuncionesGral.ConvertMayus("Documentacion comprobatoria adicional en formato PDF que avalan factura ") + " (Opcional)";
            btnCargarPago.Text = clsFuncionesGral.ConvertMayus("Cargar Pago a ministracion");
            btnCargarPago.Enabled = false;
            // btnCargarAGrid.Text = clsFuncionesGral.ConvertMayus("cargar a lista");
            */
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
                    if (objWebPage.URL != string.Empty)
                    {
                        if (objWebPage.Padre != "0")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                        if (objWebPage.Modulo == "99")
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

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        /// <summary>
        /// Evento click de link buttton de datos personales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

    }
}