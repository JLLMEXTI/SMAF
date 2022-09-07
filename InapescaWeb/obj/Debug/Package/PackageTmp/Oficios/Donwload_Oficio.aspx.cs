using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;

namespace InapescaWeb.Oficios
{
    public partial class Donwload_Oficio : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                string ConfSinAdmin = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(Session["Crip_Ubicacion"].ToString(), "SINADMIN");
                if ((Session["Crip_Rol"].ToString() == "ADMCRIPSC") || (Session["Crip_Rol"].ToString() == "SECREDIR") || ((Session["Crip_Rol"].ToString() == "JFCCRIPSC") && (Session["Crip_Ubicacion"].ToString() == ConfSinAdmin)))
                {
                    if (!IsPostBack)
                    {
                        clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());
                        Carga_Valores();

                    }
                }
                else
                {
                    Response.Redirect("../Home/Home.aspx", true);
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        public void Carga_Valores()
        {
            Label6.Text = clsFuncionesGral.ConvertMayus("Oficios de comison en su adscripcion");
            Label2.Text = clsFuncionesGral.ConvertMayus("Año :");
            
            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();
        }


        public void Crear_Tabla(string psPeriodo)
        {
            gvFiscales.DataSource = MngNegocioComision.Comisiones_Efectivas(Session["Crip_Ubicacion"].ToString(),psPeriodo );
            gvFiscales.DataBind();
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
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

        protected void gvFiscales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] cadenas = new string[3];
            string folio = gvFiscales.Rows[Convert.ToInt32(gvFiscales.SelectedIndex.ToString())].Cells[0].Text.ToString();
            string usuario = gvFiscales.Rows[Convert.ToInt32(gvFiscales.SelectedIndex.ToString())].Cells[1].Text.ToString();
            string status = gvFiscales.Rows[Convert.ToInt32(gvFiscales.SelectedIndex.ToString())].Cells[3].Text.ToString();
            
            string anio = dplAnio.SelectedValue.ToString();

            if ((anio == string.Empty) | (anio == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
                return;
            }

            if (status == "Cancelada")
            {
                status = "0";
            }
            else if (status == "Ministrada")
            {
                status = "9";
            }
            else if (status == "Comprobada sin validar")
            {
                status = "7";
            }
            else if (status == "Comprobada Validada")
            {
                status = "5";
            }

            Entidades.Comision DetalleComision = new Entidades.Comision();
            DetalleComision = MngNegocioComision.Detalle_Comision_Reimpresion(folio, Session["Crip_Ubicacion"].ToString(),anio, usuario, status );

            string rol_comisionado = MngNegocioUsuarios.Obtiene_Rol(DetalleComision.Comisionado);
            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(DetalleComision.Ubicacion_Comisionado);

            Entidades.Ubicacion oUbicacion = new Ubicacion();
            /*
            if (DetalleComision.Territorio == "3")
            {
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(DetalleComision.Ubicacion_Autoriza);
                // oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(objComision.Ubicacion_Autoriza);
            }
            else if ((DetalleComision.Ubicacion_Autoriza == "24") | (DetalleComision.Ubicacion_Autoriza == "25") | (DetalleComision.Ubicacion_Autoriza == "42") | (DetalleComision.Ubicacion_Autoriza == "44") | (DetalleComision.Ubicacion_Autoriza == "45") | (DetalleComision.Ubicacion_Autoriza == "43"))
            {
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(DetalleComision.Ubicacion_Autoriza);
            }
            else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
            {
                //CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(oDireccionTipo.Codigo) + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(oDireccionTipo.Codigo);
            }
            else
            {
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(DetalleComision.Ubicacion_Autoriza);
            }
            */

            oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(DetalleComision.Ubicacion_Comisionado);

            Entidades.Proyecto objProyecto = MngNegocioProyecto.ObtieneDatosProy(DetalleComision.Dep_Proy, DetalleComision.Proyecto, anio);

            clsPdf.Reimprime_Oficio_Comision(DetalleComision, oUbicacion, objProyecto, rol_comisionado, oDireccionTipo,DetalleComision.Ubicacion_Autoriza );

        }

    
        protected void dplAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvFiscales.DataSource = null;
            gvFiscales.DataBind();

            string anio = dplAnio.SelectedValue.ToString();
            
            if ((anio == string.Empty) | (anio == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
                return;
            }
            else
            {
                Crear_Tabla(anio);
            }
        }
    }
}