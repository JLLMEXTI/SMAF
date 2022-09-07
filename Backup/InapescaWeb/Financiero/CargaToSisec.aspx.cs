using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;

namespace InapescaWeb.Financiero
{
    public partial class CargaToSisec : System.Web.UI.Page
    {
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static List<Programa> listaProgramsDirecciones;
        static List<Entidad> ListaProyectoAdscripcion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
                Carga_Valores();
           //     CrearTabla();
            }
        }

        public void Carga_Valores()
        {
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();
            lnkHome.Text = "INICIO";
            lblTitle.Text = clsFuncionesGral.ConvertMayus("carga de datos a tabla programas para interface smaf-sisec");
            Label1.Text = clsFuncionesGral.ConvertMayus("Cargar comprometido acumulado");
            Label2.Text = clsFuncionesGral.ConvertMayus("cargar compromprobado acumulado");
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

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string trimestre = "";
            
            double ldAbb, ldAsc, ldAmzll, ldAmaztl, ldAptz, ldAlpz, ldAens, ldAgy,  ldAlrm, ldAycp, ldAvrz, ldAtmp, ldAcdc, ldAdgaa, ldAdgaipp, ldAdgaipa, ldAdgaia;
            double ldvBb, ldvSc, ldvMzll, ldvMaztl, ldvPtz, ldvLpz, ldvEns, ldvGy,  ldvLrm, ldvYcp, ldvVrz, ldvTmp, ldvCdc, ldvDgaa, ldvDgaipp, ldvDgaipa, ldvDgaia;

            trimestre = MngNegocioFinancieros.Obtiene_Trimestre(lsHoy );
            //obtiene direcciones
            List<Entidad> listaDirecciones = MngNegocioDirecciones.ObtienenDirecciones(year);

            //recorre para traer los programas de la direccion
            foreach (Entidad oEnt in listaDirecciones)
            {
                if (oEnt.Codigo != "0")
                { 
                 //Obtiene programas de direccion
                       listaProgramsDirecciones = MngNegociosProgramas.Obtiene_Programas(oEnt.Codigo );
                    //recorre lista de programas 
                    foreach (Programa oProg in listaProgramsDirecciones )
                    {
                        //obtiene lista de proyectos y dependencias del programa
                      ListaProyectoAdscripcion = MngNegocioProyecto.ListaProyectoAdcripcion(oEnt.Codigo , oProg.Id_Programa );
                    //recorre lista de proyectos
                      foreach (Entidad oProyecto in ListaProyectoAdscripcion)
                      { 
                      
                      }
                    }
                }
            }

        }
    }
}