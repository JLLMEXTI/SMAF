
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;

namespace InapescaWeb.Oficios
{
    public partial class Reimpresion_Oficio_Comision : System.Web.UI.Page
    {


        static clsDictionary Dictionary = new clsDictionary();

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
        static string lsAbreviatura;

        static string lsComisionado;
        static string lsAdscripcion;
        static string lsFolio;

        InapescaWeb.Entidades.Comision objComision = new  Entidades. Comision ();
        Ubicacion oUbicacion;
        Proyecto objProyecto;

        /// <summary>
        /// Evento load de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    Carga_Session();
                    Carga_Valores();
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, lsRol);// ConstruyeMenu();
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        /// <summary>
        /// Metodoe caga de daros de session
        /// </summary>
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
            lsAbreviatura = Session["Crip_Abreviatura"].ToString();
        }

        /// <summary>
        /// Metodo de caraga de valores iniciales de pagina
        /// </summary>
        public void Carga_Valores()
        {
            lnkUsuario.Text = lsNombre + " " + lsApPat + " " + lsApMat;
            lnkHome.Text = "INICIO";
            Label1.Text = clsFuncionesGral.ConvertMayus("Reimpresion de Oficio de Comision");
            Label2.Text = clsFuncionesGral.ConvertMayus("Folio");
            Label3.Text = clsFuncionesGral.ConvertMayus("Unidad : ");
            Label4.Text = clsFuncionesGral.ConvertMayus("Usuario :");
            Label5.Text = clsFuncionesGral.ConvertMayus("Año :");
            Button1.Text = clsFuncionesGral.ConvertMayus("Generar");

            dplUnidades .DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
            dplUnidades.DataTextField = Dictionary.DESCRIPCION;
            dplUnidades.DataValueField = Dictionary.CODIGO;
            dplUnidades.DataBind();

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();

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

        protected void dplUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsAdscripcion = dplUnidades .SelectedValue.ToString();
            if (lsAdscripcion == string.Empty)
            {
            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una unidad administrativa');", true);
                return;
            }
            else
            {
                dplUsuario .DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, lsUsuario, "", true);
                dplUsuario.DataTextField = Dictionary.DESCRIPCION;
                dplUsuario.DataValueField = Dictionary.CODIGO;
                dplUsuario.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lsFolio = txtFolio.Text;
            lsAdscripcion = dplUnidades.SelectedValue.ToString();
            string anio = dplAnio.SelectedValue.ToString();
            //  lsComisionado = dplUsuario.SelectedValue.ToString();

            if ((anio == string.Empty) | (anio == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Selecciones Año a buscar');", true);
                return;
            }

            if (lsAdscripcion == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una unidad administrativa');", true);
                return;
            }


            if (dplUsuario.SelectedIndex == 0)
            {/*
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un usuario a buscar');", true);
                return;*/
                lsComisionado = "";
            }
            else
            {
                lsComisionado = dplUsuario.SelectedValue.ToString();
            }

            if ((lsFolio == null) | (lsFolio == "") | (lsFolio == string.Empty ))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('folio fe oficio de comision obligatorio');", true);
                return;
            }

            if ((lsComisionado == Dictionary.CADENA_NULA) || (lsComisionado == ""))
            {
                objComision = MngNegocioComision.Detalle_Comision_Reimpresion1(lsFolio, lsAdscripcion, anio);
            }
            else
            {
                objComision = MngNegocioComision.Detalle_Comision_Reimpresion(lsFolio, lsAdscripcion, anio, lsComisionado);            
            }

            if(objComision.Folio == null )
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Datos no encontrados favor de validar');", true);
                return;
            }
            else
            {
                string rol_comisionado = MngNegocioUsuarios.Obtiene_Rol (objComision.Comisionado);
                Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(objComision.Ubicacion_Comisionado);

            /* if (objComision.Territorio == "3")
                {
                 //   CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas("42") + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
                    oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(objComision.Ubicacion_Autoriza);
                }
                else if ((objComision.Ubicacion_Autoriza == "24") | (objComision.Ubicacion_Autoriza == "25") | (objComision.Ubicacion_Autoriza == "42") | (objComision.Ubicacion_Autoriza == "44") | (objComision.Ubicacion_Autoriza == "45") | (objComision.Ubicacion_Autoriza == "43"))
                {
                   // CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(poComision.Ubicacion_Autoriza) + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
                    oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(objComision.Ubicacion_Autoriza);
                }
                else if (oDireccionTipo.Descripcion == Dictionary.SUBDIRECCIONES_GENERALES)
                {
                    //CLave_Oficio += MngNegocioDependencia.Obtiene_Siglas(oDireccionTipo.Codigo) + "-" + poComision.Oficio + "-" + Dictionary.YEAR;
                    oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(oDireccionTipo.Codigo);
                }
                else
                {
                    oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(objComision.Ubicacion_Autoriza);
                }
                */
               
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(objComision.Ubicacion_Comisionado );

            objProyecto = MngNegocioProyecto.ObtieneDatosProy(objComision .Dep_Proy,objComision.Proyecto, anio);

            clsPdf.Reimprime_Oficio_Comision(objComision, oUbicacion, objProyecto, rol_comisionado, oDireccionTipo, objComision.Ubicacion_Autoriza);
         
            }
          
        }
    }
}